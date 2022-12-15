using QuanLyTraiHeo.Model;
using QuanLyTraiHeo.View.Windows.Quản_lý_kho;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyTraiHeo.ViewModel
{
    class LapPhieuKhoWindowVM : BaseViewModel
    {
        public ObservableCollection<PHIEUHANGHOA> listPhieuNhapKho { get; set; }
        public ObservableCollection<PHIEUHANGHOA> listPhieuXuatKho { get; set; }
        public ObservableCollection<TrangThaiPhieuKhoModel> listTrangThai { get; set; }
        public int listviewSelectedIndexPhieuNhapKho { get; set; }
        public int listviewSelectedIndexPhieuXuatKho { get; set; }
        public string textTimKiemMaNhanVien { get; set; }
        public string textTimKiemMaDoiTac { get; set; }
        public ICommand TextTimKiemChangeCommand { get; set; }
        public ICommand TimKiemTheoNgayMinCommand { get; set; }
        public ICommand TimKiemTheoNgayMaxCommand { get; set; }
        public ICommand TaoPhieuCommand { get; set; }
        DateTime? mindate;
        DateTime? maxdate;

        public LapPhieuKhoWindowVM()
        {
            textTimKiemMaNhanVien = "";
            textTimKiemMaDoiTac = "";
            listTrangThai = new ObservableCollection<TrangThaiPhieuKhoModel>();
            listPhieuNhapKho = new ObservableCollection<PHIEUHANGHOA>();
            listPhieuXuatKho = new ObservableCollection<PHIEUHANGHOA>();
            TaoPhieuCommand = new RelayCommand<Window>((p) => { return true; }, p => { TaoPhieu(p); });
            TextTimKiemChangeCommand = new RelayCommand<ListView>((p) => { return true; }, p => { TextTimKiemChanged(p); });
            TimKiemTheoNgayMinCommand = new RelayCommand<DatePicker>((p) => { return true; }, p => { TimKiemTheoNgayMinChanged(p); });
            TimKiemTheoNgayMaxCommand = new RelayCommand<DatePicker>((p) => { return true; }, p => { TimKiemTheoNgayMaxChanged(p); });
            LoadListTrangThai();
            LoadPhieuHangHoa();
        }

        private void TaoPhieu(Window p)
        {
            TaoPhieuKho taophieukho = new TaoPhieuKho();
            taophieukho.ShowDialog();

        }

        private void LoadListTrangThai()
        {
            listTrangThai.Clear();
            var listtrangthai = from c in DataProvider.Ins.DB.PHIEUHANGHOAs
                                select new { c.TrangThai};
            var listtrangthainodupes = listtrangthai.Distinct().ToList();
            foreach (var items in listtrangthainodupes)
            {
                listTrangThai.Add(new TrangThaiPhieuKhoModel(true, items.TrangThai));
            }
        }

        private void TimKiemTheoNgayMaxChanged(DatePicker p)
        {
            maxdate = p.SelectedDate;
            LoadPhieuHangHoa();
        }

        private void TimKiemTheoNgayMinChanged(DatePicker p)
        {

            mindate = p.SelectedDate;
            LoadPhieuHangHoa();

        }

        private void TextTimKiemChanged(ListView p)
        {
            LoadPhieuHangHoa();
        }

        private void LoadPhieuHangHoa()
        {
            listPhieuNhapKho.Clear();
            listPhieuXuatKho.Clear();

            var listphieuhanghoa = DataProvider.Ins.DB.PHIEUHANGHOAs.ToList();
            //.Where(s => s.MaNhanVien.Contains(textTimKiemMaNhanVien) && s.MaDoiTac.Contains(textTimKiemMaDoiTac))
            if (mindate != null && mindate != DateTime.Now.Date)
            {
                listphieuhanghoa = listphieuhanghoa.Where(x => x.NgayLap >= mindate).ToList();
            }

            if (maxdate != null && maxdate != DateTime.Now.Date)
            {
                listphieuhanghoa = listphieuhanghoa.Where(x => x.NgayLap <= maxdate).ToList();
            }

            foreach (var items in listphieuhanghoa.ToList())
            {
                int flag = 0;
                foreach (var items2 in listTrangThai)
                {
                    if (items2.isSelected == false)
                        if (items.TrangThai == items2.trangThai)
                        {
                            flag = 1;
                            break;
                        }
                }
                if (flag != 0)
                    listphieuhanghoa.Remove(items);
            }

            foreach (var items in listphieuhanghoa)
            {
                if (items.LoaiPhieu == "Nhập kho")
                {
                    listPhieuNhapKho.Add(items);
                }
                else if (items.LoaiPhieu == "Xuất kho")
                {
                    listPhieuXuatKho.Add(items);
                }
            }

        }
    }
}
