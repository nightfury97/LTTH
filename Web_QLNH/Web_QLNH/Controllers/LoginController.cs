using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Web_QLNH.Common;
using Web_QLNH.Models;

namespace Web_QLNH.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult login(string username, string password)
        {
            DangNhap user = new DangNhap();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Common.URL.Baseurl);
             
                var postTask = client.PostAsync("login?username="+username+"&password="+password,null);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<DangNhap>();
                    readTask.Wait();

                    user = readTask.Result;
                    Session.Add(CommonConstant.USER_SESSION, user);
                    return RedirectToAction("Index", "BoiBan");
                }
                else
                {
                    ViewBag.noti = "Đăng nhập thất bại";
                    return View("Index");
                }
            }
          
        }
    }
}