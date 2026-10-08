
using Amazon.S3.Model;
using BackEnd.Data;
using BackEnd.DTO.other;
using BackEnd.Models.others;
using BackEnd.Service.media;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

[ApiController]
[Route("api/[controller]")]
public class BannerModelsController : ControllerBase
{
    private readonly MyApplicationDBContext _context;
    private readonly IImageUploadService _imageUploadService;

    public BannerModelsController(MyApplicationDBContext context, IImageUploadService imageUploadService)
    {
        _context = context;
        _imageUploadService = imageUploadService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Banner>>> GetAll()
    {
        var banners = await _context.Banners
            .Include(x => x.Image)
            .Include(x => x.Status)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync();

        if (banners.Count == 0) return NotFound($"Không tìm thấy Banner");
        var dto = banners.Select(banner => new Banner
        {
            Id = banner.Id,
            Name = banner.Name,
            Description = banner?.Description,
            Note = banner?.Note,
            DisplayOrder = banner.DisplayOrder,
            IsThumbnail = banner.IsThumbnail,
            URL = banner.Image?.URL,
            StatusName = banner.Status?.Name,
            CreatedAt = banner.CreatedAt,
            UpdatedAt = banner.UpdatedAt,
        }).ToList();

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Banner>> GetById(int id)
    {
        var banner = await _context.Banners
            .Include(x => x.Image)
            .Include(x => x.Status)
            .OrderBy(x => x.DisplayOrder)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (banner==null) return NotFound($"Không tìm thấy Banner");
        var dto = new Banner
        {
            Id = banner.Id,
            Name = banner.Name,
            Description = banner?.Description,
            Note = banner?.Note,
            DisplayOrder = banner.DisplayOrder,
            IsThumbnail = banner.IsThumbnail,
            URL = banner.Image.URL,
            StatusName = banner.Status.Name,
            CreatedAt = banner.CreatedAt,
            UpdatedAt = banner.UpdatedAt,
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task <ActionResult<CreateBanner>> Create([FromForm] CreateBanner dto)
    {
        if (dto == null) return BadRequest("Dto or request data is missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var sta = await _context.Statuses
            .Where(x => x.Name == "active" && x.Type == "banner")
            .FirstOrDefaultAsync();
        if (sta == null) return NotFound($"Không tìm thấy sta");

        ImageModel? image = null;
        if(dto.Image != null)
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

        var banner = new BannerModel
        {
            Name = dto.Name,
            Description = dto.Description,
            Note = dto.Note,
            DisplayOrder = dto.DisplayOrder,
            IsThumbnail = dto.IsThumbail,
            Image = image,
            StatusId = sta.Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Banners.Add(banner);
        await _context.SaveChangesAsync();

        var resultDto = new Banner
        {
            Id = banner.Id,
            Name = banner.Name,
            DisplayOrder = banner.DisplayOrder,
            URL = banner.Image.URL
        };

        return CreatedAtAction(nameof(GetById), new { id = banner.Id }, resultDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateBanner>> UpdateInfo(int id, [FromForm] UpdateBanner dto)
    {
        if (dto == null) return BadRequest("Dto or request data is missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var banner = await _context.Banners
            .FirstOrDefaultAsync(x => x.Id == id);

        if (banner == null) return NotFound($"Không tìm thấy Banner");

        banner.Name = dto.Name;
        banner.Description = dto.Description;
        banner.Note = dto.Note;
        banner.DisplayOrder = dto.DisplayOrder;
        banner.IsThumbnail = dto.IsThumbail;
        banner.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Banner>> Delete(int id)
    {
        var banner = await _context.Banners
            .FirstOrDefaultAsync(x => x.Id == id);
        if (banner == null) return NotFound($"Không tìm thấy Banner");

        _context.Banners.Remove(banner);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    //puts
    [HttpPut("/banner/Image/{id}")]
    public async Task<ActionResult<UpdateImgBanner>> UpdateImage(int id, [FromForm] UpdateImgBanner dto)
    {
        if (dto == null) return BadRequest("Dto or request data is missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var banner = await _context.Banners
            .FirstOrDefaultAsync(x => x.Id == id);
        if (banner == null) return NotFound($"Không tìm thấy Banner");

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
                AltText = dto.Alttest
            };

            _context.Images.Add(newImg);

            if (banner.Image != null)
            {
                await _imageUploadService.DeleteAsync(banner.Image.PublicId);
                _context.Images.Remove(banner.Image);
            }

            banner.Image = newImg;
            banner.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("/banner/status/{id}")]
    public async Task<ActionResult<UpdateImgBanner>> UpdateStatus(int id, [FromForm] UpdateStatusBanner dto)
    {
        if (dto == null) return BadRequest("Dto or request data is missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var banner = await _context.Banners
            .FirstOrDefaultAsync(x => x.Id == id);
        if (banner == null) return NotFound($"Không tìm thấy Banner");

        var sta = await _context.Statuses.Where(x => x.Type == "banner").FirstOrDefaultAsync(x => x.Id == id);
        if (sta == null) return BadRequest("Status ko danh cho banner");

        banner.StatusId = dto.StatusId;
        banner.UpdatedAt = DateTime.UtcNow;

        return NoContent();
    }

    //customized gets
    [HttpGet("listing")]
    public async Task<ActionResult<List<BannerSilder>>> Listing()
    {
        var banners = await _context.Banners
            .Where(x => x.Status.Name == "active")
            .Include(x => x.Image)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync();

        if (banners.Count == 0) return NotFound("Khong tim dc banner nao dang active");

        var dto = banners.Select(banner => new BannerSilder
        {
            Id = banner.Id,
            URL = banner.Image.URL,
            Name = banner.Name

        }).ToList();

        return Ok(dto);
    }
}
