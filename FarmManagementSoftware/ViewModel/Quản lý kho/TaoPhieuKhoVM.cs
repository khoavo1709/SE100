using FarmManagementSoftware.Model;
using FarmManagementSoftware.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using FarmManagementSoftware.View.Windows.Quản_lý_kho;

namespace FarmManagementSoftware.ViewModel
{
    public class TaoPhieuKhoVM : BaseViewModel
    {
        LapPhieuKhoVM vm;
        public NHANVIEN NVThucHien { get; set; }
        private string _dongia;
        public string dongia { get => _dongia; set { _dongia = value; OnPropertyChanged(); } }
        private string _soluong;
        public string soluong { get => _soluong; set { _soluong = value; OnPropertyChanged(); } }
        private string _soluongKT;
        public string soluongKT { get => _soluongKT; set { _soluongKT = value; OnPropertyChanged(); } }

        // Thông tin phiếu
        private string _SoPhieu;
        public string SoPhieu { get => _SoPhieu; set { _SoPhieu = value; OnPropertyChanged(); } }
        private string _selectedloaiPhieu;
        public string selectedLoaiPhieu { get => _selectedloaiPhieu; set { _selectedloaiPhieu = value; OnPropertyChanged(); } }
        private DateTime _NgayLap;
        public DateTime NgayLap { get => _NgayLap; set { _NgayLap = value; OnPropertyChanged(); } }
        private int? _Tongtien;
        public int? TongTien { get => _Tongtien; set { _Tongtien = value; OnPropertyChanged(); } }
        private ComboBoxItem _KQ;
        public ComboBoxItem KQ { get => _KQ; set { _KQ = value; OnPropertyChanged(); } }
        private string _ghiChu;
        public string ghiChu { get => _ghiChu; set { _ghiChu = value; OnPropertyChanged(); } }

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

        private ObservableCollection<CT_PHIEUHANGHOA> _ctHHs;
        public ObservableCollection<CT_PHIEUHANGHOA> CTHHs { get => _ctHHs; set { _ctHHs = value; OnPropertyChanged(); } }

        private CT_PHIEUHANGHOA _selectCTHH;
        public CT_PHIEUHANGHOA selectCTHH { get => _selectCTHH; set { _selectCTHH = value; OnPropertyChanged(); } }

        private ObservableCollection<CT_PHIEUKIEMKHO> _ctKKs;
        public ObservableCollection<CT_PHIEUKIEMKHO> CTKKs { get => _ctKKs; set { _ctKKs = value; OnPropertyChanged(); } }

        private CT_PHIEUKIEMKHO _selectCTKK;
        public CT_PHIEUKIEMKHO selectCTKK { get => _selectCTKK; set { _selectCTKK = value; OnPropertyChanged(); } }

        public ICommand btnThemHHcommand { get; set; }
        public ICommand editDonGiacommand { get; set; }
        public ICommand editSoLuongcommand { get; set; }
        public ICommand deletecommand { get; set; }
        public ICommand changeLoaiPhieucommand { get; set; }
        public ICommand changeMaKHcommand { get; set; }
        public ICommand btnHoanTatcommand { get; set; }
        public ICommand changeMaNVNcommand { get; set; }
        public ICommand editSLKTcommand { get; set; }

        public TaoPhieuKhoVM()
        {

        }
        public TaoPhieuKhoVM(LapPhieuKhoVM vm)
        {
            this.vm = vm;
            NVThucHien = Account.TaiKhoan;
            CTHHs = new ObservableCollection<CT_PHIEUHANGHOA>();
            CTKKs = new ObservableCollection<CT_PHIEUKIEMKHO>();
            NgayLap = DateTime.Now;
            TongTien = 0;

            #region command Thêm hàng hoá
            btnThemHHcommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if (selectedLoaiPhieu == null)
                { MessageBox.Show("Bạn phải chọn loại phiếu để thực hiện trước"); return; }
                ChonHangHoaWindow wc = new ChonHangHoaWindow();
                ChonHangHoaVM CHHvm = new ChonHangHoaVM(this);
                wc.DataContext = CHHvm;
                wc.ShowDialog();
            });
            #endregion

