using System;
using System.Collections.Generic;

#nullable disable

namespace BTHDe1_Ma02OnThayHoang.Models
{
    public partial class Khoa
    {
        public Khoa()
        {
            BenhNhans = new HashSet<BenhNhan>();
        }

        public int MaKhoa { get; set; }
        public string TenKhoa { get; set; }

        public virtual ICollection<BenhNhan> BenhNhans { get; set; }
    }
}
