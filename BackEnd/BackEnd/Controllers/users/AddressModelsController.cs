
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models.users;
using BackEnd.Data;

public class AddressModelsController : Controller
{
    private readonly MyApplicationDBContext _context;

    public AddressModelsController(MyApplicationDBContext context)
    {
        _context = context;
    }

    // GET: ADDRESSMODELS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Addresses.ToListAsync());
    }

    // GET: ADDRESSMODELS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var addressmodel = await _context.Addresses
            .FirstOrDefaultAsync(m => m.Id == id);
        if (addressmodel == null)
        {
            return NotFound();
        }

        return View(addressmodel);
    }

    // GET: ADDRESSMODELS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ADDRESSMODELS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Street,Note,IsDefault,CreatedAt,UpdatedAt,StatusId,Status,UserId,User,CityId,City,DistrictId,District,WardId,Ward")] AddressModel addressmodel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(addressmodel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(addressmodel);
    }

    // GET: ADDRESSMODELS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var addressmodel = await _context.Addresses.FindAsync(id);
        if (addressmodel == null)
        {
            return NotFound();
        }
        return View(addressmodel);
    }

    // POST: ADDRESSMODELS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Street,Note,IsDefault,CreatedAt,UpdatedAt,StatusId,Status,UserId,User,CityId,City,DistrictId,District,WardId,Ward")] AddressModel addressmodel)
    {
        if (id != addressmodel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(addressmodel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressModelExists(addressmodel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(addressmodel);
    }

    // GET: ADDRESSMODELS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var addressmodel = await _context.Addresses
            .FirstOrDefaultAsync(m => m.Id == id);
        if (addressmodel == null)
        {
            return NotFound();
        }

        return View(addressmodel);
    }

    // POST: ADDRESSMODELS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var addressmodel = await _context.Addresses.FindAsync(id);
        if (addressmodel != null)
        {
            _context.Addresses.Remove(addressmodel);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AddressModelExists(int? id)
    {
        return _context.Addresses.Any(e => e.Id == id);
    }
}
