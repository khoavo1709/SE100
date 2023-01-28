using Microsoft.Win32;
using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.Windows.Lập_lịch;
using FarmManagementSoftware.View.Windows.Quản_lý_loại_heo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Office.Interop.Excel;
//using System.Windows.Forms;
using WPFWindow = System.Windows;
using System.Collections.ObjectModel;
using MaterialDesignThemes.Wpf;

namespace FarmManagementSoftware
{
    /// <summary>
    /// Interaction logic for LapLichTiem.xaml
    /// </summary>
    public partial class LapLichTiem : WPFWindow.Window
    {
        public List<HEO> HeodaChon { get; set; }
        public List<LichTiem_TenThuoc> Thuoc_Tiem = new List<LichTiem_TenThuoc>();
        List<HANGHOA> DanhsachThuoc { get; set; }
        List<string> Thuoc_Vacxin = new List<string>();
        public List<LICHTIEMHEO> Lichtiem { get; set; }
        public LICHTIEMHEO lICHTIEMHEO { get; set; }
        public LapLichTiem()
        {
            InitializeComponent();
            Lichtiem = DataProvider.Ins.DB.LICHTIEMHEOs.ToList();
            foreach (var lichtiem in Lichtiem)
            {
                LichTiem_TenThuoc lichTiem_TenThuoc = new LichTiem_TenThuoc
                {
                    lichtiem = lichtiem,
                    hanghoa = DataProvider.Ins.DB.HANGHOAs.FirstOrDefault(s => s.MaHangHoa.Contains(lichtiem.MaThuoc))
                };
                Thuoc_Tiem.Add(lichTiem_TenThuoc);
            }
            Listtiemheo.SelectedItem = lICHTIEMHEO;
            Listtiemheo.ItemsSource = Thuoc_Tiem;
            Listtiemheo.SelectionMode = SelectionMode.Extended;
            setCombobox();
        }
        //event
        private void add_Button_Click(object sender, RoutedEventArgs e)
        {
            Add_LichTiem();
            Drugcode_text.Clear();
            Pigcode_text.Clear();
            Lieuluong_text.Clear();
            Datepicker_Ngaytiem.Text = "";
        }

        private void ListHeo_button_Click(object sender, RoutedEventArgs e)
        {
            ShowListHeo();
        }
        //Function
        void Add_LichTiem()
        {
            if (HeodaChon == null)
            {
                MessageBox.Show("Chưa chọn heo");
                return;
            }
            foreach (var heo in HeodaChon)
            {
                LICHTIEMHEO lichtiem = new LICHTIEMHEO();
                lichtiem.MaLichTiem = Lichtiemcode_generate();
                lichtiem.MaHeo = heo.MaHeo;
                lichtiem.MaThuoc = Drugcode_text.Text;
                try
                {
                    lichtiem.NgayTiem = Datepicker_Ngaytiem.SelectedDate.Value.Date;
                }
                catch (Exception)
                {
                    MessageBox.Show("Ngày tiêm không hợp lệ");
                    return;
                }
                try
                {
                    lichtiem.LieuLuong = Convert.ToInt32(Lieuluong_text.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show("Hãy nhập liều lượng là giá trị số", "", MessageBoxButton.OK);
                    return;
                }
                lichtiem.TrangThai = "Chưa tiêm";
                try
                {
                    DataProvider.Ins.DB.LICHTIEMHEOs.Add(lichtiem);
                    DataProvider.Ins.DB.SaveChanges();
                }
                catch (Exception)
                {

                    //MessageBox.Show("Có thông tin nhập bị lỗi, yêu cầu nhập lại.", "", MessageBoxButton.OK);
                    //return;
                }
            }
            reloadWithData();
            MessageBox.Show("Thêm thành công.", "", MessageBoxButton.OK);
        }

        string Lichtiemcode_generate()
        {
            //create a function to generate random string
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string result = new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return result;
        }

        void reloadWithData()
        {
            Listtiemheo.ItemsSource = null;
            Lichtiem.Clear();
            Thuoc_Tiem.Clear();
            Lichtiem = DataProvider.Ins.DB.LICHTIEMHEOs.ToList();
            foreach (var lichtiem in Lichtiem)
            {
                LichTiem_TenThuoc lichTiem_TenThuoc = new LichTiem_TenThuoc
                {
                    lichtiem = lichtiem,
                    hanghoa = DataProvider.Ins.DB.HANGHOAs.FirstOrDefault(s => s.MaHangHoa.Contains(lichtiem.MaThuoc))
                };
                Thuoc_Tiem.Add(lichTiem_TenThuoc);
            }
            Listtiemheo.ItemsSource = Thuoc_Tiem;
        }

        void ShowListHeo()
        {
            DanhsachHeo ds = new DanhsachHeo();
            ds.ShowDialog();
            if (ds.check == 0)
            {
                return;
            }
            HeodaChon = ds._listHEO;
            loadTxtHeoChon();
            //Pigcode_text.Text = ds.TranferCode();
        }

        public void loadTxtHeoChon()
        {
            string text = "";
            foreach (var hEO in HeodaChon)
            {
                text += hEO.MaHeo + " ";
            }
            Pigcode_text.Text = text;
        }

        void ShowListThuoc()
        {
            DanhSachThuoc ds = new DanhSachThuoc();
            ds.ShowDialog();
            if (ds.check != 0)
                Drugcode_text.Text = ds.TranferCode();
        }

        private void ListThuoc_button_Click(object sender, RoutedEventArgs e)
        {
            ShowListThuoc();
        }

        #region Click datacontext
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            LichTiem_TenThuoc lichtiem = (LichTiem_TenThuoc)Listtiemheo.SelectedItem;
            Delete(lichtiem.lichtiem);
        }

