using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KetnoiCSDL.EF;
namespace KetnoiCSDL.DAO
{
    public partial class Admindao
    {
        BunyStoreDbContext data = null;
        //Tạo constructor
        public Admindao()
        {
            data = new BunyStoreDbContext();
        }

        //Lấy ra Account có UserName được truyền vào
        public Admin GetUserName(string username)
        {
            return data.Admins.SingleOrDefault(p => p.UserName == username);
        }

        //Kiểm tra đăng nhập
        public int Login(string username, string pass)
        {
            var res = data.Admins.SingleOrDefault(p => p.UserName == username);
            if (res == null)
            {
                return -1;
            }
            else
            {
                if (res.Password == pass)
                {

                    return 1;
                }
                else
                {
                    return -2;
                }
            }
        }

        public bool Login1(string username, string password)
        {
            var res = data.Admins.SingleOrDefault(p => p.UserName == username);
            if (res == null)
            {
                return false;
            }
            return true;
        }

        public User GetById(string userName)
        {
            return data.Users.SingleOrDefault(x => x.UserName == userName);
        }
        public bool CheckUserName(string userName)
        {
            return data.Users.Count(x => x.UserName == userName) > 0;
        }
        public bool CheckEmail(string email)
        {
            return data.Users.Count(x => x.Email == email) > 0;
        }        public User GetID(int id)
        {
            return data.Users.Find(id);
        }
    }
}
