using QuanLyTraiHeo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using QuanLyTraiHeo.View.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using static QuanLyTraiHeo.ViewModel.BaoCaoTinhTrangHeoVM;
using System.Collections.ObjectModel;

namespace QuanLyTraiHeo.ViewModel
{
    public class ThemChuongVM : BaseViewModel
    {
        #region Attributes
        List<CHUONGTRAI> _ChuongTrais = new List<CHUONGTRAI>();
        List<string> listLoaiChuong;
        Themchuong tc;
        string _MaChuong;
        string _MaLoaiChuong;
        string _TinhTrang;
        int _SucChuaToiDa;
        int _SoLuongHeo;
        #endregion

        #region Property
        public List<string> ListLoaiChuong { get => listLoaiChuong; set { listLoaiChuong = value; OnPropertyChanged(); } }
        public List<CHUONGTRAI> CHUONGTRAIs { get => _ChuongTrais; set { _ChuongTrais = value; OnPropertyChanged(); } }
        public string MaChuong { get => _MaChuong; set { _MaChuong = value; OnPropertyChanged(); } }
        public string MaLoaiChuong { get => _MaLoaiChuong; set { _MaLoaiChuong = value; OnPropertyChanged(); } }
        public string TinhTrang { get => _TinhTrang; set { _TinhTrang = value; OnPropertyChanged(); } }
        public int SucChuaToiDa { get => _SucChuaToiDa; set { _SucChuaToiDa = value; OnPropertyChanged(); } }
        public int SoLuongHeo { get => _SoLuongHeo; set { _SoLuongHeo = value; OnPropertyChanged(); } }
        #endregion

        #region Command
        public ICommand ThemCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        public ICommand TaoMaChuong { get; set; }
        #endregion

        public ThemChuongVM()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, p => { tc = p as Themchuong; Load(); MaChuong = CreatMaChuong(tc.MaLC.SelectedItem as string); });
            TaoMaChuong = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                MaChuong = CreatMaChuong(tc.MaLC.SelectedItem as string);
            });
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

        void Load()
        {
            listLoaiChuong = new List<string>();
            foreach (var item in DataProvider.Ins.DB.LOAICHUONGs)
            {
                ListLoaiChuong.Add(item.MaLoaiChuong);
            }
            tc.MaLC.ItemsSource = ListLoaiChuong;
            tc.MaLC.SelectedIndex = 0;
        }

        string CreatMaChuong(string maLC)
        {
            ObservableCollection<CHUONGTRAI> Chuongs = new ObservableCollection<CHUONGTRAI>(DataProvider.Ins.DB.CHUONGTRAIs.Where(x => x.MaLoaiChuong.Equals(maLC)).ToList());
            int sl = Chuongs.Count + 1;
            string maChuong = "";
            if (maLC == "LC03112022000001")
            {
                if (sl < 10)
                {
                    maChuong = "NT00" + sl;
                }
                else if (sl < 100)
                {
                    maChuong = "NT0" + sl;
                }
                else if (sl < 1000)
                {
                    maChuong = "NT" + sl;
                }
            }
            else if (maLC == "LC03112022000002")
            {
                if (sl < 10)
                {
                    maChuong = "DT00" + sl;
                }
                else if (sl < 100)
                {
                    maChuong = "DT0" + sl;
                }
                else if (sl < 1000)
                {
                    maChuong = "DT" + sl;
                }
            }
            else if (maLC == "LC03112022000003")
            {
                if (sl < 10)
                {
                    maChuong = "HN00" + sl;
                }
                else if (sl < 100)
                {
                    maChuong = "HN0" + sl;
                }
                else if (sl < 1000)
                {
                    maChuong = "HN" + sl;
                }
            }
            else if (maLC == "LC03112022000004")
            {
                if (sl < 10)
                {
                    maChuong = "HD00" + sl;
                }
                else if (sl < 100)
                {
                    maChuong = "HD0" + sl;
                }
                else if (sl < 1000)
                {
                    maChuong = "HD" + sl;
                }
            }
            else
            {
                if (sl < 10)
                {
                    maChuong = "DG00" + sl;
                }
                else if (sl < 100)
                {
                    maChuong = "DG0" + sl;
                }
                else if (sl < 1000)
                {
                    maChuong = "DG" + sl;
                }
            }
            return maChuong;
        }
    }
}
