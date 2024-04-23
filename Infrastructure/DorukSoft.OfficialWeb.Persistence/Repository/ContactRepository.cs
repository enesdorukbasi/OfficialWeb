using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using DorukSoft.OfficialWeb.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DorukSoft.OfficialWeb.Persistence.Repository
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        private readonly MSSqlContext _context;

        public ContactRepository(MSSqlContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Contact>> GetAllContactsWithIncludes()
        {
            var data1 = await _context.Set<Contact>()
                     .Where(x => x.ProductId != null) // ProductId null değilse dahil et
                     .Include(x => x.Product)
                     .ToListAsync();
            var data2 = await _context.Set<Contact>()
               .Where(x => x.ProductId == null)
               .ToListAsync();
            return data1.Concat(data2).ToList();

        }

        public async Task<Contact?> GetByIdContactsWithIncludes(int id)
        {
            var data1 = await _context.Set<Contact>()
                     .Where(x => x.ProductId != null)
                     .Include(x => x.Product)
                     .FirstOrDefaultAsync(x => x.ContactId == id);
            var data2 = await _context.Set<Contact>()
               .Where(x => x.ProductId == null)
              .FirstOrDefaultAsync(x => x.ContactId == id);
            if (data1 != null)
            {
                return data1;
            }
            else if(data2 != null)
            {
                return data2;
            }
            else
            {
                return null;
            }
        }

        public Task<Contact> GetContactWithMessages(int id)
        {
            throw new NotImplementedException();
        }
    }
}
