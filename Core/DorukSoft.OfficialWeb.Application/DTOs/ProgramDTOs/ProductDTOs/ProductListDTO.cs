using DorukSoft.OfficialWeb.Domain.Enums;

namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class ProductListDTO
    {
        public int ProductId { get; set; }
        public List<string>? ImageUrls { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Keywords { get; set; }
        public decimal Price { get; set; }
        public int Tax { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public ProductSalesType ProductSalesType { get; set; }
        public int CategoryId { get; set; }
        public CategoryListDTO? Category { get; set; }
        public List<ProductDemandListDTO>? ProductDemands { get; set; }
    }
}
