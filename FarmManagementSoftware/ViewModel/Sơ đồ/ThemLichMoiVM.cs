using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FarmManagementSoftware.ViewModel
{
    public class ThemLichMoiVM : BaseViewModel
    {
        public string MaChuong { get; set; }
        public DSLichChuongVM DSLichChuongVM { get; set; }

        #region Event command
        public ICommand CancelCommand { get; set; }

        public ICommand AddCommand { get; set; }
        #endregion

        public ThemLichMoiVM()
        {
            AddCommand = new RelayCommand<wThemLichMoi>((p) => { return true; }, p => { ThemLichMoi(p); });

            CancelCommand = new RelayCommand<wThemLichMoi>((p) => { return true; }, p => { p.Close(); });
        }

        void ThemLichMoi(wThemLichMoi wd)
        {
            if(string.IsNullOrWhiteSpace(wd.tb_TieuDe.Text) || wd.dtp_ChonNgay.SelectedDate < DateTime.Today || wd.dtp_ChonNgay.SelectedDate == null)
            {
                MessageBox.Show("Hãy nhập đủ thông tin và ngày thực hiện phải là từ hôm nay trở đi");
                return;
            }

            DateTime ngayLam = wd.dtp_ChonNgay.SelectedDate.Value;
            LICHCHUONG lc = DataProvider.Ins.DB.LICHCHUONGs.Where(x => x.MaChuong == MaChuong && x.NgayLam.Day == ngayLam.Day && x.TrangThai == "Chưa làm").SingleOrDefault();
            if (lc != null)
            {
                MessageBox.Show(String.Format("Ngày {0} đã có lịch {1} chưa làm nên không thể tạo thêm lịch mới", lc.NgayLam, lc.TenLich));
                return;
            }

            try
            {
                LICHCHUONG lichMoi = new LICHCHUONG();
                lichMoi.MaNguoiTao = Account.TaiKhoan.MaNhanVien;
                lichMoi.TenLich = wd.tb_TieuDe.Text;
                lichMoi.NgayLam = wd.dtp_ChonNgay.SelectedDate.Value;
                lichMoi.TrangThai = "Chưa làm";

                List<LICHCHUONG> lstLich = DataProvider.Ins.DB.LICHCHUONGs.Where(x => x.MaChuong == MaChuong).ToList();
                foreach (var item in lstLich)
                {
                    if (lichMoi.NgayLam == item.NgayLam)
                    {
                        lichMoi.NgayLam = DateTime.Now.AddSeconds(1);
                    }
                }

                lichMoi.Mota = new System.Windows.Documents.TextRange(wd.rtb_ChiTiet.Document.ContentStart, wd.rtb_ChiTiet.Document.ContentEnd).Text;
                lichMoi.MaChuong = MaChuong;
                DataProvider.Ins.DB.LICHCHUONGs.Add(lichMoi);
                DataProvider.Ins.DB.SaveChanges();

                DSLichChuongVM.Load();
                wd.Close();
            }
            catch
            {
                MessageBox.Show("Xin hãy thử lại sau 1 phút!");
            }
        }
    }
}
