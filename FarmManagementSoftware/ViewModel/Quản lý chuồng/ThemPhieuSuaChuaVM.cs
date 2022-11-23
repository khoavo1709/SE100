using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace FarmManagementSoftware.ViewModel
{
    public class ThemPhieuSuaChuaVM : BaseViewModel
    {
        #region Attributes
        private List<CT_PHIEUSUACHUA> cT_PHIEUSUACHUAs = new List<CT_PHIEUSUACHUA>();
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
        private string _TrangThai = "";
        #endregion

        #region Property
        public List<CT_PHIEUSUACHUA> CT_PHIEUSUACHUAs { get => cT_PHIEUSUACHUAs; set { cT_PHIEUSUACHUAs = value; OnPropertyChanged(); } }
        public string TenNhanVien { get => _TenNhanVien; set { _TenNhanVien = value; OnPropertyChanged(); } }
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
        #endregion

        #region Command
        public ICommand AddCommand { get; set; }
        public ICommand HuyCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        #endregion

        public ThemPhieuSuaChuaVM()
        {
            cT_PHIEUSUACHUAs = DataProvider.Ins.DB.CT_PHIEUSUACHUA.ToList();
            _SoPhieu = TaoSoPhieu();
            AddCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                ChiTietPhieuSuaChua ctphieuSuaChua = new ChiTietPhieuSuaChua();
                ctphieuSuaChua.ShowDialog();
            });
            XacNhanCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                var item = new PHIEUSUACHUA() { MaNhanVien = TenNhanVien, MaDoiTac = MaDoiTac, NgaySuaChua = NgayLapPhieu, SoPhieu = SoPhieu, GhiChu = GhiChu, TongTien = TongTien, TrangThai = TrangThai };
                DataProvider.Ins.DB.PHIEUSUACHUAs.Add(item);
                DataProvider.Ins.DB.SaveChanges();
                cT_PHIEUSUACHUAs.Clear();
                MessageBox.Show("Đã thêm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                p.Close();
            });
            HuyCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                cT_PHIEUSUACHUAs.Clear();
                p.Close();
            });
        }
        string TaoSoPhieu()
        {
            int soPhieu = 0;
            var List = new List<PHIEUSUACHUA>(DataProvider.Ins.DB.PHIEUSUACHUAs);
            soPhieu = List.Count + 1;
            return soPhieu.ToString();
        }
    }
}
