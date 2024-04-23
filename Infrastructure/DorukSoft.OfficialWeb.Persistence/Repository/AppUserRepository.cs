using Azure.Core;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Domain.Entities;
using DorukSoft.OfficialWeb.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DorukSoft.OfficialWeb.Persistence.Repository
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        private readonly MSSqlContext _context;

        public AppUserRepository(MSSqlContext context) : base(context)
        {
            _context = context;
        }

        public async Task<AppUser?> LoginAsync(string email, string password)
        {
            return await _context.Set<AppUser>().Include(x => x.AppRole).AsNoTracking().FirstAsync(x => x.Email == email && x.Password == PasswordHasher.HashPassword(password));
        }
    }
}
