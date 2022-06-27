using BTHDe02_De02ThayHoang.Models;
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

namespace BTHDe02_De02ThayHoang
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        QuanLySanPhamDBContext db = new QuanLySanPhamDBContext();
        public Window2()
        {
            InitializeComponent();
            HienDL();
        }
        private void HienDL()
        {
            var query = from nh in db.NhomHangs
                        join sp in db.SanPhams
                        on nh.MaNhomHang equals sp.MaNhomHang
                        where sp.MaNhomHang == 1
                        select new
                        {
                            sp.MaSp,
                            sp.TenSanPham,
                            sp.DonGia,
                            sp.SoLuongBan,
                            nh.TenNhomHang,
                            TienBan = sp.DonGia * sp.SoLuongBan
                        };
            dtSanPham2.ItemsSource = query.ToList();
        }
    }
}
