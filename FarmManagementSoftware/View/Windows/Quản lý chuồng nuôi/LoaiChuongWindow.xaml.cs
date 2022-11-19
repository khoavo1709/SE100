using FarmManagementSoftware.Model;
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

namespace FarmManagementSoftware.View.Windows
{
    /// <summary>
    /// Interaction logic for LoaiChuongWindow.xaml
    /// </summary>
    public partial class LoaiChuongWindow : Window
    {
        public List<LOAICHUONG> Baseloaichuong { get; set; }
        public LoaiChuongWindow()
        {
            InitializeComponent();


            reloadWithDataprovider();
        }

        /// <summary>
        /// Button event
        /// </summary>
        private void btn_ThemClick(object sender, RoutedEventArgs e)
        {
            if (text1.Text == "")
            {
                MessageBox.Show("Chưa nhập mã loại heo.", "", MessageBoxButton.OK);
                return;
            }
            if (text2.Text == "" || text3.Text == "")
            {
                MessageBox.Show("Chưa nhập đầy đủ thông tin.", "", MessageBoxButton.OK);
                return;
            }
            if (text1.Text != "")
            {
                if (Isexist(text1.Text) == false)
                {
                    var t = new LOAICHUONG
                    {
                        MaLoaiChuong = text1.Text,
                        TenLoai = text2.Text,
                        MoTa = text3.Text
                    };
                    Add_ustomer(t);
                }
                else
                {
                    MessageBox.Show("Đã tồn tại mã loại heo trên", "", MessageBoxButton.OK);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Timkiem(Find_textbox.Text);
        }

        private void btnFix_Click(object sender, RoutedEventArgs e)
        {
            LOAICHUONG x = (LOAICHUONG)listviewHeo.SelectedItem;
            SuaLoaiChuong sua = new SuaLoaiChuong(x);
            sua.ShowDialog();
            updating(sua.tranferCode());
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            listviewHeo.SelectedItems.Clear();

            var item = sender as ListViewItem;
            if (item != null)
            {
                item.IsSelected = true;
                listviewHeo.SelectedItem = item;
            }
        }

        private void ListViewItem_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {

            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            LOAICHUONG x = (LOAICHUONG)listviewHeo.SelectedItem;
            Delete(x);
        }
        /// <summary>
        /// methods
        /// </summary>
        public void updating(LOAICHUONG LC)
        {
            var t = DataProvider.Ins.DB.LOAICHUONGs.FirstOrDefault(x => x.MaLoaiChuong.Equals(LC.MaLoaiChuong));
            if (t != null)
            {
                t.MaLoaiChuong = LC.MaLoaiChuong;
                t.MoTa = LC.MoTa;
                DataProvider.Ins.DB.SaveChanges();
                reloadWithDataprovider();
            }
            else
            {
                MessageBox.Show("Không tìm thấy", "", MessageBoxButton.OK);
            }
        }

        public void Add_ustomer(LOAICHUONG x)
        {
            try
            {
                DataProvider.Ins.DB.Entry(x).State = System.Data.Entity.EntityState.Added;
                DataProvider.Ins.DB.SaveChanges();
                reloadWithDataprovider();
            }
            catch (Exception)
            {

                MessageBox.Show("Lỗi nhập xuất", "", MessageBoxButton.OK);
            }
        }

        private void Delete(LOAICHUONG x)
        {
            try
            {
                DataProvider.Ins.DB.LOAICHUONGs.Remove(x);
                DataProvider.Ins.DB.SaveChanges();
                reloadWithDataprovider();
            }
            catch (Exception)
            {

                MessageBox.Show("Gặp lỗi khi xóa.", "", MessageBoxButton.OK);
            }
        }

        private void Timkiem(string a)
        {
            var t = DataProvider.Ins.DB.LOAICHUONGs.Where(s => s.MaLoaiChuong.Contains(a)).ToList();
            if (t != null)
            {
                Baseloaichuong.Clear();
                foreach (var items in t)
                {
                    Baseloaichuong.Add(items);
                }
                listviewHeo.ItemsSource = null;
                listviewHeo.ItemsSource = Baseloaichuong;
            }
            else
            {
                MessageBox.Show("Không tìm thấy", "", MessageBoxButton.OK);
            }

        }

        private bool Isexist(string check)
        {
            var temp = DataProvider.Ins.DB.LOAICHUONGs.Where(s => s.MaLoaiChuong.Contains(check)).ToList();
            if (temp != null)
            {
                return false;
            }
            return true;
        }

        private void reloadWithDataprovider()
        {
            listviewHeo.ItemsSource = null;
            Baseloaichuong = DataProvider.Ins.DB.LOAICHUONGs.ToList();
            listviewHeo.ItemsSource = Baseloaichuong;
        }

    }
}