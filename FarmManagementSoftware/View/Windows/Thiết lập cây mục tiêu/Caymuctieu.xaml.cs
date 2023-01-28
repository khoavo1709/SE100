using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using FarmManagementSoftware.Model;
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
using System.IO;

namespace FarmManagementSoftware.View.Windows
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

        List<string> GiaTriMucTieuList = new List<string>();
        #region Tính toán
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

        public static float TyLeDe = 0;
        public static double ODeItCon = 0;
        public static double SoConChetTruocKhiCaiSua_ = 0;
        public static int DoanhThu = 0;

        public static int SoHeoDuocBan = 0;
        public static double TyLeThayDan_ = 0;

        #endregion
        /// <summary>
        /// Giá trị mặc định
        /// </summary>

        #region chiSoMucTieu
        public double Tylede_muctieu;
        public double SoHeoConSinhRa_muctieu;
        public double ODeItCon_muctieu;
        public double SoHeoConSong_MucTieu;
        public double SoHeoCaiSua_muctieu;
        public double SoConChetTruocKhiCaiSua_MucTieu;
        public double ThoiGianMangThai_MucTieu;
        public double SoNgayCaiSua_MucTieu;
        public string SoNgayKhongLamViec_MucTieu;
        public double TrungBnhLua_MucTieu;
        public double SoHeoTrongNam_MucTieu;
        public double TyLeThayDan_MucTieu;
        #endregion

        /*public double Tylede_muctieuClone = Tylede_muctieu;
        public double SoHeoConSinhRa_muctieuClone = SoHeoConSinhRa_muctieu;
        public double ODeItCon_muctieuClone = ODeItCon_muctieu;
        public double SoHeoConSong_MucTieuClone = SoHeoConSong_MucTieu;
        public double SoHeoCaiSua_muctieuClone = SoHeoCaiSua_muctieu;
        public double SoConChetTruocKhiCaiSua_MucTieuClone = SoConChetTruocKhiCaiSua_MucTieu;
        public string ThoiGianMangThai_MucTieuClone = ThoiGianMangThai_MucTieu;
        public string SoNgayCaiSua_MucTieuClone = SoNgayCaiSua_MucTieu;
        public string SoNgayKhongLamViec_MucTieuClone = SoNgayKhongLamViec_MucTieu;
        public double TrungBnhLua_MucTieuClone = TrungBnhLua_MucTieu;
        public double SoHeoTrongNam_MucTieuClone = SoHeoTrongNam_MucTieu;*/

        //Constructer
        public Caymuctieu(int? yearStart, int? yearEnd)
        {
            Heo = DataProvider.Ins.DB.HEOs.ToList();
            BanHeo = DataProvider.Ins.DB.CT_PHIEUHEO.ToList();
            LichPhoiGiong = DataProvider.Ins.DB.LICHPHOIGIONGs.ToList();
            Window_Loaded();
            //setGiaTriStatic();
            InitializeComponent();
            Input(yearStart, yearEnd);
            AddEvent();
        }
        //Method

        void setGiaTriStatic()
        {
            Tylede_muctieu = 86;
            SoHeoConSinhRa_muctieu = 12.5;
            ODeItCon_muctieu = 12;
            SoHeoConSong_MucTieu = 11;
            SoHeoCaiSua_muctieu = 9.5;
            SoConChetTruocKhiCaiSua_MucTieu = 18;
            ThoiGianMangThai_MucTieu = 110;
            SoNgayCaiSua_MucTieu = 20;
            SoNgayKhongLamViec_MucTieu = "12";
            TrungBnhLua_MucTieu = 2.3;
            SoHeoTrongNam_MucTieu = 22;
            TyLeThayDan_MucTieu = 40;
        }
        public void Input(int? yearStart, int? yearEnd)
        {
            if (yearStart == null || yearEnd == null)
            {
                //yearEnd = DateTime.Today.Year;
                //yearStart = DateTime.Today.Year - 1;
                ResetView();
                hamHienView(DateTime.Today.Year - 1, DateTime.Today.Year);
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
                ResetView();
                hamHienView(y1, y2);
            }
        }
        /*public void clone()
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
        }*/
        void setValue(ThamSo thamSo)
        {
            Tylede_muctieu = thamSo.Tylede_muctieuClone;
            SoHeoConSinhRa_muctieu = thamSo.SoHeoConSinhRa_muctieuClone;
            ODeItCon_muctieu = thamSo.ODeItCon_muctieuClone;
            SoHeoConSong_MucTieu = thamSo.SoHeoConSong_MucTieuClone;
            SoHeoCaiSua_muctieu = thamSo.SoHeoCaiSua_muctieuClone;
            SoConChetTruocKhiCaiSua_MucTieu = thamSo.SoConChetTruocKhiCaiSua_MucTieuClone;
            ThoiGianMangThai_MucTieu = thamSo.ThoiGianMangThai_MucTieuClone;
            SoNgayCaiSua_MucTieu = thamSo.SoNgayCaiSua_MucTieuClone;
            SoNgayKhongLamViec_MucTieu = thamSo.SoNgayKhongLamViec_MucTieuClone;
            TrungBnhLua_MucTieu = thamSo.TrungBnhLua_MucTieuClone;
            SoHeoTrongNam_MucTieu = thamSo.SoHeoTrongNam_MucTieuClone;
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
                DoanhThuTrungBinh(y1, y2);
                TyLeThayDan();
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

            TyLeDe = 0;
            ODeItCon = 0;
            SoConChetTruocKhiCaiSua_ = 0;
            DoanhThu = 0;
        }
        void Tylede()
        {
            //float tyle;
            if (SoHeoNai != 0)
            { TyLeDe = ((float)SoHeoDe / SoHeoNai) * 100; }
            else
            { TyLeDe = 0; }
            Tylede_main.Content = Math.Round(TyLeDe, 2) + "%";
            Tylede_muctieu_textbox.Content = "Mục tiêu: " + Tylede_muctieu + "%";
            if (TyLeDe < Tylede_muctieu)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                Tylede_main.Foreground = Brushes.Red;
                Tylede_main.FontWeight = FontWeights.Bold;
                Tylede_button_image.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                Tylede_main.Foreground = Brushes.Green;
                Tylede_main.FontWeight = FontWeights.Bold;
                Tylede_button_image.Source = imageSource;
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
                Soheoconsinhra_lua_button_image.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                Soheoconsinhra_lua.Foreground = Brushes.Green;
                Soheoconsinhra_lua.FontWeight = FontWeights.Bold;
                Soheoconsinhra_lua_button_image.Source = imageSource;
            }
        }

        void TinhsocondeIt()
        {
            //double tyle;
            Odeitcon_muctieu.Content = "Mục tiêu: " + ODeItCon_muctieu + "%";
            if (SoLuaDe != 0)
            { ODeItCon = (double)(SoODeItCon / SoLuaDe) * 100; }
            else { ODeItCon = 0; }
            Odeitcon.Content = Math.Round(ODeItCon, 2) + "%";
            if (ODeItCon > ODeItCon_muctieu)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                Odeitcon.Foreground = Brushes.Red;
                Odeitcon.FontWeight = FontWeights.Bold;
                Odeitcon_button_image.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                Odeitcon.Foreground = Brushes.Green;
                Odeitcon.FontWeight = FontWeights.Bold;
                Odeitcon_button_image.Source = imageSource;
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
            if (SoConDeThucTe_TB < SoHeoConSong_MucTieu)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                Soheoconsong.Foreground = Brushes.Red;
                Soheoconsong.FontWeight = FontWeights.Bold;
                Soheoconsong_button_image.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                Soheoconsong.Foreground = Brushes.Green;
                Soheoconsong.FontWeight = FontWeights.Bold;
                Soheoconsong_button_image.Source = imageSource;
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
            if (SoConCaiSua_TB < SoHeoCaiSua_muctieu)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                Soheocaisua.Foreground = Brushes.Red;
                Soheocaisua.FontWeight = FontWeights.Bold;
                Soheocaisua_button_image.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                Soheocaisua.Foreground = Brushes.Green;
                Soheocaisua.FontWeight = FontWeights.Bold;
                Soheocaisua_button_image.Source = imageSource;
            }

        }

        void SoConChetTruocKhiCaiSua()
        {
            //double tyle;
            double temp = SoConDeThucTe_TB - SoConCaiSua_TB;
            Tylechet_muctieu.Content = "Mục tiêu: " + SoConChetTruocKhiCaiSua_MucTieu + "%";
            if (SoConDeThucTe_TB != 0)
            { SoConChetTruocKhiCaiSua_ = (double)(temp / SoConDeThucTe_TB) * 100; }
            else { SoConChetTruocKhiCaiSua_ = 0; }
            Tylechet.Content = Math.Round(SoConChetTruocKhiCaiSua_, 2) + "%";
            if (SoConChetTruocKhiCaiSua_ > SoConChetTruocKhiCaiSua_MucTieu)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                Tylechet.Foreground = Brushes.Red;
                Tylechet.FontWeight = FontWeights.Bold;
                Tylechet_button_image.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                Tylechet.Foreground = Brushes.Green;
                Tylechet.FontWeight = FontWeights.Bold;
                Tylechet_button_image.Source = imageSource;
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
            if (Songaymangthai_tb < ThoiGianMangThai_MucTieu)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                Thoigianmangthai.Foreground = Brushes.Red;
                Thoigianmangthai.FontWeight = FontWeights.Bold;
                Thoigianmangthai_button_image.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                Thoigianmangthai.Foreground = Brushes.Green;
                Thoigianmangthai.FontWeight = FontWeights.Bold;
                Thoigianmangthai_button_image.Source = imageSource;
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
            if (Songaycaisua_tb < SoNgayCaiSua_MucTieu)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                Songaycaisua.Foreground = Brushes.Red;
                Songaycaisua.FontWeight = FontWeights.Bold;
                Songaycaisua_button_image.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                Songaycaisua.Foreground = Brushes.Green;
                Songaycaisua.FontWeight = FontWeights.Bold;
                Songaycaisua_button_image.Source = imageSource;
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
            if (Songaychophoigiong_tb > double.Parse(SoNgayKhongLamViec_MucTieu))
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                SoNgayKhongLamViec.Foreground = Brushes.Red;
                SoNgayKhongLamViec.FontWeight = FontWeights.Bold;
                SoNgayKhongLamViec_button_image.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                SoNgayKhongLamViec.Foreground = Brushes.Green;
                SoNgayKhongLamViec.FontWeight = FontWeights.Bold;
                SoNgayKhongLamViec_button_image.Source = imageSource;
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
            if (SoLuaDeTrongMotNam < TrungBnhLua_MucTieu)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                TrungbinhLua.Foreground = System.Windows.Media.Brushes.Red;
                TrungbinhLua.FontWeight = FontWeights.Bold;
                TrungbinhLua_button_image.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                TrungbinhLua.Foreground = System.Windows.Media.Brushes.Green;
                TrungbinhLua.FontWeight = FontWeights.Bold;
                TrungbinhLua_button_image.Source = imageSource;
            }
        }

        void SoHeoConTrongNam()
        {
            Soheotrongnam_muctieu.Content = "Mục tiêu: " + SoHeoTrongNam_MucTieu + " con";
            SoConHeoSuaTrongMotNam = (double)(SoConCaiSua_TB * SoLuaDeTrongMotNam);
            Soheotrongnam.Content = Math.Round(SoConHeoSuaTrongMotNam, 2) + " con";
            if (SoConHeoSuaTrongMotNam < SoHeoTrongNam_MucTieu)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                Soheotrongnam.Foreground = System.Windows.Media.Brushes.Red;
                Soheotrongnam.FontWeight = FontWeights.Bold;
                Soheotrongnam_button_image.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                Soheotrongnam.Foreground = System.Windows.Media.Brushes.Green;
                Soheotrongnam.FontWeight = FontWeights.Bold;
                Soheotrongnam_button_image.Source = imageSource;
            }
        }

        void DoanhThuTrungBinh(int a, int b)
        {
            foreach (var item in BanHeo)
            {
                if (item.DonGia != null && item.PHIEUHEO.NgayLap.Value.Year <= b && item.PHIEUHEO.NgayLap.Value.Year >= a)
                {
                    SoHeoDuocBan++;
                    DoanhThu += item.DonGia.Value;
                }
            }
            DoanhThuUocTinh_uoctinh.Content = "Mục tiêu: " + (1400000 * SoConHeoSuaTrongMotNam).ToString("n0") + " triệu";
            DoanhThuUocTinh.Content = DoanhThu.ToString("n0") + " triệu";
            if ((DoanhThu /*/ SoHeoDuocban*/) < (1400000 * SoConHeoSuaTrongMotNam))
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                DoanhThuUocTinh.Foreground = System.Windows.Media.Brushes.Red;
                DoanhThuUocTinh.FontWeight = FontWeights.Bold;
                DoanhThuUocTinh_button_image.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                DoanhThuUocTinh.Foreground = System.Windows.Media.Brushes.Green;
                DoanhThuUocTinh.FontWeight = FontWeights.Bold;
                DoanhThuUocTinh_button_image.Source = imageSource;
            }
        }
        void TyLeThayDan()
        {
            if (SoHeoDuocBan != 0)
            { TyLeThayDan_ = ((float)SoHeo / SoHeoDuocBan) * 100; }
            else
            { TyLeThayDan_ = 0; }
            Tylethaydan.Content = Math.Round(TyLeDe, 2) + "%";
            Tylethaydan_muctieu.Content = "Mục tiêu: " + TyLeThayDan_MucTieu + "%";
            if (TyLeThayDan_ < TyLeThayDan_MucTieu || TyLeThayDan_ > TyLeThayDan_MucTieu + 10)
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-down-solid.png"));
                Tylethaydan.Foreground = System.Windows.Media.Brushes.Red;
                Tylethaydan.FontWeight = FontWeights.Bold;
                Tylethaydan_icon.Source = imageSource;
            }
            else
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/QuanLyTraiHeo;component/Image/thumbs-up-solid.png"));
                Tylethaydan.Foreground = System.Windows.Media.Brushes.Green;
                Tylethaydan.FontWeight = FontWeights.Bold;
                Tylethaydan_icon.Source = imageSource;
            }
        }
        void AddEvent()
        {
            //the1
            the1.MouseEnter += MouseEnter_1;
            the1.MouseLeave += MouseLeave_1;
            the1.MouseDown += ClickOn;
            DoanhThuUocTinh_button.Click += WhenClick;
            //the2
            the2.MouseEnter += MouseEnter_2;
            the2.MouseLeave += MouseLeave_2;
            the2.MouseDown += ClickOn;
            Soheotrongnam_button.Click += WhenClick;
            //the3
            the3.MouseEnter += MouseEnter_3;
            the3.MouseLeave += MouseLeave_3;
            the3.MouseDown += ClickOn;
            Soheocaisua_button.Click += WhenClick;
            //the4
            the4.MouseEnter += MouseEnter_4;
            the4.MouseLeave += MouseLeave_4;
            the4.MouseDown += ClickOn;
            TrungbinhLua_button.Click += WhenClick;
            //the5
            the5.MouseEnter += MouseEnter_5;
            the5.MouseLeave += MouseLeave_5;
            the5.MouseDown += ClickOn;
            Soheoconsong_button.Click += WhenClick;
            //the6
            the6.MouseEnter += MouseEnter_6;
            the6.MouseLeave += MouseLeave_6;
            the6.MouseDown += ClickOn;
            Tylechet_button.Click += WhenClick;
            //the7
            the7.MouseEnter += MouseEnter_7;
            the7.MouseLeave += MouseLeave_7;
            the7.MouseDown += ClickOn;
            Soheoconsinhra_lua_button.Click += WhenClick;
            //the8
            the8.MouseEnter += MouseEnter_8;
            the8.MouseLeave += MouseLeave_8;
            the8.MouseDown += ClickOn;
            Odeitcon_button.Click += WhenClick;
            //the9
            the9.MouseEnter += MouseEnter_9;
            the9.MouseLeave += MouseLeave_9;
            the9.MouseDown += ClickOn;
            SoNgayKhongLamViec_button.Click += WhenClick;
            //the10
            the10.MouseEnter += MouseEnter_10;
            the10.MouseLeave += MouseLeave_10;
            the10.MouseDown += ClickOn;
            Thoigianmangthai_button.Click += WhenClick;
            //the11
            the11.MouseEnter += MouseEnter_11;
            the11.MouseLeave += MouseLeave_11;
            the11.MouseDown += ClickOn;
            Songaycaisua_button.Click += WhenClick;
            //the12
            the12.MouseEnter += MouseEnter_12;
            the12.MouseLeave += MouseLeave_12;
            the12.MouseDown += ClickOn;
            Tylede_button.Click += WhenClick;
            //the13
            the13.MouseEnter += MouseEnter_13;
            the13.MouseLeave += MouseLeave_13;
            the13.MouseDown += ClickOn;
        }

        #region Event
        //Test
        private void _1_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        //zoom in zoom out button
        public void zoom_in_Click(object sender, RoutedEventArgs e)
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
        public void zoom_out_Click(object sender, RoutedEventArgs e)
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

        //method when enter card
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
            if (toggle.IsChecked == true)
            {
                toggle.Background = System.Windows.Media.Brushes.Yellow;
            }
            else
            {
                toggle.Background = System.Windows.Media.Brushes.White;
            }
        }
        #region EventCard
        //Event for card
        //2
        private void MouseEnter_2(object sender, MouseEventArgs e)
        {
            enterCard(Soheotrongnam_button, e.Source as Card);
        }
        private void MouseLeave_2(object sender, MouseEventArgs e)
        {
            leaveCard(Soheotrongnam_button, e.Source as Card);
        }
        //1
        private void MouseEnter_1(object sender, MouseEventArgs e)
        {
            enterCard(DoanhThuUocTinh_button, e.Source as Card);
        }
        private void MouseLeave_1(object sender, MouseEventArgs e)
        {
            leaveCard(DoanhThuUocTinh_button, e.Source as Card);
        }
        //3
        private void MouseEnter_3(object sender, MouseEventArgs e)
        {
            enterCard(Soheocaisua_button, e.Source as Card);
        }
        private void MouseLeave_3(object sender, MouseEventArgs e)
        {
            leaveCard(Soheocaisua_button, e.Source as Card);
        }

        private void MouseEnter_4(object sender, MouseEventArgs e)
        {
            enterCard(TrungbinhLua_button, e.Source as Card);
        }
        private void MouseLeave_4(object sender, MouseEventArgs e)
        {
            leaveCard(TrungbinhLua_button, e.Source as Card);
        }

        private void MouseEnter_5(object sender, MouseEventArgs e)
        {
            enterCard(Soheoconsong_button, e.Source as Card);
        }
        private void MouseLeave_5(object sender, MouseEventArgs e)
        {
            leaveCard(Soheoconsong_button, e.Source as Card);
        }

        private void MouseEnter_6(object sender, MouseEventArgs e)
        {
            enterCard(Tylechet_button, e.Source as Card);
        }
        private void MouseLeave_6(object sender, MouseEventArgs e)
        {
            leaveCard(Tylechet_button, e.Source as Card);
        }

        private void MouseEnter_7(object sender, MouseEventArgs e)
        {
            enterCard(Soheoconsinhra_lua_button, e.Source as Card);
        }
        private void MouseLeave_7(object sender, MouseEventArgs e)
        {
            leaveCard(Soheoconsinhra_lua_button, e.Source as Card);
        }

        private void MouseEnter_8(object sender, MouseEventArgs e)
        {
            enterCard(Odeitcon_button, e.Source as Card);
        }
        private void MouseLeave_8(object sender, MouseEventArgs e)
        {
            leaveCard(Odeitcon_button, e.Source as Card);
        }

        private void MouseEnter_9(object sender, MouseEventArgs e)
        {
            enterCard(SoNgayKhongLamViec_button, e.Source as Card);
        }
        private void MouseLeave_9(object sender, MouseEventArgs e)
        {
            leaveCard(SoNgayKhongLamViec_button, e.Source as Card);
        }

        private void MouseEnter_10(object sender, MouseEventArgs e)
        {
            enterCard(Thoigianmangthai_button, e.Source as Card);
        }
        private void MouseLeave_10(object sender, MouseEventArgs e)
        {
            leaveCard(Thoigianmangthai_button, e.Source as Card);
        }

        private void MouseEnter_11(object sender, MouseEventArgs e)
        {
            enterCard(Songaycaisua_button, e.Source as Card);
        }
        private void MouseLeave_11(object sender, MouseEventArgs e)
        {
            leaveCard(Songaycaisua_button, e.Source as Card);
        }

        private void MouseEnter_12(object sender, MouseEventArgs e)
        {
            enterCard(Tylede_button, e.Source as Card);
        }
        private void MouseLeave_12(object sender, MouseEventArgs e)
        {
            leaveCard(Tylede_button, e.Source as Card);
        }

        private void MouseEnter_13(object sender, MouseEventArgs e)
        {
            enterCard(Tylethaydan_button, e.Source as Card);
        }
        private void MouseLeave_13(object sender, MouseEventArgs e)
        {
            leaveCard(Tylethaydan_button, e.Source as Card);
        }
        #endregion
        private void WhenClick(object sender, RoutedEventArgs e)
        {
            checkclick(e.Source as ToggleButton);
        }

        private void ClickOn(object sender, RoutedEventArgs e)
        {
            switch (e.Source as Card)
            {
                case var value when value == the1: //nice trick
                    if ((DoanhThu /*/ SoHeoDuocban*/) < (1400000 * SoConHeoSuaTrongMotNam))
                    {
                        MessageBox.Show("Doanh số không đạt chỉ tiêu dự kiến, không đủ tiền chi trả cho nhân viên và chi phí vận hành", "Chi tiết", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
                case var value when value == the2:
                    if (SoConHeoSuaTrongMotNam < SoHeoTrongNam_MucTieu)
                    {
                        MessageBox.Show("Biện pháp:\n- Tăng số lần đẻ của nái trong năm (hay còn gọi là tăng số lứa đẻ/năm) để tăng tổng số heo con cai sữa trong năm.\n- Tăng số heo con sơ sinh ra để tăng tổng số heo con cai sữa tỏng năm.\n- Giảm tỷ lệ chết heo con ở trại đẻ để tăng tổng số heo con cai sữa trong năm.");
                    }
                    break;
                case var value when value == the3:
                    if (SoConCaiSua_TB < SoHeoCaiSua_muctieu)
                    {
                        MessageBox.Show("Biện pháp:\n- Nâng cao sức khỏe của toàn bộ heo con đủ tốt để có thể cai sữa\n- Chọn thời gian cai sữa cho heo hợp lý\n- Chuẩn bị chuồng nuôi hợp lý\n- Lưu ý chuyển heo và chế phẩm hỗ trợ");
                    }
                    break;
                case var value when value == the4:
                    if (SoLuaDeTrongMotNam < TrungBnhLua_MucTieu)
                    {
                        MessageBox.Show("Biện pháp:\n- Cho nái ăn đầy đủ dinh dưỡng và chất\n- Đảm bảo chuồng trại phù hợp với mùa trong năm\n- Điều chỉnh ngày nuôi con phù hợp( không quá ngắn hay quá dài");

                    }
                    break;
                case var value when value == the5:
                    if (SoConDeThucTe_TB < SoHeoConSong_MucTieu)
                    {
                        MessageBox.Show("Biện pháp:\n- Nên theo dõi chặt chẽ heo nái trong thời gian đẻ và ngay sau khi sinh để phát hiện sớm xem chân heo nái có vững không?, nó có bị stress gì không?... nhằm phát hiện sớm nguy cơ heo nái có thể sẽ đè chết heo con hay không.\n- Tăng cường các biện pháp giúp heo nái có 4 chân khỏe mạnh như bổ sung đầy đủ dinh dưỡng, khoáng, Canxi…\n- Giữ cho môi trường luôn yên tĩnh, không làm heo nái căng thẳng là việc vô cùng cần thiết.\n- Để ý quan sát một số heo nái có hành vi bất thường như không muốn heo con bú, đụng vào người (nhất là heo nái đẻ lứa đầu) để đề phòng.\n- Theo dõi hồ sơ ghi chép của từng heo nái xem trong những lứa đẻ trước heo nái đó có đè chết con bao giờ chưa? Nếu có thì xác suất là bao nhêu để có phương án dự phòng thích hợp.\n- Thiết kế ô chuồng heo nái đẻ sao cho kìm hãm heo nái và giúp bảo vệ heo con được tốt nhất.");
                    }
                    break;
                case var value when value == the6:
                    if (SoConChetTruocKhiCaiSua_ > SoConChetTruocKhiCaiSua_MucTieu)
                    {
                        MessageBox.Show("Biện pháp:\n- Hiểu rõ tập tính tự nhiên của lợn nái\n- Lựa chọn cho sức đề kháng của cơ thể\n- Lưu ý việc cho lợn con bú\n- Áp dụng phương pháp bồi dưỡng chéo\n- Quản lý nhiệt độ heo con.");
                    }
                    break;
                case var value when value == the7:
                    if (SoConDe_Lua_TB < SoHeoConSinhRa_muctieu)
                    {
                        MessageBox.Show("Biện pháp:\n– Heo cai sữa sau 4 tuần phải được chích ngừa.\n– Chỉ chọn heo của những heo nái đẻ từ lứa 2 trở lên và số con đẻ ra trên lứa cao.\n– Với heo nái tơ, chỉ cho phối vào lần lên giống thứ 2. Nên cho phối 2 lần cách nhau 10-12 giờ. Giữa hai lần lên giống, nên cho heo nái ăn nhiều hơn từ 0,5-0,75 kg thức ăn vào ngày thứ 10 sau khi heo lên giống lần trước.\n– Chú ý chọn giống để phối là những heo nọc có tỷ lệ phối đậu phôi cao, đẻ sai và được chích ngừa đầy đủ.\n– Sau khi phối, không cho ăn nhiều. Nếu như heo đòi ăn nhiều thì cho ăn thêm rau xanh. Thời gian này duy trì ít nhất là 3 tuần.");
                    }
                    break;
                case var value when value == the8:
                    if (ODeItCon > ODeItCon_muctieu)
                    {
                        MessageBox.Show("Biện pháp:\n- Nên theo dõi chặt chẽ heo nái trong thời gian đẻ và ngay sau khi sinh để phát hiện sớm xem chân heo nái có vững không?, nó có bị stress gì không?... nhằm phát hiện sớm nguy cơ heo nái có thể sẽ đè chết heo con hay không.\n- Tăng cường các biện pháp giúp heo nái có 4 chân khỏe mạnh như bổ sung đầy đủ dinh dưỡng, khoáng, Canxi…\n- Giữ cho môi trường luôn yên tĩnh, không làm heo nái căng thẳng là việc vô cùng cần thiết.\n- Để ý quan sát một số heo nái có hành vi bất thường như không muốn heo con bú, đụng vào người (nhất là heo nái đẻ lứa đầu) để đề phòng.\n- Theo dõi hồ sơ ghi chép của từng heo nái xem trong những lứa đẻ trước heo nái đó có đè chết con bao giờ chưa? Nếu có thì xác suất là bao nhêu để có phương án dự phòng thích hợp.\n- Thiết kế ô chuồng heo nái đẻ sao cho kìm hãm heo nái và giúp bảo vệ heo con được tốt nhất.");
                    }
                    break;
                case var value when value == the9:
                    if (Songaychophoigiong_tb > double.Parse(SoNgayKhongLamViec_MucTieu))
                    {
                        MessageBox.Show("Lưu ý cần đẩy heo nái không làm việc cho phối giống, tăng chu kì đẻ và tránh không đẻ heo nhàn rỗi dẫn tới stress");
                    }
                    break;
                case var value when value == the10:
                    if (Songaymangthai_tb < ThoiGianMangThai_MucTieu)
                    {
                        MessageBox.Show("Thời gian mang thai không đạt yêu cầu chứng tỏ tới việc chất lượng heo nái đang gặp vấn đề, cần xem xét kĩ.");
                    }
                    break;
                case var value when value == the11:
                    if (Songaycaisua_tb < SoNgayCaiSua_MucTieu)
                    {
                        MessageBox.Show("Thời gian cho bú không đảm bảo, dễ dẫn heo chết nhiều và không lớn nhanh năng suất chất lượng sụt giảm.");
                    }
                    break;
                case var value when value == the12:
                    if (TyLeDe < Tylede_muctieu)
                    {
                        MessageBox.Show("Tỷ lệ đẻ đang bị thấp, phương pháp khắc phục:\n-Phối lúc sáng sớm.\n-Bấm răng.\n-Sử dụng heo đực lai.\n-Duy trì chất lượng thức ăn.\n-Điều chỉnh vệ sinh và ánh sáng.");
                    }
                    break;
                case var value when value == the13:
                    if (true)
                    {
                        MessageBox.Show("Tỷ lệ thay đàn của trang trại hiện tại không đạt chỉ tiêu, dẫn tới chất lượng không được đảm bảo, ảnh hưởng nguồn năng suất đầu ra và đầu vào", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        public void Window_Loaded()
        {
            string applicationPath = System.IO.Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory); // the directory that your program is installed in
            string saveFilePath = System.IO.Path.Combine(applicationPath, "1.txt");
            if (File.Exists(saveFilePath))
            {
                Tylede_muctieu = double.Parse(File.ReadLines(saveFilePath).First());
                SoHeoConSinhRa_muctieu = double.Parse(File.ReadLines(saveFilePath).Skip(1).Take(1).First());
                ODeItCon_muctieu = double.Parse(File.ReadLines(saveFilePath).Skip(2).Take(1).First());
                SoHeoConSong_MucTieu = double.Parse(File.ReadLines(saveFilePath).Skip(3).Take(1).First());
                SoHeoCaiSua_muctieu = double.Parse(File.ReadLines(saveFilePath).Skip(4).Take(1).First());
                SoConChetTruocKhiCaiSua_MucTieu = double.Parse(File.ReadLines(saveFilePath).Skip(5).Take(1).First());
                ThoiGianMangThai_MucTieu = double.Parse(File.ReadLines(saveFilePath).Skip(6).Take(1).First());
                SoNgayCaiSua_MucTieu = double.Parse(File.ReadLines(saveFilePath).Skip(7).Take(1).First());
                SoNgayKhongLamViec_MucTieu = File.ReadLines(saveFilePath).Skip(8).Take(1).First();
                TrungBnhLua_MucTieu = double.Parse(File.ReadLines(saveFilePath).Skip(9).Take(1).First());
                SoHeoTrongNam_MucTieu = double.Parse(File.ReadLines(saveFilePath).Skip(10).Take(1).First());
                TyLeThayDan_MucTieu = double.Parse(File.ReadLines(saveFilePath).Skip(11).Take(1).First());
            }
            else
            {
                setGiaTriStatic();
            }
        }
    }
}

