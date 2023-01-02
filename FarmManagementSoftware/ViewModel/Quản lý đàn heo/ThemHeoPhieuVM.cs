using FarmManagementSoftware.Model;
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
    public class ThemHeoPhieuVM : BaseViewModel
    {
        public ObservableCollection<HEONHAP> ListHeoAdd { get; set; }
        public ObservableCollection<LOAIHEO> ListLoai { get; set; }
        public ObservableCollection<GIONGHEO> ListGiong { get; set; }
        public ObservableCollection<CHUONGTRAI> ListChuong { get; set; }



        public HEONHAP HeoAdd { get; set; }

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

        public int DonGia { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand HTCommand { get; set; }
        public ThemHeoPhieuVM(ObservableCollection <HEONHAP> a)
        {
            
            ListHeoAdd = new ObservableCollection<HEONHAP>();
            ListLoai = new ObservableCollection<LOAIHEO>(DataProvider.Ins.DB.LOAIHEOs);
            ListGiong = new ObservableCollection<GIONGHEO>(DataProvider.Ins.DB.GIONGHEOs);
            ListChuong = new ObservableCollection<CHUONGTRAI>(DataProvider.Ins.DB.CHUONGTRAIs.Where(x => x.SuaChuaToiDa > x.SoLuongHeo).ToList());
            ListHeoAdd = a;
            MaHeo = LayMa();

            AddCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if (GioiTinh == null)
                {
                    MessageBox.Show("Vui lòng chọn giới tính");
                    return; 
                }
                if (NgaySinh == null)
                {
                    MessageBox.Show("Vui lòng chọn ngày sinh");
                    return;
                }
                if (TrongLuong == null || TrongLuong < 0)
                {
                    MessageBox.Show("Vui lòng nhập đúng trọng lượng");
                    return;
                }
                if (SelectedLoai == null)
                {
                    MessageBox.Show("Vui lòng chọn loại heo");
                    return;
                }
                if (SelectedGiong == null)
                {
                    MessageBox.Show("Vui lòng chọn giống heo");
                    return;
                }
                if (SelectedChuong == null)
                {
                    MessageBox.Show("Vui lòng chọn mã chuồng");
                    return;
                }
                if (TinhTrang == null)
                {
                    MessageBox.Show("Vui lòng chọn tình trạng");
                    return;
                }
                if (NguonGoc == null)
                {
                    MessageBox.Show("Vui lòng chọn nguồn gốc");
                    return;
                }

                HeoAdd = new HEONHAP();
                MaHeo = LayMa();
                HeoAdd.heo.MaHeo = MaHeo;
                HeoAdd.heo.GioiTinh = GioiTinh;
                HeoAdd.heo.NgaySinh = NgaySinh;
                HeoAdd.heo.TrongLuong = TrongLuong;
                HeoAdd.heo.MaLoaiHeo = SelectedLoai.MaLoaiHeo;
                HeoAdd.heo.MaGiongHeo = SelectedGiong.MaGiongHeo;
                HeoAdd.heo.MaHeoMe = MaHeoMe;
                HeoAdd.heo.MaHeoCha = MaHeoCha;
                HeoAdd.heo.MaChuong = SelectedChuong.MaChuong;
                HeoAdd.heo.NguonGoc = NguonGoc;
                HeoAdd.heo.TinhTrang = TinhTrang;
                HeoAdd.DonGia = DonGia;
                a.Add(HeoAdd);
                GioiTinh = "";
                NgaySinh = null;
                TrongLuong = 0;
                MaLoaiHeo = "";
                MaGiongHeo = "";
                MaChuong = "";
                TinhTrang = "";
                NguonGoc = "";
                p.Close();
            });

            HTCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                p.Close();
                ListHeoAdd = null;
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
