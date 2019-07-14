using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_QLNH.Models
{
    public class DatBan
    {
        public int ma { get; set; }

        public int? soban { get; set; }

        public DateTime? thoigian { get; set; }

        public int? soluong { get; set; }

        public string tennguoidat { get; set; }

        public virtual Ban Ban { get; set; }
    }
}