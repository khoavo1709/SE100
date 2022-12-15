using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyTraiHeo.Model;
using static QuanLyTraiHeo.ViewModel.TrangChuVM;
using QuanLyTraiHeo.Model;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Data.Entity.Core.Objects;
using System.ComponentModel;
using System.Data.Entity;
using System.Globalization;

namespace QuanLyTraiHeo.ViewModel
{

    public class QuanLyNhatKyVM : BaseViewModel
    {
        #region Atributes
        private int selectedPage { get; set; }
        private int totalPage { get; set; }
        private int rowPerPage = 20;
        public ObservableCollection<NhatKy> lstNhatKy { get; set; }
        private List<NhatKy> tempLstNhatKy;
        public ObservableCollection<HanhDong> lstActions { get; set; }
        private DateTime tuNgay { get; set; }
        private DateTime denNgay { get; set; }
        #endregion


        #region Properties
        public int SelectedPage { get => this.selectedPage; set { this.selectedPage = value; OnPropertyChanged(); } }
        public int TotalPage { get =>this.totalPage;  set { this.totalPage = value; OnPropertyChanged(); } }
        public DateTime TuNgay { get => tuNgay; set { tuNgay = value; OnPropertyChanged(); } }
        public DateTime DenNgay { get => denNgay; set { denNgay = value; OnPropertyChanged(); } }
        #endregion


        #region EventCommand
        public ICommand TimKiemCommand { get; set; }
        public ICommand NextPageCommand { get; set; }
        public ICommand PreviousPageCommand { get; set; }
        public ICommand LastPageCommand { get; set; }
        public ICommand FirstPageCommand { get; set; }
        public ICommand SelectPageChangedCommand { get; set; }
        #endregion
        public QuanLyNhatKyVM()
        {

            #region initial atributes

            SelectedPage = 1;
            TotalPage = 1;
            lstNhatKy = new ObservableCollection<NhatKy>();
            tempLstNhatKy = new List<NhatKy>();
            lstActions = new ObservableCollection<HanhDong>();
            lstActions.Add(new HanhDong(true, "Nhập, xuất kho"));
            lstActions.Add(new HanhDong(true, "Kiểm kho"));
            lstActions.Add(new HanhDong(true, "Nhập, xuất heo"));
            lstActions.Add(new HanhDong(true, "Tiêm heo"));
            lstActions.Add(new HanhDong(true, "Phối giống heo"));
            lstActions.Add(new HanhDong(true, "Sửa chữa"));
            TuNgay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DenNgay = DateTime.Today;
            #endregion

            #region initial Commnd
            TimKiemCommand = new RelayCommand<ListView>((p) => { return true; }, p =>
            {
                LoadListHanhDong();
                SelectPageChanged();
            });
            PreviousPageCommand = new RelayCommand<ListView>((p) => { return true; }, p =>
            {
                SelectedPage = SelectedPage - 1;
                SelectPageChanged();
            });
            LastPageCommand = new RelayCommand<ListView>((p) => { return true; }, p =>
            {
                SelectedPage = TotalPage;
                SelectPageChanged();
            });
            FirstPageCommand = new RelayCommand<ListView>((p) => { return true; }, p =>
            {
                SelectedPage = 1;
                SelectPageChanged();
            });
            NextPageCommand = new RelayCommand<ListView>((p) => { return true; }, p =>
            {
                SelectedPage = SelectedPage + 1;
                SelectPageChanged();
            });
            SelectPageChangedCommand = new RelayCommand<ListView>((p) => { return true; }, p => { SelectPageChanged(); });

            #endregion

            #region LoadData
            LoadListHanhDong();
            SelectPageChanged();
            #endregion
        }

