using FarmManagementSoftware.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using FarmManagementSoftware.View.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using static FarmManagementSoftware.ViewModel.BaoCaoTinhTrangHeoVM;
using System.Collections.ObjectModel;

namespace FarmManagementSoftware.ViewModel
{
    public class ThemChuongVM : BaseViewModel
    {
        #region Attributes
        ObservableCollection<CHUONGTRAI> _ChuongTrais = new ObservableCollection<CHUONGTRAI>();
        Themchuong tc;
        string _MaChuong;
        string _MaLoaiChuong;
        string _TinhTrang;
        int _SucChuaToiDa;
        int _SoLuongHeo;
        private LOAICHUONG _selectedLoaiChuong;
        public LOAICHUONG selectedLoaiChuong { get => _selectedLoaiChuong; set { _selectedLoaiChuong = value; OnPropertyChanged(); } }
        private ObservableCollection<LOAICHUONG> _listLoaiChuong;
        public ObservableCollection<LOAICHUONG> listLoaiChuong2 { get => _listLoaiChuong; set { _listLoaiChuong = value; OnPropertyChanged(); } }
        #endregion

        #region Property}
        public ObservableCollection<CHUONGTRAI> CHUONGTRAIs { get => _ChuongTrais; set { _ChuongTrais = value; OnPropertyChanged(); } }
        public string MaChuong { get => _MaChuong; set { _MaChuong = value; OnPropertyChanged(); } }
        public string MaLoaiChuong { get => _MaLoaiChuong; set { _MaLoaiChuong = value; OnPropertyChanged(); } }
        public string TinhTrang { get => _TinhTrang; set { _TinhTrang = value; OnPropertyChanged(); } }
        public int SucChuaToiDa { get => _SucChuaToiDa; set { _SucChuaToiDa = value; OnPropertyChanged(); } }
        public int SoLuongHeo { get => _SoLuongHeo; set { _SoLuongHeo = value; OnPropertyChanged(); } }
        public int listviewSelectedIndex { get; set; }
        #endregion

        #region Command
        public ICommand ThemCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        public ICommand TaoMaChuong { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion

        public ThemChuongVM()
        {
            listviewSelectedIndex = 0;
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, p => { tc = p as Themchuong; Load(); });

            TaoMaChuong = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                MaChuong = CreatMaChuong(selectedLoaiChuong.MaLoaiChuong as string);
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
                var ChuongTrai = new CHUONGTRAI() { MaChuong = MaChuong, MaLoaiChuong = selectedLoaiChuong.MaLoaiChuong, LOAICHUONG = selectedLoaiChuong, TinhTrang = TinhTrang, SuaChuaToiDa = SucChuaToiDa, SoLuongHeo = SoLuongHeo };
                _ChuongTrais.Add(ChuongTrai);
                MaChuong = CreatMaChuong(selectedLoaiChuong.MaLoaiChuong as string);
                TinhTrang = null;
                SucChuaToiDa = 0;
            });
            DeleteCommand = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                if (listviewSelectedIndex < 0)
                    return;
                var ChuongTrai = _ChuongTrais[listviewSelectedIndex];
                _ChuongTrais.Remove(ChuongTrai);
            });
        }

        void Load()
        {
            listLoaiChuong2 = new ObservableCollection<LOAICHUONG>(DataProvider.Ins.DB.LOAICHUONGs);
            selectedLoaiChuong = listLoaiChuong2[0];
            MaChuong = CreatMaChuong(selectedLoaiChuong.MaLoaiChuong as string);
        }

        string CreatMaChuong(string maLC)
        {
            ObservableCollection<CHUONGTRAI> Chuongs = new ObservableCollection<CHUONGTRAI>(DataProvider.Ins.DB.CHUONGTRAIs.Where(x => x.MaLoaiChuong.Equals(maLC)).ToList());
            foreach(var chuongtrai in _ChuongTrais)
            {
                Chuongs.Add(chuongtrai);
            }
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
