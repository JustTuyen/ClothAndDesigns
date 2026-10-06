
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models.users;
using BackEnd.Data;

public class WardModelsController : Controller
{
    private readonly MyApplicationDBContext _context;

    public WardModelsController(MyApplicationDBContext context)
    {
        _context = context;
    }

    // GET: WARDMODELS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Wards.ToListAsync());
    }

    // GET: WARDMODELS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var wardmodel = await _context.Wards
            .FirstOrDefaultAsync(m => m.Id == id);
        if (wardmodel == null)
        {
            return NotFound();
        }

        return View(wardmodel);
    }

    // GET: WARDMODELS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: WARDMODELS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Code,CreatedAt,UpdatedAt,DistrictId,District,Addresses")] WardModel wardmodel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(wardmodel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(wardmodel);
    }

    // GET: WARDMODELS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var wardmodel = await _context.Wards.FindAsync(id);
        if (wardmodel == null)
        {
            return NotFound();
        }
        return View(wardmodel);
    }

    // POST: WARDMODELS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Code,CreatedAt,UpdatedAt,DistrictId,District,Addresses")] WardModel wardmodel)
    {
        if (id != wardmodel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(wardmodel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WardModelExists(wardmodel.Id))
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
        return View(wardmodel);
    }

    // GET: WARDMODELS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var wardmodel = await _context.Wards
            .FirstOrDefaultAsync(m => m.Id == id);
        if (wardmodel == null)
        {
            return NotFound();
        }

        return View(wardmodel);
    }

    // POST: WARDMODELS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var wardmodel = await _context.Wards.FindAsync(id);
        if (wardmodel != null)
        {
            _context.Wards.Remove(wardmodel);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool WardModelExists(int? id)
    {
        return _context.Wards.Any(e => e.Id == id);
    }
}
