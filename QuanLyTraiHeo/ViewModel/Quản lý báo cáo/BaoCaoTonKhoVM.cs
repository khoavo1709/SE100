using LiveCharts.Helpers;
using Microsoft.SqlServer.Server;
using Microsoft.Win32;
using OfficeOpenXml;
using QuanLyTraiHeo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static QuanLyTraiHeo.ViewModel.BaoCaoThuChiVM;
using LicenseContext = OfficeOpenXml.LicenseContext;
using SeriesCollection = LiveCharts.SeriesCollection;
using System.Windows.Controls;
using DataTable = System.Data.DataTable;
using OfficeOpenXml.Style;
using System.IO;

namespace QuanLyTraiHeo.ViewModel
{
    public class listBCTK:BaseViewModel
    {
        private string _STT;
        public string STT { get=>_STT; set { _STT = value; OnPropertyChanged(); } }
        private BAOCAOTONKHO _bctk;
        public BAOCAOTONKHO bctk { get => _bctk; set { _bctk = value; OnPropertyChanged(); } }

        public listBCTK()
        {
            STT = "";
            this.bctk = null;
        }
        public listBCTK(string sTT, BAOCAOTONKHO bctk)
        {
            STT = sTT;
            this.bctk = bctk;
        }
        public listBCTK(BAOCAOTONKHO bctk)
        {
            STT = "";
            this.bctk = bctk;
        }
    }
    public class BaoCaoTonKhoVM:BaseViewModel
    {
        private int _TongTonDau;
        public int TongTonDau { get=> _TongTonDau; set { _TongTonDau = value; OnPropertyChanged(); } }
        private int _TongTonCuoi;
        public int TongTonCuoi { get => _TongTonCuoi; set { _TongTonCuoi = value; OnPropertyChanged(); } }
        private int _TongSLNhap;
        public int TongSLNhap { get => _TongSLNhap; set { _TongSLNhap = value; OnPropertyChanged(); } }
        private int _TongSLXuat;
        public int TongSLXuat { get => _TongSLXuat; set { _TongSLXuat = value; OnPropertyChanged(); } }

        private ObservableCollection<listBCTK> _ListBCTKs;
        public ObservableCollection<listBCTK> ListBCTKs { get => _ListBCTKs; set { _ListBCTKs = value; OnPropertyChanged(); } }

        private List<int> _ListThang;
        public List<int> ListThang { get => _ListThang; set { _ListThang = value; OnPropertyChanged(); } }

        private int _SelectedThang;
        public int SelectedThang { get => _SelectedThang; set { _SelectedThang = value; OnPropertyChanged(); } }

        private List<int> _ListNam;
        public List<int> ListNam { get => _ListNam; set { _ListNam = value; OnPropertyChanged(); } }

        private int _SelectedNam;
        public int SelectedNam { get => _SelectedNam; set { _SelectedNam = value; OnPropertyChanged(); } }

        private List<string> _ListLoaiHH;
        public List<string> ListLoaiHH { get => _ListLoaiHH; set { _ListLoaiHH = value; OnPropertyChanged(); } }

        private string _SelectedLoaiHH;
        public string SelectedLoaiHH { get => _SelectedLoaiHH; set { _SelectedLoaiHH = value; OnPropertyChanged(); } }

        private List<int> _ListLoaiHangHoa;
        public List<int> ListLoaiHangHoa { get => _ListLoaiHangHoa; set { _ListLoaiHangHoa = value; OnPropertyChanged(); } }

