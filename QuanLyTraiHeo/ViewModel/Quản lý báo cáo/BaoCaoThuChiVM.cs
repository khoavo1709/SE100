using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using QuanLyTraiHeo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using OfficeOpenXml;
using MessageBox = System.Windows.MessageBox;
using OfficeOpenXml.Style;
using System.IO;

namespace QuanLyTraiHeo.ViewModel
{
    public class BaoCaoThuChiVM : BaseViewModel
    {
        #region Atributes
        private Func<ChartPoint, string> pointLabel;

        private int yearThuChiChart;

        private int monthChiChart;
        private int yearChiChart;

        private int monthThuChart;
        private int yearThuChart;

        private int monthDoiTacChart;
        private int yearDoiTacChart;

        private DateTime tuNgayChart;
        private DateTime denNgayChart;

        #endregion

        #region Properties
        public List<int> yearList { get; set; }
        public List<int> monthList { get; set; }
        public int MonthChiChart { get => monthChiChart; set { monthChiChart = value; OnPropertyChanged(); } }
        public int YearChiChart { get => yearChiChart; set { yearChiChart = value; OnPropertyChanged(); } }
        public int MonthThuChart { get => monthThuChart; set { monthThuChart = value; OnPropertyChanged(); } }
        public int YearThuChart { get => yearChiChart; set { yearChiChart += value; OnPropertyChanged(); } }
        public int MonthDoiTacChart { get => monthDoiTacChart; set { monthDoiTacChart = value; OnPropertyChanged(); } }
        public int YearDoiTacChart { get => yearDoiTacChart; set { yearDoiTacChart += value; OnPropertyChanged(); } }
        public int YearThuChiChart { get => yearThuChiChart; set { this.yearThuChiChart = value; OnPropertyChanged(); } }
        public DateTime TuNgayChart { get => tuNgayChart; set { tuNgayChart = value; OnPropertyChanged(); } }
        public DateTime DenNgayChart { get => denNgayChart; set { denNgayChart = value; OnPropertyChanged(); } }

        public ObservableCollection<PhieuThuChi> LstPhieuThuChi { get; }
        public ChartValues<ObservablePoint> DoanhThuGraphValues { get; }
        public ChartValues<ObservablePoint> ChiPhiGraphValues { get; }
        public SeriesCollection ThuChartViews { get; }
        public SeriesCollection ChiChartViews { get; }
        public SeriesCollection DoiTacChartViews { get; }

        #endregion

        #region Commands
        public ICommand ThuChiChartCommand { get; set; }
        public ICommand ThuChartCommand { get; set; }
        public ICommand ChiChartCommand { get; set; }
        public ICommand DoiTacChartCommand { get; set; }
        public ICommand ListBaoCaoCommand { get; set; }
        public ICommand ExportToExcelCommand { get; set; }

        #endregion
        public BaoCaoThuChiVM()
        {
            #region initial attributes
            yearList = new List<int>();
            monthList = new List<int>();

            for (int i = 1; i <= 12; i++)
            {
                monthList.Add(i);
            }

            yearList.Add(2022);
            yearList.Add(2021);
            yearList.Add(2020);
            yearList.Add(2019);

            monthChiChart = DateTime.Now.Month;
            monthThuChart = DateTime.Now.Month;
            monthDoiTacChart = DateTime.Now.Month;

            yearDoiTacChart = DateTime.Now.Year;
            yearThuChart = DateTime.Now.Year;
            yearChiChart = DateTime.Now.Year;
            YearThuChiChart = DateTime.Now.Year;

            TuNgayChart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            DenNgayChart = DateTime.Now;
            ThuChartViews = new SeriesCollection();
            ChiChartViews = new SeriesCollection();
            DoiTacChartViews = new SeriesCollection();

            DoanhThuGraphValues = new ChartValues<ObservablePoint>();
            ChiPhiGraphValues = new ChartValues<ObservablePoint>();

            LstPhieuThuChi = new ObservableCollection<PhieuThuChi>();

            #endregion
            #region initial command
            ThuChiChartCommand = new RelayCommand<Grid>((p) => { return true; }, p => LoadThuChiChart());
            ChiChartCommand = new RelayCommand<Grid>((p) => { return true; }, p => LoadChiChart());
            ThuChartCommand = new RelayCommand<Grid>((p) => { return true; }, p => LoadThuChart());
            DoiTacChartCommand = new RelayCommand<Grid>((p) => { return true; }, p => LoadDoiTacChart());
            ListBaoCaoCommand = new RelayCommand<Grid>((p) => { return true; }, p => LoadListThuChi());
            ExportToExcelCommand = new RelayCommand<System.Windows.Forms.ListView>((p) => { return true; }, p => ExportToExcel());

            #endregion

            #region LoadData
            LoadThuChiChart();
            LoadChiChart();
            LoadDoiTacChart();
            LoadThuChart();
            #endregion
        }


