using BTH_DeOnHinhAnh.Models;
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

namespace BTH_DeOnHinhAnh
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        QLBHContext db = new QLBHContext();

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            LoadDL();
            LoadCob();
        }
        private void LoadDL()
        {
            var query = from sp in db.SanPhams
                        join nh in db.NhomHangs
                        on sp.MaNhomHang equals nh.MaNhomHang
                        orderby sp.SoLuongBan descending
                        select new
                        {
                            sp.MaSp,
                            sp.TenSanPham,
                            sp.DonGia,
                            sp.SoLuongBan,
                            nh.TenNhomHang,
                            TienBan = Convert.ToDouble(sp.DonGia * sp.SoLuongBan).ToString("N0",new CultureInfo("is-US")),
                        };
            dbSanpham.ItemsSource = query.ToList();
        }
        private void LoadCob()
        {
            var cob = from nh in db.NhomHangs
                      select nh;
            cobNhomHang.ItemsSource = cob.ToList();
            cobNhomHang.DisplayMemberPath = "TenNhomHang";
            cobNhomHang.SelectedValuePath = "MaNhomHang";
            cobNhomHang.SelectedIndex = 0;
        }
        private void XoaDL()
        {
            txtMasp.Clear();
            txtDonGia.Clear();
            txtSoluongban.Clear();
            txtTensp.Clear();
            txtMasp.Focus();
            txtMasp.IsEnabled = true;
        }
        private bool CheckDL()
        {   
            string tb = "";
            if(txtMasp.Text=="" || txtTensp.Text=="" || txtDonGia.Text=="" || txtSoluongban.Text=="")
            {
                tb += "Bạn cần nhập đủ các trường dữ liệu";
            }    

            if(!Regex.IsMatch(txtSoluongban.Text,@"\d+"))
            {
                tb += "\n Bạn cần nhập sô lượng bán là số";
            }   
            else
            {
                int item = int.Parse(txtSoluongban.Text);
                if(item <1)
                {
                    tb += "\n Bạn cần nhập số lượng lớn hơn 1";
                }    
            }
            if (!Regex.IsMatch(txtDonGia.Text, @"\d+"))
            {
                tb += "\n Bạn cần nhập đơn giá là số";
            }
            if(tb!="")
            {
                MessageBox.Show(tb, "Thông báo");
                return false;
            }
            else
            {
                MessageBox.Show("Dữ liệu hợp lệ", "Thông báo");
                return true;
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            if(txtMasp.Text=="")
            {
                MessageBox.Show("Bạn cần nhập mã sản phẩm trước", "Thông báo");
            }
            else
            {
                var checkMa = db.SanPhams.SingleOrDefault(t => t.MaSp == int.Parse(txtMasp.Text));
                if(checkMa != null)
                {
                    MessageBox.Show("Sản phẩm này đã tồn tại, không thể thêm", "Thông báo");
                }    
                try
                {
                    if(CheckDL())
                    {
                        SanPham sp = new SanPham();
                        sp.MaSp = int.Parse(txtMasp.Text);
                        sp.TenSanPham = txtTensp.Text;
                        sp.DonGia = int.Parse(txtDonGia.Text);
                        sp.SoLuongBan = int.Parse(txtSoluongban.Text);
                        NhomHang item = (NhomHang)cobNhomHang.SelectedItem;
                        sp.MaNhomHang = item.MaNhomHang;

                        // thêm vào bảng
                        db.SanPhams.Add(sp);
                        db.SaveChanges();
                        MessageBox.Show("Thêm thành công", "Thông báo");
                        LoadDL();
                        XoaDL();
                    }    
                     
                }
                catch (Exception e1)
                {

                    MessageBox.Show("Có lỗi xảy ra khi thêm 1 sản phẩm" + e1.Message, "Thông báo");
                }
            }
        }
        // đổ dữ liẹu lên khi chọn 1 dòng
        private void dbSanpham_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            object obj = dbSanpham.SelectedItem;
            if(obj!= null)
            {
                try
                {
                    Type t = obj.GetType();

                    PropertyInfo[] p = t.GetProperties();

                    txtMasp.Text = p[0].GetValue(obj).ToString();
                    txtTensp.Text = p[1].GetValue(obj).ToString();
                    txtDonGia.Text = p[2].GetValue(obj).ToString();
                    txtSoluongban.Text = p[3].GetValue(obj).ToString();
                    cobNhomHang.Text= p[4].GetValue(obj).ToString();
                    
                }
                catch (Exception e1)
                {
                    MessageBox.Show("Có lỗi xảy ra khi chọn 1 hàng" + e1.Message, "Thông báo");
                    //throw;
                }
            }   
            
        }

        private void btnTim_Click(object sender, RoutedEventArgs e)
        {
            Window2 w = new Window2();
            w.ShowDialog();
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            txtMasp.IsEnabled = false;
            if (txtMasp.Text == "")
            {
                MessageBox.Show("Bạn cần nhập chọn sản phẩm cần sửa trước", "Thông báo");
            }
            else
            {
                var maSua = db.SanPhams.SingleOrDefault(t => t.MaSp == int.Parse(txtMasp.Text));
                if(maSua!=null)
                {
                    try
                    {
                        if(CheckDL())
                        {
                            maSua.TenSanPham = txtTensp.Text;
                            maSua.DonGia = int.Parse(txtDonGia.Text);
                            maSua.SoLuongBan = int.Parse(txtSoluongban.Text);
                            NhomHang nh = (NhomHang)cobNhomHang.SelectedItem;
                            maSua.MaNhomHang = nh.MaNhomHang;

                            db.SaveChanges();
                            LoadDL();
                            XoaDL();
                        }    
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show("Có lỗi xảy ra khi sửa sản phẩm" + e1.Message, "Thông báo");
                        //throw;
                    }
                }    

            }
        }
    }
}
