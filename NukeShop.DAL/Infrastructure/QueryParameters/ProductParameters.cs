using NukeShop.DAL.Infrastructure.QueryParameters;

namespace NukeShop.DAL.Infrastructure
{


    public class ProductParameters : QueryStringParameters
    {
        public ProductParameters()
        {
            OrderBy = "name";
        }
        public int PriceFrom { get; set; } = 0;
        public int PriceTo { get; set; } = 10000000;
        public string? Name { get; set; } 
       public string? Articul{ get; set; }
       public string? CategoryName{ get; set; }
       public string? ManufacturerName { get; set; }

    }
}