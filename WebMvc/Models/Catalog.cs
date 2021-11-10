using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    public class Catalog
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; } // Size that is returned back
        public long Count { get; set; }
        public IEnumerable<CatalogItem> Data { get; set; }
    }
}
