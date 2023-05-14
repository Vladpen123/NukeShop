using System.Text.Json.Serialization;

namespace NukeShop.Domain.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; }

    }
}