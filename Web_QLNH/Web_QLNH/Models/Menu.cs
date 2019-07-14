using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_QLNH.Models
{
    public class Menu
    {
        public int maMenu { get; set; }

        public DateTime? thoigian { get; set; }

        public int? nguoinhap { get; set; }

        public virtual DangNhap DangNhap { get; set; }
    }
}