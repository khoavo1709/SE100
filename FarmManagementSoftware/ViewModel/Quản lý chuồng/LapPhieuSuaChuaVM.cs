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
    public class LapPhieuSuaChuaVM : BaseViewModel
    {
        #region Attributes
        private ObservableCollection<PHIEUSUACHUA> _ListPhieuSuaChua;
        private string _MaNhanVien = "";
        PHIEUSUACHUA _SelectedItem;
        private DateTime? _NgaySuaChua1 = new DateTime();
        private DateTime? _NgaySuaChua2 = new DateTime();
        private string _TenDoiTac = "";
        private List<string> _ListTrangThai = new List<string>();
        #endregion

        #region Property
        public ObservableCollection<PHIEUSUACHUA> ListPhieuSuaChua { get => _ListPhieuSuaChua; set { _ListPhieuSuaChua = value; OnPropertyChanged(); } }
        public string MaNhanVien { get => _MaNhanVien; set { _MaNhanVien = value; OnPropertyChanged(); } }
        public DateTime? NgaySuaChua1 { get => _NgaySuaChua1; set { _NgaySuaChua1 = value; OnPropertyChanged(); } }
        public DateTime? NgaySuaChua2 { get => _NgaySuaChua2; set { _NgaySuaChua2 = value; OnPropertyChanged(); } }
        public string TenDoiTac { get => _TenDoiTac; set { _TenDoiTac = value; OnPropertyChanged(); } }
        public List<string> ListTrangThai { get => _ListTrangThai; set { _ListTrangThai = value; OnPropertyChanged(); } }
        public PHIEUSUACHUA SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value; OnPropertyChanged();
            }
        }
        #endregion

        #region Command
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand TimKiemTheoMaNVCommand { get; set; }
        public ICommand TimKiemTheoNgaySC1Command { get; set; }
        public ICommand TimKiemTheoNgaySC2Command { get; set; }
        public ICommand TimKiemTheoTrangThaiCommand { get; set; }
        public ICommand TimKiemTheoTenDTCommand { get; set; }
        #endregion

        public LapPhieuSuaChuaVM()
        {
            _ListPhieuSuaChua = new ObservableCollection<PHIEUSUACHUA>(DataProvider.Ins.DB.PHIEUSUACHUAs);
            AddCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                PhieuSuaChua phieuSuaChua = new PhieuSuaChua();
                phieuSuaChua.ShowDialog();
                ListPhieuSuaChua = new ObservableCollection<PHIEUSUACHUA>(DataProvider.Ins.DB.PHIEUSUACHUAs);
            });
            EditCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                PhieuSuaChua phieuSuaChua = new PhieuSuaChua();
                phieuSuaChua.ShowDialog();
                ListPhieuSuaChua = new ObservableCollection<PHIEUSUACHUA>(DataProvider.Ins.DB.PHIEUSUACHUAs);
            });
            DeleteCommand = new RelayCommand<Window>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                else return true;
            }, p =>
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn xoá ?", "Thông báo", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataProvider.Ins.DB.PHIEUSUACHUAs.Remove(SelectedItem);
                    DataProvider.Ins.DB.SaveChanges();
                    ListPhieuSuaChua = new ObservableCollection<PHIEUSUACHUA>(DataProvider.Ins.DB.PHIEUSUACHUAs);
                }
            });
            #region Tìm kiếm
            TimKiemTheoMaNVCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                _MaNhanVien = p.Text;
                TimKiem();
            });
            TimKiemTheoTenDTCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                _TenDoiTac = p.Text;
                TimKiem();
            });
            TimKiemTheoNgaySC1Command = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                _NgaySuaChua1 = p.SelectedDate;
                TimKiem();
            });
            TimKiemTheoNgaySC2Command = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                _NgaySuaChua2 = p.SelectedDate;
                TimKiem();
            });
            TimKiemTheoTrangThaiCommand = new RelayCommand<CheckBox>((p) => { return true; }, p =>
            {
                if (p.IsChecked == true)
                {
                    ListTrangThai.Add(p.Content.ToString());
                }
                else
                    ListTrangThai.Remove(p.Content.ToString());
                TimKiem();
            });
            #endregion
        }

        void TimKiem()
        {
            _ListPhieuSuaChua.Clear();
            var PhieuSuaChuas = DataProvider.Ins.DB.PHIEUSUACHUAs.ToList();
            if (!String.IsNullOrWhiteSpace(_MaNhanVien))
            {
                PhieuSuaChuas = PhieuSuaChuas.Where(x => x.MaNhanVien.Contains(_MaNhanVien)).ToList();
            }
            if (!String.IsNullOrWhiteSpace(_TenDoiTac))
            {
                PhieuSuaChuas = PhieuSuaChuas.Where(x => x.DOITAC.TenDoiTac.Contains(_TenDoiTac)).ToList();
            }
            if (_NgaySuaChua1 != null && _NgaySuaChua1 != DateTime.MinValue)
            {
                PhieuSuaChuas = PhieuSuaChuas.Where(x => x.NgaySuaChua >= _NgaySuaChua1).ToList();
            }
            if (_NgaySuaChua2 != null && _NgaySuaChua2 != DateTime.MinValue)
            {
                PhieuSuaChuas = PhieuSuaChuas.Where(x => x.NgaySuaChua <= _NgaySuaChua2).ToList();
            }
            if (ListTrangThai.Count > 0)
            {
                var DK = PhieuSuaChuas;
                foreach (var tThai in _ListTrangThai)
                {
                    PhieuSuaChuas = DK.Where(x => x.TrangThai.Equals(tThai)).ToList();
                    foreach (var item in PhieuSuaChuas)
                    {
                        PHIEUSUACHUA pHIEUSUACHUA = new PHIEUSUACHUA();
                        pHIEUSUACHUA = item;
                        _ListPhieuSuaChua.Add(pHIEUSUACHUA);
                    }
                }
            }
            else
                foreach (var item in PhieuSuaChuas)
                {
                    PHIEUSUACHUA pHIEUSUACHUA = new PHIEUSUACHUA();
                    pHIEUSUACHUA = item;
                    _ListPhieuSuaChua.Add(pHIEUSUACHUA);
                }
        }
    }
}
