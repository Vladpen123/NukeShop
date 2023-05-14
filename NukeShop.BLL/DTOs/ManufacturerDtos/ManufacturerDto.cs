using NukeShop.BLL.DTOs.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.BLL.DTOs.ManufacturerDtos
{
    public class ManufacturerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductDto>? Products { get; set; }
    }
}
