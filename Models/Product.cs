using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SalesSystemApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } // Precio sin IVA

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } // Nueva descripción

        public ICollection<SaleDetail> SaleDetails { get; set; }
    }
}