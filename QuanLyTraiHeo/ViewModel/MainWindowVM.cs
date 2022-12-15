using MaterialDesignThemes.Wpf;
using QuanLyTraiHeo.Model;
using QuanLyTraiHeo.View.Windows;
using QuanLyTraiHeo.View.Windows.Quản_lý_chức_vụ;
using QuanLyTraiHeo.View.Windows.Quản_lý_giống_heo;
using QuanLyTraiHeo.View.Windows.Quản_lý_loại_heo;
using QuanLyTraiHeo.View.Windows.Quy_Định;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp_MVVM.View.Windows;

namespace QuanLyTraiHeo.ViewModel
{
    public class MainWindowVM: BaseViewModel
    {
        #region Attributes
        public bool IsLoaded = false;
        private string _currentWindow = "";
        NHANVIEN nhanVien;
        System.Windows.Media.Imaging.BitmapImage image;
        private ObservableCollection<ThongBao> _listTHONGBAO;
        private int _countThongBaoChuaDoc;
        private ThongBao _selectedItem;
        #endregion

        #region Property
        public string currentWindow { get => _currentWindow; set { _currentWindow = value; OnPropertyChanged(); } }
        public NHANVIEN NhanVien { get => nhanVien; set { nhanVien = value; OnPropertyChanged(); } }
        public System.Windows.Media.Imaging.BitmapImage MyImage { get => image; set { image = value; OnPropertyChanged(); } }
        public ObservableCollection<ThongBao> listTHONGBAO { get => _listTHONGBAO; set { _listTHONGBAO = value; OnPropertyChanged(); } }
        public int countThongBaoChuaDoc { get => _countThongBaoChuaDoc; set { _countThongBaoChuaDoc = value; OnPropertyChanged(); } }
        public ThongBao selectedItem { get => _selectedItem; set { _selectedItem = value; OnPropertyChanged(); } }  
        #endregion

        #region CommandOpenWindow
        public ICommand OpenTrangChuWindow { get; set; }
        public ICommand OpenQuanLyThongTinCaTheWindow { get; set; }
        public ICommand OpenQuanLyLoaiHeo { get; set; }
        public ICommand OpenQuanLyGiongHeo { get; set; }
        public ICommand OpenLapPhieuBanNhapHeoWIndow { get; set; }
        public ICommand OpenLapLichTiemWindow { get; set; } 
        public ICommand OpenLaplichPhoiGiongWindow { get; set; }   
        public ICommand OpenQuanLyThongTinChuongWindow { get; set; }
        public ICommand OpenSoDoChuongWindow { get; set; }
        public ICommand OpenLapPhieuSuaChuaWindow { get; set; }
        public ICommand OpenQuanLyHangHoaTrongKhoWindow { get; set; }
        public ICommand OpenLapPhieuKhoWindow { get; set; }
        public ICommand OpenBaoCaoTinhTrangDanHeoWindow { get; set; }
        public ICommand OpenBaoCaoSuaChuaWindow { get; set; }
        public ICommand OpenBaoCaoThuChiWindow { get; set; }
        public ICommand OpenBaoCaoTonKhoWindow { get; set; }
        public ICommand OpenQuanLyThongTinNhanVienWindow { get; set; }
        public ICommand OpenQuanLyChucVu { get; set; }
        public ICommand OpenQuanLyNhatKyWindow { get; set; }
        public ICommand OpenThietLapCayMucTieuWindow { get; set; }
        public ICommand OpenQuyDinhWindow { get; set; }

        public ICommand OpenCapNhatTaiKhoan { get; set; }
        public ICommand OpenDoiMatKhau { get; set; }
        #endregion

        #region Event Command
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand OpenCTThongBaoCommand { get; set; }
        public ICommand OpenTaoThongBaoCommand { get; set; }
        #endregion]

