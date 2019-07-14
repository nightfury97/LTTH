using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_QLNH.Models
{
    public class DangNhap
    {
        public DangNhap()
        {
            Menus = new HashSet<Menu>();
        }
        public int manhanvien { get; set; }


        public string tendangnhap { get; set; }


        public string matkhau { get; set; }


        public string hoten { get; set; }

        public int? bophan { get; set; }

        public virtual BoPhan BoPhan1 { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
    }
}