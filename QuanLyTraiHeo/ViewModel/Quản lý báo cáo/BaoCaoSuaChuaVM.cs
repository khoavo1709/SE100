using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;
using OfficeOpenXml;
using QuanLyTraiHeo.Model;
using QuanLyTraiHeo.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static QuanLyTraiHeo.ViewModel.BaoCaoTinhTrangHeoVM;
using LicenseContext = OfficeOpenXml.LicenseContext;
using SeriesCollection = LiveCharts.SeriesCollection;
using System.Windows.Controls;
using DataTable = System.Data.DataTable;
using OfficeOpenXml.Style;
using Microsoft.Office.Interop.Excel;

namespace QuanLyTraiHeo.ViewModel
{
    public class BaoCaoSuaChuaVM : BaseViewModel
    {
        #region Attributes
        private Func<ChartPoint, string> pointLabel;
        public ObservableCollection<PHIEUSUACHUA> lstPhieu;
        private DateTime? _NgaySuaChua1 = new DateTime();
        private DateTime? _NgaySuaChua2 = new DateTime();
        public int[] SoLuongChuongDaSua = new int[12];
        private SeriesCollection _SeriesCollectionTSheoChart;
        private ChartValues<int> _SoChuongBinhThuong = new ChartValues<int>();
        private ChartValues<int> _SoChuongDangSuaChua = new ChartValues<int>();
        private ChartValues<int> _SoChuongKhongSuDung = new ChartValues<int>();
        private ChartValues<int> _SoChuongDaHu = new ChartValues<int>();
        private int _BinhThuong = 0;
        private int _DangSuaChua = 0;
        private int _KhongSuDung = 0;
        private int _DaHu = 0;
        #endregion

        #region Property
        public Func<ChartPoint, string> PointLabel { get => pointLabel; set { pointLabel = value; OnPropertyChanged(); } }
        public ObservableCollection<PHIEUSUACHUA> LstPhieu { get => lstPhieu; set { lstPhieu = value; OnPropertyChanged(); } }
        public ChartValues<int> SoChuongBinhThuong { get => _SoChuongBinhThuong; set { _SoChuongBinhThuong = value; OnPropertyChanged(); } }
        public ChartValues<int> SoChuongDangSuaChua { get => _SoChuongDangSuaChua; set { _SoChuongDangSuaChua = value; OnPropertyChanged(); } }
        public ChartValues<int> SoChuongKhongSuDung { get => _SoChuongKhongSuDung; set { _SoChuongKhongSuDung = value; OnPropertyChanged(); } }
        public ChartValues<int> SoChuongDaHu { get => _SoChuongDaHu; set { _SoChuongDaHu = value; OnPropertyChanged(); } }
        public SeriesCollection SeriesCollectionTSheoChart { get => _SeriesCollectionTSheoChart; set { _SeriesCollectionTSheoChart = value; OnPropertyChanged(); } }
        public DateTime? NgaySuaChua1 { get => _NgaySuaChua1; set { _NgaySuaChua1 = value; OnPropertyChanged(); } }
        public DateTime? NgaySuaChua2 { get => _NgaySuaChua2; set { _NgaySuaChua2 = value; OnPropertyChanged(); } }
        public string[] LabelsTSheoChart { get; set; }
        #endregion

        #region Command
        public ICommand XuatFileExcelCommand { get; set; }
        public ICommand TimKiemTheoNgaySC1Command { get; set; }
        public ICommand TimKiemTheoNgaySC2Command { get; set; }
        public ICommand LaySoLuongChuongDaSuaTheoNam { get; set; }
        #endregion

