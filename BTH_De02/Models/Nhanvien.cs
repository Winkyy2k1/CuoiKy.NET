using System;
using System.Collections.Generic;

#nullable disable

namespace BTH_De02.Models
{
    public partial class Nhanvien
    {
        public int MaNv { get; set; }
        public string Hoten { get; set; }
        public int? Songaycong { get; set; }
        public int? Luong { get; set; }
        public float? Thuong { get
            {
                if (Songaycong >= 27) return (float?) (Luong * 0.1);
                else return 0;
            }
               
            }
        

        public int? MaPhong { get; set; }

        public virtual PhongBan MaPhongNavigation { get; set; }
    }
}
