using QuanLyTraiHeo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyTraiHeo.ViewModel
{
    public class SuaTTHeoVM : BaseViewModel
    {
        public ObservableCollection<LOAIHEO> ListLoai { get; set; }
        public ObservableCollection<GIONGHEO> ListGiong { get; set; }
        public ObservableCollection<CHUONGTRAI> ListChuong { get; set; }


        public HEO SelectedHeo { get; set; }
        public LOAIHEO SelectedLoai { get; set; }
        public GIONGHEO SelectedGiong { get; set; }
        public CHUONGTRAI SelectedChuong { get; set; }

        public ICommand XacNhanCommand { get; set; }

        public SuaTTHeoVM()
        {
            return;
        }
        public SuaTTHeoVM(HEO selectedHeo)
        {
            SelectedHeo = selectedHeo;
            SelectedLoai = SelectedHeo.LOAIHEO;
            SelectedChuong = SelectedHeo.CHUONGTRAI;
            SelectedGiong = SelectedHeo.GIONGHEO;
            ListLoai = new ObservableCollection<LOAIHEO>(DataProvider.Ins.DB.LOAIHEOs);
            ListGiong = new ObservableCollection<GIONGHEO>(DataProvider.Ins.DB.GIONGHEOs);
            ListChuong = new ObservableCollection<CHUONGTRAI>(DataProvider.Ins.DB.CHUONGTRAIs);


            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                if (SelectedChuong == null || SelectedGiong == null || SelectedLoai == null)
                    return false;
                SelectedHeo.MaGiongHeo = SelectedGiong.MaGiongHeo;
                SelectedHeo.MaLoaiHeo = SelectedLoai.MaLoaiHeo;
                SelectedHeo.MaChuong = SelectedChuong.MaChuong;
                if (selectedHeo.GioiTinh == null || selectedHeo.TinhTrang == null || selectedHeo.NguonGoc == null || selectedHeo.NgaySinh == null || selectedHeo.TrongLuong == null)
                    return false;
                return true;
            }, p =>
            {
                DataProvider.Ins.DB.SaveChanges();
                p.Close();
            });
        }
    }
}