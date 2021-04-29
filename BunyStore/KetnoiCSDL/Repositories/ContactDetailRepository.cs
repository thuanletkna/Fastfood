using KetnoiCSDL.EF;
using KetnoiCSDL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KetnoiCSDL.Repositories
{
    public interface IContactDetailRepository : IRepository<Contact>
    {
    }

    public class ContactDetailRepository : RepositoryBase<Contact>, IContactDetailRepository
    {
        public ContactDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
