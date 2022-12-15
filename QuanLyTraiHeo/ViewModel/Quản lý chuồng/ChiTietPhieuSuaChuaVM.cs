using QuanLyTraiHeo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace QuanLyTraiHeo.ViewModel
{
    public class ChiTietPhieuSuaChuaVM : BaseViewModel
    {
        #region Attributes
        string _MaChuong = "";
        string _MoTa = "";
        //string _SoPhieu = "";
        #endregion

        #region Property
        public string MaChuong { get => _MaChuong; set { _MaChuong = value; OnPropertyChanged(); } }
        public string MoTa { get => _MoTa; set { _MoTa = value; OnPropertyChanged(); } }
        public ObservableCollection<CHUONGTRAI> ListChuongTrai { get; set; }
        //public string SoPhieu { get => _SoPhieu; set { _SoPhieu = value; OnPropertyChanged(); } }
        #endregion

        #region Command
        public ICommand XacNhanCommand { get; set; }
        public ICommand HuyCommand { get; set; }
        #endregion

        public ChiTietPhieuSuaChuaVM(ObservableCollection<CTPhieuModel> vm)
        {
            ListChuongTrai = new ObservableCollection<CHUONGTRAI>(DataProvider.Ins.DB.CHUONGTRAIs);
            XacNhanCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                CTPhieuModel temp = new CTPhieuModel(MaChuong, MoTa);
                vm.Add(temp);
                MessageBox.Show("Đã thêm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                p.Close();
            });
            HuyCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
        }
        public ChiTietPhieuSuaChuaVM()
        {

        }
    }
}
