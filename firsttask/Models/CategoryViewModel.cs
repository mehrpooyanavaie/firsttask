using System.ComponentModel.DataAnnotations;

namespace firsttask.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<string> ProductsNames { get; set; }
    }
}
