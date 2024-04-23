using DorukSoft.OfficialWeb.Application.DTOs;

namespace DorukSoft.OfficialWeb.Application.Interfaces
{
    public interface IEmailService
    {
        Task PublishQuee(EmailSendRequestDTO dto);
    }
}
