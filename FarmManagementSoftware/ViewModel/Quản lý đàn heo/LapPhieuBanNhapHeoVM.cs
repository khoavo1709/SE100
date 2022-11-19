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
    public class LapPhieuBanNhapHeoVM : BaseViewModel
    {
        public ObservableCollection<PHIEUHEO> ListPhieuNhap { get; set; }
        public ObservableCollection<PHIEUHEO> ListPhieuXuat { get; set; }

        public List<string> ListTrangThai { get; set; }
        public string TenNV { get; set; }
        public string TenKH { get; set; }
        DateTime? mindate;
        DateTime? maxdate;

        public ICommand TaoPhieuCommand { get; set; }
        public ICommand TimKiemTheoTenNVCommand { get; set; }
        public ICommand TimKiemTheoNgayMinCommand { get; set; }
        public ICommand TimKiemTheoNgayMaxCommand { get; set; }
        public ICommand TimKiemTheoTenKHCommand { get; set; }
        public ICommand TTCheck { get; set; }

        public LapPhieuBanNhapHeoVM()
        {
            ListTrangThai = new List<string>();
            ListPhieuNhap = new ObservableCollection<PHIEUHEO>(DataProvider.Ins.DB.PHIEUHEOs.Where(x => x.LoaiPhieu == "Phiếu nhập heo"));
            ListPhieuXuat = new ObservableCollection<PHIEUHEO>(DataProvider.Ins.DB.PHIEUHEOs.Where(x => x.LoaiPhieu == "Phiếu xuất heo"));

            TaoPhieuCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                PhieuNhapBanHeo Phieu = new PhieuNhapBanHeo();

                Phieu.ShowDialog();
            });
            TimKiemTheoNgayMinCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                if (p.SelectedDate != DateTime.Today && p.SelectedDate != null)
                {
                    mindate = p.SelectedDate;
                    TimKiem();
                }
            });
            TimKiemTheoNgayMaxCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                if (p.SelectedDate != DateTime.Today && p.SelectedDate != null)
                {
                    maxdate = p.SelectedDate;
                    TimKiem();
                }
            });
            TimKiemTheoTenNVCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                TenNV = p.Text;
                TimKiem();
            });
            TimKiemTheoTenKHCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                TenKH = p.Text;
                TimKiem();
            });
            TTCheck = new RelayCommand<CheckBox>((p) => { return true; }, p =>
            {
                if (p.IsChecked == true)
                    ListTrangThai.Add(p.Content.ToString());
                else ListTrangThai.Remove(p.Content.ToString());
                TimKiem();
            });
        }
        void TimKiem()
        {
            ListPhieuNhap.Clear();
            ListPhieuXuat.Clear();
            List<PHIEUHEO> full = DataProvider.Ins.DB.PHIEUHEOs.Where(x => x.LoaiPhieu != null).ToList();
            List<PHIEUHEO> phieutheoNV;
            List<PHIEUHEO> phieutheoKH;
            List<PHIEUHEO> phieutheoNgayMin;
            List<PHIEUHEO> phieutheoNgayMax;
            List<PHIEUHEO> phieutheoTrangThai = new List<PHIEUHEO>();

            if (TenNV != null)
            {
                phieutheoNV = full.Where(x => x.NHANVIEN.HoTen.Contains(TenNV)).ToList();
            }
            else { phieutheoNV = full; }

            if (TenKH != null)
            {
                phieutheoKH = full.Where(x => x.DOITAC.TenDoiTac.Contains(TenKH)).ToList();
            }
            else { phieutheoKH = full; }
            if (mindate != null)
            {
                phieutheoNgayMin = full.Where(x => x.NgayLap >= mindate).ToList();
            }
            else { phieutheoNgayMin = full; }
            if (maxdate != null && maxdate >= mindate)
            {
                phieutheoNgayMax = full.Where(x => x.NgayLap <= maxdate).ToList();
            }
            else { phieutheoNgayMax = full; }

            if (ListTrangThai.Count > 0)
            {
                foreach (string i in ListTrangThai)
                {
                    List<PHIEUHEO> x = DataProvider.Ins.DB.PHIEUHEOs.Where(a => a.TrangThai == i).ToList();
                    foreach (PHIEUHEO h in x)
                    {
                        phieutheoTrangThai.Add(h);
                    }
                }
            }
            else phieutheoTrangThai = full;
            IEnumerable<PHIEUHEO> phieuheo = from PHIEUHEO a in phieutheoNV
                                             join PHIEUHEO b in phieutheoKH
                                             on a.SoPhieu equals b.SoPhieu
                                             join PHIEUHEO c in phieutheoNgayMin
                                             on a.SoPhieu equals c.SoPhieu
                                             join PHIEUHEO d in phieutheoNgayMax
                                             on a.SoPhieu equals d.SoPhieu
                                             join PHIEUHEO e in phieutheoTrangThai
                                             on a.SoPhieu equals e.SoPhieu
                                             select a;
            foreach (PHIEUHEO h in phieuheo)
            {
                if (h.LoaiPhieu == "Phiếu nhập heo")
                    ListPhieuNhap.Add(h);
                else
                    ListPhieuXuat.Add(h);
            }
        }
    }
}