        public BaoCaoSuaChuaVM()
        {
            TinhGiaTriBieuDo();
            LoadDT(2022);
            TimKiemTheoNgaySC1Command = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                _NgaySuaChua1 = p.SelectedDate;
                TimKiem();
            });
            LaySoLuongChuongDaSuaTheoNam = new RelayCommand<System.Windows.Controls.TextBox>((p) => { return true; }, p =>
            {
                if (!string.IsNullOrWhiteSpace(p.Text))
                {
                    LoadDT(Convert.ToInt32(p.Text));
                }
            });
            TimKiemTheoNgaySC2Command = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                _NgaySuaChua2 = p.SelectedDate;
                TimKiem();
            });
            LstPhieu = new ObservableCollection<PHIEUSUACHUA>(DataProvider.Ins.DB.PHIEUSUACHUAs);
            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            #region binding dữ liệu cho Biểu đồ tổng số chuồng trại đã sửa chửa
            //SeriesCollectionTSheoChart = new SeriesCollection
            //{
            //    new LineSeries
            //    {
            //        Title ="Chuồng đã sửa chữa",
            //        Values = new ChartValues<int>(SoLuongChuongDaSua)
            //    }
            //};
            LabelsTSheoChart = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            #endregion
            XuatFileExcelCommand = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                string filePath = "";
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Excel | *.xlsx";
                if (dialog.ShowDialog() == true)
                {
                    filePath = dialog.FileName;
                }
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("Đường dẫn không hợp lệ!");
                    return;
                }
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                try
                {
                    using (ExcelPackage excelPackage = new ExcelPackage())
                    {
                        excelPackage.Workbook.Properties.Author = "Dang Khoa";
                        excelPackage.Workbook.Properties.Title = "Báo cáo sửa chữa";
                        excelPackage.Workbook.Worksheets.Add("Báo cáo");
                        ExcelWorksheet ws = excelPackage.Workbook.Worksheets[0];
                        ws.Name = "Báo cáo";
                        ws.Cells.Style.Font.Size = 14;
                        ws.Cells.Style.Font.Name = "Times New Roman";
                        string[] arrColumnHeader = {
                            "Số phiếu",
                            "Ngày lập phiếu",
                            "Mã nhân viên",
                            "Mã đối tác",
                            "Ghi chú",
                            "Trạng thái",
                            "Tổng tiền"
                        };
                        var countColHeader = arrColumnHeader.Count();
                        ws.Cells[1, 1].Value = "Báo cáo sửa chữa";
                        ws.Cells[1, 1, 1, countColHeader].Merge = true;
                        ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                        ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        int colIndex = 1;
                        int rowIndex = 2;
                        foreach (var item in arrColumnHeader)
                        {
                            var cell = ws.Cells[rowIndex, colIndex];
                            var fill = cell.Style.Fill;
                            var x = cell.Style.WrapText;
                            fill.PatternType = ExcelFillStyle.Solid;
                            fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                            var border = cell.Style.Border;
                            border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                            cell.Value = item;
                            colIndex++;
                        }
                        foreach (var item in LstPhieu)
                        {
                            colIndex = 1;
                            rowIndex++;
                            ws.Cells[rowIndex, colIndex++].Value = item.SoPhieu.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.NgaySuaChua.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.MaNhanVien.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.MaDoiTac.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.GhiChu.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.TrangThai.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.TongTien.ToString();
                        }
                        Byte[] bin = excelPackage.GetAsByteArray();
                        File.WriteAllBytes(filePath, bin);
                    }
                    MessageBox.Show("Xuất file excel thành công!");
                }
                catch
                {
                    MessageBox.Show("Có lỗi xảy ra khi xuất file excel!");
                }
            });
        }
        public void LoadDT(int nam)
        {
            SeriesCollectionTSheoChart = new SeriesCollection();
            ChartValues<int> giatri = new ChartValues<int>();
            LineSeries c1 = new LineSeries
            {
                Title = "Chuồng đã sửa chữa",
            };
            ResetData();
            var temp1 = DataProvider.Ins.DB.PHIEUSUACHUAs.Where(x => x.NgaySuaChua.Value.Year == nam).ToList();
            for (int i = 0; i < 12; ++i)
            {
                var temp2 = temp1.Where(x => x.NgaySuaChua.Value.Month == (i + 1)).ToList();
                foreach (var y in temp2)
                {
                    SoLuongChuongDaSua[i] += y.CT_PHIEUSUACHUA.Count;
                }
                giatri.Add(SoLuongChuongDaSua[i]);
            }
            c1.Values = giatri;
            SeriesCollectionTSheoChart.Add(c1);
        }
        public void TinhGiaTriBieuDo()
        {
            var temp = DataProvider.Ins.DB.CHUONGTRAIs.ToList();
            foreach (var i in temp)
            {
                if (i.TinhTrang.Equals("Bình thường"))
                {
                    _BinhThuong++;
                    _SoChuongBinhThuong = new ChartValues<int>() { _BinhThuong };
                }
                if (i.TinhTrang.Equals("Đã hư hỏng"))
                {
                    _DaHu++;
                    _SoChuongDaHu = new ChartValues<int>() { _DaHu };
                }
                if (i.TinhTrang.Equals("Không sử dụng"))
                {
                    _KhongSuDung++;
                    _SoChuongKhongSuDung = new ChartValues<int>() { _KhongSuDung };
                }
                if (i.TinhTrang.Equals("Đang sửa chữa"))
                {
                    _DangSuaChua++;
                    _SoChuongDangSuaChua = new ChartValues<int>() { _DangSuaChua };
                }
            }
        }
        //public void LaySoLuongChuongDaSua(int nam)
        //{
        //    ResetData();
        //    var temp1 = DataProvider.Ins.DB.PHIEUSUACHUAs.Where(x => x.NgaySuaChua.Value.Year == nam).ToList();
        //    for (int i = 0; i < 12; ++i)
        //    {
        //        var temp2 = temp1.Where(x => x.NgaySuaChua.Value.Month == (i + 1)).ToList();
        //        foreach (var y in temp2)
        //        {
        //            SoLuongChuongDaSua[i] += y.CT_PHIEUSUACHUA.Count;
        //        }
        //    }
        //}
        void ResetData()
        {
            for (int i = 0; i < 12; i++)
            {
                SoLuongChuongDaSua[i] = 0;
            }
        }
        void TimKiem()
        {
            lstPhieu.Clear();
            var PhieuSuaChuas = DataProvider.Ins.DB.PHIEUSUACHUAs.ToList();
            if (_NgaySuaChua1 != null && _NgaySuaChua1 != DateTime.MinValue)
            {
                PhieuSuaChuas = PhieuSuaChuas.Where(x => x.NgaySuaChua >= _NgaySuaChua1).ToList();
            }
            if (_NgaySuaChua2 != null && _NgaySuaChua2 != DateTime.MinValue)
            {
                PhieuSuaChuas = PhieuSuaChuas.Where(x => x.NgaySuaChua <= _NgaySuaChua2).ToList();
            }
            foreach (var item in PhieuSuaChuas)
            {
                PHIEUSUACHUA pHIEUSUACHUA = new PHIEUSUACHUA();
                pHIEUSUACHUA = item;
                lstPhieu.Add(pHIEUSUACHUA);
            }
        }
    }

}
