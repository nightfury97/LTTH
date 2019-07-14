using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_QLNH.Models
{
    public class Ban
    {
        public Ban()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            DatBans = new HashSet<DatBan>();
            DonHangs = new HashSet<DonHang>();
        }
        public int so { get; set; }

        public string vitri { get; set; }

        public int? succhua { get; set; }

        public int? tinhtrang { get; set; }

        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual ICollection<DatBan> DatBans { get; set; }

        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}