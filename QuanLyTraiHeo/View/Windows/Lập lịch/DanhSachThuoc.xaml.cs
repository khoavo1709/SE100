using QuanLyTraiHeo.Model;
using System;
using System.Collections.Generic;
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

namespace QuanLyTraiHeo.View.Windows.Lập_lịch
{
    /// <summary>
    /// Interaction logic for DanhSachThuoc.xaml
    /// </summary>
    public partial class DanhSachThuoc : Window
    {
        List<HANGHOA> DanhsachThuoc { get; set; }
        HANGHOA hh { get; set; }
        string Thuoc = "Thuốc";
        public DanhSachThuoc()
        {
            InitializeComponent();

            DanhsachThuoc = DataProvider.Ins.DB.HANGHOAs.Where(s => s.LoaiHangHoa == Thuoc).ToList();
            ListThuoc.ItemsSource = DanhsachThuoc;
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListThuoc.SelectedItems.Clear();

            var item = sender as ListViewItem;
            if (item != null)
            {
                item.IsSelected = true;
                ListThuoc.SelectedItem = item;
            }
        }

        private void ListViewItem_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {

            }
        }

        private void check_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public string TranferCode()
        {
            hh = ListThuoc.SelectedItem as HANGHOA;
            return hh.MaHangHoa;
        }
    }
}
