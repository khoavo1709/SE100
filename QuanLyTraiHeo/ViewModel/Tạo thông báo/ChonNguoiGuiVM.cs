using QuanLyTraiHeo.Model;
using QuanLyTraiHeo.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyTraiHeo.ViewModel
{
    public class ChonNguoiGuiVM : BaseViewModel
    {
        #region Attributes
        private TaoThongBaoVM vmThongBao;
        private string _maChucVu = "20221024000001";
        private NHANVIEN _selectNGUOIGUI;
        private ObservableCollection<NguoiGui> _listNHANVIEN;
        private ObservableCollection<NHANVIEN> _listNGUOIGUI;

        private string _Ten = "";
        private DateTime _Ngaysinh;
        private DateTime _Ngayvaolam;
        #endregion

        #region Property
        public string maChucVu { get => _maChucVu; set { _maChucVu = value; OnPropertyChanged(); } }
        public NHANVIEN selectNGUOIGUI { get => _selectNGUOIGUI; set { _selectNGUOIGUI = value; OnPropertyChanged(); } }
        public ObservableCollection<NguoiGui> listNHANVIEN { get => _listNHANVIEN; set { _listNHANVIEN = value; OnPropertyChanged(); } }
        public ObservableCollection<NHANVIEN> listNGUOIGUI { get => _listNGUOIGUI; set { _listNGUOIGUI = value; OnPropertyChanged(); } }
  
        #endregion

        #region command
        public ICommand HoanTatCommand { get; set; }
        public ICommand TimKiemTheoTenCommand { get; set; }
        public ICommand TimKiemTheoNgaySinhCommand { get; set; }
        public ICommand TimKiemTheoNgayVaoLamCommand { get; set; }
        public ICommand btnHuyBoCommand { get; set; }
        #endregion
        public ChonNguoiGuiVM()
        {
            #region list Nhân viên
            loadDSNhanVien();
            #endregion
        }

        public ChonNguoiGuiVM(TaoThongBaoVM vm)
        {
            vmThongBao = vm;
            #region list Nhân viên
            if(vmThongBao.selectCHUCVU.TenChucVu != "Tất cả")
                maChucVu = vmThongBao.selectCHUCVU.MaChucVu;
            else maChucVu = null;
            loadDSNhanVien();
            listNGUOIGUI = new ObservableCollection<NHANVIEN>();
            #endregion

            #region code button hoàn tất
            HoanTatCommand = new RelayCommand<Window>((p) => { return true; }, p => {
                loadDSNGuoiGui();
                vmThongBao.ListNGUOIGUI = listNGUOIGUI;
                vmThongBao.loadTxtNGUOIGUI();
                p.Close();
            });
            #endregion

            #region code tìm kiếm tên
            TimKiemTheoTenCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                _Ten = p.Text;
                TimKiem();
            });
            #endregion

            #region code tìm kiếm ngày sinh
            TimKiemTheoNgaySinhCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                if (p.SelectedDate != null)
                    _Ngaysinh = p.SelectedDate.Value;
                else _Ngaysinh = DateTime.MinValue;
                TimKiem();
            });
            #endregion

            #region code tìm kiếm ngày vào làm
            TimKiemTheoNgayVaoLamCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                if (p.SelectedDate != null)
                    _Ngayvaolam = p.SelectedDate.Value;
                else _Ngayvaolam = DateTime.MinValue;
                TimKiem();
            });
            #endregion

            #region code btn Huỷ bỏ
            btnHuyBoCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                p.Close();
            });
            #endregion

        }

        void loadDSNhanVien()
        {
            listNHANVIEN = new ObservableCollection<NguoiGui>();

            //listNGUOIGUI = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs.Where(x => x.CHUCVU.MaChucVu == maChucVu));

            //var nhanviens = DataProvider.Ins.DB.NHANVIENs;
            var Nhanviens = DataProvider.Ins.DB.NHANVIENs.ToList();
            if (maChucVu!=null)
                Nhanviens = Nhanviens.Where(x => x.MaChucVu == maChucVu).ToList();

            foreach (var Nhanvien in Nhanviens)
            {
                NguoiGui nguoigui = new NguoiGui();
                nguoigui.nhanvien = Nhanvien;
                nguoigui.IsChecked = false;
                listNHANVIEN.Add(nguoigui);
            }   
        }


        void loadDSNGuoiGui()
        {
            foreach (var nguoi in listNHANVIEN)
            {
                if(nguoi.IsChecked == true)
                {
                    listNGUOIGUI.Add(nguoi.nhanvien);
                }
            }
        }

        void TimKiem()
        {
            listNHANVIEN.Clear();
            var Nhanviens = DataProvider.Ins.DB.NHANVIENs.ToList();
            if (maChucVu!=null)
                Nhanviens = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaChucVu == maChucVu).ToList();
            if(_Ten != "")
                Nhanviens = Nhanviens.Where(x => x.HoTen.Contains(_Ten) == true).ToList();
            if(_Ngaysinh.Year != 1)
                Nhanviens = Nhanviens.Where(x => x.NgaySinh.Value == _Ngaysinh).ToList();
            if(_Ngayvaolam.Year != 1)
                Nhanviens = Nhanviens.Where(x => x.NgayVaoLam.Value == _Ngayvaolam).ToList();

            foreach (var Nhanvien in Nhanviens)
            {
                NguoiGui nguoigui = new NguoiGui();
                nguoigui.nhanvien = Nhanvien;
                nguoigui.IsChecked = false;
                listNHANVIEN.Add(nguoigui);
            }
        }
    }
}