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
    public class JeloesController : Controller
    {
        private readonly iHungryContext _context;

        public JeloesController(iHungryContext context)
        {
            _context = context;
        }

        // GET: Jeloes
        public async Task<IActionResult> Index(string naziv,string kor,string pass)
        {
            ViewBag.Kriviunos = false;
            if (kor=="restoran" && pass=="1234")
            {
                ViewBag.Kriviunos = false;
                ViewBag.Admin = true;
            }
            else if(kor!=null&pass!=null)
            {
                ViewBag.Kriviunos = true;
                ViewBag.Admin = false;
            }
            else
            {
                ViewBag.Admin = false;
              
            }
            var jela = from j in _context.Jelo
                       select j;
            if(!String.IsNullOrEmpty(naziv))
            {
                jela = jela.Where(j => j.Naziv.Contains(naziv));
            }
            return View(jela.ToList());
        }


        // GET: Jeloes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jelo = await _context.Jelo
                .SingleOrDefaultAsync(m => m.Id == id);
            if (jelo == null)
            {
                return NotFound();
            }

            return View(jelo);
        }

        // GET: Jeloes/Create
        public IActionResult Create()
        {
            return View();
        }
        


        // POST: Jeloes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Cijena")] Jelo jelo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jelo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jelo);
        }

        // GET: Jeloes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jelo = await _context.Jelo.SingleOrDefaultAsync(m => m.Id == id);
            if (jelo == null)
            {
                return NotFound();
            }
            return View(jelo);
        }

        // POST: Jeloes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Cijena")] Jelo jelo)
        {
            if (id != jelo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jelo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JeloExists(jelo.Id))
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
            return View(jelo);
        }

        // GET: Jeloes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jelo = await _context.Jelo
                .SingleOrDefaultAsync(m => m.Id == id);
            if (jelo == null)
            {
                return NotFound();
            }

            return View(jelo);
        }

        // POST: Jeloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jelo = await _context.Jelo.SingleOrDefaultAsync(m => m.Id == id);
            _context.Jelo.Remove(jelo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JeloExists(int id)
        {
            return _context.Jelo.Any(e => e.Id == id);
        }
    }
}
