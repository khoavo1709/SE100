using QuanLyTraiHeo.View.Windows.Quản_lý_loại_heo;
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
using QuanLyTraiHeo.Model;

namespace QuanLyTraiHeo.View.Windows.Quản_lý_giống_heo
{
    /// <summary>
    /// Interaction logic for Quanlygiongheo.xaml
    /// </summary>
    public partial class Quanlygiongheo : Window
    {
        public List<GIONGHEO> Basegiongheo { get; set; }
        public Quanlygiongheo()
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
                    var t = new GIONGHEO
                    {
                        MaGiongHeo = text1.Text,
                        TenGiongHeo = text2.Text,
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
            GIONGHEO giongheo = (GIONGHEO)listviewHeo.SelectedItem;
            SuaGiongHeo sua = new SuaGiongHeo(giongheo);
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
            GIONGHEO gIONGHEO = (GIONGHEO)listviewHeo.SelectedItem;
            Delete(gIONGHEO);
        }
        /// <summary>
        /// methods
        /// </summary>
        public void updating(GIONGHEO GH)
        {
            var t = DataProvider.Ins.DB.GIONGHEOs.FirstOrDefault(GIONGHEO => GIONGHEO.MaGiongHeo.Equals(GH.MaGiongHeo));
            if (t != null)
            {
                t.TenGiongHeo = GH.TenGiongHeo;
                t.MoTa = GH.MoTa;
                DataProvider.Ins.DB.SaveChanges();
                reloadWithDataprovider();
            }
            else
            {
                MessageBox.Show("Không tìm thấy", "", MessageBoxButton.OK);
            }
        }

        public void Add_ustomer(GIONGHEO giongheo)
        {
            try
            {
                    DataProvider.Ins.DB.Entry(giongheo).State = System.Data.Entity.EntityState.Added;
                    DataProvider.Ins.DB.SaveChanges();
                    reloadWithDataprovider();
            }
            catch (Exception)
            {

                MessageBox.Show("Lỗi nhập xuất", "", MessageBoxButton.OK);
            }
        }

        private void Delete(GIONGHEO giongheo)
        {
            try
            {
                DataProvider.Ins.DB.GIONGHEOs.Remove(giongheo);
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
            var t = DataProvider.Ins.DB.GIONGHEOs.Where(s => s.MaGiongHeo.Contains(a)).ToList();
            if (t != null)
            {
                Basegiongheo.Clear();
                foreach (var items in t)
                {
                    Basegiongheo.Add(items);
                }
                listviewHeo.ItemsSource = null;
                listviewHeo.ItemsSource = Basegiongheo;
            }
            else
            {
                MessageBox.Show("Không tìm thấy", "", MessageBoxButton.OK);
            }

        }

        private bool Isexist(string check)
        {
            var BaseGiongHeotemp = DataProvider.Ins.DB.GIONGHEOs.Where(s => s.MaGiongHeo.Contains(check)).ToList();
            if (BaseGiongHeotemp != null)
            {
                return false;
            }
            return true;
        }

        private void reloadWithDataprovider()
        {
            listviewHeo.ItemsSource = null;
            Basegiongheo = DataProvider.Ins.DB.GIONGHEOs.ToList();
            listviewHeo.ItemsSource = Basegiongheo;
        }


    }
}
