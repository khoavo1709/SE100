using FarmManagementSoftware.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FarmManagementSoftware.ViewModel
{
    public class listThongBaoTrongNgayVM : BaseViewModel
    {
        private bool _IsActive;
        public bool IsActive { get => _IsActive; set { _IsActive = value; OnPropertyChanged(); } }
        public ChiTietThongBaoVM vmCTThongBao;
        private DateTime _NgayThongBao;
        public DateTime NgayThongBao { get => _NgayThongBao; set { _NgayThongBao = value; OnPropertyChanged(); } }

        private ObservableCollection<ThongBao> _thongbaotrongngay;
        public ObservableCollection<ThongBao> thongbaotrongngay { get => _thongbaotrongngay; set { _thongbaotrongngay = value; OnPropertyChanged(); } }

        private ThongBao _selectedThongBao;
        public ThongBao selectedThongBao { get => _selectedThongBao; set { _selectedThongBao = value; OnPropertyChanged(); } }

        public ICommand selectThongBaotrongngayCommand { get; set; }
        public listThongBaoTrongNgayVM()
        {

        }
        public listThongBaoTrongNgayVM(DateTime ngayThongBao, ChiTietThongBaoVM vm)
        {
            IsActive = true;
            NgayThongBao = ngayThongBao;
            vmCTThongBao = vm;

            LoadDSThongBaoTrongNgay();

            selectThongBaotrongngayCommand = new RelayCommand<Object>((p) => { return true; }, p => {
                if (selectedThongBao != null)
                {
                    vmCTThongBao.SelectedItem = selectedThongBao;
                    selectedThongBao.TinhTrang = "Đã đọc";
                    DataProvider.Ins.DB.SaveChanges();
                }
                selectedThongBao = null;
            });
        }
        void LoadDSThongBaoTrongNgay()
        {
            thongbaotrongngay = new ObservableCollection<ThongBao>(DataProvider.Ins.DB.ThongBaos.Where(x => x.ThoiGian.Value.Day == NgayThongBao.Day && x.C_MaNguoiNhan == vmCTThongBao.maNhanVien).OrderByDescending(x => x.ThoiGian));
            if (thongbaotrongngay.Count == 0)
            {
                IsActive = false;
            }
            else
            {
                IsActive = true;
            }
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
                thongbaotrongngay.Add(thongbao);
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
    }
}
