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
    public class AppointmentsController : Controller
    {
        private readonly AgendaClientLegerContext _context;

        public AppointmentsController(AgendaClientLegerContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var agendaClientLegerContext = _context.Appointments.Include(a => a.IdBrokerNavigation).Include(a => a.IdCustomerNavigation);
            return View(await agendaClientLegerContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointments = await _context.Appointments
                .Include(a => a.IdBrokerNavigation)
                .Include(a => a.IdCustomerNavigation)
                .FirstOrDefaultAsync(m => m.IdAppointment == id);
            if (appointments == null)
            {
                return NotFound();
            }

            return View(appointments);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["IdBroker"] = new SelectList(_context.Brokers, "IdBroker", "Firstname");
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "Firstname");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAppointment,DateHour,Subject,IdBroker,IdCustomer")] Appointments appointments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBroker"] = new SelectList(_context.Brokers, "IdBroker", "Firstname", appointments.IdBroker);
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "Firstname", appointments.IdCustomer);
            return View(appointments);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointments = await _context.Appointments.FindAsync(id);
            if (appointments == null)
            {
                return NotFound();
            }
            ViewData["IdBroker"] = new SelectList(_context.Brokers, "IdBroker", "Firstname", appointments.IdBroker);
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "Firstname", appointments.IdCustomer);
            return View(appointments);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAppointment,DateHour,Subject,IdBroker,IdCustomer")] Appointments appointments)
        {
            if (id != appointments.IdAppointment)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentsExists(appointments.IdAppointment))
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
            ViewData["IdBroker"] = new SelectList(_context.Brokers, "IdBroker", "Firstname", appointments.IdBroker);
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "Firstname", appointments.IdCustomer);
            return View(appointments);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointments = await _context.Appointments
                .Include(a => a.IdBrokerNavigation)
                .Include(a => a.IdCustomerNavigation)
                .FirstOrDefaultAsync(m => m.IdAppointment == id);
            if (appointments == null)
            {
                return NotFound();
            }

            return View(appointments);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'AgendaClientLegerContext.Appointments'  is null.");
            }
            var appointments = await _context.Appointments.FindAsync(id);
            if (appointments != null)
            {
                _context.Appointments.Remove(appointments);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentsExists(int id)
        {
          return (_context.Appointments?.Any(e => e.IdAppointment == id)).GetValueOrDefault();
        }
    }
}
