using Microsoft.Office.Interop.Excel;
using QuanLyTraiHeo.Model;
using QuanLyTraiHeo.View.Windows.Lập_lịch;
using System;
using System.Collections.Generic;
using Microsoft.Win32;
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
using WPFWindow = System.Windows;

namespace QuanLyTraiHeo.View.Windows
{
    /// <summary>
    /// Interaction logic for LichPhoiGiongWindow.xaml
    /// </summary>
    public partial class LichPhoiGiongWindow : WPFWindow.Window
    {
        public List<LICHPHOIGIONG> LichPhoiGiong { get; set; }
        public LICHPHOIGIONG lICHPHOIGIONG { get; set; }
        public LichPhoiGiongWindow()
        {
            InitializeComponent();

            LichPhoiGiong = DataProvider.Ins.DB.LICHPHOIGIONGs.ToList();
            ListPhoigiong.SelectedItem = lICHPHOIGIONG;
            ListPhoigiong.ItemsSource = LichPhoiGiong;
            ListPhoigiong.SelectionMode = SelectionMode.Extended;
        }

        //event
        private void add_Button_Click(object sender, RoutedEventArgs e)
        {
            Add_LichPhoiGiong();
        }

        private void ListHeod_button_Click(object sender, RoutedEventArgs e)
        {
            ShowListHeod();
        }

        private void ListHeon_button_Click(object sender, RoutedEventArgs e)
        {
            ShowListHeoc();
        }
        
        void Add_LichPhoiGiong()
        {
            LICHPHOIGIONG lichphoigiong = new LICHPHOIGIONG();
            lichphoigiong.MaLichPhoi = Lichphoigiongcode_generate();
            
            lichphoigiong.MaHeoDuc = Pigcode_textd.Text;
            lichphoigiong.MaHeoCai = Pigcode_textn.Text;
            lichphoigiong.Trangthai = TrangThai.Text;
            
            //try
            //{
            //1
            if (Datepicker_ngayde.SelectedDate == null)
            {
                lichphoigiong.NgayDuKienDe = null;
            }
            else
            {
                lichphoigiong.NgayDuKienDe = Datepicker_ngayde.SelectedDate;
            }
            //2
            if (Datepicker_Ngayphoigiong.SelectedDate == null)
            {
                lichphoigiong.NgayPhoiGiong = null;
            }
            else
            {
                lichphoigiong.NgayPhoiGiong = Datepicker_Ngayphoigiong.SelectedDate;
            }
            //3
            if (Ngaydethucte.SelectedDate == null)
            {
                lichphoigiong.NgayDeThucTe = null;
            }
            else
            {
                lichphoigiong.NgayDeThucTe = Ngaydethucte.SelectedDate.Value;
            }
            //4
            if (Ngaycaisua.SelectedDate == null)
            {
                lichphoigiong.NgayCaiSua = null;
            }
            else
            {
                lichphoigiong.NgayCaiSua = Ngaycaisua.SelectedDate;
            }
            //5
            if (NgayPhoiGiongLai.SelectedDate == null)
            {
                lichphoigiong.NgayPhoiGiongLaiDuKien = null;
            }
            else
            {
                lichphoigiong.NgayPhoiGiongLaiDuKien = NgayPhoiGiongLai.SelectedDate;
            }
            /*}
            catch (Exception)
            {
                MessageBox.Show("Ngày nhập không hợp lệ");
                return;
            }
            try
            {*/
            //1
            if (Socon.Text == "")
            {
                lichphoigiong.SoCon = null;
            }
            else
            {
                lichphoigiong.SoCon = int.Parse(Socon.Text);
            }
            //2
            if (Sochet.Text == "")
            {
                lichphoigiong.SoConChet = null;
            }
            else
            {
                lichphoigiong.SoConChet = int.Parse(Sochet.Text);
            }
            //3
            if (Soconchon.Text == "")
            {
                lichphoigiong.SoConChon = null;
            }
            else
            {
                lichphoigiong.SoConChon = int.Parse(Soconchon.Text);
            }
            
            /*}
            catch (Exception)
            {

                MessageBox.Show("Hãy nhập giá trị số", "", MessageBoxButton.OK);
            }*/
                try
                {
                    DataProvider.Ins.DB.LICHPHOIGIONGs.Add(lichphoigiong);
                    DataProvider.Ins.DB.SaveChanges();
                }
                catch (Exception)
                {

                    MessageBox.Show("Có thông tin nhập bị lỗi, yêu cầu nhập lại.", "", MessageBoxButton.OK);
                }
                reloadWithData();
        }

        string Lichphoigiongcode_generate()
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
            LichPhoiGiong = DataProvider.Ins.DB.LICHPHOIGIONGs.ToList();
            ListPhoigiong.ItemsSource = LichPhoiGiong;
        }

        void ShowListHeod()
        {
            DanhsachHeoDuc duc = new DanhsachHeoDuc();
            duc.ShowDialog();
            Pigcode_textd.Text = duc.TranferCode();
        }

        void ShowListHeoc()
        {
            DanhsachHeoCai cai = new DanhsachHeoCai();
            cai.ShowDialog();
            Pigcode_textn.Text = cai.TranferCode();
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            LICHPHOIGIONG phoigiong = (LICHPHOIGIONG)ListPhoigiong.SelectedItem;
            Delete(phoigiong);
        }

