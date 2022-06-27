using BTHDe1_Ma02OnThayHoang;
using BTHDe1_Ma02OnThayHoang.Models;
using System;
using System.Collections.Generic;
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

namespace BTH_De02OnThayHoang
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        QuanLyBenhNhanDBContext db = new QuanLyBenhNhanDBContext(); 
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //HienDL(); // Hiện thi tất cả dữ liẹu có trong database
            LoadDL();
            HienCob();
        }

        private void XoaDL()
        {
            txtMabn.Clear();
            txtHoten.Clear();
            txtSoNgay.Clear();
            txtMabn.Focus();

        }
        private void HienDL()
        {
            var query = from bn in db.BenhNhans
                        join k in db.Khoas
                        on bn.MaKhoa equals k.MaKhoa
                        select new
                        {
                            bn.MaBn,
                            bn.HoTen,
                            bn.SoNgayNamVien,
                            k.TenKhoa,
                            bn.VienPhi
                        };
            dataBenhNhan.ItemsSource = query.ToList();
        }
        private void LoadDL() // load dữ liệu theo yeu cầu đề bài
        {
            var query = from bn in db.BenhNhans
                        join k in db.Khoas
                        on bn.MaKhoa equals k.MaKhoa
                        orderby bn.SoNgayNamVien descending
                        select new
                        {
                            bn.MaBn,
                            bn.HoTen,
                            bn.SoNgayNamVien,
                            k.TenKhoa,
                            bn.VienPhi
                        };
            dataBenhNhan.ItemsSource = query.ToList();
        }
        private void HienCob()
        {
            var query = from k in db.Khoas
                        select k;
            cobKhoaKham.ItemsSource = query.ToList();
            cobKhoaKham.DisplayMemberPath = "TenKhoa";
            cobKhoaKham.SelectedValuePath = "MaKhoa";
            cobKhoaKham.SelectedIndex = 0;
        }
        // chọn 1 dòng trong datagrid - đổ dữ liẹu lên ô text box
        private void dataBenhNhan_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            object obj = dataBenhNhan.SelectedItem;
            if(obj != null) // nếu đã chọn 1 dòng
            {
                try
                {
                    Type t = obj.GetType();
                    PropertyInfo[] p = t.GetProperties();
                    txtMabn.Text = p[0].GetValue(obj).ToString();
                    txtHoten.Text = p[1].GetValue(obj).ToString();
                    txtSoNgay.Text = p[2].GetValue(obj).ToString();
                    cobKhoaKham.Text = p[3].GetValue(obj).ToString();
                }
                catch (Exception e1)
                {
                    MessageBox.Show("Bạn chưa chọn dòng");
                }
            }    
        }

        private bool CheckDL()
        {
            string tb = "";
            if(txtMabn.Text=="" || txtHoten.Text == "" || txtSoNgay.Text == "")
            {
                tb += "\n Bạn cần nhập đủ các trường dữ liệu";
            }    

            if(!Regex.IsMatch(txtMabn.Text, @"\d+"))
            {
                tb += "\n Mã bệnh nhân phải là số";
            }

            if (!Regex.IsMatch(txtSoNgay.Text, @"\d+"))
            {
                tb += "\n Số ngày nằm viện phải là số";
            }
            else
            {
                int so = int.Parse(txtSoNgay.Text);
                if(so < 1)
                {
                    tb += "\n Số ngày nằm viện phải lớn hơn 1";
                }    
            }    
            if(tb!="")
            {
                MessageBox.Show(tb, "Thông báo");
                return false;
            }    
            else
            {
                MessageBox.Show("Kiểm tra dữ liệu đã hợp lệ", "Thông báo");
                return true;
            }    
           
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            if (txtMabn.Text == "")
            {
                MessageBox.Show("Bạn cần nhập mã bệnh nhân trước", "Thông báo");
            }
            else
            {
                var maMoi = db.BenhNhans.SingleOrDefault(t => t.MaBn == int.Parse(txtMabn.Text));
                if (maMoi != null)
                {
                    MessageBox.Show("Mã bệnh nhân này đã tồn tại, không thể thêm");
                }
                else
                {
                    try
                    {
                        if (CheckDL())
                        {
                            BenhNhan bn = new BenhNhan();
                            bn.MaBn = int.Parse(txtMabn.Text);
                            bn.HoTen = txtHoten.Text;
                            bn.SoNgayNamVien = int.Parse(txtSoNgay.Text);
                            Khoa k = (Khoa)cobKhoaKham.SelectedItem;
                            bn.MaKhoa = k.MaKhoa;

                            // thêm dữ liệu vào database
                            db.BenhNhans.Add(bn);
                            db.SaveChanges();
                            HienDL();
                            XoaDL();
                        }
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show("Có lỗi khi thêm " + e1.Message);
                        //throw;
                    }
                }
            } 
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {   if (txtMabn.Text == "" )
            {
                MessageBox.Show("bạn cần chọn bệnh nhân cần xoá trước");
            }
            else
            {
                var maXoa = db.BenhNhans.SingleOrDefault(t => t.MaBn == int.Parse(txtMabn.Text));
                if (maXoa != null)
                {
                    MessageBoxResult rs = MessageBox.Show("Chắc chắn xoá không?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (rs == MessageBoxResult.Yes)
                    {
                        // Xoá bệnh nhân
                        db.BenhNhans.Remove(maXoa);
                        db.SaveChanges();
                        HienDL();
                        XoaDL();
                    }
                }   
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (txtMabn.Text == "")
            {
                MessageBox.Show("bạn cần chọn bệnh nhân cần sửa trước");
               
            }
            else
            {
                var maSua = db.BenhNhans.SingleOrDefault(t => t.MaBn == int.Parse(txtMabn.Text));
                if (maSua != null)
                {
                    try
                    {
                        if(CheckDL())
                        {
                            // Sửa thông tin, không cho sửa mã bệnh nhân
                            
                            maSua.HoTen = txtHoten.Text;
                            maSua.SoNgayNamVien = int.Parse(txtSoNgay.Text);
                            Khoa tam = (Khoa)cobKhoaKham.SelectedItem;
                            maSua.MaKhoa = tam.MaKhoa;

                            // Lưu lại vào database
                            db.SaveChanges();
                            HienDL();
                            XoaDL();
                        }    
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show("Có lỗi khi sửa thông tin bệnh nhân. ");
                      
                    }
                }
            }
        }

        private void btnTim_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tìm thành công");

            Window1 w = new Window1();
            w.Show();
        }

    }
}
