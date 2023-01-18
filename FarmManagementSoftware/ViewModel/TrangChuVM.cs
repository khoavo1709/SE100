using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using FarmManagementSoftware.Model;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using static OfficeOpenXml.ExcelErrorValue;
using static FarmManagementSoftware.ViewModel.BaoCaoSuaChuaVM;

namespace FarmManagementSoftware.ViewModel
{
    public class TrangChuVM:BaseViewModel
    {
        private Func<ChartPoint, string> pointLabel;
        public Func<ChartPoint, string> PointLabel { get => pointLabel; set { pointLabel = value; OnPropertyChanged(); } }
        private Func<double, string> _formatFunc;
        public Func<double, string> formatFunc { get=> _formatFunc; set { _formatFunc = value; OnPropertyChanged(); } }

        public SeriesCollection SeriesCollectionDSCPChart { get; set; }
        public string[] LabelsDSCPChart { get; set; }
        public SeriesCollection SeriesCollectionNVChart { get; set; }
        public string[] LabelsNVChart { get; set; }

        private List<int> _listNamColumnChartDoanhThuChiTieu;
        public List<int> listNamColumnChartDoanhThuChiTieu { get => _listNamColumnChartDoanhThuChiTieu; set { _listNamColumnChartDoanhThuChiTieu = value; OnPropertyChanged(); } }

        private int _SoLuongHeoTot;
        public int SoLuongHeoTot { get => _SoLuongHeoTot; set { _SoLuongHeoTot = value; OnPropertyChanged(); }  }

        private float _PhanTramHeoTot;
        public float PhanTramHeoTot { get => _PhanTramHeoTot; set { _PhanTramHeoTot = value; OnPropertyChanged(); } }

        private int _SoLuongHeoBenhChet;
        public int SoLuongHeoBenhChet { get => _SoLuongHeoBenhChet; set { _SoLuongHeoBenhChet = value; OnPropertyChanged();} }

        private float _PhanTramHeoBenhChet;
        public float PhanTramHeoBenhChet { get => _PhanTramHeoBenhChet; set { _PhanTramHeoBenhChet = value; OnPropertyChanged();} }

        private int _SoLuongChuongHong;
        public int SoLuongChuongHong { get => _SoLuongChuongHong; set { _SoLuongChuongHong = value; OnPropertyChanged();} }

        private float _PhanTramChuongHong;
        public float PhanTramChuongHong { get => _PhanTramChuongHong; set { _PhanTramChuongHong = value; OnPropertyChanged();} }

        private int Doanhthutrongngay;
        private string _DoanhThuTrongNgay;
        public string DoanhThuTrongNgay { get => _DoanhThuTrongNgay; set { _DoanhThuTrongNgay = value; OnPropertyChanged();} }

        private float _TangTruongDoanhThu;
        public float TangTruongDoanhThu { get => _TangTruongDoanhThu; set { _TangTruongDoanhThu = value; OnPropertyChanged();} }

        private bool _IsTangDoanhThu;
        public bool IsTangDoanhThu { get => _IsTangDoanhThu; set { _IsTangDoanhThu = value; OnPropertyChanged();} }  

        private int Chitieutrongngay;
        private string _ChiTieuTrongNgay;
        public string ChiTieuTrongNgay { get => _ChiTieuTrongNgay; set { _ChiTieuTrongNgay = value; OnPropertyChanged();} }

        private float _SuyGiamChiPhi;
        public float SuyGiamChiPhi { get => _SuyGiamChiPhi; set { _SuyGiamChiPhi = value; OnPropertyChanged();} }

        private bool _IsGiamChiPhi;
        public bool IsGiamChiPhi { get => _IsGiamChiPhi; set { _IsGiamChiPhi = value; OnPropertyChanged(); } }

        private SeriesCollection _SeriesCoCauHeo;
        public SeriesCollection SeriesCoCauHeo { get => _SeriesCoCauHeo; set { _SeriesCoCauHeo = value; OnPropertyChanged();} }

        private SeriesCollection _SeriesCoCauChuong;
        public SeriesCollection SeriesCoCauChuong { get => _SeriesCoCauChuong; set { _SeriesCoCauChuong = value; OnPropertyChanged(); } }

