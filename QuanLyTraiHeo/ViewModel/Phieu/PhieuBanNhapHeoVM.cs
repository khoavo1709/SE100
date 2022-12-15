using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyTraiHeo.Model;
using QuanLyTraiHeo.View.Windows;

namespace QuanLyTraiHeo.ViewModel
{
    public class PhieuBanNhapHeoVM : BaseViewModel
    {
        public ObservableCollection<HEO> ListHeo { get; set; }
    
        public PHIEUHEO PhieuHeo { get; set; }
        private string _MaPhieu;
        public string MaPhieu { get => _MaPhieu; set { _MaPhieu = value; OnPropertyChanged(); } }
        public string loaiPhieu = null;
        public DateTime? NgayLap { get; set; }
        public int DonGia { get; set; }

        private int _TongTien=0;
        public int TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }


        public DOITAC KhachHang { get; set; }

        private string _MaKH;
        public string MaKH { get => _MaKH; set { _MaKH = value; OnPropertyChanged(); } }

        private string _Ten;
        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }

        private string _Mail;
        public string Mail { get => _Mail; set { _Mail = value;OnPropertyChanged(); } }
        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }

        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }

        public NHANVIEN NhanVien { get; set; }
        public string TenNV {get;set;}
        public ICommand SelectedLoaiPhieu { get; set; }
        public ICommand ThemHeo { get; set; }
        public ICommand DonGiaChanged { get; set; }
        public ICommand ThayDoiMaHK { get; set; }
        public ICommand HoanTatCommand { get; set; }
        public ICommand HuyBoCommand { get; set; }

        public PhieuBanNhapHeoVM()
        {
            
            ListHeo = new ObservableCollection<HEO>();
            PhieuHeo = new PHIEUHEO();
            KhachHang = new DOITAC();
            NhanVien = new NHANVIEN();
            var NV = DataProvider.Ins.DB.NHANVIENs.Where(x => x.C_Username == Properties.Settings.Default.Username).First();
            NhanVien = NV;
            TenNV = NhanVien.HoTen;
            SelectedLoaiPhieu = new RelayCommand<ComboBox>((p) => { return true; }, p =>
            {
                ComboBoxItem z = (ComboBoxItem)p.SelectedItem;
                loaiPhieu = z.Content.ToString();
                MaPhieu = LayMa();
                p.IsEnabled = false;
            });
            ThemHeo = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (loaiPhieu == null)
                { 
                    MessageBox.Show("Vui lòng chọn loại phiếu trước");
                    return;
                }
                if (loaiPhieu == "Phiếu nhập heo")
                {
                    ThemHeoPhieu them = new ThemHeoPhieu
                    {
                        DataContext = new ThemHeoPhieuVM(ListHeo)
                    };

                    them.ShowDialog();
                }
                if (loaiPhieu == "Phiếu xuất heo")
                {
                    ChonHeoXuatVM vm = new ChonHeoXuatVM(this);
                    ChonHeoXuat chon = new ChonHeoXuat
                    {
                        DataContext = vm
                    };
                    chon.ShowDialog();          
                }
            });
            DonGiaChanged = new RelayCommand <TextBox>((p) => { return true; }, p =>
            {
                int.TryParse(p.Text, out int DonGia);
                if(ListHeo.Count > 0)
                    foreach (HEO x in ListHeo)
                    {
                        TongTien += (int)x.TrongLuong * DonGia;
                    }
            });
            ThayDoiMaHK = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                var ListKH = DataProvider.Ins.DB.DOITACs.Where(x => x.MaDoiTac == p.Text).ToList();
                if(ListKH.Count>0)
                {
                    KhachHang = ListKH.First();
                    MaKH = KhachHang.MaDoiTac;
                    Ten = KhachHang.TenDoiTac;
                    SDT = KhachHang.SDT;
                    Mail = KhachHang.Email;
                    DiaChi= KhachHang.DiaChi;
                }   
                else
                {
                    Ten = "";
                    SDT = "";
                    Mail = "";
                    DiaChi = "";
                }    
            });

            HoanTatCommand= new RelayCommand<Window>((p) => 
            { 
                return true;
            }, (p) =>
            {
               if (DataProvider.Ins.DB.DOITACs.Where(x=>x.MaDoiTac==KhachHang.MaDoiTac).Count()!=1)
                {
                    KhachHang.MaDoiTac = MaKH;
                    KhachHang.LoaiDoiTac = "Khách hàng";
                    KhachHang.TenDoiTac = Ten;
                    KhachHang.SDT= SDT;
                    KhachHang.Email = Mail;
                    KhachHang.SDT = SDT;
                    KhachHang.DiaChi = DiaChi;
                    DataProvider.Ins.DB.DOITACs.Add(KhachHang);
                }
                PhieuHeo.SoPhieu = MaPhieu;
                PhieuHeo.LoaiPhieu = loaiPhieu;
                PhieuHeo.MaNhanVien = NhanVien.MaNhanVien;
                PhieuHeo.MaDoiTac = KhachHang.MaDoiTac;
                PhieuHeo.NgayLap = NgayLap;
                PhieuHeo.TrangThai = "Chưa hoàn thành";

                foreach (HEO x in ListHeo)
                {
                    if (loaiPhieu == "Phiếu nhập heo")
                        DataProvider.Ins.DB.HEOs.Add(x);
                    CT_PHIEUHEO CT = new CT_PHIEUHEO
                    {
                        MaHeo = x.MaHeo,
                        SoPhieu = MaPhieu,
                        DonGia = DonGia,
                        TrongLuong = x.TrongLuong
                    };
                    TongTien += (int)x.TrongLuong * DonGia;
                    DataProvider.Ins.DB.CT_PHIEUHEO.Add(CT);
                    if (loaiPhieu == "Phiếu xuất heo")
                    {
                        x.TinhTrang = "Đã xuất";
                    }
                }

                PhieuHeo.TongTien = TongTien;
                DataProvider.Ins.DB.PHIEUHEOs.Add(PhieuHeo);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Thêm thành công");
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
        string CreatPhieuHeo(int lan)
        {
            ObservableCollection<PHIEUHEO> PhieuHeos = new ObservableCollection<PHIEUHEO>(DataProvider.Ins.DB.PHIEUHEOs);
            int soPhieu;
            soPhieu = PhieuHeos.Count + lan;        
            string maPhieuHeo="";
            if (soPhieu == 0)
            {
                if (loaiPhieu == "Phiếu nhập heo")
                {
                    maPhieuHeo = "PN000001";
                }
                if (loaiPhieu == "Phiếu xuất heo")
                {
                    maPhieuHeo = "PX000001";
                }

            }
            else
            {
                int STT = soPhieu;
                STT++;
                string strSTT = STT.ToString();
                for (int i = strSTT.Length; i <= 5; i++)
                {
                    strSTT = "0" + strSTT;
                }

                if (loaiPhieu == "Phiếu nhập heo")
                {
                    maPhieuHeo = "PN" + strSTT ;
                }
                if (loaiPhieu == "Phiếu xuất heo")
                {
                    maPhieuHeo = "PX" + strSTT ;
                }
            }
            return maPhieuHeo;
        }
        string LayMa()
        {
            string MaCu = CreatPhieuHeo(0);
            int i = 0;
            var SL = new List<PHIEUHEO>(DataProvider.Ins.DB.PHIEUHEOs.Where(x => x.SoPhieu == MaCu));
            while (SL.Count > 0)
            {
                i++;
                MaCu = CreatPhieuHeo(i);
                SL = new List<PHIEUHEO>(DataProvider.Ins.DB.PHIEUHEOs.Where(x => x.SoPhieu == MaCu));
            }
            return MaCu;
        }

    }
}
