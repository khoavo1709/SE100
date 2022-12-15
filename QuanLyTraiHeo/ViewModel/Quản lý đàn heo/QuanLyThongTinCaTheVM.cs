using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using QuanLyTraiHeo.Model;
using QuanLyTraiHeo.View.Windows;
using System.Windows.Controls;
using QuanLyTraiHeo.View.Windows.Quản_lý_nhân_viên;
using System.Net;
using Microsoft.Expression.Interactivity.Media;

namespace QuanLyTraiHeo.ViewModel
{
    public class QuanLyThongTinCaTheVM : BaseViewModel
    {
        public ObservableCollection<HEO> ListHeo { get; set; }
        public ObservableCollection<LOAIHEO> ListLoai { get; set; }
        public ObservableCollection<GIONGHEO> ListGiong { get; set; }



        public HEO SelectedHeo { get; set; }
        public LOAIHEO SelectedLoai { get; set; }
        public GIONGHEO SelectedGiong { get; set; }
        public List<string> ListTenLoai { get; set; }
        public List<string> ListTenGiong { get; set; }
        public List <string> ListTinhTrang { get; set; }
        public List<string> ListNguonGoc { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand ShowCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand TimKiemTheoMa_TenCommand { get; set; }
        public ICommand TimKiemTheoNgaySinhMinCommand { get; set; }
        public ICommand TimKiemTheoNgaySinhMaxCommand { get; set; }
        public ICommand TimKiemTheoTrongLuongMinCommand { get; set; }
        public ICommand TimKiemTheoTrongLuongMaxCommand { get; set; }
        public ICommand TimKiemTheoLoaiCommand { get; set; }
        public ICommand TimKiemTheoGiongCommand { get; set; }

        public ICommand TTCheck { get; set; }
        public ICommand NGCheck { get; set; }

        string matim;
        DateTime? mindate;
        DateTime? maxdate;
        int minTL=0;
        int maxTL=0;
        public QuanLyThongTinCaTheVM()
        {
            ListHeo = new ObservableCollection<HEO>(DataProvider.Ins.DB.HEOs);
            ListLoai = new ObservableCollection<LOAIHEO>(DataProvider.Ins.DB.LOAIHEOs);
            ListGiong = new ObservableCollection<GIONGHEO>(DataProvider.Ins.DB.GIONGHEOs);
            ListTenLoai = new List<string>();
            ListTenGiong = new List<string>();  
            ListTinhTrang = new List<string>();
            ListNguonGoc = new List<string>();


            AddCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                ThemTTHeo themTTHeo = new ThemTTHeo();
                themTTHeo.ShowDialog();
                ListHeo = new ObservableCollection<HEO>(DataProvider.Ins.DB.HEOs);

            });
            EditCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                SuaTTHeoVM suaTTHeoVM = new SuaTTHeoVM(SelectedHeo);
                SuaTTHeo suaTTHeo = new SuaTTHeo();
                suaTTHeo.DataContext = suaTTHeoVM;
                suaTTHeo.ShowDialog();
            });
            DeleteCommand = new RelayCommand<Window>((p) =>
            {
                if (SelectedHeo == null)
                    return false;
                else return true;
            }, p =>
            {
                if (SelectedHeo.CT_PHIEUHEO.Count() > 0 || SelectedHeo.LICHTIEMHEOs.Count() > 0 || SelectedHeo.LICHPHOIGIONGs.Count() > 0)
                {
                    MessageBox.Show("Không thẻ xoá con heo này. Vì đang tồn tại trong lịch hoặc phiếu?", "Chú ý");
                    return;
                }
                MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn xoá ?", "Cảnh báo", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataProvider.Ins.DB.HEOs.Remove(SelectedHeo);
                    ListHeo.Remove(SelectedHeo);
                    DataProvider.Ins.DB.SaveChanges();

                }
            }
            );

            TimKiemTheoMa_TenCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
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
                maxTL=int.Parse(p.Text);
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
               if(p.IsChecked==true)
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
        }
        void TimKiem()
        {
            List<HEO> full = DataProvider.Ins.DB.HEOs.ToList();
            List<HEO> hEOs;
            List<HEO> hEOs1;
            List<HEO> hEOs2;
            List<HEO> hEOs3;
            List<HEO> hEOs4;
            List<HEO> hEOs5 = new List<HEO>();
            List<HEO> hEOs6 = new List<HEO>();
            List<HEO> hEOs7 = new List<HEO>();
            List<HEO> hEOs8 = new List<HEO>();
            ListHeo.Clear();

            if (matim != null && matim != "")
                hEOs = full.Where(Heo => Heo.MaHeo.Contains(matim)).ToList();
            else hEOs = full;
            if (mindate != null && mindate != DateTime.Now.Date)
                hEOs1 = full.Where(x => x.NgaySinh >= mindate).ToList();
            else hEOs1 = full;

            if (maxdate != null && maxdate != DateTime.Now.Date)
                hEOs2 = full.Where(x => x.NgaySinh <= maxdate).ToList();
            else
                hEOs2 = full;
            if (minTL > 0)
                hEOs3 = full.Where(x => x.TrongLuong >= minTL).ToList();
            else hEOs3 = full;

            if (maxTL > minTL)
                hEOs4 = full.Where(x => x.TrongLuong <= maxTL).ToList();
            else
                hEOs4 = full;
            if (ListTenLoai.Count > 0)
            {
                foreach (string i in ListTenLoai)
                {
                    List<HEO> x = full.Where(a => a.LOAIHEO.TenLoaiHeo == i).ToList();
                    foreach (HEO h in x)
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
                    List<HEO> x = full.Where(a => a.GIONGHEO.TenGiongHeo == i).ToList();
                    foreach (HEO h in x)
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
                    List<HEO> x = full.Where(a => a.TinhTrang == i).ToList();
                    foreach (HEO h in x)
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
                    List<HEO> x = full.Where(a => a.NguonGoc == i).ToList();
                    foreach (HEO h in x)
                    {
                        hEOs8.Add(h);
                    }
                }
            }
            else
                hEOs8 = full;
            IEnumerable<HEO> heo = from HEO a in hEOs
                                   join HEO b in hEOs1
                                   on a.MaHeo equals b.MaHeo
                                   join HEO c in hEOs2
                                   on a.MaHeo equals c.MaHeo
                                   join HEO d in hEOs3
                                   on a.MaHeo equals d.MaHeo
                                   join HEO e in hEOs4
                                   on a.MaHeo equals e.MaHeo
                                   join HEO f in hEOs5
                                   on a.MaHeo equals f.MaHeo
                                   join HEO g in hEOs6
                                   on a.MaHeo equals g.MaHeo
                                   join HEO h in hEOs7
                                   on a.MaHeo equals h.MaHeo
                                   join HEO j in hEOs8
                                   on a.MaHeo equals j.MaHeo
                                   select a;

            foreach (HEO h in heo)
            {
                ListHeo.Add(h);
            }
        }
    }
}
