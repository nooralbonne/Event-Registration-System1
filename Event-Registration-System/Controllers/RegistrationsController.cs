using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Event_Registration_System.Data;
using Event_Registration_System.Models;
using Event_Registration_System.Services;

namespace Event_Registration_System.Controllers
{
    public class RegistrationsController : Controller
    {
        private readonly EventRegistrationDbContext _context;
        private readonly EmailService _emailService;

        public RegistrationsController(EventRegistrationDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: Registrations
        public async Task<IActionResult> Index()
        {
            var registrations = await _context.Registrations.Include(r => r.Event).ToListAsync();
            return View(registrations);
        }

        // GET: Registrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.Event)
                .FirstOrDefaultAsync(m => m.RegistrationId == id);

            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // GET: Registrations/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId");
            return View();
        }

        // POST: Registrations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Registration registration)
        {
            try
            {
                _context.Add(registration);
                await _context.SaveChangesAsync();

                // Send confirmation email
                var emailSent = await SendConfirmationEmail(registration);

                if (emailSent)
                {
                    TempData["SuccessMessage"] = "Registration created successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Registration created, but email failed to send.";
                }

                // Return the same view with success message instead of redirecting
                ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", registration.EventId);
                return View(registration);  // Stay on the Create page
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while creating the registration.");
            }

            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", registration.EventId);
            return View(registration);
        }


        private async Task<bool> SendConfirmationEmail(Registration registration)
        {
            var subject = "Event Registration Confirmation";
            var body = $"Hello {registration.ParticipantName},<br><br>You have successfully registered for the event.";
            return await _emailService.SendEmailAsync(registration.Email, registration.ParticipantName, subject, body, body);
        }

        // GET: Registrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }

            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", registration.EventId);
            return View(registration);
        }

        // POST: Registrations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Registration registration)
        {
            if (id != registration.RegistrationId)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(registration);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.RegistrationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", registration.EventId);
            return View(registration);
        }

        // GET: Registrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.Event)
                .FirstOrDefaultAsync(m => m.RegistrationId == id);

            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration != null)
            {
                _context.Registrations.Remove(registration);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registrations.Any(e => e.RegistrationId == id);
        }
    }
}
