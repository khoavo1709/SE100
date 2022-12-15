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
    public class ChiTietPhieuVM : BaseViewModel
    {
        public ObservableCollection<HEO> ListHeo { get; set; }
        public string MaPhieu { get => _MaPhieu; set { _MaPhieu = value; OnPropertyChanged(); } }
        public string LoaiPhieu { get => _LoaiPhieu; set { _LoaiPhieu = value; OnPropertyChanged(); } }
        public DateTime? NgayLap { get => _NgayLap; set { _NgayLap = value; OnPropertyChanged(); } }
        public int DonGia { get; set; }

        private int _TongTien = 0;
        public int TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }
        public DOITAC KhachHang { get; set; }

        private string _MaKH;
        public string MaKH { get => _MaKH; set { _MaKH = value; OnPropertyChanged(); } }

        private string _Ten;
        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }

        private string _Mail;
        public string Mail { get => _Mail; set { _Mail = value; OnPropertyChanged(); } }
        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }

        private string _DiaChi;
        private string _MaPhieu;
        private string _TrangThai;
        private DateTime? _NgayLap;
        private string _LoaiPhieu;
        public ICommand HoanTatCommand { get; set; }
        public ICommand HuyBoCommand { get; set; }

        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }

        public NHANVIEN NhanVien { get; set; }
        public string TenNV { get; set; }
        public string TrangThai { get => _TrangThai; set { _TrangThai = value; OnPropertyChanged(); } }

        public List <CT_PHIEUHEO> CT { get; set; }
        public ChiTietPhieuVM()
        { 
        }

        public ChiTietPhieuVM(PHIEUHEO selectedPhieu)
        {
            CT = DataProvider.Ins.DB.CT_PHIEUHEO.Where(x => x.SoPhieu == selectedPhieu.SoPhieu).ToList();
            ListHeo = new ObservableCollection<HEO>();
            foreach (var phieu in CT)
            {
                ListHeo.Add(DataProvider.Ins.DB.HEOs.Where(x=>x.MaHeo==phieu.MaHeo).First());
            }    
            NhanVien = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNhanVien == selectedPhieu.MaNhanVien).First();
            KhachHang = DataProvider.Ins.DB.DOITACs.Where(x => x.MaDoiTac == selectedPhieu.MaDoiTac).First();
            TenNV = NhanVien.HoTen;
            MaKH = KhachHang.MaDoiTac;
            Ten = KhachHang.TenDoiTac;
            Mail = KhachHang.Email;
            SDT = KhachHang.SDT;
            DiaChi = KhachHang.DiaChi;
            MaPhieu = selectedPhieu.SoPhieu;
            TrangThai = selectedPhieu.TrangThai;
            LoaiPhieu = selectedPhieu.LoaiPhieu;
            NgayLap = selectedPhieu.NgayLap;
            TongTien = (int)selectedPhieu.TongTien;

            HoanTatCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                
                selectedPhieu.TrangThai = TrangThai;
                foreach (HEO x in ListHeo)
                {
                    if(TrangThai == "Đã huỷ")
                    {
                        x.TinhTrang = "Sức khoẻ tốt";
                    }    
                }
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Đã sửa");
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
    }

}
