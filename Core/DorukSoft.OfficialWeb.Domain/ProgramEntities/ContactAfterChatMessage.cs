namespace DorukSoft.OfficialWeb.Domain.ProgramEntities
{
    public class ContactAfterChatMessage
    {
        public int ContactAfterChatMessageId { get; set; }
        public string? Message { get; set; }
        public int ContactId { get; set; }
        public Contact? Contact { get; set; }
    }
}
