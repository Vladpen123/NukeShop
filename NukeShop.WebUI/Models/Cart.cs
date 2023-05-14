using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NukeShop.BLL.DTOs.ProductDtos;

namespace NukeShop.WebUI.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
        public void AddItem(ProductDto product, int quantity)
        {
            CartLine? line = Lines
            .Where(p => p.Product.Id == product.Id)
            .FirstOrDefault();
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(ProductDto product) =>
        Lines.RemoveAll(l => l.Product.Id == product.Id);
        public decimal ComputeTotalValue() =>
        Lines.Sum(e => e.Product.Price * e.Quantity);

 public void Clear() => Lines.Clear();
    }
    public class CartLine
    {
        public int CartLineID { get; set; }
        public ProductDto Product { get; set; } = new();
        public int Quantity { get; set; }
    }
}
