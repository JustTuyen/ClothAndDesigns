
using BackEnd.Data;
using BackEnd.DTO.product;
using BackEnd.Models.others;
using BackEnd.Models.productions;
using BackEnd.Models.productions.tags;
using BackEnd.Service.media;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[ApiController]
[Route("api/[controller]")]
public class ProductModelsController : ControllerBase
{
    private readonly MyApplicationDBContext _context;
    private readonly IImageUploadService _imageUploadService;

    public ProductModelsController(MyApplicationDBContext context, IImageUploadService imageUploadService)
    {
        _context = context;
        _imageUploadService = imageUploadService;
    }

    // GET: PRODUCTMODELS
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAll()
    {
        var pros = await _context.Products
            .Include(x => x.Status)
            .Include(x => x.Discount)
            .Include(x => x.ImageGallaries)
            .Include(x => x.SubCategory)
            .Include(x => x.ProductTags)
                .ThenInclude(x => x.Tag)
            .OrderBy(x => x.Name)
            .ToListAsync();

        if (pros.Count == 0) return Ok(new { message = "no pros in db" });

        var dto = pros.Select(pro => new Product
        {
            Id = pro.Id,
            Name = pro.Name,
            Description = pro.Description,
            BasePrice = pro.BasePrice,
            Slug = pro.Slug,
            CreatedAt = pro.CreatedAt,
            UpdatedAt = pro.UpdatedAt,
            ViewCount = pro.ViewCount,
            LikeCount = pro.LikeCount,
            DiscountPercentage = pro.Discount?.Percentage,
            SubCatName = pro.SubCategory?.Name,
            StatusName = pro.Status?.Name,

        }).ToList();

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var pro = await _context.Products
            .Include(x => x.Status)
            .Include(x => x.Discount)
            .Include(x => x.ImageGallaries)
            .Include(x => x.SubCategory)
            .Include(x => x.ProductTags)
                .ThenInclude(x => x.Tag)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (pro == null) return BadRequest("no product with the id");

        var dto = new Product
        {
            Id = pro.Id,
            Name = pro.Name,
            Description = pro.Description,
            BasePrice = pro.BasePrice,
            Slug = pro.Slug,
            CreatedAt = pro.CreatedAt,
            UpdatedAt = pro.UpdatedAt,
            ViewCount = pro.ViewCount,
            LikeCount = pro.LikeCount,
            DiscountPercentage = pro.Discount?.Percentage,
            SubCatName = pro.SubCategory?.Name,
            StatusName = pro.Status?.Name,
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<CreateProduct>> Create([FromForm] CreateProduct dto)
    {
        if (dto == null) return BadRequest("Dto or request data is missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var sta = await _context.Statuses
            .FirstOrDefaultAsync(x => x.Name == "active" && x.Type == "product");
        if (sta == null) return Problem($"Không tìm thấy sta", statusCode: 500);

        var sub = await _context.SubCategories
            .FirstOrDefaultAsync(x => x.Id == dto.SubCategoryId);
        if (sub == null) return NotFound($"Không tìm thấy sub cats");

        if (dto.DiscountId.HasValue)
        {
            var discountValid = await _context.Discounts
                .AnyAsync(x =>
                    x.Id == dto.DiscountId.Value &&
                    x.Status.Name.ToLower() == "active" &&
                    x.EndDate > DateTime.UtcNow);
            if (!discountValid)
                return BadRequest("Discount không tồn tại, không active hoặc đã hết hạn.");
        }

        if (await _context.Products.AnyAsync(p => p.Slug == dto.Slug))
            return Conflict($"Slug '{dto.Slug}' đã tồn tại.");


        var tagIds = dto.TagIds?.Distinct().ToList() ?? new List<int>();
        if (tagIds.Count > 0)
        {
            var existingIds = await _context.Tags
                .Where(t => tagIds.Contains(t.Id))
                .Select(t => t.Id)
                .ToListAsync();

            var missing = tagIds.Except(existingIds).ToList();
            if (missing.Count > 0)
                return BadRequest($"Invalid TagIDs: {string.Join(", ", missing)}");
        }

        var uploadedKeys = new List<string>();
        var imageGalleries = new List<ImageGallaryModel>();

        try
        {
            if (dto.Images != null && dto.Images.Any())
            {
                int displayOrder = 0;
                foreach (var img in dto.Images)
                {
                    var uploadResult = await _imageUploadService.UploadAsync(img);
                    uploadedKeys.Add(uploadResult.Key);

                    imageGalleries.Add(new ImageGallaryModel
                    {
                        Image = new ImageModel
                        {
                            URL = uploadResult.Url,
                            PublicId = uploadResult.Key,
                            Width = uploadResult.Width,
                            Height = uploadResult.Height,
                            SizeBytes = uploadResult.SizeBytes,
                            Type = uploadResult.ContentType,
                            AltText = dto.AltText,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        IsThumbnail = displayOrder == 0,
                        DisplayOrder = displayOrder++
                    });
                }
            }


            var pro = new ProductModel
            {
                Name = dto.Name,
                Description = dto.Description,
                BasePrice = dto.BasePrice,
                Slug = dto.Slug,
                SubCategoryId = sub.Id,
                StatusId = sta.Id,
                DiscountId = dto.DiscountId,
                ImageGallaries = imageGalleries,
                ProductTags = tagIds.Select(id => new ProductTagModel { TagId = id }).ToList()
            };

            _context.Products.Add(pro);
            await _context.SaveChangesAsync();

            var resultdto = new ResultProduct

            {
                Id = pro.Id,
                Name = pro.Name
            };

            return CreatedAtAction(nameof(GetById), new { id = pro.Id }, resultdto);
        }
        //check error of upload
        catch
        {
            foreach (var key in uploadedKeys)
                await _imageUploadService.DeleteAsync(key);
            throw;
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateProduct>> Update(int id, [FromForm] UpdateProduct dto)
    {
        if (dto == null) return BadRequest("Dto or request data is missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var pro = await _context.Products
        .Include(p => p.ProductTags)          
        .FirstOrDefaultAsync(x => x.Id == id);
        if (pro == null) return NotFound("Không tìm thấy product với id này.");

        if (!await _context.SubCategories.AnyAsync(x => x.Id == dto.SubCategoryId))
            return BadRequest("Không tìm thấy subcategory.");

        if (await _context.Products.AnyAsync(p => p.Slug == dto.Slug && p.Id != id))
            return Conflict($"Slug '{dto.Slug}' đã tồn tại.");

        if (dto.TagIds != null)
        {
            var newTagIds = dto.TagIds.Distinct().ToList();

            var existingIds = await _context.Tags
                .Where(t => newTagIds.Contains(t.Id))
                .Select(t => t.Id)
                .ToListAsync();

            var missing = newTagIds.Except(existingIds).ToList();
            if (missing.Count > 0)
                return BadRequest($"Invalid TagIDs: {string.Join(", ", missing)}");

            var toRemove = pro.ProductTags.Where(pt => !newTagIds.Contains(pt.TagId)).ToList();
            _context.productTags.RemoveRange(toRemove);

            var currentIds = pro.ProductTags.Select(pt => pt.TagId).ToHashSet();
            foreach (var tagId in newTagIds.Where(t => !currentIds.Contains(t)))
                pro.ProductTags.Add(new ProductTagModel { TagId = tagId });
        }

        pro.Name = dto.Name;
        pro.Description = dto.Description;
        pro.BasePrice = dto.BasePrice;
        pro.Slug = dto.Slug;
        pro.SubCategoryId = dto.SubCategoryId;
        pro.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult<Product>> Delete(int id)
    {
        var pro = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (pro == null) return BadRequest("No product with this id");

        _context.Products.Remove(pro);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    //
    [HttpPut("/product/status/{id}")]
    public async Task<ActionResult<ProductStatus>> UpdateStatus(int id, [FromForm] ProductStatus dto)
    {
        if(dto == null) return BadRequest("Dto or request data is missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var pro = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == id);
        if (pro == null) return NotFound("Không tìm thấy product với id này.");

        var sta = await _context.Statuses
            .FirstOrDefaultAsync(x => x.Type == "product" && x.Id == id);
        if (sta == null) return BadRequest("Không tìm thấy status cho product với id này.");

        pro.StatusId = sta.Id;
        pro.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("/product/discount/{id}")]
    public async Task<ActionResult<ProductDiscount>> UpdateDiscount(int id, [FromForm] ProductDiscount dto)
    {
        if (dto == null) return BadRequest("Dto or request data is missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var pro = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == id);
        if (pro == null) return NotFound("Không tìm thấy product với id này.");

        if (dto.DiscountId == 0 )
        {
            pro.DiscountId = null;
        }
        else
        {
            var discountValid = await _context.Discounts.AnyAsync(x =>
            x.Id == dto.DiscountId &&
            x.Status.Name.ToLower() == "active" &&
            x.EndDate > DateTime.UtcNow);

            if (!discountValid)
                return BadRequest("Discount không tồn tại, không active hoặc đã hết hạn.");

            pro.DiscountId = dto.DiscountId;
        }

        pro.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("/product/images/{id}")]
    public async Task<ActionResult<ProductImages>> UpdateImage(int id, [FromForm] ProductImages dto)
    {
        if (dto == null) return BadRequest("Dto or request data is missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        if (dto.Images == null || !dto.Images.Any())
            return BadRequest("Chưa có ảnh nào được gửi lên.");

        var pro = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == id);
        if (pro == null) return NotFound("Không tìm thấy product với id này.");

        var productImages = _context.ImageGallaries
            .Where(g => g.ProductId == id && g.VariationId == null);

        var nextOrder = (await productImages.MaxAsync(g => (int?)g.DisplayOrder) ?? -1) + 1;
        var hasThumbnail = await productImages.AnyAsync(g => g.IsThumbnail);
        var uploadedKeys = new List<string>();
        try
        {
            foreach (var img in dto.Images)
            {
                var uploadResult = await _imageUploadService.UploadAsync(img);
                uploadedKeys.Add(uploadResult.Key);

                _context.ImageGallaries.Add(new ImageGallaryModel
                {
                    ProductId = id,                        
                    Image = new ImageModel
                    {
                        URL = uploadResult.Url,
                        PublicId = uploadResult.Key,
                        Width = uploadResult.Width,
                        Height = uploadResult.Height,
                        SizeBytes = uploadResult.SizeBytes,
                        Type = uploadResult.ContentType,
                        AltText = dto.AltText,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    IsThumbnail = !hasThumbnail,            
                    DisplayOrder = nextOrder++
                });
                hasThumbnail = true;                        
            }

            pro.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();               
            return NoContent();
        }
        catch
        {
            foreach (var key in uploadedKeys)
                await _imageUploadService.DeleteAsync(key);
            throw;
        }
    }

    //
    [HttpGet("filter")]
    public async Task<ActionResult<List<ListingProduct>>> Filter([FromQuery] ProductFilter filter)
    {
        var now = DateTime.UtcNow;
        var page = Math.Max(filter.Page, 1);
        var pageSize = Math.Clamp(filter.PageSize, 1, 100);

        var query = _context.Products
            .Where(p => p.Status!.Name.ToLower() == "active");

        if (filter.MinPrice.HasValue)
            query = query.Where(p => p.BasePrice >= filter.MinPrice);

        if (filter.MaxPrice.HasValue)
            query = query.Where(p => p.BasePrice <= filter.MaxPrice);

        if (filter.CreatedFrom.HasValue)
            query = query.Where(p => p.CreatedAt >= filter.CreatedFrom);

        if (filter.CreatedTo.HasValue)
            query = query.Where(p => p.CreatedAt <= filter.CreatedTo);

        if (filter.MinViewCount.HasValue)
            query = query.Where(p => p.ViewCount >= filter.MinViewCount);

        if (!string.IsNullOrWhiteSpace(filter.Keyword))
        {
            var keyword = filter.Keyword.Trim().ToLower();
            query = query.Where(p => p.Name.ToLower().Contains(keyword));
        }

        if (filter.HasDiscount == true)
            query = query.Where(p => p.Discount != null
                && p.Discount.StartDate <= now
                && p.Discount.EndDate >= now);

        var ordered = filter.SortBy?.ToLower() switch
        {
            "viewcount" => filter.SortDescending
                ? query.OrderByDescending(p => p.ViewCount)
                : query.OrderBy(p => p.ViewCount),

            "price" => filter.SortDescending
                ? query.OrderByDescending(p => p.BasePrice)
                : query.OrderBy(p => p.BasePrice),

            "discount" => filter.SortDescending
                ? query.OrderByDescending(p => p.Discount != null
                    && p.Discount.StartDate <= now && p.Discount.EndDate >= now
                    ? p.Discount.Percentage : 0)
                : query.OrderBy(p => p.Discount != null
                    && p.Discount.StartDate <= now && p.Discount.EndDate >= now
                    ? p.Discount.Percentage : 0),

            _ => filter.SortDescending
                ? query.OrderByDescending(p => p.CreatedAt)
                : query.OrderBy(p => p.CreatedAt)
        };

        var sorted = ordered.ThenBy(p => p.Id);   // thứ tự ổn định giữa các trang

        var totalCount = await sorted.CountAsync();
        var pros = await sorted
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ListingProduct
            {
                Id = p.Id,
                Name = p.Name,
                BasePrice = p.BasePrice,
                DiscountPercentage = p.Discount != null
                    && p.Discount.StartDate <= now && p.Discount.EndDate >= now
                    ? p.Discount.Percentage : 0m,
                ImageURL = p.ImageGallaries
                    .Where(ig => ig.VariationId == null)
                    .OrderByDescending(ig => ig.IsThumbnail)
                    .ThenBy(ig => ig.DisplayOrder)
                    .Select(ig => ig.Image!.URL)
                    .FirstOrDefault()
            })
            .ToListAsync();

        Response.Headers.Append("X-Total-Count", totalCount.ToString());
        return Ok(pros);
    }
}
