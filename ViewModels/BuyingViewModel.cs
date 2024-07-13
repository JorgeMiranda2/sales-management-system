namespace SalesSystemApp.ViewModels
{
    public class BuyingViewModel
    {
            public List<ProductViewModel>? Products { get; set; }
            public string UserEmail { get; set; }
            public List<ProductItemInput> ProductsInput { get; set; } = new List<ProductItemInput>();


        public class ProductViewModel
        {
            public int ProductId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; } // Añade esto para capturar la cantidad
        }

     
        public class ProductItemInput
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }

}
