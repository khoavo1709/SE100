using QuanLyTraiHeo.Model;
using QuanLyTraiHeo.View.UC;
using QuanLyTraiHeo.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyTraiHeo.ViewModel
{
    public class DSLichChuongVM: BaseViewModel
    {

        #region Attribute
        wDSLichChuong wd;

        LichUC lichBeforSelected;
        LichUC lichSelected;
        #endregion

        #region Propaty
        public string MaChuong { get; set; }
        public string LoaiChuong { get; set; }
        public int? SucChuaToiDa { get; set; }
        public int SoLuongHeo { get; set; }
        #endregion

        #region Event command
        public ICommand LoadedWindowCommand { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand TickCommand { get; set; }
        #endregion

        public DSLichChuongVM()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, p => { wd = p as wDSLichChuong; ; Load(); });
            AddCommand = new RelayCommand<Window>((p) => { return true; }, p => { ThemLichMoi(); });
            TickCommand = new RelayCommand<Window>((p) => { return true; }, p => { TickIsDone(); });
            DeleteCommand = new RelayCommand<Window>((p) => { return true; }, p => { Delete(); });
        }

        public void Load()
        {
            wd.tb_nguoiTao.Visibility = Visibility.Hidden;
            wd.tb_thoiGian.Visibility = Visibility.Hidden;

            wd.sp_ListLich.Children.Clear();

            foreach (var item in DataProvider.Ins.DB.LICHCHUONGs.Where(x => x.MaChuong == MaChuong).OrderByDescending(x => x.NgayLam))
            {
                LichUC lich = new LichUC();
                lich.tb_NguoiLap.Text = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNhanVien == item.MaNguoiTao).SingleOrDefault().HoTen;
                lich.tb_TieuDeLich.Text = item.TenLich;
                lich.tb_ThoiGian.Text = item.NgayLam.Date.ToString("dd/MM/yyy");
                lich.tb_TinhTrang.Text = item.TrangThai;
                lich.tb_ChiTietLich.Text = item.Mota;
                lich.Click += Lich_Click;
                lich.Tag = item;

                if (item.NgayLam < DateTime.Today.Date)
                {
                    lich.border_Lich.BorderBrush = Brushes.AliceBlue;
                }

                wd.sp_ListLich.Children.Add(lich);
            }
        }

        void ThemLichMoi()
        {
            wThemLichMoi themLichMoi = new wThemLichMoi();
            themLichMoi.tb_NguoiTao.Text = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNhanVien == Account.TaiKhoan.MaNhanVien).SingleOrDefault().HoTen;
            themLichMoi.DataContext = new ThemLichMoiVM() { MaChuong = wd.tb_MaChuong.Text , DSLichChuongVM = this};
            themLichMoi.ShowDialog();
        }

        private void Lich_Click(object sender, RoutedEventArgs e)
        {
            lichBeforSelected = lichSelected;

            lichSelected = sender as LichUC;

            lichSelected.LichBeforSelected = lichBeforSelected;

            if (lichSelected == lichSelected.Tag as LichUC)
            {
                return;
            }

            wd.tb_Lich.Text = lichSelected.tb_TieuDeLich.Text;
            wd.tb_NguoiTao.Text = lichSelected.tb_NguoiLap.Text;
            wd.tb_ThoiGian.Text = lichSelected.tb_ThoiGian.Text;
            wd.rtb_ChiTiet.Document.Blocks.Clear();
            wd.rtb_ChiTiet.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(lichSelected.tb_ChiTietLich.Text)));
            wd.tb_thoiGian.Visibility = Visibility.Visible;
            wd.tb_nguoiTao.Visibility = Visibility.Visible;
            wd.btn_Tick.Visibility = Visibility.Visible;
            wd.btn_Delete.Visibility = Visibility.Visible;
        }

        void Delete()
        {
            if (lichSelected == null) return;

            string tieuDe = lichSelected.tb_TieuDeLich.Text;
            string maNguoiTao = (lichSelected.Tag as LICHCHUONG).MaNguoiTao;
            DateTime ngayLam = (lichSelected.Tag as LICHCHUONG).NgayLam;

            LICHCHUONG lich = DataProvider.Ins.DB.LICHCHUONGs.Where(x => x.NgayLam == ngayLam  && x.TenLich == tieuDe && x.MaNguoiTao == maNguoiTao).SingleOrDefault();

            DataProvider.Ins.DB.LICHCHUONGs.Remove(lich);

            DataProvider.Ins.DB.SaveChanges();

            lichSelected = null;
            wd.tb_Lich.Text = "";
            wd.tb_NguoiTao.Text = "";
            wd.tb_ThoiGian.Text = "";
            wd.rtb_ChiTiet.Document.Blocks.Clear();

            wd.btn_Delete.Visibility = Visibility.Hidden;
            wd.btn_Tick.Visibility = Visibility.Hidden;

            Load();
        }

        void TickIsDone()
        {
            if (lichSelected == null) return;

            string tieuDe = lichSelected.tb_TieuDeLich.Text;
            string maNguoiTao = (lichSelected.Tag as LICHCHUONG).MaNguoiTao;
            DateTime ngayLam = (lichSelected.Tag as LICHCHUONG).NgayLam;

            LICHCHUONG lich = DataProvider.Ins.DB.LICHCHUONGs.Where(x => x.NgayLam == ngayLam && x.TenLich == tieuDe && x.MaNguoiTao == maNguoiTao).SingleOrDefault();

            lich.TrangThai = "Đã làm";
            lich.MaNguoiLam = Account.TaiKhoan.MaNhanVien;

            //DataProvider.Ins.DB.LICHCHUONGs.(lich);

            DataProvider.Ins.DB.SaveChanges();

            lichSelected = null;
            wd.tb_Lich.Text = "";
            wd.tb_NguoiTao.Text = "";
            wd.tb_ThoiGian.Text = "";
            wd.rtb_ChiTiet.Document.Blocks.Clear();

            wd.btn_Delete.Visibility = Visibility.Hidden;
            wd.btn_Tick.Visibility = Visibility.Hidden;

            Load();
        }
    }
}
