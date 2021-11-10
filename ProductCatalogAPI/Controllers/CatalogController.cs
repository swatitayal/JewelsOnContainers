using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductCatalogAPI.Data;
using ProductCatalogAPI.Domain;
using ProductCatalogAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : Controller
    {
        private readonly CatalogContext _context;
        private readonly IConfiguration _config;
        public CatalogController(CatalogContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        
        [HttpGet("[Action]")]
        public async Task<IActionResult> CatalogBrands()
        {
          var brands= await _context.CatalogBrands.ToListAsync();
            return Ok(brands);
        }

        [HttpGet("[Action]")]
        public async Task<IActionResult> CatalogTypes()
        {
            var types = await _context.CatalogTypes.ToListAsync();
            return Ok(types);
        }

        [HttpGet("[Action]")]
        public async Task<IActionResult> Items([FromQuery]int pageIndex = 0, [FromQuery]int pageSize = 6)
        {
            var itemsCount = _context.Catalog.LongCountAsync(); //Not needed to wait for this thread to be completed. So no "await" keyword.
            var items = await _context.Catalog
                                 .OrderBy(c => c.Name)
                                 .Skip(pageIndex * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
            items = ChangePictureUrl(items);

            var model = new PaginatedItemsViewModel
            {
                Data = items,
                PageIndex = pageIndex,
                PageSize = items.Count,
                Count = itemsCount.Result //because itemsCount is a Task and we just need its result back.
            };

            return Ok(model);
        }

        [HttpGet("[action]/type/{catalogTypeId}/brand/{catalogBrandId}")]
        public async Task<IActionResult> Items(
            int? catalogTypeId,
            int? catalogBrandId,
            [FromQuery] int pageIndex = 0, 
            [FromQuery] int pageSize = 6)
        {
            var query = (IQueryable<CatalogItem>)_context.Catalog; // Just a query of CatalogItem that need not to be exceuted right now.
            if (catalogTypeId.HasValue)
            {
                query = query.Where(c => c.CatalogTypeId == catalogTypeId);
            }

            if (catalogBrandId.HasValue)
            {
                query = query.Where(c => c.CatalogBrandId == catalogBrandId);
            }

            var itemsCount  = query.LongCountAsync(); //Not needed to wait for this thread to be completed. So no "await" keyword.
            var items = await query
                                 .OrderBy(c => c.Name)
                                 .Skip(pageIndex * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
            items = ChangePictureUrl(items);

            var model = new PaginatedItemsViewModel
            {
                Data = items,
                PageIndex = pageIndex,
                PageSize = items.Count,
                Count = itemsCount.Result
            };

            return Ok(model);
        }

        private List<CatalogItem> ChangePictureUrl(List<CatalogItem> items)
        {
            items.ForEach(item => 
                item.PictureUrl = item.PictureUrl.Replace("http://externalcatalogbaseurltobereplaced", _config["ExternalCatalogUrl"]));
            return items;
        }
    }
}
 