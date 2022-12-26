using FarmManagementSoftware.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using FarmManagementSoftware.View.Windows;
using System.Collections.Specialized;
using FarmManagementSoftware.View.Windows.Quản_lý_nhân_viên;

namespace FarmManagementSoftware.ViewModel
{
    public class ThemPhieuSuaChuaVM : BaseViewModel
    {
        #region Attributes
        private List<CT_PHIEUSUACHUA> cT_PHIEUSUACHUAs = new List<CT_PHIEUSUACHUA>();
        private ObservableCollection<CTPhieuModel> cTPhieuModels = new ObservableCollection<CTPhieuModel>();
        private string _TenNhanVien = "";
        private string _MaDoiTac = "";
        private string _TenDoiTac = "";
        private string _Email = "";
        private string _SDT = "";
        private string _DiaChiLienLac = "";
        private string _SoPhieu = "";
        private DateTime _NgayLapPhieu = DateTime.Now;
        private string _GhiChu = "";
        private int _TongTien = 0;
        private string _TrangThai = "Đang sửa chữa";
        private string _MaChuongCanTim = "";
        private bool _Flag = false;
        #endregion

        #region Property
        public List<CT_PHIEUSUACHUA> CT_PHIEUSUACHUAs { get => cT_PHIEUSUACHUAs; set { cT_PHIEUSUACHUAs = value; OnPropertyChanged(); } }
        public ObservableCollection<CTPhieuModel> CTPhieu { get => cTPhieuModels; set { cTPhieuModels = value; OnPropertyChanged(); } }
        public ObservableCollection<NHANVIEN> ListNhanVien { get; set; }
        public string TenNhanVien { get => _TenNhanVien; set { _TenNhanVien = value; OnPropertyChanged(); } }
        public bool Flag { get => _Flag; set { _Flag = value; OnPropertyChanged(); } }
        public string MaDoiTac { get => _MaDoiTac; set { _MaDoiTac = value; OnPropertyChanged(); } }
        public string TenDoiTac { get => _TenDoiTac; set { _TenDoiTac = value; OnPropertyChanged(); } }
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }
        public string DiaChiLienLac { get => _DiaChiLienLac; set { _DiaChiLienLac = value; OnPropertyChanged(); } }
        public string SoPhieu { get => _SoPhieu; set { _SoPhieu = value; OnPropertyChanged(); } }
        public DateTime NgayLapPhieu { get => _NgayLapPhieu; set { _NgayLapPhieu = value; OnPropertyChanged(); } }
        public string GhiChu { get => _GhiChu; set { _GhiChu = value; OnPropertyChanged(); } }
        public int TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }
        public string TrangThai { get => _TrangThai; set { _TrangThai = value; OnPropertyChanged(); } }
        public string MaChuongCanTim { get => _MaChuongCanTim; set { _MaChuongCanTim = value; OnPropertyChanged(); } }
        public int listviewSelectedIndex { get; set; }
        #endregion

        #region Command
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand HuyCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        public ICommand TimKiemTheoMaChuongCommand { get; set; }
        public ICommand XuatThongTinDoiTacCommand { get; set; }
        #endregion

        public ThemPhieuSuaChuaVM()
        {
            listviewSelectedIndex = 0;
            cT_PHIEUSUACHUAs = DataProvider.Ins.DB.CT_PHIEUSUACHUA.ToList();
            ListNhanVien = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);
            _SoPhieu = TaoSoPhieu();
            AddCommand = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                ChiTietPhieuSuaChua ctphieuSuaChua = new ChiTietPhieuSuaChua
                {
                    DataContext = new ChiTietPhieuSuaChuaVM(CTPhieu)
                };
                ctphieuSuaChua.ShowDialog();
            });
            XacNhanCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                var temp = new DOITAC() { MaDoiTac = MaDoiTac, TenDoiTac = TenDoiTac, SDT = SDT, DiaChi = DiaChiLienLac, Email = Email, LoaiDoiTac = "Đối tác sửa chữa" };
                DataProvider.Ins.DB.DOITACs.Add(temp);
                DataProvider.Ins.DB.SaveChanges();
                var item = new PHIEUSUACHUA() { MaNhanVien = LayMaNhanVien(TenNhanVien), MaDoiTac = MaDoiTac, NgaySuaChua = NgayLapPhieu, SoPhieu = SoPhieu, GhiChu = GhiChu, TongTien = TongTien, TrangThai = TrangThai };
                DataProvider.Ins.DB.PHIEUSUACHUAs.Add(item);
                DataProvider.Ins.DB.SaveChanges();
                cT_PHIEUSUACHUAs.Clear();
                foreach (var x in CTPhieu)
                {
                    var y = new CT_PHIEUSUACHUA() { SoPhieu = SoPhieu, MaChuong = x.MaChuong, MoTa = x.MoTa };
                    DataProvider.Ins.DB.CT_PHIEUSUACHUA.Add(y);
                    DataProvider.Ins.DB.SaveChanges();
                }
                MessageBox.Show("Đã thêm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                p.Close();
            });
            DeleteCommand = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                if (listviewSelectedIndex < 0)
                    return;
                var x = cTPhieuModels[listviewSelectedIndex];
                cTPhieuModels.Remove(x);
            });
            HuyCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                cT_PHIEUSUACHUAs.Clear();
                p.Close();
            });
            TimKiemTheoMaChuongCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                _MaChuongCanTim = p.Text;
                TimKiem();
            });
            XuatThongTinDoiTacCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                _MaDoiTac = p.Text;
                var temp = DataProvider.Ins.DB.DOITACs.Where(x => x.MaDoiTac.Trim().Equals(_MaDoiTac)).ToList();
                if (temp.Count > 0)
                {
                    var i = temp.First();
                    TenDoiTac = i.TenDoiTac;
                    Email = i.Email;
                    SDT = i.SDT;
                    DiaChiLienLac = i.DiaChi;
                    Flag = true;
                }
                else
                {
                    if (Flag == true)
                    {
                        TenDoiTac = "";
                        Email = "";
                        SDT = "";
                        DiaChiLienLac = "";
                        Flag = false;
                    }
                }
            });
        }
        string LayMaNhanVien(string ten)
        {
            string ma = "";
            var item = DataProvider.Ins.DB.NHANVIENs.Where(x => x.HoTen.Equals(ten)).ToList();
            foreach (var i in item)
            {
                ma = i.MaNhanVien;
            }
            return ma;
        }
        string TaoSoPhieu()
        {
            string soPhieu = "";
            var List = new List<PHIEUSUACHUA>(DataProvider.Ins.DB.PHIEUSUACHUAs);
            int sl = List.Count + 1;
            if (sl < 10)
            {
                soPhieu = "SC00000" + sl;
            }
            else if (sl < 100)
            {
                soPhieu = "SC0000" + sl;
            }
            else if (sl < 1000)
            {
                soPhieu = "SC000" + sl;
            }
            else if (sl < 10000)
            {
                soPhieu = "SC0" + sl;
            }
            else if (sl < 100000)
            {
                soPhieu = "SC0" + sl;
            }
            else if (sl < 1000000)
            {
                soPhieu = "SC" + sl;
            }
            return soPhieu;
        }
        void TimKiem()
        {
            var temp1 = CTPhieu;
            var temp2 = CTPhieu;
            int count = 0;
            CTPhieu.Clear();
            if (!String.IsNullOrWhiteSpace(_MaChuongCanTim))
            {
                foreach (var i in temp1)
                {
                    if (i.MaChuong.Contains(_MaChuongCanTim))
                    {
                        CTPhieu.Add(i);
                        count++;
                    }
                }
            }
            if (count == 0)
            {
                CTPhieu = temp2;
            }
        }
    }
}
