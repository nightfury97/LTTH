using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_QLNH.Models
{
    public class ChiTietMenu
    {

        public int maMenu { get; set; }

        public int monan { get; set; }

        public int? bandau { get; set; }

        public int? conlai { get; set; }

        public int? daban { get; set; }

        public virtual MonAn MonAn1 { get; set; }
    }
}