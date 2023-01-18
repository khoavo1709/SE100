using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.Windows.Quản_lý_giống_heo;
using FarmManagementSoftware.View.Windows.Quản_lý_loại_heo;
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
using FarmManagementSoftware.View.Windows.Thiết_lập_cây_mục_tiêu;
using System.Windows.Documents;

namespace FarmManagementSoftware.ViewModel
{
    public class ThemHeoPhieuVM : BaseViewModel
    {
        public ObservableCollection<HEOPHIEU> ListHeoAdd { get; set; }
        public ObservableCollection<LOAIHEO> ListLoai { get; set; }
        public ObservableCollection<GIONGHEO> ListGiong { get; set; }
        public ObservableCollection<CHUONGTRAI> ListChuong { get; set; }



        public HEOPHIEU HeoAdd { get; set; }
        public HEO SelectedMe { get => _SelectedMe; set { _SelectedMe = value; OnPropertyChanged(); } }
        public HEO SelectedCha { get => _SelectedCha; set { _SelectedCha = value; OnPropertyChanged(); } }
        public LOAIHEO SelectedLoai { get; set; }
        public GIONGHEO SelectedGiong { get; set; }
        public CHUONGTRAI SelectedChuong { get; set; }
        public List<HEO> ListCha { get; }
        public List<HEO> ListMe { get; }
        THAMSO thamso = new THAMSO();


        public string MaHeo { get => _MaHeo; set { _MaHeo = value; OnPropertyChanged(); } }
        public string MaLoaiHeo { get => _MaLoaiHeo; set { _MaLoaiHeo = value; OnPropertyChanged(); } }
        public string MaGiongHeo { get => _MaGiongHeo; set { _MaGiongHeo = value; OnPropertyChanged(); } }
        public string GioiTinh { get => _GioiTinh; set { _GioiTinh = value; OnPropertyChanged(); } }
        public int TrongLuong { get => _TrongLuong; set { _TrongLuong = value; OnPropertyChanged(); } }
        public DateTime? NgaySinh { get => _NgaySinh; set { _NgaySinh = value; OnPropertyChanged(); } }
        public string MaChuong { get => _MaChuong; set { _MaChuong = value; OnPropertyChanged(); } }
        public string MaHeoCha { get; set; }
        public string MaHeoMe { get; set; }
        public string NguonGoc { get => _NguonGoc; set { _NguonGoc = value; OnPropertyChanged(); } }
        public string TinhTrang { get => _TinhTrang; set { _TinhTrang = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand HuyCommand { get; set; }
        private int _DonGia;
        public int DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); }}

        PhieuBanNhapHeoVM goc;
        private HEO _SelectedMe;
        private HEO _SelectedCha;
        private string _MaHeo;
        private string _MaLoaiHeo;
        private string _MaGiongHeo;
        private string _GioiTinh;
        private int _TrongLuong;
        private DateTime? _NgaySinh;
        private string _MaChuong;
        private string _NguonGoc;
        private string _TinhTrang;

