using GestionSubcription.Data;
using GestionSubcription.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionSubcription.Controllers
{
    public class SubscriptionController : Controller
       
    {

        //Index --- sjow all Subscription
        private readonly ApplicationDbContext _context;

        public SubscriptionController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IActionResult> Index()
        {
            var subscriptions = await _context.Subscriptions
                .Include(s => s.Client)
                .Include(s => s.Application)
                .ToListAsync();
            return View(subscriptions);
        }
        // Add
        public IActionResult Create()
        {
            ViewBag.Clients = _context.Clients.ToList();
            ViewBag.Applications = _context.Applications.ToList();

            return View();
        }




        //[HttpPost]
        //public async Task<IActionResult> Create(Subscription subscription)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        subscription.LicenseKey = GenerateLicenseKey();
        //        _context.Subscriptions.Add(subscription);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(subscription);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subscription subscription)
        {
            Console.WriteLine("✅ POST Create method called"); // Debug log

            if (!ModelState.IsValid)
            {
                Console.WriteLine("❌ Model state is invalid! Errors:");

                foreach (var error in ModelState)
                {
                    foreach (var subError in error.Value.Errors)
                    {
                        Console.WriteLine($"- {error.Key}: {subError.ErrorMessage}");
                    }
                }

                ViewBag.Clients = _context.Clients.ToList();
                ViewBag.Applications = _context.Applications.ToList();
                return View(subscription);
            }

            try
            {
                Console.WriteLine($"📌 Saving Subscription: ClientId={subscription.ClientId}, AppId={subscription.ApplicationId}");

                // Generate License Key
                subscription.LicenseKey = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 20);

                // Add and Save
                _context.Subscriptions.Add(subscription);
                await _context.SaveChangesAsync();

                Console.WriteLine("✅ Subscription saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR: {ex.Message}");
                ModelState.AddModelError("", "An error occurred while saving the subscription.");
                ViewBag.Clients = _context.Clients.ToList();
                ViewBag.Applications = _context.Applications.ToList();
                return View(subscription);
            }

            return RedirectToAction(nameof(Index));
        }



        //// Edite
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null) return NotFound();

        //    var subscription = await _context.Subscriptions.FindAsync(id);
        //    if (subscription == null) return NotFound();

        //    return View(subscription);
        //}
        // GET: Subscription/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var subscription = await _context.Subscriptions.FindAsync(id);
            if (subscription == null) return NotFound();

            ViewBag.Clients = _context.Clients.ToList();
            ViewBag.Applications = _context.Applications.ToList();

            return View(subscription);
        }

        // POST: Subscription/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Subscription subscription)
        {
            if (id != subscription.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Clients = _context.Clients.ToList();
                ViewBag.Applications = _context.Applications.ToList();
                return View(subscription);
            }

            var existingSubscription = await _context.Subscriptions.FindAsync(id);
            if (existingSubscription == null) return NotFound();

            existingSubscription.StartDate = subscription.StartDate;
            existingSubscription.EndDate = subscription.EndDate;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, Subscription subscription)
        //{
        //    if (id != subscription.Id) return NotFound();

        //    if (ModelState.IsValid)
        //    {
        //        var existingSubscription = await _context.Subscriptions.FindAsync(id);
        //        if (existingSubscription == null) return NotFound();

        //        existingSubscription.StartDate = subscription.StartDate;
        //        existingSubscription.EndDate = subscription.EndDate;

        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(subscription);
        //}


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);
            if (subscription == null) return NotFound();

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // Generate a subscription

        private string GenerateLicenseKey()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 20);
        }

        // admin Reg key
        public async Task<IActionResult> RegenerateLicense(int id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);
            if (subscription == null) return NotFound();

            subscription.LicenseKey = GenerateLicenseKey();
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Verify
        public bool IsLicenseValid(string licenseKey)
        {
            var subscription = _context.Subscriptions.FirstOrDefault(s => s.LicenseKey == licenseKey);
            if (subscription == null) return false;

            return DateTime.Now >= subscription.StartDate && DateTime.Now <= subscription.EndDate;
        }


    }
}
