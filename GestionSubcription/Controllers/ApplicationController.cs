using GestionSubcription.Data;
using GestionSubcription.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace GestionSubcription.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Applications.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Application application)
        {
            if (ModelState.IsValid)
            {
                _context.Applications.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(application);
        }


        //Edit application by Id 
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null) return NotFound();

            return View(application);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, Application application)
        {
            if (id != application.Id) return NotFound();

            var existingApplication = await _context.Applications.FindAsync(id);
            if (existingApplication == null) return NotFound();

            // Update application details
            existingApplication.Name = application.Name;
            existingApplication.Description = application.Description;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var application = await _context.Applications.FindAsync(id);
        //    if (application == null) return NotFound();

        //    return View(application);
        //}


        ////  GET: Edit application by name
        //[HttpGet]
        //public async Task<IActionResult> Edit(string name)
        //{
        //    if (string.IsNullOrEmpty(name)) return NotFound();

        //    var application = await _context.Applications.FirstOrDefaultAsync(a => a.Name == name);
        //    if (application == null) return NotFound();

        //    return View(application);
        //}

        ////  POST: Save changes to the application
        //[HttpPost]
        //public async Task<IActionResult> Edit(Application application)
        //{
        //    if (application == null || string.IsNullOrEmpty(application.Name)) return NotFound();

        //    var existingApplication = await _context.Applications.FirstOrDefaultAsync(a => a.Name == application.Name);
        //    if (existingApplication == null) return NotFound();

        //    // Update application details
        //    existingApplication.Name = application.Name;
        //    existingApplication.Description = application.Description;

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        // DELETE

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var application = await _context.Applications.FindAsync(id);
            if (application == null) return NotFound();
            return View(application);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
