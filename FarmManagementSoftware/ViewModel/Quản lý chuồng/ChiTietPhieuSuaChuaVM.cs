using FarmManagementSoftware.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

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
           
        }
        public ChiTietPhieuSuaChuaVM(ObservableCollection<CT_PHIEUSUACHUA> a)
        {
            XacNhanCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                CT_PHIEUSUACHUA cT_PHIEUSUACHUA = new CT_PHIEUSUACHUA() { SoPhieu = SoPhieu, MaChuong = MaChuong, MoTa = Mota };
                a.Add(cT_PHIEUSUACHUA);
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
