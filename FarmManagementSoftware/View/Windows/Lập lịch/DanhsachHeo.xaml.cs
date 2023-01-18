using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.Windows.Quản_lý_chức_vụ;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using FarmManagementSoftware.View.Windows;
using System.Data;
using System.Drawing;
using ServiceStack.Text;
using FarmManagementSoftware.ViewModel;

namespace FarmManagementSoftware.View.Windows.Lập_lịch
{
    /// <summary>
    /// Interaction logic for DanhsachHeo.xaml
    /// </summary>
    public partial class DanhsachHeo : Window
    {
        #region Attributes
        
        private List<ChonHeo> _listChonHeo = new List<ChonHeo>();
        public List<HEO> _listHEO = new List<HEO>();
        #endregion
        List<HANGHOA> _listVacxin = new List<HANGHOA>();
        public List<LichTiem_TenThuoc> Thuoc_Tiem = new List<LichTiem_TenThuoc>();
        List<LICHTIEMHEO> ListLichTiem = new List<LICHTIEMHEO>();
        List<string> ListMaChuong = new List<string>() { "All"};
        List<string> ListLichSuTiem = new List<string>() { };
        List<string> ListThuoc = new List<string>();
        List<HEO> Listheo { get; set;}
        List<LICHTIEMHEO> listTiemHeo { get; set;}
        List<ChonHeo> ListChonHeo { get; set; }
        public HEO heo { get; set; }

        public int check = 0;
        
        public DanhsachHeo()
        {
            InitializeComponent();
            //pre-check
            Listheo = DataProvider.Ins.DB.HEOs.ToList();
            //load
            GetListTenThuoc();
            loadMaChuong();
            loadDatagrid("All");
            TiemVacxin_CB.ItemsSource = ListThuoc;
        }

        /*        private void check_click(object sender, RoutedEventArgs e)
                {
                    this.Close();
                }*/

        /*void loadDSHeo()
        {
            _listChonHeo = new ObservableCollection<ChonHeo>();

            //listNGUOIGUI = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs.Where(x => x.CHUCVU.MaChucVu == maChucVu));

            //var nhanviens = DataProvider.Ins.DB.NHANVIENs;
            var Nhanviens = DataProvider.Ins.DB.NHANVIENs.ToList();
            if (maChucVu != null)
                Nhanviens = Nhanviens.Where(x => x.MaChucVu == maChucVu).ToList();

            foreach (var Nhanvien in Nhanviens)
            {
                NguoiGui nguoigui = new NguoiGui();
                nguoigui.nhanvien = Nhanvien;
                nguoigui.IsChecked = false;
                listNHANVIEN.Add(nguoigui);
            }
        }*/
        //Lay danh sach heo da chon
        void loadDSChonHeo()
        {
            foreach (var heo in _listChonHeo)
            {
                if (heo.IsChecked == true)
                {
                    _listHEO.Add(heo.heo);
                }
            }
        }
        
        //Lay ma chuong
        void loadMaChuong()
        {
            foreach(CHUONGTRAI CT in DataProvider.Ins.DB.CHUONGTRAIs)
            {
                ListMaChuong.Add(CT.MaChuong);
            }
            MaChuong_CB.ItemsSource = ListMaChuong;
        }

        //load datagrid theo mau
        void loadDatagrid(string MaChuong)
        {
            ReloadDatagrid();
            foreach (var Heo in Listheo)
            {
                if (MaChuong == "All")
                {
                    ChonHeo chonheo = new ChonHeo();
                    chonheo.heo = Heo;
                    chonheo.IsChecked = false;
                    chonheo.Tuoi = (int)(DateTime.Now - (DateTime)Heo.NgaySinh).TotalDays;
                    TuoiHeo(chonheo);
                    _listChonHeo.Add(chonheo);
                }
                else
                {
                    if (Heo.MaChuong == MaChuong)
                    {
                        ChonHeo chonheo = new ChonHeo();
                        chonheo.heo = Heo;
                        chonheo.IsChecked = false;
                        chonheo.Tuoi = (int)(DateTime.Now - (DateTime)Heo.NgaySinh).TotalDays;
                        TuoiHeo(chonheo);
                        _listChonHeo.Add(chonheo);
                    }
                }
            }
            ListMaHeo_.ItemsSource = _listChonHeo;
            ListMaHeo_.Items.Refresh();
        }

