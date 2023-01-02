using FarmManagementSoftware.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FarmManagementSoftware.ViewModel
{
    public class ChiTietPhieuKhoVM : BaseViewModel
    {
        public NHANVIEN NVThucHien { get; set; }
        // list Loại phiếu
        //private List<string> _listLoaiPhieu;
        public List<string> listLoaiPhieu { get; set; }
        public List<string> listTrangThai { get; set; }
        public List<string> listKQ { get; set; }
        // Thông tin phiếu
        private string _SoPhieu;
        public string SoPhieu { get => _SoPhieu; set { _SoPhieu = value; OnPropertyChanged(); } }
        private string _selectedloaiPhieu;
        public string selectedLoaiPhieu { get => _selectedloaiPhieu; set { _selectedloaiPhieu = value; OnPropertyChanged(); } }
        private DateTime _NgayLap;
        public DateTime NgayLap { get => _NgayLap; set { _NgayLap = value; OnPropertyChanged(); } }
        private int? _Tongtien;
        public int? TongTien { get => _Tongtien; set { _Tongtien = value; OnPropertyChanged(); } }
        private string _KQ;
        public string KQ { get => _KQ; set { _KQ = value; OnPropertyChanged(); } }
        private string _ghiChu;
        public string ghiChu { get => _ghiChu; set { _ghiChu = value; OnPropertyChanged(); } }
        private string _selectedTrangThai;
        public string selectedTrangThai { get => _selectedTrangThai; set { _selectedTrangThai = value; OnPropertyChanged(); } }

        // thông tin khách hàng
        private string _maKH;
        public string maKH { get => _maKH; set { _maKH = value; OnPropertyChanged(); } }
        private string _tenKH;
        public string tenKH { get => _tenKH; set { _tenKH = value; OnPropertyChanged(); } }
        private string _email;
        public string email { get => _email; set { _email = value; OnPropertyChanged(); } }
        private string _sdt;
        public string sdt { get => _sdt; set { _sdt = value; OnPropertyChanged(); } }
        private string _diaChi;
        public string diaChi { get => _diaChi; set { _diaChi = value; OnPropertyChanged(); } }

        // thông tin nhân viên
        private string _maNVN;
        public string maNVN { get => _maNVN; set { _maNVN = value; OnPropertyChanged(); } }
        private string _tenNVN;
        public string tenNVN { get => _tenNVN; set { _tenNVN = value; OnPropertyChanged(); } }
        // chi tiết
        private ObservableCollection<CT_PHIEUHANGHOA> _CTPhieuHangHoa;
        public ObservableCollection<CT_PHIEUHANGHOA> CTPhieuHangHoa { get => _CTPhieuHangHoa; set { _CTPhieuHangHoa = value; OnPropertyChanged(); } }
        private ObservableCollection<CT_PHIEUKIEMKHO> _CTPhieuKiemKho;
        public ObservableCollection<CT_PHIEUKIEMKHO> CTPhieuKiemKho { get => _CTPhieuKiemKho; set { _CTPhieuKiemKho = value; OnPropertyChanged(); } }

        public ICommand btnHoanTatcommand { get; set; }
        public ICommand btnHuyBocommand { get; set; }
        public ChiTietPhieuKhoVM()
        {

        }
        public ChiTietPhieuKhoVM(PHIEUHANGHOA phieuhanghoa)
        {
            listLoaiPhieu = new List<string>();
            listLoaiPhieu.Add("Phiếu nhập kho");
            listLoaiPhieu.Add("Phiếu xuất nội");
            listLoaiPhieu.Add("Phiếu xuất ngoại");
            listLoaiPhieu.Add("Phiếu kiểm kho");

            listTrangThai = new List<string>();
            listTrangThai.Add("Đã hoàn thành");
            listTrangThai.Add("Chưa hoàn thành");
            listTrangThai.Add("Đã hủy");



            loadPhieuHangHoa(phieuhanghoa);
            defineCommand(phieuhanghoa);
        }
        public ChiTietPhieuKhoVM(PHIEUKIEMKHO phieukiemkho)
        {
            listLoaiPhieu = new List<string>();
            listLoaiPhieu.Add("Phiếu nhập kho");
            listLoaiPhieu.Add("Phiếu xuất nội");
            listLoaiPhieu.Add("Phiếu xuất ngoại");
            listLoaiPhieu.Add("Phiếu kiểm kho");

            listTrangThai = new List<string>();
            listTrangThai.Add("Đã hoàn thành");
            listTrangThai.Add("Chưa hoàn thành");
            listTrangThai.Add("Đã hủy");

            listKQ = new List<string>();
            listKQ.Add("Đúng");
            listKQ.Add("Sai");
            defineCommand();
            loadPhieuKiemKho(phieukiemkho);
        }

        void defineCommand(PHIEUHANGHOA phieuhanghoa = null)
        {
            #region command btn Hoàn tất
            btnHoanTatcommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if (selectedLoaiPhieu != "Phiếu kiểm kho" && phieuhanghoa != null)
                {
                    if (selectedLoaiPhieu == "Phiếu nhập kho")
                    {
                        updateHangHoaTrongKhoPhieuNhap(phieuhanghoa);
                    }
                    else
                    {
                        updateHangHoaTrongKhoPhieuXuat(phieuhanghoa);
                    }
                    phieuhanghoa.TrangThai = selectedTrangThai;
                    DataProvider.Ins.DB.SaveChanges();
                }
                MessageBox.Show("Đã thay đổi thành công");
                p.Close();
            });
            #endregion

            #region command btn Hủy bỏ
            btnHuyBocommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                p.Close();
            });
            #endregion
        }
        void loadPhieuHangHoa(PHIEUHANGHOA phieuhanghoa)
        {
            SoPhieu = phieuhanghoa.SoPhieu;
            NgayLap = phieuhanghoa.NgayLap.Value;
            selectedLoaiPhieu = phieuhanghoa.LoaiPhieu;
            selectedTrangThai = phieuhanghoa.TrangThai;
            TongTien = phieuhanghoa.TongTien;

            if (phieuhanghoa.DOITAC != null)
            {
                maKH = phieuhanghoa.MaDoiTac;
                tenKH = phieuhanghoa.DOITAC.TenDoiTac;
                diaChi = phieuhanghoa.DOITAC.DiaChi;
                email = phieuhanghoa.DOITAC.Email;
                sdt = phieuhanghoa.DOITAC.SDT;
            }

            if (phieuhanghoa.NHANVIEN1 != null)
            {
                maNVN = phieuhanghoa.MaNhanVienNhan;
                tenNVN = phieuhanghoa.NHANVIEN1.HoTen;
            }

            NVThucHien = phieuhanghoa.NHANVIEN;
            CTPhieuHangHoa = new ObservableCollection<CT_PHIEUHANGHOA>(phieuhanghoa.CT_PHIEUHANGHOA);
        }
        void loadPhieuKiemKho(PHIEUKIEMKHO phieukiemkho)
        {
            SoPhieu = phieukiemkho.SoPhieu;
            NgayLap = phieukiemkho.NgayLap.Value;
            selectedLoaiPhieu = "Phiếu kiểm kho";
            KQ = phieukiemkho.KetQua;
            ghiChu = phieukiemkho.GhiChu;

            NVThucHien = phieukiemkho.NHANVIEN;
            CTPhieuKiemKho = new ObservableCollection<CT_PHIEUKIEMKHO>(phieukiemkho.CT_PHIEUKIEMKHO);
        }
        void updateHangHoaTrongKhoPhieuNhap(PHIEUHANGHOA phieunhap = null)
        {
            if (phieunhap != null)
            {
                if (phieunhap.TrangThai == "Đã hoàn thành" && selectedTrangThai != "Đã hoàn thành")
                {
                    foreach (var item in phieunhap.CT_PHIEUHANGHOA)
                    {
                        item.HANGHOA.SoLuongTonKho -= item._soLuong;
                    }
                }
                else if (phieunhap.TrangThai != "Đã hoàn thành" && selectedTrangThai == "Đã hoàn thành")
                {
                    foreach (var item in phieunhap.CT_PHIEUHANGHOA)
                    {
                        item.HANGHOA.SoLuongTonKho += item._soLuong;
                        BAOCAOTONKHO bctk = DataProvider.Ins.DB.BAOCAOTONKHOes.Where(x => x.Thang == phieunhap.NgayLap.Value.Month && x.Nam == phieunhap.NgayLap.Value.Year && x.MaHH == item.MaHangHoa).SingleOrDefault();
                        bctk.TonCuoi = item.HANGHOA.SoLuongTonKho;
                        bctk.SoLuongNhapThem += item._soLuong;
                    }
                }
            }
        }
        void updateHangHoaTrongKhoPhieuXuat(PHIEUHANGHOA phieuxuat = null)
        {
            if (phieuxuat != null)
            {
                if (phieuxuat.TrangThai == "Đã hoàn thành" && selectedTrangThai != "Đã hoàn thành")
                {
                    foreach (var item in phieuxuat.CT_PHIEUHANGHOA)
                    {
                        item.HANGHOA.SoLuongTonKho += item._soLuong;
                    }
                }
                else if (phieuxuat.TrangThai != "Đã hoàn thành" && selectedTrangThai == "Đã hoàn thành")
                {
                    foreach (var item in phieuxuat.CT_PHIEUHANGHOA)
                    {
                        item.HANGHOA.SoLuongTonKho -= item._soLuong;
                        BAOCAOTONKHO bctk = DataProvider.Ins.DB.BAOCAOTONKHOes.Where(x => x.Thang == phieuxuat.NgayLap.Value.Month && x.Nam == phieuxuat.NgayLap.Value.Year && x.MaHH == item.MaHangHoa).SingleOrDefault();
                        bctk.TonCuoi = item.HANGHOA.SoLuongTonKho;
                        bctk.SoLuongXuatRa += item._soLuong;
                    }
                }
            }
        }
    }
}
