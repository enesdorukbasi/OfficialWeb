using DorukSoft.OfficialWeb.Domain.ProgramEntities;

namespace DorukSoft.OfficialWeb.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdWithSubCategories(int id);
    }
}
