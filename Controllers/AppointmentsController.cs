using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinic_Appointment_System.Data;
using Clinic_Appointment_System.Models;

namespace Clinic_Appointment_System.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _context;

        public AppointmentsController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Bütün randevuların siyahısı (Read)
        public async Task<IActionResult> Index()
        {
            var appointments = await _context.Appointments
                .OrderByDescending(x => x.AppointmentDate)
                .ToListAsync();

            ViewBag.TotalCount = appointments.Count;
            return View(appointments);
        }

        // 2. Yeni randevu səhifəsi (Create - GET)
        public IActionResult Create()
        {
            return View();
        }

        // 3. Yeni randevunun bazaya yazılması (Create - POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return View(appointment);
            }

            appointment.Status = "Gözləmədə";
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // 4. Randevunun detalları (Details)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(x => x.Id == id);

            if (appointment == null)
                return NotFound();

            return View(appointment);
        }

        // 5. Redaktə səhifəsi (Edit - GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var appointment = await _context.Appointments
                .FindAsync(id);

            if (appointment == null)
                return NotFound();

            return View(appointment);
        }

        // 6. Redaktənin yadda saxlanılması (Edit - POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Appointment appointment)
        {
            if (id != appointment.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(appointment);

            var existing = await _context.Appointments.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.PatientName = appointment.PatientName;
            existing.Phone = appointment.Phone;
            existing.Department = appointment.Department;
            existing.AppointmentDate = appointment.AppointmentDate;
            existing.Notes = appointment.Notes;
            existing.Status = appointment.Status;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // 7. Silmə təsdiq səhifəsi (Delete - GET)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(x => x.Id == id);

            if (appointment == null)
                return NotFound();

            return View(appointment);
        }

        // 8. Silmə əməliyyatının icrası (Delete - POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}