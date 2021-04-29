using KetnoiCSDL.EF;
using KetnoiCSDL.Infrastructure;
using KetnoiCSDL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KetnoiCSDL.Service
{
    public interface IContactDetailService
    {
        Contact GetDefaultContact();
    }
    public class ContactDetailService : IContactDetailService
    {
        IContactDetailRepository _contactDetailRepository;
        IUnitOfWork _unitOfWork;

        public ContactDetailService(IContactDetailRepository contactDetailRepository, IUnitOfWork unitOfWork)
        {
            this._contactDetailRepository = contactDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public Contact GetDefaultContact()
        {
            return _contactDetailRepository.GetSingleByCondition(x => (bool)x.Status);
        }
    }
}