        private void btnFix_Click(object sender, RoutedEventArgs e)
        {
            LICHPHOIGIONG phoigiong = (LICHPHOIGIONG)ListPhoigiong.SelectedItem;
            SuaLichPhoiGiong sua = new SuaLichPhoiGiong(phoigiong);
            sua.ShowDialog();
            if (sua.returnValue() == null)
                return;
            updating(sua.returnValue());
        }



        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListPhoigiong.SelectedItems.Clear();

            var item = sender as ListViewItem;
            if (item != null)
            {
                item.IsSelected = true;
                ListPhoigiong.SelectedItem = item;
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

        public void updating(LICHPHOIGIONG tt)
        {
            var t = DataProvider.Ins.DB.LICHPHOIGIONGs.FirstOrDefault(LICHPHOIGIONG => LICHPHOIGIONG.MaLichPhoi.Contains(tt.MaLichPhoi));
            if (t != null)
            {
                t.MaHeoDuc = tt.MaHeoDuc;
                t.MaHeoCai = tt.MaHeoCai;
                
                t.NgayCaiSua = tt.NgayCaiSua;
                t.NgayDuKienDe = tt.NgayDuKienDe;
                t.NgayDeThucTe = tt.NgayDeThucTe;
                t.NgayPhoiGiong = tt.NgayPhoiGiong;
                t.NgayPhoiGiongLaiDuKien = tt.NgayPhoiGiongLaiDuKien;
                
                t.SoCon = tt.SoCon;
                t.SoConChet = tt.SoConChet;
                t.SoConChon = tt.SoConChon;
                
                t.Trangthai = tt.Trangthai;
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

        private void Delete(LICHPHOIGIONG tiemheo)
        {
            try
            {
                var t = DataProvider.Ins.DB.LICHPHOIGIONGs.FirstOrDefault(s => s.MaLichPhoi.Contains(tiemheo.MaLichPhoi.ToString()));
                DataProvider.Ins.DB.LICHPHOIGIONGs.Remove(t);
                DataProvider.Ins.DB.SaveChanges();
                reloadWithData();
            }
            catch (Exception)
            {

                MessageBox.Show("Gặp lỗi khi xóa.", "", MessageBoxButton.OK);
            }
        }

        private void Timkiem()
        {
            if ((Heoduc_code.Text != "")/*&&(Find_loaiheo.Text != "") */&& (Heocai_code.Text != ""))
            {
                var ti = DataProvider.Ins.DB.LICHPHOIGIONGs.Where(s => s.MaHeoDuc.Contains(Find_date.Text)).ToList();
                //ti = ti.Where(s => s.HEO.LOAIHEO.TenLoaiHeo.Contains(Find_loaiheo.Text)).ToList();
                ti = ti.Where(s => s.MaHeoCai == Heocai_code.Text).ToList();
                ListPhoigiong.ItemsSource = ti;
            }

            /*if (FindLoaiThuoc.Text != "")
            {
                var ti = DataProvider.Ins.DB.LICHTIEMHEOs.Where(s => s.MaThuoc.Contains(FindLoaiThuoc.Text)).ToList();
                Listtiemheo.ItemsSource = ti;
            }
            {
                var ti = DataProvider.Ins.DB.LICHTIEMHEOs.Where(s => s.HEO.GIONGHEO.TenGiongHeo.Contains(Find_giongheo.Text)).ToList();
                Listtiemheo.ItemsSource = ti;
            }
        }
    }
    var t = DataProvider.Ins.DB.LICHTIEMHEOs.Where(s => s.MaLichTiem.Contains(Timkiem_text.Text));
            Listtiemheo.ItemsSource = t;*/
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
                    worksheet.Cells[1, 1] = "Ngày phối giống";
                    worksheet.Cells[1, 2] = "Mã heo nái";
                    worksheet.Cells[1, 3] = "Mã heo đực";
                    worksheet.Cells[1, 4] = "Ngày đẻ dự kiến";
                    worksheet.Cells[1, 5] = "Ngày đẻ thực tế";
                    worksheet.Cells[1, 6] = "Số con đẻ";
                    worksheet.Cells[1, 7] = "Số con chết";
                    worksheet.Cells[1, 8] = "Ngày heo con cai sữa";
                    worksheet.Cells[1, 9] = "Số con chọn";
                    worksheet.Cells[1, 10] = "Trạng thái";
                    int i = 2;
                    foreach (var items in DataProvider.Ins.DB.LICHPHOIGIONGs)
                    {
                        worksheet.Cells[i, 1] = items.NgayPhoiGiong;
                        worksheet.Cells[i, 2] = items.MaHeoCai;
                        worksheet.Cells[i, 3] = items.MaHeoDuc;
                        worksheet.Cells[i, 4] = items.NgayDuKienDe;
                        worksheet.Cells[i, 5] = items.NgayDeThucTe;
                        worksheet.Cells[i, 6] = items.SoCon;
                        worksheet.Cells[i, 7] = items.SoConChet;
                        worksheet.Cells[i, 8] = items.NgayCaiSua;
                        worksheet.Cells[i, 9] = items.SoConChon;
                        worksheet.Cells[i, 10] = items.Trangthai;
                        i++;
                    }
                    worksheet.SaveAs(sfd.FileName, XlFileFormat.xlExcel12, null, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Dữ liệu đã được lưu thành công", "", MessageBoxButton.OK);
                }
            }
        }
        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            Timkiem();
        }
    }
}
