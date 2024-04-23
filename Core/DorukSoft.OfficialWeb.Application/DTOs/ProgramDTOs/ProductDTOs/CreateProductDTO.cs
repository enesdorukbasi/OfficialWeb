using DorukSoft.OfficialWeb.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class CreateProductDTO
    {
        public string? Title { get; set; }
        public List<IFormFile>? UpdatedImages { get; set; }
        public string? Content { get; set; }
        public string? Keywords { get; set; }
        public decimal Price { get; set; }
        public int Tax { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public ProductSalesType ProductSalesType { get; set; }
        public int CategoryId { get; set; }
        public List<CategoryListDTO>? Categories { get; set; }
    }
}
