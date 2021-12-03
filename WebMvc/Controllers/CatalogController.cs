using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _service;
        public CatalogController (ICatalogService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(int? page, int? brandFilterApplied, int? typesFilterApplied)
        {
            var itemsOnPage = 10;
            int pagetest = 0;

            if (page.HasValue == true )
            {
                pagetest = page.Value;
            }

            var catalog = _service.GetCatalogItemsAsync(pagetest, itemsOnPage, brandFilterApplied, typesFilterApplied).Result;

            var vm = new CatalogIndexViewModel
            {
                Brands = await _service.GetBrandsAsync(),
                Types = await _service.GetTypesAsync(),
                CatalogItems = catalog.Data,
                PaginationInfo = new PaginationInfo
                {
                    TotalItems = catalog.Count,
                    ItemsPerPage = catalog.PageSize,
                    ActualPage = catalog.PageIndex,
                    TotalPages = (int)Math.Ceiling((decimal)catalog.Count / itemsOnPage),
                },
                BrandFilterApplied = brandFilterApplied,
                TypeFilterApplied = typesFilterApplied
            };
            return View(vm);
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your Application Description Page";

            return View();
        }
    }
}
