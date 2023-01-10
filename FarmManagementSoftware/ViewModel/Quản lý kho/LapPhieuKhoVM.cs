using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.Windows.Quản_lý_kho;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FarmManagementSoftware.ViewModel
{
    public class LapPhieuKhoVM : BaseViewModel
    {
        DateTime _dateMin;
        DateTime _dateMax;
        string _NhanVienLapPhieu;
        string _Khachhang;

        ListView listViewdsNhap;
        ListView listViewdsXuat;
        ListView listViewdsKiem;
        Expander expdsNhap;
        Expander expdsXuat;
        Expander expdsKiem;

        private PHIEUHANGHOA _Selectedphieuhanghoa;
        public PHIEUHANGHOA Selectedphieuhanghoa { get => _Selectedphieuhanghoa; set { _Selectedphieuhanghoa = value; OnPropertyChanged(); } }

        private PHIEUKIEMKHO _Selectedphieukiemkho;
        public PHIEUKIEMKHO Selectedphieukiemkho { get => _Selectedphieukiemkho; set { _Selectedphieukiemkho = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUHANGHOA> _dsPhieuNhap;
        public ObservableCollection<PHIEUHANGHOA> dsPhieuNhap { get => _dsPhieuNhap; set { _dsPhieuNhap = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUHANGHOA> _dsPhieuXuat;
        public ObservableCollection<PHIEUHANGHOA> dsPhieuXuat { get => _dsPhieuXuat; set { _dsPhieuXuat = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUKIEMKHO> _dsPhieuKiem;
        public ObservableCollection<PHIEUKIEMKHO> dsPhieuKiem { get => _dsPhieuKiem; set { _dsPhieuKiem = value; OnPropertyChanged(); } }

        private List<string> listTrangThai;

        public ICommand ChangeDateStartCommand { get; set; }
        public ICommand ChangeDateEndCommand { get; set; }
        public ICommand TTCheckCommand { get; set; }
        public ICommand TimKiemTenNhanVienCommand { get; set; }
        public ICommand TimKiemKhachHangCommand { get; set; }
        public ICommand TaoPhieuKhocommand { get; set; }
        public ICommand isActiveWindow { get; set; }
        public ICommand showdetailPhieuHangHoa { get; set; }
        public ICommand showdetailPhieuKiemKho { get; set; }

        public LapPhieuKhoVM()
        {
            _dateMin = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            _dateMax = DateTime.Today;
            _NhanVienLapPhieu = "";
            _Khachhang = "";

            dsPhieuNhap = new ObservableCollection<PHIEUHANGHOA>();
            dsPhieuXuat = new ObservableCollection<PHIEUHANGHOA>();
            dsPhieuKiem = new ObservableCollection<PHIEUKIEMKHO>();
            listTrangThai = new List<string>();
            listTrangThai.Add("Đã hoàn thành");
            listTrangThai.Add("Chưa hoàn thành");
            listTrangThai.Add("Đã hủy");

            LoadWindow();

            #region command change date start
            ChangeDateStartCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                if (p.SelectedDate <= DateTime.Today && p.SelectedDate != null)
                {
                    _dateMin = p.SelectedDate.Value;
                    LoadWindow();
                }
                else
                {
                    p.SelectedDate = _dateMin;
                    if (p.SelectedDate > DateTime.Today)
                        MessageBox.Show("Thời gian tìm kiểm phải nhỏ hơn hoặc bằng ngày hôm nay");
                    else if (p.SelectedDate == null) MessageBox.Show("Thời gian tìm kiểm không thể trống");
                }
            });
            #endregion

            #region command change date start
            ChangeDateEndCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                if (p.SelectedDate <= DateTime.Today && p.SelectedDate != null)
                {
                    _dateMax = p.SelectedDate.Value;
                    LoadWindow();
                }
                else
                {
                    p.SelectedDate = _dateMax;
                    if (p.SelectedDate > DateTime.Today)
                        MessageBox.Show("Thời gian tìm kiểm phải nhỏ hơn hoặc bằng ngày hôm nay");
                    else if (p.SelectedDate == null) MessageBox.Show("Thời gian tìm kiểm không thể trống");
                }
            });
            #endregion

            #region command check Trạng thái
            TTCheckCommand = new RelayCommand<CheckBox>((p) => { return true; }, p =>
            {
                if (p.IsChecked == true)
                    listTrangThai.Add(p.Content.ToString());
                else listTrangThai.Remove(p.Content.ToString());
                LoadWindow();
            });
            #endregion

            #region command tìm kiếm tên nhân viên thực hiện
            TimKiemTenNhanVienCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                _NhanVienLapPhieu = p.Text;
                LoadWindow();
            });
            #endregion

            #region command tìm kiếm tên khách hàng
            TimKiemKhachHangCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                _Khachhang = p.Text;
                LoadWindow();
            });
            #endregion

            #region command tạo phiếu kho
            TaoPhieuKhocommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                TaoPhieuKho wc = new TaoPhieuKho();
                TaoPhieuKhoVM TPKvm = new TaoPhieuKhoVM(this);
                wc.DataContext = TPKvm;
                wc.ShowDialog();
            });
            #endregion

            #region command xem chi tiết phiếu hàng hóa
            showdetailPhieuHangHoa = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                ChiTietPhieuKhoWindow wc = new ChiTietPhieuKhoWindow();
                ChiTietPhieuKhoVM CTvm = new ChiTietPhieuKhoVM(Selectedphieuhanghoa);
                wc.DataContext = CTvm;
                wc.ShowDialog();
                LoadDSPhieuNhapKho();
                LoadDSPhieuXuatKho();
            });
            #endregion

            #region command xem chi tiết phiếu kiểm kho
            showdetailPhieuKiemKho = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                ChiTietPhieuKhoWindow wc = new ChiTietPhieuKhoWindow();
                ChiTietPhieuKhoVM CTvm = new ChiTietPhieuKhoVM(Selectedphieukiemkho);
                wc.DataContext = CTvm;
                wc.ShowDialog();
                LoadDSPhieuKiemKho();
            });
            #endregion

            isActiveWindow = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                LapPhieuKhoWindow wc = (LapPhieuKhoWindow)p;
                listViewdsNhap = wc.Lw_dsNhap;
                listViewdsXuat = wc.lW_dsXuat;
                listViewdsKiem = wc.lW_dsKiem;
                expdsNhap = wc.exp_dsNhap;
                expdsXuat = wc.exp_dsXuat;
                expdsKiem = wc.exp_dsKiem;

            });
        }
        void LoadWindow()
        {
            LoadDSPhieuNhapKho();
            LoadDSPhieuXuatKho();
            LoadDSPhieuKiemKho();
        }

        public void ForcusdsNhap()
        {
            expdsKiem.IsExpanded = false;
            expdsXuat.IsExpanded = false;
            expdsNhap.IsExpanded = true;
            listViewdsNhap.SelectedIndex = listViewdsNhap.Items.Count - 1;
            listViewdsNhap.ScrollIntoView(listViewdsNhap.SelectedItem);
        }

        public void ForcusdsXuat()
        {
            expdsKiem.IsExpanded = false;
            expdsXuat.IsExpanded = true;
            expdsNhap.IsExpanded = false;
            listViewdsXuat.SelectedIndex = listViewdsXuat.Items.Count - 1;
            listViewdsXuat.ScrollIntoView(listViewdsXuat.SelectedItem);
        }

        public void ForcusdsKiem()
        {
            expdsKiem.IsExpanded = true;
            expdsXuat.IsExpanded = false;
            expdsNhap.IsExpanded = false;
            listViewdsKiem.SelectedIndex = listViewdsKiem.Items.Count - 1;
            listViewdsKiem.ScrollIntoView(listViewdsKiem.SelectedItem);
        }

        void LoadDSPhieuNhapKho()
        {
            dsPhieuNhap.Clear();
            DateTime max = _dateMax.AddDays(1);
            var phieuNhaps = DataProvider.Ins.DB.PHIEUHANGHOAs.Where(x => x.LoaiPhieu == "Phiếu nhập kho" && x.NgayLap >= _dateMin && x.NgayLap <= max).ToList();
            phieuNhaps = phieuNhaps.Where(x => listTrangThai.Contains(x.TrangThai)).ToList();
            phieuNhaps = phieuNhaps.Where(x => x.NHANVIEN.HoTen.Contains(_NhanVienLapPhieu)).ToList();
            phieuNhaps = phieuNhaps.Where(x => x.DOITAC.TenDoiTac.Contains(_Khachhang)).ToList();

            foreach (var item in phieuNhaps)
            {
                dsPhieuNhap.Add(item);
            }
        }

        void LoadDSPhieuXuatKho()
        {
            dsPhieuXuat.Clear();
            DateTime max = _dateMax.AddDays(1);
            var phieuXuats = DataProvider.Ins.DB.PHIEUHANGHOAs.Where(x => x.LoaiPhieu != "Phiếu nhập kho" && x.NgayLap >= _dateMin && x.NgayLap <= max).ToList();
            phieuXuats = phieuXuats.Where(x => listTrangThai.Contains(x.TrangThai)).ToList();
            phieuXuats = phieuXuats.Where(x => x.NHANVIEN.HoTen.Contains(_NhanVienLapPhieu)).ToList();
            phieuXuats = phieuXuats.Where(x => x.NHANVIEN1 == null || (x.NHANVIEN1 != null && x.NHANVIEN1.HoTen.Contains(_Khachhang))).ToList();
            phieuXuats = phieuXuats.Where(x => x.DOITAC == null || (x.DOITAC != null && x.DOITAC.TenDoiTac.Contains(_Khachhang))).ToList();

            foreach (var item in phieuXuats)
            {
                dsPhieuXuat.Add(item);
            }
        }

        void LoadDSPhieuKiemKho()
        {
            dsPhieuKiem.Clear();
            var phieuKiems = DataProvider.Ins.DB.PHIEUKIEMKHOes.Where(x => x.NgayLap >= _dateMin && x.NgayLap <= _dateMax).ToList();
            phieuKiems = phieuKiems.Where(x => x.NHANVIEN.HoTen.Contains(_NhanVienLapPhieu)).ToList();

            foreach (var item in phieuKiems)
            {
                dsPhieuKiem.Add(item);
            }
        }
    }
}
