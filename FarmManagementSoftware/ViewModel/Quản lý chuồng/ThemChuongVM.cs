using FarmManagementSoftware.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace FarmManagementSoftware.ViewModel
{
    public class ThemChuongVM : BaseViewModel
    {
        #region Attributes
        List<CHUONGTRAI> _ChuongTrais = new List<CHUONGTRAI>();
        string _MaChuong;
        string _MaLoaiChuong;
        string _TinhTrang;
        int _SucChuaToiDa;
        int _SoLuongHeo;
        #endregion

        #region Property
        public List<CHUONGTRAI> CHUONGTRAIs { get => _ChuongTrais; set { _ChuongTrais = value; OnPropertyChanged(); } }
        public string MaChuong { get => _MaChuong; set { _MaChuong = value; OnPropertyChanged(); } }
        public string MaLoaiChuong { get => _MaLoaiChuong; set { _MaLoaiChuong = value; OnPropertyChanged(); } }
        public string TinhTrang { get => _TinhTrang; set { _TinhTrang = value; OnPropertyChanged(); } }
        public int SucChuaToiDa { get => _SucChuaToiDa; set { _SucChuaToiDa = value; OnPropertyChanged(); } }
        public int SoLuongHeo { get => _SoLuongHeo; set { _SoLuongHeo = value; OnPropertyChanged(); } }
        #endregion

        #region Command
        public ICommand ThemCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        #endregion

        public ThemChuongVM()
        {
            XacNhanCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                foreach (var item in _ChuongTrais)
                {
                    DataProvider.Ins.DB.CHUONGTRAIs.Add(item);
                }
                DataProvider.Ins.DB.SaveChanges();
                _ChuongTrais.Clear();
                MessageBox.Show("Đã thêm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                p.Close();
            });
            ThemCommand = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                var ChuongTrai = new CHUONGTRAI() { MaChuong = MaChuong, MaLoaiChuong = MaLoaiChuong, TinhTrang = TinhTrang, SuaChuaToiDa = SucChuaToiDa, SoLuongHeo = SoLuongHeo };
                _ChuongTrais.Add(ChuongTrai);
                p.Items.Refresh();
            });
        }
    }
}
