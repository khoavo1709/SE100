using System;
using QuanLyTraiHeo.View.Windows.Thiết_lập_cây_mục_tiêu;
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
using QuanLyTraiHeo.Model;
using System.Windows.Media.Animation;

namespace QuanLyTraiHeo
{
    /// <summary>
    /// Interaction logic for ThietLapCayMucTieuWindow.xaml
    /// </summary>
    public partial class ThietLapCayMucTieuWindow : Window
    {
        Caymuctieu cmt;
        Object obj;
        static int check = 0;
        public List<LICHTIEMHEO> Lichtiem { get; set; }
        public List<HEO> Heo { get; set; }
        public List<LICHPHOIGIONG> LichPhoiGiong { get; set; }
        int Year1;
        int Year2;
        public ThietLapCayMucTieuWindow()
        {
            InitializeComponent();
            cmt = new Caymuctieu((int?)OutlinedComboBox.SelectedItem,(int?)OutlinedComboBox1.SelectedItem);            
            cmt.Close();
            obj = cmt.Content;
            cmt.Content = null;
            showmake.Children.Clear();
            showmake.Children.Add(obj as UIElement);
        }
        
        void changeView()
        {
            if ((OutlinedComboBox.SelectedItem != null) && (OutlinedComboBox1.SelectedItem != null))
            {
                Year1 = Convert.ToInt16(OutlinedComboBox.Text);
                Year2 = Convert.ToInt16(OutlinedComboBox1.Text);
                cmt.Caculate(Year1, Year2);
            }
            else
            {
                cmt.Input((int?)OutlinedComboBox.SelectedItem, (int?)OutlinedComboBox1.SelectedItem);
            }
        }
        private void BoLoc(object sender, RoutedEventArgs e)
        {
            changeView();
        }
        private void Muctieu_button(object sender, RoutedEventArgs e)
        {
            DatMucTieu datMucTieu = new DatMucTieu(cmt.Tylede_muctieuClone, cmt.SoHeoConSinhRa_muctieuClone, cmt.ODeItCon_muctieuClone, cmt.SoHeoConSong_MucTieuClone, cmt.SoHeoCaiSua_muctieuClone, cmt.SoConChetTruocKhiCaiSua_MucTieuClone, cmt.ThoiGianMangThai_MucTieuClone, cmt.SoNgayCaiSua_MucTieuClone, cmt.SoNgayKhongLamViec_MucTieuClone, cmt.TrungBnhLua_MucTieuClone, cmt.SoHeoTrongNam_MucTieuClone);
            datMucTieu.ShowDialog();
            cmt = new Caymuctieu((int?)OutlinedComboBox.SelectedItem, (int?)OutlinedComboBox1.SelectedItem);
            cmt.Tylede_muctieuClone = datMucTieu.Tylede_muctieu_au;
            cmt.SoHeoConSinhRa_muctieuClone = datMucTieu.SoHeoConSinhRa_muctieu_au;
            cmt.ODeItCon_muctieuClone = datMucTieu.ODeItCon_muctieu_au;
            cmt.SoHeoConSong_MucTieuClone = datMucTieu.SoHeoConSong_MucTieu_au;
            cmt.SoHeoCaiSua_muctieuClone = datMucTieu.SoHeoCaiSua_muctieu_au;
            cmt.SoConChetTruocKhiCaiSua_MucTieuClone = datMucTieu.SoConChetTruocKhiCaiSua_MucTieu_au;
            cmt.ThoiGianMangThai_MucTieuClone = datMucTieu.ThoiGianMangThai_MucTieu_au;
            cmt.SoNgayCaiSua_MucTieuClone = datMucTieu.SoNgayCaiSua_MucTieu_au;
            cmt.SoNgayKhongLamViec_MucTieuClone = datMucTieu.SoNgayKhongLamViec_MucTieu_au;
            cmt.TrungBnhLua_MucTieuClone = datMucTieu.TrungBnhLua_MucTieu_au;
            cmt.SoHeoTrongNam_MucTieuClone = datMucTieu.SoHeoTrongNam_MucTieu_au;
            
            cmt.Tylede_muctieu_textbox.Content = "Mục tiêu: " + cmt.Tylede_muctieuClone + "%";
            cmt.Soheoconsinhra_lua_muctieu.Content = "Mục tiêu: " + cmt.SoHeoConSinhRa_muctieuClone + " con";
            cmt.Odeitcon_muctieu.Content = "Mục tiêu: " + cmt.ODeItCon_muctieuClone + "%";
            cmt.Soheoconsong_muctieu.Content = "Mục tiêu: " + cmt.SoHeoConSong_MucTieuClone + " con";
            cmt.Soheocaisua_muctieu.Content = "Mục tiêu: " + cmt.SoHeoCaiSua_muctieuClone + " con";
            cmt.Tylechet_muctieu.Content = "Mục tiêu: " + cmt.SoConChetTruocKhiCaiSua_MucTieuClone + "%";
            cmt.Thoigianmangthai_muctieu.Content = "Mục tiêu: " + cmt.ThoiGianMangThai_MucTieuClone + " ngày";
            cmt.Songaycaisua_muctieu.Content = "Mục tiêu: " + cmt.SoNgayCaiSua_MucTieuClone + " ngày";
            cmt.Songaycaisua_muctieu.Content = "Mục tiêu: " + cmt.SoNgayKhongLamViec_MucTieuClone + " ngày";
            cmt.Songaycaisua_muctieu.Content = "Mục tiêu: " + cmt.TrungBnhLua_MucTieuClone + " ngày";
            cmt.Soheotrongnam_muctieu.Content = "Mục tiêu: " + cmt.SoHeoTrongNam_MucTieuClone + " con";
            cmt.clone();
            cmt.Close();
            obj = cmt.Content;
            cmt.Content = null;
            showmake.Children.Clear();
            showmake.Children.Add(obj as UIElement);
            //changeView();
        }
    }
}
