using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_QLNH.Models
{
    public class DonHang
    {
        public int ma { get; set; }

        public int? ban { get; set; }

        public DateTime? batdau { get; set; }

        public DateTime? ketthuc { get; set; }

        public int? tinhtrang { get; set; }

        public double? tong { get; set; }

        public double? giamgia { get; set; }

        public double? tinhtien { get; set; }

        public virtual Ban Ban1 { get; set; }
    }
}