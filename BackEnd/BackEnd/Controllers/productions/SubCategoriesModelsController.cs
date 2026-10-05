
using BackEnd.Data;
using BackEnd.DTO.other;
using BackEnd.DTO.product;
using BackEnd.Models.productions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
[ApiController]
[Route("api/[controller]")]
public class SubCategoriesModelsController : Controller
{
    private readonly MyApplicationDBContext _context;

    public SubCategoriesModelsController(MyApplicationDBContext context)
    {
        _context = context;
    }

    // GET: SUBCATEGORIESMODELS
    [HttpGet]
    public async Task<ActionResult<List<SubCategory>>> GetAll()
    {
        var subcats = await _context.SubCategories
            .Include(x => x.Category)
            .Include(x => x.Status)
            .ToListAsync();
        if (subcats.Count == 0) return NotFound("ko tim dc subcats nao");

        var dto = subcats.Select(sub => new SubCategory
        {
            Id = sub.Id,
            Name = sub.Name,
            Description = sub.Description,
            Slug = sub.Slug,
            CategoryaName = sub.Category.Name,
            StatusName = sub.Status.Name,
            CreatedAt = sub.CreatedAt,
            UpdatedAt = sub.UpdatedAt,
        }).ToList();

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubCategory>> GetById(int id)
    {
        var sub = await _context.SubCategories
            .Include(x => x.Category)
            .Include(x => x.Status)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (sub == null) return NotFound("No subcat with this id is found");

        var dto = new SubCategory
        {
            Id = sub.Id,
            Name = sub.Name,
            Description = sub.Description,
            Slug = sub.Slug,
            CategoryaName = sub.Category.Name,
            StatusName = sub.Status.Name,
            CreatedAt = sub.CreatedAt,
            UpdatedAt = sub.UpdatedAt,
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<CreateSubCate>> Create([FromForm] CreateSubCate dto)
    {
        if (dto == null) return BadRequest("Dto or request data is missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var sta = await _context.Statuses
            .Where(x => x.Type == "subcategory" && x.Name == "active")
            .FirstOrDefaultAsync();
        if (sta == null) return BadRequest("sta ko co cho subcategory");

        var cat = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == dto.CategoryId);
        if (cat == null) return BadRequest("ko cat co id nao cho subcat");

        var sub = new SubCategoriesModel
        {
            Name = dto.Name,
            Description = dto.Description,
            Slug = dto.Slug,
            CategoryId = dto.CategoryId,
            StatusId = sta.Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        _context.SubCategories.Add(sub);
        await _context.SaveChangesAsync();

        var resutldto = new SubCatResultDTO
        {
            Id = sub.Id,
            Slug = sub.Slug,
            Name = sub.Name
        };

        return CreatedAtAction(nameof(GetById), new { id = sub.Id }, resutldto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateInfro>>UpdateInfo(int id, [FromForm] UpdateInfro dto)
    {
        if (dto == null) return BadRequest("Dto or request data is missing");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var sub = await _context.SubCategories
            .Include(x => x.Category)
            .Include(x => x.Status)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (sub == null) return NotFound("No subcat with this id is found");

        sub.Name = dto.Name;
        sub.Description = dto.Description;
        sub.Slug = dto.Slug;
        sub.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<SubCategory>> Delete(int id)
    {
        var sub = await _context.SubCategories
            .Include(x => x.Category)
            .Include(x => x.Status)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (sub == null) return NotFound("No subcat with this id is found");

        _context.SubCategories.Remove(sub);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    //puts
    [HttpPut("/subcatogory/category/{id}")]
    [HttpPut("/subcatogory/status/{id}")]
    [HttpGet("listing")]
}
