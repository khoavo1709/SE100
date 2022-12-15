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

namespace QuanLyTraiHeo.ViewModel
{
    public class DSHeoVM
    {
        wDSHeo wd;
        CHUONGTRAI chuong;

        #region Event command
        public ICommand LoadedWindowCommand { get; set; }
        #endregion
        public DSHeoVM(CHUONGTRAI _chuong)
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, p => { LoadListHeo(p); });
            chuong = _chuong;
        }

        void LoadListHeo(Window p)
        {
            wd = (p as wDSHeo);
            if (wd == null) return;

            wd.sp_ListHeo.Children.Clear();

            List<HEO> list = new List<HEO>();
            list = DataProvider.Ins.DB.CHUONGTRAIs.Where(x => x.MaChuong == chuong.MaChuong).SingleOrDefault().HEOs.ToList();

            foreach (var item in list)
            {
                Heo_UC heo = new Heo_UC();
                heo.TB_GioiTinh.Text = item.GioiTinh;
                heo.TB_MaHeo.Text = item.MaHeo;
                heo.TB_LoaiHeo.Text = item.MaLoaiHeo;
                heo.TB_TinhTrang.Text= item.TinhTrang;
                heo.TB_TrongLuong.Text = item.TrongLuong.ToString();
                heo.TB_NgaySinh.Text = item.NgaySinh.Value.Date.ToString("MM/dd/yyyy");
                heo.Click += Heo_Click;
                heo.Tag = item;
                wd.sp_ListHeo.Children.Add(heo);
            }

        }

        void Heo_Click(object sender, RoutedEventArgs e)
        {
            wd.sp_ListLich.Children.Clear();

            HEO heo = (sender as Heo_UC).Tag as HEO;

            if (heo == null)
            {
                return;
            }

            Dictionary<DateTime?, object> dic = new Dictionary<DateTime?, object>();

            #region Lấy danh dách lịch tiêm
            foreach (var item in DataProvider.Ins.DB.LICHTIEMHEOs.Where(x => x.MaHeo == heo.MaHeo))
            {
                //dic.Add(item.NgayTiem, item);

                Exception exc = new Exception();
                exc = null;

                try
                {
                    dic.Add(item.NgayTiem, item);
                }
                catch(Exception exc0)
                {
                    exc = exc0;
                }

                while (exc != null)
                {
                    try
                    {
                        dic.Add(item.NgayTiem, item);
                        
                        exc = null;
                    }
                    catch(Exception exc1)
                    {
                        exc = exc1;
                        item.NgayTiem = item.NgayTiem.Value.AddMinutes(1);
                    }
                }

            }
            #endregion

            #region Lấy danh sách lịch phối giống
            if (chuong.MaLoaiChuong == "LC03112022000004" && heo.MaLoaiHeo == "LH02112022000001")// Nếu là heo đực và nằm trong chuồng heo đực giống
            {
                foreach (var item in DataProvider.Ins.DB.LICHPHOIGIONGs.Where(x => x.MaHeoDuc == heo.MaHeo))
                {
                    dic.Add(item.NgayPhoiGiong, item);


                }
            }
            else if (chuong.MaLoaiChuong == "LC03112022000003" && heo.MaLoaiHeo == "LH02112022000002")// Nếu là heo cái và nằm trong chuồng heo đực nái
            {
                foreach (var item in DataProvider.Ins.DB.LICHPHOIGIONGs.Where(x => x.MaHeoCai == heo.MaHeo))
                {
                    dic.Add(item.NgayPhoiGiong, item);
                }
            }
            #endregion

            #region Sắp xếp lại lịch từ ngày từ ngày lớn nhất đến ngày cũ nhất

            var sortedList = dic.OrderByDescending(x => x.Key).ToList();

            #endregion

            foreach (var item in sortedList)
            {
                LichUC lich = new LichUC();

                try 
                {
                    LICHTIEMHEO lichTiem = item.Value as LICHTIEMHEO;
                    lich.tb_NguoiLap.Text = "";
                    lich.tb_TieuDeLich.Text = "Lịch tiêm";
                    lich.tb_ThoiGian.Text = lichTiem.NgayTiem?.ToString("dd/MM/yyy");
                    lich.tb_TinhTrang.Text = lichTiem.TrangThai;
                    lich.tb_ChiTietLich.Text = lichTiem.MaThuoc;
                    lich.Click += Lich_Click;
                    string chiTiet = "";
                    chiTiet += "Mã heo tiêm : " + lichTiem.MaHeo + "\n";
                    chiTiet += "Loại thuốc : " + lichTiem.MaThuoc + "\n";
                    chiTiet += "Liều lượng : " + lichTiem.LieuLuong + "\n";
                    chiTiet += "Trạng thái : " + lichTiem.TrangThai + "\n";
                    lich.Tag = chiTiet;

                    if (lichTiem.NgayTiem < DateTime.Today.Date)
                    {
                        lich.border_Lich.BorderBrush = System.Windows.Media.Brushes.AliceBlue;
                    }
                }
                catch
                {
                    LICHPHOIGIONG lichPhoi= item.Value as LICHPHOIGIONG;
                    lich.tb_NguoiLap.Text = "";
                    lich.tb_TieuDeLich.Text = "Lịch phối giống";
                    lich.tb_ThoiGian.Text = lichPhoi.NgayPhoiGiong?.ToString("dd/MM/yyy");
                    lich.tb_TinhTrang.Text = lichPhoi.Trangthai;
                    lich.tb_ChiTietLich.Text = "Heo đực " + lichPhoi.MaHeoDuc + " x Heo cái" + lichPhoi.MaHeoCai;
                    lich.Click += Lich_Click;
                    string chiTiet = "";
                    chiTiet += lich.tb_ChiTietLich.Text + "\n";
                    chiTiet += "Ngày đẻ dự kiến : " + lichPhoi.NgayDuKienDe + "\n";
                    chiTiet += "Ngày đẻ thức tế : " + lichPhoi.NgayDeThucTe + "\n";
                    chiTiet += "Ngày số con : " + lichPhoi.SoCon + "\n";
                    chiTiet += "Ngày đẻ con chết : " + lichPhoi.SoConChet + "\n";
                    chiTiet += "Ngày cai sữa : " + lichPhoi.NgayCaiSua + "\n";
                    chiTiet += "Số con chọn  : " + lichPhoi.SoConChon + "\n";
                    chiTiet += "Ngày phối giống lại dự kiến : " + lichPhoi.NgayPhoiGiongLaiDuKien + "\n";
                    chiTiet += "Trạng thái : " + lichPhoi.Trangthai + "\n";
                    lich.Tag = chiTiet;

                    if (lichPhoi.NgayPhoiGiong < DateTime.Today.Date)
                    {
                        lich.border_Lich.BorderBrush = System.Windows.Media.Brushes.AliceBlue;
                    }
                }

                

                wd.sp_ListLich.Children.Add(lich);

            }
            
        }

        LichUC lichBeforSelected;
        LichUC lichSelected;

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
            wd.rtb_ChiTiet.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(lichSelected.Tag as string)));
            wd.tb_thoiGian.Visibility = Visibility.Visible;
            wd.tb_nguoiTao.Visibility = Visibility.Visible;
            wd.btn_Tick.Visibility = Visibility.Visible;
            wd.btn_Delete.Visibility = Visibility.Visible;
        }
    }
}
