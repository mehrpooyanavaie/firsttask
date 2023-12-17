using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace firsttask.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public string? ImageBase64 { get; set; }
    }
}
