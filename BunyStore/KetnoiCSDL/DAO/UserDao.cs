using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KetnoiCSDL.EF;
using PagedList;
using Microsoft.AspNet.Identity;
namespace KetnoiCSDL.DAO

{

    public class UserDao
    {
        BunyStoreDbContext db = null;
        public UserDao()
        {
            db = new BunyStoreDbContext();
        }
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        
        public long InsertForFacebook(User entity)
        {
            var user = db.Users.SingleOrDefault(x => x.UserName == entity.UserName);
            if (user == null)
            {
                db.Users.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            else
            {
                return user.ID;
            }

        }

        public bool Login(String UserName, string PassWord)
        {
            var res = db.Users.Count(x => x.UserName == UserName && x.Password == PassWord);
        if (res > 0)
            {
                 return true;
            }
            else
            {
                return false;
            }
        }

        public bool Edit(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }

        public User GetById(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }
        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }
        public User GetByUserName(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }

        public User GetUserName(string userName)
        {
            return db.Users.Find(userName);
        }

        public IEnumerable<User> ListAllPaging(string searchString, int pageNumber, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(pageNumber, pageSize);
        }

        public bool CheckUserName(string userName)
        {
            return db.Users.Count(x => x.UserName == userName) > 0;
        }
        public bool CheckEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }
        public User GetID(int id)
        {
            return db.Users.Find(id);
        }

        public bool Update(User entity)
        {

            try
            {
               
                var user = db.Users.SingleOrDefault(x => x.UserName == entity.UserName);
                user.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                user.Status = entity.Status;
                if (!string.IsNullOrEmpty(entity.ProvinceID))
                {
                    user.ProvinceID = entity.ProvinceID;
                }
                if (!string.IsNullOrEmpty(entity.DistrictID))
                {
                    user.DistrictID = entity.DistrictID;
                }
                if (!string.IsNullOrEmpty(entity.PrecinctID))
                {
                    user.PrecinctID = entity.PrecinctID;
                }

                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }

    }
}
