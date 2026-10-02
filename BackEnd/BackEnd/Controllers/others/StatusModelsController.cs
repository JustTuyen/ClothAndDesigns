
using BackEnd.Data;
using BackEnd.DTO.other;
using BackEnd.Models.others;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

[ApiController]
[Route("api/[controller]")]
public class StatusModelsController : ControllerBase
{
    private readonly MyApplicationDBContext _context;

    public StatusModelsController(MyApplicationDBContext context)
    {
        _context = context;
    }

    // GET: STATUSMODELS
    [HttpGet]
    public async Task<ActionResult<List<Status>>> GetAll()
    {
        var stas = await _context.Statuses
            .OrderBy(p => p.Type)
            .ToListAsync();

        if (stas.Count == 0) return NotFound($"Không tìm thấy status");
        
        var dto = stas.Select(sa => new Status
        {
            id = sa.Id,
            Name = sa.Name,
            Type = sa.Type
        }).ToList();

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<Status>>> GetById(int id)
    {
        var sta = await _context.Statuses.FirstOrDefaultAsync(s => s.Id == id);
        if (sta == null) return BadRequest("no status with this id is found!");

        var dto = new Status
        {
            id = sta.Id,
            Name = sta.Name,
            Type = sta.Type
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<CreateStatus>> Create([FromForm] CreateStatus dto)
    {
        var sta = new StatusModel
        {
            Name = dto.Name,
            Type = dto.Type,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Statuses.Add(sta);
        await _context.SaveChangesAsync();

        var resultDto = new Status
        {
            id = sta.Id,
            Name = sta.Name,
            Type = sta.Type
        };

        return CreatedAtAction(nameof(GetById), new { id = sta.Id }, resultDto);
    }

    [HttpGet("${type}")]
    public async Task<ActionResult<List<Status>>> GetByType(string type)
    {
        var normalizedType = type.Trim().ToLowerInvariant();

        var stas = await _context.Statuses
            .Where(s => s.Type.ToLower() == normalizedType)
            .ToListAsync();

        if (stas.Count == 0) return NotFound($"Không tìm thấy status với type '{type}'.");

        var dto = stas.Select(sa => new Status
        {
            id = sa.Id,
            Name = sa.Name,
            Type = sa.Type
        }).ToList();

        return Ok(dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateStatus>> Update(int id, [FromForm] UpdateStatus dto)
    {
        if (dto == null) return BadRequest("Dto is null or missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var sta = await _context.Statuses.FirstOrDefaultAsync(s => s.Id == id);
        if (sta == null) return BadRequest("no status with this id is found!");

        sta.Type = dto.Type;
        sta.Name = dto.Name;
        sta.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var sta = await _context.Statuses.FirstOrDefaultAsync(s => s.Id == id);
        if (sta == null) return BadRequest("no status with this id is found!");

        _context.Statuses.Remove(sta);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
