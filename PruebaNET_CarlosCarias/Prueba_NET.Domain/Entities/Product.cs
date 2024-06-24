using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_NET.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        [NotMapped]
        public string? StatusName { get; set; }
        public int Stock {  get; set; }
        public decimal Price { get; set; }


    }
}
