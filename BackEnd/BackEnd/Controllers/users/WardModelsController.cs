
using BackEnd.Data;
using BackEnd.DTO.user;
using BackEnd.Models.users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[ApiController]
[Route("api/[controller]")]
public class WardModelsController : Controller
{
    private readonly MyApplicationDBContext _context;

    public WardModelsController(MyApplicationDBContext context)
    {
        _context = context;
    }

    // GET: WARDMODELS
    [HttpGet]
    public async Task<ActionResult<List<Ward>>> GetAll()
    {
        var wards = await _context.Wards
            .Include(x => x.District)
            .OrderBy(x => x.Name)
            .ToListAsync();
        if (wards.Count == 0) return Ok(new { message = "No wards is found" });

        var dto = wards.Select(x => new District
        {
            Id = x.Id,
            Code = x.Code,
            CityName = x.District.Name,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt,
        }).ToList();

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Ward>> GetById(int id)
    {
        var dis = await _context.Wards
            .Include(x => x.District)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (dis == null) return Ok(new { message = "No ward is found" });

        var dto = new Ward
        {
            Id = dis.Id,
            Code = dis.Code,
            DistrictName = dis.District.Name,
            CreatedAt = dis.CreatedAt,
            UpdatedAt = dis.UpdatedAt,
           
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<CreateDistrict>> Create([FromForm] CreateWard dto)
    {
        if (dto == null) return BadRequest("Dto is null or missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var district = await _context.Districts.FirstOrDefaultAsync(i => i.Id == dto.DistrictId);
        if (district == null) return BadRequest("no district wiht the id");

        var dis = new WardModel
        {
            Name = dto.Name,
            Code = dto.Code,
            DistrictId = district.Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        _context.Wards.Add(dis);
        await _context.SaveChangesAsync();

        var result = new Result
        {
            Id = dis.Id,
            Name = dis.Name,
            Code = dis.Code
        };

        return CreatedAtAction(nameof(GetById), new { id = dis.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateDistrict>> Update(int id, [FromForm] UpdateWard dto)
    {
        if (dto == null) return BadRequest("Dto is null or missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var dis = await _context.Districts.FirstOrDefaultAsync(i => i.Id == dto.DistrictId);
        if (dis == null) return BadRequest("no dis wiht the id");

        var ward = await _context.Wards.FirstOrDefaultAsync(i => i.Id == id);
        if (ward == null) return BadRequest("no ward with this id");

        ward.Name = dto.Name;
        ward.DistrictId = dis.Id;
        ward.Code = dto.Code;
        ward.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Ward>> Delete(int id)
    {
        var ward = await _context.Wards.FirstOrDefaultAsync(i => i.Id == id);
        if (ward == null) return BadRequest("no waard wiht the id");

        _context.Wards.Remove(ward);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    //
    [HttpGet("listing")]
    public async Task<ActionResult<List<WardListing>>> Listing()
    {
        var wards = await _context.Wards
            .OrderBy(x => x.Name)
            .ToListAsync();

        if (wards.Count == 0) return Ok(new { message = "No district is found" });

        var dto = wards.Select(dis => new DistrictListing
        {
            Id = dis.Id,
            Name = dis.Name,
        }).ToList();

        return Ok(dto);
    }
}