        private int _selectedNamChartDoanhthuChiTieu;
        public int selectedNamChartDoanhthuChiTieu { get => _selectedNamChartDoanhthuChiTieu; set { _selectedNamChartDoanhthuChiTieu = value; OnPropertyChanged(); } }

        private List<HoatDong> _lstHoatDong;
        public List<HoatDong> lstHoatDong { get => _lstHoatDong; set { _lstHoatDong = value; OnPropertyChanged(); } }

        private List<string> _lstTBMuaBenh;
        public List<string> lstTBMuaBenh { get => _lstTBMuaBenh; set { _lstTBMuaBenh = value; OnPropertyChanged(); } }

        SnackbarMessageQueue _messageQueue;
        public SnackbarMessageQueue messageQueue { get => _messageQueue; set { _messageQueue = value; OnPropertyChanged(); } }

        public ICommand changeSelectedNamChartDoanhThu { get; set; }
        public ICommand LoadedCommand { get; set; }

        public TrangChuVM()
        {
            PointLabel = chartPoint =>
               string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            formatFunc = (x) => string.Format("{0:#,#}", x);

            SeriesCoCauHeo = new SeriesCollection();
            SeriesCoCauChuong = new SeriesCollection();
            SeriesCollectionDSCPChart = new SeriesCollection();
            SeriesCollectionNVChart = new SeriesCollection();
            listNamColumnChartDoanhThuChiTieu = new List<int>();
            lstHoatDong = new List<HoatDong>();
            IsTangDoanhThu = true;
            IsGiamChiPhi = true;
            selectedNamChartDoanhthuChiTieu = DateTime.Today.Year;
            lstTBMuaBenh = new List<string>();
            messageQueue = new SnackbarMessageQueue();

            #region Khởi tạo list năm cho ColumnChart Doanh thu chi tiêu
            var listNamColumnChartDoanhThuChiTieuTheoPhieuHeo = DataProvider.Ins.DB.PHIEUHEOs.Select(x => x.NgayLap.Value.Year).Distinct().ToList();
            var listNamColumnChartDoanhThuChiTieuTheoPhieuSuaChua = DataProvider.Ins.DB.PHIEUSUACHUAs.Select(x => x.NgaySuaChua.Value.Year).Distinct().ToList();
            var listNamColumnChartDoanhThuChiTieuTheoPhieuHangHoa = DataProvider.Ins.DB.PHIEUHANGHOAs.Select(x => x.NgayLap.Value.Year).Distinct().ToList();

            foreach(var item in listNamColumnChartDoanhThuChiTieuTheoPhieuHeo)
            {
                listNamColumnChartDoanhThuChiTieu.Add(item);
            }
            foreach (var item in listNamColumnChartDoanhThuChiTieuTheoPhieuSuaChua)
            {
                listNamColumnChartDoanhThuChiTieu.Add(item);
            }
            foreach (var item in listNamColumnChartDoanhThuChiTieuTheoPhieuHangHoa)
            {
                listNamColumnChartDoanhThuChiTieu.Add(item);
            }

            listNamColumnChartDoanhThuChiTieu = listNamColumnChartDoanhThuChiTieu.Distinct().ToList();

            #endregion

            LoadDSThongSo();
            loadPieChartCoCauHeo();
            loadPieChartCoCauChuong();
            loadLineChartDoanhThuChiTieu();
            loadColumnChartNhanVien();
            LoadThongBaoMuaBenh();
            

            //#region Binding dữ liệu lên biểu đồ doanh thu và chi phí trong ngày
            //SeriesCollectionDSCPChart = new SeriesCollection
            //{
            //    new LineSeries
            //    {
            //        Title ="Doanh thu",
            //        Values = new ChartValues<double> { 35, 23, 41, 55 ,48, 62, 63, 41, 55, 44, 41, 33 }
            //    },
            //    new LineSeries
            //    {
            //        Title ="Chi phí",
            //        Values = new ChartValues<double> { 13, 12, 11, 15 ,14, 22, 13, 11, 25, 24, 14, 13 }
            //    }
            //};

            //LabelsDSCPChart = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            //#endregion

            //#region binding dữ liệu cho danh sách hoạt động
            //lstHoatDong.Add(new HoatDong() { icon = "Warehouse", TenNhanVien = "Trần Trung Thành", Mota = "Thực hiện một phiếu nhập kho trị giá 3,000,000 VND", MaPhieu = "SP01" });
            //lstHoatDong.Add(new HoatDong() { icon = "Warehouse", TenNhanVien = "", Mota = "", MaPhieu = "" });
            //#endregion

            #region command change SelectedNamChartDoanhThuChiPhi
            changeSelectedNamChartDoanhThu = new RelayCommand<Window>((p) => { return true; }, p => {
                loadLineChartDoanhThuChiTieu();
            });

            LoadedCommand = new RelayCommand<Snackbar>((p) => { return true; }, p => {
                
                foreach (var s in lstTBMuaBenh)
                {
                    messageQueue.Enqueue(
                    s,
                    "Tôi biết rồi",
                    param => Trace.WriteLine("Actioned: " + param),
                    s);
                }
            });
            #endregion
        }
        public void LoadTrangChu()
        {
            PointLabel = chartPoint =>
               string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            Func<double, string> formatFunc = (x) => string.Format("{0:0.000}", x);

            SeriesCoCauHeo = new SeriesCollection();
            SeriesCoCauChuong = new SeriesCollection();
            SeriesCollectionDSCPChart = new SeriesCollection();
            SeriesCollectionNVChart = new SeriesCollection();
            listNamColumnChartDoanhThuChiTieu = new List<int>();
            lstHoatDong = new List<HoatDong>();
            IsTangDoanhThu = true;
            IsGiamChiPhi = true;
            selectedNamChartDoanhthuChiTieu = DateTime.Today.Year;

            #region Khởi tạo list năm cho ColumnChart Doanh thu chi tiêu
            var listNamColumnChartDoanhThuChiTieuTheoPhieuHeo = DataProvider.Ins.DB.PHIEUHEOs.Select(x => x.NgayLap.Value.Year).Distinct().ToList();
            var listNamColumnChartDoanhThuChiTieuTheoPhieuSuaChua = DataProvider.Ins.DB.PHIEUSUACHUAs.Select(x => x.NgaySuaChua.Value.Year).Distinct().ToList();
            var listNamColumnChartDoanhThuChiTieuTheoPhieuHangHoa = DataProvider.Ins.DB.PHIEUHANGHOAs.Select(x => x.NgayLap.Value.Year).Distinct().ToList();

            foreach (var item in listNamColumnChartDoanhThuChiTieuTheoPhieuHeo)
            {
                listNamColumnChartDoanhThuChiTieu.Add(item);
            }
            foreach (var item in listNamColumnChartDoanhThuChiTieuTheoPhieuSuaChua)
            {
                listNamColumnChartDoanhThuChiTieu.Add(item);
            }
            foreach (var item in listNamColumnChartDoanhThuChiTieuTheoPhieuHangHoa)
            {
                listNamColumnChartDoanhThuChiTieu.Add(item);
            }

            listNamColumnChartDoanhThuChiTieu = listNamColumnChartDoanhThuChiTieu.Distinct().ToList();

            #endregion
            LoadDSThongSo();
            loadPieChartCoCauHeo();
            loadPieChartCoCauChuong();
            loadLineChartDoanhThuChiTieu();
            loadColumnChartNhanVien();
        }

