using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_QLNH.Models
{
    public class MonAn
    {
        public MonAn()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            ChiTietMenus = new HashSet<ChiTietMenu>();
        }

     
        public int ma { get; set; }

        public int? maloai { get; set; }

        public string ten { get; set; }

        public string mota { get; set; }

        public string hinhanh { get; set; }

        public double? gia { get; set; }

        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual ICollection<ChiTietMenu> ChiTietMenus { get; set; }

        public virtual LoaiMonAn LoaiMonAn { get; set; }
    }
}