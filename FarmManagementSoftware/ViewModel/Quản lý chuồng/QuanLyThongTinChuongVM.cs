using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
        int _SucChuaCanTim1 = 0;
        int _SucChuaCanTim2 = 0;
        int _SoHeoCanTim1 = 0;
        int _SoHeoCanTim2 = 0;
        private ObservableCollection<CHUONGTRAI> _ListChuongTrai;
        private ObservableCollection<LOAICHUONG> _ListLoaiChuong;

        List<string> _ListTenLoaiChuongCanTim = new List<string>();
        #endregion                                                                                                                                                                                                              

        #region Property
        public string MaChuongCanTim { get => _MaChuongCanTim; set { _MaChuongCanTim = value; OnPropertyChanged(); } }
        public int SucChuaCanTim1 { get => _SucChuaCanTim1; set { _SucChuaCanTim1 = value; OnPropertyChanged(); } }
        public int SucChuaCanTim2 { get => _SucChuaCanTim2; set { _SucChuaCanTim2 = value; OnPropertyChanged(); } }
        public int SoHeoCanTim1 { get => _SoHeoCanTim1; set { _SoHeoCanTim1 = value; OnPropertyChanged(); } }
        public int SoHeoCanTim2 { get => _SoHeoCanTim2; set { _SoHeoCanTim2 = value; OnPropertyChanged(); } }
        public ObservableCollection<CHUONGTRAI> ListChuongTrai { get => _ListChuongTrai; set { _ListChuongTrai = value; OnPropertyChanged(); } }
        public ObservableCollection<LOAICHUONG> ListLoaiChuong { get => _ListLoaiChuong; set { _ListLoaiChuong = value; OnPropertyChanged(); } }
        public List<string> ListTenLoaiChuongCanTim { get => _ListTenLoaiChuongCanTim; set { _ListTenLoaiChuongCanTim = value; OnPropertyChanged(); } }
        public int listviewSelectedIndex { get; set; }
        public CHUONGTRAI SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value; OnPropertyChanged();
            }
        }
        //public int MaxSC { get => MaxSucChua; set { MaxSucChua = value; OnPropertyChanged(); } }
        //public int MaxSH { get => MaxHeo; set { MaxHeo = value; OnPropertyChanged(); } }
        #endregion

        #region Command
        public ICommand AddCommand { get; set; }
        public ICommand ShowCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand TimKiemTheoMaChuongCommand { get; set; }
        public ICommand TimKiemTheoSucChua1Command { get; set; }
        public ICommand TimKiemTheoSucChua2Command { get; set; }
        public ICommand TimKiemTheoSoHeo1Command { get; set; }
        public ICommand TimKiemTheoSoHeo2Command { get; set; }
        public ICommand TimKiemTheoLoaiChuongCommand { get; set; }
        #endregion

        public QuanLyThongTinChuongVM()
        {
            listviewSelectedIndex = 0;
            _ListChuongTrai = new ObservableCollection<CHUONGTRAI>(DataProvider.Ins.DB.CHUONGTRAIs);
            _ListLoaiChuong = new ObservableCollection<LOAICHUONG>(DataProvider.Ins.DB.LOAICHUONGs);
            foreach(var loaichuong in _ListLoaiChuong)
            {
                ListTenLoaiChuongCanTim.Add(loaichuong.TenLoai.ToString());
            }
            AddCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Themchuong themChuong = new Themchuong();
                ThemChuongVM vm = new ThemChuongVM();
                themChuong.DataContext = vm;
                themChuong.ShowDialog();
                
                ListChuongTrai = new ObservableCollection<CHUONGTRAI>(DataProvider.Ins.DB.CHUONGTRAIs);
                //MaxC();
                //MaxH();
            });
            ShowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                ThongTinChuong thongTinChuong = new ThongTinChuong();
                thongTinChuong.DataContext = SelectedItem;
                thongTinChuong.ShowDialog();
            });
            //EditCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            //{
            //    SuaChuong suaChuong = new SuaChuong();
            //    suaChuong.DataContext = SelectedItem;
            //    suaChuong.ShowDialog();
            //    MaxC();
            //    MaxH();
            //});
            EditCommand = new RelayCommand<Window>((p) => { return true; }, p => { Edit(p); });
            DeleteCommand = new RelayCommand<Window>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                else return true;
            }, p =>
            {
                if (SelectedItem.SoLuongHeo > 0)
                {
                    MessageBox.Show("Không thể xóa chuồng này vì vẫn còn heo trong chuồng");
                    return;
                }
                MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn xoá ?", "Thông báo", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataProvider.Ins.DB.CHUONGTRAIs.Remove(SelectedItem);
                    DataProvider.Ins.DB.SaveChanges();
                    ListChuongTrai = new ObservableCollection<CHUONGTRAI>(DataProvider.Ins.DB.CHUONGTRAIs);
                    //MaxC();
                    //MaxH();
                }
            });
            #region Tìm kiếm
            TimKiemTheoMaChuongCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                _MaChuongCanTim = p.Text;
                TimKiem();
            });
            TimKiemTheoSucChua1Command = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                if (p.Text != "")
                {
                    try
                    {
                        _SucChuaCanTim1 = Convert.ToInt32(p.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Giá trị nhập phải là số nguyên");
                    }
                }
                else
                {
                    _SucChuaCanTim1 = 0;
                }
                TimKiem();
            });
            TimKiemTheoSucChua2Command = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {    
                if (p.Text != "")
                {
                    try
                    {
                        _SucChuaCanTim2 = Convert.ToInt32(p.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Giá trị nhập phải là số nguyên");
                    }
                }
                else
                {
                    _SucChuaCanTim2 = -1;
                }
                TimKiem();
            });
            TimKiemTheoSoHeo1Command = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                if (p.Text != "")
                    try
                    {
                        _SoHeoCanTim1 = Convert.ToInt32(p.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Giá trị nhập phải là số nguyên");
                    }
                else
                {
                    _SoHeoCanTim1 = 0;
                } 
                TimKiem();
            });
            TimKiemTheoSoHeo2Command = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                if (p.Text != "")
                    try
                    {
                        _SoHeoCanTim2 = Convert.ToInt32(p.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Giá trị nhập phải là số nguyên");
                    }
                else
                {
                    _SoHeoCanTim2 = -1;
                }    
                TimKiem();
            });
            TimKiemTheoLoaiChuongCommand = new RelayCommand<CheckBox>((p) => { return true; }, p =>
            {
                if (p.IsChecked == true)
                {
                    _ListTenLoaiChuongCanTim.Add(p.Content.ToString());
                }
                else
                {
                    _ListTenLoaiChuongCanTim.Remove(p.Content.ToString());
                }
                TimKiem();
            });
            #endregion
            //MaxC();
            //MaxH();
        }

        private void Edit(Window p)
        {
            if (listviewSelectedIndex < 0)
                return;
            SuaChuongVM suaChuongVM = new SuaChuongVM(ListChuongTrai[listviewSelectedIndex]);
            SuaChuong suaChuong = new SuaChuong();
            suaChuong.DataContext = suaChuongVM;
            suaChuong.ShowDialog();
            //MaxC();
            //MaxH();
        }

        //int MaxH()
        //{
        //    _ListChuongTrai = new ObservableCollection<CHUONGTRAI>(DataProvider.Ins.DB.CHUONGTRAIs);
        //    foreach (var item in _ListChuongTrai)
        //    {
        //        if ((int)(item.SoLuongHeo) > MaxHeo)
        //        {
        //            MaxHeo = (int)item.SoLuongHeo;
        //        }
        //    }
        //    return MaxHeo;
        //}

        //int MaxC()
        //{
        //    _ListChuongTrai = new ObservableCollection<CHUONGTRAI>(DataProvider.Ins.DB.CHUONGTRAIs);
        //    foreach (var item in _ListChuongTrai)
        //    {
        //        if ((int)(item.SuaChuaToiDa) > MaxSucChua)
        //        {
        //            MaxSucChua = (int)item.SuaChuaToiDa;
        //        }
        //    }
        //    return MaxSucChua;
        //}

        void TimKiem()
        {
            ListChuongTrai.Clear();
            var ChuongTrais = DataProvider.Ins.DB.CHUONGTRAIs.ToList(); ;
            if (_MaChuongCanTim != "")
            {
                ChuongTrais = ChuongTrais.Where(x => x.MaChuong.Contains(_MaChuongCanTim)).ToList();
            }
            if (_SucChuaCanTim1 > 0)
            {
                ChuongTrais = ChuongTrais.Where(x => x.SuaChuaToiDa >= _SucChuaCanTim1).ToList();
            }
            if (_SucChuaCanTim2 > 0)
            {
                ChuongTrais = ChuongTrais.Where(x => x.SuaChuaToiDa <= _SucChuaCanTim2).ToList();
            }
            if (_SoHeoCanTim1 > 0)
            {
                ChuongTrais = ChuongTrais.Where(x => x.SoLuongHeo >= _SoHeoCanTim1).ToList();
            }
            if (_SoHeoCanTim2 > 0)
            {
                ChuongTrais = ChuongTrais.Where(x => x.SoLuongHeo <= _SoHeoCanTim2).ToList();
            }
            if (_ListTenLoaiChuongCanTim.Count > 0)
            {
                var DK = ChuongTrais;
                foreach (var lChuong in _ListTenLoaiChuongCanTim)
                {
                    ChuongTrais = DK.Where(x => x.LOAICHUONG.TenLoai.Equals(lChuong)).ToList();
                    foreach (var item in ChuongTrais)
                    {
                        CHUONGTRAI cHUONGTRAI = new CHUONGTRAI();
                        cHUONGTRAI = item;
                        _ListChuongTrai.Add(cHUONGTRAI);
                    }
                }
            }
            //else
            //{
            //    foreach (var item in ChuongTrais)
            //    {
            //        CHUONGTRAI cHUONGTRAI = new CHUONGTRAI();
            //        cHUONGTRAI = item;
            //        ListChuongTrai.Add(cHUONGTRAI);
            //    }
            //}
        }
    }
}
