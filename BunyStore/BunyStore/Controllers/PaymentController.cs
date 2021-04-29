using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BunnyStore.Common.Paypal;
using BunyStore.Models;
using KetnoiCSDL.DAO;
using KetnoiCSDL.EF;
using PayPal.Api;
using BunyStore.MomoAPI;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Data.Entity.Migrations;
using BunyStore.Common;

namespace BunyStore.Controllers
{
    public class PaymentController : Controller
    {
        private PayPal.Api.Payment payment;
        private BunyStoreDbContext db = new BunyStoreDbContext();
        private const string CartSession = "CartSession";

        //public static Customer temp;
        ////// GET: Paypal



        //tạm tắt
        public ActionResult PaymentWithPaypal(CustomerModel user)
        {
            //getting the apiContext as earlier
            //CustomerModel temp = new CustomerModel();
            //if (user.shipName != null)
            //{
            //    temp.shipName = user.shipName;
            //    temp.Phone = user.Phone;
            //    temp.Email = user.Email;
            //    temp.Address = user.Address;
            //    temp.DistrictID = user.DistrictID;
            //    temp.ProvinceID = user.ProvinceID;
            //    temp.PrecinctID = user.PrecinctID;
            //    CommonConstants.CustomerTemp = temp;
            //}
            APIContext apiContext = PaymentConfiguration.GetAPIContext();
            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class
                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    // So we have provided URL of this controller only
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority
                    +
                     "/Payment/PaymentWithPayPal?";
                    //guid we are generating for storing the paymentID received in session
                    //after calling the create function and it is used in the payment execution
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This section is executed when we have received all the payments parameters
                    // from the previous call to the function Create
                    // Executing a payment
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error" + ex.Message);
                return View("FailureView");
            }

            //Khoi tao hoa don
            var order = new KetnoiCSDL.EF.Order();

            //Kiểm tra xem khách hàng có đăng nhập hay không
            var user_temp = (BunyStore.Common.UserLogin)Session[BunyStore.Common.CommonConstants.USER_SESSION];
            if (user_temp != null)
            {
                //Nếu có đăng nhập thì gán vào UserID
                order.CustomerID = user_temp.UserID;
            }

            // tiến hành tạo bill
            order.CreatedDate = DateTime.Now;
            order.ShipAddress = Common.CommonConstants.CustomerTemp.Address;
            order.ShipMobile = Common.CommonConstants.CustomerTemp.Phone;
            order.ShipName = Common.CommonConstants.CustomerTemp.shipName;
            order.ShipEmail = Common.CommonConstants.CustomerTemp.Email;
            order.ShipPrecinct = Common.CommonConstants.CustomerTemp.PrecinctID;
            order.ShipProvince = Common.CommonConstants.CustomerTemp.ProvinceID;
            order.ShipDistrict = Common.CommonConstants.CustomerTemp.DistrictID;
            order.Status = "Chờ xử lý";
            order.PaymentForms = "Paypal";
            try
            {
                var id = new OrderDao().Insert(order);

                var cart = (List<CartItem>)Session[CartSession];
                var detailDao = new KetnoiCSDL.DAO.OrderDetailDao();
                decimal total = 0;
                foreach (var item in cart)
                {
                    // Tạo mới hóa đơn vào database
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail);


                    //cập nhật số lượng bán 
                    Product Update_product = db.Products.Where(p => p.ID == item.Product.ID).FirstOrDefault();

                    // chắc chắn rằng không rỗng
                    if (Update_product != null)
                    {
                        //vì trong database set mặc định là null nên không thể cộng được mà phải thêm 1 phần if để kiểm tra null hay có giá trị
                        if (Update_product.BoughtCount == null)
                        {
                            Update_product.BoughtCount = int.Parse(item.Quantity.ToString());
                        }
                        else
                        {
                            Update_product.BoughtCount += item.Quantity;
                        }
                        db.Products.AddOrUpdate(Update_product);
                        db.SaveChanges();
                        var a = Update_product.BoughtCount;
                    }
                    //db.SaveChanges();
                    Session[CartSession] = null;
                }
                Common.CommonConstants.CustomerTemp = null;
            }
            catch (Exception ex)
            {
                //ghi log
                return Redirect("/loi-thanh-toan");
            }

