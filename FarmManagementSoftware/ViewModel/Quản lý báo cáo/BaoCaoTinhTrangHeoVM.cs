using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;
using FarmManagementSoftware.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SeriesCollection = LiveCharts.SeriesCollection;
using System.Windows.Controls;
using System.ComponentModel;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;
using DataTable = System.Data.DataTable;
using OfficeOpenXml.Style;
using Microsoft.Office.Interop.Excel;
using Window = System.Windows.Window;

namespace FarmManagementSoftware.ViewModel
{
    public class BaoCaoTinhTrangHeoVM : BaseViewModel
    {
        private Func<ChartPoint, string> pointLabel;
        public Func<ChartPoint, string> PointLabel { get => pointLabel; set { pointLabel = value; OnPropertyChanged(); } }

        private SeriesCollection _SeriesCollectionTSheoChart;
        public SeriesCollection SeriesCollectionTSheoChart { get => _SeriesCollectionTSheoChart; set { _SeriesCollectionTSheoChart = value; OnPropertyChanged(); } }
        public string[] LabelsTSheoChart { get; set; }
        private ObservableCollection<HEO> _ListHeo { get; set; }
        public ObservableCollection<HEO> ListHeo { get => _ListHeo; set { _ListHeo = value; OnPropertyChanged(); } }
        public ObservableCollection<LOAIHEO> ListLoai { get; set; }
        public ObservableCollection<GIONGHEO> ListGiong { get; set; }

        public ObservableCollection<PHIEUHEO> ListPhieu { get; set; }
        public HEO SelectedHeo { get; set; }
        public LOAIHEO SelectedLoai { get; set; }
        public GIONGHEO SelectedGiong { get; set; }
        DateTime? NgaySinh = DateTime.MinValue;
        DateTime? NgayNhap = DateTime.MinValue;

        public int SelectedNam { get; set; }

        private List<int> _Nam;
        public List<int> Nam { get => _Nam; set { _Nam = value; OnPropertyChanged(); } }
        private SeriesCollection _CoCauLoai { get; set; }
        public SeriesCollection CoCauLoai { get => _CoCauLoai; set { _CoCauLoai = value; OnPropertyChanged(); } }
        private SeriesCollection _CoCauGiong { get; set; }
        public SeriesCollection CoCauGiong { get => _CoCauGiong; set { _CoCauGiong = value; OnPropertyChanged(); } }
        public ICommand SLNam { get; set; }
        public ICommand NgayNhapCommand { get; set; }
        public ICommand NgaySinhCommand { get; set; }
        public ICommand LoaiChanged { get; set; }
        public ICommand GiongChanged { get; set; }

