using DorukSoft.OfficialWeb.Application.DTOs.ProgramDTOs.ContactAfterChatMessageDTOs;

namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class ContactListDTO
    {
        public int ContactId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Content { get; set; }
        public List<string>? ShowedAdminMails { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int? ProductId { get; set; }
        public ProductListDTO? Product { get; set; }
        public List<ContactAfterChatMessageListDTO>? ChatMessages { get; set; }
    }
}