            #region command sửa đơn giá
            editDonGiacommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                if (dongia != null && dongia != "" && selectCTHH != null)
                {
                    selectCTHH._donGia = int.Parse(dongia);
                }
                dongia = null;
                TinhTongTien();
            });
            #endregion

            #region command sửa số lượng
            editSoLuongcommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                if (int.Parse(soluong) > selectCTHH.HANGHOA.SoLuongTonKho)
                {
                    MessageBox.Show("Số lượng của mặt hàng này không đủ!");
                    return;
                }
                if (soluong != null && soluong != "" && selectCTHH != null)
                {
                    selectCTHH._soLuong = int.Parse(soluong);
                }
                soluong = null;
                TinhTongTien();
            });
            #endregion

            #region command sửa số lượng kiểm tra
            editSLKTcommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                if (soluongKT != null && soluongKT != "" && selectCTKK != null)
                {
                    selectCTKK._soLuongKiemTra = int.Parse(soluongKT);
                }
                soluongKT = null;
                TinhTongTien();
            });
            #endregion

            #region command xoá ctHH
            deletecommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                CTHHs.Remove(selectCTHH);
                selectCTHH = null;
            });
            #endregion

            #region command changed loại phiếu
            changeLoaiPhieucommand = new RelayCommand<ComboBox>((p) => { return true; }, p =>
            {
                ComboBoxItem item = (ComboBoxItem)p.SelectedItem;
                selectedLoaiPhieu = item.Content.ToString();
                switch (selectedLoaiPhieu)
                {
                    case "Phiếu nhập kho":
                        SoPhieu = createMaPhieuNhapKho();
                        break;
                    case "Phiếu xuất nội":
                        SoPhieu = createMaPhieuXuatNoi();
                        break;
                    case "Phiếu xuất ngoại":
                        SoPhieu = createMaPhieuXuatNgoai();
                        break;
                    case "Phiếu kiểm kho":
                        SoPhieu = createMaPhieuKiemKho();
                        break;
                }
                p.IsEnabled = false;
            });
            #endregion

            #region command changed maKH
            changeMaKHcommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                var KH = DataProvider.Ins.DB.DOITACs.Where(x => x.MaDoiTac == p.Text).SingleOrDefault();
                if (KH != null)
                {
                    tenKH = KH.TenDoiTac;
                    email = KH.Email;
                    sdt = KH.SDT;
                    diaChi = KH.DiaChi;
                }
            });
            #endregion

            #region command change mã NVN
            changeMaNVNcommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                var NV = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNhanVien == p.Text).SingleOrDefault();
                if (NV != null)
                {
                    tenNVN = NV.HoTen;
                }
            });
            #endregion

            #region command btn hoàn tất
            btnHoanTatcommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if (selectedLoaiPhieu == null)
                {
                    MessageBox.Show("Bạn chưa chọn loại phiếu để thực hiện");
                    return;
                }
                if (NgayLap >= DateTime.Now)
                {
                    MessageBox.Show("Ngày lập không thể lớn hơn hôm nay");
                    return;
                }
                var baocaotonkho = DataProvider.Ins.DB.BAOCAOTONKHOes.Where(x => x.Thang == NgayLap.Month && x.Nam == NgayLap.Year).Count();
                if (baocaotonkho <= 0)
                {
                    var dsHangHoa = DataProvider.Ins.DB.HANGHOAs.ToList();
                    foreach (var d in dsHangHoa)
                    {
                        BAOCAOTONKHO bctk = new BAOCAOTONKHO();
                        bctk.MaBCTK = createMaBCTK();
                        bctk.Thang = DateTime.Today.Month;
                        bctk.Nam = DateTime.Today.Year;
                        bctk.MaHH = d.MaHangHoa;
                        bctk.TonDau = d.SoLuongTonKho;
                        bctk.TonCuoi = d.SoLuongTonKho;
                        bctk.SoLuongNhapThem = 0;
                        bctk.SoLuongXuatRa = 0;
                        DataProvider.Ins.DB.BAOCAOTONKHOes.Add(bctk);
                        DataProvider.Ins.DB.SaveChanges();
                    }

                }
                switch (selectedLoaiPhieu)
                {
                    case "Phiếu nhập kho":
                        if (AddphieuNhapKho())
                            p.Close();
                        break;
                    case "Phiếu xuất nội":
                        if (AddphieuXuatNoi())
                            p.Close();
                        break;
                    case "Phiếu xuất ngoại":
                        if (AddphieuXuatNgoai())
                            p.Close();
                        break;
                    case "Phiếu kiểm kho":
                        if (AddphieuKiemKho())
                            p.Close();
                        break;
                }

            });
            #endregion
        }

        string createMaPhieuNhapKho()
        {
            string MaPhieu = "";
            var pHIEUHANGHOAs = DataProvider.Ins.DB.PHIEUHANGHOAs.Where(x => x.LoaiPhieu == "Phiếu nhập kho").ToList();
            if (pHIEUHANGHOAs.Count == 0)
            {
                MaPhieu = "NK000001";
            }
            else
            {
                int STT = pHIEUHANGHOAs.Count;
                do
                {
                    STT++;
                    string strSTT = STT.ToString();
                    for (int i = strSTT.Length; i <= 6; i++)
                    {
                        strSTT = "0" + strSTT;
                    }

                    MaPhieu = "NK" + strSTT;
                }
                while (DataProvider.Ins.DB.PHIEUHANGHOAs.Count(x => x.SoPhieu == MaPhieu) > 0);

            }

            return MaPhieu;
        }

        string createMaPhieuXuatNoi()
        {
            string MaPhieu = "";
            var pHIEUHANGHOAs = DataProvider.Ins.DB.PHIEUHANGHOAs.Where(x => x.LoaiPhieu == "Phiếu xuất nội").ToList();
            if (pHIEUHANGHOAs.Count == 0)
            {
                MaPhieu = "XN000001";
            }
            else
            {
                int STT = pHIEUHANGHOAs.Count;
                do
                {
                    STT++;
                    string strSTT = STT.ToString();
                    for (int i = strSTT.Length; i <= 6; i++)
                    {
                        strSTT = "0" + strSTT;
                    }

                    MaPhieu = "XN" + strSTT;
                }
                while (DataProvider.Ins.DB.PHIEUHANGHOAs.Count(x => x.SoPhieu == MaPhieu) > 0);
            }
            return MaPhieu;
        }

        string createMaPhieuXuatNgoai()
        {
            string MaPhieu = "";
            var pHIEUHANGHOAs = DataProvider.Ins.DB.PHIEUHANGHOAs.Where(x => x.LoaiPhieu == "Phiếu xuất ngoại").ToList();
            if (pHIEUHANGHOAs.Count == 0)
            {
                MaPhieu = "XNG00001";
            }
            else
            {
                int STT = pHIEUHANGHOAs.Count;
                do
                {
                    STT++;
                    string strSTT = STT.ToString();
                    for (int i = strSTT.Length; i <= 5; i++)
                    {
                        strSTT = "0" + strSTT;
                    }

                    MaPhieu = "XNG" + strSTT;
                }
                while (DataProvider.Ins.DB.PHIEUHANGHOAs.Count(x => x.SoPhieu == MaPhieu) > 0);
            }
            return MaPhieu;
        }

        string createMaPhieuKiemKho()
        {
            string MaPhieu = "";
            var pHIEUHANGHOAs = DataProvider.Ins.DB.PHIEUKIEMKHOes.ToList();
            if (pHIEUHANGHOAs.Count == 0)
            {
                MaPhieu = "KK000001";
            }
            else
            {
                int STT = pHIEUHANGHOAs.Count;
                do
                {
                    STT++;
                    string strSTT = STT.ToString();
                    for (int i = strSTT.Length; i <= 6; i++)
                    {
                        strSTT = "0" + strSTT;
                    }

                    MaPhieu = "KK" + strSTT;
                }
                while (DataProvider.Ins.DB.PHIEUHANGHOAs.Count(x => x.SoPhieu == MaPhieu) > 0);
            }
            return MaPhieu;
        }

        string createMaBCTK()
        {
            string MaPhieu = "";
            var pHIEUHANGHOAs = DataProvider.Ins.DB.PHIEUKIEMKHOes.ToList();
            if (pHIEUHANGHOAs.Count == 0)
            {
                MaPhieu = "BCTK000001";
            }
            else
            {
                int STT = pHIEUHANGHOAs.Count;
                do
                {
                    STT++;
                    string strSTT = STT.ToString();
                    for (int i = strSTT.Length; i < 6; i++)
                    {
                        strSTT = "0" + strSTT;
                    }

                    MaPhieu = "BCTK" + strSTT;
                }
                while (DataProvider.Ins.DB.BAOCAOTONKHOes.Count(x => x.MaBCTK == MaPhieu) > 0);
            }
            return MaPhieu;
        }
        string nextMaBCTK(string maPhieu)
        {
            string MaPhieu = "";
            string strSTT = maPhieu.Substring(4);
            int STT = int.Parse(strSTT);
            do
            {
                STT++;
                strSTT = STT.ToString();
                for (int i = strSTT.Length; i <= 6; i++)
                {
                    strSTT = "0" + strSTT;
                }

                MaPhieu = "BCTK" + strSTT;
            }
            while (DataProvider.Ins.DB.PHIEUHANGHOAs.Count(x => x.SoPhieu == MaPhieu) > 0);
            return MaPhieu;
        }

        public void TinhTongTien()
        {
            TongTien = 0;
            foreach (var item in CTHHs)
            {
                TongTien += item._donGia * item._soLuong;
            }
        }

        bool AddphieuNhapKho()
        {
            if (maKH == null || tenKH == null || email == null || sdt == null || diaChi == null)
            {
                MessageBox.Show("Bạn chưa điền đầy đủ thông tin khách hàng");
                return false;
            }
            foreach (var item in CTHHs)
            {
                CT_PHIEUHANGHOA ct = new CT_PHIEUHANGHOA();
                ct.MaHangHoa = item.MaHangHoa;
                ct.SoPhieu = SoPhieu;
                ct._soLuong = item._soLuong;
                ct._donGia = item._donGia;

                DataProvider.Ins.DB.CT_PHIEUHANGHOA.Add(ct);
            }

            PHIEUHANGHOA phieu = new PHIEUHANGHOA();
            phieu.SoPhieu = SoPhieu;
            phieu.MaDoiTac = maKH;
            phieu.DOITAC = DataProvider.Ins.DB.DOITACs.Where(x => x.MaDoiTac == maKH).SingleOrDefault();
            phieu.MaNhanVien = NVThucHien.MaNhanVien;
            phieu.NHANVIEN = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNhanVien == NVThucHien.MaNhanVien).SingleOrDefault();
            phieu.NgayLap = NgayLap;
            phieu.LoaiPhieu = selectedLoaiPhieu.ToString();
            phieu.TrangThai = "Chưa hoàn thành";
            phieu.TongTien = TongTien;

            DataProvider.Ins.DB.PHIEUHANGHOAs.Add(phieu);
            DataProvider.Ins.DB.SaveChanges();

            vm.dsPhieuNhap.Add(phieu);
            vm.ForcusdsNhap();
            MessageBox.Show("Đã phiếu thành công");
            return true;
        }

        bool AddphieuXuatNoi()
        {
            if (maNVN == null)
            {
                MessageBox.Show("Bạn chưa nhập thông tin nhân viên");
                return false;
            }
            else if (tenNVN == null)
            {
                MessageBox.Show("Không tìm thầy mã nhân viên này trong hệ thống");
                return false;
            }
            foreach (var item in CTHHs)
            {
                CT_PHIEUHANGHOA ct = new CT_PHIEUHANGHOA();
                ct.MaHangHoa = item.MaHangHoa;
                ct.SoPhieu = SoPhieu;
                ct._soLuong = item._soLuong;
                ct._donGia = item._donGia;

                DataProvider.Ins.DB.CT_PHIEUHANGHOA.Add(ct);
            }

            PHIEUHANGHOA phieu = new PHIEUHANGHOA();
            phieu.SoPhieu = SoPhieu;
            phieu.MaNhanVienNhan = maNVN;
            phieu.NHANVIEN1 = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNhanVien == maNVN).SingleOrDefault();
            phieu.MaNhanVien = NVThucHien.MaNhanVien;
            phieu.NHANVIEN = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNhanVien == NVThucHien.MaNhanVien).SingleOrDefault();
            phieu.NgayLap = NgayLap;
            phieu.LoaiPhieu = selectedLoaiPhieu.ToString();
            phieu.TrangThai = "Chưa hoàn thành";
            phieu.TongTien = TongTien;

            DataProvider.Ins.DB.PHIEUHANGHOAs.Add(phieu);
            DataProvider.Ins.DB.SaveChanges();

            vm.dsPhieuXuat.Add(phieu);
            MessageBox.Show("Đã phiếu thành công");
            return true;
        }

        bool AddphieuXuatNgoai()
        {
            if (maKH == null || tenKH == null || email == null || sdt == null || diaChi == null)
            {
                MessageBox.Show("Bạn chưa điền đầy đủ thông tin khách hàng");
                return false;
            }
            foreach (var item in CTHHs)
            {
                CT_PHIEUHANGHOA ct = new CT_PHIEUHANGHOA();
                ct.MaHangHoa = item.MaHangHoa;
                ct.SoPhieu = SoPhieu;
                ct._soLuong = item._soLuong;
                ct._donGia = item._donGia;

                DataProvider.Ins.DB.CT_PHIEUHANGHOA.Add(ct);
            }

            PHIEUHANGHOA phieu = new PHIEUHANGHOA();
            phieu.SoPhieu = SoPhieu;
            phieu.MaDoiTac = maKH;
            phieu.DOITAC = DataProvider.Ins.DB.DOITACs.Where(x => x.MaDoiTac == maKH).SingleOrDefault();
            phieu.MaNhanVien = NVThucHien.MaNhanVien;
            phieu.NHANVIEN = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNhanVien == NVThucHien.MaNhanVien).SingleOrDefault();
            phieu.NgayLap = NgayLap;
            phieu.LoaiPhieu = selectedLoaiPhieu.ToString();
            phieu.TrangThai = "Chưa hoàn thành";
            phieu.TongTien = TongTien;

            DataProvider.Ins.DB.PHIEUHANGHOAs.Add(phieu);
            DataProvider.Ins.DB.SaveChanges();

            vm.dsPhieuXuat.Add(phieu);
            MessageBox.Show("Đã phiếu thành công");
            return true;
        }

        bool AddphieuKiemKho()
        {
            if (KQ == null || KQ.Content.ToString() == "")
            {
                MessageBox.Show("Bạn chưa nhập kết quả kiểm kho");
                return false;
            }
            foreach (var item in CTKKs)
            {
                CT_PHIEUKIEMKHO ct = new CT_PHIEUKIEMKHO();
                ct.MaHangHoa = item.MaHangHoa;
                ct.SoPhieu = SoPhieu;
                ct.SoLuongHienCo = item.SoLuongHienCo;
                ct._soLuongKiemTra = item._soLuongKiemTra;
                if (item._soLuongKiemTra == null)
                {
                    MessageBox.Show("Bạn chưa nhập số lượng kiểm tra của hàng hoá có mã: " + ct.MaHangHoa);
                    return false;
                }

                DataProvider.Ins.DB.CT_PHIEUKIEMKHO.Add(ct);
            }

            PHIEUKIEMKHO phieu = new PHIEUKIEMKHO();

            phieu.SoPhieu = SoPhieu;
            phieu.NgayLap = NgayLap;
            phieu.MaNhanVien = NVThucHien.MaNhanVien;
            phieu.NHANVIEN = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNhanVien == NVThucHien.MaNhanVien).SingleOrDefault();
            phieu.KetQua = KQ.Content.ToString();
            phieu.GhiChu = ghiChu;

            DataProvider.Ins.DB.PHIEUKIEMKHOes.Add(phieu);
            DataProvider.Ins.DB.SaveChanges();

            MessageBox.Show("Đã thêm phiếu thành công");
            vm.dsPhieuKiem.Add(phieu);

            return true;
        }
    }
}
