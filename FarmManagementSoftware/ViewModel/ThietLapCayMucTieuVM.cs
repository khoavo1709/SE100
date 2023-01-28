using AutoMapper;
using MaterialDesignThemes.Wpf;
using OfficeOpenXml.ConditionalFormatting;
using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FarmManagementSoftware.ViewModel
{
    public class ThietLapCayMucTieuVM:BaseViewModel
    {
        private int _NamBD;
        public int NamBD { get=>_NamBD; set { _NamBD = value; OnPropertyChanged(); } }
        private List<int> _lstNamBD;
        public List<int> lstNamBD { get => _lstNamBD; set { _lstNamBD = value; OnPropertyChanged(); } }

        private int _NamKT;
        public int NamKT { get => _NamKT; set { _NamKT = value; OnPropertyChanged(); } }
        private List<int> _lstNamKT;
        public List<int> lstNamKT { get => _lstNamKT; set { _lstNamKT = value; OnPropertyChanged(); } }

        private double _Doanhthu;
        public double Doanhthu { get => _Doanhthu; set { _Doanhthu = value; OnPropertyChanged(); } }
        private bool _isDoanhthu;
        public bool isDoanhthu { get => _isDoanhthu; set { _isDoanhthu = value; OnPropertyChanged(); } }

        private double _soheoconCaisuaNam;
        public double soheoconCaisuaNam { get => _soheoconCaisuaNam; set { _soheoconCaisuaNam = value; OnPropertyChanged(); } }
        private bool _issoheoconCaisuaNam;
        public bool issoheoconCaisuaNam { get => _issoheoconCaisuaNam; set { _issoheoconCaisuaNam = value; OnPropertyChanged(); } }

        private double _TBLuaNam;
        public double TBLuaNam { get => _TBLuaNam; set { _TBLuaNam = value; OnPropertyChanged(); } }
        private bool _isTBLuaNam;
        public bool isTBLuaNam { get => _isTBLuaNam; set { _isTBLuaNam = value; OnPropertyChanged(); } }

        private double _soheoconCaisua;
        public double soheoconCaisua { get => _soheoconCaisua; set { _soheoconCaisua = value; OnPropertyChanged(); } }
        private bool _issoheoconCaisua;
        public bool issoheoconCaisua { get => _issoheoconCaisua; set { _issoheoconCaisua = value; OnPropertyChanged(); } }

        private double _songayheonaikolamviec;
        public double songayheonaikolamviec { get => _songayheonaikolamviec; set { _songayheonaikolamviec = value; OnPropertyChanged(); } }
        private bool _issongayheonaikolamviec;
        public bool issongayheonaikolamviec { get => _issongayheonaikolamviec; set { _issongayheonaikolamviec = value; OnPropertyChanged(); } }

        private double _thoigianmangthai;
        public double thoigianmangthai { get => _thoigianmangthai; set { _thoigianmangthai = value; OnPropertyChanged(); } }
        private bool _isthoigianmangthai;
        public bool isthoigianmangthai { get => _isthoigianmangthai; set { _isthoigianmangthai = value; OnPropertyChanged(); } }

        private double _thoigianchobu;
        public double thoigianchobu { get => _thoigianchobu; set { _thoigianchobu = value; OnPropertyChanged(); } }
        private bool _isthoigianchobu;
        public bool isthoigianchobu { get => _isthoigianchobu; set { _isthoigianchobu = value; OnPropertyChanged(); } }

        private double _soheoconsong;
        public double soheoconsong { get => _soheoconsong; set { _soheoconsong = value; OnPropertyChanged(); } }
        private bool _issoheoconsong;
        public bool issoheoconsong { get => _issoheoconsong; set { _issoheoconsong = value; OnPropertyChanged(); } }

        private double _soheochet;
        public double soheochet { get => _soheochet; set { _soheochet = value; OnPropertyChanged(); } }
        private bool _issoheochet;
        public bool issoheochet { get => _issoheochet; set { _issoheochet = value; OnPropertyChanged(); } }

        private double _soheosinhra;
        public double soheosinhra { get => _soheosinhra; set { _soheosinhra = value; OnPropertyChanged(); } }
        private bool _issoheosinhra;
        public bool issoheosinhra { get => _issoheosinhra; set { _issoheosinhra = value; OnPropertyChanged(); } }

        private double _odeitcon;
        public double odeitcon { get => _odeitcon; set { _odeitcon = value; OnPropertyChanged(); } }
        private bool _isodeitcon;
        public bool isodeitcon { get => _isodeitcon; set { _isodeitcon = value; OnPropertyChanged(); } }

        private double _tylede;
        public double tylede { get => _tylede; set { _tylede = value; OnPropertyChanged(); } }
        private bool _istylede;
        public bool istylede { get => _istylede; set { _istylede = value; OnPropertyChanged(); } }

        private float _tylethaydan;
        public float tylethaydan { get => _tylethaydan; set { _tylethaydan = value; OnPropertyChanged(); } }
        private bool _istylethaydan;
        public bool istylethaydan { get => _istylethaydan; set { _istylethaydan = value; OnPropertyChanged(); } }

        private THAMSOCMT _thamso;
        public THAMSOCMT thamso { get => _thamso; set { _thamso = value; OnPropertyChanged(); } }

        private int SoHeo = 0;
        private int SoHeoDuc = 0;
        private int SoHeoNai = 0;
        private double SoConDe = 0;
        private int SoLuaDe = 0;
        private int SoHeoDe = 0;
        private int SoODeItCon = 0;
        private int SoConDeThucTe = 0;
        private double SoConDeThucTe_TB = 0;
        private int SoConCaiSua = 0;
        private double SoConCaiSua_TB = 0;
        private int Songaymangthai = 0;
        private double Songaymangthai_tb = 0;
        private int Songaycaisuanha = 0;
        private double Songaycaisua_tb = 0;
        private int Songaychophoigiong = 0;
        private double Songaychophoigiong_tb = 0;
        private double SoLuaDeTrongMotNam = 0;
        private double SoConHeoSuaTrongMotNam = 0;

        public ICommand LocCommand { get; set; }
        public ICommand DatMucTieuCommand { get; set; }

        public ThietLapCayMucTieuVM()
        {
            lstNamBD = new List<int>();
            lstNamKT = new List<int>();
            var lsrnam = DataProvider.Ins.DB.LICHPHOIGIONGs.Select(x=>x.NgayPhoiGiong.Value.Year).Distinct().ToList();
            foreach(var lsr in lsrnam)
            {
                int nam = lsr;
                lstNamBD.Add(nam);
                lstNamKT.Add(nam);
            }
            NamBD = DateTime.Now.Year;
            NamKT = DateTime.Now.Year;

            thamso = new THAMSOCMT();
            loadCMT();

            #region command bộ lọc
            LocCommand =  new RelayCommand<Window>((p) => { return true; }, p => {
                clearCMT();
                loadCMT();
            });
            #endregion

            #region command đặt mục tiêu
                DatMucTieuCommand = new RelayCommand<Window>((p) => { return true; }, p => {
                DatMucTieu wc = new DatMucTieu();
                DatMucTieuVM vm = new DatMucTieuVM(thamso);
                wc.DataContext = vm;
                wc.ShowDialog();
                clearCMT();
                loadCMT();
            });
            #endregion


        }
        void clearCMT()
        {
             SoHeo = 0;
             SoHeoDuc = 0;
             SoHeoNai = 0;
             SoConDe = 0;
             SoLuaDe = 0;
             SoHeoDe = 0;
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
        void loadCMT()
        {
            CT_PHIEUHEO test;
            CT_PHIEUHEO test1;

            foreach (var t in DataProvider.Ins.DB.LICHPHOIGIONGs)
            {
                //test = (CT_PHIEUHEO)t.HEO.CT_PHIEUHEO;
                //test1 = (CT_PHIEUHEO)t.HEO1.CT_PHIEUHEO;
                if (t.NgayPhoiGiong != null && t.NgayPhoiGiong.Value.Year >= NamBD && t.NgayPhoiGiong.Value.Year <= NamKT)
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
                TinhDoanhThu();
            }
            catch (Exception)
            {
                //Tylede_main.Content = Math.Round(tyle, 2) + "%";
                //Tylede_muctieu_textbox.Content = "Mục tiêu: " + Tylede_muctieu + "%";

            }
        }

        void Tylede()
        {
            float tyle;
            if (SoHeoNai != 0)
            { tyle = ((float)SoHeoDe / SoHeoNai) * 100; }
            else
            { tyle = 0; }
            tylede = Math.Round(tyle, 2);
            if (tylede >= thamso.Tylede_muctieu)
                istylede = true;
            else istylede = false;
        }

        void SoHeoDe_lua()
        {
            if (SoLuaDe != 0)
            { soheosinhra = (double)SoConDe / SoLuaDe; }
            else
            { soheosinhra = 0; }
            if (soheosinhra >= thamso.SoHeoConSinhRa_muctieu)
                issoheosinhra = true;
            else issoheosinhra = false;
        }
        void TinhsocondeIt()
        {
            double tyle = 0;
            if (SoLuaDe != 0)
            { 
                tyle = ((double)SoODeItCon / SoLuaDe) * 100; 
            }
            else { tyle = 0; }
            odeitcon = Math.Round(tyle, 2);
            if (odeitcon <= thamso.ODeItCon_muctieu)
                isodeitcon = true;
            else isodeitcon = false;
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
            soheoconsong = Math.Round(SoConDeThucTe_TB, 2);
            if (soheoconsong >= thamso.SoHeoConSong_MucTieu)
                issoheoconsong = true;
            else issoheoconsong = false;
        }

        void SoHeoCaiSua()
        {
            if (SoLuaDe != 0)
            { SoConCaiSua_TB = (double)(SoConCaiSua / SoLuaDe); }
            else
            { SoConCaiSua_TB = 0; }
            soheoconCaisua = Math.Round(SoConCaiSua_TB, 2);
            if (soheoconCaisua >= thamso.SoHeoCaiSua_muctieu)
                issoheoconCaisua = true;
            else issoheoconCaisua = false;
        }

        void SoConChetTruocKhiCaiSua()
        {
            double tyle;
            double temp = SoConDeThucTe_TB - SoConCaiSua_TB;
            if (SoConDeThucTe_TB != 0)
            { tyle = (double)(temp / SoConDeThucTe_TB) * 100; }
            else { tyle = 0; }
            soheochet = Math.Round(tyle, 2);
            if (soheochet <= thamso.SoConChetTruocKhiCaiSua_MucTieu)
                issoheochet = true;
            else issoheochet = false;
        }

        void ThoiGianMangThai()
        {
            if (SoLuaDe != 0)
            { Songaymangthai_tb = ((double)Songaymangthai_tb / SoLuaDe); }
            else
            {
                Songaymangthai_tb = 0;
            }
            thoigianmangthai = Math.Round(Songaymangthai_tb, 2);
            if (thoigianmangthai >= thamso.ThoiGianMangThai_MucTieu_Min && thoigianmangthai <= thamso.ThoiGianMangThai_MucTieu_Max)
                isthoigianmangthai = true;
            else isthoigianmangthai = false;
        }

        void SoNgayCaiSua()
        {
            if (SoLuaDe != 0)
                Songaycaisua_tb = ((double)Songaycaisua_tb / SoLuaDe);
            else
                Songaycaisua_tb = 0;
            thoigianchobu = Math.Round(Songaycaisua_tb, 2);
            if (thoigianchobu >= thamso.SoNgayCaiSua_MucTieu_Min && thoigianchobu <= thamso.SoNgayCaiSua_MucTieu_Max)
                isthoigianchobu = true;
            else isthoigianchobu= false;
        }

        void SongayHeoNaiKhongLamViec()
        {
            if (SoLuaDe != 0)
                Songaychophoigiong_tb = ((double)Songaychophoigiong_tb / SoLuaDe);
            else
                Songaychophoigiong_tb = 0;
            songayheonaikolamviec = Math.Round(Songaychophoigiong_tb, 2);
            if (songayheonaikolamviec >= thamso.SoNgayKhongLamViec_MucTieu_Min && songayheonaikolamviec <= thamso.SoNgayKhongLamViec_MucTieu_Max)
                issongayheonaikolamviec = true;
            else issongayheonaikolamviec= false;
        }

        void Trungbinhlua()
        {
            if (Songaycaisua_tb + Songaychophoigiong_tb + Songaymangthai_tb != 0)
                SoLuaDeTrongMotNam = (365 / (double)(Songaycaisua_tb + Songaychophoigiong_tb + Songaymangthai_tb));
            else
                SoLuaDeTrongMotNam = 0;
            TBLuaNam = Math.Round(SoLuaDeTrongMotNam, 2);
            if (TBLuaNam >= thamso.TrungBnhLua_MucTieu)
                isTBLuaNam = true;
            else isTBLuaNam = false;
        }

        void SoHeoConTrongNam()
        {
            SoConHeoSuaTrongMotNam = (double)(SoConCaiSua_TB * SoLuaDeTrongMotNam);
            soheoconCaisuaNam = Math.Round(SoConHeoSuaTrongMotNam, 2);

            if (soheoconCaisuaNam >= thamso.SoHeoTrongNam_MucTieu)
                issoheoconCaisuaNam = true;
            else issoheoconCaisuaNam= false;
        }

        void TinhDoanhThu()
        {
            double Doanhthu1 = 0;
            
            try
            {
                Doanhthu1 = int.Parse(DataProvider.Ins.DB.PHIEUHEOs.Where(x => x.LoaiPhieu == "Phiếu xuất heo" && NamBD <= x.NgayLap.Value.Year && x.NgayLap.Value.Year <= NamKT && x.TrangThai == "Đã hoàn thành").Sum(x => x.TongTien).ToString());
            }
            catch (Exception e) { }
            try
            {
                Doanhthu1 += int.Parse(DataProvider.Ins.DB.PHIEUHANGHOAs.Where(x => x.LoaiPhieu == "Phiếu xuất ngoại" && NamBD <= x.NgayLap.Value.Year && x.NgayLap.Value.Year <= NamKT && x.TrangThai == "Đã hoàn thành").Sum(x => x.TongTien).ToString());
            }
            catch (Exception e) { }
            Doanhthu = Doanhthu1;
            if (Doanhthu >= thamso.Doanhthu_muctieu)
                isDoanhthu = true;
            else isDoanhthu = false;
        }
    }

    public class THAMSOCMT: BaseViewModel
    {
        private double _Doanhthu_muctieu;
        public double Doanhthu_muctieu { get=>_Doanhthu_muctieu; set { _Doanhthu_muctieu = value; OnPropertyChanged(); } }

        private double _Tylede_muctieu;
        public double Tylede_muctieu { get=> _Tylede_muctieu; set { _Tylede_muctieu = value; OnPropertyChanged(); } }

        private double _SoHeoConSinhRa_muctieu;
        public double SoHeoConSinhRa_muctieu { get => _SoHeoConSinhRa_muctieu; set { _SoHeoConSinhRa_muctieu = value; OnPropertyChanged(); } }

        private double _ODeItCon_muctieu;
        public double ODeItCon_muctieu { get => _ODeItCon_muctieu; set { _ODeItCon_muctieu = value; OnPropertyChanged(); } }

        private double _SoHeoConSong_MucTieu { get; set; }
        public double SoHeoConSong_MucTieu { get => _SoHeoConSong_MucTieu; set { _SoHeoConSong_MucTieu = value; OnPropertyChanged(); } }

        private double _SoHeoCaiSua_muctieu { get; set; }
        public double SoHeoCaiSua_muctieu { get => _SoHeoCaiSua_muctieu; set { _SoHeoCaiSua_muctieu = value; OnPropertyChanged(); } }

        private double _SoConChetTruocKhiCaiSua_MucTieu { get; set; }
        public double SoConChetTruocKhiCaiSua_MucTieu { get => _SoConChetTruocKhiCaiSua_MucTieu; set { _SoConChetTruocKhiCaiSua_MucTieu = value; OnPropertyChanged(); } }

        private double _ThoiGianMangThai_MucTieu_Min { get; set; }
        public double ThoiGianMangThai_MucTieu_Min { get => _ThoiGianMangThai_MucTieu_Min; set { _ThoiGianMangThai_MucTieu_Min = value; OnPropertyChanged(); } }

        private double _ThoiGianMangThai_MucTieu_Max { get; set; }
        public double ThoiGianMangThai_MucTieu_Max { get => _ThoiGianMangThai_MucTieu_Max; set { _ThoiGianMangThai_MucTieu_Max = value; OnPropertyChanged(); } }

        private double _SoNgayCaiSua_MucTieu_Min { get; set; }
        public double SoNgayCaiSua_MucTieu_Min { get => _SoNgayCaiSua_MucTieu_Min; set { _SoNgayCaiSua_MucTieu_Min = value; OnPropertyChanged(); } }

        private double _SoNgayCaiSua_MucTieu_Max { get; set; }
        public double SoNgayCaiSua_MucTieu_Max { get => _SoNgayCaiSua_MucTieu_Max; set { _SoNgayCaiSua_MucTieu_Max = value; OnPropertyChanged(); } }

        private double _SoNgayKhongLamViec_MucTieu_Min { get; set; }
        public double SoNgayKhongLamViec_MucTieu_Min { get => _SoNgayKhongLamViec_MucTieu_Min; set { _SoNgayKhongLamViec_MucTieu_Min = value; OnPropertyChanged(); } }

        private double _SoNgayKhongLamViec_MucTieu_Max { get; set; }
        public double SoNgayKhongLamViec_MucTieu_Max { get => _SoNgayKhongLamViec_MucTieu_Max; set { _SoNgayKhongLamViec_MucTieu_Max = value; OnPropertyChanged(); } }

        private double _TrungBnhLua_MucTieu { get; set; }
        public double TrungBnhLua_MucTieu { get => _TrungBnhLua_MucTieu; set { _TrungBnhLua_MucTieu = value; OnPropertyChanged(); } }

        private double _SoHeoTrongNam_MucTieu { get; set; }
        public double SoHeoTrongNam_MucTieu { get => _SoHeoTrongNam_MucTieu; set { _SoHeoTrongNam_MucTieu = value; OnPropertyChanged(); } }

        public THAMSOCMT()
        {
             Doanhthu_muctieu = 2500000000;
             Tylede_muctieu = 86;
             SoHeoConSinhRa_muctieu = 12.5;
             ODeItCon_muctieu = 12;
             SoHeoConSong_MucTieu = 11;
             SoHeoCaiSua_muctieu = 9.5;
             SoConChetTruocKhiCaiSua_MucTieu = 18;
             ThoiGianMangThai_MucTieu_Min = 110;
             ThoiGianMangThai_MucTieu_Max = 117;
             SoNgayCaiSua_MucTieu_Min = 20;
             SoNgayCaiSua_MucTieu_Max = 28;
             SoNgayKhongLamViec_MucTieu_Min = 12;
             SoNgayKhongLamViec_MucTieu_Max = 15;
             TrungBnhLua_MucTieu = 2.3;
             SoHeoTrongNam_MucTieu = 22;
        }
    }


}