        public MainWindowVM()   
        {
            currentWindow = "Trang chủ";
            
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, p => {
                IsLoaded = true;
                p.Hide();
                
                wLogin wLogin = new wLogin();
                wLogin.ShowDialog();

                if (wLogin.DataContext == null) return;

                var loginWD = wLogin.DataContext as LoginVM;

                if (loginWD.IsLogin)
                {
                    p.Show();

                    NhanVien = loginWD.NhanVien;
                    MyImage = CapNhatTaiKhoanVM.BytesToBitmapImage(NhanVien.BytesImage);

                    listTHONGBAO = new ObservableCollection<ThongBao>(DataProvider.Ins.DB.ThongBaos.Where(x => x.C_MaNguoiNhan == NhanVien.MaNhanVien));

                    loadCountThongBao();
                }
                else
                {
                    p.Close();
                }

            });

            CodeCommandOpenWindow();
        }

        #region Method
        public void UpdateNhanVien()
        {
            OnPropertyChanged("NhanVien");
            OnPropertyChanged("MyImage");
        }

        public void loadCountThongBao()
        {
            var listTHONGBAOchuadoc = new ObservableCollection<ThongBao>(DataProvider.Ins.DB.ThongBaos.Where(x => x.TinhTrang == "Chưa đọc" && x.C_MaNguoiNhan == NhanVien.MaNhanVien));
            countThongBaoChuaDoc = listTHONGBAOchuadoc.Count;
        }

