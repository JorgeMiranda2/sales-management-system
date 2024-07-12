
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesSystemApp.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public DateTime SaleDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalBeforeTax { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAfterTax { get; set; }

       
        public ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
    }
}