        private void btnFix_Click(object sender, RoutedEventArgs e)
        {
            LichTiem_TenThuoc tiemheo = (LichTiem_TenThuoc)Listtiemheo.SelectedItem;
            SuaLichHeo sua = new SuaLichHeo(tiemheo.lichtiem);
            sua.ShowDialog();
            if (sua.returnValue() == null)
                return;
            updating(sua.returnValue());
        }
        #endregion


        /// <summary>
        /// For click on listviewitems
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Listtiemheo.SelectedItems.Clear();

            var item = sender as ListViewItem;
            if (item != null)
            {
                item.IsSelected = true;
                Listtiemheo.SelectedItem = item;
            }
        }

        private void ListViewItem_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                //To do somthing later
            }
        }
        ///

        public void updating(LICHTIEMHEO tt)
        {
            var t = DataProvider.Ins.DB.LICHTIEMHEOs.FirstOrDefault(LICHTIEMHEO => LICHTIEMHEO.MaLichTiem.Contains(tt.MaLichTiem));
            if (t != null)
            {
                t.NgayTiem = tt.NgayTiem;
                t.LieuLuong = tt.LieuLuong;
                t.TrangThai = tt.TrangThai;
                t.MaThuoc = tt.MaThuoc;
                t.MaHeo = tt.MaHeo;
                try
                {
                    DataProvider.Ins.DB.SaveChanges();
                }
                catch (Exception)
                {

                    MessageBox.Show("Yêu cầu nhập đúng mã heo cùng các thông tin", "", MessageBoxButton.OK);
                }
                reloadWithData();
            }
            else
            {
                MessageBox.Show("Không tìm thấy", "", MessageBoxButton.OK);
            }
        }

        private void Delete(LICHTIEMHEO tiemheo)
        {
            try
            {
                var t = DataProvider.Ins.DB.LICHTIEMHEOs.FirstOrDefault(s => s.MaLichTiem.Contains(tiemheo.MaLichTiem.ToString()));
                DataProvider.Ins.DB.LICHTIEMHEOs.Remove(t);
                DataProvider.Ins.DB.SaveChanges();
                reloadWithData();
            }
            catch (Exception)
            {

                MessageBox.Show("Gặp lỗi khi xóa.", "", MessageBoxButton.OK);
            }
        }

        void setCombobox()
        {
            List<string> Tengiongheo = new List<string>();
            foreach (var i in DataProvider.Ins.DB.GIONGHEOs.Where(s => s.TenGiongHeo != null).ToList())
            {
                Tengiongheo.Add(i.TenGiongHeo);
            }
            List<string> Tenloaiheo = new List<string>();
            foreach (var i1 in DataProvider.Ins.DB.LOAIHEOs.Where(s => s.TenLoaiHeo != null).ToList())
            {
                Tenloaiheo.Add(i1.TenLoaiHeo);
            }
            List<string> Tenthuoc = new List<string>();
            foreach (var i2 in DataProvider.Ins.DB.HANGHOAs.Where(s => s.LoaiHangHoa == "Thuốc" || s.LoaiHangHoa == "Vacxin").ToList())
            {
                Tenthuoc.Add(i2.TenHangHoa);
            }
            Find_giongheo.ItemsSource = Tengiongheo;
            Find_loaiheo.ItemsSource = Tenloaiheo;
            FindLoaiThuoc.ItemsSource = Tenthuoc;
        }
        private void Timkiem()
        {
            reloadWithData();
            /*var output = DataProvider.Ins.DB.LICHTIEMHEOs.Where(s => Find_loaiheo.Text != null ? s.HEO.LOAIHEO.TenLoaiHeo.Equals(Find_loaiheo.Text) : (s.HEO.MaHeo != null) && Find_date.SelectedDate.Value != null ? s.NgayTiem == Find_date.SelectedDate.Value : (s.HEO.MaHeo != null) && Find_giongheo.Text != null ? s.HEO.GIONGHEO.TenGiongHeo.Equals(Find_giongheo.Text) : (s.HEO.MaHeo != null)).ToList();
                Thuoc_Tiem.Clear();
                //Lichtiem = DataProvider.Ins.DB.LICHTIEMHEOs.ToList();
                foreach (var lichtiem in output)
                {
                    LichTiem_TenThuoc lichTiem_TenThuoc = new LichTiem_TenThuoc
                    {
                        lichtiem = lichtiem,
                        hanghoa = DataProvider.Ins.DB.HANGHOAs.FirstOrDefault(s => s.MaHangHoa.Contains(lichtiem.MaThuoc))
                    };
                    Thuoc_Tiem.Add(lichTiem_TenThuoc);
                }
                Listtiemheo.ItemsSource = Thuoc_Tiem;*/
            if (Find_giongheo.SelectedValue != null)
            {
                Thuoc_Tiem = Thuoc_Tiem.FindAll(s => s.lichtiem.HEO.GIONGHEO.TenGiongHeo.Contains(Find_giongheo.SelectedValue.ToString()));
                Listtiemheo.ItemsSource = Thuoc_Tiem;
            }
            if (Find_loaiheo.SelectedValue != null)
            {
                Thuoc_Tiem = Thuoc_Tiem.FindAll(s => s.lichtiem.HEO.LOAIHEO.TenLoaiHeo.Contains(Find_loaiheo.SelectedValue.ToString()));
                Listtiemheo.ItemsSource = Thuoc_Tiem;
            }
            if (Find_date.SelectedDate != null)
            {
                Thuoc_Tiem = Thuoc_Tiem.FindAll(s => s.lichtiem.NgayTiem.Equals(Find_date.SelectedDate));
                Listtiemheo.ItemsSource = Thuoc_Tiem;
            }
            if (FindLoaiThuoc.SelectedValue != null)
            {
                Thuoc_Tiem = Thuoc_Tiem.FindAll(s => s.hanghoa.TenHangHoa.Contains(FindLoaiThuoc.SelectedValue.ToString()));
                Listtiemheo.ItemsSource = Thuoc_Tiem;
            }
        }

        private void Output_excel_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xls", ValidateNames = true };
            {
                if (sfd.ShowDialog() == true)
                {
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Workbook workbook = app.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet worksheet = (Worksheet)app.ActiveSheet;
                    app.Visible = false;
                    worksheet.Cells[1, 1] = "Ngày tiêm heo";
                    worksheet.Cells[1, 2] = "Mã heo";
                    worksheet.Cells[1, 3] = "Loại heo";
                    worksheet.Cells[1, 4] = "Giống heo";
                    worksheet.Cells[1, 5] = "Liều lượng";
                    worksheet.Cells[1, 6] = "Trạng thái";
                    int i = 2;
                    foreach (var items in DataProvider.Ins.DB.LICHTIEMHEOs)
                    {
                        worksheet.Cells[i, 1] = items.NgayTiem.Value.ToString();
                        worksheet.Cells[i, 2] = items.MaHeo;
                        worksheet.Cells[i, 3] = items.HEO.LOAIHEO.TenLoaiHeo;
                        worksheet.Cells[i, 4] = items.HEO.GIONGHEO.TenGiongHeo;
                        worksheet.Cells[i, 5] = items.LieuLuong;
                        worksheet.Cells[i, 6] = items.TrangThai;
                        i++;
                    }
                    worksheet.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Dữ liệu đã được lưu thành công", "", MessageBoxButton.OK);
                }
            }
        }

        private void Find_giongheo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Timkiem();
        }

        private void Find_loaiheo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Timkiem();
        }

        private void FindLoaiThuoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Timkiem();
        }

        private void Find_date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Timkiem();
        }
    }
}



