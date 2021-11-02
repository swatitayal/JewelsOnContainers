using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Data
{
    public static  class CatalogSeed
    {
        public static void Seed(CatalogContext context)
        {
            context.Database.Migrate();
            if(!context.CatalogTypes.Any())
            {
                context.CatalogTypes.AddRange(GetCatalogTypes());
                context.SaveChanges();
            }
            if (!context.CatalogBrands.Any())
            {
                context.CatalogBrands.AddRange(GetCatalogBrands());
                context.SaveChanges();
            }
            if (!context.Catalog.Any())
            {
                context.Catalog.AddRange(GetCatalogItems());
                context.SaveChanges();
            }
        }

        private static IEnumerable<CatalogItem> GetCatalogItems()
        {
            return new List<CatalogItem>
            {
                new CatalogItem { CatalogTypeId =2, CatalogBrandId = 2, Description = "Any1", Name ="AnyName", Price = 199.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/1" },
                new CatalogItem { CatalogTypeId =2, CatalogBrandId = 1, Description = "Any2", Name ="Name2", Price = 199.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/2" },
                new CatalogItem { CatalogTypeId =1, CatalogBrandId = 1, Description = "Any3", Name ="3 Name", Price = 199.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/3" },
                new CatalogItem { CatalogTypeId =3, CatalogBrandId = 1, Description = "Any4", Name ="4 Name", Price = 199.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/4" },
                new CatalogItem { CatalogTypeId =2, CatalogBrandId = 3, Description = "Any5", Name ="5Name", Price = 199.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/5" },
                new CatalogItem { CatalogTypeId =1, CatalogBrandId = 3, Description = "Any6", Name ="6Name", Price = 199.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/6" },
                new CatalogItem { CatalogTypeId =3, CatalogBrandId = 2, Description = "Any7", Name ="7Name", Price = 199.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/7" },
                new CatalogItem { CatalogTypeId =3, CatalogBrandId = 3, Description = "Any8", Name ="8Name", Price = 199.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/8" },
                new CatalogItem { CatalogTypeId =2, CatalogBrandId = 3, Description = "Any9", Name ="9Name", Price = 199.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/9" },
                new CatalogItem { CatalogTypeId =3, CatalogBrandId = 2, Description = "Any10", Name ="10Name", Price = 199.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/10" },
                new CatalogItem { CatalogTypeId =1, CatalogBrandId = 2, Description = "Any11", Name ="11Name", Price = 199.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/11" },
                new CatalogItem { CatalogTypeId =2, CatalogBrandId = 2, Description = "Any12", Name ="12Name", Price = 199.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/12" },
            };
        }

        private static IEnumerable<CatalogBrand> GetCatalogBrands()
        {
            return new List<CatalogBrand>
            {
                new CatalogBrand { Brand = "Tiffany & Co." },
                new CatalogBrand { Brand = "DeBeers" },
                new CatalogBrand { Brand = "Graff" },
            };
        }

        private static IEnumerable<CatalogType> GetCatalogTypes()
        {
            return new List<CatalogType>
            {
                new CatalogType { Type= "Engagement Ring" },
                new CatalogType { Type= "Wedding Ring" },
                new CatalogType { Type= "Fashion Ring" },
            };
        }
    }
}