        #region Method
        private void SelectPageChanged()
        {
            if (SelectedPage < 1 )
                SelectedPage = 1;
            if (SelectedPage > TotalPage)
                SelectedPage = TotalPage;
            lstNhatKy.Clear();
            for (int i = (SelectedPage - 1) * rowPerPage; i < tempLstNhatKy.Count() && i < SelectedPage * rowPerPage; i++)
            {
                lstNhatKy.Add(tempLstNhatKy[i]);
            }
        }
        private void LoadListHanhDong()
        {
            lstNhatKy.Clear();

            tempLstNhatKy.Clear();
            foreach (var item in lstActions)
            {
                if (item.ischecked == true)
                {
                    switch (item.TenHanhDong)
                    {
                        case "Nhập, xuất kho":
                            LoadPhieuHangHoa();
                            break;
                        case "Kiểm kho":
                            LoadPhieuKiemKho();
                            break;
                        case "Nhập, xuất heo":
                            LoadPhieuHeo();
                            break;
                        case "Tiêm heo":
                            LoadPhieuTiemHeo();
                            break;
                        case "Phối giống heo":
                            LoadPhieuPhoiGiong();
                            break;
                        case "Sửa chữa":
                            LoadPhieuSuaChua();
                            break;
                        default: break;
                    }
                }
            }

            SelectedPage = 1;

            if (tempLstNhatKy.Count() % rowPerPage > 0)
                TotalPage = tempLstNhatKy.Count() / rowPerPage + 1;
            else
                TotalPage = tempLstNhatKy.Count() / rowPerPage ;

            if (TotalPage == 0)
                TotalPage = 1;
            tempLstNhatKy = tempLstNhatKy.OrderByDescending(x => x.ThoiGian).ToList();

        }
        private void LoadPhieuHangHoa()
        {
            var Dataset = (from Phieu in DataProvider.Ins.DB.PHIEUHANGHOAs
                               //                           where (Phieu.NgayLap >= TuNgay && Phieu.NgayLap <= DenNgay)

                           select Phieu

                           ).AsEnumerable()

                           .Select(p => new NhatKy
                           {
                               icon = "Warehouse",
                               TenNhanVien = p.NHANVIEN.HoTen,
                               MaPhieu = p.SoPhieu,
                               Ngay = "Ngày" + p.NgayLap.Value.ToString(" dd/MM/yyyy"),
                               ThoiGian = (DateTime)p.NgayLap,
                               HanhDong = "Vừa tạo 1 phiếu " + p.LoaiPhieu.ToString() + " trị giá " + string.Format("{0:#,##0}", p.TongTien) + " VNĐ"
                           }).ToList();
            foreach (var item in Dataset)
                tempLstNhatKy.Add(item);
        }
        private void LoadPhieuSuaChua()
        {
            var Dataset = (from Phieu in DataProvider.Ins.DB.PHIEUSUACHUAs
                               //where (Phieu.NgayLap >= TuNgay && Phieu.NgayLap <= DenNgay)
                           select Phieu).AsEnumerable()
                           .Select(p => new NhatKy
                           {
                               icon = "Warehouse",
                               TenNhanVien = p.NHANVIEN.HoTen,

                               MaPhieu = p.SoPhieu,
                               Ngay = "Ngày" + p.NgaySuaChua.Value.ToString(" dd/MM/yyyy"),

                               ThoiGian = (DateTime)p.NgaySuaChua,
                               HanhDong = "Sữa chữa chuồng, chi phí " + string.Format("{0:#,##0}", p.TongTien) + " VNĐ"

                           }).ToList();
            foreach (var item in Dataset)
                lstNhatKy.Add(item);
        }
        private void LoadPhieuKiemKho()
        {
            var Dataset = (from Phieu in DataProvider.Ins.DB.PHIEUKIEMKHOes
                               //where (Phieu.NgayLap >= TuNgay && Phieu.NgayLap <= DenNgay)
                           select Phieu).AsEnumerable()
                           .Select(p => new NhatKy
                           {
                               icon = "Warehouse",
                               TenNhanVien = p.NHANVIEN.HoTen,

                               MaPhieu = p.SoPhieu,
                               ThoiGian = (DateTime)p.NgayLap,
                               Ngay = "Ngày" + p.NgayLap.Value.ToString(" dd/MM/yyyy"),

                               HanhDong = "Vừa kiểm kho, kết quả : " + p.KetQua.ToString()

                           }).ToList();
            foreach (var item in Dataset)
                lstNhatKy.Add(item);
        }
        private void LoadPhieuHeo()
        {
            var Dataset = (from Phieu in DataProvider.Ins.DB.PHIEUHEOs
                               //                           where (Phieu.NgayLap >= TuNgay && Phieu.NgayLap <= DenNgay)
                           select Phieu).AsEnumerable()
                           .Select(p => new NhatKy
                           {
                               icon = "Warehouse",
                               TenNhanVien = p.NHANVIEN.HoTen,
                               Ngay = "Ngày" + p.NgayLap.Value.ToString(" dd/MM/yyyy"),

                               MaPhieu = p.SoPhieu,
                               ThoiGian = (DateTime)p.NgayLap,
                               HanhDong = "Vừa " + p.LoaiPhieu.ToString() + ", trị giá " + string.Format("{0:#,##0}", p.TongTien) + " VNĐ"
                           }).ToList();
            foreach (var item in Dataset)
                lstNhatKy.Add(item);
        }
        private void LoadPhieuTiemHeo()
        {
            var Dataset = (from Phieu in DataProvider.Ins.DB.LICHTIEMHEOs
                               //                           where (Phieu.NgayLap >= TuNgay && Phieu.NgayLap <= DenNgay)
                           select Phieu).AsEnumerable()
                           .Select(p => new NhatKy
                           {
                               //Ngay = "Ngày" + p.NgayLap.Value.ToString(" dd/MM/yyyy"),

                               icon = "Warehouse",
                               MaPhieu = p.MaLichTiem,
                               HanhDong = "Lên lịch tiêm heo ngày : " + p.NgayTiem.ToString()
                           }).ToList();
            foreach (var item in Dataset)
                lstNhatKy.Add(item);
        }
        private void LoadPhieuPhoiGiong()
        {
            var Dataset = (from Phieu in DataProvider.Ins.DB.LICHPHOIGIONGs
                               //                           where (Phieu.NgayLap >= TuNgay && Phieu.NgayLap <= DenNgay)
                           select Phieu).AsEnumerable()
                           .Select(p => new NhatKy
                           {
                               icon = "Warehouse",
                               MaPhieu = p.MaLichPhoi,
                               HanhDong = "tạo phiếu phối giống",
                               //Ngay = "Ngày" + p.NgayLap.Value.ToString(" dd/MM/yyyy")

                           }).ToList();
            foreach (var item in Dataset)
                lstNhatKy.Add(item);

        }
        #endregion
    }
}
