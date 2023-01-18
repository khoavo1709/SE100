using OfficeOpenXml.ConditionalFormatting;
using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.Windows.Quản_lý_nhân_viên;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace FarmManagementSoftware.ViewModel
{
    public class listThongBaoTrongNgayVM: BaseViewModel
    {
        private bool _IsActive;
        public bool IsActive { get=> _IsActive; set { _IsActive = value; OnPropertyChanged(); } }
        public ChiTietThongBaoVM vmCTThongBao;
        private DateTime _NgayThongBao;
        public DateTime NgayThongBao { get => _NgayThongBao; set { _NgayThongBao = value; OnPropertyChanged(); } }

        private ObservableCollection<THONGBAOCHITIET> _thongbaotrongngay;
        public ObservableCollection<THONGBAOCHITIET> thongbaotrongngay { get => _thongbaotrongngay; set { _thongbaotrongngay = value; OnPropertyChanged(); } }

        private THONGBAOCHITIET _selectedThongBao;
        public THONGBAOCHITIET selectedThongBao { get => _selectedThongBao; set { _selectedThongBao = value; OnPropertyChanged(); } }

        public ICommand selectThongBaotrongngayCommand { get; set; }
        public listThongBaoTrongNgayVM()
        {

        }
        public listThongBaoTrongNgayVM(DateTime ngayThongBao, ChiTietThongBaoVM vm)
        {
            IsActive = true;
            NgayThongBao = ngayThongBao;
            vmCTThongBao = vm;
            thongbaotrongngay = new ObservableCollection<THONGBAOCHITIET>();

            LoadDSThongBaoTrongNgay();

            selectThongBaotrongngayCommand = new RelayCommand<Object>((p) => { return true; }, p => {
                if (selectedThongBao != null)
                {
                    if (vmCTThongBao.cbTinhTrang.Content.ToString() == "Đã gửi")
                    {
                        vmCTThongBao.SelectedItem = selectedThongBao.tb;
                        selectedThongBao = null;
                        return;
                    }    
                    vmCTThongBao.SelectedItem = selectedThongBao.tb;
                    selectedThongBao.tb._TinhTrang = "Đã đọc";
                    OnPropertyChanged("selectedThongBao");
                    DataProvider.Ins.DB.SaveChanges();
                }
                selectedThongBao = null;
            });
        }   
        void LoadDSThongBaoTrongNgay()
        {
            thongbaotrongngay.Clear();
            var Thongbaotrongngay = new ObservableCollection<ThongBao>(DataProvider.Ins.DB.ThongBaos.Where(x => x.ThoiGian.Value.Day == NgayThongBao.Day && x.C_MaNguoiNhan==vmCTThongBao.maNhanVien).OrderByDescending(x=> x.ThoiGian)).ToList();

            foreach(var item in Thongbaotrongngay)
            {
                THONGBAOCHITIET tb = new THONGBAOCHITIET();
                if (vmCTThongBao.cbTinhTrang.Content.ToString() == "Đã gửi")
                {
                    tb.isTBGui = 1;
                    if (checkTB(item))
                        continue;
                }
                tb.tb = item;
                thongbaotrongngay.Add(tb);  
            }

            if (thongbaotrongngay.Count == 0)
            {
                IsActive = false;
            }
            else
            {
                IsActive = true;
            }
        }

        bool checkTB(ThongBao tb)
        {
            
            foreach(var item in thongbaotrongngay)
            {
                if(item.tb.ThoiGian.Value == tb.ThoiGian.Value)
                {
                    return false;
                }
            }
            return true;
        }

        public void TimKiem(string txtTieuDe, string tinhtrang)
        {
            thongbaotrongngay.Clear();
            var thongbaos = DataProvider.Ins.DB.ThongBaos.Where(x => x.ThoiGian.Value.Day == NgayThongBao.Day).OrderByDescending(x => x.ThoiGian).ToList();
            if (txtTieuDe != "")
            {
                thongbaos = thongbaos.Where(x => x.TieuDe.Contains(txtTieuDe)).ToList();
            }
            if (tinhtrang.ToString() == "Tất cả")
            {
                thongbaos = thongbaos.Where(x => x.C_MaNguoiNhan == vmCTThongBao.maNhanVien).ToList();
            }
            else if (tinhtrang.ToString() == "Đã gửi")
            {
                thongbaos = thongbaos.Where(x => x.C_MaNguoiGui == vmCTThongBao.maNhanVien).ToList();
            }
            else
            {
                thongbaos = thongbaos.Where(x => x.TinhTrang == "Chưa đọc" && x.C_MaNguoiNhan == vmCTThongBao.maNhanVien).ToList();
            }

            foreach (var thongbao in thongbaos)
            {
                THONGBAOCHITIET tb = new THONGBAOCHITIET();
                if(vmCTThongBao.cbTinhTrang.Content.ToString() == "Đã gửi")
                {
                    tb.isTBGui = 1;
                    if (!checkTB(thongbao))
                        continue;
                }
                tb.tb = thongbao;
                thongbaotrongngay.Add(tb);
            }

            if(thongbaotrongngay.Count == 0)
            {
                IsActive = false;
            }
            else
            {
                IsActive = true;
            }

        }
        public class THONGBAOCHITIET: BaseViewModel
        {
            public int isTBGui { get; set; }
            private ThongBao _tb;
            public ThongBao tb { get=>_tb; set { _tb = value; OnPropertyChanged(); } }
            public THONGBAOCHITIET()
            {
                isTBGui = 0;
                tb = null;
            }
        }
    }
}
