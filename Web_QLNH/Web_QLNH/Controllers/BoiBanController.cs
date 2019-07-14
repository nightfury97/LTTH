using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Web_QLNH.Models;

namespace Web_QLNH.Controllers
{
    public class BoiBanController : Controller
    {
        // GET: BoiBan
        public ActionResult Index()
        {
            List<Table> bans = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Common.URL.Baseurl);
                //HTTP GET
                var responseTask = client.GetAsync("api/Table");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Table>>();
                    readTask.Wait();
                    bans = readTask.Result;
                }
                else
                {

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(bans);
        }

        public ActionResult DsMonAn(int ma)
        {
            List<MonAn> bans = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Common.URL.Baseurl);
                //HTTP GET
                var responseTask = client.PostAsync("danhsachmon?ma=" + ma, null);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<MonAn>>();
                    readTask.Wait();
                    bans = readTask.Result;
                }
                else
                {

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }           
            return View(bans);
        }

        public JsonResult reservation(int soban)
        {
            
            string key = "0f8fad5b-d9cb-469f-a165-70867728950e";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Common.URL.Baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",key);
                //HTTP GET
                var responseTask = client.GetAsync("api/Table?idTable=" + soban);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return Json(new { status = true });
                }
                else
                {
                   
                    return Json(new { status = false });
                }
            }

        }

        public ActionResult GoiMon(int soban, int mamonan, int soluong)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Common.URL.Baseurl);
                //HTTP GET
                var responseTask = client.PostAsync("themmon?ma="+mamonan+"&soban="+soban+"&soluong="+soluong+"&yeucau=làm nhanh", null);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.mes = "thành công";
                }
                else
                {
                    ViewBag.mes = "thất bại";                }
            }
            return View();
        }
    }
}