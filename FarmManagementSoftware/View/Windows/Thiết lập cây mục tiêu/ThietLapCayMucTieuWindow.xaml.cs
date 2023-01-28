using System;
using FarmManagementSoftware.View.Windows.Thiết_lập_cây_mục_tiêu;
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
using FarmManagementSoftware.Model;
using System.Windows.Media.Animation;
using FarmManagementSoftware.View.Windows;

namespace FarmManagementSoftware
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
            cmt = new Caymuctieu((int?)OutlinedComboBox.SelectedItem, (int?)OutlinedComboBox1.SelectedItem);
            cmt.Close();
            obj = cmt.Content;
            cmt.Content = null;
            showmake.Children.Clear();
            showmake.Children.Add(obj as UIElement);
            zoomIn.Click += cmt.zoom_in_Click;
            zoomOut.Click += cmt.zoom_out_Click;
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
            ThamSo thamso = new ThamSo();
            DatMucTieu datMucTieu = new DatMucTieu(cmt.Tylede_muctieu, cmt.SoHeoConSinhRa_muctieu, cmt.ODeItCon_muctieu, cmt.SoHeoConSong_MucTieu, cmt.SoHeoCaiSua_muctieu, cmt.SoConChetTruocKhiCaiSua_MucTieu, cmt.ThoiGianMangThai_MucTieu, cmt.SoNgayCaiSua_MucTieu, cmt.SoNgayKhongLamViec_MucTieu, cmt.TrungBnhLua_MucTieu, cmt.SoHeoTrongNam_MucTieu, cmt.TyLeThayDan_MucTieu);
            datMucTieu.ShowDialog();
            cmt.Window_Loaded();
            changeView();
            if (datMucTieu.ReturnValue(thamso) != null)
            {
                /*cmt.Tylede_muctieu = thamso.Tylede_muctieu;
                cmt.SoHeoConSinhRa_muctieu = thamso.SoHeoConSinhRa_muctieu;
                cmt.ODeItCon_muctieu = thamso.ODeItCon_muctieu;
                cmt.SoHeoConSong_MucTieu = thamso.SoHeoConSong_MucTieu;
                cmt.SoHeoCaiSua_muctieu = thamso.SoHeoCaiSua_muctieu;
                cmt.SoConChetTruocKhiCaiSua_MucTieu = thamso.SoConChetTruocKhiCaiSua_MucTieu;
                cmt.ThoiGianMangThai_MucTieu = thamso.ThoiGianMangThai_MucTieu;
                cmt.SoNgayCaiSua_MucTieu = thamso.SoNgayCaiSua_MucTieu;
                cmt.SoNgayKhongLamViec_MucTieu = thamso.SoNgayKhongLamViec_MucTieu;
                cmt.TrungBnhLua_MucTieu = thamso.TrungBnhLua_MucTieu;
                cmt.SoHeoTrongNam_MucTieu = thamso.SoHeoTrongNam_MucTieu;*/
                changeView();
            }
            /*            if (datMucTieu.check == 1)
                        {
                            MessageBox.Show("Button save");
                            cmt = new Caymuctieu((int?)OutlinedComboBox.SelectedItem, (int?)OutlinedComboBox1.SelectedItem);*/
            /*                cmt.Tylede_muctieuClone = datMucTieu.Tylede_muctieu_au;
                            cmt.SoHeoConSinhRa_muctieuClone = datMucTieu.SoHeoConSinhRa_muctieu_au;
                            cmt.ODeItCon_muctieuClone = datMucTieu.ODeItCon_muctieu_au;
                            cmt.SoHeoConSong_MucTieuClone = datMucTieu.SoHeoConSong_MucTieu_au;
                            cmt.SoHeoCaiSua_muctieuClone = datMucTieu.SoHeoCaiSua_muctieu_au;
                            cmt.SoConChetTruocKhiCaiSua_MucTieuClone = datMucTieu.SoConChetTruocKhiCaiSua_MucTieu_au;
                            cmt.ThoiGianMangThai_MucTieuClone = datMucTieu.ThoiGianMangThai_MucTieu_au;
                            cmt.SoNgayCaiSua_MucTieuClone = datMucTieu.SoNgayCaiSua_MucTieu_au;
                            cmt.SoNgayKhongLamViec_MucTieuClone = datMucTieu.SoNgayKhongLamViec_MucTieu_au;
                            cmt.TrungBnhLua_MucTieuClone = datMucTieu.TrungBnhLua_MucTieu_au;
                            cmt.SoHeoTrongNam_MucTieuClone = datMucTieu.SoHeoTrongNam_MucTieu_au;*/

            /*                cmt.Tylede_muctieu_textbox.Content = "Mục tiêu: " + cmt.Tylede_muctieuClone + "%";
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
                            showmake.Children.Add(obj as UIElement);*/
        }
        //changeView();
    }
}

