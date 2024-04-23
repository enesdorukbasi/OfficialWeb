using DorukSoft.OfficialWeb.Domain.Entities;

namespace DorukSoft.OfficialWeb.Application.Interfaces
{
    public interface IAppUserRepository
    {
        Task<AppUser?> LoginAsync(string email, string password);
    }
}
