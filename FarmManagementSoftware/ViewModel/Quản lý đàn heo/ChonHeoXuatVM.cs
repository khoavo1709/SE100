using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.Windows.Quản_lý_đàn_heo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static FarmManagementSoftware.ViewModel.BaoCaoSuaChuaVM;

namespace FarmManagementSoftware.ViewModel
{
    public class ChonHeoXuatVM : BaseViewModel
    {
        PhieuBanNhapHeoVM goc;
        public List<string> ListTinhTrang { get; set; }
        public List<string> ListNguonGoc { get; set; }

        IEnumerable<HEO> ListHeoXuatDuoc;

        private ObservableCollection<HEOXUAT> _ListHeoX;
        public ObservableCollection<HEOXUAT> ListHeoX { get => _ListHeoX; set { _ListHeoX = value; OnPropertyChanged(); } }
        private List<HEOXUAT> _ListHeoTimKiem;
        public HEOXUAT SelectedHeo { get; set; }
        public ICommand TimKiemTheoTenCommand { get; set; }
        public ICommand TimKiemTheoNgaySinhMinCommand { get; set; }
        public ICommand TimKiemTheoNgaySinhMaxCommand { get; set; }
        public ICommand TimKiemTheoTrongLuongMinCommand { get; set; }
        public ICommand TimKiemTheoTrongLuongMaxCommand { get; set; }
        public ICommand TimKiemTheoLoaiCommand { get; set; }
        public ICommand TTCheck { get; set; }
        public ICommand NGCheck { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
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
            foreach (HEO h in ListHeoXuatDuoc)
            {
                HEOXUAT heoX = new HEOXUAT();
                heoX.heo = h;
                heoX.IsChecked = false;
                ListHeoX.Add(heoX);
                heoX.DonGia = 0;
            }
            _ListHeoTimKiem = new List<HEOXUAT>();
            if(_ListHeoTimKiem.Count<=0)
                 _ListHeoTimKiem = ListHeoX.ToList();
            ListTinhTrang = new List<string>();
            ListNguonGoc = new List<string>();
            TimKiemTheoTenCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                matim = p.Text;
                TimKiem();
            });
            TimKiemTheoNgaySinhMinCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                if (p.Text.Count() > 0)
                {
                    mindate = p.SelectedDate;
                }
                TimKiem();

            });
            TimKiemTheoNgaySinhMaxCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                if (p.SelectedDate < mindate)
                {
                    MessageBox.Show("Ngày đến phải sau ngày từ");
                    return;
                }
                if (p.Text.Count() > 0)
                {
                    maxdate = p.SelectedDate;
                }
                TimKiem();

            });
            TimKiemTheoTrongLuongMinCommand = new RelayCommand<TextBox>((p) => {
                return true;
            }, p =>
            {
                if (p.Text.Count() > 0)
                    minTL = int.Parse(p.Text);
                else minTL = 0;
                TimKiem();
            });
            TimKiemTheoTrongLuongMaxCommand = new RelayCommand<TextBox>((p) => {
                return true;
            }, p =>
            {
                if (p.Text.Count() > 0)
                    maxTL = int.Parse(p.Text);
                else maxTL = 0;
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

            EditCommand = new RelayCommand<ListView>((p) => { return true; }, p =>
            {

                DonGia w = new DonGia(SelectedHeo);
                w.ShowDialog();
            });
            DeleteCommand = new RelayCommand<ListView>((p) => { return true; }, p =>
            {
                SelectedHeo.IsChecked = false;
                SelectedHeo.DonGia = 0;
            });
            HoanTatCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                foreach (HEOXUAT x in ListHeoX)
                {
                    if (x.IsChecked == true)
                        goc.ListHeoX.Add(x);
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
            List<HEOXUAT> full= _ListHeoTimKiem.ToList();
            List<HEOXUAT> hEOs;
            List<HEOXUAT> hEOs1;
            List<HEOXUAT> hEOs2;
            List<HEOXUAT> hEOs3;
            List<HEOXUAT> hEOs4;
            List<HEOXUAT> hEOs7 = new List<HEOXUAT>();
            List<HEOXUAT> hEOs8 = new List<HEOXUAT>();
            ListHeoX.Clear();

            if (matim != null && matim != "")
                hEOs = full.Where(Heo => Heo.heo.MaHeo.Contains(matim)).ToList();
            else hEOs = full;
            if (mindate != null && mindate != DateTime.Now.Date)
                hEOs1 = full.Where(x => x.heo.NgaySinh >= mindate).ToList();
            else hEOs1 = full;

            if (maxdate != null && maxdate != DateTime.Now.Date)
                hEOs2 = full.Where(x => x.heo.NgaySinh <= maxdate).ToList();
            else
                hEOs2 = full;
            if (minTL > 0)
                hEOs3 = full.Where(x => x.heo.TrongLuong >= minTL).ToList();
            else hEOs3 = full;

            if (maxTL > minTL)
                hEOs4 = full.Where(x => x.heo.TrongLuong <= maxTL).ToList();
            else
                hEOs4 = full;
            if (ListTinhTrang.Count > 0)
            {
                foreach (string i in ListTinhTrang)
                {
                    List<HEOXUAT> x = full.Where(a => a.heo.TinhTrang == i).ToList();
                    foreach (HEOXUAT h in x)
                    {
                        hEOs7.Add(h);
                    }
                }
            }
            else
                hEOs7 = full;
            if (ListNguonGoc.Count > 0)
            {
                foreach (string i in ListNguonGoc)
                {
                    List<HEOXUAT> x = full.Where(a => a.heo.NguonGoc == i).ToList();
                    foreach (HEOXUAT h in x)
                    {
                        hEOs8.Add(h);
                    }
                }
            }
            else
                hEOs8 = full;
            IEnumerable<HEOXUAT> heo = from HEOXUAT a in hEOs
                                       join HEOXUAT b in hEOs1
                                       on a.heo.MaHeo equals b.heo.MaHeo
                                       join HEOXUAT c in hEOs2
                                   on a.heo.MaHeo equals c.heo.MaHeo
                                       join HEOXUAT d in hEOs3
                                   on a.heo.MaHeo equals d.heo.MaHeo
                                       join HEOXUAT e in hEOs4
                                   on a.heo.MaHeo equals e.heo.MaHeo
                                       join HEOXUAT f in hEOs7
                                   on a.heo.MaHeo equals f.heo.MaHeo
                                       join HEOXUAT j in hEOs8
                                   on a.heo.MaHeo equals j.heo.MaHeo
                                       orderby a.heo.MaHeo descending
                                       select a;

            foreach (HEOXUAT h in heo)
            {
                ListHeoX.Add(h);
            }
            MessageBox.Show("Ket qua"+ListHeoX.Count().ToString());
        }
    }
}
