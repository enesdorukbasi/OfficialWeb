namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class ProductDemandListDTO
    {
        public int ProductDemandId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Content { get; set; }
        public int ProductId { get; set; }
        public ProductListDTO? Product { get; set; }
    }
}
