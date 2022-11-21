using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FarmManagementSoftware.ViewModel
{
    public class QuanLyThongTinChuongVM : BaseViewModel
    {
        #region Attributes
        int MaxSucChua = 0;
        int MaxHeo = 0;
        CHUONGTRAI _SelectedItem;
        string _MaChuongCanTim = "";
        int _SucChuaCanTim = 0;
        int _SoHeoCanTim = 0;
        private ObservableCollection<CHUONGTRAI> _ListChuongTrai;
        private ObservableCollection<LOAICHUONG> _ListLoaiChuong;
        private ObservableCollection<LOAICHUONG> _ListLoaiChuongCanTim;
        private ObservableCollection<CHUONGTRAI> _ListMaLoaiChuongCanTim;
        List<string> _LoaiChuongCanTim;
        #endregion                                                                                                                                                                                                              

        #region Property
        public string MaChuongCanTim { get => _MaChuongCanTim; set { _MaChuongCanTim = value; OnPropertyChanged(); } }
        public int SucChuaCanTim { get => _SucChuaCanTim; set { _SucChuaCanTim = value; OnPropertyChanged(); } }
        public int SoHeoCanTim { get => _SoHeoCanTim; set { _SoHeoCanTim = value; OnPropertyChanged(); } }
        public ObservableCollection<CHUONGTRAI> ListChuongTrai { get => _ListChuongTrai; set { _ListChuongTrai = value; OnPropertyChanged(); } }
        public ObservableCollection<LOAICHUONG> ListLoaiChuong { get => _ListLoaiChuong; set { _ListLoaiChuong = value; OnPropertyChanged(); } }
        public ObservableCollection<LOAICHUONG> ListLoaiChuongCanTim { get => _ListLoaiChuongCanTim; set { _ListLoaiChuongCanTim = value; OnPropertyChanged(); } }
        public ObservableCollection<CHUONGTRAI> ListMaLoaiChuongCanTim { get => _ListMaLoaiChuongCanTim; set { _ListMaLoaiChuongCanTim = value; OnPropertyChanged(); } }
        public List<string> LoaiChuongCanTim { get => _LoaiChuongCanTim; set { _LoaiChuongCanTim = value; OnPropertyChanged(); } }
        public CHUONGTRAI SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value; OnPropertyChanged();
            }
        }
        public int MaxSC { get => MaxSucChua; set { MaxSucChua = value; OnPropertyChanged(); } }
        public int MaxSH { get => MaxHeo; set { MaxHeo = value; OnPropertyChanged(); } }
        #endregion

        #region Command
        public ICommand AddCommand { get; set; }
        public ICommand ShowCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand TimKiemTheoMaChuongCommand { get; set; }
        public ICommand TimKiemTheoSucChuaCommand { get; set; }
        public ICommand TimKiemTheoSoHeoCommand { get; set; }
        public ICommand TimKiemTheoLoaiChuongCommand { get; set; }
        #endregion

        public QuanLyThongTinChuongVM()
        {
            _ListChuongTrai = new ObservableCollection<CHUONGTRAI>(DataProvider.Ins.DB.CHUONGTRAIs);
            _ListLoaiChuong = new ObservableCollection<LOAICHUONG>(DataProvider.Ins.DB.LOAICHUONGs);
            AddCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                ThemChuong themChuong = new ThemChuong();
                themChuong.ShowDialog();
                _ListChuongTrai = new ObservableCollection<CHUONGTRAI>(DataProvider.Ins.DB.CHUONGTRAIs);
                MaxC();
                MaxH();
            });
            ShowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                ThongTinChuong thongTinChuong = new ThongTinChuong();
                thongTinChuong.DataContext = SelectedItem;
                thongTinChuong.ShowDialog();    
            });
            EditCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                SuaChuong suaChuong = new SuaChuong();
                suaChuong.DataContext = SelectedItem;
                suaChuong.ShowDialog();
                MaxC();
                MaxH();
            });
            DeleteCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (SelectedItem != null)
                {
                    DataProvider.Ins.DB.CHUONGTRAIs.Remove(SelectedItem);
                    DataProvider.Ins.DB.SaveChanges();
                    _ListChuongTrai = new ObservableCollection<CHUONGTRAI>(DataProvider.Ins.DB.CHUONGTRAIs);
                    MessageBox.Show("xóa");
                    MaxC();
                    MaxH();
                }
            });
            #region Tìm kiếm
            TimKiemTheoMaChuongCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                _MaChuongCanTim = p.Text;
                TimKiem();
            });
            TimKiemTheoSucChuaCommand = new RelayCommand<Slider>((p) => { return true; }, p =>
            {
                _SucChuaCanTim = (int)p.Value;
                TimKiem();
            });
            TimKiemTheoSoHeoCommand = new RelayCommand<Slider>((p) => { return true; }, p =>
            {
                _SoHeoCanTim = (int)p.Value;
                TimKiem();
            });
            TimKiemTheoLoaiChuongCommand = new RelayCommand<CheckBox>((p) => { return true; }, p =>
            {
                _LoaiChuongCanTim.Add((string)p.Content);
                foreach (var item in _LoaiChuongCanTim)
                {
                    var temp = new ObservableCollection<LOAICHUONG>(DataProvider.Ins.DB.LOAICHUONGs.Where(x => x.TenLoai == item));
                    foreach (var i in temp)
                    {
                        _ListLoaiChuongCanTim.Add(i);
                    }
                }
                LayMaLoaiChuong();
                TimKiem();
            });
            #endregion
            MaxC();
            MaxH();
        }
        void MaxC()
        {
            _ListChuongTrai = new ObservableCollection<CHUONGTRAI>(DataProvider.Ins.DB.CHUONGTRAIs);
            foreach (var item in _ListChuongTrai)
            {
                if ((int)(item.SoLuongHeo) > MaxHeo)
                {
                    MaxHeo = (int)item.SoLuongHeo;
                }
            }
        }
        void MaxH()
        {
            _ListChuongTrai = new ObservableCollection<CHUONGTRAI>(DataProvider.Ins.DB.CHUONGTRAIs);
            foreach (var item in _ListChuongTrai)
            {
                if ((int)(item.SuaChuaToiDa) > MaxSucChua)
                {
                    MaxSucChua = (int)item.SuaChuaToiDa;
                }
            }
        }
        void LayMaLoaiChuong()
        {
            foreach (var item in _ListLoaiChuongCanTim)
            {
                var temp = new ObservableCollection<CHUONGTRAI>(DataProvider.Ins.DB.CHUONGTRAIs.Where(x => x.MaLoaiChuong == item.MaLoaiChuong));
                foreach (var i in temp)
                {
                    _ListMaLoaiChuongCanTim.Add(i);
                }
            }
        }
        void TimKiem()
        {
            _ListChuongTrai.Clear();
            var ChuongTrais = DataProvider.Ins.DB.CHUONGTRAIs.ToList();
            var temp = DataProvider.Ins.DB.CHUONGTRAIs.ToList();
            if (_MaChuongCanTim != "")
            {
                ChuongTrais = ChuongTrais.Where(x => x.MaChuong.Contains(_MaChuongCanTim) == true).ToList();
            }
            if (_SucChuaCanTim != 0)
            {
                ChuongTrais = ChuongTrais.Where(x => x.SuaChuaToiDa.Equals(_SucChuaCanTim)).ToList();
            }
            if (_SoHeoCanTim != 0)
            {
                ChuongTrais = ChuongTrais.Where(x => x.SoLuongHeo.Equals(_SoHeoCanTim)).ToList();
            }
            if (_ListLoaiChuongCanTim != null)
            {

            }
            foreach (var item in ChuongTrais)
            {
                CHUONGTRAI cHUONGTRAI = new CHUONGTRAI();
                cHUONGTRAI = item;
                _ListChuongTrai.Add(cHUONGTRAI);
            }
        }
    }
}