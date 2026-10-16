
using BackEnd.Data;
using BackEnd.DTO.other;
using BackEnd.DTO.product;
using BackEnd.Models.others;
using BackEnd.Models.productions;
using BackEnd.Service.media;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[ApiController]
[Route("api/[controller]")]
public class DiscountModelsController : ControllerBase
{
    private readonly MyApplicationDBContext _context;
    private readonly IImageUploadService _imageUploadService;

    public DiscountModelsController(MyApplicationDBContext context, IImageUploadService imageUploadService)
    {
        _context = context;
        _imageUploadService = imageUploadService;
    }

    // GET: DISCOUNTMODELS
    [HttpGet]
    public async Task<ActionResult<List<Discount>>> GetAll()
    {
        var discounts = await _context.Discounts
            .Include(x => x.Status)
            .Include(x => x.Image)
            .OrderBy(x => x.StatusId)
            .ToListAsync();

        if (discounts.Count == 0) return Ok(new { message = "no discount in db" });

        var dto = discounts.Select(dis => new Discount
        {
            Id = dis.Id,
            Name = dis.Name,
            Description = dis.Description,
            StatusName = dis.Status.Name,
            ImgURL = dis.Image.URL,
            StartDate = dis.StartDate,
            DurationInDays = dis.DurationInDays,
            EndDate = dis.EndDate,
            CreatedAt = dis.CreatedAt,
            UpdatedAt = dis.UpdatedAt
        }).ToList();

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Discount>> GetById(int id)
    {
        var dis = await _context.Discounts
            .Include(x => x.Status)
            .Include(x => x.Image)
            .OrderBy(x => x.StatusId)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (dis == null) return BadRequest("No discount with this id");

        var dto = new Discount
        {
            Id = dis.Id,
            Name = dis.Name,
            Description = dis.Description,
            StatusName = dis.Status.Name,
            ImgURL = dis.Image.URL,
            StartDate = dis.StartDate,
            DurationInDays = dis.DurationInDays,
            EndDate = dis.EndDate,
            CreatedAt = dis.CreatedAt,
            UpdatedAt = dis.UpdatedAt
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<CreateDiscount>> CreateDiscount([FromForm] CreateDiscount dto)
    {
        if (dto == null) return BadRequest("Dto or request data is missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var sta = await _context.Statuses
            .FirstOrDefaultAsync(x => x.Name == "active" && x.Type == "discount");
        if (sta == null) return BadRequest("no status for disocunt");

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

        var dis = new DiscountModel
        {
            Name = dto.Name,
            Description = dto.Description,
            Percentage = dto.Percentage,
            Image = image,
            StatusId = sta.Id,
            StartDate = dto.StartDate,
            DurationInDays = dto.DurationInDays,
            EndDate = dto.StartDate.AddDays(dto.DurationInDays),
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };


        _context.Discounts.Add(dis);
        await _context.SaveChangesAsync();

        var resultdto = new ResultDiscount
        {
            Id = dis.Id,
            Name = dis.Name,
            Percentage = dis.Percentage
        };

        return CreatedAtAction(nameof(GetById), new { id = dis.Id }, resultdto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateDiscount>> UpdateDiscount(int id, [FromForm] UpdateDiscount dto)
    {
        if (dto == null) return BadRequest("Dto or request data is missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var dis = await _context.Discounts
            .FirstOrDefaultAsync(x => x.Id == id);
        if (dis == null) return BadRequest("no discount with this id");

        dis.Name = dto.Name;
        dis.Description = dto.Description;
        dis.Percentage = dto.Percentage;
        dis.StartDate = dto.StartDate;
        dis.DurationInDays = dto.DurationInDays;
        dis.EndDate = dto.StartDate.AddDays(dto.DurationInDays);
        dis.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Discount>> Delete(int id)
    {
        var dis = await _context.Discounts
            .FirstOrDefaultAsync(x => x.Id == id);
        if (dis == null) return BadRequest("no discount with this id");

        _context.Discounts.Remove(dis);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    //
    [HttpPut("/discount/status/{id}")]
    public async Task<ActionResult<DiscountStatus>> UpdateStatus(int id, [FromForm] DiscountStatus dto)
    {
        if (dto == null) return BadRequest("Dto or request data is missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var dis = await _context.Discounts
            .FirstOrDefaultAsync(x => x.Id == id);
        if (dis == null) return BadRequest("no discount with this id");

        var sta = await _context.Statuses
            .FirstOrDefaultAsync(x => x.Type == "discount" && x.Id == dto.StatusId);
        if (sta == null) return BadRequest("this status is not for discount");

        dis.StatusId = sta.Id;
        dis.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("/discount/image/{id}")]
    public async Task<ActionResult<DiscountImage>> UpdateImage(int id, [FromForm] DiscountImage dto)
    {

        if (dto == null) return BadRequest("Dto or request data is missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var dis = await _context.Discounts
            .FirstOrDefaultAsync(x => x.Id == id);
        if (dis == null) return BadRequest("no discount with this id");

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

            if (dis.Image != null)
            {
                await _imageUploadService.DeleteAsync(dis.Image.PublicId);
                _context.Images.Remove(dis.Image);
            }

            dis.Image = newImg;
            dis.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("listing")]
    public async Task<ActionResult<List<DiscountListing>>> Listing()
    {
        var discounts = await _context.Discounts
            .Where(x => x.Status.Name == "active")
            .Include(x => x.Status)
            .Include(x => x.Image)
            .OrderBy(x => x.StatusId)
            .ToListAsync();

        if (discounts.Count == 0) return Ok(new { message = "no discount in db" });

        var dto = discounts.Select(dis => new DiscountListing
        {
            Id = dis.Id,
            Name = dis.Name,
            ImgURL = dis.Image.URL,
        }).ToList();

        return Ok(dto);
    }
}