        //Ham Tinh Tuoi Heo
        void TuoiHeo(ChonHeo chonheo)
        {
            if (chonheo.Tuoi < 30)
            {
                chonheo.ShowTuoi = chonheo.Tuoi.ToString() + " ngày";
            }
            if (chonheo.Tuoi >= 30)
            {
                chonheo.ShowTuoi = (chonheo.Tuoi / 30).ToString() + " tháng " + (chonheo.Tuoi % 30).ToString() + " ngày";
            }
            if (chonheo.Tuoi >= 365)
            {
                chonheo.ShowTuoi = ((chonheo.Tuoi / 30) / 12).ToString() + " năm " + (chonheo.Tuoi % 365).ToString() + " ngày";
            }
        }

        //Reload lai datagrid
        void ReloadDatagrid()
        {
            _listChonHeo.Clear();
        }
        //lay danh sach ten thuoc instant
        void GetListTenThuoc()
        {
            Thuoc_Tiem.Clear();
            ListLichTiem = DataProvider.Ins.DB.LICHTIEMHEOs.ToList();
            foreach (var lichtiem in ListLichTiem)
            {
                LichTiem_TenThuoc lichTiem_TenThuoc = new LichTiem_TenThuoc
                {
                    lichtiem = lichtiem,
                    hanghoa = DataProvider.Ins.DB.HANGHOAs.FirstOrDefault(s => s.MaHangHoa.Contains(lichtiem.MaThuoc))
                };
                Thuoc_Tiem.Add(lichTiem_TenThuoc);
            }
            _listVacxin = DataProvider.Ins.DB.HANGHOAs.Where(s => s.LoaiHangHoa == "Thuốc" || s.LoaiHangHoa == "Vacxin").ToList();
            foreach (HANGHOA h in _listVacxin)
            {
                ListThuoc.Add(h.TenHangHoa);
            }
            TiemVacxin_CB.ItemsSource = ListThuoc;
        }
        //Event khi click row in datagrid
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListMaHeo_.SelectedItems.Clear();
            var item = sender as DataGridRow;
            if (item != null)
            {
                item.IsSelected = true;
                ListMaHeo_.SelectedItem = item;
            }
        }
        //Event click row part 2
        private void ListViewItem_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataGridRow item = sender as DataGridRow;
            if (item != null && item.IsSelected)
            {
                GetListTenThuoc();
                LichSuTiem_TB.Clear();
                ListLichSuTiem.Clear();
                ChonHeo CH = ListMaHeo_.SelectedItem as ChonHeo;
                foreach(LICHTIEMHEO TH in CH.heo.LICHTIEMHEOs)
                {
                    //MessageBox.Show("pass");
                    if (TH.TrangThai == "Đã tiêm")
                    {
                        HANGHOA hanghoa = Thuoc_Tiem.First(s => s.lichtiem.MaLichTiem.Equals(TH.MaLichTiem) && s.lichtiem.TrangThai.Equals("Đã tiêm")).hanghoa;
                        ListLichSuTiem.Add(hanghoa.TenHangHoa + " vào ngày " + Thuoc_Tiem.First(s => s.lichtiem.MaLichTiem.Equals(TH.MaLichTiem)).lichtiem.NgayTiem);
                    }
                }   
                if(ListLichSuTiem.IsNullOrEmpty())
                {
                    LichSuTiem_TB.Text = "Heo chưa ghi nhận được tiêm.";
                }
                else
                {
                    foreach (var a in ListLichSuTiem)
                    {

                        LichSuTiem_TB.Text += "Đã tiêm " + a + "\n";
                    }
                }    
            }
        }

        /*//lay danh sach vacxin
        void GetVacxin()
        {
            foreach (var test in Thuoc_Tiem)
            {
                ListThuoc.Add(test.)
                 Thuoc_Tiem.Where(s => s.hanghoa.TenHangHoa.)
            }
            TiemVacxin_CB.ItemsSource =
        }*/
        public string TranferCode()
        {
            ChonHeo chonHeo = (ChonHeo)ListMaHeo_.SelectedItem;
            heo = chonHeo.heo;
            return heo.MaHeo;
        }

        private void Confirm_button_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
            loadDSChonHeo();
            this.Close();
        }

        private void Huy_button_Click(object sender, RoutedEventArgs e)
        {
            check = 0;
            this.Close();
        }

        private void MaChuong_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ReloadDatagrid();
            //loadDatagrid(MaChuong_CB.SelectedItem as string);
            Timkiem();
        }

        private void TrangThai_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ReloadDatagrid();
            ////loadDatagrid(MaChuong_CB.SelectedItem as string);
            ////List<HEO> Listheos = new List<HEO>();
            ////foreach(var heo in _listChonHeo)
            ////{
            ////    Listheos.Add(heo.heo);
            ////}
            ////_listChonHeo.Clear();
            //string trangthai = TrangThai_CB.SelectedValue.ToString();


            //foreach (var Heo in Listheo)
            //{
            //    HANGHOA Vacxin;
            //    if (TiemVacxin_CB.SelectedValue != null)
            //        Vacxin = DataProvider.Ins.DB.HANGHOAs.Where(x => x.TenHangHoa == TiemVacxin_CB.SelectedValue.ToString()).SingleOrDefault();
            //    else Vacxin = null;

            //    if (TrangThai_CB.SelectedValue.ToString() == "Đã tiêm" /*&& (Heo.LICHTIEMHEOs.Where(s => s.MaThuoc.Equals(TiemVacxin_CB.SelectedItem.ToString()).CompareTo(null)*/)
            //    {
            //        if(Vacxin != null)
            //            ListLichTiem = Heo.LICHTIEMHEOs.Where(s => s.MaThuoc.Equals(Vacxin.MaHangHoa)) as List<LICHTIEMHEO>;
            //        else ListLichTiem = Heo.LICHTIEMHEOs as List<LICHTIEMHEO>;
            //        if (ListLichTiem != null)
            //        {
            //            ChonHeo chonheo = new ChonHeo();
            //            chonheo.heo = Heo;
            //            chonheo.IsChecked = false;
            //            chonheo.Tuoi = (int)(DateTime.Now - (DateTime)Heo.NgaySinh).TotalDays;
            //            TuoiHeo(chonheo);
            //            _listChonHeo.Add(chonheo);
            //        }
            //    }
            //    else
            //    {
            //        ListLichTiem = Heo.LICHTIEMHEOs.Where(s => s.MaThuoc.Equals(Vacxin.MaHangHoa)) as List<LICHTIEMHEO>;
            //        if (ListLichTiem == null)
            //        {
            //            ChonHeo chonheo = new ChonHeo();
            //            chonheo.heo = Heo;
            //            chonheo.IsChecked = false;
            //            chonheo.Tuoi = (int)(DateTime.Now - (DateTime)Heo.NgaySinh).TotalDays;
            //            TuoiHeo(chonheo);
            //            _listChonHeo.Add(chonheo);
            //        }
            //    }
            //}
            //ListMaHeo_.ItemsSource = _listChonHeo;
            //ListMaHeo_.Items.Refresh();
            Timkiem();
        }

        private void TiemVacxin_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ReloadDatagrid();

            //foreach (var Heo in Listheo)
            //{

            //    if (TrangThai_CB.SelectedItem.ToString() == "Đã tiêm" /*&& (Heo.LICHTIEMHEOs.Where(s => s.MaThuoc.Equals(TiemVacxin_CB.SelectedItem.ToString()).CompareTo(null)*/)
            //    {
            //        ListLichTiem = Heo.LICHTIEMHEOs.Where(s => s.MaThuoc.Equals(TiemVacxin_CB.SelectedItem.ToString())) as List<LICHTIEMHEO>;
            //        if (ListLichTiem != null)
            //        {
            //            ChonHeo chonheo = new ChonHeo();
            //            chonheo.heo = Heo;
            //            chonheo.IsChecked = false;
            //            chonheo.Tuoi = (int)(DateTime.Now - (DateTime)Heo.NgaySinh).TotalDays;
            //            TuoiHeo(chonheo);
            //            _listChonHeo.Add(chonheo);
            //        }
            //    }
            //    else
            //    {
            //        ListLichTiem = Heo.LICHTIEMHEOs.Where(s => s.MaThuoc.Equals(TiemVacxin_CB.SelectedItem.ToString())) as List<LICHTIEMHEO>;
            //        if (ListLichTiem == null)
            //        {
            //            ChonHeo chonheo = new ChonHeo();
            //            chonheo.heo = Heo;
            //            chonheo.IsChecked = false;
            //            chonheo.Tuoi = (int)(DateTime.Now - (DateTime)Heo.NgaySinh).TotalDays;
            //            TuoiHeo(chonheo);
            //            _listChonHeo.Add(chonheo);
            //        }
            //    }
            //}
            //ListMaHeo_.ItemsSource = _listChonHeo;
            //ListMaHeo_.Items.Refresh();
            Timkiem();
        }

        bool checktuoimin(DateTime date)
        {
            int tuoimin = int.Parse(tb_TuoiMin.Text);
            return (DateTime.Now - date).TotalDays >= tuoimin;
        }
        bool checktuoimax(DateTime date)
        {
            int tuoimax = int.Parse(tb_TuoiMax.Text);
            return (DateTime.Now - date).TotalDays <= tuoimax;
        }

        void Timkiem()
        {
            ReloadDatagrid();
            var newlist = DataProvider.Ins.DB.HEOs.ToList();
            if (MaChuong_CB.SelectedItem != null && MaChuong_CB.SelectedItem.ToString() != "All")
                newlist = newlist.Where(x => x.MaChuong == MaChuong_CB.SelectedItem.ToString()).ToList();
            if(tb_TuoiMin.Text!= null && tb_TuoiMin.Text != "")
            {
                newlist = newlist.Where(x => checktuoimin(x.NgaySinh.Value)).ToList();
            }

            if(tb_TuoiMax.Text!= null && tb_TuoiMax.Text != "")
            {
                newlist = newlist.Where(x => checktuoimax(x.NgaySinh.Value)).ToList();
            }

            if (TrangThai_CB.SelectedValue != null)
            {
                if (TrangThai_CB.SelectedValue.ToString() == "Đã tiêm")
                {
                    newlist = newlist.Where(x => x.LICHTIEMHEOs.Where(s => s.TrangThai == TrangThai_CB.SelectedValue.ToString()).Count() > 0).ToList();
                    string MaVacxin = "";
                    if (TiemVacxin_CB.SelectedItem != null)
                    {
                        MaVacxin = DataProvider.Ins.DB.HANGHOAs.Where(x => x.TenHangHoa == TiemVacxin_CB.SelectedItem.ToString()).SingleOrDefault().MaHangHoa;
                        newlist = newlist.Where(x => x.LICHTIEMHEOs.Where(s => s.MaThuoc == MaVacxin).Count() > 0).ToList();
                    }
                }
                else
                {
                    string MaVacxin = "";
                    if (TiemVacxin_CB.SelectedItem != null)
                    {
                        MaVacxin = DataProvider.Ins.DB.HANGHOAs.Where(x => x.TenHangHoa == TiemVacxin_CB.SelectedItem.ToString()).SingleOrDefault().MaHangHoa;
                        newlist = newlist.Where(x => x.LICHTIEMHEOs.Where(s => s.MaThuoc == MaVacxin).Count() == 0).ToList();
                    }
                }
            }

            

            foreach (var Heo in newlist)
            {
                
                ChonHeo chonheo = new ChonHeo();
                chonheo.heo = Heo;
                chonheo.IsChecked = false;
                chonheo.Tuoi = (int)(DateTime.Now - (DateTime)Heo.NgaySinh).TotalDays;
                TuoiHeo(chonheo);
                _listChonHeo.Add(chonheo);
            }
            ListMaHeo_.ItemsSource = _listChonHeo;
            ListMaHeo_.Items.Refresh();
        }

        private void TbTuoiMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            Timkiem();
        }

        private void TuoiMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            Timkiem();
        }
    }
}

