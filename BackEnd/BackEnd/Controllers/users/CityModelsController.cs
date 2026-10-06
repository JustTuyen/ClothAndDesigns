
using BackEnd.Data;
using BackEnd.DTO.other;
using BackEnd.DTO.user;
using BackEnd.Models.users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CityModelsController : ControllerBase
{
    private readonly MyApplicationDBContext _context;

    public CityModelsController(MyApplicationDBContext context)
    {
        _context = context;
    }

    // GET: CITYMODELS
    [HttpGet]
    public async Task<ActionResult<List<City>>> GetAll()
    {
        var cities = await _context.Cities
            .OrderBy(x => x.Name)
            .ToListAsync();

        if (cities.Count == 0) return Ok("no cities to find.period");
        var dto = cities.Select(city => new City
        {
            Id = city.Id,
            Code = city.Code,
            Name = city.Name,
            CreatedAt = city.CreatedAt,
            UpdatedAt = city.UpdatedAt,
            Districts = city.Districts
                .OrderBy(x => x.Name)
                .Select(x => new DistrictListing
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
        }).ToList();

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<City>> GetById(int id)
    {
        var city = await _context.Cities
            .Include(x => x.Districts)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (city == null) return NotFound("No city with this city");

        var dto = new City
        {
            Id = city.Id,
            Code = city.Code,
            Name = city.Name,
            CreatedAt = city.CreatedAt,
            UpdatedAt = city.UpdatedAt,
            Districts = city.Districts
                .OrderBy(x => x.Name)
                .Select(x => new DistrictListing
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<CreateCity>> Create([FromForm] CreateCity dto)
    {
        if (dto == null) return BadRequest("Dto is null or missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var city = new CityModel
        {
            Code = dto.Code,
            Name = dto.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        _context.Cities.Add(city);
        await _context.SaveChangesAsync();

        var result = new Result
        {
            Name = city.Name,
            Code = city.Code
        };
        return CreatedAtAction(nameof(GetById), new { id = city.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateCity>> Update(int id, [FromForm] UpdateCity dto)
    {
        if (dto == null) return BadRequest("Dto is null or missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
        if (city == null) return NotFound("no city with this id");

        city.Name = dto.Name;
        city.Code = dto.Code;
        city.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<City>> Delete(int id)
    {
        var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
        if (city == null) return NotFound("no city with this id");

        _context.Cities.Remove(city);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    //
    [HttpGet("listing")]
    public async Task<ActionResult<List<CityListing>>> Listing()
    {
        var cities = await _context.Cities
            .OrderBy(x => x.Name)
            .Include(x => x.Districts)
                .ThenInclude(x => x.Wards)
            .ToListAsync();

        if (cities.Count == 0) return Ok("no cities to find.period");

        var dto = cities.Select(city => new CityListing
        {
            Id = city.Id,
            Name = city.Name,
            Districts = city.Districts
                .OrderBy(d => d.Name)
                .Select(d => new DistrictListing
                {
                    Id = d.Id,
                    Name = d.Name,
                    Wards = d.Wards
                        .OrderBy(w => w.Name)
                        .Select(w => new WardListing
                        {
                            Id = w.Id,
                            Name = w.Name
                        }).ToList()
                }).ToList()
        }).ToList();

        return Ok(dto);
    }
}