        public ThemHeoPhieuVM(PhieuBanNhapHeoVM a)
        {
            goc = a;
            thamso = DataProvider.Ins.DB.THAMSOes.First();
            HEO X = new HEO();
            X.MaHeo = "Không chọn";
            SelectedMe = SelectedCha = X;
            NguonGoc = "Sinh trong trang trại";
            TinhTrang = "Sức khoẻ tốt";
            ListMe = new ObservableCollection<HEO>().ToList();
            ListCha = new ObservableCollection<HEO>().ToList();
            ListLoai = new ObservableCollection<LOAIHEO>(DataProvider.Ins.DB.LOAIHEOs);
            ListCha.Add(X);
            var Cha = new ObservableCollection<HEO>(DataProvider.Ins.DB.HEOs.Where(x => x.GioiTinh == "Đực")).ToList();

            foreach (HEO x in Cha)
            {
                ListCha.Add(x);
            }
            ListMe.Add(X);
            var Me = new ObservableCollection<HEO>(DataProvider.Ins.DB.HEOs.Where(x => x.GioiTinh == "Cái")).ToList();
            foreach (HEO x in Me)
            {
                ListMe.Add(x);
            }
            HeoAdd = new HEOPHIEU();
            ListLoai = new ObservableCollection<LOAIHEO>(DataProvider.Ins.DB.LOAIHEOs);
            ListGiong = new ObservableCollection<GIONGHEO>(DataProvider.Ins.DB.GIONGHEOs);
            ListChuong = new ObservableCollection<CHUONGTRAI>(DataProvider.Ins.DB.CHUONGTRAIs.Where(x => x.SuaChuaToiDa > x.SoLuongHeo).ToList());
            MaHeo = LayMa();

            AddCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                HeoAdd = new HEOPHIEU();
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
                if ( TrongLuong <=   0)
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

                MaHeo = LayMa();
                HeoAdd.heo.MaHeo = MaHeo;
                HeoAdd.heo.GioiTinh = GioiTinh;
                HeoAdd.heo.NgaySinh = NgaySinh;
                HeoAdd.heo.TrongLuong = TrongLuong;
                HeoAdd.heo.LOAIHEO = new LOAIHEO();
                HeoAdd.heo.LOAIHEO = SelectedLoai;
                HeoAdd.heo.GIONGHEO = new GIONGHEO();
                HeoAdd.heo.GIONGHEO = SelectedGiong;
                if (SelectedMe.MaHeo != "Không chọn")
                    HeoAdd.heo.MaHeoMe = SelectedMe.MaHeo;
                if (SelectedCha.MaHeo != "Không chọn")
                    HeoAdd.heo.MaHeoCha = SelectedCha.MaHeo; ;
                HeoAdd.heo.CHUONGTRAI = new CHUONGTRAI();
                HeoAdd.heo.CHUONGTRAI = SelectedChuong;
                HeoAdd.heo.NguonGoc = NguonGoc;
                HeoAdd.heo.TinhTrang = TinhTrang;
                HeoAdd.DonGia = DonGia;
                if (!KiemTra())
                    return;
                a.ListHeo.Add(HeoAdd);
                ClearForm();
            });

            HuyCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                ClearForm();
                p.Close();
            });


        }
        string CreatMaHeo(int lan)
        {
            ObservableCollection<HEO> Heos = new ObservableCollection<HEO>(DataProvider.Ins.DB.HEOs);
            int soHeo;
            if (goc.ListHeo != null)
            { soHeo = Heos.Count + goc.ListHeo.Count + lan; }
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
        bool KiemTra()
        {
            string msg;
            if (HeoAdd.heo.GioiTinh == "Cái" && HeoAdd.heo.LOAIHEO.TenLoaiHeo.Contains("đực"))
            {
                msg = "Chọn sai giới tính hoặc loại heo";
                MessageBox.Show(msg);
                return false;
            }
            if (HeoAdd.heo.GioiTinh == "Đực" && HeoAdd.heo.LOAIHEO.TenLoaiHeo.Contains("nái"))
            {
                msg = "Chọn sai giới tính hoặc loại heo";
                MessageBox.Show(msg);
                return false;
            }
            TimeSpan tuoiheo = (TimeSpan)(DateTime.Now.Date - HeoAdd.heo.NgaySinh);
            if (tuoiheo.Days < thamso.TuoiNhapDan && HeoAdd.heo.LOAIHEO.TenLoaiHeo != "Heo con")
            {
                msg = "Heo còn quá nhỏ, chưa thể nhập đàn";
                MessageBox.Show(msg);
                return false;
            }
            if (HeoAdd.heo.MaHeoMe != null && HeoAdd.heo.MaHeoCha != null)
            {
                if (!(HeoAdd.heo.LOAIHEO.TenLoaiHeo.Contains("con")) && (HeoAdd.heo.MaHeoMe != "Không chọn" && HeoAdd.heo.MaHeoCha != "Không chọn"))
                {
                    msg = "Chỉ chọn heo cha, heo mẹ cho heo thuộc loại heo con";
                    MessageBox.Show(msg);
                    return false;
                }
                if ((HeoAdd.heo.LOAIHEO.TenLoaiHeo.Contains("con")) && (HeoAdd.heo.MaHeoMe == "Không chọn" || HeoAdd.heo.MaHeoCha == "Không chọn"))
                {
                    msg = "Heo con phải có cả heo cha và heo mẹ";
                    MessageBox.Show(msg);
                    return false;
                }
            }

            if (HeoAdd.heo.LOAIHEO.TenLoaiHeo.Contains("nái"))
                if(!HeoAdd.heo.CHUONGTRAI.MaChuong.Contains("HN") && !HeoAdd.heo.CHUONGTRAI.MaChuong.Contains("HD"))
                    {
                        msg = "Chuồng hiện tại không phù hợp với loại heo nái";
                        MessageBox.Show(msg);
                        return false;
                    }
            if (HeoAdd.heo.LOAIHEO.TenLoaiHeo.Contains("con") && HeoAdd.heo.CHUONGTRAI.MaChuong.Contains("DG"))
            {
                msg = "Heo con không thể ở chuồng đực giống";
                MessageBox.Show(msg);
                return false;
            }
            if (HeoAdd.heo.LOAIHEO.TenLoaiHeo.Contains("đực") && (HeoAdd.heo.CHUONGTRAI.MaChuong.Contains("N") || HeoAdd.heo.CHUONGTRAI.MaChuong.Contains("HD")))
            {
                msg = "Heo đực không thể ở chuồng heo nái khác";
                MessageBox.Show(msg);
                return false;
            }
            if (HeoAdd.heo.LOAIHEO.TenLoaiHeo.Contains("thịt") && !HeoAdd.heo.CHUONGTRAI.MaChuong.Contains("T"))
            {
                msg = "Heo thịt chỉ có thể ở chuồng heo thịt";
                MessageBox.Show(msg);
                return false;
            }
            return true;
        }

        void ClearForm()
        {
            HeoAdd = null;
            MaHeo = LayMa();
            GioiTinh = null;
            NgaySinh = null;
            TrongLuong = 0;
            DonGia = 0;
            SelectedLoai = null;
            MaLoaiHeo = null;
            SelectedGiong = null;
            MaGiongHeo = null;
            MaHeoCha = MaHeoMe = "Không chọn";
            SelectedChuong = null;
            MaChuong = null;
            HEO X = new HEO();
            X.MaHeo = "Không chọn";
            SelectedMe = SelectedCha = X;
            NguonGoc = "Sinh trong trang trại";
            TinhTrang = "Sức khoẻ tốt";

        }
    }
}
