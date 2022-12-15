using QuanLyTraiHeo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static QuanLyTraiHeo.ViewModel.BaoCaoSuaChuaVM;

namespace QuanLyTraiHeo.ViewModel
{
    public class ChonHeoXuatVM : BaseViewModel
    {
        PhieuBanNhapHeoVM goc;
        public List<string> ListTinhTrang { get; set; }
        public List<string> ListNguonGoc { get; set; }
        IEnumerable<HEO> ListHeoXuatDuoc;
        private ObservableCollection<HEOXUAT> _ListHeoX;
        public ObservableCollection<HEOXUAT> ListHeoX { get => _ListHeoX; set { _ListHeoX = value; OnPropertyChanged(); } }
        public ICommand TimKiemTheoTenCommand { get; set; }
        public ICommand TimKiemTheoNgaySinhMinCommand { get; set; }
        public ICommand TimKiemTheoNgaySinhMaxCommand { get; set; }
        public ICommand TimKiemTheoTrongLuongMinCommand { get; set; }
        public ICommand TimKiemTheoTrongLuongMaxCommand { get; set; }
        public ICommand TimKiemTheoLoaiCommand { get; set; }
        public ICommand TTCheck { get; set; }
        public ICommand NGCheck { get; set; }
        public ICommand HuyBoCommand { get; set; }
        public ICommand HoanTatCommand { get; set; }
        string matim;
        DateTime? mindate;
        DateTime? maxdate;
        int minTL = 0;
        int maxTL = 0;
        public ChonHeoXuatVM(PhieuBanNhapHeoVM vm)
        {
            goc = vm;
            var AllHEO = new ObservableCollection<HEO>(DataProvider.Ins.DB.HEOs);
            var ALLCTPhieu = new ObservableCollection<CT_PHIEUHEO>(DataProvider.Ins.DB.CT_PHIEUHEO);
            ListHeoX = new ObservableCollection<HEOXUAT>();
            ListHeoXuatDuoc = from HEO a in AllHEO
                                         where !(from b in ALLCTPhieu
                                                 select b.MaHeo).Contains(a.MaHeo)
                                         select a;
            foreach(HEO h in ListHeoXuatDuoc)
            {
                HEOXUAT heoX = new HEOXUAT();
                heoX.heo = h;
                heoX.IsChecked = false;
                ListHeoX.Add(heoX);
            }
            ListTinhTrang = new List<string>();
            ListNguonGoc = new List<string>();
            TimKiemTheoTenCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                matim = p.Text;
            });
            TimKiemTheoNgaySinhMinCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                if (p.SelectedDate != DateTime.Today && p.SelectedDate != null)
                {
                    mindate = p.SelectedDate;
                    TimKiem();
                }
            });
            TimKiemTheoNgaySinhMaxCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                if (p.SelectedDate != DateTime.Today && p.SelectedDate != null)
                {
                    maxdate = p.SelectedDate;
                    TimKiem();
                }
            });
            TimKiemTheoTrongLuongMinCommand = new RelayCommand<TextBox>((p) => {

                if (int.TryParse(p.Text, out int n) && n > 0)
                    return true;
                return false;
            }, p =>
            {
                minTL = int.Parse(p.Text);
                TimKiem();
            });
            TimKiemTheoTrongLuongMaxCommand = new RelayCommand<TextBox>((p) => {

                if (int.TryParse(p.Text, out int n) && n > 0)
                    return true;
                return false;
            }, p =>
            {
                maxTL = int.Parse(p.Text);
                TimKiem();

            });
            TTCheck = new RelayCommand<CheckBox>((p) => { return true; }, p =>
            {
                if (p.IsChecked == true)
                    ListTinhTrang.Add(p.Content.ToString());
                else ListTinhTrang.Remove(p.Content.ToString());
                TimKiem();
            });
            NGCheck = new RelayCommand<CheckBox>((p) => { return true; }, p =>
            {
                if (p.IsChecked == true)
                    ListNguonGoc.Add(p.Content.ToString());
                else ListNguonGoc.Remove(p.Content.ToString());
                TimKiem();
            });

            HoanTatCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                foreach(HEOXUAT x in ListHeoX)
                {
                    if(x.IsChecked == true)
                        goc.ListHeo.Add(x.heo);
                }
                p.Close();
                p.DataContext = null;
            });
            HuyBoCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
                p.DataContext = null;
            }
            );
        }
        void TimKiem()
        {
        }
    }
}
