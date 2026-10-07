
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models.users;
using BackEnd.Data;
using BackEnd.DTO.user;

public class AddressModelsController : Controller
{
    private readonly MyApplicationDBContext _context;

    public AddressModelsController(MyApplicationDBContext context)
    {
        _context = context;
    }

    // GET: ADDRESSMODELS
    [HttpGet]
    public async Task<ActionResult<List<Address>>> GetAll()
    {
        var adds = await _context.Addresses
            .OrderBy(x => x.CreatedAt)
            .Include(x => x.User)
            .Include(x => x.City)
            .Include(x => x.District)
            .Include(x => x.Ward)
            .Include(x => x.Status)
            .ToListAsync();

        if (adds.Count == 0) return Ok(new { message = "No add is found" });

        var dto = adds.Select(x => new Address
        {
            Id = x.Id,
            Street = x.Street,
            Note = x.Note,
            IsDefault = x.IsDefault,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt,
            StatusName = x.Status.Name,
            CityName = x.City.Name,
            DistrictName = x.District.Name,
            WardName = x.Ward.Name,
            FullName = $"{x.User.FirstName ?? ""} {x.User.LastName ?? ""}".Trim()
        }).ToList();

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Address>> GetById(int id)
    {
        var x = await _context.Addresses
            .OrderBy(x => x.CreatedAt)
            .Include(x => x.User)
            .Include(x => x.City)
            .Include(x => x.District)
            .Include(x => x.Ward)
            .Include(x => x.Status)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (x == null) return BadRequest("No address with this id");

        var dto = new Address
        {
            Id = x.Id,
            Street = x.Street,
            Note = x.Note,
            IsDefault = x.IsDefault,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt,
            StatusName = x.Status.Name,
            CityName = x.City.Name,
            DistrictName = x.District.Name,
            WardName = x.Ward.Name,
            FullName = $"{x.User.FirstName ?? ""} {x.User.LastName ?? ""}".Trim()
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<CreateAddress>> Create([FromForm] CreateAddress dto)
    {
        if (dto == null) return BadRequest("Dto is null or missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var sta = await _context.Statuses
            .Where(x => x.Type == "address" && x.Name == "active")
            .FirstOrDefaultAsync();
        if (sta == null) return BadRequest("no status for address");

        var city = await _context.Cities
            .FirstOrDefaultAsync(x => x.Id == dto.CityId);
        if (city == null) return BadRequest("Không tìm thấy city với id này.");

        var dis = await _context.Districts
            .FirstOrDefaultAsync(x => x.Id == dto.DistrictId && x.CityId == dto.CityId);
        if (dis == null) return BadRequest("Không tìm thấy district khớp với city đã chọn.");

        var ward = await _context.Wards
            .FirstOrDefaultAsync(x => x.Id == dto.WardId && x.DistrictId == dto.DistrictId);
        if (dis == null) return BadRequest("Không tìm thấy ward khớp với district đã chọn.");

        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == dto.UserId && x.Status.Name == "active");
        if (user == null) return BadRequest("Không tìm thấy user khớp với id đã chọn or dang hoat dong.");

        var address = new AddressModel
        {
            UserId = user.Id,
            StatusId = sta.Id,
            CityId = city.Id,
            DistrictId = dis.Id,
            WardId = ward.Id,
            Street = dto.Street,
            IsDefault = dto.IsDefault,
            Note = dto.Note
        };

        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();

        var result = new AddressResult
        {
            Id = address.Id
        };

        return CreatedAtAction(nameof(GetById), new { id = address.Id }, result);
    }

    [HttpDelete("id")]
    public async Task<ActionResult<Address>> Delete(int id)
    {
        var x = await _context.Addresses.FirstOrDefaultAsync(x => x.Id == id);
        if (x == null) return BadRequest("No address with this id");

        _context.Addresses.Remove(x);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateAddress>> Update(int id, [FromForm] UpdateAddress dto)
    {
        if (dto == null) return BadRequest("Dto is null or missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var address = await _context.Addresses.FirstOrDefaultAsync(x => x.Id == id);
        if (address == null) return BadRequest("No address with this id");

        var city = await _context.Cities
            .FirstOrDefaultAsync(x => x.Id == dto.CityId);
        if (city == null) return BadRequest("Không tìm thấy city với id này.");

        var dis = await _context.Districts
            .FirstOrDefaultAsync(x => x.Id == dto.DistrictId && x.CityId == dto.CityId);
        if (dis == null) return BadRequest("Không tìm thấy district khớp với city đã chọn.");

        var ward = await _context.Wards
            .FirstOrDefaultAsync(x => x.Id == dto.WardId && x.DistrictId == dto.DistrictId);
        if (dis == null) return BadRequest("Không tìm thấy ward khớp với district đã chọn.");

        if (dto.IsDefault)
        {
            await _context.Addresses
                .Where(x => x.UserId == address.UserId && x.Id != id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(a => a.IsDefault, false)
                .SetProperty(a => a.UpdatedAt, DateTime.UtcNow));
        }
        address.IsDefault = dto.IsDefault;
        address.Street = dto.Street;
        address.Note = dto.Note;
        address.CityId = city.Id;
        address.DistrictId = dis.Id;
        address.WardId = ward.Id;
        address.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return NoContent();
    }
    //
    [HttpPut("/address/status/{id}")]
    public async Task<ActionResult<AddressStatus>> UpdateStatus(int id, [FromForm] AddressStatus dto)
    {
        if (dto == null) return BadRequest("Dto is null or missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var address = await _context.Addresses.FirstOrDefaultAsync(x => x.Id == id);
        if (address == null) return BadRequest("No address with this id");

        var sta = await _context.Statuses
            .Where(x => x.Type == "address")
            .FirstOrDefaultAsync();
        if (sta == null) return BadRequest("no status for address");

        address.StatusId = sta.Id;
        address.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("listing")]
    public async Task<ActionResult<List<AddressListing>>> Listing()
    {
        var adds = await _context.Addresses
           .OrderBy(x => x.CreatedAt)
           .Include(x => x.User)
           .Include(x => x.City)
           .Include(x => x.District)
           .Include(x => x.Ward)
           .ToListAsync();

        if (adds.Count == 0) return Ok(new { message = "No add is found" });

        var dto = adds.Select(add => new AddressListing
        {
            Id = add.Id,
            Street = add.Street,
            IsDefault = add.IsDefault,
            CityName = add.City.Name,
            DistrictName = add.District.Name,
            wardName = add.Ward.Name,
            FullName = $"{add.User.FirstName ?? ""} {add.User.LastName ?? ""}".Trim()
        }).ToList();

        return Ok(dto);
    }

}
