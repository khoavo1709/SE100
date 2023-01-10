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
        public ObservableCollection<LOAIHEO> ListLoai { get; set; }
        public ObservableCollection<GIONGHEO> ListGiong { get; set; }

        public List<string> ListTenLoai { get; set; }
        public List<string> ListTenGiong { get; set; }
        public List<string> ListTinhTrang { get; set; }
        public List<string> ListNguonGoc { get; set; }

        IEnumerable<HEO> ListHeoXuatDuoc;

        private ObservableCollection<HEOPHIEU> _ListHeoX;
        public ObservableCollection<HEOPHIEU> ListHeoX { get => _ListHeoX; set { _ListHeoX = value; OnPropertyChanged(); } }
        private List<HEOPHIEU> _ListHeoTimKiem;
        public HEOPHIEU SelectedHeo
        {
            get => _SelectedHeo; set
            {
                _SelectedHeo = value;
                if (_SelectedHeo != null)
                {
                    dongia = _SelectedHeo.DonGia;
                    if (!KiemTra())
                    {
                        _SelectedHeo.IsChecked = false;
                    }
                }
            }
        }
        public ICommand TimKiemTheoMa_TenCommand { get; set; }
        public ICommand TimKiemTheoNgaySinhMinCommand { get; set; }
        public ICommand TimKiemTheoNgaySinhMaxCommand { get; set; }
        public ICommand TimKiemTheoTrongLuongMinCommand { get; set; }
        public ICommand TimKiemTheoTrongLuongMaxCommand { get; set; }
        public ICommand TimKiemTheoLoaiCommand { get; set; }
        public ICommand TimKiemTheoGiongCommand { get; set; }
        public ICommand TTCheck { get; set; }
        public ICommand NGCheck { get; set; }
        public ICommand DonGiaCommand2 { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand HuyBoCommand { get; set; }
        public ICommand HoanTatCommand { get; set; }
        string matim;
<<<<<<< HEAD
        DateTime? mindate;
        DateTime? maxdate;
        int minTL = 0;
        int maxTL = 0;
=======
        public DateTime? mindate { get => _mindate; set { _mindate = value; OnPropertyChanged(); } }
        public DateTime? maxdate { get => _maxdate; set { _maxdate = value; OnPropertyChanged(); } }
        public int minTL { get => _minTL; set { _minTL = value; OnPropertyChanged(); } }
        public int maxTL { get => _maxTL; set { _maxTL = value; OnPropertyChanged(); } }

        private DateTime? _maxdate;
        private DateTime? _mindate;

        private int _maxTL;
        private int _minTL;
        public int dongia { get => _dongia; set { _dongia = value; OnPropertyChanged(); } }

        THAMSO thamso = DataProvider.Ins.DB.THAMSOes.First();
        private int _dongia;
        private HEOPHIEU _SelectedHeo;
>>>>>>> Tú

        public ChonHeoXuatVM(PhieuBanNhapHeoVM vm)
        {

            goc = vm;
            var AllHEO = new ObservableCollection<HEO>(DataProvider.Ins.DB.HEOs);
            var ALLCTPhieu = new ObservableCollection<CT_PHIEUHEO>(DataProvider.Ins.DB.CT_PHIEUHEO);
            ListHeoX = new ObservableCollection<HEOPHIEU>();
            ListHeoXuatDuoc = from HEO a in AllHEO
                              where !(from b in ALLCTPhieu
                                      select b.MaHeo).Contains(a.MaHeo)
                              select a;
            foreach (HEO h in ListHeoXuatDuoc)
            {
                HEOPHIEU heoX = new HEOPHIEU();
                heoX.heo = h;
                heoX.IsChecked = false;
                ListHeoX.Add(heoX);
                heoX.DonGia = 0;
            }
            DateTime Now = DateTime.Now;
            mindate = new DateTime(Now.Year, Now.Month, 1);
            maxdate = new DateTime(Now.Year, Now.Month, Now.Day + 1);
            minTL = (int)ListHeoX.Min(x => x.heo.TrongLuong);
            maxTL = (int)ListHeoX.Max(x => x.heo.TrongLuong);

            ListLoai = new ObservableCollection<LOAIHEO>(DataProvider.Ins.DB.LOAIHEOs);
            ListTenLoai = new List<string>();
            foreach (LOAIHEO l in ListLoai)
            {
                ListTenLoai.Add(l.TenLoaiHeo);
            }
            ListGiong = new ObservableCollection<GIONGHEO>(DataProvider.Ins.DB.GIONGHEOs);
            ListTenGiong = new List<string>();
            foreach (GIONGHEO l in ListGiong)
            {
                ListTenGiong.Add(l.TenGiongHeo);
            }
            ListTinhTrang = new List<string>();
            ListTinhTrang.Add("Sức khoẻ tốt");
            ListTinhTrang.Add("Đang mang thai");
            ListTinhTrang.Add("Đang bị bệnh");
            ListTinhTrang.Add("Đã xuất");
            ListTinhTrang.Add("Đã đào thải");

            ListNguonGoc = new List<string>();
            ListNguonGoc.Add("Nhập ngoài");
            ListNguonGoc.Add("Sinh trong trang trại");


            _ListHeoTimKiem = new List<HEOPHIEU>();
            if (_ListHeoTimKiem.Count <= 0)
                _ListHeoTimKiem = ListHeoX.ToList();
            TimKiem();
            TimKiemTheoMa_TenCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
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
            TimKiemTheoLoaiCommand = new RelayCommand<CheckBox>((p) => { return true; }, p =>
            {

                if (p.IsChecked == true)
                    ListTenLoai.Add(p.Content.ToString());
                else ListTenLoai.Remove(p.Content.ToString());
                TimKiem();
            });
            TimKiemTheoGiongCommand = new RelayCommand<CheckBox>((p) => { return true; }, p =>
            {

                if (p.IsChecked == true)
                    ListTenGiong.Add(p.Content.ToString());
                else ListTenGiong.Remove(p.Content.ToString());
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

            DonGiaCommand2 = new RelayCommand<ListView>((p) => { return true; }, p =>
            {
                SelectedHeo.DonGia = dongia;
                dongia = 0;
            });
            HoanTatCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                foreach (HEOPHIEU x in ListHeoX)
                {
                    if (x.IsChecked == true)
                        goc.ListHeo.Add(x);
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
            List<HEOPHIEU> full = _ListHeoTimKiem.ToList();
            List<HEOPHIEU> hEOs;
            List<HEOPHIEU> hEOs1;
            List<HEOPHIEU> hEOs2;
            List<HEOPHIEU> hEOs3;
            List<HEOPHIEU> hEOs4;
            List<HEOPHIEU> hEOs5 = new List<HEOPHIEU>();
            List<HEOPHIEU> hEOs6 = new List<HEOPHIEU>();
            List<HEOPHIEU> hEOs7 = new List<HEOPHIEU>();
            List<HEOPHIEU> hEOs8 = new List<HEOPHIEU>();
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
            if (ListTenLoai.Count > 0)
            {
                foreach (string i in ListTenLoai)
                {
                    List<HEOPHIEU> x = full.Where(a => a.heo.LOAIHEO.TenLoaiHeo == i).ToList();
                    foreach (HEOPHIEU h in x)
                    {
                        hEOs5.Add(h);
                    }
                }
            }
            else
                hEOs5 = full;
            if (ListTenGiong.Count > 0)
            {
                foreach (string i in ListTenGiong)
                {
                    List<HEOPHIEU> x = full.Where(a => a.heo.GIONGHEO.TenGiongHeo == i).ToList();
                    foreach (HEOPHIEU h in x)
                    {
                        hEOs6.Add(h);
                    }
                }
            }
            else
                hEOs6 = full;
            if (ListTinhTrang.Count > 0)
            {
                foreach (string i in ListTinhTrang)
                {
                    List<HEOPHIEU> x = full.Where(a => a.heo.TinhTrang == i).ToList();
                    foreach (HEOPHIEU h in x)
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
                    List<HEOPHIEU> x = full.Where(a => a.heo.NguonGoc == i).ToList();
                    foreach (HEOPHIEU h in x)
                    {
                        hEOs8.Add(h);
                    }
                }
            }
            else
                hEOs8 = full;
            IEnumerable<HEOPHIEU> heo = from HEOPHIEU a in hEOs
                                        join HEOPHIEU b in hEOs1
                                        on a.heo.MaHeo equals b.heo.MaHeo
                                        join HEOPHIEU c in hEOs2
                                    on a.heo.MaHeo equals c.heo.MaHeo
                                        join HEOPHIEU d in hEOs3
                                    on a.heo.MaHeo equals d.heo.MaHeo
                                        join HEOPHIEU e in hEOs4
                                    on a.heo.MaHeo equals e.heo.MaHeo
                                        join HEOPHIEU f in hEOs5
                                    on a.heo.MaHeo equals f.heo.MaHeo
                                        join HEOPHIEU g in hEOs6
                                    on a.heo.MaHeo equals g.heo.MaHeo
                                        join HEOPHIEU h in hEOs7
                                   on a.heo.MaHeo equals h.heo.MaHeo
                                        join HEOPHIEU j in hEOs8
                                    on a.heo.MaHeo equals j.heo.MaHeo
                                        orderby a.heo.MaHeo descending
                                        select a;

            foreach (HEOPHIEU h in heo)
            {
                ListHeoX.Add(h);
            }
        }
        bool KiemTra()
        {
            string msg;

            TimeSpan tuoiheo = (TimeSpan)(DateTime.Now.Date - SelectedHeo.heo.NgaySinh);
            if (tuoiheo.Days < thamso.MonthXuatChuongMin)
            {
                msg = "Heo chưa đến tuổi xuất chuồng";
                MessageBox.Show(msg);
                return false;
            }
            if (tuoiheo.Days > thamso.MonthXuatChuongMax)
            {
                msg = "Heo đã quá tuổi xuất chuồng";
                MessageBox.Show(msg);
                return false;
            }
            if (SelectedHeo.heo.TrongLuong < thamso.XuatChuongMin)
            {
                msg = "Heo chưa đủ cân nặng xuất chuồng";
                MessageBox.Show(msg);
                return false;
            }
            if (SelectedHeo.heo.TrongLuong > thamso.XuatChuongMax)
            {
                msg = "Heo đã quá cân nặng xuất chuồng";
                MessageBox.Show(msg);
                return false;
            }
            return true;
        }

    }
}
