using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.Windows.Thiết_lập_cây_mục_tiêu;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FarmManagementSoftware.ViewModel
{
    public class SuaTTHeoVM : BaseViewModel
    {
        public ObservableCollection<LOAIHEO> ListLoai { get; set; }
        public ObservableCollection<GIONGHEO> ListGiong { get; set; }
        public ObservableCollection<CHUONGTRAI> ListChuong { get; set; }

        public string MaHeoMe { get; set; }
        public string MaHeoCha {get;set;}

        public HEO SelectedHeo { get; set; }
        public LOAIHEO SelectedLoai { get; set; }
        public GIONGHEO SelectedGiong { get; set; }
        public CHUONGTRAI SelectedChuong { get; set; }
        public CHUONGTRAI chuongBanDau { get; set; }

        public string _MaHeo;
        public string _MaLoaiHeo { get; set; }  
        public string _MaGiongHeo { get; set; }
        public string _GioiTinh { get; set; }
        public int? _TrongLuong { get; set; }
        public DateTime? _NgaySinh { get; set; }
        public string _MaChuong { get; set; }
        public string _NguonGoc { get; set; }
        public string _TinhTrang { get; set; }

        public ICommand XacNhanCommand { get; set; }

        public SuaTTHeoVM()
        {
            return;
        }
        public SuaTTHeoVM(QuanLyThongTinCaTheVM vm)
        {
            SelectedHeo = vm.SelectedHeo;
            _MaHeo = SelectedHeo.MaHeo;
            _GioiTinh = SelectedHeo.GioiTinh;
            _TrongLuong = SelectedHeo.TrongLuong;
            _NgaySinh = SelectedHeo.NgaySinh;
            _NguonGoc = SelectedHeo.NguonGoc;
            _TinhTrang = SelectedHeo.TinhTrang;
            CHUONGTRAI cu = SelectedHeo.CHUONGTRAI;
            SelectedLoai = SelectedHeo.LOAIHEO;
            SelectedChuong = SelectedHeo.CHUONGTRAI;
            chuongBanDau = SelectedHeo.CHUONGTRAI;
            SelectedGiong = SelectedHeo.GIONGHEO;
            if(SelectedHeo.MaHeoCha!=null && SelectedHeo.MaHeoMe!=null)
            {
                MaHeoMe=SelectedHeo.MaHeoMe;
                MaHeoCha=SelectedHeo.MaHeoCha;
            }    
            ListLoai = new ObservableCollection<LOAIHEO>(DataProvider.Ins.DB.LOAIHEOs);
            ListGiong = new ObservableCollection<GIONGHEO>(DataProvider.Ins.DB.GIONGHEOs);
            ListChuong = new ObservableCollection<CHUONGTRAI>(DataProvider.Ins.DB.CHUONGTRAIs);


            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                if (SelectedChuong == null || SelectedGiong == null || SelectedLoai == null)
                    return false;
                if (_GioiTinh == null || _TinhTrang == null || _NguonGoc == null || _NgaySinh == null || _TrongLuong == null)
                    return false;
                return true;
            }, p =>
            {

                if (!KiemTra())
                {
                    return;
                }

                if (SelectedChuong.SoLuongHeo < SelectedChuong.SuaChuaToiDa && SelectedHeo.MaChuong != SelectedChuong.MaChuong)
                {
                    SelectedChuong.SoLuongHeo += 1;
                    cu.SoLuongHeo -= 1;
                }
                else
                {
                    MessageBox.Show("Sức chứa của chuồng không đủ. Heo" + SelectedHeo.MaHeo + " chưa được sửa");
                    return;
                }
                SelectedHeo.GioiTinh = _GioiTinh;
                SelectedHeo.TinhTrang = _TinhTrang;
                SelectedHeo.NguonGoc = _NguonGoc;
                SelectedHeo.NgaySinh = _NgaySinh;
                SelectedHeo.TrongLuong = _TrongLuong;

                SelectedHeo.GIONGHEO = SelectedGiong;
                SelectedHeo.LOAIHEO = SelectedLoai;
                SelectedHeo.CHUONGTRAI = SelectedChuong;
                DataProvider.Ins.DB.SaveChanges();
                vm.SelectedHeo = SelectedHeo; 
                p.Close();
            });
        }
        bool KiemTra()
        {
            string msg;
            if (_GioiTinh == "Cái" && SelectedLoai.TenLoaiHeo.Contains("đực"))
            {
                msg = "Chọn sai giới tính hoặc loại heo";
                MessageBox.Show(msg);
                return false;
            }
            if (_GioiTinh == "Đực" && SelectedLoai.TenLoaiHeo.Contains("nái"))
            {
                msg = "Chọn sai giới tính hoặc loại heo";
                MessageBox.Show(msg);
                return false;
            }

            if (MaHeoMe != null && MaHeoCha != null)
            {
                if (!(SelectedLoai.TenLoaiHeo.Contains("con")) && (MaHeoMe != "Không chọn" || MaHeoCha != "Không chọn"))
                {
                    msg = "Chỉ chọn heo cha, heo mẹ cho heo thuộc loại heo con";
                    MessageBox.Show(msg);
                    return false;
                }
            }

            if (SelectedLoai.TenLoaiHeo.Contains("nái"))
                if (!SelectedChuong.MaChuong.Contains("HN") && !SelectedChuong.MaChuong.Contains("HD"))
                {
                    msg = "Chuồng hiện tại không phù hợp với loại heo nái";
                    MessageBox.Show(msg);
                    return false;
                }
            if (SelectedLoai.TenLoaiHeo.Contains("con") && SelectedChuong.MaChuong.Contains("DG"))
            {
                msg = "Heo con không thể ở chuồng đực giống";
                MessageBox.Show(msg);
                return false;
            }
            if (SelectedLoai.TenLoaiHeo.Contains("đực") && (SelectedChuong.MaChuong.Contains("N") && SelectedChuong.MaChuong.Contains("HD")))
            {
                msg = "Heo đực không thể ở chuồng heo nái khác";
                MessageBox.Show(msg);
                return false;
            }
            if (SelectedLoai.TenLoaiHeo.Contains("thịt") && !SelectedChuong.MaChuong.Contains("T"))
            {
                msg = "Heo thịt chỉ có thể ở chuồng heo thịt";
                MessageBox.Show(msg);
                return false;
            }
            return true;
        }

    }
}