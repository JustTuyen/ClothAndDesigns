
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models.productions;
using BackEnd.Data;
using BackEnd.DTO.user;
using BackEnd.Models.users;
[ApiController]
[Route("api/[controller]")]
public class DiscountModelsController : Controller
{
    private readonly MyApplicationDBContext _context;

    public DiscountModelsController(MyApplicationDBContext context)
    {
        _context = context;
    }

    // GET: DISCOUNTMODELS
    [HttpGet]
    public async Task<ActionResult<List<District>>> GetAll()
    {
        var districts = await _context.Districts
            .Include(x => x.Wards)
            .Include(x => x.City)
            .OrderBy(x => x.Name)
            .ToListAsync();
        if (districts.Count == 0) return Ok(new { message = "No district is found" });

        var dto = districts.Select(dis => new District
        {
            Id = dis.Id,
            Code = dis.Code,
            CityName = dis.City.Name,
            CreatedAt = dis.CreatedAt,
            UpdatedAt = dis.UpdatedAt,
            Wards = dis.Wards
                .OrderBy(x => x.Name)
                .Select(x => new WardListing
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
        }).ToList();

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<District>> GetById(int id)
    {
        var dis = await _context.Districts
            .Include(x => x.Wards)
            .Include(x => x.City)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (dis == null) return NotFound("no distrc with this id");

        var dto = new District
        {
            Id = dis.Id,
            Code = dis.Code,
            CityName = dis.City.Name,
            CreatedAt = dis.CreatedAt,
            UpdatedAt = dis.UpdatedAt,
            Wards = dis.Wards
                .OrderBy(x => x.Name)
                .Select(x => new WardListing
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<CreateDistrict>> Create([FromForm] CreateDistrict dto)
    {
        if (dto == null) return BadRequest("Dto is null or missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var city = await _context.Cities.FirstOrDefaultAsync(i => i.Id == dto.CityId);
        if (city == null) return BadRequest("no city wiht the id");

        var dis = new DistrictModel
        {
            Name = dto.Name,
            Code = dto.Code,
            CityId = city.Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        _context.Districts.Add(dis);
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
    public async Task<ActionResult<UpdateDistrict>> Update(int id, [FromForm] UpdateDistrict dto)
    {
        if (dto == null) return BadRequest("Dto is null or missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var city = await _context.Cities.FirstOrDefaultAsync(i => i.Id == dto.CityId);
        if (city == null) return BadRequest("no city wiht the id");

        var dis = await _context.Districts.FirstOrDefaultAsync(i => i.Id == id);
        if (dis == null) return BadRequest("no district with this id");

        dis.Name = dto.Name;
        dis.CityId = city.Id;
        dis.Code = dto.Code;
        dis.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}
