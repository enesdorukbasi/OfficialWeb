using DorukSoft.OfficialWeb.Domain.ProgramEntities;

namespace DorukSoft.OfficialWeb.Application.Interfaces
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllContactsWithIncludes();
        Task<Contact?> GetByIdContactsWithIncludes(int id);
        Task<Contact> GetContactWithMessages(int id);
    }
}
