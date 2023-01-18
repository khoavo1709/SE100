﻿using System;
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
using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.Windows;

namespace FarmManagementSoftware.ViewModel
{
    public class PhieuBanNhapHeoVM : BaseViewModel
    {
        public ObservableCollection<HEOPHIEU> ListHeo { get => _ListHeo; set { _ListHeo = value; OnPropertyChanged(); } }

        public HEOPHIEU SelectedHeo { get => _SelectedHeo; set { _SelectedHeo = value; } }

        public PHIEUHEO PhieuHeo { get; set; }
        private string _MaPhieu;
        public string MaPhieu { get => _MaPhieu; set { _MaPhieu = value; OnPropertyChanged(); } }
        public string loaiPhieu = null;
        public DateTime? NgayLap { get; set; }
        public int _dongia { get; set; }

        private int? _TongTien = 0;
        public int? TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }


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
        private ObservableCollection<HEOPHIEU> _ListHeo;
        private HEOPHIEU _SelectedHeo;

        public int DonGia { get => _dongia; set { _dongia = value; OnPropertyChanged(); } }

        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }

        public NHANVIEN NhanVien { get; set; }
        public string TenNV { get; set; }
        public ICommand SelectedLoaiPhieu { get; set; }
        public ICommand ThemHeo { get; set; }
        public ICommand DonGiaChanged { get; set; }
        public ICommand ThayDoiMaHK { get; set; }
        public ICommand DonGiaCommand2 { get; set; }
        public ICommand DeleteCommand { get; set; }


        public ICommand HoanTatCommand { get; set; }
        public ICommand HuyBoCommand { get; set; }

        public PhieuBanNhapHeoVM()
        {
            NgayLap = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
            ListHeo=new ObservableCollection<HEOPHIEU>();
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
                else if(ListHeo.Count>0 && loaiPhieu== "Phiếu xuất heo")
                {
                    MessageBox.Show("Chỉ được thêm heo vào một lần mỗi phiếu");
                    return;
                }
                if (loaiPhieu == "Phiếu nhập heo")
                {
                    ThemHeoPhieu them = new ThemHeoPhieu
                    {
                        DataContext = new ThemHeoPhieuVM(this),
                    };
                    them.ShowDialog();
                    foreach(HEOPHIEU h in ListHeo)
                    {
                        TongTien += h.DonGia * h.heo.TrongLuong;
                    }
                }
                if (loaiPhieu == "Phiếu xuất heo")
                {
                    ChonHeoXuat chon = new ChonHeoXuat
                    {
                        DataContext = new ChonHeoXuatVM(this),
                     };
                    chon.ShowDialog();
                    foreach (HEOPHIEU h in ListHeo)
                    {
                        TongTien += h.DonGia * h.heo.TrongLuong;
                    }
                }
            });
            ThayDoiMaHK = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                var ListKH = DataProvider.Ins.DB.DOITACs.Where(x => x.MaDoiTac == p.Text).ToList();
                if (ListKH.Count > 0)
                {
                    KhachHang = ListKH.First();
                    MaKH = KhachHang.MaDoiTac;
                    Ten = KhachHang.TenDoiTac;
                    SDT = KhachHang.SDT;
                    Mail = KhachHang.Email;
                    DiaChi = KhachHang.DiaChi;
                }
                else
                {
                    Ten = "";
                    SDT = "";
                    Mail = "";
                    DiaChi = "";
                }
            });

            HoanTatCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                if(MaKH == null || Ten == null || SDT == null || Mail == null || DiaChi == null)
                {
                    MessageBox.Show("Vui lùng nhập đủ thông tin khách hàng");
                    return;
                } 
                if(ListHeo.Count < 0)
                {
                    MessageBox.Show("Phiếu phải có ít nhất một cá thể heo");
                    return;
                }    
                
                if (DataProvider.Ins.DB.DOITACs.Where(x => x.MaDoiTac == KhachHang.MaDoiTac).Count() != 1)
                {
                    KhachHang.MaDoiTac = MaKH;
                    KhachHang.LoaiDoiTac = "Khách hàng";
                    KhachHang.TenDoiTac = Ten;
                    KhachHang.SDT = SDT;
                    KhachHang.Email = Mail;
                    KhachHang.DiaChi = DiaChi;
                    DataProvider.Ins.DB.DOITACs.Add(KhachHang);
                }
                PhieuHeo.SoPhieu = MaPhieu;
                PhieuHeo.LoaiPhieu = loaiPhieu;
                PhieuHeo.MaNhanVien = NhanVien.MaNhanVien;
                PhieuHeo.MaDoiTac = KhachHang.MaDoiTac;
                PhieuHeo.NgayLap = NgayLap;
                PhieuHeo.TrangThai = "Chưa hoàn thành";

                if (loaiPhieu == "Phiếu nhập heo")
                {
                    foreach (HEOPHIEU x in ListHeo)
                    {
                        if (x.heo.CHUONGTRAI.SoLuongHeo < x.heo.CHUONGTRAI.SuaChuaToiDa)
                        {
                            x.heo.CHUONGTRAI.SoLuongHeo += 1;
                            DataProvider.Ins.DB.HEOs.Add(x.heo);
                            CT_PHIEUHEO CT = new CT_PHIEUHEO
                            {
                                MaHeo = x.heo.MaHeo,
                                SoPhieu = MaPhieu,
                                DonGia = x.DonGia,
                                TrongLuong = x.heo.TrongLuong
                            };
                            DataProvider.Ins.DB.CT_PHIEUHEO.Add(CT);
                        }
                        else
                        {
                            MessageBox.Show("Sức chứa của chuồng không đủ. Heo" + x.heo.MaHeo + " chưa được lưu");
                            ListHeo.Remove(x);
                        }
                       
                    }
                }
                if (loaiPhieu == "Phiếu xuất heo")
                {
                    foreach (HEOPHIEU x in ListHeo)
                    {
                        x.heo.TinhTrang = "Đã xuất";
                        x.heo.CHUONGTRAI.SoLuongHeo -= 1;
                        CT_PHIEUHEO CT = new CT_PHIEUHEO
                        {
                            MaHeo = x.heo.MaHeo,
                            SoPhieu = MaPhieu,
                            DonGia = x.DonGia,
                            TrongLuong = x.heo.TrongLuong
                        };
                        DataProvider.Ins.DB.CT_PHIEUHEO.Add(CT);
                    }
                }
                PhieuHeo.TongTien = TongTien;
                DataProvider.Ins.DB.PHIEUHEOs.Add(PhieuHeo);
                try
                {
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thêm thành công");
                }
                catch
                {
                    MessageBox.Show("Lỗi khi thêm vào csdl");

                }
                p.Close();
                p.DataContext = null;
            });

            DonGiaCommand2 = new RelayCommand<ListView>((p) => { return true; }, p =>
            {
                SelectedHeo.DonGia = DonGia;
                DonGia = 0;
                foreach (HEOPHIEU h in ListHeo)
                {
                    TongTien += h.DonGia * h.heo.TrongLuong;
                }

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
            DeleteCommand = new RelayCommand<ListView>((p) => { return true; }, p =>
            {
                ListHeo.Remove(SelectedHeo);
                if (ListHeo.Count > 0)
                {
                    foreach (HEOPHIEU h in ListHeo)
                    {
                        TongTien += h.DonGia * h.heo.TrongLuong;
                    }
                }
                else TongTien = 0;

            });
        }
        string CreatPhieuHeo(int lan)
        {
            ObservableCollection<PHIEUHEO> PhieuHeos = new ObservableCollection<PHIEUHEO>(DataProvider.Ins.DB.PHIEUHEOs);
            int soPhieu;
            soPhieu = PhieuHeos.Count + lan;
            string maPhieuHeo = "";
            if (soPhieu == 0)
            {
                if (loaiPhieu == "Phiếu nhập heo")
                {
                    maPhieuHeo = "NHEO000001";
                }
                if (loaiPhieu == "Phiếu xuất heo")
                {
                    maPhieuHeo = "XHEO000001";
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
                    maPhieuHeo = "NHEO" + strSTT;
                }
                if (loaiPhieu == "Phiếu xuất heo")
                {
                    maPhieuHeo = "XHEO" + strSTT;
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
