using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iHungry.Models;

namespace iHungry.Controllers
{
    public class KošaricaController : Controller
    {
        private readonly iHungryContext _context;

        public KošaricaController(iHungryContext context)
        {
            _context = context;
        }

        // GET: Košarica
        public async Task<IActionResult> Index()
        {
            var iHungryContext = _context.Košarica.Include(k => k.Jelo);
            return View(await iHungryContext.ToListAsync());
        }

        // GET: Košarica/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var košarica = await _context.Košarica
                .Include(k => k.Jelo)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (košarica == null)
            {
                return NotFound();
            }

            return View(košarica);
        }

        // GET: Košarica/Create
        public IActionResult Create()
        {
            ViewData["JeloId"] = new SelectList(_context.Jelo, "Id", "Naziv");
            return View();
        }

        // POST: Košarica/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JeloId,Količina")] Košarica košarica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(košarica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JeloId"] = new SelectList(_context.Jelo, "Id", "Naziv", košarica.JeloId);
            return View(košarica);
        }

        // GET: Košarica/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var košarica = await _context.Košarica.SingleOrDefaultAsync(m => m.Id == id);
            if (košarica == null)
            {
                return NotFound();
            }
            ViewData["JeloId"] = new SelectList(_context.Jelo, "Id", "Naziv", košarica.JeloId);
            return View(košarica);
        }
        public IActionResult Dodajukošaricu(int? id)
        {

            ViewData["JeloId"] = new SelectList(_context.Jelo, "Id", "Naziv", id);

            return View("~/Views/Košarica/Create.cshtml");
        }
        public IActionResult Naruči()
        {
            if (!_context.Košarica.Any())
            {
                return View("~/Views/Košarica/Greška.cshtml");
            }
            _context.Database.ExecuteSqlCommand("TRUNCATE TABLE Košarica");

            return View("~/Views/Košarica/Narudžba.cshtml");
        }

        // POST: Košarica/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JeloId,Količina")] Košarica košarica)
        {
            if (id != košarica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(košarica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KošaricaExists(košarica.Id))
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
            ViewData["JeloId"] = new SelectList(_context.Jelo, "Id", "Naziv", košarica.JeloId);
            return View(košarica);
        }

        // GET: Košarica/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var košarica = await _context.Košarica
                .Include(k => k.Jelo)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (košarica == null)
            {
                return NotFound();
            }

            return View(košarica);
        }

        // POST: Košarica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var košarica = await _context.Košarica.SingleOrDefaultAsync(m => m.Id == id);
            _context.Košarica.Remove(košarica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KošaricaExists(int id)
        {
            return _context.Košarica.Any(e => e.Id == id);
        }
    }
}
