using DorukSoft.OfficialWeb.Domain.PageEntities;

namespace DorukSoft.OfficialWeb.Application.Interfaces
{
    public interface IGenericPageRepository
    {
        Task<GenericPage> GetGenericPageById(int id);
    }
}
