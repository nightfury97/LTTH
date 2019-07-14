using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_QLNH.Models
{
    public class BoPhan
    {
        public int ma { get; set; }

        public string ten { get; set; }

        public string mota { get; set; }

        public virtual ICollection<DangNhap> DangNhaps { get; set; }
    }
}