        #region Methods
        private void LoadThuChiChart()
        {
            DoanhThuGraphValues.Clear();
            ChiPhiGraphValues.Clear();
            //Load sqldata phieu thu, chi
            var PhieuHangHoa = (from d in DataProvider.Ins.DB.PHIEUHANGHOAs
                                where (d.NgayLap.Value.Year == YearThuChiChart)
                                group d by new
                                {
                                    Year = d.NgayLap.Value.Year,
                                    LoaiPhieu = d.LoaiPhieu,
                                    Month = d.NgayLap.Value.Month
                                } into g
                                select new
                                {
                                    Month = g.Key.Month,
                                    loaiphieu = g.Key.LoaiPhieu,

                                    Total = g.Sum(x => x.TongTien)
                                }).ToList();
            var PhieuSuaChua = (from d in DataProvider.Ins.DB.PHIEUSUACHUAs
                                where (d.NgaySuaChua.Value.Year == YearThuChiChart)
                                group d by new
                                {
                                    Year = d.NgaySuaChua.Value.Year,
                                    Month = d.NgaySuaChua.Value.Month
                                } into g
                                select new
                                {
                                    Month = g.Key.Month,
                                    Total = g.Sum(x => x.TongTien)
                                }).ToList();

            var PhieuHeo = (from d in DataProvider.Ins.DB.PHIEUHEOs
                            where (d.NgayLap.Value.Year == YearThuChiChart)
                            group d by new
                            {
                                Year = d.NgayLap.Value.Year,
                                LoaiPhieu = d.LoaiPhieu,
                                Month = d.NgayLap.Value.Month
                            } into g
                            select new
                            {
                                Month = g.Key.Month,
                                loaiphieu = g.Key.LoaiPhieu,

                                Total = g.Sum(x => x.TongTien)
                            }).ToList();


            for (int i = 1; i <= 12; i++)
            {
                var DoanhThuPoint = new ObservablePoint();
                var ChiPhiPoint = new ObservablePoint();

                DoanhThuPoint.X = i;
                DoanhThuPoint.Y = 0;

                ChiPhiPoint.X = i;
                ChiPhiPoint.Y = 0;

                //Load data doanh thu
                var pointHangHoaThu = PhieuHangHoa.Where(x => (x.Month == i /*&& x.loaiphieu== ""*/));
                if (pointHangHoaThu.Count() > 0)
                {
                    DoanhThuPoint.Y += pointHangHoaThu.ElementAt(0).Total.Value;
                }
                var pointPhieuHeoThu = PhieuHeo.Where(x => (x.Month == i /*&& x.loaiphieu== ""*/));
                if (pointPhieuHeoThu.Count() > 0)
                {
                    DoanhThuPoint.Y += pointPhieuHeoThu.ElementAt(0).Total.Value;
                }

                //Load data chi phi
                var pointHangHoaChi = PhieuHangHoa.Where(x => (x.Month == i /*&& x.loaiphieu== ""*/));
                if (pointHangHoaChi.Count() > 0)
                {
                    ChiPhiPoint.Y += pointHangHoaChi.ElementAt(0).Total.Value;
                }
                var pointPhieuHeoChi = PhieuHeo.Where(x => (x.Month == i /*&& x.loaiphieu== ""*/));
                if (pointPhieuHeoChi.Count() > 0)
                {
                    ChiPhiPoint.Y += pointPhieuHeoChi.ElementAt(0).Total.Value;
                }
                var pointSuaChua = PhieuSuaChua.Where(x => (x.Month == i /*&& x.loaiphieu== ""*/));
                if (pointSuaChua.Count() > 0)
                {
                    ChiPhiPoint.Y += pointSuaChua.ElementAt(0).Total.Value;
                }


                ChiPhiGraphValues.Add(ChiPhiPoint);
                DoanhThuGraphValues.Add(DoanhThuPoint);
            }


        }
        private void LoadThuChart()
        {

            ThuChartViews.Clear();
            //Load Value BanHeo tu hoa don ban heo
            var valueBanHeo = (from d in DataProvider.Ins.DB.PHIEUHEOs
                               where (
                                       d.NgayLap.Value.Year == YearThuChart
                                       && d.NgayLap.Value.Month == MonthThuChart
                                       //&& d.LoaiPhieu.ToString() == ""
                                       )
                               group d by new
                               {
                                   Year = d.NgayLap.Value.Year,
                                   Month = d.NgayLap.Value.Month
                               } into g
                               select new
                               {
                                   Total = g.Sum(x => x.TongTien)
                               }).ToList();


            PieSeries pieBanHeo = new PieSeries
            {
                Title = "Bán heo",
                Values = new ChartValues<ObservableValue> { new ObservableValue(0) },
                DataLabels = true
            };

            if (valueBanHeo.Count > 0)
            {
                pieBanHeo.Values.Clear();
                pieBanHeo.Values.Add(new ObservableValue(valueBanHeo.ElementAt(0).Total.Value));
            }
            ThuChartViews.Add(pieBanHeo);

            //Load Value BanHeo tu hoa don ban heo
            var valueHangHoa = (from d in DataProvider.Ins.DB.PHIEUHANGHOAs
                                where (
                                        d.NgayLap.Value.Year == YearThuChart
                                        && d.NgayLap.Value.Month == MonthThuChart
                                        //&& d.LoaiPhieu.ToString() == ""
                                        )
                                group d by new
                                {
                                    Year = d.NgayLap.Value.Year,
                                    Month = d.NgayLap.Value.Month
                                } into g
                                select new
                                {
                                    Total = g.Sum(x => x.TongTien)
                                }).ToList();


            PieSeries pieHangHoa = new PieSeries
            {
                Title = "Bán hàng hóa",
                Values = new ChartValues<ObservableValue> { new ObservableValue(0) },
                DataLabels = true
            };
            if (valueHangHoa.Count > 0)
            {
                pieHangHoa.Values.Clear();
                pieHangHoa.Values.Add(new ObservableValue(valueHangHoa.ElementAt(0).Total.Value));
            }

            ThuChartViews.Add(pieHangHoa);

        }
        private void LoadChiChart()
        {
            ChiChartViews.Clear();
            //Load Value BanHeo tu hoa don ban heo
            var valueBanHeo = (from d in DataProvider.Ins.DB.PHIEUHEOs
                               where (
                                       d.NgayLap.Value.Year == YearChiChart
                                       && d.NgayLap.Value.Month == MonthChiChart
                                       //&& d.LoaiPhieu.ToString() == ""
                                       )
                               group d by new
                               {
                                   Year = d.NgayLap.Value.Year,
                                   Month = d.NgayLap.Value.Month
                               } into g
                               select new
                               {
                                   Total = g.Sum(x => x.TongTien)
                               }).ToList();


            PieSeries pieBanHeo = new PieSeries
            {
                Title = "Mua heo",
                Values = new ChartValues<ObservableValue> { new ObservableValue(0) },
                DataLabels = true
            };

            if (valueBanHeo.Count > 0)
            {
                pieBanHeo.Values.Clear();
                pieBanHeo.Values.Add(new ObservableValue(valueBanHeo.ElementAt(0).Total.Value));
            }
            ChiChartViews.Add(pieBanHeo);

            //Load Value BanHeo tu hoa don ban heo
            var valueHangHoa = (from d in DataProvider.Ins.DB.PHIEUHANGHOAs
                                where (
                                        d.NgayLap.Value.Year == YearChiChart
                                        && d.NgayLap.Value.Month == MonthChiChart
                                        //&& d.LoaiPhieu.ToString() == ""
                                        )
                                group d by new
                                {
                                    Year = d.NgayLap.Value.Year,
                                    Month = d.NgayLap.Value.Month
                                } into g
                                select new
                                {
                                    Total = g.Sum(x => x.TongTien)
                                }).ToList();


            PieSeries pieHangHoa = new PieSeries
            {
                Title = "Mua hàng hóa",
                Values = new ChartValues<ObservableValue> { new ObservableValue(0) },
                DataLabels = true
            };
            if (valueHangHoa.Count > 0)
            {
                pieHangHoa.Values.Clear();
                pieHangHoa.Values.Add(new ObservableValue(valueHangHoa.ElementAt(0).Total.Value));
            }

            ChiChartViews.Add(pieHangHoa);

            //Load value sua chua tu hoa don sua chua
            var valueSuaChua = (from d in DataProvider.Ins.DB.PHIEUSUACHUAs
                                where (
                                        d.NgaySuaChua.Value.Year == YearChiChart
                                        && d.NgaySuaChua.Value.Month == MonthChiChart
                                        && d.NgaySuaChua.ToString() == ""
                                        )
                                group d by new
                                {
                                    Year = d.NgaySuaChua.Value.Year,
                                    Month = d.NgaySuaChua.Value.Month
                                } into g
                                select new
                                {
                                    Total = g.Sum(x => x.TongTien)
                                }).ToList();


            PieSeries pieSuaChua = new PieSeries
            {
                Title = "Sữa chữa",
                Values = new ChartValues<ObservableValue> { new ObservableValue(0) },
                DataLabels = true
            };
            if (valueSuaChua.Count > 0)
            {
                pieSuaChua.Values.Clear();
                pieSuaChua.Values.Add(new ObservableValue(valueSuaChua.ElementAt(0).Total.Value));
            }

            ChiChartViews.Add(pieSuaChua);

        }
        private void LoadDoiTacChart()
        {
            DoiTacChartViews.Clear();
            //Load Value BanHeo tu hoa don ban heo
            var valueDoiTacPH = (from d in DataProvider.Ins.DB.PHIEUHEOs
                                 where (
                                         d.NgayLap.Value.Year == YearDoiTacChart
                                         && d.NgayLap.Value.Month == MonthDoiTacChart
                                         //&& d.LoaiPhieu.ToString() == ""
                                         )
                                 group d by new
                                 {
                                     Year = d.NgayLap.Value.Year,
                                     Month = d.NgayLap.Value.Month,
                                     doiTac = d.DOITAC
                                 } into g
                                 select new
                                 {
                                     doitac = g.Key.doiTac,
                                     Total = g.Sum(x => x.TongTien)
                                 }).ToList();
            var valueDoiTacPHH = (from d in DataProvider.Ins.DB.PHIEUHANGHOAs
                                  where (
                                          d.NgayLap.Value.Year == YearDoiTacChart
                                          && d.NgayLap.Value.Month == MonthDoiTacChart
                                          //&& d.LoaiPhieu.ToString() == ""
                                          )
                                  group d by new
                                  {
                                      Year = d.NgayLap.Value.Year,
                                      Month = d.NgayLap.Value.Month,
                                      doiTac = d.DOITAC
                                  } into g
                                  select new
                                  {
                                      doitac = g.Key.doiTac,
                                      Total = g.Sum(x => x.TongTien)
                                  }).ToList();

            var valueDoiTac = (from g in valueDoiTacPH.Union(valueDoiTacPHH)
                               group g by new {
                                   doitac = g.doitac
                               } into p
                               select new
                               {
                                   TenDoiTac = p.Key.doitac.TenDoiTac,
                                   total = p.Sum(x => x.Total)
                               }).ToList();

            foreach (var i in valueDoiTac)
            {
                PieSeries pieDoiTac = new PieSeries
                {
                    Title = i.TenDoiTac,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(i.total.Value) },
                    DataLabels = true
                };

                DoiTacChartViews.Add(pieDoiTac);
            }


        }
        private void LoadListThuChi()
        {
            LstPhieuThuChi.Clear();

            var PhieuHangHoa = (from d in DataProvider.Ins.DB.PHIEUHANGHOAs
                                where (d.NgayLap <= DenNgayChart && d.NgayLap >= TuNgayChart)
                                select new PhieuThuChi
                                {
                                    IDPhieu = d.SoPhieu,
                                    Ngay = d.NgayLap.Value,
                                    NhanVien = d.NHANVIEN.HoTen,
                                    DoiTac = d.DOITAC.TenDoiTac,
                                    LoaiPhieu = d.LoaiPhieu,
                                    TongTien = d.TongTien.ToString()

                                }).ToList();
            var PhieuHeo = (from d in DataProvider.Ins.DB.PHIEUHEOs
                            where (d.NgayLap <= DenNgayChart && d.NgayLap >= TuNgayChart)
                            select new PhieuThuChi
                            {
                                IDPhieu = d.SoPhieu,
                                Ngay = d.NgayLap.Value,
                                NhanVien = d.NHANVIEN.HoTen,
                                DoiTac = d.DOITAC.TenDoiTac,
                                LoaiPhieu = d.LoaiPhieu,
                                TongTien = d.TongTien.ToString()

                            }).ToList();
            var PhieuSuaChua = (from d in DataProvider.Ins.DB.PHIEUSUACHUAs
                                where (d.NgaySuaChua <= DenNgayChart && d.NgaySuaChua >= TuNgayChart)
                                select new PhieuThuChi
                                {
                                    IDPhieu = d.SoPhieu,
                                    Ngay = d.NgaySuaChua.Value,
                                    NhanVien = d.NHANVIEN.HoTen,
                                    DoiTac = d.DOITAC.TenDoiTac,
                                    LoaiPhieu = "Phiếu sữa chữa",
                                    TongTien = d.TongTien.ToString()

                                }).ToList();

            foreach (var i in PhieuHangHoa)
            {
                LstPhieuThuChi.Add(i);
            }
            foreach (var i in PhieuHeo)
            {
                LstPhieuThuChi.Add(i);
            }
            foreach (var i in PhieuSuaChua)
            {
                LstPhieuThuChi.Add(i);
            }

        }
        private void ExportToExcel()
        {
            string filePath = "";
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel | *.xlsx";
            if (dialog.ShowDialog() == DialogResult.OK)
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
                    excelPackage.Workbook.Properties.Title = "Báo cáo thu chi";
                    excelPackage.Workbook.Worksheets.Add("Báo cáo");
                    ExcelWorksheet ws = excelPackage.Workbook.Worksheets[0];
                    ws.Name = "Báo cáo";
                    ws.Cells.Style.Font.Size = 14;
                    ws.Cells.Style.Font.Name = "Times New Roman";
                    ws.Cells[1, 1].Value = "Báo cáo sửa chữa";
                    int colIndex = 1;
                    int rowIndex = 1;
                        colIndex = 1;
                        rowIndex++;
                    ws.Cells[rowIndex, colIndex++].Value = "Mã phiếu";
                        ws.Cells[rowIndex, colIndex++].Value = "Ngày lập";
                    ws.Cells[rowIndex, colIndex++].Value = "Nhân viên";
                    ws.Cells[rowIndex, colIndex++].Value = "Đối tác";
                    ws.Cells[rowIndex, colIndex++].Value = "Loại phiếu";
                    ws.Cells[rowIndex, colIndex++].Value = "Tổng tiền";

                    foreach (var item in LstPhieuThuChi)
                    {
                        colIndex = 1;
                        rowIndex++;
                        ws.Cells[rowIndex, colIndex++].Value = item.IDPhieu.ToString();
                        ws.Cells[rowIndex, colIndex++].Value = item.Ngay.ToString();
                        ws.Cells[rowIndex, colIndex++].Value = item.NhanVien.ToString();
                        ws.Cells[rowIndex, colIndex++].Value = item.DoiTac.ToString();
                        ws.Cells[rowIndex, colIndex++].Value = item.LoaiPhieu.ToString();
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
        }
        #endregion
        public class PhieuThuChi
        {
            public string IDPhieu { get; set; }
            public DateTime Ngay { get; set; }
            public string NhanVien { get; set; }
            public string DoiTac { get; set; }
           public string LoaiPhieu { get; set; }    
            public string TongTien { get; set; }
        }
    }
}
