using BTHDe1_Ma02OnThayHoang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BTHDe1_Ma02OnThayHoang
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        QuanLyBenhNhanDBContext db = new QuanLyBenhNhanDBContext();
        public Window1()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            HienDL();
        }
        private void HienDL()
        {
            var query = from bn in db.BenhNhans
                        join k in db.Khoas
                        on bn.MaKhoa equals k.MaKhoa
                        where k.MaKhoa == 1
                        select new
                        {
                            bn.MaBn,
                            bn.HoTen,
                            bn.SoNgayNamVien,
                            k.TenKhoa,
                            bn.VienPhi
                        };
            dataBN2.ItemsSource = query.ToList();
        }
    }
}
