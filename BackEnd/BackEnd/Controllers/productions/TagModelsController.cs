
using BackEnd.Data;
using BackEnd.DTO.produc;
using BackEnd.DTO.user;
using BackEnd.Models.productions.tags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

[ApiController]
[Route("api/[controller]")]
public class TagModelsController : Controller
{
    private readonly MyApplicationDBContext _context;

    public TagModelsController(MyApplicationDBContext context)
    {
        _context = context;
    }

    // GET: TAGMODELS
    [HttpGet]
    public async Task<ActionResult<List<Tag>>> GetAll()
    {
        var tags = await _context.Tags
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();

        if (tags.Count == 0) return Ok(new { message = "No tafs is found" });
        var dto = tags.Select(tag => new Tag
        {
            Id = tag.Id,
            Name = tag.Name,
            CreatedAt = tag.CreatedAt,
            UpdatedAt = tag.UpdatedAt
        }).ToList();

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tag>> GetById(int id)
    {
        var tag = await _context.Tags
            .FirstOrDefaultAsync(x => x.Id == id);
        if (tag == null) return NotFound(new { message = "No tafs is found" });
        var dto = new Tag
        {
            Id = tag.Id,
            Name = tag.Name,
            CreatedAt = tag.CreatedAt,
            UpdatedAt = tag.UpdatedAt
        };
        return Ok(dto);

    }

    [HttpPost]
    public async Task<ActionResult<CreateTag>> Create([FromForm] CreateTag dto)
    {
        if (dto == null) return BadRequest("Dto is null or missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var tag = new TagModel
        {
            Name = dto.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();

        var result = new ListingTag
        {
            Id = tag.Id,
            Name = tag.Name
        };

        return CreatedAtAction(nameof(GetById), new { id = tag.Id }, result);
    }

    [HttpPut("id")]
    public async Task<ActionResult<UpdateTag>> Update(int id, [FromForm] UpdateTag dto)
    {
        if (dto == null) return BadRequest("Dto is null or missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var tag = await _context.Tags
           .FirstOrDefaultAsync(x => x.Id == id);
        if (tag == null) return NotFound(new { message = "No tafs is found" });

        tag.Name = dto.Name;
        tag.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Tag>> Delete(int id)
    {
        var tag = await _context.Tags
          .FirstOrDefaultAsync(x => x.Id == id);
        if (tag == null) return NotFound(new { message = "No tafs is found" });

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    //
    [HttpGet("listing")]
    public async Task<ActionResult<List<ListingTag>>> Listing()
    {
        var tags = await _context.Tags
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();

        if (tags.Count == 0) return Ok(new { message = "No tafs is found" });
        var dto = tags.Select(tag => new ListingTag
        {
            Id = tag.Id,
            Name = tag.Name,
        }).ToList();

        return Ok(dto);
    }
}
