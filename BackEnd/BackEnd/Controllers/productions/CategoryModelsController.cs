
using BackEnd.Data;
using BackEnd.DTO.other;
using BackEnd.DTO.product;
using BackEnd.Models.others;
using BackEnd.Models.productions;
using BackEnd.Service.media;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
[ApiController]
[Route("api/[controller]")]
public class CategoryModelsController : Controller
{
    private readonly MyApplicationDBContext _context;
    private readonly IImageUploadService _imageUploadService;

    public CategoryModelsController(MyApplicationDBContext context, IImageUploadService imageUploadService)
    {
        _context = context;
        _imageUploadService = imageUploadService;
    }

    // GET: CATEGORYMODELS
    [HttpGet]
    public async Task <ActionResult<Task<Category>>> GetAll()
    {
        var cats = await _context.Categories
            .Include(x => x.Image)
            .Include(x => x.Status)
            .ToListAsync();

        if (cats.Count == 0) return NotFound("ko tim thay cats nao :/");

        var dto = cats.Select(cat => new Category
        {
            Id = cat.Id,
            Name = cat.Name,
            Description = cat.Description,
            CreatedAt = cat.CreatedAt,
            UpdatedAt = cat.UpdatedAt,
            URL = cat.Image.URL,
            AltText = cat.Image.AltText,
            StatusName = cat.Status.Name
        }).ToList();

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetById(int id)
    {
        var cat = await _context.Categories
            .Include(x => x.Image)
            .Include(x => x.Status)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (cat == null) return NotFound("ko cat nao dc tim thay voi id nay");

        var dto = new Category
        {
            Id = cat.Id,
            Name = cat.Name,
            Description = cat.Description,
            CreatedAt = cat.CreatedAt,
            UpdatedAt = cat.UpdatedAt,
            URL = cat.Image.URL,
            AltText = cat.Image.AltText,
            StatusName = cat.Status.Name
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<CreateCategory>> Create([FromForm] CreateCategory dto)
    {
        if (dto == null) return BadRequest("Dto or request data is missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var sta = await _context.Statuses
            .Where(x => x.Name == "active" && x.Type == "category")
            .FirstOrDefaultAsync();
        if (sta == null) return NotFound($"Không tìm thấy sta");

        ImageModel? image = null;
        if (dto.Image != null)
        {
            var uploadResult = await _imageUploadService.UploadAsync(dto.Image);
            image = new ImageModel
            {
                URL = uploadResult.Url,
                PublicId = uploadResult.Key,
                Width = uploadResult.Width,
                Height = uploadResult.Height,
                Type = dto.Image.ContentType,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                SizeBytes = uploadResult.SizeBytes,
                AltText = dto.AltText
            };

            _context.Images.Add(image);
        }

        var cat = new CategoryModel
        {
            Name = dto.Name,
            Description = dto.Description,
            Image = image,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            StatusId = sta.Id
        };

        _context.Categories.Add(cat);
        await _context.SaveChangesAsync();

        var resultdto = new CatResultDTO
        {
            Id = cat.Id,
            URL = cat.Image.URL,
            Name = cat.Name
        };

        return CreatedAtAction(nameof(GetById), new { id = cat.Id }, resultdto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateCat>> Update(int id, [FromForm] UpdateCat dto)
    {
        if (dto == null) return BadRequest("Dto or request data is missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var cat = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (cat == null) return NotFound($"Không tìm thấy Category");

        cat.Name = dto.Name;
        cat.Description = dto.Description;
        cat.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Category>> Delete(int id)
    {
        var cat = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (cat == null) return NotFound($"Không tìm thấy Category");

        _context.Categories.Remove(cat);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    //
    [HttpGet("listing")]
    public async Task<ActionResult<List<ListingCat>>> Listing()
    {
        var cats = await _context.Categories.Where(x => x.Status.Name == "active")
            .Include(x => x.Image)
            .Include(x => x.Status)
            .ToListAsync();

        var dto = cats.Select(cat => new ListingCat
        {
            Id = cat.Id,
            Name = cat.Name,
            URL = cat.Image?.URL,
            AltText = cat.Image?.AltText
        }).ToList();

        return Ok(dto);
    }

    [HttpPut("/category/Image/{id}")]
    public async Task<ActionResult<UpdateImage>> UpdateImg(int id, [FromForm] UpdateImage dto)
    {
        var cat = await _context.Categories
           .FirstOrDefaultAsync(x => x.Id == id);

        if (cat == null) return NotFound($"Không tìm thấy Category");

        if (dto.Image != null)
        {
            var uploadResult = await _imageUploadService.UploadAsync(dto.Image);
            var newImg = new ImageModel
            {
                URL = uploadResult.Url,
                PublicId = uploadResult.Key,
                Width = uploadResult.Width,
                Height = uploadResult.Height,
                Type = dto.Image.ContentType,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                SizeBytes = uploadResult.SizeBytes,
                AltText = dto.AltText
            };

            _context.Images.Add(newImg);

            if (cat.Image != null)
            {
                await _imageUploadService.DeleteAsync(cat.Image.PublicId);
                _context.Images.Remove(cat.Image);
            }

            cat.Image = newImg;
            cat.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("/category/statuc/{id}")]
    public async Task<ActionResult<CatUpdateStatus>> UpdateStatus(int id, [FromForm] CatUpdateStatus dto)
    {
        if (dto == null) return BadRequest("Dto or request data is missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var cat = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == id);
        if (cat == null) return NotFound($"Không tìm thấy category");

        var sta = await _context.Statuses.Where(x => x.Type == "category").FirstOrDefaultAsync(x => x.Id == id);
        if (sta == null) return BadRequest("Status ko danh cho category");

        cat.StatusId = dto.StatusId;
        cat.UpdatedAt = DateTime.UtcNow;

        return NoContent();
    }
}