        public ICommand XuatFileExcelCommand { get; set; }
        public BaoCaoTinhTrangHeoVM()
        {
            SelectedNam = DateTime.Now.Year;
            Nam = new List<int>();
            ListHeo = new ObservableCollection<HEO>(DataProvider.Ins.DB.HEOs);
            ListLoai = new ObservableCollection<LOAIHEO>(DataProvider.Ins.DB.LOAIHEOs);
            ListGiong = new ObservableCollection<GIONGHEO>(DataProvider.Ins.DB.GIONGHEOs);
            ListPhieu = new ObservableCollection<PHIEUHEO>(DataProvider.Ins.DB.PHIEUHEOs.Where(x => x.LoaiPhieu == "Phiếu nhập heo"));
            CoCauLoai = new SeriesCollection();
            CoCauGiong = new SeriesCollection();

            foreach (HEO item in ListHeo)
            {
                if(!Nam.Contains(item.NgaySinh.Value.Year))
                {
                    Nam.Add(item.NgaySinh.Value.Year);
                }
            }
            foreach (PHIEUHEO item in DataProvider.Ins.DB.PHIEUHEOs)
            {
                if (!Nam.Contains(item.NgayLap.Value.Year))
                {
                    Nam.Add(item.NgayLap.Value.Year);
                }
            }

            foreach (var Loai in ListLoai)
            {
                PieSeries pieSeries = new PieSeries
                {
                    Title = Loai.TenLoaiHeo,
                    Values = new ChartValues<int> { ListHeo.Count(x => x.MaLoaiHeo == Loai.MaLoaiHeo) },
                    DataLabels = true
                };
                pieSeries.LabelPoint = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
                CoCauLoai.Add(pieSeries);
                
            }

            foreach (var Giong in ListGiong)
            {
                PieSeries pieSeries = new PieSeries
                {
                    Title = Giong.TenGiongHeo,
                    Values = new ChartValues<int> { ListHeo.Count(x => x.MaGiongHeo == Giong.MaGiongHeo) },
                    DataLabels = true
                };
                pieSeries.LabelPoint = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

                CoCauGiong.Add(pieSeries);
            }
            LoadSLHeoTD();

            SLNam = new RelayCommand<Window>((p) =>
            {
                    return true;
            }, p =>
            {

                LoadSLHeoTD();

            });
            NgaySinhCommand = new RelayCommand<DatePicker>((p) =>
            {
                return true;
            }, p =>
            {
                NgaySinh = p.SelectedDate;
                TimKiem();

            });
            NgayNhapCommand = new RelayCommand<DatePicker>((p) =>
            {
                return true;
            }, p =>
            {
                NgayNhap = p.SelectedDate;
                TimKiem();
            });
            LoaiChanged = new RelayCommand<Window>((p) =>
            {
                return true;
            }, p =>
            {
                TimKiem();
            });
            GiongChanged = new RelayCommand<Window>((p) =>
            {
                return true;
            }, p =>
            {
                TimKiem();
            });

            #region Excel
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
                            "Mã heo",
                            "Giới tính",
                            "Ngày sinh",
                            "Tên loại heo",
                            "Tên giống heo",
                            "Mã chuồng",
                            "Trọng lượng",
                            "Tình trạng",
                            "Nguônf gốc"
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
                        foreach (var item in ListHeo)
                        {
                            colIndex = 1;
                            rowIndex++;
                            ws.Cells[rowIndex, colIndex++].Value = item.MaHeo.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.GioiTinh.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = string.Format("{0:dd/MM/yyyy}", item.NgaySinh);
                            ws.Cells[rowIndex, colIndex++].Value = item.LOAIHEO.TenLoaiHeo.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.GIONGHEO.TenGiongHeo.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.MaChuong.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.TrongLuong.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.TinhTrang.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.NguonGoc.ToString();

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
            #endregion
        }

        public void LoadSLHeoTD()
        {
            SeriesCollectionTSheoChart = new SeriesCollection();
            ObservableCollection<PHIEUHEO> PN = new ObservableCollection<PHIEUHEO>(DataProvider.Ins.DB.PHIEUHEOs.Where(x => x.LoaiPhieu == "Phiếu nhập heo" && x.NgayLap.Value.Year == SelectedNam));
            ObservableCollection<PHIEUHEO> PX = new ObservableCollection<PHIEUHEO>(DataProvider.Ins.DB.PHIEUHEOs.Where(x => x.LoaiPhieu == "Phiếu xuất heo" && x.NgayLap.Value.Year == SelectedNam));
            ChartValues<double> giatri = new ChartValues<double>();
            #region binding dữ liệu cho Biểu đồ tổng số heo và số heo chết
            LineSeries c1 = new LineSeries
            {
                Title = "Số heo nhập"
            };
            for (int i = 0; i <=12; i++)
            {
                int slnhap = 0;
                foreach (var phieu in PN)
                {
                    if (phieu.NgayLap.Value.Month == i)
                    {
                        slnhap += phieu.CT_PHIEUHEO.Count();
                    }
                }
                giatri.Add(slnhap);
            }
            c1.Values = giatri;
            SeriesCollectionTSheoChart.Add(c1);
            giatri = new ChartValues<double>();
            LineSeries c2 = new LineSeries
            {
                Title = "Số heo xuất"
            };
            for (int i = 0; i <= 12; i++)
            {
                int slxuat = 0;
                foreach (var phieu in PX)
                {
                    if (phieu.NgayLap.Value.Month == i)
                    {
                        slxuat += phieu.CT_PHIEUHEO.Count();
                    }
                }
                giatri.Add(slxuat);
            }
            c2.Values = giatri;
            SeriesCollectionTSheoChart.Add(c2);

            ObservableCollection<HEO> HD = new ObservableCollection<HEO>(DataProvider.Ins.DB.HEOs.Where(x => x.NguonGoc == "Sinh trong trang trại" && x.NgaySinh.Value.Year == SelectedNam));
            giatri = new ChartValues<double>();
            LineSeries c3 = new LineSeries
            {
                Title = "Số heo đẻ"
            };
            for (int i = 0; i <= 12; i++)
            {
                int slh = 0;
                foreach (var heo in HD)
                {
                    if (heo.NgaySinh.Value.Month == i)
                    {
                        slh++;
                    }
                }
                giatri.Add(slh);
            }
            c3.Values = giatri;
            SeriesCollectionTSheoChart.Add(c3);
            LabelsTSheoChart = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            #endregion
        }
        public void TimKiem()

        {
            List<HEO> full = DataProvider.Ins.DB.HEOs.ToList();
            List<HEO> hEOs1;
            List<HEO> hEOs2 = new List<HEO>();
            List<HEO> hEOs3;
            List<HEO> hEOs4;

            if (NgaySinh != null && NgaySinh.Value != DateTime.MinValue)
            {
                hEOs1 = full.Where(x => x.NgaySinh == NgaySinh).ToList();
            }
            else hEOs1 = full;
            if (NgayNhap != null && NgayNhap.Value != DateTime.MinValue)
            {
                List<PHIEUHEO> phieuNhap = new List<PHIEUHEO>(DataProvider.Ins.DB.PHIEUHEOs.Where(x => x.LoaiPhieu == "Phiếu nhập heo"));
                foreach (PHIEUHEO phieu in phieuNhap)
                {
                    if (phieu.NgayLap == NgayNhap)
                    {
                        List<CT_PHIEUHEO> CT = new List<CT_PHIEUHEO>(DataProvider.Ins.DB.CT_PHIEUHEO.Where(x => x.SoPhieu == phieu.SoPhieu));
                        foreach (CT_PHIEUHEO ct in CT)
                        {
                            HEO x = DataProvider.Ins.DB.HEOs.Where(h => h.MaHeo == ct.MaHeo).First();
                            hEOs2.Add(x);
                        }
                    }
                }
            }
            else hEOs2 = full;

            if (SelectedLoai != null)
            {
                hEOs3 = full.Where(x => x.LOAIHEO.MaLoaiHeo == SelectedLoai.MaLoaiHeo).ToList();
            }
            else
                hEOs3 = full;
            if (SelectedGiong != null)
            {
                hEOs4 = full.Where(x => x.GIONGHEO.MaGiongHeo == SelectedGiong.MaGiongHeo).ToList();
            }
            else
                hEOs4 = full;
            IEnumerable<HEO> heo = from HEO a in hEOs1
                                   join HEO b in hEOs2
                                   on a.MaHeo equals b.MaHeo
                                   join HEO c in hEOs3
                                   on a.MaHeo equals c.MaHeo
                                   join HEO d in hEOs4
                                   on a.MaHeo equals d.MaHeo
                                   select a;
            ListHeo.Clear();
            foreach (HEO h in heo)
            {
                ListHeo.Add(h);
            }
        }
    }
}