        void CodeCommandOpenWindow()
        {
            OpenTrangChuWindow = new RelayCommand<Grid>((p) => { return true; }, p => {
                TrangChuWindow wc = new TrangChuWindow();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Trang chủ";
            });
            OpenQuanLyThongTinCaTheWindow = new RelayCommand<Grid>((p) => { return true; }, p => {
                QuanLyThongTinCaTheWindow wc = new QuanLyThongTinCaTheWindow();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Quản lý thông tin cá thể";
            });
            OpenQuanLyLoaiHeo = new RelayCommand<Grid>((p) => { return true; }, p => {
                Quanlyloaiheo wc = new Quanlyloaiheo();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Quản lý thông tin loại heo";
            });
            OpenQuanLyGiongHeo = new RelayCommand<Grid>((p) => { return true; }, p => {
                Quanlygiongheo wc = new Quanlygiongheo();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Quản lý thông tin giống heo";
            });
            OpenLapPhieuBanNhapHeoWIndow = new RelayCommand<Grid>((p) => { return true; }, p => {
                LapPhieuBanNhapHeoWindow wc = new LapPhieuBanNhapHeoWindow();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Lập phiếu bán/nhập heo";
            });
            OpenLapLichTiemWindow = new RelayCommand<Grid>((p) => { return true; }, p => {
                LapLichTiem wc = new LapLichTiem();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Lập lịch tiêm heo";
            });
            OpenLaplichPhoiGiongWindow = new RelayCommand<Grid>((p) => { return true; }, p => {
                LichPhoiGiongWindow wc = new LichPhoiGiongWindow();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Lập lịch phối giống heo";
            });
            OpenQuanLyThongTinChuongWindow = new RelayCommand<Grid>((p) => { return true; }, p => {
                QuanLyThongTinChuong wc = new QuanLyThongTinChuong();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Quản lý thông tin chuồng nuôi";
            });
            OpenSoDoChuongWindow = new RelayCommand<Grid>((p) => { return true; }, p => {
                wSoDo wc = new wSoDo();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Sơ đồ chuồng nuôi";
            });
            OpenLapPhieuSuaChuaWindow = new RelayCommand<Grid>((p) => { return true; }, p => {
                LapPhieuSuaChuaWindow wc = new LapPhieuSuaChuaWindow();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Lập phiếu sửa chữa";
            });
            OpenQuanLyHangHoaTrongKhoWindow = new RelayCommand<Grid>((p) => { return true; }, p => {
                QuanLyHangHoaWindow wc = new QuanLyHangHoaWindow();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Quản lý hàng hoá trong kho";
            });
            OpenLapPhieuKhoWindow = new RelayCommand<Grid>((p) => { return true; }, p => {
                LapPhieuKhoWindow wc = new LapPhieuKhoWindow();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Lập phiếu kho";
            });
            OpenBaoCaoTinhTrangDanHeoWindow = new RelayCommand<Grid>((p) => { return true; }, p => {
                BaoCaoTinhTrangDanHeoWindow wc = new BaoCaoTinhTrangDanHeoWindow();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Lập báo cáo tình trạng đàn heo";
            });
            OpenBaoCaoSuaChuaWindow = new RelayCommand<Grid>((p) => { return true; }, p => {
                BaoCaoSuaChuaWindow wc = new BaoCaoSuaChuaWindow();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Lập báo cáo sửa chữa";
            });
            OpenBaoCaoThuChiWindow = new RelayCommand<Grid>((p) => { return true; }, p => {
                BaoCaoThuChiWindow wc = new BaoCaoThuChiWindow();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Lập lịch tiêm/phối giống heo";
            });
            OpenBaoCaoTonKhoWindow = new RelayCommand<Grid>((p) => { return true; }, p => {
                BaoCaoTonKhoWindow wc = new BaoCaoTonKhoWindow();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Lập báo cáo tồn kho";
            });
            OpenQuanLyThongTinNhanVienWindow = new RelayCommand<Grid>((p) => { return true; }, p => {
                QuanLyThongTinNhanVienWindow wc = new QuanLyThongTinNhanVienWindow();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Quản lý thông tin nhân viên";
            });
            OpenQuanLyChucVu = new RelayCommand<Grid>((p) => { return true; }, p => {
                Quanlychucvu wc = new Quanlychucvu();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Quản lý thông tin chức vụ";
            });
            OpenQuanLyNhatKyWindow = new RelayCommand<Grid>((p) => { return true; }, p => {
                QuanLyNhatKyWindow wc = new QuanLyNhatKyWindow();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Quản lý nhật ký";
            });
            OpenThietLapCayMucTieuWindow = new RelayCommand<Grid>((p) => { return true; }, p => {
                ThietLapCayMucTieuWindow wc = new ThietLapCayMucTieuWindow();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Thiết lập cây mục tiêu";
            });
            OpenQuyDinhWindow = new RelayCommand<Grid>((p) => { return true; }, p => {
                QuyDinhWindow wc = new QuyDinhWindow();
                wc.Close();
                Object content = wc.Content;
                wc.Content = null;
                p.Children.Clear();
                p.Children.Add(content as UIElement);
                currentWindow = "Thiết lập cây mục tiêu";
            });

            OpenCapNhatTaiKhoan = new RelayCommand<Window>((p) => { return true; }, p => {
                CapNhatTaiKhoanWindow wc = new CapNhatTaiKhoanWindow();
                CapNhatTaiKhoanVM capNhatTaiKhoanVM = new CapNhatTaiKhoanVM(this);
                wc.DataContext = capNhatTaiKhoanVM;
                wc.ShowDialog();

            });

            OpenDoiMatKhau = new RelayCommand<Window>((p) => { return true; }, p => {
                DoiMatKhau wc = new DoiMatKhau();
                DoiMatKhauVM capNhatTaiKhoanVM = new DoiMatKhauVM(this);
                wc.DataContext = capNhatTaiKhoanVM;
                wc.ShowDialog();

            });

            OpenCTThongBaoCommand = new RelayCommand<Window>((p) => { return true; }, p => {
                if (selectedItem != null)
                {
                    ChitTietThongBaoWindow wc = new ChitTietThongBaoWindow();
                    ChiTietThongBaoVM vm = new ChiTietThongBaoVM(this);
                    wc.DataContext = vm;
                    wc.ShowDialog();
                }
            });
            OpenTaoThongBaoCommand = new RelayCommand<Window>((p) => { return true; }, p => {
                TaoThongBaoWindow wc = new TaoThongBaoWindow();
                TaoThongBaoVM vm = new TaoThongBaoVM();
                vm.nguoigui = NhanVien;
                wc.DataContext = vm;
                wc.ShowDialog();
            });
        }
        #endregion
    }
}
