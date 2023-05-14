using NukeShop.API.Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.API.Models.DTOs
{
    public class ManufacturerResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProductResultDto>? Products { get; set; }
    }
}
