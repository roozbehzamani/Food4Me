using MainEnterpriseProject.Models.DataModel;
using MainEnterpriseProject.Models.Domain;
using MainEnterpriseProject.Models.Repository;
using MainEnterpriseProject.Models.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;

namespace MainEnterpriseProject.Controllers
{
    public class AdminController : Controller
    {
        DataBase db = new DataBase();
        AllUtility utility = new AllUtility();
        ControllerRep cr = new ControllerRep();
        UserRepository userRep = new UserRepository();
        OrderRepository orderRep = new OrderRepository();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public void Export()
        {
            try
            {
                Excel.Application application = new Excel.Application();
                Excel.Workbook workbook = application.Workbooks.Add(System.Reflection.Missing.Value);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                worksheet.Cells[1, 1] = "Personeli";
                worksheet.Cells[1, 2] = "Fname";
                worksheet.Cells[1, 3] = "Lname";
                worksheet.Cells[1, 3] = "Date";

                int row = 2;

                var q = (from a in db.Tbl_OrderFactor
                         where a.Tab_users.access.Equals("دانشجو")
                         select a).ToList();

                List<ExcelExport> lstExcel = new List<ExcelExport>();

                foreach (var item in q)
                {
                    ExcelExport ee = new ExcelExport();

                    ee.SNumber = item.Tab_users.SNumber;
                    ee.SFamily = item.Tab_users.Family;
                    ee.SName = item.Tab_users.name;
                    ee.Date = item.Date.ToPersianDateString();

                    lstExcel.Add(ee);
                }

                foreach (var myItem in lstExcel)
                {
                    worksheet.Cells[row, 1] = myItem.SNumber;
                    worksheet.Cells[row, 2] = myItem.SName;
                    worksheet.Cells[row, 3] = myItem.SFamily;
                    worksheet.Cells[row, 4] = myItem.Date;
                    row++;
                }

                workbook.SaveAs("D:\\List.xls");
                workbook.Close();
                Marshal.ReleaseComObject(workbook);

                application.Quit();
                Marshal.FinalReleaseComObject(application);

                ViewBag.Result = "Done";
            }
            catch (Exception ex)
            {
                ViewBag.Result = ex.Message;
            }
        }
        [HttpGet]
        public ActionResult TopAdmin_AddRestuarant()
        {

            Session["access"] = "ادمین کل";
            if (Session["email"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Session["access"].ToString().Equals("ادمین کل"))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        [HttpPost]
        public ActionResult TopAdmin_AddRestuarant(Tbl_Restaurant tRestaurant, Tab_users tUser, string SaveChange, HttpPostedFileBase file)
        {
            if (Session["email"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"].ToString() != "ادمین کل")
                return RedirectToAction("Index", "Home");
            if (SaveChange.Equals("ذخیره"))
            {

                if (Session["email"].ToString().Equals(null))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string email = Session["email"].ToString();
                    if (Session["access"].ToString().Equals("ادمین کل"))
                    {
                        tUser.access = "رستوران";
                        tUser.enable = true;
                        tUser.password = "123456789";
                        tUser.image = "sdfsdf";

                        db.Tab_users.Add(tUser);
                        if (Convert.ToBoolean(db.SaveChanges()))
                        {
                            tRestaurant.resAvgServiceTime = "15";
                            tRestaurant.resPoints = 2;
                            tRestaurant.resLatLng = "34.2,45.8";
                            tRestaurant.userEmail = tUser.email;

                            if (file != null)
                            {

                                if (file.ContentType == "image/jpeg")
                                {
                                    if (file.ContentLength <= 10485760)
                                    {
                                        Random r = new Random();
                                        string ran = r.Next(100000, 999999).ToString() + ".jpg";
                                        var uploadurl = "ftp://Image@185.159.152.62";
                                        var uploadfilename = ran;
                                        var username = "Image";
                                        var password = "1741034681.Shm";
                                        Stream streamObj = file.InputStream;
                                        byte[] buffer = new byte[file.ContentLength];
                                        streamObj.Read(buffer, 0, buffer.Length);
                                        streamObj.Close();
                                        streamObj = null;
                                        string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                                        var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                                        requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                                        requestObj.Credentials = new NetworkCredential(username, password);
                                        Stream requestStream = requestObj.GetRequestStream();
                                        requestStream.Write(buffer, 0, buffer.Length);
                                        requestStream.Flush();
                                        requestStream.Close();
                                        requestObj = null;
                                        tRestaurant.resImage = ran;
                                    }
                                    else
                                    {
                                        return Content("سایز زیاد است");
                                    }
                                }
                                else
                                {
                                    return Content("فرمت درست نیس");
                                }
                            }
                            else
                            {
                                return Content("عکس موجود نیس");
                            }
                            db.Tbl_Restaurant.Add(tRestaurant);
                            if (Convert.ToBoolean(db.SaveChanges()))
                            {
                                return Content("همه چی درست");
                            }
                            else
                            {
                                return Content("مشکل");
                            }
                        }
                        else
                        {
                            return Content("یوزر ایراد دارد");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public ActionResult Res_EditFood(int ID)
        {
            if (Session["email"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"].ToString() != "رستوران")
                return RedirectToAction("Index", "Home");


            if (Session["email"] != null)
            {
                string u = Session["Email"].ToString();
                var q = (from a in db.tab_products
                         where a.id.Equals(ID)
                         select a).SingleOrDefault();

                return View(q);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult Res_EditFood(tab_products tproducts, string SaveChange, HttpPostedFileBase file)
        {

            if (Session["email"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"].ToString() != "رستوران")
                return RedirectToAction("Index", "Home");

            if (SaveChange.Equals("ذخیره"))
            {

                if (Session["email"].ToString().Equals(null))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string email = Session["email"].ToString();
                    if (Session["email"] != null)
                    {
                        var q = (from a in db.tab_products
                                 where a.id.Equals(tproducts.id)
                                 select a).SingleOrDefault();


                        q.cost = tproducts.cost;
                        q.name = tproducts.name;
                        q.CreateMaterial = tproducts.CreateMaterial;
                        q.bakingTime = tproducts.bakingTime;
                        q.menuID = tproducts.menuID;
                        q.Recipe = tproducts.Recipe;
                        q.FoodCount = tproducts.FoodCount;
                        q.OrderType = tproducts.OrderType;


                        if (file != null)
                        {
                            if (file.ContentType == "image/jpeg")
                            {
                                if (file.ContentLength <= 10485760)
                                {
                                    Random r = new Random();
                                    string ran = r.Next(100000, 999999).ToString() + ".jpg";
                                    var uploadurl = "ftp://Image@185.159.152.62";
                                    var uploadfilename = ran;
                                    var username = "Image";
                                    var password = "1741034681.Shm";
                                    Stream streamObj = file.InputStream;
                                    byte[] buffer = new byte[file.ContentLength];
                                    streamObj.Read(buffer, 0, buffer.Length);
                                    streamObj.Close();
                                    streamObj = null;
                                    string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                                    var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                                    requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                                    requestObj.Credentials = new NetworkCredential(username, password);
                                    Stream requestStream = requestObj.GetRequestStream();
                                    requestStream.Write(buffer, 0, buffer.Length);
                                    requestStream.Flush();
                                    requestStream.Close();
                                    requestObj = null;
                                    q.foodImage = ran;
                                }
                                else
                                {
                                    return Content("سایز زیاد است");
                                }
                            }
                            else
                            {
                                return Content("فرمت درست نیس");
                            }
                        }
                        else
                        {
                            db.tab_products.Attach(q);
                            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                            if (Convert.ToBoolean(db.SaveChanges()))
                            {
                                var qfoodPriceAvrage = (from a in db.tab_products
                                                        where a.resID.Equals(q.resID)
                                                        select a).ToList();

                                var qCurrentRes = (from a in db.Tbl_Restaurant
                                                   where a.ID.Equals(q.resID)
                                                   select a).SingleOrDefault();

                                int avg = (int)qfoodPriceAvrage.Average(s => s.cost);
                                qCurrentRes.FoodAvragePrice = avg;
                                db.Tbl_Restaurant.Attach(qCurrentRes);
                                db.Entry(qCurrentRes).State = System.Data.Entity.EntityState.Modified;
                                if (Convert.ToBoolean(db.SaveChanges()))
                                {
                                    int id = (from a in db.Tbl_Restaurant
                                              where a.userEmail.Equals(email)
                                              select a).SingleOrDefault().ID;
                                    List<tab_products> qFoodList = (from a in db.tab_products
                                                                    where a.resID.Equals(id)
                                                                    select a).ToList();

                                    return View("Res_FoodManager", qFoodList);
                                }
                                else
                                {
                                    int id = (from a in db.Tbl_Restaurant
                                              where a.userEmail.Equals(email)
                                              select a).SingleOrDefault().ID;
                                    List<tab_products> qFoodList = (from a in db.tab_products
                                                                    where a.resID.Equals(id)
                                                                    select a).ToList();

                                    return View("Res_FoodManager", qFoodList);
                                }
                            }
                            else
                            {
                                return Content("مشکل");
                            }
                        }

                        db.tab_products.Attach(q);
                        db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                        if (Convert.ToBoolean(db.SaveChanges()))
                        {
                            var qfoodPriceAvrage = (from a in db.tab_products
                                                    where a.resID.Equals(q.resID)
                                                    select a).ToList();

                            var qCurrentRes = (from a in db.Tbl_Restaurant
                                               where a.ID.Equals(q.resID)
                                               select a).SingleOrDefault();

                            int avg = (int)qfoodPriceAvrage.Average(s => s.cost);
                            qCurrentRes.FoodAvragePrice = avg;
                            db.Tbl_Restaurant.Attach(qCurrentRes);
                            db.Entry(qCurrentRes).State = System.Data.Entity.EntityState.Modified;
                            if (Convert.ToBoolean(db.SaveChanges()))
                            {
                                int id = (from a in db.Tbl_Restaurant
                                          where a.userEmail.Equals(email)
                                          select a).SingleOrDefault().ID;
                                List<tab_products> qFoodList = (from a in db.tab_products
                                                                where a.resID.Equals(id)
                                                                select a).ToList();

                                return View("Res_FoodManager", qFoodList);
                            }
                            else
                            {
                                int id = (from a in db.Tbl_Restaurant
                                          where a.userEmail.Equals(email)
                                          select a).SingleOrDefault().ID;
                                List<tab_products> qFoodList = (from a in db.tab_products
                                                                where a.resID.Equals(id)
                                                                select a).ToList();

                                return View("Res_FoodManager", qFoodList);
                            }
                        }
                        else
                        {
                            int id = (from a in db.Tbl_Restaurant
                                      where a.userEmail.Equals(email)
                                      select a).SingleOrDefault().ID;
                            List<tab_products> qFoodList = (from a in db.tab_products
                                                            where a.resID.Equals(id)
                                                            select a).ToList();

                            return View("Res_FoodManager", qFoodList);
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult TopAdmin_EditRestuarant(int ID)
        {


            if (Session["access"].ToString() != "ادمین کل")
                return RedirectToAction("Index", "Home");
            if (Session["email"] != null)
            {
                string u = Session["Email"].ToString();
                var q = (from a in db.Tbl_Restaurant
                         where a.ID.Equals(ID)
                         select a).SingleOrDefault();

                return View(q);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public ActionResult TopAdmin_EditRestuarant(Tbl_Restaurant tRestaurant, string SaveChange, HttpPostedFileBase file)
        {
            if (Session["email"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"].ToString() != "ادمین کل")
                return RedirectToAction("Index", "Home");

            if (SaveChange.Equals("ذخیره"))
            {

                if (Session["email"].ToString().Equals(null))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string email = Session["email"].ToString();
                    if (Session["email"] != null)
                    {
                        var q = (from a in db.Tbl_Restaurant
                                 where a.ID.Equals(tRestaurant.ID)
                                 select a).SingleOrDefault();

                        q.resType = tRestaurant.resType;
                        q.resPhone = tRestaurant.resPhone;
                        q.resName = tRestaurant.resName;
                        q.resAddress = tRestaurant.resAddress;

                        if (file != null)
                        {

                            if (file.ContentType == "image/jpeg")
                            {
                                if (file.ContentLength <= 10485760)
                                {
                                    Random r = new Random();
                                    string ran = r.Next(100000, 999999).ToString() + ".jpg";
                                    var uploadurl = "ftp://Image@185.159.152.62";
                                    var uploadfilename = ran;
                                    var username = "Image";
                                    var password = "1741034681.Shm";
                                    Stream streamObj = file.InputStream;
                                    byte[] buffer = new byte[file.ContentLength];
                                    streamObj.Read(buffer, 0, buffer.Length);
                                    streamObj.Close();
                                    streamObj = null;
                                    string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                                    var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                                    requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                                    requestObj.Credentials = new NetworkCredential(username, password);
                                    Stream requestStream = requestObj.GetRequestStream();
                                    requestStream.Write(buffer, 0, buffer.Length);
                                    requestStream.Flush();
                                    requestStream.Close();
                                    requestObj = null;
                                    q.resImage = ran;
                                }
                                else
                                {
                                    return Content("سایز زیاد است");
                                }
                            }
                            else
                            {
                                return Content("فرمت درست نیس");
                            }
                        }
                        else
                        {
                            db.Tbl_Restaurant.Attach(q);
                            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                            if (Convert.ToBoolean(db.SaveChanges()))
                            {
                                return Content("همه چی درست");
                            }
                            else
                            {
                                return Content("مشکل");
                            }
                        }

                        db.Tbl_Restaurant.Attach(q);
                        db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                        if (Convert.ToBoolean(db.SaveChanges()))
                        {
                            return Content("همه چی درست");
                        }
                        else
                        {
                            return Content("مشکل");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public ActionResult TopAdmin_AddFlorist()
        {

            Session["access"] = "ادمین کل";
            if (Session["email"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Session["access"].ToString().Equals("ادمین کل"))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        [HttpPost]
        public ActionResult TopAdmin_AddFlorist(Tbl_Floriest tFloriest, Tab_users tUser, string SaveChange, HttpPostedFileBase file)
        {
            if (Session["email"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"].ToString() != "ادمین کل")
                return RedirectToAction("Index", "Home");
            if (SaveChange.Equals("ذخیره"))
            {

                if (Session["email"].ToString().Equals(null))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string email = Session["email"].ToString();
                    if (Session["access"].ToString().Equals("ادمین کل"))
                    {
                        tUser.access = "گل فروشی";
                        tUser.enable = true;
                        tUser.password = "123456789";
                        tUser.image = "sdfsdf";

                        db.Tab_users.Add(tUser);
                        if (Convert.ToBoolean(db.SaveChanges()))
                        {
                            tFloriest.FLatLng = "34.2,45.8";
                            tFloriest.userEmail = tUser.email;

                            if (file != null)
                            {

                                if (file.ContentType == "image/jpeg")
                                {
                                    if (file.ContentLength <= 10485760)
                                    {
                                        Random r = new Random();
                                        string ran = r.Next(100000, 999999).ToString() + ".jpg";
                                        var uploadurl = "ftp://Image@185.159.152.62";
                                        var uploadfilename = ran;
                                        var username = "Image";
                                        var password = "1741034681.Shm";
                                        Stream streamObj = file.InputStream;
                                        byte[] buffer = new byte[file.ContentLength];
                                        streamObj.Read(buffer, 0, buffer.Length);
                                        streamObj.Close();
                                        streamObj = null;
                                        string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                                        var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                                        requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                                        requestObj.Credentials = new NetworkCredential(username, password);
                                        Stream requestStream = requestObj.GetRequestStream();
                                        requestStream.Write(buffer, 0, buffer.Length);
                                        requestStream.Flush();
                                        requestStream.Close();
                                        requestObj = null;
                                        tFloriest.FImage = ran;
                                    }
                                    else
                                    {
                                        return Content("سایز زیاد است");
                                    }
                                }
                                else
                                {
                                    return Content("فرمت درست نیس");
                                }
                            }
                            else
                            {
                                return Content("عکس موجود نیس");
                            }
                            db.Tbl_Floriest.Add(tFloriest);
                            if (Convert.ToBoolean(db.SaveChanges()))
                            {
                                return Content("همه چی درست");
                            }
                            else
                            {
                                return Content("مشکل");
                            }
                        }
                        else
                        {
                            return Content("یوزر ایراد دارد");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public ActionResult TopAdmin_EditFlorist(int ID)
        {

            if (Session["access"].ToString() != "ادمین کل")
                return RedirectToAction("Index", "Home");
            if (Session["email"] != null)
            {
                string u = Session["Email"].ToString();
                var q = (from a in db.Tbl_Floriest
                         where a.ID.Equals(ID)
                         select a).SingleOrDefault();

                return View(q);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult TopAdmin_EditFlorist(Tbl_Floriest tFloriest, string SaveChange, HttpPostedFileBase file)
        {
            if (Session["access"].ToString() != "ادمین کل")
                return RedirectToAction("Index", "Home");
            if (SaveChange.Equals("ذخیره"))
            {

                if (Session["email"].ToString().Equals(null))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string email = Session["email"].ToString();
                    if (Session["email"] != null)
                    {
                        var q = (from a in db.Tbl_Floriest
                                 where a.ID.Equals(tFloriest.ID)
                                 select a).SingleOrDefault();


                        q.FPhone = tFloriest.FPhone;
                        q.FName = tFloriest.FName;
                        q.FAddress = tFloriest.FAddress;

                        if (file != null)
                        {

                            if (file.ContentType == "image/jpeg")
                            {
                                if (file.ContentLength <= 10485760)
                                {
                                    Random r = new Random();
                                    string ran = r.Next(100000, 999999).ToString() + ".jpg";
                                    var uploadurl = "ftp://Image@185.159.152.62";
                                    var uploadfilename = ran;
                                    var username = "Image";
                                    var password = "1741034681.Shm";
                                    Stream streamObj = file.InputStream;
                                    byte[] buffer = new byte[file.ContentLength];
                                    streamObj.Read(buffer, 0, buffer.Length);
                                    streamObj.Close();
                                    streamObj = null;
                                    string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                                    var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                                    requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                                    requestObj.Credentials = new NetworkCredential(username, password);
                                    Stream requestStream = requestObj.GetRequestStream();
                                    requestStream.Write(buffer, 0, buffer.Length);
                                    requestStream.Flush();
                                    requestStream.Close();
                                    requestObj = null;
                                    q.FImage = ran;
                                }
                                else
                                {
                                    return Content("سایز زیاد است");
                                }
                            }
                            else
                            {
                                return Content("فرمت درست نیس");
                            }
                        }
                        else
                        {
                            db.Tbl_Floriest.Attach(q);
                            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                            if (Convert.ToBoolean(db.SaveChanges()))
                            {
                                return Content("همه چی درست");
                            }
                            else
                            {
                                return Content("مشکل");
                            }
                        }

                        db.Tbl_Floriest.Attach(q);
                        db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                        if (Convert.ToBoolean(db.SaveChanges()))
                        {
                            return Content("همه چی درست");
                        }
                        else
                        {
                            return Content("مشکل");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public ActionResult TopAdmin_AddConfectionery()
        {

            Session["access"] = "ادمین کل";
            if (Session["email"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Session["access"].ToString().Equals("ادمین کل"))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        [HttpPost]
        public ActionResult TopAdmin_AddConfectionery(Tbl_Confectionary tConfectionary, Tab_users tUser, string SaveChange, HttpPostedFileBase file)
        {


            if (SaveChange.Equals("ذخیره"))
            {

                if (Session["email"].ToString().Equals(null))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string email = Session["email"].ToString();
                    if (Session["access"].ToString().Equals("ادمین کل"))
                    {
                        tUser.access = "قنادی";
                        tUser.enable = true;
                        tUser.password = "123456789";
                        tUser.image = "sdfsdf";

                        db.Tab_users.Add(tUser);
                        if (Convert.ToBoolean(db.SaveChanges()))
                        {

                            tConfectionary.CLatLng = "34.2,45.8";
                            tConfectionary.userEmail = tUser.email;

                            if (file != null)
                            {

                                if (file.ContentType == "image/jpeg")
                                {
                                    if (file.ContentLength <= 10485760)
                                    {
                                        Random r = new Random();
                                        string ran = r.Next(100000, 999999).ToString() + ".jpg";
                                        var uploadurl = "ftp://Image@185.159.152.62";
                                        var uploadfilename = ran;
                                        var username = "Image";
                                        var password = "1741034681.Shm";
                                        Stream streamObj = file.InputStream;
                                        byte[] buffer = new byte[file.ContentLength];
                                        streamObj.Read(buffer, 0, buffer.Length);
                                        streamObj.Close();
                                        streamObj = null;
                                        string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                                        var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                                        requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                                        requestObj.Credentials = new NetworkCredential(username, password);
                                        Stream requestStream = requestObj.GetRequestStream();
                                        requestStream.Write(buffer, 0, buffer.Length);
                                        requestStream.Flush();
                                        requestStream.Close();
                                        requestObj = null;
                                        tConfectionary.CImage = ran;
                                    }
                                    else
                                    {
                                        return Content("سایز زیاد است");
                                    }
                                }
                                else
                                {
                                    return Content("فرمت درست نیس");
                                }
                            }
                            else
                            {
                                return Content("عکس موجود نیس");
                            }
                            db.Tbl_Confectionary.Add(tConfectionary);
                            if (Convert.ToBoolean(db.SaveChanges()))
                            {
                                return Content("همه چی درست");
                            }
                            else
                            {
                                return Content("مشکل");
                            }
                        }
                        else
                        {
                            return Content("یوزر ایراد دارد");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public ActionResult TopAdmin_EditConfectionery(int ID)
        {

            if (Session["access"].ToString() != "ادمین کل")
                return RedirectToAction("Index", "Home");


            if (Session["email"] != null)
            {
                string u = Session["Email"].ToString();
                var q = (from a in db.Tbl_Confectionary
                         where a.ID.Equals(ID)
                         select a).SingleOrDefault();

                return View(q);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult TopAdmin_EditConfectionery(Tbl_Confectionary tConfectionary, string SaveChange, HttpPostedFileBase file)
        {
            if (Session["access"].ToString() != "ادمین کل")
                return RedirectToAction("Index", "Home");

            if (SaveChange.Equals("ذخیره"))
            {

                if (Session["email"].ToString().Equals(null))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string email = Session["email"].ToString();
                    if (Session["email"] != null)
                    {
                        var q = (from a in db.Tbl_Confectionary
                                 where a.ID.Equals(tConfectionary.ID)
                                 select a).SingleOrDefault();


                        q.CPhone = tConfectionary.CPhone;
                        q.CName = tConfectionary.CName;
                        q.CAddress = tConfectionary.CAddress;

                        if (file != null)
                        {

                            if (file.ContentType == "image/jpeg")
                            {
                                if (file.ContentLength <= 10485760)
                                {
                                    Random r = new Random();
                                    string ran = r.Next(100000, 999999).ToString() + ".jpg";
                                    var uploadurl = "ftp://Image@185.159.152.62";
                                    var uploadfilename = ran;
                                    var username = "Image";
                                    var password = "1741034681.Shm";
                                    Stream streamObj = file.InputStream;
                                    byte[] buffer = new byte[file.ContentLength];
                                    streamObj.Read(buffer, 0, buffer.Length);
                                    streamObj.Close();
                                    streamObj = null;
                                    string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                                    var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                                    requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                                    requestObj.Credentials = new NetworkCredential(username, password);
                                    Stream requestStream = requestObj.GetRequestStream();
                                    requestStream.Write(buffer, 0, buffer.Length);
                                    requestStream.Flush();
                                    requestStream.Close();
                                    requestObj = null;
                                    q.CImage = ran;
                                }
                                else
                                {
                                    return Content("سایز زیاد است");
                                }
                            }
                            else
                            {
                                return Content("فرمت درست نیس");
                            }
                        }
                        else
                        {
                            db.Tbl_Confectionary.Attach(q);
                            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                            if (Convert.ToBoolean(db.SaveChanges()))
                            {
                                return Content("همه چی درست");
                            }
                            else
                            {
                                return Content("مشکل");
                            }
                        }

                        db.Tbl_Confectionary.Attach(q);
                        db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                        if (Convert.ToBoolean(db.SaveChanges()))
                        {
                            return Content("همه چی درست");
                        }
                        else
                        {
                            return Content("مشکل");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult TopAdmin_FoodManager()
        {



            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("ادمین کل"))
                {

                    List<Tbl_Restaurant> q = (from a in db.Tbl_Restaurant
                                              where a.resType.Equals("رستوران")
                                              select a).ToList();

                    return View(q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult TopAdmin_DeleteRes(int id)
        {



            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("ادمین کل"))
                {

                    var q = (from a in db.Tbl_Restaurant
                             where a.ID.Equals(id)
                             select a).SingleOrDefault();

                    db.Tbl_Restaurant.Remove(q);
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        return RedirectToAction("TopAdmin_FoodManager");
                    }
                    else
                    {
                        return RedirectToAction("TopAdmin_FoodManager");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult TopAdmin_CaffiShopManager()
        {


            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("ادمین کل"))
                {

                    List<Tbl_Restaurant> q = (from a in db.Tbl_Restaurant
                                              where a.resType.Equals("کافی شاپ")
                                              select a).ToList();

                    return View(q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult TopAdmin_DeleteCaffiShop(int id)
        {



            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("ادمین کل"))
                {

                    var q = (from a in db.Tbl_Restaurant
                             where a.ID.Equals(id)
                             select a).SingleOrDefault();

                    db.Tbl_Restaurant.Remove(q);
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        return RedirectToAction("TopAdmin_CaffiShopManager");
                    }
                    else
                    {
                        return RedirectToAction("TopAdmin_CaffiShopManager");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult TopAdmin_FastFoodManager()
        {




            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("ادمین کل"))
                {

                    List<Tbl_Restaurant> q = (from a in db.Tbl_Restaurant
                                              where a.resType.Equals("فست فود")
                                              select a).ToList();

                    return View(q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult TopAdmin_DeleteFastFood(int id)
        {


            if (Session["email"].ToString().Equals(null))
            {

                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("ادمین کل"))
                {

                    var q = (from a in db.Tbl_Restaurant
                             where a.ID.Equals(id)
                             select a).SingleOrDefault();

                    db.Tbl_Restaurant.Remove(q);
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        return RedirectToAction("TopAdmin_FastFoodManager");
                    }
                    else
                    {
                        return RedirectToAction("TopAdmin_FastFoodManager");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult TopAdmin_FloristManager()
        {


            if (Session["email"].ToString().Equals(null))
            {

                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("ادمین کل"))
                {

                    List<Tbl_Floriest> q = (from a in db.Tbl_Floriest
                                            select a).ToList();

                    return View(q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult TopAdmin_DeleteFlorist(int id)
        {


            if (Session["email"].ToString().Equals(null))
            {

                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("ادمین کل"))
                {

                    var q = (from a in db.Tbl_Floriest
                             where a.ID.Equals(id)
                             select a).SingleOrDefault();

                    db.Tbl_Floriest.Remove(q);
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        return RedirectToAction("TopAdmin_FastFoodManager");
                    }
                    else
                    {
                        return RedirectToAction("TopAdmin_FloristManager");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult TopAdmin_ConfectioneryManager()
        {



            if (Session["email"].ToString().Equals(null))
            {

                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("ادمین کل"))
                {

                    List<Tbl_Confectionary> q = (from a in db.Tbl_Confectionary
                                                 select a).ToList();

                    return View(q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult TopAdmin_DeleteConfectionery(int id)
        {

            Session["access"] = "ادمین کل";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("ادمین کل"))
                {

                    var q = (from a in db.Tbl_Confectionary
                             where a.ID.Equals(id)
                             select a).SingleOrDefault();

                    db.Tbl_Confectionary.Remove(q);
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        return RedirectToAction("TopAdmin_ConfectioneryManager");
                    }
                    else
                    {
                        return RedirectToAction("TopAdmin_ConfectioneryManager");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult TopAdmin_ContactManager()
        {

            Session["access"] = "ادمین کل";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("ادمین کل"))
                {

                    List<Tab_mail> q = (from a in db.Tab_mail
                                        select a).ToList();

                    return View(q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult TopAdmin_DeleteContact(int id)
        {

            Session["access"] = "ادمین کل";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("ادمین کل"))
                {

                    var q = (from a in db.Tab_mail
                             where a.id.Equals(id)
                             select a).SingleOrDefault();

                    db.Tab_mail.Remove(q);
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        return RedirectToAction("TopAdmin_ContactManager");
                    }
                    else
                    {
                        return RedirectToAction("TopAdmin_ContactManager");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult TopAdmin_SendAnswerEmail(string userEmail, string answerText)
        {

            Session["access"] = "ادمین کل";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("ادمین کل"))
                {
                    utility.SendEmail("smtp.gmail.com", "zamani.roozbeh.75@gmail.com", "1741034681shm", userEmail, "پاسخ نظر ارسال شده ", answerText);
                    List<Tab_mail> q = (from a in db.Tab_mail
                                        select a).ToList();

                    return View("TopAdmin_ContactManager", q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult SupportAdmin_SendAnswerEmail(string userEmail, string answerText)
        {

            Session["access"] = "مدیر پشتیبانی";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("مدیر پشتیبانی"))
                {
                    utility.SendEmail("smtp.gmail.com", "zamani.roozbeh.75@gmail.com", "1741034681shm", userEmail, "پاسخ نظر ارسال شده ", answerText);
                    List<Tab_mail> q = (from a in db.Tab_mail
                                        select a).ToList();

                    return View("SupportAdmin_ContactManager", q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult TopAdmin_SendEmailForUser(string userEmail, string answerText)
        {

            Session["access"] = "ادمین کل";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("ادمین کل"))
                {
                    utility.SendEmail("smtp.gmail.com", "zamani.roozbeh.75@gmail.com", "1741034681shm", userEmail, "پاسخ نظر ارسال شده ", answerText);
                    List<Tab_users> q = (from a in db.Tab_users
                                         select a).ToList();

                    return View("TopAdmin_UserManager", q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult StudentAdmin_SendEmailForUser(string userEmail, string answerText)
        {

            Session["access"] = "مدیر دانشجویی";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("مدیر دانشجویی"))
                {
                    utility.SendEmail("smtp.gmail.com", "zamani.roozbeh.75@gmail.com", "1741034681shm", userEmail, "پاسخ نظر ارسال شده ", answerText);
                    List<Tab_users> q = (from a in db.Tab_users
                                         where a.access.Equals("دانشجو")
                                         select a).ToList();

                    return View("StudentAdmin_UserManager", q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult SupportAdmin_ContactManager()
        {

            Session["access"] = "مدیر پشتیبانی";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("مدیر پشتیبانی"))
                {

                    List<Tab_mail> q = (from a in db.Tab_mail
                                        select a).ToList();

                    return View(q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult SupportAdmin_DeleteContact(int id)
        {

            Session["access"] = "مدیر پشتیبانی";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("مدیر پشتیبانی"))
                {

                    var q = (from a in db.Tab_mail
                             where a.id.Equals(id)
                             select a).SingleOrDefault();

                    db.Tab_mail.Remove(q);
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        return RedirectToAction("SupportAdmin_ContactManager");
                    }
                    else
                    {
                        return RedirectToAction("SupportAdmin_ContactManager");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult TopAdmin_UserManager()
        {

            Session["access"] = "ادمین کل";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("ادمین کل"))
                {

                    List<Tab_users> q = (from a in db.Tab_users
                                         select a).ToList();

                    return View(q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult TopAdmin_EnableUser(int id)
        {


            Session["access"] = "ادمین کل";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("ادمین کل"))
                {

                    var q = (from a in db.Tab_users
                             where a.id.Equals(id)
                             select a).SingleOrDefault();

                    q.enable = true;

                    db.Tab_users.Attach(q);
                    db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        List<Tab_users> qUser = (from a in db.Tab_users
                                                 select a).ToList();

                        return RedirectToAction("TopAdmin_UserManager");
                    }
                    else
                    {
                        return RedirectToAction("TopAdmin_UserManager");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
        }
        public ActionResult TopAdmin_DisableUser(int id)
        {


            Session["access"] = "ادمین کل";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("ادمین کل"))
                {

                    var q = (from a in db.Tab_users
                             where a.id.Equals(id)
                             select a).SingleOrDefault();

                    q.enable = false;

                    db.Tab_users.Attach(q);
                    db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        List<Tab_users> qUser = (from a in db.Tab_users
                                                 select a).ToList();

                        return RedirectToAction("TopAdmin_UserManager");
                    }
                    else
                    {
                        return RedirectToAction("TopAdmin_UserManager");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
        }
        public ActionResult StudentAdmin_UserManager()
        {

            Session["access"] = "مدیر دانشجویی";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("مدیر دانشجویی"))
                {

                    List<Tab_users> q = (from a in db.Tab_users
                                         where a.access.Equals("دانشجو")
                                         select a).ToList();

                    return View(q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult StudentAdmin_EnableUser(int id)
        {


            Session["access"] = "مدیر دانشجویی";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("مدیر دانشجویی"))
                {


                    var q = (from a in db.Tab_users
                             where a.id.Equals(id)
                             select a).SingleOrDefault();

                    q.enable = true;

                    db.Tab_users.Attach(q);
                    db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        List<Tab_users> qUser = (from a in db.Tab_users
                                                 select a).ToList();

                        return RedirectToAction("StudentAdmin_UserManager");
                    }
                    else
                    {
                        return RedirectToAction("StudentAdmin_UserManager");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
        }
        public ActionResult StudentAdmin_DisableUser(int id)
        {


            Session["access"] = "مدیر دانشجویی";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("مدیر دانشجویی"))
                {

                    var q = (from a in db.Tab_users
                             where a.id.Equals(id)
                             select a).SingleOrDefault();

                    q.enable = false;

                    db.Tab_users.Attach(q);
                    db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        List<Tab_users> qUser = (from a in db.Tab_users
                                                 select a).ToList();

                        return RedirectToAction("StudentAdmin_UserManager");
                    }
                    else
                    {
                        return RedirectToAction("StudentAdmin_UserManager");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
        }
        [HttpGet]
        public ActionResult Profile()
        {

            if (Session["email"] != null)
            {
                string u = Session["email"].ToString();
                var q = (from a in db.Tab_users
                         where a.email.Equals(u)
                         select a).SingleOrDefault();

                return View(q);
            }
            else
            {

                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult Profile(Tab_users tUser, string SaveChange, HttpPostedFileBase file)
        {
            if (SaveChange.Equals("ذخیره"))
            {

                if (Session["email"].ToString().Equals(null))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string email = Session["email"].ToString();
                    if (Session["email"] != null)
                    {
                        var q = (from a in db.Tab_users
                                 where a.email.Equals(email)
                                 select a).SingleOrDefault();

                        q.name = tUser.name;
                        q.ncode = tUser.ncode;
                        q.home_phone = tUser.home_phone;
                        q.sex = tUser.sex;
                        q.birth_date = tUser.birth_date;
                        q.home_address = tUser.home_address;

                        if (file != null)
                        {

                            if (file.ContentType == "image/jpeg")
                            {
                                if (file.ContentLength <= 10485760)
                                {
                                    Random r = new Random();
                                    string ran = r.Next(100000, 999999).ToString() + ".jpg";
                                    var uploadurl = "ftp://Image@185.159.152.62/imgpic";
                                    var uploadfilename = ran;
                                    var username = "Image";
                                    var password = "1741034681Shm";
                                    Stream streamObj = file.InputStream;
                                    byte[] buffer = new byte[file.ContentLength];
                                    streamObj.Read(buffer, 0, buffer.Length);
                                    streamObj.Close();
                                    streamObj = null;
                                    string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                                    var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                                    requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                                    requestObj.Credentials = new NetworkCredential(username, password);
                                    Stream requestStream = requestObj.GetRequestStream();
                                    requestStream.Write(buffer, 0, buffer.Length);
                                    requestStream.Flush();
                                    requestStream.Close();
                                    requestObj = null;
                                    q.image = ran;
                                }
                                else
                                {
                                    return Content("سایز زیاد است");
                                }
                            }
                            else
                            {
                                return Content("فرمت درست نیس");
                            }
                        }
                        else
                        {
                            db.Tab_users.Attach(q);
                            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                            if (Convert.ToBoolean(db.SaveChanges()))
                            {
                                return Content("همه چی درست");
                            }
                            else
                            {
                                return Content("مشکل");
                            }
                        }

                        db.Tab_users.Attach(q);
                        db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                        if (Convert.ToBoolean(db.SaveChanges()))
                        {
                            return Content("همه چی درست");
                        }
                        else
                        {
                            return Content("مشکل");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public ActionResult Res_AddFood()
        {
            if (Session["email"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"].ToString() != "رستوران")
                return RedirectToAction("Index", "Home");


            Session["access"] = "رستوران";
            return View();
        }
        [HttpPost]
        public ActionResult Res_AddFood(tab_products t, HttpPostedFileBase file)
        {
            if (Session["email"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"].ToString() != "رستوران")
                return RedirectToAction("Index", "Home");

            string userEmail = Session["email"].ToString();

            var q = (from a in db.Tbl_Restaurant
                     where a.userEmail.Equals(userEmail)
                     select a).SingleOrDefault();

            if (t.OrderType.Equals(null))
            {
                t.OrderType = false;
            }

            t.resID = q.ID;
            t.User_Email = userEmail;

            if (file != null)
            {

                if (file.ContentType == "image/jpeg")
                {
                    if (file.ContentLength <= 10485760)
                    {
                        Random r = new Random();
                        string ran = r.Next(100000, 999999).ToString() + ".jpg";
                        var uploadurl = "ftp://Image@185.159.152.62";
                        var uploadfilename = ran;
                        var username = "Image";
                        var password = "1741034681.Shm";
                        Stream streamObj = file.InputStream;
                        byte[] buffer = new byte[file.ContentLength];
                        streamObj.Read(buffer, 0, buffer.Length);
                        streamObj.Close();
                        streamObj = null;
                        string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                        var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                        requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                        requestObj.Credentials = new NetworkCredential(username, password);
                        Stream requestStream = requestObj.GetRequestStream();
                        requestStream.Write(buffer, 0, buffer.Length);
                        requestStream.Flush();
                        requestStream.Close();
                        requestObj = null;
                        t.foodImage = ran;
                    }
                    else
                    {
                        return Content("سایز زیاد است");
                    }
                }
                else
                {
                    return Content("فرمت درست نیس");
                }
            }
            else
            {
                db.tab_products.Add(t);
                if (Convert.ToBoolean(db.SaveChanges()))
                {
                    var qfoodPriceAvrage = (from a in db.tab_products
                                            where a.resID.Equals(q.ID)
                                            select a).ToList();

                    var qCurrentRes = (from a in db.Tbl_Restaurant
                                       where a.ID.Equals(q.ID)
                                       select a).SingleOrDefault();

                    int avg = (int)qfoodPriceAvrage.Average(s => s.cost);
                    qCurrentRes.FoodAvragePrice = avg;
                    db.Tbl_Restaurant.Attach(qCurrentRes);
                    db.Entry(qCurrentRes).State = System.Data.Entity.EntityState.Modified;
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        int id = (from a in db.Tbl_Restaurant
                                  where a.userEmail.Equals(userEmail)
                                  select a).SingleOrDefault().ID;
                        List<tab_products> qFoodList = (from a in db.tab_products
                                                        where a.resID.Equals(id)
                                                        select a).ToList();

                        return View("Res_FoodManager", qFoodList);
                    }
                    else
                    {
                        int id = (from a in db.Tbl_Restaurant
                                  where a.userEmail.Equals(userEmail)
                                  select a).SingleOrDefault().ID;
                        List<tab_products> qFoodList = (from a in db.tab_products
                                                        where a.resID.Equals(id)
                                                        select a).ToList();

                        return View("Res_FoodManager", qFoodList);
                    }
                }
                else
                {
                    int id = (from a in db.Tbl_Restaurant
                              where a.userEmail.Equals(userEmail)
                              select a).SingleOrDefault().ID;
                    List<tab_products> qFoodList = (from a in db.tab_products
                                                    where a.resID.Equals(id)
                                                    select a).ToList();

                    return View("Res_FoodManager", qFoodList);
                }
            }

            db.tab_products.Add(t);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                var qfoodPriceAvrage = (from a in db.tab_products
                                        where a.resID.Equals(q.ID)
                                        select a).ToList();

                var qCurrentRes = (from a in db.Tbl_Restaurant
                                   where a.ID.Equals(q.ID)
                                   select a).SingleOrDefault();

                int avg = (int)qfoodPriceAvrage.Average(s => s.cost);
                qCurrentRes.FoodAvragePrice = avg;
                db.Tbl_Restaurant.Attach(qCurrentRes);
                db.Entry(qCurrentRes).State = System.Data.Entity.EntityState.Modified;
                if (Convert.ToBoolean(db.SaveChanges()))
                {
                    int id = (from a in db.Tbl_Restaurant
                              where a.userEmail.Equals(userEmail)
                              select a).SingleOrDefault().ID;
                    List<tab_products> qFoodList = (from a in db.tab_products
                                                    where a.resID.Equals(id)
                                                    select a).ToList();

                    return View("Res_FoodManager", qFoodList);
                }
                else
                {
                    int id = (from a in db.Tbl_Restaurant
                              where a.userEmail.Equals(userEmail)
                              select a).SingleOrDefault().ID;
                    List<tab_products> qFoodList = (from a in db.tab_products
                                                    where a.resID.Equals(id)
                                                    select a).ToList();

                    return View("Res_FoodManager", qFoodList);
                }
            }
            else
            {
                int id = (from a in db.Tbl_Restaurant
                          where a.userEmail.Equals(userEmail)
                          select a).SingleOrDefault().ID;
                List<tab_products> qFoodList = (from a in db.tab_products
                                                where a.resID.Equals(id)
                                                select a).ToList();

                return View("Res_FoodManager", qFoodList);
            }

        }
        [HttpGet]
        public ActionResult Floriset_AddFlower()
        {

            Session["access"] = "گل فروشی";
            if (Session["email"] == null)
            {

                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Session["access"].ToString().Equals("گل فروشی"))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

        }
        [HttpPost]
        public ActionResult Floriset_AddFlower(Tbl_Flower tFlower, string SaveChange, HttpPostedFileBase file)
        {
            if (SaveChange.Equals("ذخیره"))
            {

                if (Session["email"].ToString().Equals(null))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string email = Session["email"].ToString();
                    if (Session["access"].ToString().Equals("گل فروشی"))
                    {
                        int id = (from a in db.Tbl_Floriest
                                  where a.userEmail.Equals(email)
                                  select a).SingleOrDefault().ID;

                        tFlower.FlorestID = id;
                        tFlower.FlwColor = "dfsdfsdfsdfsdf";

                        if (file != null)
                        {

                            if (file.ContentType == "image/jpeg")
                            {
                                if (file.ContentLength <= 10485760)
                                {
                                    Random r = new Random();
                                    string ran = r.Next(100000, 999999).ToString() + ".jpg";
                                    var uploadurl = "ftp://Image@185.159.152.62";
                                    var uploadfilename = ran;
                                    var username = "Image";
                                    var password = "1741034681.Shm";
                                    Stream streamObj = file.InputStream;
                                    byte[] buffer = new byte[file.ContentLength];
                                    streamObj.Read(buffer, 0, buffer.Length);
                                    streamObj.Close();
                                    streamObj = null;
                                    string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                                    var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                                    requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                                    requestObj.Credentials = new NetworkCredential(username, password);
                                    Stream requestStream = requestObj.GetRequestStream();
                                    requestStream.Write(buffer, 0, buffer.Length);
                                    requestStream.Flush();
                                    requestStream.Close();
                                    requestObj = null;
                                    tFlower.FlwImage = ran;
                                }
                                else
                                {
                                    return Content("سایز زیاد است");
                                }
                            }
                            else
                            {
                                return Content("فرمت درست نیس");
                            }
                        }
                        else
                        {
                            return Content("عکس موجود نیس");
                        }

                        db.Tbl_Flower.Add(tFlower);
                        if (Convert.ToBoolean(db.SaveChanges()))
                        {
                            return Content("همه چی درست");
                        }
                        else
                        {
                            return Content("مشکل");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public ActionResult Floriset_EditFlower(int ID)
        {

            if (Session["email"] != null)
            {
                string u = Session["Email"].ToString();
                var q = (from a in db.Tbl_Flower
                         where a.ID.Equals(ID)
                         select a).SingleOrDefault();

                return View(q);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult Floriset_EditFlower(Tbl_Flower tFlower, string SaveChange, HttpPostedFileBase file)
        {
            if (SaveChange.Equals("ذخیره"))
            {

                if (Session["email"].ToString().Equals(null))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string email = Session["email"].ToString();
                    if (Session["email"] != null)
                    {
                        var q = (from a in db.Tbl_Flower
                                 where a.ID.Equals(tFlower.ID)
                                 select a).SingleOrDefault();

                        q.FlwName = tFlower.FlwName;
                        q.FlwPrice = tFlower.FlwPrice;
                        q.FlwType = tFlower.FlwType;
                        q.FlwMaintenance = tFlower.FlwMaintenance;

                        if (file != null)
                        {

                            if (file.ContentType == "image/jpeg")
                            {
                                if (file.ContentLength <= 10485760)
                                {
                                    Random r = new Random();
                                    string ran = r.Next(100000, 999999).ToString() + ".jpg";
                                    var uploadurl = "ftp://Image@185.159.152.62/imgpic";
                                    var uploadfilename = ran;
                                    var username = "Image";
                                    var password = "1741034681Shm";
                                    Stream streamObj = file.InputStream;
                                    byte[] buffer = new byte[file.ContentLength];
                                    streamObj.Read(buffer, 0, buffer.Length);
                                    streamObj.Close();
                                    streamObj = null;
                                    string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                                    var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                                    requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                                    requestObj.Credentials = new NetworkCredential(username, password);
                                    Stream requestStream = requestObj.GetRequestStream();
                                    requestStream.Write(buffer, 0, buffer.Length);
                                    requestStream.Flush();
                                    requestStream.Close();
                                    requestObj = null;
                                    q.FlwImage = ran;
                                }
                                else
                                {
                                    return Content("سایز زیاد است");
                                }
                            }
                            else
                            {
                                return Content("فرمت درست نیس");
                            }
                        }
                        else
                        {
                            db.Tbl_Flower.Attach(q);
                            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                            if (Convert.ToBoolean(db.SaveChanges()))
                            {
                                return Content("همه چی درست");
                            }
                            else
                            {
                                return Content("مشکل");
                            }
                        }

                        db.Tbl_Flower.Attach(q);
                        db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                        if (Convert.ToBoolean(db.SaveChanges()))
                        {
                            return Content("همه چی درست");
                        }
                        else
                        {
                            return Content("مشکل");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public ActionResult Confectionery_AddSweets()
        {


            Session["access"] = "قنادی";
            if (Session["email"] == null)
            {

                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Session["access"].ToString().Equals("قنادی"))
                {
                    return View();
                }
                else
                {

                    return RedirectToAction("Index", "Home");
                }
            }
        }
        [HttpPost]
        public ActionResult Confectionery_AddSweets(Tbl_Sweets tSweet, string SaveChange, HttpPostedFileBase file)
        {

            if (SaveChange.Equals("ذخیره"))
            {

                if (Session["email"].ToString().Equals(null))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string email = Session["email"].ToString();
                    if (Session["access"].ToString().Equals("قنادی"))
                    {
                        int id = (from a in db.Tbl_Confectionary
                                  where a.userEmail.Equals(email)
                                  select a).SingleOrDefault().ID;

                        tSweet.ConfectionaryID = id;
                        tSweet.S_Description = "fgdfg";

                        if (file != null)
                        {

                            if (file.ContentType == "image/jpeg")
                            {
                                if (file.ContentLength <= 10485760)
                                {
                                    Random r = new Random();
                                    string ran = r.Next(100000, 999999).ToString() + ".jpg";
                                    var uploadurl = "ftp://Image@185.159.152.62/imgpic";
                                    var uploadfilename = ran;
                                    var username = "Image";
                                    var password = "1741034681Shm";
                                    Stream streamObj = file.InputStream;
                                    byte[] buffer = new byte[file.ContentLength];
                                    streamObj.Read(buffer, 0, buffer.Length);
                                    streamObj.Close();
                                    streamObj = null;
                                    string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                                    var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                                    requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                                    requestObj.Credentials = new NetworkCredential(username, password);
                                    Stream requestStream = requestObj.GetRequestStream();
                                    requestStream.Write(buffer, 0, buffer.Length);
                                    requestStream.Flush();
                                    requestStream.Close();
                                    requestObj = null;
                                    tSweet.SImage = ran;
                                }
                                else
                                {
                                    return Content("سایز زیاد است");
                                }
                            }
                            else
                            {
                                return Content("فرمت درست نیس");
                            }
                        }
                        else
                        {
                            return Content("عکس موجود نیس");
                        }

                        db.Tbl_Sweets.Add(tSweet);
                        if (Convert.ToBoolean(db.SaveChanges()))
                        {
                            return Content("همه چی درست");
                        }
                        else
                        {
                            return Content("مشکل");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public ActionResult Confectionery_EditSweets(int ID)
        {


            if (Session["access"].ToString() != "قنادی")
                return RedirectToAction("Index", "Home");
            if (Session["email"] != null)
            {
                string u = Session["Email"].ToString();
                var q = (from a in db.Tbl_Sweets
                         where a.ID.Equals(ID)
                         select a).SingleOrDefault();

                return View(q);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult Confectionery_EditSweets(Tbl_Sweets tSweet, string SaveChange, HttpPostedFileBase file)
        {
            if (SaveChange.Equals("ذخیره"))
            {

                if (Session["email"].ToString().Equals(null))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string email = Session["email"].ToString();
                    if (Session["email"] != null)
                    {
                        var q = (from a in db.Tbl_Sweets
                                 where a.ID.Equals(tSweet.ID)
                                 select a).SingleOrDefault();

                        q.S_Name = tSweet.S_Name;
                        q.S_Price = tSweet.S_Price;
                        q.S_Type = tSweet.S_Type;

                        if (file != null)
                        {

                            if (file.ContentType == "image/jpeg")
                            {
                                if (file.ContentLength <= 10485760)
                                {
                                    Random r = new Random();
                                    string ran = r.Next(100000, 999999).ToString() + ".jpg";
                                    var uploadurl = "ftp://Image@185.159.152.62";
                                    var uploadfilename = ran;
                                    var username = "Image";
                                    var password = "1741034681.Shm";
                                    Stream streamObj = file.InputStream;
                                    byte[] buffer = new byte[file.ContentLength];
                                    streamObj.Read(buffer, 0, buffer.Length);
                                    streamObj.Close();
                                    streamObj = null;
                                    string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                                    var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                                    requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                                    requestObj.Credentials = new NetworkCredential(username, password);
                                    Stream requestStream = requestObj.GetRequestStream();
                                    requestStream.Write(buffer, 0, buffer.Length);
                                    requestStream.Flush();
                                    requestStream.Close();
                                    requestObj = null;
                                    q.SImage = ran;
                                }
                                else
                                {
                                    return Content("سایز زیاد است");
                                }
                            }
                            else
                            {
                                return Content("فرمت درست نیس");
                            }
                        }
                        else
                        {
                            db.Tbl_Sweets.Attach(q);
                            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                            if (Convert.ToBoolean(db.SaveChanges()))
                            {
                                return Content("همه چی درست");
                            }
                            else
                            {
                                return Content("مشکل");
                            }
                        }

                        db.Tbl_Sweets.Attach(q);
                        db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                        if (Convert.ToBoolean(db.SaveChanges()))
                        {
                            return Content("همه چی درست");
                        }
                        else
                        {
                            return Content("مشکل");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Res_FoodManager()
        {

            Session["access"] = "رستوران";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("رستوران"))
                {
                    int id = (from a in db.Tbl_Restaurant
                              where a.userEmail.Equals(email)
                              select a).SingleOrDefault().ID;
                    List<tab_products> q = (from a in db.tab_products
                                            where a.resID.Equals(id)
                                            select a).ToList();

                    return View(q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Res_DeleteFood(int id)
        {
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("رستوران"))
                {
                    var q = (from a in db.tab_products
                             where a.id.Equals(id)
                             select a).SingleOrDefault();

                    db.tab_products.Remove(q);
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        var qfoodPriceAvrage = (from a in db.tab_products
                                                where a.resID.Equals(q.resID)
                                                select a).ToList();

                        var qCurrentRes = (from a in db.Tbl_Restaurant
                                           where a.ID.Equals(q.resID)
                                           select a).SingleOrDefault();

                        int avg = (int)qfoodPriceAvrage.Average(s => s.cost);
                        qCurrentRes.FoodAvragePrice = avg;
                        db.Tbl_Restaurant.Attach(qCurrentRes);
                        db.Entry(qCurrentRes).State = System.Data.Entity.EntityState.Modified;
                        if (Convert.ToBoolean(db.SaveChanges()))
                        {
                            int myID = (from a in db.Tbl_Restaurant
                                        where a.userEmail.Equals(email)
                                        select a).SingleOrDefault().ID;
                            List<tab_products> qFoodList = (from a in db.tab_products
                                                            where a.resID.Equals(myID)
                                                            select a).ToList();

                            return View("Res_FoodManager", qFoodList);
                        }
                        else
                        {
                            int myID = (from a in db.Tbl_Restaurant
                                        where a.userEmail.Equals(email)
                                        select a).SingleOrDefault().ID;
                            List<tab_products> qFoodList = (from a in db.tab_products
                                                            where a.resID.Equals(myID)
                                                            select a).ToList();

                            return View("Res_FoodManager", qFoodList);
                        }
                    }
                    else
                    {
                        return RedirectToAction("Res_FoodManager");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Floriset_FlowerManager()
        {

            Session["access"] = "گل فروشی";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("گل فروشی"))
                {
                    int id = (from a in db.Tbl_Floriest
                              where a.userEmail.Equals(email)
                              select a).SingleOrDefault().ID;
                    List<Tbl_Flower> q = (from a in db.Tbl_Flower
                                          where a.FlorestID.Equals(id)
                                          select a).ToList();

                    return View(q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Floriset_DeleteFlower(int id)
        {

            Session["access"] = "گل فروشی";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("گل فروشی"))
                {
                    var q = (from a in db.Tbl_Flower
                             where a.ID.Equals(id)
                             select a).SingleOrDefault();

                    db.Tbl_Flower.Remove(q);
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        return RedirectToAction("Floriset_FlowerManager");
                    }
                    else
                    {
                        return RedirectToAction("Floriset_FlowerManager");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Confectionery_SweetsManager()
        {

            Session["access"] = "قنادی";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("قنادی"))
                {
                    int id = (from a in db.Tbl_Confectionary
                              where a.userEmail.Equals(email)
                              select a).SingleOrDefault().ID;
                    List<Tbl_Sweets> q = (from a in db.Tbl_Sweets
                                          where a.ConfectionaryID.Equals(id)
                                          select a).ToList();

                    return View(q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

        }
        public ActionResult Confectionery_DeleteSweets(int id)
        {

            Session["access"] = "قنادی";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("قنادی"))
                {
                    var q = (from a in db.Tbl_Sweets
                             where a.ID.Equals(id)
                             select a).SingleOrDefault();

                    db.Tbl_Sweets.Remove(q);
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        return RedirectToAction("Confectionery_SweetsManager");
                    }
                    else
                    {
                        return RedirectToAction("Confectionery_SweetsManager");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        [HttpGet]
        public ActionResult Res_AddPacking()
        {

            Session["access"] = "رستوران";
            if (Session["email"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Session["access"].ToString().Equals("رستوران"))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        [HttpPost]
        public ActionResult Res_AddPacking(Tbl_Packing tPacking, string SaveChange, HttpPostedFileBase file)
        {
            if (SaveChange.Equals("ذخیره"))
            {

                if (Session["email"].ToString().Equals(null))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string email = Session["email"].ToString();
                    if (Session["access"].ToString().Equals("رستوران"))
                    {


                        if (file != null)
                        {

                            if (file.ContentType == "image/jpeg")
                            {
                                if (file.ContentLength <= 10485760)
                                {
                                    Random r = new Random();
                                    string ran = r.Next(100000, 999999).ToString() + ".jpg";
                                    var uploadurl = "ftp://Image@185.159.152.62";
                                    var uploadfilename = ran;
                                    var username = "Image";
                                    var password = "1741034681.Shm";
                                    Stream streamObj = file.InputStream;
                                    byte[] buffer = new byte[file.ContentLength];
                                    streamObj.Read(buffer, 0, buffer.Length);
                                    streamObj.Close();
                                    streamObj = null;
                                    string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                                    var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                                    requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                                    requestObj.Credentials = new NetworkCredential(username, password);
                                    Stream requestStream = requestObj.GetRequestStream();
                                    requestStream.Write(buffer, 0, buffer.Length);
                                    requestStream.Flush();
                                    requestStream.Close();
                                    requestObj = null;
                                    tPacking.packingImage = ran;
                                }
                                else
                                {
                                    return Content("سایز زیاد است");
                                }
                            }
                            else
                            {
                                return Content("فرمت درست نیس");
                            }
                        }
                        else
                        {
                            return Content("عکس موجود نیس");
                        }

                        db.Tbl_Packing.Add(tPacking);
                        if (Convert.ToBoolean(db.SaveChanges()))
                        {
                            return Content("همه چی درست");
                        }
                        else
                        {
                            return Content("مشکل");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public ActionResult Res_AddDecoration()
        {

            Session["access"] = "رستوران";
            if (Session["email"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Session["access"].ToString().Equals("رستوران"))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        [HttpPost]
        public ActionResult Res_AddDecoration(Tbl_FoodAlbume tAlbume, string SaveChange, HttpPostedFileBase file)
        {
            if (SaveChange.Equals("ذخیره"))
            {

                if (Session["email"].ToString().Equals(null))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string email = Session["email"].ToString();
                    if (Session["access"].ToString().Equals("رستوران"))
                    {


                        if (file != null)
                        {

                            if (file.ContentType == "image/jpeg")
                            {
                                if (file.ContentLength <= 10485760)
                                {
                                    Random r = new Random();
                                    string ran = r.Next(100000, 999999).ToString() + ".jpg";
                                    var uploadurl = "ftp://Image@185.159.152.62";
                                    var uploadfilename = ran;
                                    var username = "Image";
                                    var password = "1741034681.Shm";
                                    Stream streamObj = file.InputStream;
                                    byte[] buffer = new byte[file.ContentLength];
                                    streamObj.Read(buffer, 0, buffer.Length);
                                    streamObj.Close();
                                    streamObj = null;
                                    string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                                    var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                                    requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                                    requestObj.Credentials = new NetworkCredential(username, password);
                                    Stream requestStream = requestObj.GetRequestStream();
                                    requestStream.Write(buffer, 0, buffer.Length);
                                    requestStream.Flush();
                                    requestStream.Close();
                                    requestObj = null;
                                    tAlbume.albumImage = ran;
                                }
                                else
                                {
                                    return Content("سایز زیاد است");
                                }
                            }
                            else
                            {
                                return Content("فرمت درست نیس");
                            }
                        }
                        else
                        {
                            return Content("عکس موجود نیس");
                        }

                        db.Tbl_FoodAlbume.Add(tAlbume);
                        if (Convert.ToBoolean(db.SaveChanges()))
                        {
                            return Content("همه چی درست");
                        }
                        else
                        {
                            return Content("مشکل");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Res_AddOption()
        {

            Session["access"] = "رستوران";
            if (Session["email"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Session["access"].ToString().Equals("رستوران"))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Res_PackingManager()
        {

            Session["access"] = "رستوران";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("رستوران"))
                {
                    List<Tbl_Packing> lstPacking = new List<Tbl_Packing>();

                    int id = (from a in db.Tbl_Restaurant
                              where a.userEmail.Equals(email)
                              select a).SingleOrDefault().ID;

                    var q = (from a in db.tab_products
                             where a.resID.Equals(id)
                             select a).ToList();

                    foreach (var item in q)
                    {
                        List<Tbl_Packing> qqPacking = (from a in db.Tbl_Packing
                                                       where a.foodID.Equals(item.id)
                                                       select a).ToList();

                        lstPacking.AddRange(qqPacking);
                    }


                    return View(lstPacking);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Res_DeletePacking(int id)
        {

            Session["access"] = "رستوران";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("رستوران"))
                {
                    var q = (from a in db.Tbl_Packing
                             where a.ID.Equals(id)
                             select a).SingleOrDefault();

                    db.Tbl_Packing.Remove(q);
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        return RedirectToAction("Res_PackingManager");
                    }
                    else
                    {
                        return RedirectToAction("Res_PackingManager");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        [HttpGet]
        public ActionResult Res_EditPacking(int ID)
        {

            Session["access"] = "رستوران";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("رستوران"))
                {
                    var q = (from a in db.Tbl_Packing
                             where a.ID.Equals(ID)
                             select a).SingleOrDefault();

                    return View(q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        [HttpPost]
        public ActionResult Res_EditPacking(Tbl_Packing tpacking, string SaveChange, HttpPostedFileBase file)
        {
            if (SaveChange.Equals("ذخیره"))
            {

                if (Session["email"].ToString().Equals(null))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string email = Session["email"].ToString();
                    if (Session["email"] != null)
                    {
                        var q = (from a in db.Tbl_Packing
                                 where a.ID.Equals(tpacking.ID)
                                 select a).SingleOrDefault();

                        q.packingDescription = tpacking.packingDescription;
                        q.foodID = tpacking.foodID;

                        if (file != null)
                        {

                            if (file.ContentType == "image/jpeg")
                            {
                                if (file.ContentLength <= 10485760)
                                {
                                    Random r = new Random();
                                    string ran = r.Next(100000, 999999).ToString() + ".jpg";
                                    var uploadurl = "ftp://Image@185.159.152.62";
                                    var uploadfilename = ran;
                                    var username = "Image";
                                    var password = "1741034681.Shm";
                                    Stream streamObj = file.InputStream;
                                    byte[] buffer = new byte[file.ContentLength];
                                    streamObj.Read(buffer, 0, buffer.Length);
                                    streamObj.Close();
                                    streamObj = null;
                                    string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                                    var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                                    requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                                    requestObj.Credentials = new NetworkCredential(username, password);
                                    Stream requestStream = requestObj.GetRequestStream();
                                    requestStream.Write(buffer, 0, buffer.Length);
                                    requestStream.Flush();
                                    requestStream.Close();
                                    requestObj = null;
                                    q.packingImage = ran;
                                }
                                else
                                {
                                    return Content("سایز زیاد است");
                                }
                            }
                            else
                            {
                                return Content("فرمت درست نیس");
                            }
                        }
                        else
                        {
                            db.Tbl_Packing.Attach(q);
                            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                            if (Convert.ToBoolean(db.SaveChanges()))
                            {
                                return Content("همه چی درست");
                            }
                            else
                            {
                                return Content("مشکل");
                            }
                        }

                        db.Tbl_Packing.Attach(q);
                        db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                        if (Convert.ToBoolean(db.SaveChanges()))
                        {
                            return Content("همه چی درست");
                        }
                        else
                        {
                            return Content("مشکل");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Res_DecoratiocnManager()
        {

            Session["access"] = "رستوران";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("رستوران"))
                {
                    List<Tbl_FoodAlbume> lstPacking = new List<Tbl_FoodAlbume>();

                    int id = (from a in db.Tbl_Restaurant
                              where a.userEmail.Equals(email)
                              select a).SingleOrDefault().ID;

                    var q = (from a in db.tab_products
                             where a.resID.Equals(id)
                             select a).ToList();

                    foreach (var item in q)
                    {
                        List<Tbl_FoodAlbume> qqPacking = (from a in db.Tbl_FoodAlbume
                                                          where a.foodID.Equals(item.id)
                                                          select a).ToList();

                        lstPacking.AddRange(qqPacking);
                    }


                    return View(lstPacking);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Res_DeleteDecoration(int id)
        {

            Session["access"] = "رستوران";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("رستوران"))
                {
                    var q = (from a in db.Tbl_FoodAlbume
                             where a.ID.Equals(id)
                             select a).SingleOrDefault();

                    db.Tbl_FoodAlbume.Remove(q);
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        return RedirectToAction("Res_DecoratiocnManager");
                    }
                    else
                    {
                        return RedirectToAction("Res_DecoratiocnManager");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        [HttpGet]
        public ActionResult Res_EditDecoration(int ID)
        {

            Session["access"] = "رستوران";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("رستوران"))
                {
                    var q = (from a in db.Tbl_FoodAlbume
                             where a.ID.Equals(ID)
                             select a).SingleOrDefault();

                    return View(q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        [HttpPost]
        public ActionResult Res_EditDecoration(Tbl_FoodAlbume tAlbum, string SaveChange, HttpPostedFileBase file)
        {
            if (SaveChange.Equals("ذخیره"))
            {

                if (Session["email"].ToString().Equals(null))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string email = Session["email"].ToString();
                    if (Session["email"] != null)
                    {
                        var q = (from a in db.Tbl_FoodAlbume
                                 where a.ID.Equals(tAlbum.ID)
                                 select a).SingleOrDefault();

                        q.albumName = tAlbum.albumName;
                        q.foodID = tAlbum.foodID;

                        if (file != null)
                        {

                            if (file.ContentType == "image/jpeg")
                            {
                                if (file.ContentLength <= 10485760)
                                {
                                    Random r = new Random();
                                    string ran = r.Next(100000, 999999).ToString() + ".jpg";
                                    var uploadurl = "ftp://Image@185.159.152.62";
                                    var uploadfilename = ran;
                                    var username = "Image";
                                    var password = "1741034681.Shm";
                                    Stream streamObj = file.InputStream;
                                    byte[] buffer = new byte[file.ContentLength];
                                    streamObj.Read(buffer, 0, buffer.Length);
                                    streamObj.Close();
                                    streamObj = null;
                                    string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                                    var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                                    requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                                    requestObj.Credentials = new NetworkCredential(username, password);
                                    Stream requestStream = requestObj.GetRequestStream();
                                    requestStream.Write(buffer, 0, buffer.Length);
                                    requestStream.Flush();
                                    requestStream.Close();
                                    requestObj = null;
                                    q.albumImage = ran;
                                }
                                else
                                {
                                    return Content("سایز زیاد است");
                                }
                            }
                            else
                            {
                                return Content("فرمت درست نیس");
                            }
                        }
                        else
                        {
                            db.Tbl_FoodAlbume.Attach(q);
                            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                            if (Convert.ToBoolean(db.SaveChanges()))
                            {
                                return Content("همه چی درست");
                            }
                            else
                            {
                                return Content("مشکل");
                            }
                        }

                        db.Tbl_FoodAlbume.Attach(q);
                        db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                        if (Convert.ToBoolean(db.SaveChanges()))
                        {
                            return Content("همه چی درست");
                        }
                        else
                        {
                            return Content("مشکل");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Res_OptionManager()
        {
            return View();
        }
        public ActionResult Res_CommentFoodManager()
        {
            if (Session["email"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"].ToString() != "رستوران")
                return RedirectToAction("Index", "Home");

            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("رستوران"))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Floriset_CommentFlowerManager(int ID)
        {

            Session["access"] = "گل فروشی";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("گل فروشی"))
                {
                    var q = (from a in db.Tbl_FlowerComment
                             where a.flowerID.Equals(ID)
                             select a).ToList();
                    return View(q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Confectionery_CommentSweetsManager(int ID)
        {

            Session["access"] = "قنادی";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("قنادی"))
                {
                    var q = (from a in db.Tbl_SweetsComment
                             where a.SweetsID.Equals(ID)
                             select a).ToList();
                    return View(q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Res_FoodOrderManager()
        {
            if (Session["email"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"].ToString() != "رستوران")
                return RedirectToAction("Index", "Home");

            string email = Session["email"].ToString();

            var q = (from a in db.Tbl_Restaurant
                     where a.userEmail.Equals(email)
                     select a).SingleOrDefault();

            var qOrder = (from a in db.Tbl_OrderFactor
                          where a.ResID.Equals(q.ID)
                          select a).OrderByDescending(i => i.Date).ToList();
            return View(qOrder);
        }
        public ActionResult FinancialAdmin_AllFoodOrderManager()
        {
            if (Session["email"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"].ToString() != "مدیر مالی")
                return RedirectToAction("Index", "Home");
            return View();
        }
        public ActionResult Florist_FlowerOrderManager()
        {
            return View();
        }
        public ActionResult Confectionery_SweetsOrderManager()
        {
            return View();
        }
        public ActionResult Res_RestaurantReservManager()
        {

            Session["access"] = "رستوران";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("رستوران"))
                {
                    int id = (from a in db.Tbl_Restaurant
                              where a.userEmail.Equals(email)
                              select a).SingleOrDefault().ID;
                    List<Tbl_ResReservationFactor> q = (from a in db.Tbl_ResReservationFactor
                                                        where a.ResID.Equals(id)
                                                        select a).ToList();

                    return View(q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Res_FoodReservManager()
        {

            Session["access"] = "رستوران";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("رستوران"))
                {
                    int id = (from a in db.Tbl_Restaurant
                              where a.userEmail.Equals(email)
                              select a).SingleOrDefault().ID;
                    List<Tbl_FoodReservationFactor> q = (from a in db.Tbl_FoodReservationFactor
                                                         where a.ResID.Equals(id)
                                                         select a).ToList();

                    return View(q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Florist_FlowerReservManager()
        {

            Session["access"] = "گل فروشی";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("گل فروشی"))
                {
                    int id = (from a in db.Tbl_Floriest
                              where a.userEmail.Equals(email)
                              select a).SingleOrDefault().ID;
                    List<Tbl_FlowerReservationFactor> q = (from a in db.Tbl_FlowerReservationFactor
                                                           where a.FloristID.Equals(id)
                                                           select a).ToList();

                    return View(q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Confectionery_SweetsReservManager()
        {

            Session["access"] = "قنادی";
            if (Session["email"].ToString().Equals(null))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                if (Session["access"].ToString().Equals("قنادی"))
                {
                    int id = (from a in db.Tbl_Confectionary
                              where a.userEmail.Equals(email)
                              select a).SingleOrDefault().ID;
                    List<Tbl_SweetReservationFactor> q = (from a in db.Tbl_SweetReservationFactor
                                                          where a.ConfectionaryID.Equals(id)
                                                          select a).ToList();

                    return View(q);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult ResFinancialReport()
        {
            return View();
        }
        public ActionResult FloristFinancialReport()
        {
            return View();
        }
        public ActionResult ConfectioneryFinancialReport()
        {
            return View();
        }
        public ActionResult FinancialAdmin_AddCreditManager()
        {
            return View();
        }
        public ActionResult TopAdmin_AddCreditManager()
        {
            if (Session["email"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"].ToString() != "ادمین کل")
                return RedirectToAction("Index", "Home");


            return View();
        }
        public ActionResult FinancialManager()
        {
            if (Session["email"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"].ToString() != "مدیر مالی")
                return RedirectToAction("Index", "Home");

            return View();
        }
        public ActionResult TopAdmin_FinancialManager()
        {
            if (Session["email"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"].ToString() != "ادمین کل")
                return RedirectToAction("Index", "Home");

            return View();
        }
        public ActionResult AllFinancialTransaction()
        {
            if (Session["email"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"].ToString() != "ادمین کل")
                return RedirectToAction("Index", "Home");

            return View();
        }

        public JsonResult GetUserList(string access)
        {
            if (Session["email"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"].ToString() != "ادمین کل" && Session["access"].ToString() != "مدیر مالی")
                return Json(0, JsonRequestBehavior.AllowGet);
            var q = (from a in db.Tab_users
                     where a.access.Equals(access)
                     select a).ToList();

            List<UserWithCredit> lstUserWithCredit = new List<UserWithCredit>();

            foreach (var item in q)
            {
                UserWithCredit user = new UserWithCredit();

                user.Credit = cr.SingleUserCredit(item.email);
                user.ID = item.id;
                user.Name = item.name;
                user.Phone = item.mob_phone;
                user.SNumber = item.SNumber;
                user.UserEmail = item.email;

                lstUserWithCredit.Add(user);
            }
            return Json(lstUserWithCredit, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddSingleUserCredit(string credit, int UserID)
        {
            if (Session["email"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"].ToString() != "ادمین کل" && Session["access"].ToString() != "مدیر مالی")
                return Json(0, JsonRequestBehavior.AllowGet);

            var qUser = (from a in db.Tab_users
                         where a.id.Equals(UserID)
                         select a).SingleOrDefault();


            Tbl_Credit tbl_Credit = new Tbl_Credit();

            tbl_Credit.Time = DateTime.Now.ToString("t");
            tbl_Credit.Credit = Convert.ToInt32(credit);
            tbl_Credit.Date = DateTime.Now;
            tbl_Credit.UserEmail = qUser.email;
            tbl_Credit.RootUser = "U";
            tbl_Credit.TransactionCode = "0";

            db.Tbl_Credit.Add(tbl_Credit);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                var q = (from a in db.Tab_users
                         where a.access.Equals(qUser.access)
                         select a).ToList();

                List<UserWithCredit> lstUserWithCredit = new List<UserWithCredit>();

                foreach (var item in q)
                {
                    UserWithCredit user = new UserWithCredit();

                    user.Credit = cr.SingleUserCredit(item.email);
                    user.ID = item.id;
                    user.Name = item.name;
                    user.Phone = item.mob_phone;
                    user.SNumber = item.SNumber;
                    user.UserEmail = item.email;

                    lstUserWithCredit.Add(user);
                }
                return Json(lstUserWithCredit, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var q = (from a in db.Tab_users
                         where a.access.Equals(qUser.access)
                         select a).ToList();

                List<UserWithCredit> lstUserWithCredit = new List<UserWithCredit>();

                foreach (var item in q)
                {
                    UserWithCredit user = new UserWithCredit();

                    user.Credit = cr.SingleUserCredit(item.email);
                    user.ID = item.id;
                    user.Name = item.name;
                    user.Phone = item.mob_phone;
                    user.SNumber = item.SNumber;
                    user.UserEmail = item.email;

                    lstUserWithCredit.Add(user);
                }
                return Json(lstUserWithCredit, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddOption(string opName, string opPrice, string opGroupName, string opType)
        {
            if (Session["email"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"].ToString() != "رستوران")
                return Json(0, JsonRequestBehavior.AllowGet);

            var qItem = (from a in db.Tbl_FoodItemGroup
                         where a.GroupName.Equals(opGroupName)
                         select a).SingleOrDefault();

            if (qItem == null)
            {
                Tbl_FoodItemGroup tbl_FoodItemGroup = new Tbl_FoodItemGroup();

                tbl_FoodItemGroup.GroupName = opGroupName;

                db.Tbl_FoodItemGroup.Add(tbl_FoodItemGroup);
                if (Convert.ToBoolean(db.SaveChanges()))
                {
                    bool flag;
                    Tbl_CustomizationFood tbl_Customization = new Tbl_CustomizationFood();

                    tbl_Customization.foodItem = opName;
                    tbl_Customization.itemGroupID = tbl_FoodItemGroup.ID;
                    tbl_Customization.itemPrice = Convert.ToInt32(opPrice);
                    if (opType.Equals("کاهنده"))
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    tbl_Customization.Used = flag;
                    db.Tbl_CustomizationFood.Add(tbl_Customization);
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        return Json(1, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(0, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(-1, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                bool flag;
                Tbl_CustomizationFood tbl_Customization = new Tbl_CustomizationFood();

                tbl_Customization.foodItem = opName;
                tbl_Customization.itemGroupID = qItem.ID;
                tbl_Customization.itemPrice = Convert.ToInt32(opPrice);
                if (opType.Equals("کاهنده"))
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                tbl_Customization.Used = flag;
                db.Tbl_CustomizationFood.Add(tbl_Customization);
                if (Convert.ToBoolean(db.SaveChanges()))
                {
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult AddFood(string price, string name, string createMaterial, string time, string recepi, string type)
        {
            if (Session["email"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"].ToString() != "رستوران")
                return Json(0, JsonRequestBehavior.AllowGet);

            tab_products tblProduct = new tab_products();

            tblProduct.bakingTime = time;
            tblProduct.cost = Convert.ToInt32(price);
            tblProduct.CreateMaterial = createMaterial;
            tblProduct.foodImage = "fsdfsdf";
            tblProduct.menuID = 1;
            tblProduct.name = name;
            tblProduct.Recipe = recepi;

            return null;
        }
        public JsonResult FoodCommentList(string foodID)
        {
            if (Session["email"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"].ToString() != "رستوران")
                return Json(0, JsonRequestBehavior.AllowGet);

            int x = Convert.ToInt32(foodID);
            List<Tab_comments> q = (from a in db.Tab_comments
                                    where a.product_id.Equals(x)
                                    select a).ToList();

            List<FoodComment> lstComment = new List<FoodComment>();

            foreach (var item in q)
            {
                FoodComment t = new FoodComment();
                t.id = item.id;
                t.name = item.name;
                t.phone = item.phone;
                t.read = item.read;
                t.Stars = item.Stars;
                t.text = item.text;
                t.data = item.data.ToString("d");
                t.email = item.email;
                t.cm_like = item.cm_like;
                t.cm_dislike = item.cm_dislike;
                t.confirm = item.confirm;
                t.product_id = item.product_id;



                lstComment.Add(t);
            }

            return Json(lstComment, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreditForAll(string access, string credit)
        {
            if (Session["email"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"].ToString() != "ادمین کل" && Session["access"].ToString() != "مدیر مالی")
                return Json(0, JsonRequestBehavior.AllowGet);

            var q = (from a in db.Tab_users
                     where a.access.Equals(access)
                     select a).ToList();

            var qAllCredit = (from a in db.Tbl_Credit
                              where a.RootUser.Equals("U")
                              select a).ToList();

            db.Tbl_Credit.RemoveRange(qAllCredit);
            db.SaveChanges();

            List<Tbl_Credit> lstCredit = new List<Tbl_Credit>();

            foreach (var item in q)
            {
                Tbl_Credit tbl_Credit = new Tbl_Credit();

                tbl_Credit.Credit = Convert.ToInt32(credit);
                tbl_Credit.Date = DateTime.Now;
                tbl_Credit.RootUser = "U";
                tbl_Credit.Time = DateTime.Now.ToString("t");
                tbl_Credit.TransactionCode = "0";
                tbl_Credit.UserEmail = item.email;

                lstCredit.Add(tbl_Credit);
            }

            db.Tbl_Credit.AddRange(lstCredit);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                var qUser = (from a in db.Tab_users
                             where a.access.Equals(access)
                             select a).ToList();

                List<UserWithCredit> lstUserWithCredit = new List<UserWithCredit>();

                foreach (var item in qUser)
                {
                    UserWithCredit user = new UserWithCredit();

                    user.Credit = cr.SingleUserCredit(item.email);
                    user.ID = item.id;
                    user.Name = item.name;
                    user.Phone = item.mob_phone;
                    user.SNumber = item.SNumber;
                    user.UserEmail = item.email;

                    lstUserWithCredit.Add(user);
                }
                return Json(lstUserWithCredit, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var qUser = (from a in db.Tab_users
                             where a.access.Equals(access)
                             select a).ToList();

                List<UserWithCredit> lstUserWithCredit = new List<UserWithCredit>();

                foreach (var item in qUser)
                {
                    UserWithCredit user = new UserWithCredit();

                    user.Credit = cr.SingleUserCredit(item.email);
                    user.ID = item.id;
                    user.Name = item.name;
                    user.Phone = item.mob_phone;
                    user.SNumber = item.SNumber;
                    user.UserEmail = item.email;

                    lstUserWithCredit.Add(user);
                }
                return Json(lstUserWithCredit, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CreditForSomeUsers(string access, string credit, string userList)
        {
            if (Session["email"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"].ToString() != "ادمین کل" && Session["access"].ToString() != "مدیر مالی")
                return Json(0, JsonRequestBehavior.AllowGet);

            int MyID = Convert.ToInt32(userList);
            var qUser = (from a in db.Tab_users
                         where a.id.Equals(MyID)
                         select a).SingleOrDefault();

            Tbl_Credit tbl_Credit = new Tbl_Credit();

            tbl_Credit.Credit = Convert.ToInt32(credit);
            tbl_Credit.Date = DateTime.Now;
            tbl_Credit.RootUser = "U";
            tbl_Credit.Time = DateTime.Now.ToString("t");
            tbl_Credit.TransactionCode = "0";
            tbl_Credit.UserEmail = qUser.email;

            db.Tbl_Credit.Add(tbl_Credit);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                var q = (from a in db.Tab_users
                         where a.access.Equals(access)
                         select a).ToList();

                List<UserWithCredit> lstUserWithCredit = new List<UserWithCredit>();

                foreach (var item in q)
                {
                    UserWithCredit user = new UserWithCredit();

                    user.Credit = cr.SingleUserCredit(item.email);
                    user.ID = item.id;
                    user.Name = item.name;
                    user.Phone = item.mob_phone;
                    user.SNumber = item.SNumber;
                    user.UserEmail = item.email;

                    lstUserWithCredit.Add(user);
                }
                return Json(lstUserWithCredit, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var q = (from a in db.Tab_users
                         where a.access.Equals(access)
                         select a).ToList();

                List<UserWithCredit> lstUserWithCredit = new List<UserWithCredit>();

                foreach (var item in q)
                {
                    UserWithCredit user = new UserWithCredit();

                    user.Credit = cr.SingleUserCredit(item.email);
                    user.ID = item.id;
                    user.Name = item.name;
                    user.Phone = item.mob_phone;
                    user.SNumber = item.SNumber;
                    user.UserEmail = item.email;

                    lstUserWithCredit.Add(user);
                }
                return Json(lstUserWithCredit, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAllTransactions()
        {
            if (Session["email"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"].ToString() != "ادمین کل")
                return Json(0, JsonRequestBehavior.AllowGet);
            var qFirstCredit = (from a in db.Tbl_Credit
                                select a).OrderBy(s => s.Date).FirstOrDefault();

            DateTime firstDT = qFirstCredit.Date;

            List<DailyTransaction> lstDaily = new List<DailyTransaction>();

            for (int i = 0; i < 30; i++)
            {
                DailyTransaction dt = new DailyTransaction();
                dt.NowDate = firstDT.AddDays(i).ToString("d");
                dt.TotalCredit = userRep.DailyCredit(firstDT.AddDays(i)).ToString();
                dt.TotalOrder = userRep.Date(firstDT.AddDays(i)).ToString();

                lstDaily.Add(dt);
            }

            return Json(lstDaily, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTransactions(string ID)
        {
            if (Session["email"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"].ToString() != "مدیر مالی")
                return Json(0, JsonRequestBehavior.AllowGet);

            var q = (from a in db.Tbl_OrderFactor
                     select a).OrderBy(i => i.Date).ToList();

            DateTime firstDate = q.First().Date;
            DateTime endDate = DateTime.Now;

            double DateCount = (endDate - firstDate).TotalDays;
            int CountDate = Convert.ToInt32(DateCount);
            DateTime first = firstDate;
            List<DailyTransaction> lstDaily = new List<DailyTransaction>();

            for (int i = 0; i < CountDate; i++)
            {
                DailyTransaction dt = new DailyTransaction();

                dt.NowDate = first.AddDays(i).ToString("d");
                dt.TotalCredit = userRep.DailyTotalSales(first.AddDays(i)).ToString();
                dt.TotalOrder = userRep.DailyTotalDelivery(first.AddDays(i)).ToString();

                lstDaily.Add(dt);
            }
            return Json(lstDaily, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RemoveComment(string ID)
        {
            if (Session["email"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"].ToString() != "رستوران")
                return Json(0, JsonRequestBehavior.AllowGet);
            int myID = Convert.ToInt32(ID);

            var q = (from a in db.Tab_comments
                     where a.id.Equals(myID)
                     select a).SingleOrDefault();

            int x = q.product_id;

            db.Tab_comments.Remove(q);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                List<Tab_comments> qAllCommnet = (from a in db.Tab_comments
                                                  where a.product_id.Equals(x)
                                                  select a).ToList();

                List<FoodComment> lstComment = new List<FoodComment>();

                foreach (var item in qAllCommnet)
                {
                    FoodComment t = new FoodComment();
                    t.id = item.id;
                    t.name = item.name;
                    t.phone = item.phone;
                    t.read = item.read;
                    t.Stars = item.Stars;
                    t.text = item.text;
                    t.data = item.data.ToString("d");
                    t.email = item.email;
                    t.cm_like = item.cm_like;
                    t.cm_dislike = item.cm_dislike;
                    t.confirm = item.confirm;
                    t.product_id = item.product_id;



                    lstComment.Add(t);
                }

                return Json(lstComment, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Tab_comments> qAllCommnet = (from a in db.Tab_comments
                                                  where a.product_id.Equals(x)
                                                  select a).ToList();

                List<FoodComment> lstComment = new List<FoodComment>();

                foreach (var item in qAllCommnet)
                {
                    FoodComment t = new FoodComment();
                    t.id = item.id;
                    t.name = item.name;
                    t.phone = item.phone;
                    t.read = item.read;
                    t.Stars = item.Stars;
                    t.text = item.text;
                    t.data = item.data.ToString("d");
                    t.email = item.email;
                    t.cm_like = item.cm_like;
                    t.cm_dislike = item.cm_dislike;
                    t.confirm = item.confirm;
                    t.product_id = item.product_id;



                    lstComment.Add(t);
                }

                return Json(lstComment, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult EditOrderStatus(string factorID)
        {
            if (Session["email"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"].ToString() != "رستوران")
                return Json(0, JsonRequestBehavior.AllowGet);
            string ID = factorID.Split(',')[0];
            string status = factorID.Split(',')[1];

            int IDD = Convert.ToInt32(ID);

            var q = (from a in db.Tbl_OrderFactor
                     where a.ID.Equals(IDD)
                     select a).SingleOrDefault();

            q.OrderStatus = status;
            db.Tbl_OrderFactor.Attach(q);
            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult OrderFilterByDate(string startDate, string endDate)
        {
            if (Session["email"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"].ToString() != "رستوران")
                return Json(0, JsonRequestBehavior.AllowGet);

            var qItemDelete = (from a in db.Tbl_ResPrintItem
                               select a).ToList();
            if (qItemDelete.Count() > 0)
            {
                db.Tbl_ResPrintItem.RemoveRange(qItemDelete);
                db.SaveChanges();
            }

            var qFactorDelete = (from a in db.Tbl_ResPrintFactor
                                 select a).SingleOrDefault();

            if (qFactorDelete != null)
            {
                db.Tbl_ResPrintFactor.Remove(qFactorDelete);
                db.SaveChanges();
            }
            
            string year = startDate.Split('/')[0];
            string month = startDate.Split('/')[1];
            string day = startDate.Split('/')[2];

            string FinalYear = utility.PersianToEnglish(year);
            string FinalMonth = utility.PersianToEnglish(month);
            string FinalDay = utility.PersianToEnglish(day);

            string newDate = FinalDay + "/" + FinalMonth + "/" + FinalYear;

            string endYear = endDate.Split('/')[0];
            string endMonth = endDate.Split('/')[1];
            string endDayay = endDate.Split('/')[2];

            string FinalEndYear = utility.PersianToEnglish(endYear);
            string FinalEndMonth = utility.PersianToEnglish(endMonth);
            string FinalEndDay = utility.PersianToEnglish(endDayay);

            string newEndDate = FinalEndDay + "/" + FinalEndMonth + "/" + FinalEndYear;

            DateTime StartDT = DateTime.ParseExact(newDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime EndDT = DateTime.ParseExact(newEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string email = Session["email"].ToString();
            var q = (from a in db.Tbl_Restaurant
                     where a.userEmail.Equals(email)
                     select a).SingleOrDefault();

            var qOrder = (from a in db.Tbl_OrderFactor
                          where a.ResID.Equals(q.ID)
                          select a).ToList();

            List<Tbl_OrderFactor> lst = new List<Tbl_OrderFactor>();
            lst.AddRange(qOrder);

            foreach (var item in qOrder)
            {
                string dt = item.Date.ToString("d");
                DateTime itemDT = DateTime.ParseExact(dt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (itemDT < StartDT || itemDT > EndDT)
                {
                    lst.Remove(item);
                }
            }
            qOrder.Clear();
            qOrder.AddRange(lst);

            List<SearchOrferFood> lstSearchResult = new List<SearchOrferFood>();
            List<Tbl_ResPrintItem> tbl_ResPrintItem = new List<Tbl_ResPrintItem>();
            int myPrintCounter = 1;
            int totalCost = 0;

            foreach (var item2 in qOrder)
            {
                SearchOrferFood sof = new SearchOrferFood();
                Tbl_ResPrintItem oi = new Tbl_ResPrintItem();

                sof.Address = item2.Tbl_Address.Address;
                sof.Date = item2.Date.ToPersianDateString();
                sof.ID = item2.ID;
                sof.Name = userRep.GetSingleUserInfo(item2.UserEmail).name;
                sof.Phone = userRep.GetSingleUserInfo(item2.UserEmail).mob_phone;
                sof.Price = orderRep.TotalfactorPrice(item2.ID).ToString();
                sof.Status = item2.OrderStatus;
                sof.Time = item2.Time.ToString("t");

                totalCost += orderRep.TotalfactorPrice(item2.ID);
                oi.Counter = myPrintCounter;
                oi.Name = userRep.GetSingleUserInfo(item2.UserEmail).name;
                oi.OrderDate = item2.Date.ToPersianDateString();
                oi.OrderPrice = orderRep.TotalfactorPrice(item2.ID).ToString();
                oi.OrderTime = item2.Time.ToString("t");
                if (userRep.GetSingleUserInfo(item2.UserEmail).access.Equals("دانشجو"))
                {
                    oi.SNumber = userRep.GetSingleUserInfo(item2.UserEmail).SNumber;
                }
                else
                {
                    oi.SNumber = userRep.GetSingleUserInfo(item2.UserEmail).mob_phone;
                }

                tbl_ResPrintItem.Add(oi);
                lstSearchResult.Add(sof);
                myPrintCounter++;
            }

            db.Tbl_ResPrintItem.AddRange(tbl_ResPrintItem);
            db.SaveChanges();

            Tbl_ResPrintFactor tbl_ResPrintFactor = new Tbl_ResPrintFactor();

            tbl_ResPrintFactor.EndDate = newEndDate;
            tbl_ResPrintFactor.ResName = q.resName;
            tbl_ResPrintFactor.ResUserName = q.Tab_users.name + " " + q.Tab_users.Family;
            tbl_ResPrintFactor.StartDate = newDate;
            tbl_ResPrintFactor.TotalPrice = totalCost.ToString();

            db.Tbl_ResPrintFactor.Add(tbl_ResPrintFactor);
            db.SaveChanges();


            return Json(lstSearchResult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SpecialResOrderFilterByDate(string startDate, string endDate, int resID)
        {
            if (Session["email"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"].ToString() != "مدیر مالی")
                return Json(0, JsonRequestBehavior.AllowGet);

            var qItemDelete = (from a in db.Tbl_ResPrintItem
                               select a).ToList();
            if (qItemDelete.Count() > 0)
            {
                db.Tbl_ResPrintItem.RemoveRange(qItemDelete);
                db.SaveChanges();
            }

            var qFactorDelete = (from a in db.Tbl_ResPrintFactor
                                 select a).SingleOrDefault();

            if (qFactorDelete != null)
            {
                db.Tbl_ResPrintFactor.Remove(qFactorDelete);
                db.SaveChanges();
            }

            string year = startDate.Split('/')[0];
            string month = startDate.Split('/')[1];
            string day = startDate.Split('/')[2];

            string FinalYear = utility.PersianToEnglish(year);
            string FinalMonth = utility.PersianToEnglish(month);
            string FinalDay = utility.PersianToEnglish(day);

            string newDate = FinalDay + "/" + FinalMonth + "/" + FinalYear;

            string endYear = endDate.Split('/')[0];
            string endMonth = endDate.Split('/')[1];
            string endDayay = endDate.Split('/')[2];

            string FinalEndYear = utility.PersianToEnglish(endYear);
            string FinalEndMonth = utility.PersianToEnglish(endMonth);
            string FinalEndDay = utility.PersianToEnglish(endDayay);

            string newEndDate = FinalEndDay + "/" + FinalEndMonth + "/" + FinalEndYear;

            DateTime StartDT = DateTime.ParseExact(newDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime EndDT = DateTime.ParseExact(newEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var qOrder = (from a in db.Tbl_OrderFactor
                          where a.ResID.Equals(resID)
                          select a).ToList();

            List<Tbl_OrderFactor> lst = new List<Tbl_OrderFactor>();
            lst.AddRange(qOrder);

            foreach (var item in qOrder)
            {
                string dt = item.Date.ToString("d");
                DateTime itemDT = DateTime.ParseExact(dt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (itemDT < StartDT || itemDT > EndDT)
                {
                    lst.Remove(item);
                }
            }
            qOrder.Clear();
            qOrder.AddRange(lst);

            List<SearchOrferFood> lstSearchResult = new List<SearchOrferFood>();
            List<Tbl_ResPrintItem> tbl_ResPrintItem = new List<Tbl_ResPrintItem>();
            int myPrintCounter = 1;
            int totalCost = 0;

            var qRestuarant = (from a in db.Tbl_Restaurant
                               where a.ID.Equals(resID)
                               select a).SingleOrDefault();

            foreach (var item2 in qOrder)
            {
                SearchOrferFood sof = new SearchOrferFood();
                Tbl_ResPrintItem oi = new Tbl_ResPrintItem();

                sof.Address = item2.Tbl_Address.Address;
                sof.Date = item2.Date.ToPersianDateString();
                sof.ID = item2.ID;
                sof.Name = userRep.GetSingleUserInfo(item2.UserEmail).name;
                sof.Phone = userRep.GetSingleUserInfo(item2.UserEmail).mob_phone;
                sof.Price = orderRep.TotalfactorPrice(item2.ID).ToString();
                sof.Status = item2.OrderStatus;
                sof.Time = item2.Time.ToString("t");

                totalCost += orderRep.TotalfactorPrice(item2.ID);
                oi.Counter = myPrintCounter;
                oi.Name = userRep.GetSingleUserInfo(item2.UserEmail).name;
                oi.OrderDate = item2.Date.ToPersianDateString();
                oi.OrderPrice = orderRep.TotalfactorPrice(item2.ID).ToString();
                oi.OrderTime = item2.Time.ToString("t");
                if (userRep.GetSingleUserInfo(item2.UserEmail).access.Equals("دانشجو"))
                {
                    oi.SNumber = userRep.GetSingleUserInfo(item2.UserEmail).SNumber;
                }
                else
                {
                    oi.SNumber = userRep.GetSingleUserInfo(item2.UserEmail).mob_phone;
                }

                tbl_ResPrintItem.Add(oi);
                lstSearchResult.Add(sof);
                myPrintCounter++;
            }

            db.Tbl_ResPrintItem.AddRange(tbl_ResPrintItem);
            db.SaveChanges();

            Tbl_ResPrintFactor tbl_ResPrintFactor = new Tbl_ResPrintFactor();

            tbl_ResPrintFactor.EndDate = newEndDate;
            tbl_ResPrintFactor.ResName = qRestuarant.resName;
            tbl_ResPrintFactor.ResUserName = qRestuarant.Tab_users.name + " " + qRestuarant.Tab_users.Family;
            tbl_ResPrintFactor.StartDate = newDate;
            tbl_ResPrintFactor.TotalPrice = totalCost.ToString();

            db.Tbl_ResPrintFactor.Add(tbl_ResPrintFactor);
            db.SaveChanges();


            return Json(lstSearchResult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CounterOrderFilterByDate(string startDate, int resID)
        {
            if (Session["email"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"].ToString() != "مدیر مالی")
                return Json(0, JsonRequestBehavior.AllowGet);

            string year = startDate.Split('/')[0];
            string month = startDate.Split('/')[1];
            string day = startDate.Split('/')[2];

            string FinalYear = utility.PersianToEnglish(year);
            string FinalMonth = utility.PersianToEnglish(month);
            string FinalDay = utility.PersianToEnglish(day);

            string newDate = FinalDay + "/" + FinalMonth + "/" + FinalYear;

            DateTime StartDT = DateTime.ParseExact(newDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var qFoodList = (from a in db.tab_products
                             where a.resID.Equals(resID)
                             select a).ToList();

            var qOrderList = (from a in db.Tbl_OrderFactor
                              where a.ResID.Equals(resID)
                              select a).ToList();


            List<FoodOrderCountManager> lstSearchResult = new List<FoodOrderCountManager>();

            foreach (var item2 in qFoodList)
            {
                FoodOrderCountManager sof = new FoodOrderCountManager();

                sof.foodName = item2.name;
                if (item2.FoodCount.Value == -1)
                {
                    sof.CurrentCount = "نامحدود";
                }
                else
                {
                    sof.CurrentCount = item2.FoodCount.Value.ToString();
                }
                sof.Date = newDate;
                int myFoodCounter = 0;
                foreach (var item in qOrderList)
                {
                    var qItemList = (from a in db.Tbl_OrderFactorItem
                                     where a.FactorID.Equals(item.ID)
                                     select a).ToList();
                    foreach (var item3 in qItemList)
                    {
                        if (item3.FoodID == item2.id)
                        {
                            myFoodCounter += item3.FoodCount;
                        }
                    }
                }
                sof.TotalOrderCount = myFoodCounter;

                int myCounter = 0;
                foreach (var item in qOrderList)
                {
                    if (item.Date.Equals(newDate))
                    {
                        var qItemList = (from a in db.Tbl_OrderFactorItem
                                         where a.FactorID.Equals(item.ID)
                                         select a).ToList();
                        foreach (var item3 in qItemList)
                        {
                            if (item3.FoodID == item2.id)
                            {
                                myFoodCounter += item3.FoodCount;
                            }
                        }
                    }
                }
                sof.orderCount = myCounter;

                lstSearchResult.Add(sof);
            }


            return Json(lstSearchResult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FilterResult()
        {
            if (Session["email"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"].ToString() != "رستوران")
                return RedirectToAction("Index", "Home");

            string email = Session["email"].ToString();

            var q = (from a in db.Tbl_Restaurant
                     where a.userEmail.Equals(email)
                     select a).SingleOrDefault();

            var qOrder = (from a in db.Tbl_OrderFactor
                          where a.ResID.Equals(q.ID)
                          select a).OrderByDescending(i => i.Date).ToList();
            return View(qOrder);
        }
        public JsonResult EditFoodCount(string ID, string value)
        {
            int foodID = Convert.ToInt32(ID);
            int foodCount = Convert.ToInt32(value);

            var q = (from a in db.tab_products
                     where a.id.Equals(foodID)
                     select a).SingleOrDefault();

            q.FoodCount = foodCount;
            db.tab_products.Attach(q);
            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AllOrderFilterByDate(string startDate, string endDate)
        {


            if (Session["email"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"].ToString() != "رستوران")
                return Json(0, JsonRequestBehavior.AllowGet);

            string year = startDate.Split('/')[0];
            string month = startDate.Split('/')[1];
            string day = startDate.Split('/')[2];

            string FinalYear = utility.PersianToEnglish(year);
            string FinalMonth = utility.PersianToEnglish(month);
            string FinalDay = utility.PersianToEnglish(day);

            string newDate = FinalDay + "/" + FinalMonth + "/" + FinalYear;

            string endYear = endDate.Split('/')[0];
            string endMonth = endDate.Split('/')[1];
            string endDayay = endDate.Split('/')[2];

            string FinalEndYear = utility.PersianToEnglish(endYear);
            string FinalEndMonth = utility.PersianToEnglish(endMonth);
            string FinalEndDay = utility.PersianToEnglish(endDayay);

            string newEndDate = FinalEndDay + "/" + FinalEndMonth + "/" + FinalEndYear;

            DateTime StartDT = DateTime.ParseExact(newDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime EndDT = DateTime.ParseExact(newEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string email = Session["email"].ToString();
            var q = (from a in db.Tbl_Restaurant
                     where a.userEmail.Equals(email)
                     select a).SingleOrDefault();

            var qOrder = (from a in db.Tbl_OrderFactor
                          where a.ResID.Equals(q.ID)
                          select a).ToList();

            List<Tbl_OrderFactor> lst = new List<Tbl_OrderFactor>();
            lst.AddRange(qOrder);

            foreach (var item in qOrder)
            {
                string dt = item.Date.ToString("d");
                DateTime itemDT = DateTime.ParseExact(dt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (itemDT < StartDT || itemDT > EndDT)
                {
                    lst.Remove(item);
                }
            }
            qOrder.Clear();
            qOrder.AddRange(lst);

            List<SearchOrferFood> lstSearchResult = new List<SearchOrferFood>();

            foreach (var item2 in qOrder)
            {
                SearchOrferFood sof = new SearchOrferFood();

                sof.Address = item2.Tbl_Address.Address;
                sof.Date = item2.Date.ToPersianDateString();
                sof.ID = item2.ID;
                sof.Name = userRep.GetSingleUserInfo(item2.UserEmail).name;
                sof.Phone = userRep.GetSingleUserInfo(item2.UserEmail).mob_phone;
                sof.Price = orderRep.TotalfactorPrice(item2.ID).ToString();
                sof.Status = item2.OrderStatus;
                sof.Time = item2.Time.ToString("t");

                lstSearchResult.Add(sof);
            }


            return Json(lstSearchResult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SpecialResOrders(int ResID)
        {
            var q = (from a in db.Tbl_OrderFactor
                     where a.ResID.Equals(ResID)
                     select a).ToList();

            List<SearchOrferFood> lstSearchResult = new List<SearchOrferFood>();

            foreach (var item2 in q)
            {
                SearchOrferFood sof = new SearchOrferFood();

                sof.Address = item2.Tbl_Address.Address;
                sof.Date = item2.Date.ToPersianDateString();
                sof.ID = item2.ID;
                sof.Name = userRep.GetSingleUserInfo(item2.UserEmail).name;
                sof.Phone = userRep.GetSingleUserInfo(item2.UserEmail).mob_phone;
                sof.Price = orderRep.TotalfactorPrice(item2.ID).ToString();
                sof.Status = item2.OrderStatus;
                sof.Time = item2.Time.ToString("t");

                lstSearchResult.Add(sof);
            }
            return Json(lstSearchResult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FinancialAdmin_AllFoodOrderCountManager()
        {
            if (Session["email"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"].ToString() != "مدیر مالی")
                return RedirectToAction("Index", "Home");
            return View();
        }
        public ActionResult ResFactorPrint()
        {
            return View();
        }
        public ActionResult report()
        {
            if (Session["email"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"].ToString() != "رستوران")
                return RedirectToAction("Index", "Home");

            string email = Session["email"].ToString();


            var report = new StiReport();
            report.Load(Server.MapPath("/Reports/Report.mrt"));
            report.Compile();
            var qFactor = (from a in db.Tbl_ResPrintFactor
                           select a).SingleOrDefault();

            var qItems = (from a in db.Tbl_ResPrintItem
                          select a).ToList();
            report.RegBusinessObject("OrderListReport", qFactor);
            report.RegBusinessObject("OrderItems", qItems);
            return StiMvcViewer.GetReportSnapshotResult(report);
        }
        public ActionResult viewerEvent()
        {
            return StiMvcViewer.ViewerEventResult(HttpContext);
        }
        public ActionResult FinancialAdminFactorPrint()
        {
            return View();
        }
        public ActionResult FinancialAdminReport()
        {
            if (Session["email"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["access"].ToString() != "مدیر مالی")
                return RedirectToAction("Index", "Home");

            string email = Session["email"].ToString();


            var report = new StiReport();
            report.Load(Server.MapPath("/Reports/Report.mrt"));
            report.Compile();
            var qFactor = (from a in db.Tbl_ResPrintFactor
                           select a).SingleOrDefault();

            var qItems = (from a in db.Tbl_ResPrintItem
                          select a).ToList();
            report.RegBusinessObject("OrderListReport", qFactor);
            report.RegBusinessObject("OrderItems", qItems);
            return StiMvcViewer.GetReportSnapshotResult(report);
        }
        public ActionResult FinancialAdminViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult(HttpContext);
        }
        public JsonResult ResUpdateGetOrderStatus(bool status)
        {
            if (Session["email"] == null)
                return Json("شما وارد نشده اید", JsonRequestBehavior.AllowGet);
            if (Session["access"] == null)
                return Json("برای شما سطح دسترسی تعریف نشده است", JsonRequestBehavior.AllowGet);
            if (Session["access"].ToString() != "رستوران")
                return Json("شما اجازه دسترسی ندارید", JsonRequestBehavior.AllowGet);

            string email = Session["email"].ToString();

            var q = (from a in db.Tbl_Restaurant
                     where a.userEmail.Equals(email)
                     select a).SingleOrDefault();

            q.isGetOrder = status;
            db.Tbl_Restaurant.Attach(q);
            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                if (status)
                {
                    return Json("دریافت سفارش فعال گردید", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("دریافت سفارش غیرفعال شد", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("عملیات درخواستی با خطا مواجه گردید . لطفا به پشتیبانی اطلاع دهید", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CommentConfirm(int ID)
        {
            var q = (from a in db.Tab_comments
                     where a.id.Equals(ID)
                     select a).SingleOrDefault();

            if (q.confirm)
            {
                q.confirm = false;
            }
            else
            {
                q.confirm = true;
            }

            db.Tab_comments.Attach(q);
            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                List<Tab_comments> qAllCommnet = (from a in db.Tab_comments
                                                  where a.product_id.Equals(q.product_id)
                                                  select a).ToList();

                List<FoodComment> lstComment = new List<FoodComment>();

                foreach (var item in qAllCommnet)
                {
                    FoodComment t = new FoodComment();
                    t.id = item.id;
                    t.name = item.name;
                    t.phone = item.phone;
                    t.read = item.read;
                    t.Stars = item.Stars;
                    t.text = item.text;
                    t.data = item.data.ToString("d");
                    t.email = item.email;
                    t.cm_like = item.cm_like;
                    t.cm_dislike = item.cm_dislike;
                    t.confirm = item.confirm;
                    t.product_id = item.product_id;



                    lstComment.Add(t);
                }
                return Json(lstComment, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Tab_comments> qAllCommnet = (from a in db.Tab_comments
                                                  where a.product_id.Equals(q.product_id)
                                                  select a).ToList();

                List<FoodComment> lstComment = new List<FoodComment>();

                foreach (var item in qAllCommnet)
                {
                    FoodComment t = new FoodComment();
                    t.id = item.id;
                    t.name = item.name;
                    t.phone = item.phone;
                    t.read = item.read;
                    t.Stars = item.Stars;
                    t.text = item.text;
                    t.data = item.data.ToString("d");
                    t.email = item.email;
                    t.cm_like = item.cm_like;
                    t.cm_dislike = item.cm_dislike;
                    t.confirm = item.confirm;
                    t.product_id = item.product_id;



                    lstComment.Add(t);
                }
                return Json(lstComment, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditShiftTime(string fss , string ess , string fss2 , string ess2)
        {
            DateTime firstTime = DateTime.ParseExact(fss, "hh:mm tt", CultureInfo.InvariantCulture);
            DateTime secendTime = DateTime.ParseExact(ess, "hh:mm tt", CultureInfo.InvariantCulture);
            DateTime TirthTime = DateTime.ParseExact(fss2, "hh:mm tt", CultureInfo.InvariantCulture);
            DateTime Fourthtime = DateTime.ParseExact(ess2, "hh:mm tt", CultureInfo.InvariantCulture);

            TimeSpan ts_firstTime = firstTime.TimeOfDay;
            TimeSpan ts_secendTime = secendTime.TimeOfDay;
            TimeSpan ts_TirthTime = TirthTime.TimeOfDay;
            TimeSpan ts_Fourthtime = Fourthtime.TimeOfDay;

            string firstShift = ts_firstTime.ToString(@"hh\:mm") + "-" + ts_secendTime.ToString(@"hh\:mm");
            string secendShift = ts_TirthTime.ToString(@"hh\:mm") + "-" + ts_Fourthtime.ToString(@"hh\:mm");

            string email = Session["email"].ToString();

            var q = (from a in db.Tbl_Restaurant
                     where a.userEmail.Equals(email)
                     select a).SingleOrDefault();

            q.FirstTime = firstShift;
            q.SecendTime = secendShift;

            db.Tbl_Restaurant.Attach(q);
            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}