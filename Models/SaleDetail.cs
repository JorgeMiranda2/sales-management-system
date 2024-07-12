
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesSystemApp.Models
{
    public class SaleDetail
    {
        public int SaleDetailId { get; set; }
        public int SaleId { get; set; }
        
        [ForeignKey("SaleId")]
        public Sale? Sale { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } // Precio sin IVA

        
       
    }
}