namespace DorukSoft.OfficialWeb.Domain.ProgramEntities
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Content { get; set; }
        public List<string>? ShowedAdminMails { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public List<ContactAfterChatMessage>? ChatMessages { get; set; }
    }
}
