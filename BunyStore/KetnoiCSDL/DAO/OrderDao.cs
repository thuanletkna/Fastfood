using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KetnoiCSDL.EF;
using PagedList;

namespace KetnoiCSDL.DAO
{
    public class OrderDao
    {
        BunyStoreDbContext db = null;
        public OrderDao()
        {
            db = new BunyStoreDbContext();
        }

        //Danh sách đơn hàng theo ID khách hàng
        public IEnumerable<Order> ListAllPagingAll(string searchString, int page, int pageSize, int ID)
        {
            IQueryable<Order> model = db.Orders.Where(p => p.CustomerID == ID);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ID.ToString().Contains(searchString) || x.ShipName.Contains(searchString) || x.ShipEmail.Contains(searchString) || x.ShipMobile.ToString().Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        //XỬ LÝ DANH SÁCH TẤT CẢ CÁC ĐƠN HÀNG
        public IEnumerable<Order> ListAllPagingAll(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ID.ToString().Contains(searchString) || x.ShipName.Contains(searchString) || x.ShipEmail.Contains(searchString) || x.ShipMobile.ToString().Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        //XỬ LÝ DANH SÁCH TẤT CẢ CÁC ĐƠN HÀNG ĐÃ GIAO
        public IEnumerable<Order> ListAllPagingTrue(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders.Where(x=> x.Status == "Đã giao hàng");
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ID.ToString().Contains(searchString) || x.ShipName.Contains(searchString) || x.ShipEmail.Contains(searchString) || x.ShipMobile.ToString().Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        //XỬ LÝ DANH SÁCH TẤT CẢ CÁC ĐƠN HÀNG ĐANG GIAO
        public IEnumerable<Order> ListAllPagingFalse(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders.Where(x => x.Status == "Đang giao hàng");
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ID.ToString().Contains(searchString) || x.ShipName.Contains(searchString) || x.ShipEmail.Contains(searchString) || x.ShipMobile.ToString().Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }
    }
}