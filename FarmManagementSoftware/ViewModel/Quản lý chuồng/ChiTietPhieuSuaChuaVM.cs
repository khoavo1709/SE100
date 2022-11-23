using FarmManagementSoftware.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace FarmManagementSoftware.ViewModel
{
    public class ChiTietPhieuSuaChuaVM : BaseViewModel
    {
        #region Attributes
        string _MaChuong = "";
        string _Mota = "";
        string _SoPhieu = "";
        #endregion

        #region Property
        public string MaChuong { get => _MaChuong; set { _MaChuong = value; OnPropertyChanged(); } }
        public string Mota { get => _Mota; set { _Mota = value; OnPropertyChanged(); } }
        public string SoPhieu { get => _SoPhieu; set { _SoPhieu = value; OnPropertyChanged(); } }
        #endregion

        #region Command
        public ICommand XacNhanCommand { get; set; }
        public ICommand HuyCommand { get; set; }
        #endregion

        public ChiTietPhieuSuaChuaVM()
        {
            XacNhanCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                CT_PHIEUSUACHUA cT_PHIEUSUACHUA = new CT_PHIEUSUACHUA() { SoPhieu = SoPhieu, MaChuong = MaChuong, MoTa = Mota };
                DataProvider.Ins.DB.CT_PHIEUSUACHUA.Add(cT_PHIEUSUACHUA);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Đã thêm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                p.Close();
            });
            HuyCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
        }
    }
}
