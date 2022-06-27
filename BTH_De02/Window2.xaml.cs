using BTH_De02.Models;
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

namespace BTH_De02
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        NhanvienDBContext db = new NhanvienDBContext();
        public Window2()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            TimKiem();
        }

        private void TimKiem()
        {
            var query = from pb in db.PhongBans
                        join nv in db.Nhanviens
                        on pb.MaPhong equals nv.MaPhong
                        group nv.MaNv by pb.MaPhong into gr
                        select new
                        {
                            MaPhong = gr.Key,
                            SoLuong = gr.Count(),
                        };

            var query2 = from pb in db.PhongBans
                        join nv in db.Nhanviens
                        on pb.MaPhong equals nv.MaPhong
                        group nv.Luong by pb.MaPhong into gr
                        select new
                        {
                            MaPhong = gr.Key,
                            TongLuong = gr.Sum(),
                        };

            var query4 = from e1 in query
                         join e2 in query2
                         on e1.MaPhong equals e2.MaPhong
                         select new
                         {   e1.MaPhong,
                             e1.SoLuong,
                             e2.TongLuong
                         };

            var query3 = from pb2 in db.PhongBans
                         join e4 in query4 
                         on pb2.MaPhong equals e4.MaPhong 
                         select new
                         {
                             pb2.MaPhong,
                             pb2.TenPhong,
                             e4.SoLuong,
                             e4.TongLuong,
                         };
            dbPhongBan.ItemsSource = query3.ToList();
        }
    }
}
