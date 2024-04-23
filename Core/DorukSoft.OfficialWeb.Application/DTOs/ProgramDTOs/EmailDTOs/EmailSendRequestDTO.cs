namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class EmailSendRequestDTO
    {
        public required string ToMailAddress { get; set; }
        public required string CcMailAddress { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}
