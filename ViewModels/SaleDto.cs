namespace SalesSystemApp.ViewModels
{
    public class SaleDto
    {
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalBeforeTax { get; set; }
        public decimal TotalAfterTax { get; set; }
        public List<SaleDetailDto> SaleDetails { get; set; } = new List<SaleDetailDto>();
    }

    public class SaleDetailDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } // Precio sin IVA
    }

    public class SearchSalesViewModel
    {
        public string UserEmail { get; set; }
        public List<SaleDto> Sales { get; set; } = new List<SaleDto>();
    }
}
