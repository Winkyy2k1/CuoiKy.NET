using BTHDe02_De02ThayHoang.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BTHDe02_De02ThayHoang
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        QuanLySanPhamDBContext db= new QuanLySanPhamDBContext();
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            HienDL();
            HienCob();
        }

        private void HienCob()
        {
            var query = from nh in db.NhomHangs select nh;
            cobNhomhang.ItemsSource = query.ToList();
            cobNhomhang.DisplayMemberPath = "TenNhomHang";
            cobNhomhang.SelectedValuePath = "MaNhomHang";
            cobNhomhang.SelectedIndex = 0;
        }
        private void HienDL()
        {
            var query = from nh in db.NhomHangs
                        join sp in db.SanPhams
                        on nh.MaNhomHang equals sp.MaNhomHang
                        orderby sp.SoLuongBan descending
                        select new
                        {
                            sp.MaSp,
                            sp.TenSanPham,
                            sp.DonGia,
                            sp.SoLuongBan,
                            nh.TenNhomHang,
                            TienBan =Convert.ToDouble(sp.DonGia * sp.SoLuongBan).ToString("N0"),
                            //TienBan =Convert.ToDouble(sp.DonGia * sp.SoLuongBan).ToString("N0", new CultureInfo("is-US")) ,
                        };
            dtSanPham.ItemsSource = query.ToList();
        }

        private bool CheckDL()
        {
            string tb = "";
            if(txtMasp.Text=="" || txtTensp.Text=="" || txtDonGia.Text =="" || txtSoluongBan.Text =="")
            {
                tb += "Bạn cần nhập đủ các trường dữ liệu";
            }    
            if(!Regex.IsMatch(txtMasp.Text,@"\d+"))
            {
                tb += "\n Bạn càn nhận mã sản phẩm là số nguyên.";
            }
            if (!Regex.IsMatch(txtDonGia.Text, @"\d+"))
            {
                tb += "\n Bạn càn nhận đơn giá là số.";
            }
            if (!Regex.IsMatch(txtSoluongBan.Text, @"\d+"))
            {
                tb += "\n Bạn càn nhận số lượng bán là số.";
            }
            else
            {
                double so = double.Parse(txtSoluongBan.Text);
                if(so < 1 )
                {
                    tb += "\n bạn cần nhập số lượng bán >= 1";
                }    
            }    

            if(tb != "")
            {
                MessageBox.Show(tb, "Thông báo");
                return false;
            }    
            return true;
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra không cho trùng trong CSDL. 
            if (txtMasp.Text == "")
            {
                MessageBox.Show("Bạn cần nhập mã sản phẩm trước", "Thông báo");
            }
            else
            {   // Kiểm tra không trùng mã sản phẩm trong database
                var spMoi = db.SanPhams.SingleOrDefault(t => t.MaSp == int.Parse(txtMasp.Text));
                if (spMoi != null)
                {
                    MessageBox.Show("Mã sản phẩm này đã tồn tại, không thể thêm", "Thông báo");
                }
                try
                {
                    if ( CheckDL() )
                    {
                        SanPham sp = new SanPham();
                        sp.MaSp = int.Parse(txtMasp.Text);
                        sp.TenSanPham = txtTensp.Text;
                        sp.DonGia = double.Parse(txtDonGia.Text);
                        sp.SoLuongBan = double.Parse(txtSoluongBan.Text);
                        NhomHang a = (NhomHang ) cobNhomhang.SelectedItem;
                        sp.MaNhomHang = a.MaNhomHang;
                        // Thêm sp vào 
                        db.SanPhams.Add(sp);
                        db.SaveChanges();
                        MessageBox.Show("Thêm thành công", "Thông báo");
                        HienDL();
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show("Thêm không thành công" + e1.Message, "Thông báo");
                }
            } 
        }

        private void dtSanPham_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            object obj = dtSanPham.SelectedItem;
            if(obj != null)
            {
                try
                {
                    Type t = obj.GetType();
                    PropertyInfo[] p = t.GetProperties();
                    txtMasp.Text = p[0].GetValue(obj).ToString();
                    txtTensp.Text = p[1].GetValue(obj).ToString();
                    txtDonGia.Text = p[2].GetValue(obj).ToString();
                    txtSoluongBan.Text = p[3].GetValue(obj).ToString();
                    cobNhomhang.Text = p[4].GetValue(obj).ToString();
                }
                catch (Exception e1)
                {
                    MessageBox.Show("Có lỗi khi chọn hàng", "Thông báo");
                    //throw;
                }
            }    
        }

        private void btnTim_Click(object sender, RoutedEventArgs e)
        {
            Window2 w = new Window2();
            w.ShowDialog();
        }
    }
}
