
using BackEnd.Data;
using BackEnd.DTO.user;
using BackEnd.Models.users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection.Metadata.Ecma335;
[ApiController]
[Route("api/[controller]")]
public class UserModelsController : ControllerBase
{
    private readonly MyApplicationDBContext _context;

    public UserModelsController(MyApplicationDBContext context)
    {
        _context = context;
    }

    //
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAll()
    {
        var users = await _context.Users
            .Include(x => x.Status)
            .OrderBy(x => x.Role)
            .ToListAsync();

        if (users.Count == 0) return Ok(new { message = "no user in db" });

        var dto = users.Select(user => new User
        {
            Id = user.Id,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            FullName = user.FirstName + " " + user.LastName,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            Role = user.Role,
            StatusName = user.Status.Name
        }).ToList();

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(int id)
    {
        var user = await _context.Users
            .Include(x => x.Status)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user == null) return NotFound("no user wiht this id");
        var dto = new User
        {
            Id = user.Id,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            FullName = user.FirstName + " " + user.LastName,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            Role = user.Role,
            StatusName = user.Status.Name
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<CreateUser>> Create([FromForm] CreateUser dto)
    {
        if (dto == null) return BadRequest("Dto is null or missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var sta = await _context.Statuses.FirstOrDefaultAsync(x => x.Type == "user" && x.Name == "active");
        if (sta == null) return BadRequest("no sta for user");

        if (await _context.Users.AnyAsync(x => x.Email == dto.Email))
            return Conflict($"email '{dto.Email} already got");

        if (await _context.Users.AnyAsync(x => x.PhoneNumber == dto.PhoneNumber))
            return Conflict($"PhoneNumber '{dto.PhoneNumber} already got");

        var user = new UserModel
        {
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            Password = dto.Password,
            Gender = dto.Gender,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            DateOfBirth = dto.DateOfBirth,
            StatusId = sta.Id,
            
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var result = new UserResult
        {
            Id = user.Id,
            Email = user.Email
        };

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateUser>> Update(int id, [FromForm] UpdateUser dto)
    {
        if (dto == null) return BadRequest("Dto is null or missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (await _context.Users.AnyAsync(x => x.Email == dto.Email))
            return Conflict($"email '{dto.Email} already got");

        if (await _context.Users.AnyAsync(x => x.PhoneNumber == dto.PhoneNumber))
            return Conflict($"PhoneNumber '{dto.PhoneNumber} already got");

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null) return BadRequest("No user with this id");

        user.PhoneNumber = dto.PhoneNumber;
        user.Email = dto.Email;
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Gender = dto.Gender;
        user.DateOfBirth = dto.DateOfBirth;
        user.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> Delete(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null) return BadRequest("No user with this id");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    //
    [HttpPut("/user/role/{id}")]
    public async Task<ActionResult<UpdateRole>> UpdateRole(int id, [FromForm] UpdateRole dto)
    {
        if (dto == null) return BadRequest("Dto is null or missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null) return BadRequest("No user with this id");

        if (!Enum.TryParse<UserRole>(dto.Role, ignoreCase: true, out var role) || !Enum.IsDefined(role))
            return BadRequest("Role không hợp lệ.");
        
        user.Role = role;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("/user/status/{id}")]
    public async Task<ActionResult<UserStatus>> UpdateStatus(int id, [FromForm] UserStatus dto)
    {
        if (dto == null) return BadRequest("Dto is null or missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var sta = await _context.Statuses.FirstOrDefaultAsync(x => x.Id == dto.StatusId && x.Type == "user");
        if (sta == null) return BadRequest("No staus for user");

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null) return BadRequest("No user with this id");

        user.StatusId = sta.Id;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}