        public ICommand TimKiemTheoThang { get; set; }
        public ICommand TimKiemTheoNam { get; set; }
        public ICommand TimKiemTheoLoaiHH { get; set; }
        public ICommand XuatFileExcelCommand { get; set; }
        public BaoCaoTonKhoVM()
        {
            ListBCTKs = new ObservableCollection<listBCTK>();
            ListThang = new List<int>();
            ListNam = new List<int>();
            ListLoaiHH = new List<string>();

            #region lấy dữ liệu cho comboBox Tháng
            ListThang = DataProvider.Ins.DB.BAOCAOTONKHOes.Select(x => x.Thang.Value).Distinct().ToList();
            SelectedThang = DateTime.Today.Month;
            #endregion

            #region lấy dữ liệu cho comboBox Năm
            ListNam = DataProvider.Ins.DB.BAOCAOTONKHOes.Select(x => x.Nam.Value).Distinct().ToList();
            SelectedNam = DateTime.Today.Year;
            #endregion

            #region lấy dữ liệu cho comboBox thức ăn
            ListLoaiHH = DataProvider.Ins.DB.BAOCAOTONKHOes.Select(x => x.HANGHOA.LoaiHangHoa).Distinct().ToList();
            ListLoaiHH.Add("Tất cả");
            SelectedLoaiHH = "Tất cả";
            #endregion
            #region binding dữ liệu cho danh sách hàng hoá trong kho
            LoadBCTK();

            //lstHangHoa.Add(new HangHoa() { STT = "1", IDHangHoa = "TA01", TenHangHoa = "Thức ăn HH cao cấp cho heo nái mang thai 8042", LoaiHangHoa = "Thức ăn", Soluong = "20", DVT = "bao", TonDau = "10", NhapThem = "10" });
            //lstHangHoa.Add(new HangHoa() { STT = "2", IDHangHoa = "TA02", TenHangHoa = "B52/AMPI-COL", LoaiHangHoa = "Thuốc", Soluong = "50", DVT = "chai", TonDau = "5", NhapThem = "45" });
            //lstHangHoa.Add(new HangHoa() { STT = "", IDHangHoa = "", TenHangHoa = "TỔNG SỐ LƯỢNG", LoaiHangHoa = "", Soluong = "70", DVT = "", TonDau = "", NhapThem = "" });
            #endregion
            #region command Tìm kiếm theo tháng
            TimKiemTheoThang = new RelayCommand<Window>((p) => { return true; }, p => {
                LoadBCTK();
                OnPropertyChanged("ListBCTKs");
            });
            #endregion

            #region command Tìm kiếm theo Nam
            TimKiemTheoNam = new RelayCommand<Window>((p) => { return true; }, p => {
                LoadBCTK();
                OnPropertyChanged("ListBCTKs");
            });
            #endregion

            #region command Tìm kiếm theo LoaiHH
            TimKiemTheoLoaiHH = new RelayCommand<Window>((p) => { return true; }, p => {
                LoadBCTK();
                OnPropertyChanged("ListBCTKs");
            });
            #endregion

            #region command Xuất file excel
            XuatFileExcelCommand = new RelayCommand<Window>((p) => { return true; }, p => {
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
                        excelPackage.Workbook.Properties.Title = "Báo cáo tồn kho";
                        excelPackage.Workbook.Worksheets.Add("Báo cáo");
                        ExcelWorksheet ws = excelPackage.Workbook.Worksheets[0];
                        ws.Name = "Báo cáo";
                        ws.Cells.Style.Font.Size = 13;
                        ws.Cells.Style.Font.Name = "Times New Roman";
                        string[] arrColumnHeader = {
                            "Số thứ tự",
                            "Mã hàng hoá",
                            "Tên hàng hoá",
                            "Loại hàng hoá",
                            "Tồn đầu",
                            "Tồn cuối",
                            "Số lượng nhận thêm",
                            "Số lượng xuất ra"
                        };
                        ws.Column(1).Width = 10;
                        ws.Column(2).Width = 25;
                        ws.Column(3).Width = 50;
                        ws.Column(4).Width = 20;
                        ws.Column(5).Width = 10;
                        ws.Column(6).Width = 10;
                        ws.Column(7).Width = 20;
                        ws.Column(8).Width = 20;

                        var countColHeader = arrColumnHeader.Count();
                        ws.Cells[1, 1].Value = "Báo cáo tồn kho";
                        ws.Cells[1, 1, 1, countColHeader].Merge = true;
                        ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                        ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        
                        ws.Cells[2, 1].Value = "Nhân viên thực hiện: ";
                        ws.Cells[2, 1, 2, 2].Merge = true;
                        ws.Cells[2, 3].Value = "Trần Trung Thành";
                        ws.Cells[2, 3, 2, 8].Merge = true;

                        ws.Cells[3, 1].Value = "Ngày lập báo cáo: ";
                        ws.Cells[3, 1, 3, 2].Merge = true;
                        ws.Cells[3, 3].Value = DateTime.Today.ToShortDateString();
                        ws.Cells[3, 3, 3, 8].Merge = true;

                        int colIndex = 1;
                        int rowIndex = 4;
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
                        foreach (var item in ListBCTKs)
                        {
                            colIndex = 1;
                            rowIndex++;
                            if (item.bctk.HANGHOA == null)
                            {
                                ws.Cells[rowIndex, 1].Value = item.bctk.MaHH.ToString();
                                ws.Cells[rowIndex, 1, rowIndex, 4].Merge = true;
                                ws.Cells[rowIndex, 1, rowIndex, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells[rowIndex, 5].Value = item.bctk.TonDau.ToString();
                                ws.Cells[rowIndex, 6].Value = item.bctk.TonCuoi.ToString();
                                ws.Cells[rowIndex, 7].Value = item.bctk.SoLuongNhapThem.ToString();
                                ws.Cells[rowIndex, 8].Value = item.bctk.SoLuongXuatRa.ToString();
                                break;
                            }    
                            ws.Cells[rowIndex, colIndex++].Value = item.STT.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.bctk.MaHH.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.bctk.HANGHOA.TenHangHoa.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.bctk.HANGHOA.LoaiHangHoa.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.bctk.TonDau.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.bctk.TonCuoi.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.bctk.SoLuongNhapThem.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.bctk.SoLuongXuatRa.ToString();
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
        void LoadBCTK()
        {
            ListBCTKs.Clear();
            ObservableCollection<BAOCAOTONKHO> lstBCTK; 
            if (SelectedLoaiHH != "Tất cả")
            {
                lstBCTK = new ObservableCollection<BAOCAOTONKHO>(DataProvider.Ins.DB.BAOCAOTONKHOes.Where(x => x.Thang == SelectedThang && x.Nam == SelectedNam && x.HANGHOA.LoaiHangHoa == SelectedLoaiHH));
            }
            else
            {
                lstBCTK = new ObservableCollection<BAOCAOTONKHO>(DataProvider.Ins.DB.BAOCAOTONKHOes.Where(x => x.Thang == SelectedThang && x.Nam == SelectedNam));
            }
            int i = 1;
            foreach (var item in lstBCTK)
            {
                listBCTK l = new listBCTK(i.ToString(), item);
                ListBCTKs.Add(l);
                i++;
            }
            TongTonDau = lstBCTK.Sum(x => x.TonDau).Value;
            TongTonCuoi = lstBCTK.Sum(x => x.TonCuoi).Value;
            TongSLNhap = lstBCTK.Sum(x => x.SoLuongNhapThem).Value;
            TongSLXuat = lstBCTK.Sum(x => x.SoLuongXuatRa).Value;
            BAOCAOTONKHO tong = new BAOCAOTONKHO();
            tong.MaHH = "TỔNG SỐ LƯỢNG";
            tong.TonDau = TongTonDau;
            tong.TonCuoi = TongTonCuoi;
            tong.SoLuongNhapThem = TongSLNhap;
            tong.SoLuongXuatRa = TongSLXuat;
            listBCTK lTong = new listBCTK(tong);
            ListBCTKs.Add(lTong);
        }
        

    }
    
}
