using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace firsttask.Models
{
    public class PostProductModel
    {
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }
        [Required]
        public double ProductPrice { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