        void LoadThongBaoMuaBenh()
        {
            lstTBMuaBenh.Clear();
            var Muabenhs = DataProvider.Ins.DB.MuaDichBenhs.Where(x => x.NgayBatDau <= DateTime.Now && DateTime.Now <= x.NgayKetThuc).ToList();
            foreach(var muabenh in Muabenhs)
            {
                string tb = "Hiện giờ mùa bệnh " + muabenh.TenDichBenh + " đã tới! Hãy đến xem cách phòng tránh trong quy định mùa bệnh";
                lstTBMuaBenh.Add(tb);
            }
            lstTBMuaBenh.Add("c");
        }

        void loadSoLuongHeoTot()
        {
            SoLuongHeoTot = DataProvider.Ins.DB.HEOs.Where(x => x.TinhTrang == "Sức khoẻ tốt").Count();
        }
        void loadPhanTramHeoTot()
        {
            var TongSoheo = DataProvider.Ins.DB.HEOs.Count();
            if(TongSoheo > 0)
                PhanTramHeoTot = SoLuongHeoTot * 100 / TongSoheo;
            else
                PhanTramHeoTot = 0;
        }
        void loadSoLuongHeoBenhChet()
        {
            SoLuongHeoBenhChet = DataProvider.Ins.DB.HEOs.Where(x => x.TinhTrang == "Đang bị bệnh" || x.TinhTrang == "Đã chết").Count();
        }
        void loadPhanTramHeoBenhChet()
        {
            var TongSoheo = DataProvider.Ins.DB.HEOs.Count();
            if(TongSoheo > 0)
                PhanTramHeoBenhChet = SoLuongHeoBenhChet*100 / TongSoheo;
            else
                PhanTramHeoBenhChet = 0;
        }
        void loadSoLuongChuongHong()
        {
            SoLuongChuongHong = DataProvider.Ins.DB.CHUONGTRAIs.Where(x => x.TinhTrang=="Đang hư hỏng").Count();
        }
        void loadPhanTramHeoChuongHong()
        {
            var TongSoChuong = DataProvider.Ins.DB.CHUONGTRAIs.Count();
            if (TongSoChuong > 0)
                _PhanTramChuongHong = SoLuongChuongHong * 100 / TongSoChuong;
            else _PhanTramChuongHong = 0;
        }
        void LoadDoanhThuTrongNgay()
        {
            Doanhthutrongngay = 0;
            DateTime d1 = DateTime.Today;
            DateTime d2 = DateTime.Today.AddDays(1);
            try
            {
                Doanhthutrongngay = int.Parse(DataProvider.Ins.DB.PHIEUHEOs.Where(x => x.LoaiPhieu == "Phiếu xuất heo" && d1 <= x.NgayLap && x.NgayLap < d2 && x.TrangThai == "Đã hoàn thành").Sum(x => x.TongTien).ToString());
            }
            catch(Exception e) { }
            try
            {
                Doanhthutrongngay += int.Parse(DataProvider.Ins.DB.PHIEUHANGHOAs.Where(x => x.LoaiPhieu == "Phiếu xuất ngoại" && d1 <= x.NgayLap && x.NgayLap < d2 && x.TrangThai == "Đã hoàn thành").Sum(x => x.TongTien).ToString());
            }
            catch(Exception e) { }
            DoanhThuTrongNgay = String.Format("{0:#,##0}", Doanhthutrongngay);
        }
        void loadTangTruongDoanhThu()
        {
            DateTime homqua = DateTime.Today.AddDays(-1);
            int DoanhThuHomQua = 0;
            DateTime d1 = homqua;
            DateTime d2 = homqua.AddDays(1);
            try
            {
                DoanhThuHomQua = int.Parse(DataProvider.Ins.DB.PHIEUHEOs.Where(x => x.LoaiPhieu == "Phiếu xuất heo" && d1 <= x.NgayLap && x.NgayLap < d2 && x.TrangThai == "Đã hoàn thành").Sum(x => x.TongTien).ToString());
            }
            catch(Exception e) { }
            try
            {
                DoanhThuHomQua += int.Parse(DataProvider.Ins.DB.PHIEUHANGHOAs.Where(x => x.LoaiPhieu == "Phiếu xuất ngoại" && d1 <= x.NgayLap && x.NgayLap < d2 && x.TrangThai == "Đã hoàn thành").Sum(x => x.TongTien).ToString());
            }
            catch (Exception e) { }
            if (DoanhThuHomQua > Doanhthutrongngay) IsTangDoanhThu = false;
            else IsTangDoanhThu = true;
            if (DoanhThuHomQua > 0)
                TangTruongDoanhThu = Math.Abs((Doanhthutrongngay - DoanhThuHomQua)*100/DoanhThuHomQua);
            else TangTruongDoanhThu = 100;
        }
        void loadChiPhiTrongNgay()
        {
            Chitieutrongngay = 0;
            DateTime d1 = DateTime.Today;
            DateTime d2 = DateTime.Today.AddDays(1);
            try
            {
                Chitieutrongngay = int.Parse(DataProvider.Ins.DB.PHIEUHEOs.Where(x => x.LoaiPhieu == "Phiếu nhập heo" && d1 <= x.NgayLap && x.NgayLap < d2 && x.TrangThai == "Đã hoàn thành").Sum(x => x.TongTien).GetValueOrDefault().ToString());
            }
            catch(Exception e) { }
            try
            {
                Chitieutrongngay += int.Parse(DataProvider.Ins.DB.PHIEUHANGHOAs.Where(x => x.LoaiPhieu == "Phiếu nhập kho" && d1<=x.NgayLap && x.NgayLap<d2 && x.TrangThai == "Đã hoàn thành").Sum(x => x.TongTien).ToString());
            }
            catch (Exception e) { }
            try
            {
                Chitieutrongngay += int.Parse(DataProvider.Ins.DB.PHIEUSUACHUAs.Where(x => d1 <= x.NgaySuaChua && x.NgaySuaChua < d2 && x.TrangThai == "Đã hoàn thành").Sum(x => x.TongTien).ToString());
            }
            catch (Exception e) { }
            ChiTieuTrongNgay = String.Format("{0:#,##0}", Chitieutrongngay);
        }
        void loadSuyGiamChiPhi()
        {
            DateTime homqua = DateTime.Today.AddDays(-1);
            DateTime d1 = homqua;
            DateTime d2 = homqua.AddDays(1);
            int ChiPhiHomQua = 0;
            try
            {
                ChiPhiHomQua = int.Parse(DataProvider.Ins.DB.PHIEUHEOs.Where(x => x.LoaiPhieu == "Phiếu xuất heo" && d1 <= x.NgayLap && x.NgayLap < d2 && x.TrangThai == "Đã hoàn thành").Sum(x => x.TongTien).ToString());
            }
            catch (Exception e) { }
            try
            {
                ChiPhiHomQua += int.Parse(DataProvider.Ins.DB.PHIEUHANGHOAs.Where(x => x.LoaiPhieu == "Phiếu xuất ngoại" && d1 <= x.NgayLap && x.NgayLap < d2 && x.TrangThai == "Đã hoàn thành").Sum(x => x.TongTien).ToString());
            }
            catch(Exception e) { }
            if (ChiPhiHomQua < Chitieutrongngay) IsGiamChiPhi = false;
            else IsGiamChiPhi = true;
            if (ChiPhiHomQua>0)
                SuyGiamChiPhi = Math.Abs((Chitieutrongngay - ChiPhiHomQua) * 100 / ChiPhiHomQua);
            else 
                SuyGiamChiPhi = 100;
        }
        void LoadDSThongSo() 
        {
            loadSoLuongHeoTot();
            loadPhanTramHeoTot();
            loadSoLuongHeoBenhChet();
            loadPhanTramHeoBenhChet();
            loadSoLuongChuongHong();
            loadPhanTramHeoChuongHong();
            LoadDoanhThuTrongNgay();
            loadTangTruongDoanhThu();
            loadChiPhiTrongNgay();
            loadSuyGiamChiPhi();
            loadDSHoatDong();
        }
        void loadPieChartCoCauHeo()
        {
            SeriesCoCauHeo.Clear();
            var listLoaiHeo = DataProvider.Ins.DB.HEOs.GroupBy(x => x.LOAIHEO.TenLoaiHeo).ToList();
            foreach(var item in listLoaiHeo)
            {
                var pieseries = new PieSeries();
                pieseries.Title = item.Key.ToString();
                pieseries.Values = new ChartValues<ObservableValue> { new ObservableValue(item.Count()) };
                pieseries.DataLabels = true;
                pieseries.LabelPoint = PointLabel;

                SeriesCoCauHeo.Add(pieseries);
            }
        }
        void loadPieChartCoCauChuong()
        {
            SeriesCoCauChuong.Clear();
            var listChuongNuoi = DataProvider.Ins.DB.CHUONGTRAIs.GroupBy(x => x.TinhTrang).ToList();
            foreach (var item in listChuongNuoi)
            {
                var pieseries = new PieSeries();
                pieseries.Title = item.Key.ToString();
                pieseries.Values = new ChartValues<ObservableValue> { new ObservableValue(item.Count()) };
                pieseries.DataLabels = true;
                pieseries.LabelPoint = PointLabel;

                SeriesCoCauChuong.Add(pieseries);
            }
        }
        void loadLineChartDoanhThuChiTieu()
        {
            SeriesCollectionDSCPChart.Clear();

            // cài đặt doanh thu
            var lineDoanhThu = new LineSeries();
            lineDoanhThu.Title = "Doanh thu";
            lineDoanhThu.Values = new ChartValues<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            SeriesCollectionDSCPChart.Add(lineDoanhThu);

            var listDoanhThuTheoPhieuHeo = DataProvider.Ins.DB.PHIEUHEOs.Where(x => x.LoaiPhieu == "Phiếu xuất heo" && x.NgayLap.Value.Year == selectedNamChartDoanhthuChiTieu && x.TrangThai == "Đã hoàn thành").GroupBy(x => x.NgayLap.Value.Month).ToList();
            foreach(var item in listDoanhThuTheoPhieuHeo)
            {
                double Tongtien = double.Parse(lineDoanhThu.Values[item.Key - 1].ToString()) + double.Parse(item.Sum(x => x.TongTien).ToString());
                lineDoanhThu.Values[item.Key - 1] = Tongtien;
            }
            var listDoanhThuTheoPhieuHangHoa = DataProvider.Ins.DB.PHIEUHANGHOAs.Where(x => x.LoaiPhieu == "Phiếu xuất ngoại" && x.NgayLap.Value.Year == selectedNamChartDoanhthuChiTieu && x.TrangThai == "Đã hoàn thành").GroupBy(x => x.NgayLap.Value.Month).ToList();
            foreach (var item in listDoanhThuTheoPhieuHangHoa)
            {
                double Tongtien = double.Parse(lineDoanhThu.Values[item.Key - 1].ToString()) + double.Parse(item.Sum(x => x.TongTien).ToString());
                lineDoanhThu.Values[item.Key - 1] = Tongtien;
            }
            var listDoanhThuTheoPhieuSuaChua = DataProvider.Ins.DB.PHIEUSUACHUAs.Where(x => x.NgaySuaChua.Value.Year == selectedNamChartDoanhthuChiTieu && x.TrangThai == "Đã hoàn thành").GroupBy(x => x.NgaySuaChua.Value.Month).ToList();
            foreach(var item in listDoanhThuTheoPhieuSuaChua)
            {
                double Tongtien = double.Parse(lineDoanhThu.Values[item.Key - 1].ToString()) + double.Parse(item.Sum(x => x.TongTien).ToString());
                lineDoanhThu.Values[item.Key - 1] = Tongtien;
            }

            //cài đặt chi tiêu
            var lineChiTieu = new LineSeries();
            lineChiTieu.Title = "Chi tiêu";
            lineChiTieu.Values = new ChartValues<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            SeriesCollectionDSCPChart.Add(lineChiTieu);

            var listChiTieuTheoPhieuHeo = DataProvider.Ins.DB.PHIEUHEOs.Where(x => x.LoaiPhieu == "Phiếu nhập heo" && x.NgayLap.Value.Year == DateTime.Today.Year && x.TrangThai == "Đã hoàn thành").GroupBy(x => x.NgayLap.Value.Month).ToList();
            foreach (var item in listChiTieuTheoPhieuHeo)
            {
                double Tongtien = double.Parse(lineChiTieu.Values[item.Key - 1].ToString()) + double.Parse(item.Sum(x => x.TongTien).ToString());
                lineChiTieu.Values[item.Key - 1] = Tongtien;
            }
            var listChiTieuTheoPhieuHangHoa = DataProvider.Ins.DB.PHIEUHANGHOAs.Where(x => x.LoaiPhieu == "Phiếu nhập kho" && x.NgayLap.Value.Year == DateTime.Today.Year && x.TrangThai == "Đã hoàn thành").GroupBy(x => x.NgayLap.Value.Month).ToList();
            foreach (var item in listChiTieuTheoPhieuHangHoa)
            {
                double Tongtien = double.Parse(lineChiTieu.Values[item.Key - 1].ToString()) + double.Parse(item.Sum(x => x.TongTien).ToString());
                lineChiTieu.Values[item.Key - 1] = Tongtien;
            }

            LabelsDSCPChart = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };

        }
        void loadColumnChartNhanVien()
        {
            SeriesCollectionNVChart.Clear();
            var listNhanVien = DataProvider.Ins.DB.NHANVIENs.GroupBy(x => x.CHUCVU.TenChucVu).ToList();
            var row = new RowSeries();
            LabelsNVChart = new string[listNhanVien.Count];
            row.Values = new ChartValues<double>();
            row.Title = "Số nhân viên";
            SeriesCollectionNVChart.Add(row);
            int i = 0;
            foreach (var item in listNhanVien)
            {
                double count = item.Count();
                row.Values.Add(count);
                LabelsNVChart[i] = item.Key.ToString();
                i++;
            }
        }
        void loadDSHoatDong()
        {
            lstHoatDong.Clear();
            DateTime d1 = DateTime.Today;
            DateTime d2 = DateTime.Today.AddDays(1);
            var lstHDHeo = DataProvider.Ins.DB.PHIEUHEOs.Where(x => d1 <= x.NgayLap && x.NgayLap < d2 && x.TrangThai == "Đã hoàn thành").ToList();
            foreach (var item in lstHDHeo)
            {
                var hoatdong = new HoatDong();
                hoatdong.icon = "PiggyBank";
                hoatdong.TenNhanVien = item.NHANVIEN.HoTen;
                hoatdong.Mota = "Đã thực hiện 1 phiếu " + item.LoaiPhieu+ " trị giá "+ string.Format("{0:#,##0}", item.TongTien) + " VND";
                hoatdong.MaPhieu = "Mã phiếu: " + item.SoPhieu;
                lstHoatDong.Add(hoatdong);
            }

            var lstHDSuaChua = DataProvider.Ins.DB.PHIEUSUACHUAs.Where(x => d1 <= x.NgaySuaChua && x.NgaySuaChua < d2 && x.TrangThai == "Đã hoàn thành").ToList();
            foreach (var item in lstHDSuaChua)
            {
                var hoatdong = new HoatDong();
                hoatdong.icon = "Wrench";
                hoatdong.TenNhanVien = item.NHANVIEN.HoTen;
                hoatdong.Mota = "Đã thực hiện 1 phiếu sửa chữa trị giá " + string.Format("{0:#,##0}", item.TongTien) + " VND";
                hoatdong.MaPhieu = "Mã phiếu: " + item.SoPhieu;
                lstHoatDong.Add(hoatdong);
            }

            var lstHDKho = DataProvider.Ins.DB.PHIEUHANGHOAs.Where(x => d1 <= x.NgayLap && x.NgayLap < d2 && x.TrangThai == "Đã hoàn thành").ToList();
            foreach(var item in lstHDKho)
            {
                var hoatdong = new HoatDong();
                hoatdong.icon = "Warehouse";
                hoatdong.TenNhanVien = item.NHANVIEN.HoTen;
                hoatdong.Mota = "Đã thực hiện 1 phiếu " + item.LoaiPhieu + " trị giá " + string.Format("{0:#,##0}", item.TongTien) + " VND";
                hoatdong.MaPhieu = "Mã phiếu: " + item.SoPhieu;
                lstHoatDong.Add(hoatdong);
            }
        }
        public class HoatDong
        {
            public string icon { get; set; }
            public string TenNhanVien { get; set; }
            public string Mota { get; set; }
            public string MaPhieu { get; set; }
        }
    }
}
