using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCLaManuAgendaCourtage.Models;

namespace MVCLaManuAgendaCourtage.Controllers
{
    public class BrokersController : Controller
    {
        private readonly AgendaClientLegerContext _context;

        public BrokersController(AgendaClientLegerContext context)
        {
            _context = context;
        }

        // GET: Brokers
        public async Task<IActionResult> Index()
        {
              return _context.Brokers != null ? 
                          View(await _context.Brokers.ToListAsync()) :
                          Problem("Entity set 'AgendaClientLegerContext.Brokers'  is null.");
        }

        // GET: Brokers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Brokers == null)
            {
                return NotFound();
            }

            var brokers = await _context.Brokers
                .FirstOrDefaultAsync(m => m.IdBroker == id);
            if (brokers == null)
            {
                return NotFound();
            }

            return View(brokers);
        }

        // GET: Brokers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brokers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBroker,Lastname,Firstname,Mail,PhoneNumber")] Brokers brokers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brokers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brokers);
        }

        // GET: Brokers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Brokers == null)
            {
                return NotFound();
            }

            var brokers = await _context.Brokers.FindAsync(id);
            if (brokers == null)
            {
                return NotFound();
            }
            return View(brokers);
        }

        // POST: Brokers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBroker,Lastname,Firstname,Mail,PhoneNumber")] Brokers brokers)
        {
            if (id != brokers.IdBroker)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brokers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrokersExists(brokers.IdBroker))
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
            return View(brokers);
        }

        // GET: Brokers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Brokers == null)
            {
                return NotFound();
            }

            var brokers = await _context.Brokers
                .FirstOrDefaultAsync(m => m.IdBroker == id);
            if (brokers == null)
            {
                return NotFound();
            }

            return View(brokers);
        }

        // POST: Brokers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Brokers == null)
            {
                return Problem("Entity set 'AgendaClientLegerContext.Brokers'  is null.");
            }
            var brokers = await _context.Brokers.FindAsync(id);
            if (brokers != null)
            {
                _context.Brokers.Remove(brokers);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrokersExists(int id)
        {
          return (_context.Brokers?.Any(e => e.IdBroker == id)).GetValueOrDefault();
        }
    }
}
