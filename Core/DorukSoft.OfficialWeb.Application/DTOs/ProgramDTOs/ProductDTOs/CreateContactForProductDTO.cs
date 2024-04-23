namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class CreateContactForProductDTO
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Content { get; set; }
    }
}
