
using System.ComponentModel.Design;

namespace NukeShop.BLL.Utils.QueryParameters

{ 

    public class ProductParameters : QueryStringParameters
    {
        public ProductParameters()
        {
            OrderBy = "name";
        }
        public int PriceFrom { get; set; } = 0;
        public int PriceTo { get; set; } = 10000000;
        public string? Name { get; set; } = string.Empty;
       public string? Articul{ get; set; } = string.Empty;
        public string? CategoryName { get; set; } = string.Empty;
       public string? ManufacturerName { get; set; } = string.Empty;
    }
}