            return RedirectToAction("Success", "Cart");
        }


        ////Tùy theo máy
        //private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        //{
        //    var itemList = new ItemList() { items = new List<Item>() };
        //    var cart = Session[CartSession];
        //    var listcart = new List<CartItem>();
        //    listcart = (List<CartItem>)cart;

        //    double tong = 0; // giá trị của tổng hóa đơn
        //    string temp = ""; //  chuỗi sau khi đã xử lý hoàn chỉnh
        //    string str1 = "", str2 = ""; // 2 chuỗi con 
        //    int dem = 0; // biến tìm ra dấu,

        //    //Các giá trị bao gồm danh sách sản phẩm, thông tin đơn hàng
        //    //Sẽ được thay đổi bằng hành vi thao tác mua hàng trên website
        //    foreach (var item in listcart)
        //    {
        //        temp = (float.Parse(item.Product.Price.ToString()) / 23000).ToString();// chuyển từ VND->USD
        //        temp = Math.Round(Convert.ToDecimal(temp), 2).ToString(); // làm tròn lấy 2 số thập phân sau dấu phẩy

        //        //Tùy máy
        //        //dem = temp.IndexOf(","); // tìm kiếm vị trí dấu phẩy
        //        //str1 = temp.Substring(0, dem);// tách chuỗi phía trước dấu phẩy
        //        //str2 = temp.Substring(dem + 1, temp.Length - str1.Length -1); //tách chuỗi phía sau dấu phẩy
        //        //temp = str1 + "." + str2; // ghép chuỗi


        //        itemList.items.Add(new Item()
        //        {
        //            //Thông tin đơn hàng
        //            name = item.Product.Name + " x " + item.Quantity.ToString(),
        //            currency = "USD",
        //            price = temp,
        //            quantity = item.Quantity.ToString(),
        //        });
        //        tong += ((double.Parse(temp) * double.Parse(item.Quantity.ToString())));
        //        tong = double.Parse(Math.Round(Convert.ToDecimal(tong), 2).ToString());

        //        //Tùy máy
        //        //str1 = tong.ToString().Substring(0, dem);// tách chuỗi phía trước dấu phẩy
        //        //str2 = tong.ToString().Substring(dem + 1, tong.ToString().Length - str1.Length - 1); //tách chuỗi phía sau dấu phẩy
        //        //temp = str1 + "." + str2; // ghép chuỗi
        //    }

        //    //Hình thức thanh toán qua paypal
        //    var payer = new Payer() { payment_method = "paypal" };
        //    // Configure Redirect Urls here with RedirectUrls object
        //    var redirUrls = new RedirectUrls()
        //    {
        //        cancel_url = redirectUrl,
        //        return_url = redirectUrl
        //    };
        //    //các thông tin trong đơn hàng
        //    var details = new Details()
        //    {
        //        tax = "0",
        //        shipping = "0",
        //        subtotal = tong.ToString() // tổng
        //    };
        //    //Đơn vị tiền tệ và tổng đơn hàng cần thanh toán
        //    var amount = new Amount()
        //    {
        //        currency = "USD",
        //        total = tong.ToString(), // Total must be equal to sum of shipping, tax and subtotal. nếu không được mở khóa phần trên sửa tong thành temp
        //        details = details
        //    };
        //    var transactionList = new List<Transaction>();
        //    //Tất cả thông tin thanh toán cần đưa vào transaction
        //    transactionList.Add(new Transaction()
        //    {
        //        description = "Thông tin sản phẩm",
        //        invoice_number = Guid.NewGuid().ToString(),
        //        amount = amount,
        //        item_list = itemList
        //    });
        //    this.payment = new Payment()
        //    {
        //        intent = "sale",
        //        payer = payer,
        //        transactions = transactionList,
        //        redirect_urls = redirUrls
        //    };
        //    // Create a payment using a APIContext
        //    return this.payment.Create(apiContext);
        //}


        //Tùy theo máy
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var itemList = new ItemList() { items = new List<Item>() };
            var cart = Session[CartSession];
            var listcart = new List<CartItem>();
            listcart = (List<CartItem>)cart;

            double tong = 0; // giá trị của tổng hóa đơn
            string temp = ""; //  chuỗi sau khi đã xử lý hoàn chỉnh
            string str1 = "", str2 = ""; // 2 chuỗi con 
            int dem = 0; // biến tìm ra dấu,

            //Các giá trị bao gồm danh sách sản phẩm, thông tin đơn hàng
            //Sẽ được thay đổi bằng hành vi thao tác mua hàng trên website
            foreach (var item in listcart)
            {
                temp = (float.Parse(item.Product.Price.ToString()) / 23000).ToString();// chuyển từ VND->USD
                temp = Math.Round(Convert.ToDecimal(temp), 2).ToString(); // làm tròn lấy 2 số thập phân sau dấu phẩy
                dem = temp.IndexOf(","); // tìm kiếm vị trí dấu phẩy
                str1 = temp.Substring(0, dem);// tách chuỗi phía trước dấu phẩy
                str2 = temp.Substring(dem + 1, temp.Length - str1.Length - 1); //tách chuỗi phía sau dấu phẩy
                temp = str1 + "." + str2; // ghép chuỗi
                itemList.items.Add(new Item()
                {
                    //Thông tin đơn hàng
                    name = item.Product.Name + " x " + item.Quantity.ToString(),
                    currency = "USD",
                    price = temp,
                    quantity = item.Quantity.ToString(),
                });
                tong += ((double.Parse(item.Product.Price.ToString()) * double.Parse(item.Quantity.ToString())) / 23000);
                tong = double.Parse(Math.Round(Convert.ToDecimal(tong), 2).ToString());
                dem = tong.ToString().IndexOf(",");
                str1 = tong.ToString().Substring(0, dem);// tách chuỗi phía trước dấu phẩy
                str2 = tong.ToString().Substring(dem + 1, tong.ToString().Length - str1.Length - 1); //tách chuỗi phía sau dấu phẩy
                temp = str1 + "." + str2; // ghép chuỗi
            }


            //Hình thức thanh toán qua paypal
            var payer = new Payer() { payment_method = "paypal" };
            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };
            //các thông tin trong đơn hàng
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = temp // tổng
            };
            //Đơn vị tiền tệ và tổng đơn hàng cần thanh toán
            var amount = new Amount()
            {
                currency = "USD",
                total = temp, // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };
            var transactionList = new List<Transaction>();
            //Tất cả thông tin thanh toán cần đưa vào transaction
            transactionList.Add(new Transaction()
            {
                description = "Thông tin sản phẩm",
                invoice_number = Guid.NewGuid().ToString(),
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }


        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }



        //-------------------------------------------------------------------------------------------------------------------------------------------
        public ActionResult PaymentWithMoMo(User user)
        {
            var cart = (List<CartItem>)Session[CartSession];


            string endpoint = ConfigurationManager.AppSettings["endpoint"].ToString();
            string partnerCode = ConfigurationManager.AppSettings["partnerCode"].ToString();
            string accessKey = ConfigurationManager.AppSettings["accessKey"].ToString();
            string serectkey = ConfigurationManager.AppSettings["serectkey"].ToString();
            string orderInfo = ConfigurationManager.AppSettings["orderInfo"].ToString();
            string returnUrl = ConfigurationManager.AppSettings["returnUrl"].ToString();
            string notifyurl = ConfigurationManager.AppSettings["notifyurl"].ToString();

            string amount = cart.Sum(n => n.Product.Price).ToString();
            string orderid = Guid.NewGuid().ToString();
            string requestId = Guid.NewGuid().ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;
            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };
            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());
            JObject jmessage = JObject.Parse(responseFromMomo);
            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

        public ActionResult ReturnUrl()
        {
            string param = Request.QueryString.ToString().Substring(0, Request.QueryString.ToString().IndexOf("signature") - 1);
            param = Server.UrlDecode(param);
            MoMoSecurity crypto = new MoMoSecurity();
            string serectkey = ConfigurationManager.AppSettings["serectkey"].ToString();
            string signature = crypto.signSHA256(param, serectkey);
            if (signature != Request["signature"].ToString())
            {
                ViewBag.message = "Thong tin request khong hop le";
                return View();
            }
            if (!Request.QueryString["errorCode"].Equals("0"))
            {
                ViewBag.message = "Thanh toán thất bại";
            }
            else
            {

                //Khoi tao hoa don
                var order = new KetnoiCSDL.EF.Order();

                //Kiểm tra xem khách hàng có đăng nhập hay không
                var user_temp = (BunyStore.Common.UserLogin)Session[BunyStore.Common.CommonConstants.USER_SESSION];
                if (user_temp != null)
                {
                    //Nếu có đăng nhập thì gán vào UserID
                    order.CustomerID = user_temp.UserID;
                }

                // tiến hành tạo bill
                order.CreatedDate = DateTime.Now;
                order.ShipAddress = Common.CommonConstants.CustomerTemp.Address;
                order.ShipMobile = Common.CommonConstants.CustomerTemp.Phone;
                order.ShipName = Common.CommonConstants.CustomerTemp.shipName;
                order.ShipEmail = Common.CommonConstants.CustomerTemp.Email;
                order.ShipPrecinct = Common.CommonConstants.CustomerTemp.PrecinctID;
                order.ShipProvince = Common.CommonConstants.CustomerTemp.ProvinceID;
                order.ShipDistrict = Common.CommonConstants.CustomerTemp.DistrictID;
                order.Status = "Chờ xử lý";
                order.PaymentForms = "Momo";
                try
                {
                    var id = new OrderDao().Insert(order);

                    var cart = (List<CartItem>)Session[CartSession];
                    var detailDao = new KetnoiCSDL.DAO.OrderDetailDao();
                    decimal total = 0;
                    foreach (var item in cart)
                    {
                        // Tạo mới hóa đơn vào database
                        var orderDetail = new OrderDetail();
                        orderDetail.ProductID = item.Product.ID;
                        orderDetail.OrderID = id;
                        orderDetail.Price = item.Product.Price;
                        orderDetail.Quantity = item.Quantity;
                        detailDao.Insert(orderDetail);


                        //cập nhật số lượng bán 
                        Product Update_product = db.Products.Where(p => p.ID == item.Product.ID).FirstOrDefault();

                        // chắc chắn rằng không rỗng
                        if (Update_product != null)
                        {
                            //vì trong database set mặc định là null nên không thể cộng được mà phải thêm 1 phần if để kiểm tra null hay có giá trị
                            if (Update_product.BoughtCount == null)
                            {
                                Update_product.BoughtCount = int.Parse(item.Quantity.ToString());
                            }
                            else
                            {
                                Update_product.BoughtCount += item.Quantity;
                            }
                            db.Products.AddOrUpdate(Update_product);
                            db.SaveChanges();
                            var a = Update_product.BoughtCount;
                        }
                        //db.SaveChanges();
                        Session[CartSession] = null;
                    }
                    Common.CommonConstants.CustomerTemp = null;
                }
                catch (Exception ex)
                {
                    //ghi log
                    return Redirect("/loi-thanh-toan");
                }

                return RedirectToAction("Success", "Cart");

            }
            return View();
        }

        public JsonResult NotifyUrl()
        {
            string param = ""; //Request.QueryString.ToString().Substring(0, Request.QueryString.ToString().IndexOf("signature") - 1);
            param = "partner_code=" + Request["partner_code"] +
                    "&access_key=" + Request["access_key"] +
                    "&amount=" + Request["amount"] +
                    "&order_id=" + Request["order_id"] +
                    "&order_info=" + Request["order_info"] +
                    "&order_type=" + Request["order_type"] +
                    "&transaction_id=" + Request["transaction_id"] +
                    "&mesage=" + Request["mesage"] +
                    "&response_time=" + Request["response_time"] +
                    "&status_code=" + Request["status_code"];
            param = Server.UrlDecode(param);
            MoMoSecurity crypto = new MoMoSecurity();
            string serectkey = ConfigurationManager.AppSettings["serectkey"].ToString();
            string status_code = Request["status_code"].ToString();
            if ((status_code != "0"))
            {

            }
            else
            {

            }
            return Json("", JsonRequestBehavior.AllowGet);
        }


        //-------------------------------------------------------------------------------------------
        public ActionResult PaymentWithDelivety(User user)
        {
            //Khoi tao hoa don
            var order = new KetnoiCSDL.EF.Order();

            //Kiểm tra xem khách hàng có đăng nhập hay không
            var user_temp = (BunyStore.Common.UserLogin)Session[BunyStore.Common.CommonConstants.USER_SESSION];
            if (user_temp != null)
            {
                //Nếu có đăng nhập thì gán vào UserID
                order.CustomerID = user_temp.UserID;
            }

            // tiến hành tạo bill
            order.CreatedDate = DateTime.Now;
            order.ShipAddress = Common.CommonConstants.CustomerTemp.Address;
            order.ShipMobile = Common.CommonConstants.CustomerTemp.Phone;
            order.ShipName = Common.CommonConstants.CustomerTemp.shipName;
            order.ShipEmail = Common.CommonConstants.CustomerTemp.Email;
            order.ShipPrecinct = Common.CommonConstants.CustomerTemp.PrecinctID;
            order.ShipProvince = Common.CommonConstants.CustomerTemp.ProvinceID;
            order.ShipDistrict = Common.CommonConstants.CustomerTemp.DistrictID;
            order.Status = "Chờ xử lý";
            order.PaymentForms = "Thanh toán khi nhận hàng";
            try
            {
                var id = new OrderDao().Insert(order);

                var cart = (List<CartItem>)Session[CartSession];
                var detailDao = new KetnoiCSDL.DAO.OrderDetailDao();
                decimal total = 0;
                foreach (var item in cart)
                {
                    // Tạo mới hóa đơn vào database
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail);


                    //cập nhật số lượng bán 
                    Product Update_product = db.Products.Where(p => p.ID == item.Product.ID).FirstOrDefault();

                    // chắc chắn rằng không rỗng
                    if (Update_product != null)
                    {
                        //vì trong database set mặc định là null nên không thể cộng được mà phải thêm 1 phần if để kiểm tra null hay có giá trị
                        if (Update_product.BoughtCount == null)
                        {
                            Update_product.BoughtCount = int.Parse(item.Quantity.ToString());
                        }
                        else
                        {
                            Update_product.BoughtCount += item.Quantity;
                        }
                        db.Products.AddOrUpdate(Update_product);
                        db.SaveChanges();
                        var a = Update_product.BoughtCount;
                    }
                    //db.SaveChanges();
                    Session[CartSession] = null;
                }
                Common.CommonConstants.CustomerTemp = null;
            }
            catch (Exception ex)
            {
                //ghi log
                return Redirect("/loi-thanh-toan");
            }

            return RedirectToAction("Success", "Cart");
        }
    }
}