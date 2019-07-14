using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_QLNH.Models
{
    public class LoaiMonAn
    {
        public LoaiMonAn()
        {
            MonAns = new HashSet<MonAn>();
        }

    
    public int ma { get; set; }


    public string ten { get; set; }


    public string mota { get; set; }

    public virtual ICollection<MonAn> MonAns { get; set; }
}
}