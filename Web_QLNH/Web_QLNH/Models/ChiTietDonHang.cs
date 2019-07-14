using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_QLNH.Models
{
    public class ChiTietDonHang
    {
        public int maDonHang { get; set; }

        public int monan { get; set; }

        public string ghichu { get; set; }

        public int? soluong { get; set; }

        public int? tinhtrang { get; set; }

        public virtual Ban Ban { get; set; }

        public virtual MonAn MonAn1 { get; set; }
    }
}