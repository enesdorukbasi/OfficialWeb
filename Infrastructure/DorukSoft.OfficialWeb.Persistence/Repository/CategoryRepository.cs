using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using DorukSoft.OfficialWeb.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DorukSoft.OfficialWeb.Persistence.Repository
{
    public class CategoryRepository : Repository<Category>,  ICategoryRepository 
    {
        private readonly MSSqlContext _context;

        public CategoryRepository(MSSqlContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Category?> GetByIdWithSubCategories(int id)
        {
            return await _context.Set<Category>().Include(x => x.SubCategories).FirstAsync(x => x.CategoryId == id);
        }
    }
}
