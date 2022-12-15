using MaterialDesignThemes.Wpf;
using QuanLyTraiHeo.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyTraiHeo.View.Windows.Thiết_lập_cây_mục_tiêu
{
    /// <summary>
    /// Interaction logic for Caymuctieu.xaml
    /// </summary>
    public partial class Caymuctieu : Window
    {
        public List<LICHTIEMHEO> Lichtiem { get; set; }
        public List<HEO> Heo { get; set; }
        public List<LICHPHOIGIONG> LichPhoiGiong { get; set; }
        public List<CT_PHIEUHEO> BanHeo { get; set; }

        public static int SoHeo = 0;
        public static int SoHeoDe = 0;
        public static int SoHeoNai = 0;
        public static int SoHeoDuc = 0;
        public static int SoLuaDe = 0;
        public static int SoConDe = 0;

        public static double SoConDe_Lua_TB = 0;
        public static int SoODeItCon = 0;
        public static int SoConDeThucTe = 0;
        public static double SoConDeThucTe_TB = 0;

        public static int SoConCaiSua = 0;
        public static double SoConCaiSua_TB = 0;

        public static int Songaymangthai = 0;
        public static double Songaymangthai_tb = 0;

        public static int Songaycaisuanha = 0;
        public static double Songaycaisua_tb = 0;

        public static int Songaychophoigiong = 0;
        public static double Songaychophoigiong_tb = 0;

        public static double SoLuaDeTrongMotNam = 0;
        public static double SoConHeoSuaTrongMotNam = 0;

        public static int SoHeoDuocban = 0;
        /// <summary>
        /// Giá trị mặc định
        /// </summary>
        public static double Tylede_muctieu = 86;
        public static double SoHeoConSinhRa_muctieu = 12.5;
        public static double ODeItCon_muctieu = 12;
        public static double SoHeoConSong_MucTieu = 11;
        public static double SoHeoCaiSua_muctieu = 9.5;
        public static double SoConChetTruocKhiCaiSua_MucTieu = 18;
        public static string ThoiGianMangThai_MucTieu = "110-117";
        public static string SoNgayCaiSua_MucTieu = "20-28";
        public static string SoNgayKhongLamViec_MucTieu = "12";
        public static double TrungBnhLua_MucTieu = 2.3;
        public static double SoHeoTrongNam_MucTieu = 22;

        public double Tylede_muctieuClone = Tylede_muctieu;
        public double SoHeoConSinhRa_muctieuClone = SoHeoConSinhRa_muctieu;
        public double ODeItCon_muctieuClone = ODeItCon_muctieu;
        public double SoHeoConSong_MucTieuClone = SoHeoConSong_MucTieu;
        public double SoHeoCaiSua_muctieuClone = SoHeoCaiSua_muctieu;
        public double SoConChetTruocKhiCaiSua_MucTieuClone = SoConChetTruocKhiCaiSua_MucTieu;
        public string ThoiGianMangThai_MucTieuClone = ThoiGianMangThai_MucTieu;
        public string SoNgayCaiSua_MucTieuClone = SoNgayCaiSua_MucTieu;
        public string SoNgayKhongLamViec_MucTieuClone = SoNgayKhongLamViec_MucTieu;
        public double TrungBnhLua_MucTieuClone = TrungBnhLua_MucTieu;
        public double SoHeoTrongNam_MucTieuClone = SoHeoTrongNam_MucTieu;

        public Caymuctieu(int? yearStart, int? yearEnd)
        {
            Heo = DataProvider.Ins.DB.HEOs.ToList();
            LichPhoiGiong = DataProvider.Ins.DB.LICHPHOIGIONGs.ToList();
            InitializeComponent();
            Input(yearStart, yearEnd);
        }
        public void Input(int? yearStart, int? yearEnd)
        {
            if (yearStart == null || yearEnd == null)
            {
                //yearEnd = DateTime.Today.Year;
                //yearStart = DateTime.Today.Year - 1;
                hamHienView(DateTime.Today.Year - 1, DateTime.Today.Year);
                ResetView();
            }
        }
        public void Caculate(int y1, int y2)
        {

            if (y1 > y2)
            {
                MessageBox.Show("Năm bắt đầu phải nhỏ hơn năm kết thúc");
            }
            else
            {
                hamHienView(y1, y2);
                ResetView();
            }
        }
        public void clone()
        {
            Tylede_muctieu = Tylede_muctieuClone;
            Tylede_muctieu_textbox.Content = "Mục tiêu: " + Tylede_muctieu + "%";
            SoHeoConSinhRa_muctieu = SoHeoConSinhRa_muctieuClone;
            Soheoconsinhra_lua_muctieu.Content = "Mục tiêu: " + SoHeoConSinhRa_muctieu + " con";
            ODeItCon_muctieu = ODeItCon_muctieuClone;
            Odeitcon_muctieu.Content = "Mục tiêu: " + ODeItCon_muctieu + "%";
            SoHeoConSong_MucTieu = SoHeoConSong_MucTieuClone;
            Soheoconsong_muctieu.Content = "Mục tiêu: " + SoHeoConSong_MucTieu + " con";
            SoHeoCaiSua_muctieu = SoHeoCaiSua_muctieuClone;
            Soheocaisua_muctieu.Content = "Mục tiêu: " + SoHeoCaiSua_muctieu + " con";
            SoConChetTruocKhiCaiSua_MucTieu = SoConChetTruocKhiCaiSua_MucTieuClone;
            Tylechet_muctieu.Content = "Mục tiêu: " + SoConChetTruocKhiCaiSua_MucTieu + "%";
            ThoiGianMangThai_MucTieu = ThoiGianMangThai_MucTieuClone;
            Thoigianmangthai_muctieu.Content = "Mục tiêu: " + ThoiGianMangThai_MucTieu + " ngày";
            SoNgayCaiSua_MucTieu = SoNgayCaiSua_MucTieuClone;
            Songaycaisua_muctieu.Content = "Mục tiêu: " + SoNgayCaiSua_MucTieu + " ngày";
            SoNgayKhongLamViec_MucTieu = SoNgayKhongLamViec_MucTieuClone;
            Songaycaisua_muctieu.Content = "Mục tiêu: " + SoNgayCaiSua_MucTieu + " ngày";
            TrungBnhLua_MucTieu = TrungBnhLua_MucTieuClone;
            Songaycaisua_muctieu.Content = "Mục tiêu: " + SoNgayCaiSua_MucTieu + " ngày";
            SoHeoTrongNam_MucTieu = SoHeoTrongNam_MucTieuClone;
            Soheotrongnam_muctieu.Content = "Mục tiêu: " + SoHeoTrongNam_MucTieu + " con";
        }

        void hamHienView(int y1, int y2)
        {
            CT_PHIEUHEO test;
            CT_PHIEUHEO test1;

            foreach (var t in DataProvider.Ins.DB.LICHPHOIGIONGs)
            {
                //test = (CT_PHIEUHEO)t.HEO.CT_PHIEUHEO;
                //test1 = (CT_PHIEUHEO)t.HEO1.CT_PHIEUHEO;
                if (t.NgayPhoiGiong != null && t.NgayPhoiGiong.Value.Year >= y1 && t.NgayPhoiGiong.Value.Year <= y2)
                {
                    SoHeo += 2;
                    if (t.HEO.GioiTinh == "Đực")
                    { SoHeoDuc++; }
                    else
                    { SoHeoNai++; }

                    /*if(t.HEO.CT_PHIEUHEO != null && BanHeo.Contains(test) != true)
                    {
                        BanHeo.Add(test);
                        SoHeoDuocban++;
                    }
                    if (t.HEO1.CT_PHIEUHEO != null && BanHeo.Contains(test1) != true)
                    {
                        BanHeo.Add(test1);
                        SoHeoDuocban++;
                    }*/

                    if (t.HEO1.GioiTinh == "Đực")
                    { SoHeoDuc++; }
                    else
                    { SoHeoNai++; }

                    if (t.SoCon != null)
                    {
                        SoConDe += (int)t.SoCon;
                        SoLuaDe++;

                        if (t.NgayDeThucTe != null)
                            SoHeoDe++;

                        if (t.SoCon <= 8)
                        {
                            SoODeItCon++;
                        }

                        if (t.SoConChet != null)
                        {
                            SoConDeThucTe += (int)t.SoCon - (int)t.SoConChet;
                        }
                        else
                        {
                            SoConDeThucTe += (int)t.SoCon;
                        }

                        if (t.SoCon != null)
                        {
                            SoConCaiSua += (int)t.SoConChon;
                        }

                        if ((t.NgayPhoiGiong != null) && (t.NgayDeThucTe != null))
                        {
                            Songaymangthai = (int)((DateTime)t.NgayDeThucTe - (DateTime)t.NgayPhoiGiong).TotalDays;
                            Songaymangthai_tb += Songaymangthai;
                        }

                        if ((t.NgayCaiSua != null) && (t.NgayDeThucTe != null))
                        {
                            Songaycaisuanha = (int)((DateTime)t.NgayCaiSua - (DateTime)t.NgayDeThucTe).TotalDays;
                            Songaycaisua_tb += Songaycaisuanha;
                        }

                        if ((t.NgayCaiSua != null) && (t.NgayPhoiGiongLaiDuKien != null))
                        {
                            Songaychophoigiong = (int)((DateTime)t.NgayPhoiGiongLaiDuKien - (DateTime)t.NgayCaiSua).TotalDays;
                            Songaychophoigiong_tb += Songaychophoigiong;
                        }
                    }
                }
            }
            try
            {
                Tylede();
                SoHeoDe_lua();
                TinhsocondeIt();
                SoconSinhraConSong();
                SoHeoCaiSua();
                SoConChetTruocKhiCaiSua();
                ThoiGianMangThai();
                SoNgayCaiSua();
                SongayHeoNaiKhongLamViec();
                Trungbinhlua();
                SoHeoConTrongNam();
            }
            catch (Exception)
            {


            }
        }

        void ResetView()
        {
            SoHeo = 0;
            SoHeoDe = 0;
            SoHeoNai = 0;
            SoHeoDuc = 0;
            SoLuaDe = 0;
            SoConDe = 0;
            SoConDe_Lua_TB = 0;
            SoODeItCon = 0;
            SoConDeThucTe = 0;
            SoConDeThucTe_TB = 0;
            SoConCaiSua = 0;
            SoConCaiSua_TB = 0;

            Songaymangthai = 0;
            Songaymangthai_tb = 0;

            Songaycaisuanha = 0;
            Songaycaisua_tb = 0;

            Songaychophoigiong = 0;
            Songaychophoigiong_tb = 0;

            SoLuaDeTrongMotNam = 0;
            SoConHeoSuaTrongMotNam = 0;
        }
        void Tylede()
        {
            float tyle;
            if (SoHeoNai != 0)
            { tyle = ((float)SoHeoDe / SoHeoNai) * 100; }
            else
            { tyle = 0; }
            Tylede_main.Content = Math.Round(tyle, 2) + "%";
            Tylede_muctieu_textbox.Content = "Mục tiêu: " + Tylede_muctieu + "%";
            if (tyle < 82)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                Tylede_main.Foreground = Brushes.Red;
                Tylede_main.FontWeight = FontWeights.Bold;
                Tylede_icon.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                Tylede_main.Foreground = Brushes.Green;
                Tylede_main.FontWeight = FontWeights.Bold;
                Tylede_icon.Source = imageSource;
            }
        }

        void SoHeoDe_lua()
        {
            Soheoconsinhra_lua_muctieu.Content = "Mục tiêu: " + SoHeoConSinhRa_muctieu + " con";
            if (SoLuaDe != 0)
            { SoConDe_Lua_TB = (double)SoConDe / SoLuaDe; }
            else
            { SoConDe_Lua_TB = 0; }
            Soheoconsinhra_lua.Content = SoConDe_Lua_TB + " con";
            if (SoConDe_Lua_TB < SoHeoConSinhRa_muctieu)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                Soheoconsinhra_lua.Foreground = Brushes.Red;
                Soheoconsinhra_lua.FontWeight = FontWeights.Bold;
                Soheoconsinhra_lua_Icon.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                Soheoconsinhra_lua.Foreground = Brushes.Green;
                Soheoconsinhra_lua.FontWeight = FontWeights.Bold;
                Soheoconsinhra_lua_Icon.Source = imageSource;
            }
        }

        void TinhsocondeIt()
        {
            double tyle;
            Odeitcon_muctieu.Content = "Mục tiêu: " + ODeItCon_muctieu + "%";
            if (SoLuaDe != 0)
            { tyle = (double)(SoODeItCon / SoLuaDe) * 100; }
            else { tyle = 0; }
            Odeitcon.Content = Math.Round(tyle, 2) + "%";
            if (tyle > ODeItCon_muctieu)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                Odeitcon.Foreground = Brushes.Red;
                Odeitcon.FontWeight = FontWeights.Bold;
                Odeitcon_Icon.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                Odeitcon.Foreground = Brushes.Green;
                Odeitcon.FontWeight = FontWeights.Bold;
                Odeitcon_Icon.Source = imageSource;
            }
        }

        void SoconSinhraConSong()
        {
            if (SoLuaDe != 0)
            {
                SoConDeThucTe_TB = (double)SoConDeThucTe / SoLuaDe;
            }
            else
            {
                SoConDeThucTe_TB = 0;
            }
            Soheoconsong_muctieu.Content = "Mục tiêu: " + SoHeoConSong_MucTieu + " con";
            Soheoconsong.Content = Math.Round(SoConDeThucTe_TB, 2) + " con";
            if (SoConDeThucTe_TB < 11)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                Soheoconsong.Foreground = Brushes.Red;
                Soheoconsong.FontWeight = FontWeights.Bold;
                Soheoconsong_Icon.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                Soheoconsong.Foreground = Brushes.Green;
                Soheoconsong.FontWeight = FontWeights.Bold;
                Soheoconsong_Icon.Source = imageSource;
            }
        }

        void SoHeoCaiSua()
        {
            Soheocaisua_muctieu.Content = "Mục tiêu: " + SoHeoCaiSua_muctieu + " con";
            if (SoLuaDe != 0)
            { SoConCaiSua_TB = (double)(SoConCaiSua / SoLuaDe); }
            else
            { SoConCaiSua_TB = 0; }
            Soheocaisua.Content = Math.Round(SoConCaiSua_TB, 2) + " con";
            if (SoConCaiSua_TB < 9.5)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                Soheocaisua.Foreground = Brushes.Red;
                Soheocaisua.FontWeight = FontWeights.Bold;
                Soheocaisua_Icon.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                Soheocaisua.Foreground = Brushes.Green;
                Soheocaisua.FontWeight = FontWeights.Bold;
                Soheocaisua_Icon.Source = imageSource;
            }

        }

        void SoConChetTruocKhiCaiSua()
        {
            double tyle;
            double temp = SoConDeThucTe_TB - SoConCaiSua_TB;
            Tylechet_muctieu.Content = "Mục tiêu: " + SoConChetTruocKhiCaiSua_MucTieu + "%";
            if (SoConDeThucTe_TB != 0)
            { tyle = (double)(temp / SoConDeThucTe_TB) * 100; }
            else { tyle = 0; }
            Tylechet.Content = Math.Round(tyle, 2) + "%";
            if (tyle > 18)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                Tylechet.Foreground = Brushes.Red;
                Tylechet.FontWeight = FontWeights.Bold;
                Tylechet_Icon.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                Tylechet.Foreground = Brushes.Green;
                Tylechet.FontWeight = FontWeights.Bold;
                Tylechet_Icon.Source = imageSource;
            }
        }

        void ThoiGianMangThai()
        {
            Thoigianmangthai_muctieu.Content = "Mục tiêu: " + ThoiGianMangThai_MucTieu + " ngày";
            if (SoLuaDe != 0)
            { Songaymangthai_tb = (double)(Songaymangthai_tb / SoLuaDe); }
            else
            {
                Songaymangthai_tb = 0;
            }
            Thoigianmangthai.Content = Math.Round(Songaymangthai_tb, 2) + " ngày";
            if (Songaymangthai_tb < 110 || Songaymangthai_tb > 117)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                Thoigianmangthai.Foreground = Brushes.Red;
                Thoigianmangthai.FontWeight = FontWeights.Bold;
                Thoigianmangthai_Icon.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                Thoigianmangthai.Foreground = Brushes.Green;
                Thoigianmangthai.FontWeight = FontWeights.Bold;
                Thoigianmangthai_Icon.Source = imageSource;
            }
        }

        void SoNgayCaiSua()
        {
            Songaycaisua_muctieu.Content = "Mục tiêu: " + SoNgayCaiSua_MucTieu + " ngày";
            if (SoLuaDe != 0)
                Songaycaisua_tb = (double)(Songaycaisua_tb / SoLuaDe);
            else
                Songaycaisua_tb = 0;
            Songaycaisua.Content = Math.Round(Songaycaisua_tb, 2) + " ngày";
            if (Songaycaisua_tb < 20 || Songaycaisua_tb > 28)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                Songaycaisua.Foreground = Brushes.Red;
                Songaycaisua.FontWeight = FontWeights.Bold;
                Songaycaisua_Icon.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                Songaycaisua.Foreground = Brushes.Green;
                Songaycaisua.FontWeight = FontWeights.Bold;
                Songaycaisua_Icon.Source = imageSource;
            }
        }

        void SongayHeoNaiKhongLamViec()
        {
            SoNgayKhongLamViec_muctieu.Content = "Mục tiêu: " + SoNgayKhongLamViec_MucTieu + " ngày";
            if (SoLuaDe != 0)
                Songaychophoigiong_tb = (double)(Songaychophoigiong_tb / SoLuaDe);
            else
                Songaychophoigiong_tb = 0;
            SoNgayKhongLamViec.Content = Math.Round(Songaychophoigiong_tb, 2) + " ngày";
            if (Songaychophoigiong_tb > 12)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                SoNgayKhongLamViec.Foreground = Brushes.Red;
                SoNgayKhongLamViec.FontWeight = FontWeights.Bold;
                SoNgayKhongLamViec_Icon.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                SoNgayKhongLamViec.Foreground = Brushes.Green;
                SoNgayKhongLamViec.FontWeight = FontWeights.Bold;
                SoNgayKhongLamViec_Icon.Source = imageSource;
            }
        }

        void Trungbinhlua()
        {
            TrungbinhLua_muctieu.Content = "Mục tiêu: " + TrungBnhLua_MucTieu + " lứa";
            if (Songaycaisua_tb + Songaychophoigiong_tb + Songaymangthai_tb != 0)
                SoLuaDeTrongMotNam = (double)(365 / (Songaycaisua_tb + Songaychophoigiong_tb + Songaymangthai_tb));
            else
                SoLuaDeTrongMotNam = 0;
            TrungbinhLua.Content = Math.Round(SoLuaDeTrongMotNam, 2) + " lứa";
            if (SoLuaDeTrongMotNam < 2.3)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                TrungbinhLua.Foreground = System.Windows.Media.Brushes.Red;
                TrungbinhLua.FontWeight = FontWeights.Bold;
                TrungbinhLua_Icon.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                TrungbinhLua.Foreground = System.Windows.Media.Brushes.Green;
                TrungbinhLua.FontWeight = FontWeights.Bold;
                TrungbinhLua_Icon.Source = imageSource;
            }
        }

        void SoHeoConTrongNam()
        {
            Soheotrongnam_muctieu.Content = "Mục tiêu: " + SoHeoTrongNam_MucTieu + " con";
            SoConHeoSuaTrongMotNam = (double)(SoConCaiSua_TB * SoLuaDeTrongMotNam);
            Soheotrongnam.Content = Math.Round(SoConHeoSuaTrongMotNam, 2) + " con";
            if (SoConHeoSuaTrongMotNam < 22)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                Soheotrongnam.Foreground = System.Windows.Media.Brushes.Red;
                Soheotrongnam.FontWeight = FontWeights.Bold;
                //Soheotrongnam_Icon.Source = imageSource;
                Soheotrongnam_button_image.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                Soheotrongnam.Foreground = System.Windows.Media.Brushes.Green;
                Soheotrongnam.FontWeight = FontWeights.Bold;
                //Soheotrongnam_Icon.Source = imageSource;
                Soheotrongnam_button_image.Source = imageSource;
            }
        }

        void DoanhThuTrungBinh()
        {
            int temp = 0;
            foreach (var item in BanHeo)
            {
                temp += int.Parse(item.DonGia.ToString());
            }
            DoanhThuUocTinh_uoctinh.Content = "Mục tiêu: " + (1400000 * SoConHeoSuaTrongMotNam) + " triệu";
            DoanhThuUocTinh.Content = (double)temp / SoHeoDuocban + " triệu";
            if ((temp / SoHeoDuocban) < (1400000 * SoConHeoSuaTrongMotNam))
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                DoanhThuUocTinh.Foreground = System.Windows.Media.Brushes.Red;
                DoanhThuUocTinh.FontWeight = FontWeights.Bold;
                DoanhThuUocTinh_Icon.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                DoanhThuUocTinh.Foreground = System.Windows.Media.Brushes.Green;
                DoanhThuUocTinh.FontWeight = FontWeights.Bold;
                DoanhThuUocTinh_Icon.Source = imageSource;
            }
        }

        private void _1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Doanh số không đạt chỉ tiêu dự kiến, không đủ tiền chi trả cho nhân viên và chi phí vận hành", "Chi tiết", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void zoom_in_Click(object sender, RoutedEventArgs e)
        {

            var scaler = Zoomviewbox.LayoutTransform as ScaleTransform;

            if (scaler == null)
            {
                // Currently no zoom, so go instantly to max zoom.
                Zoomviewbox.LayoutTransform = new ScaleTransform(1.5, 1.5);
            }
            else
            {
                double curZoomFactor = scaler.ScaleX;

                // If the current ScaleX and ScaleY properties were set by animation,
                // we'll have to remove the animation before we can explicitly set
                // them to "local" values.

                if (scaler.HasAnimatedProperties)
                {
                    // Remove the animation by assigning a null
                    // AnimationTimeline to the properties.
                    // Note that this causes them to revert to
                    // their most recently assigned "local" values.

                    scaler.BeginAnimation(ScaleTransform.ScaleXProperty, null);
                    scaler.BeginAnimation(ScaleTransform.ScaleYProperty, null);
                }

                if (curZoomFactor < 3.0)
                {
                    scaler.ScaleX += 0.5;
                    scaler.ScaleY += 0.5;
                }
            }
        }

        void enterCard(ToggleButton toggle, Card c)
        {
            toggle.IsChecked = true;
            toggle.Background = System.Windows.Media.Brushes.Yellow;
            c.Background = Brushes.WhiteSmoke;
        }

        void leaveCard(ToggleButton toggle, Card c)
        {
            toggle.IsChecked = false;
            toggle.Background = System.Windows.Media.Brushes.White;
            c.Background = Brushes.White;
        }
        void checkclick(ToggleButton toggle)
        {
            if (Soheotrongnam_button.IsChecked == true)
            {
                toggle.Background = System.Windows.Media.Brushes.Yellow;
            }
            else
            {
                toggle.Background = System.Windows.Media.Brushes.White;
            }
        }
        private void the2_MouseEnter(object sender, MouseEventArgs e)
        {
            enterCard(Soheotrongnam_button, the2);
        }

        private void the2_MouseLeave(object sender, MouseEventArgs e)
        {
            leaveCard(Soheotrongnam_button, the2);
        }

        private void Soheotrongnam_button_Click(object sender, RoutedEventArgs e)
        {
            checkclick(Soheotrongnam_button);
        }

        private void zoom_out_Click(object sender, RoutedEventArgs e)
        {
            var scaler = Zoomviewbox.LayoutTransform as ScaleTransform;

            if (scaler == null)
            {
                // Currently no zoom, so go instantly to max zoom.
                Zoomviewbox.LayoutTransform = new ScaleTransform(1.5, 1.5);
            }
            else
            {
                double curZoomFactor = scaler.ScaleX;

                // If the current ScaleX and ScaleY properties were set by animation,
                // we'll have to remove the animation before we can explicitly set
                // them to "local" values.

                if (scaler.HasAnimatedProperties)
                {
                    // Remove the animation by assigning a null
                    // AnimationTimeline to the properties.
                    // Note that this causes them to revert to
                    // their most recently assigned "local" values.

                    scaler.BeginAnimation(ScaleTransform.ScaleXProperty, null);
                    scaler.BeginAnimation(ScaleTransform.ScaleYProperty, null);
                }

                if (curZoomFactor > 1.0)
                {
                    scaler.ScaleX -= 0.5;
                    scaler.ScaleY -= 0.5;
                }
            }
        }
    }
}

