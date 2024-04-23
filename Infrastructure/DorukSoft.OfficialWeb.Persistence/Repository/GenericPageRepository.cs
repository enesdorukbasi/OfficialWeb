using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.PageEntities;
using DorukSoft.OfficialWeb.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DorukSoft.OfficialWeb.Persistence.Repository
{
    public class GenericPageRepository : Repository<GenericPage>, IGenericPageRepository
    {
        private readonly MSSqlContext _context;
        public GenericPageRepository(MSSqlContext context) : base(context)
        {
            _context = context;
        }

        public async Task<GenericPage> GetGenericPageById(int id)
        {
            var entity = await _context.Set<GenericPage>().Include(x => x.ViewerPageMedias).FirstAsync(x=>x.PageId == id);
            return entity;
        }
    }
}
