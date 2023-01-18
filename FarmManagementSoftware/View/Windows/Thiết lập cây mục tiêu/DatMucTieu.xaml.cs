﻿using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace FarmManagementSoftware.View.Windows.Thiết_lập_cây_mục_tiêu
{
    /// <summary>
    /// Interaction logic for DatMucTieu.xaml
    /// </summary>
    public partial class DatMucTieu : Window
    {

        //public double Tylede_muctieu_au = 86;
        //public double SoHeoConSinhRa_muctieu_au = 12.5;
        //public double ODeItCon_muctieu_au = 12;
        //public double SoHeoConSong_MucTieu_au = 11;
        //public double SoHeoCaiSua_muctieu_au = 9.5;
        //public double SoConChetTruocKhiCaiSua_MucTieu_au = 18;
        //public string ThoiGianMangThai_MucTieu_au = "110-117";
        //public string SoNgayCaiSua_MucTieu_au = "20-28";
        //public string SoNgayKhongLamViec_MucTieu_au = "12";
        //public double TrungBnhLua_MucTieu_au = 2.3;
        //public double SoHeoTrongNam_MucTieu_au = 22;
        //public double DoanhThu_muctieu_au = 0;
        //public double Tylethaydan_au = 0;

        //public int check = 0;
        public DatMucTieu()
        {
            InitializeComponent();
        }
        public DatMucTieu(double Tylede_muctieu, double SoHeoConSinhRa_muctieu, double ODeItCon_muctieu, double SoHeoConSong_MucTieu, double SoHeoCaiSua_muctieu, double SoConChetTruocKhiCaiSua_MucTieu, string ThoiGianMangThai_MucTieu, string SoNgayCaiSua_MucTieu, string SoNgayKhongLamViec_MucTieu, double TrungBnhLua_MucTieu, double SoHeoTrongNam_MucTieu)
        {
            InitializeComponent();
            //Tylede_muctieu_au = Tylede_muctieu;
            //SoHeoConSinhRa_muctieu_au = SoHeoConSinhRa_muctieu;
            //ODeItCon_muctieu_au = ODeItCon_muctieu;
            //SoHeoConSong_MucTieu_au = SoHeoConSong_MucTieu;
            //SoHeoCaiSua_muctieu_au = SoHeoCaiSua_muctieu;
            //SoConChetTruocKhiCaiSua_MucTieu_au = SoConChetTruocKhiCaiSua_MucTieu;
            //ThoiGianMangThai_MucTieu_au = ThoiGianMangThai_MucTieu;
            //SoNgayCaiSua_MucTieu_au = SoNgayCaiSua_MucTieu;
            //SoNgayKhongLamViec_MucTieu_au = SoNgayKhongLamViec_MucTieu;
            //TrungBnhLua_MucTieu_au = TrungBnhLua_MucTieu;
            //SoHeoTrongNam_MucTieu_au = SoHeoTrongNam_MucTieu;
            //muctieu12.Text = Tylede_muctieu.ToString();
            //muctieu2.Text = SoHeoConSinhRa_muctieu.ToString();
            //muctieu1.Text = ODeItCon_muctieu.ToString();
            //muctieu.Text = SoHeoConSong_MucTieu.ToString();
            //muctieu5.Text = SoHeoCaiSua_muctieu.ToString();
            //muctieu4.Text = SoConChetTruocKhiCaiSua_MucTieu.ToString();
            //muctieu8.Text = ThoiGianMangThai_MucTieu;
            //muctieu7.Text = SoNgayCaiSua_MucTieu;
            //muctieu9.Text = SoNgayKhongLamViec_MucTieu;
            //muctieu6.Text = TrungBnhLua_MucTieu.ToString();
            //muctieu10.Text = SoHeoTrongNam_MucTieu.ToString();
            //muctieu3.Text = Tylethaydan_au.ToString();
            //muctieu11.Text = DoanhThu_muctieu_au.ToString();
        }
        Regex regex = new Regex("[^0-9.]+");
        private void CheckIsNumber(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regex.IsMatch(e.Text);
        }

        //public ThamSo ReturnValue(ThamSo TS)
        //{
        //    if (check == 1)
        //    {
        //        TS.Tylede_muctieuClone = Tylede_muctieu_au;
        //        TS.SoHeoConSinhRa_muctieuClone = SoHeoConSinhRa_muctieu_au;
        //        TS.ODeItCon_muctieuClone = ODeItCon_muctieu_au;
        //        TS.SoHeoConSong_MucTieuClone = SoHeoConSong_MucTieu_au;
        //        TS.SoHeoCaiSua_muctieuClone = SoHeoCaiSua_muctieu_au;
        //        TS.SoConChetTruocKhiCaiSua_MucTieuClone = SoConChetTruocKhiCaiSua_MucTieu_au;
        //        TS.ThoiGianMangThai_MucTieuClone = ThoiGianMangThai_MucTieu_au;
        //        TS.SoNgayCaiSua_MucTieuClone = SoNgayCaiSua_MucTieu_au;
        //        TS.SoNgayKhongLamViec_MucTieuClone = SoNgayKhongLamViec_MucTieu_au;
        //        TS.TrungBnhLua_MucTieuClone = TrungBnhLua_MucTieu_au;
        //        TS.SoHeoTrongNam_MucTieuClone = SoHeoTrongNam_MucTieu_au;
        //        return TS; 
        //    }
        //    return null;
        //}

        //void setValue()
        //{
        //    Tylede_muctieu_au = double.Parse(muctieu12.Text);
        //    SoHeoConSinhRa_muctieu_au = Convert.ToDouble(muctieu2.Text);
        //    ODeItCon_muctieu_au = Convert.ToDouble(muctieu1.Text);
        //    SoHeoConSong_MucTieu_au = Convert.ToDouble(muctieu.Text);
        //    SoHeoCaiSua_muctieu_au = Convert.ToDouble(muctieu5.Text);
        //    SoConChetTruocKhiCaiSua_MucTieu_au = Convert.ToDouble(muctieu4.Text);
        //    ThoiGianMangThai_MucTieu_au = muctieu8.Text;
        //    SoNgayCaiSua_MucTieu_au = muctieu7.Text;
        //    SoNgayKhongLamViec_MucTieu_au = muctieu9.Text;
        //    TrungBnhLua_MucTieu_au = Convert.ToDouble(muctieu6.Text);
        //    SoHeoTrongNam_MucTieu_au = Convert.ToDouble(muctieu10.Text);
        //}

        //private void Confirm_button_Click(object sender, RoutedEventArgs e)
        //{
        //    check = 1;
        //    setValue();
        //    this.Close();
        //}

        //private void Huy_button_Click(object sender, RoutedEventArgs e)
        //{
        //    check = 0;
        //    this.Close();
        //}
    }
}
