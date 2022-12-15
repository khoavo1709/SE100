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

namespace QuanLyTraiHeo.ViewModel
{
    public class ThemTTHeoVM : BaseViewModel
    {
        public ObservableCollection<HEO> ListHeoAdd { get; set; }
        public ObservableCollection<LOAIHEO> ListLoai { get; set; }
        public ObservableCollection<GIONGHEO> ListGiong { get; set; }
        public ObservableCollection<CHUONGTRAI> ListChuong { get; set; }



        public HEO HeoAdd { get; set; }
        public LOAIHEO SelectedLoai { get; set; }
        public GIONGHEO SelectedGiong { get; set; }
        public CHUONGTRAI SelectedChuong { get; set; }
        public string MaHeo { get; set; }
        public string MaLoaiHeo { get; set; }
        public string MaGiongHeo { get; set; }
        public string GioiTinh { get; set; }
        public int TrongLuong { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string MaChuong { get; set; }
        public string MaHeoCha { get; set; }
        public string MaHeoMe { get; set; }
        public string NguonGoc { get; set; }
        public string TinhTrang { get; set; }



        public ICommand AddCommand { get; set; }
        public ICommand HuyCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }


        public ThemTTHeoVM()
        {
            ListHeoAdd = new ObservableCollection<HEO>();
            ListLoai = new ObservableCollection<LOAIHEO>(DataProvider.Ins.DB.LOAIHEOs);
            ListGiong = new ObservableCollection<GIONGHEO>(DataProvider.Ins.DB.GIONGHEOs);
            ListChuong = new ObservableCollection<CHUONGTRAI>(DataProvider.Ins.DB.CHUONGTRAIs);
            MaHeo = LayMa();

            AddCommand = new RelayCommand<TextBox>((p) => {
                if (SelectedChuong == null || SelectedGiong == null || SelectedLoai == null)
                    return false;

                if (GioiTinh == null || TinhTrang == null || NguonGoc == null || NgaySinh == null || TrongLuong == 0 || NgaySinh > DateTime.Today)
                    return false;

                return true;
            }, p =>
            {
                HeoAdd = new HEO();
                MaHeo = LayMa();
                HeoAdd.MaHeo = MaHeo;
                HeoAdd.GioiTinh = GioiTinh;
                HeoAdd.NgaySinh = NgaySinh;
                HeoAdd.TrongLuong = TrongLuong;
                HeoAdd.MaLoaiHeo = SelectedLoai.MaLoaiHeo;
                HeoAdd.MaGiongHeo = SelectedGiong.MaGiongHeo;
                HeoAdd.MaHeoMe = MaHeoMe;
                HeoAdd.MaHeoCha = MaHeoCha;
                HeoAdd.MaChuong = SelectedChuong.MaChuong;
                HeoAdd.NguonGoc = NguonGoc;
                HeoAdd.TinhTrang = TinhTrang;
                ListHeoAdd.Add(HeoAdd);
                p.Text = LayMa();
            });

            HuyCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                p.Close();
                ListHeoAdd = null;
            });

            DeleteCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                ListHeoAdd.Remove(HeoAdd);
            });
            XacNhanCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                foreach (var item in ListHeoAdd)
                {
                    DataProvider.Ins.DB.HEOs.Add(item);
                }
                DataProvider.Ins.DB.SaveChanges();
                p.Close();
            });

        }
        string CreatMaHeo(int lan)
        {
            ObservableCollection<HEO> Heos = new ObservableCollection<HEO>(DataProvider.Ins.DB.HEOs);
            int soHeo;
            if (ListHeoAdd != null)
            { soHeo = Heos.Count + ListHeoAdd.Count + lan; }
            else
            {
                soHeo = Heos.Count + lan;
            }
            string maHeo;
            if (soHeo == 0)
            {
                maHeo = "HEO000001" + DateTime.Now.ToString("_ddMM");
            }
            else
            {
                int STT = soHeo;
                STT++;
                string strSTT = STT.ToString();
                for (int i = strSTT.Length; i <= 5; i++)
                {
                    strSTT = "0" + strSTT;
                }

                maHeo = "HEO" + strSTT + DateTime.Now.ToString("_ddMM");
            }
            return maHeo;
        }
        string LayMa()
        {
            string MaCu = CreatMaHeo(0);
            int i = 0;
            var SL = new List<HEO>(DataProvider.Ins.DB.HEOs.Where(x => x.MaHeo == MaCu));
            while (SL.Count > 0)
            {
                i++;
                MaCu = CreatMaHeo(i);
                SL = new List<HEO>(DataProvider.Ins.DB.HEOs.Where(x => x.MaHeo == MaCu));
            }
            return MaCu;
        }
    }

}
