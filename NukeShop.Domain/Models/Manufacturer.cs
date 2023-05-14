namespace NukeShop.Domain.Models
{
    public class Manufacturer : Entity
    {
        public string Name { get; set; }

        public  IEnumerable<Product> Products { get; set; }

    }
}