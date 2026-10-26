using Microsoft.AspNetCore.Mvc;
using CrisisManagementPlatform.Services;
using CrisisManagementPlatform.Models;

namespace CrisisManagementPlatform.Controllers
{
    public class CrisisController : Controller
    {
        private readonly CrisisService _crisisService;

        public CrisisController(CrisisService crisisService)
        {
            _crisisService = crisisService;
        }

        // GET: /Crisis/Index
        public async Task<IActionResult> Index(string? searchTerm, CrisisStatus? statusFilter)
        {
            var crises = await _crisisService.GetCrisesAsync(searchTerm, statusFilter);

            // Real stats from database
            ViewBag.TotalCount = await _crisisService.GetTotalCrisesCountAsync();
            ViewBag.ActiveCount = await _crisisService.GetActiveCrisesCountAsync();
            ViewBag.RegisteredCount = await _crisisService.GetRegisteredCrisesCountAsync();

            ViewBag.SearchTerm = searchTerm;
            ViewBag.StatusFilter = statusFilter;

            return View(crises);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _crisisService.GetCategoriesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Crisis crisis)
        {
            if (ModelState.IsValid)
            {
                await _crisisService.CreateCrisisAsync(crisis);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = await _crisisService.GetCategoriesAsync();
            return View(crisis);
        }

        [HttpPost]
        public async Task<IActionResult> Activate(int id)
        {
            var success = await _crisisService.ActivateCrisisAsync(id);
            if (!success)
            {
                TempData["Error"] = "تعذر تفعيل الأزمة.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}