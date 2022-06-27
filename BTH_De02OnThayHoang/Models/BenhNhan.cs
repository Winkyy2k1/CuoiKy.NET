using System;
using System.Collections.Generic;

#nullable disable

namespace BTHDe1_Ma02OnThayHoang.Models
{
    public partial class BenhNhan
    {
        public int MaBn { get; set; }
        public string HoTen { get; set; }
        public int? SoNgayNamVien { get; set; }
        public double? VienPhi { get
            {
                return SoNgayNamVien * 2000;
            } }
        public int? MaKhoa { get; set; }

        public virtual Khoa MaKhoaNavigation { get; set; }
    }
}
