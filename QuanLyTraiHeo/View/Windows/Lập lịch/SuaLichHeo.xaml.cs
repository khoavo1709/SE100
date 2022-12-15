using QuanLyTraiHeo.Model;
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

namespace QuanLyTraiHeo.View.Windows.Lập_lịch
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SuaLichHeo : Window
    {
        string temp { get; set; }
        static int check = 0;
        public SuaLichHeo(LICHTIEMHEO tiem)
        {
            InitializeComponent();
            temp = tiem.MaLichTiem;
            textcode.Text = tiem.MaHeo;
            Drugcode_text.Text = tiem.MaThuoc;
            Datepicker_Ngaytiem.SelectedDate = tiem.NgayTiem;
            Lieuluong_text.Text = tiem.LieuLuong.ToString();
            Trangthai_combobox.Text = tiem.TrangThai;
        }

        private void Confirm_button_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
            this.Close();
        }

        void ShowListHeo()
        {
            DanhsachHeo ds = new DanhsachHeo();
            ds.ShowDialog();
            textcode.Text = ds.TranferCode();
        }

        private void ListHeo_button_Click(object sender, RoutedEventArgs e)
        {
            ShowListHeo();
        }

        void ShowListThuoc()
        {
            DanhSachThuoc ds = new DanhSachThuoc();
            ds.ShowDialog();
            Drugcode_text.Text = ds.TranferCode();
        }

        private void ListThuoc_button_Click(object sender, RoutedEventArgs e)
        {
            ShowListThuoc();
        }
        
        public LICHTIEMHEO returnValue()
        {
            if (check == 1)
            {
                LICHTIEMHEO tiem = new LICHTIEMHEO();
                tiem.MaLichTiem = temp;
                tiem.MaHeo = textcode.Text;
                tiem.MaThuoc = Drugcode_text.Text;
                tiem.NgayTiem = Datepicker_Ngaytiem.SelectedDate.Value;
                tiem.LieuLuong = int.Parse(Lieuluong_text.Text);
                tiem.TrangThai = Trangthai_combobox.Text;
                return tiem;
            }
            return null;
        }

        private void Huy_button_Click(object sender, RoutedEventArgs e)
        {
            check = 0;
            this.Close();
        }
    }
    
}
