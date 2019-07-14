using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace QLNhaHangApi.Controllers
{
    public class HomeController : Controller
    {
        

        public ActionResult Index()
        {

            ViewBag.Title = "Home Page";

            return View();
        }

        public static void load()
        {
            string serverIP = "192.168.43.180";
            int port = 8535;
            try { 
            TcpClient client = new TcpClient(serverIP, port);
            string mes = "Load";

            int bytecount = Encoding.ASCII.GetByteCount(mes.ToCharArray());

            byte[] sendData = new byte[bytecount];

            sendData = Encoding.ASCII.GetBytes(mes.ToCharArray());

            NetworkStream stream = client.GetStream();

            stream.Write(sendData, 0, sendData.Length);

            stream.Close();
            client.Close();
            }
            catch
            { }
        }
    }
}
