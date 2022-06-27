using BTH_De02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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

namespace BTH_De02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NhanvienDBContext nv = new NhanvienDBContext();
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            CobMaPhong();
            HienThiData();
        }
        private void XoaDL()
        {
            txtManv.Clear();
            txtHoten.Clear();
            txtLuong.Clear();
            txtSoNgay.Clear();
            txtManv.Focus();
        }

        private void CobMaPhong()
        {
            var query = from phong in nv.PhongBans select phong;
            cobMaPhong.ItemsSource = query.ToList();
            cobMaPhong.DisplayMemberPath = "TenPhong";
            cobMaPhong.SelectedValuePath = "MaPhong";
            cobMaPhong.SelectedIndex = 0;
        }

        private void HienThiData1()
        {
            var query = from nv in nv.Nhanviens
                        where nv.Songaycong >=25
                        orderby nv.Songaycong ascending
                        select new
                        {
                            nv.MaPhong,
                            nv.MaNv,
                            nv.Hoten,
                            nv.Songaycong,
                            nv.Luong,
                            nv.Thuong
                        };
            dbNhanVien.ItemsSource = query.ToList();
        }

        private void HienThiData()
        {
            var query = from nv in nv.Nhanviens 
                        //orderby nv.Songaycong ascending 
                        select new
            {
                        nv.MaPhong,
                        nv.MaNv,
                        nv.Hoten,
                        nv.Songaycong,
                        nv.Luong,
                        nv.Thuong
            };
            dbNhanVien.ItemsSource = query.ToList();
        }

        private bool CheckDL()
        {
            string tb="";
            if(txtHoten.Text=="" || txtManv.Text=="" || txtSoNgay.Text =="" || txtLuong.Text == "")
            {
                tb += "\n Bạn cần nhập tất cả các trường dữ liệu";
            }
            int ngay = int.Parse(txtSoNgay.Text);
            if(ngay<= 20 || ngay >= 30 )
            {
                tb += "\n Bạn cần nhập ngày từ 20 đến 30";
            }
            int luong = int.Parse(txtLuong.Text);
            if (luong <= 3000 || luong >= 9000)
            {
                tb += "\n Bạn cần nhập lương từ 3000 đến 9000";
            }
            if (tb != "")
            {
                MessageBox.Show(tb, "Thông báo");
                return false;
            }
        
           return true;

        }
        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            var query = nv.Nhanviens.SingleOrDefault(nv => nv.MaNv.Equals(txtManv.Text));
                if(query != null)
                {
                MessageBox.Show("Mã sản phẩm này đã tồn tại, không thể thêm. ");
                HienThiData();
                }
            else
            {
                try
                {
                    if (CheckDL())
                    {
                        Nhanvien nvMoi = new Nhanvien();
                        nvMoi.MaNv = int.Parse(txtManv.Text);
                        nvMoi.Hoten = txtHoten.Text;
                        nvMoi.Songaycong = int.Parse(txtSoNgay.Text);
                        nvMoi.Luong = int.Parse(txtLuong.Text);
                        PhongBan item = (PhongBan)cobMaPhong.SelectedItem;
                        nvMoi.MaPhong = item.MaPhong;
                        nv.Nhanviens.Add(nvMoi);
                        nv.SaveChanges();
                        MessageBox.Show("Thêm thành công", "Thông báo");
                        HienThiData();
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show("Có lỗi xảy ra khi thêm: " + e1.Message, "Thông báo");
                    nv.ChangeTracker.Clear();
                }
            }        
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var nvXoa = nv.Nhanviens.SingleOrDefault(n => n.MaNv == int.Parse(txtManv.Text));
                if(nvXoa != null)
                {
                    MessageBoxResult rs = MessageBox.Show("bạn có chắc chắc xoá không? ", "Thống báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if( rs == MessageBoxResult.Yes)
                    {
                        nv.Nhanviens.Remove(nvXoa);
                        nv.SaveChanges();
                        HienThiData();
                    }    
                }
                else
                {
                    MessageBox.Show("Bạn cần chọn 1 hàng để xoá ", "Thông báo");
                }    
            }
            catch (Exception e1)
            {
                MessageBox.Show("Bạn cần chọn 1 hàng để xoá");
                
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var nvSua = nv.Nhanviens.SingleOrDefault(t => t.MaNv == int.Parse(txtManv.Text));
                if(nvSua!=null)
                {
                    if(CheckDL())
                    {
                        // Không cho sửa mã nhân viên
                        
                        nvSua.Hoten = txtHoten.Text;
                        nvSua.Luong = int.Parse(txtLuong.Text);
                        nvSua.Songaycong = int.Parse(txtSoNgay.Text);

                        PhongBan item = (PhongBan)cobMaPhong.SelectedItem;
                        nvSua.MaPhong = item.MaPhong;

                        nv.SaveChanges();
                        MessageBox.Show("Sưa thành công", "TB");
                        HienThiData();
                    }    
                }    
            }
            catch (Exception e1)
            {
                MessageBox.Show("Bạn cần chọn 1 hàng để sửa");
            }
        }

        // Hiện thi tt khi chọn 1 dòng trong dataGrid
        private void dbNhanVien_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                object obj = dbNhanVien.SelectedItem;
                if (obj != null)
                { 
                    Type t = obj.GetType();
                    PropertyInfo[] p = t.GetProperties();
                    cobMaPhong.SelectedValue = p[0].GetValue(obj).ToString();
                    txtManv.Text = p[1].GetValue(obj).ToString();
                    txtHoten.Text = p[2].GetValue(obj).ToString();
                    txtSoNgay.Text = p[3].GetValue(obj).ToString();
                    txtLuong.Text = p[4].GetValue(obj).ToString();

                }
            }
            catch (Exception e1)
            {
                MessageBox.Show("Bạn phải chọn 1 hàng trong bảng");
               
            }
        }

        private void btnTim_Click(object sender, RoutedEventArgs e)
        {
            Window2 w = new Window2();
            w.ShowDialog();
        }
    }
}
