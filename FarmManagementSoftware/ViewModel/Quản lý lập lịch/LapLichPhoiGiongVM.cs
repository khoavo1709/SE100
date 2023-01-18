using MaterialDesignThemes.Wpf;
using Microsoft.Office.Interop.Excel;
using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.Windows;
using FarmManagementSoftware.View.Windows.Lập_lịch;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace FarmManagementSoftware.ViewModel
{
    public class LapLichPhoiGiongVM: BaseViewModel
    {
        /*#region Atributes

        SnackbarMessageQueue myCustomMessageQueue;
        Brush messageQueueColor;

        LichPhoiGiongWindow wd;

        ObservableCollection<LICHPHOIGIONG> listLich;
        ObservableCollection<string> listHeoDuc;
        ObservableCollection<string> listHeoCai;
        #endregion

        #region Property
        public ObservableCollection<LICHPHOIGIONG> ListLich { get => listLich; set { listLich = value; OnPropertyChanged(); } }
        public ObservableCollection<string> ListHeoDuc { get => listHeoDuc; set => listHeoDuc = value; }
        public ObservableCollection<string> ListHeoCai { get => listHeoCai; set => listHeoCai = value; }
        public int listviewSelectedIndex { get; set; }
        public SnackbarMessageQueue MyCustomMessageQueue { get => myCustomMessageQueue; set { myCustomMessageQueue = value; OnPropertyChanged(); } }
        public Brush MessageQueueColor { get => messageQueueColor; set { messageQueueColor = value; OnPropertyChanged(); } }

        #endregion

        #region Event Command
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand SelectedItemCommand { get; set; }
        #endregion

        #region Command
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        #endregion

        public LapLichPhoiGiongVM()
        {
            listLich = new ObservableCollection<LICHPHOIGIONG>();
            listHeoDuc = new ObservableCollection<string>();
            listHeoCai = new ObservableCollection<string>();

            foreach (var item in DataProvider.Ins.DB.HEOs.Where(x => x.MaLoaiHeo == "LH02112022000002"))
            {
                ListHeoDuc.Add(item.MaHeo);
            }
            foreach (var item in DataProvider.Ins.DB.HEOs.Where(x => x.MaLoaiHeo == "LH02112022000001"))
            {
                ListHeoDuc.Add(item.MaHeo);
            }

            LoadedWindowCommand = new RelayCommand<object>((p) => { return true; }, p => { Load(); wd = p as LichPhoiGiongWindow; });
            SelectedItemCommand = new RelayCommand<object>((p) => { return true; }, p => { SelectedItem(p); });
            AddCommand = new RelayCommand<object>((p) => { return true; }, p => { AddNew(); });
            EditCommand = new RelayCommand<System.Windows.Window>((p) => { return true; }, p => { Edit(); });
            DeleteCommand = new RelayCommand<System.Windows.Window>((p) => { return true; }, p => { Delete(); });
        }

        public void Load()
        {
            listviewSelectedIndex = 0;
            ListLich = new ObservableCollection<LICHPHOIGIONG>(DataProvider.Ins.DB.LICHPHOIGIONGs);
        }

        void SelectedItem(object p)
        {
            //SelectedLichPhoi = p as LICHPHOIGIONG;
        }

        #region Add
        void AddNew()
        {
            SuaLichPhoiGiong sua = new SuaLichPhoiGiong(null, this);
            sua.ShowDialog();

            if (sua.OK)
            {
                var bc = new BrushConverter();
                MessageQueueColor = (Brush)bc.ConvertFrom("#00de1e");
                MyCustomMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000));
                MyCustomMessageQueue.Enqueue("Thêm thành công");
            }

            Load();
        }
        #endregion

        #region Edit
        void Edit()
        {
            if (listviewSelectedIndex < 0)
                return;
            SuaLichPhoiGiong sua = new SuaLichPhoiGiong(ListLich[listviewSelectedIndex], this);
            sua.ShowDialog();

            if (sua.OK)
            {
                var bc = new BrushConverter();
                MessageQueueColor = (Brush)bc.ConvertFrom("#FFDE03");
                MyCustomMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000));
                MyCustomMessageQueue.Enqueue("Sửa thành công");
            }
            

            Load();
        }
        #endregion

        #region Delete
        void Delete()
        {
            if (listviewSelectedIndex < 0)
                return;
            try
            {
                DataProvider.Ins.DB.LICHPHOIGIONGs.Remove(ListLich[listviewSelectedIndex]);
                DataProvider.Ins.DB.SaveChanges();

                var bc = new BrushConverter();
                MessageQueueColor = (Brush)bc.ConvertFrom("#FF0266");
                MyCustomMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000));
                MyCustomMessageQueue.Enqueue("Xóa thành công");

                Load();
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Gặp lỗi khi xóa.", "", MessageBoxButton.OK);
            }
        }

        #endregion

        void Output_excel()
        {
            SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xls", ValidateNames = true };
            {
                if (sfd.ShowDialog() == DialogResult.OK)
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
                    System.Windows.MessageBox.Show("Dữ liệu đã được lưu thành công", "", MessageBoxButton.OK);
                }
            }
        }*/


    }
}
