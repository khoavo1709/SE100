using FarmManagementSoftware.View.Windows.Quản_lý_nhân_viên;
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
using FarmManagementSoftware.Model;

namespace FarmManagementSoftware.View.Windows.Quản_lý_loại_heo
{
    /// <summary>
    /// Interaction logic for Quanlyloaiheo.xaml
    /// </summary>
    public partial class Quanlyloaiheo : Window
    {
        public List<LOAIHEO> BaseLoaiHeo { get; set; }
        public Quanlyloaiheo()
        {
            InitializeComponent();
            BaseLoaiHeo = DataProvider.Ins.DB.LOAIHEOs.ToList();
            listviewHeo.ItemsSource = BaseLoaiHeo;
        }

        private void btn_ThemClick(object sender, RoutedEventArgs e)
        {
            /*if (Pigcode_textbox.Text == "")
            {
                MessageBox.Show("Chưa nhập mã loại heo.", "", MessageBoxButton.OK);
                return;
            }*/
            string temp = MaLoaiHeo_CodeGenerate();
            if (Pigname_textbox.Text == "" || Mota_textbox.Text == "")
            {
                MessageBox.Show("Chưa nhập đầy đủ thông tin.", "", MessageBoxButton.OK);
                return;
            }
            if (Pigname_textbox.Text != "")
            {
                if (Isexist(Pigname_textbox.Text) == false)
                {
                    var t = new LOAIHEO
                    {
                        MaLoaiHeo = temp,
                        TenLoaiHeo = Pigname_textbox.Text,
                        MoTa = Mota_textbox.Text
                    };
                    Add_ustomer(t);
                    MessageBox.Show("Thêm thành công.", "", MessageBoxButton.OK);
                    Pigname_textbox.Clear();
                    Mota_textbox.Clear();
                }
                else
                {
                    MessageBox.Show("Đã tồn tại tên loại heo trên.", "", MessageBoxButton.OK);
                    Pigname_textbox.Clear();
                    Mota_textbox.Clear();
                }
                reloadUsingDTProvider();
            }
        }

        string MaLoaiHeo_CodeGenerate()
        {
            //create a function to generate random string
            Random random = new Random();
            string chars = "0123456789";
            string result = "LH";
            result += (new string(Enumerable.Repeat(chars, 14)
              .Select(s => s[random.Next(s.Length)]).ToArray()));
            return result;
        }

        private void Find_button_Click(object sender, RoutedEventArgs e)
        {
            Timkiem(Find_textbox.Text);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            LOAIHEO lOAIHEO = (LOAIHEO)listviewHeo.SelectedItem;
            if (lOAIHEO != null)
                Delete(lOAIHEO);
        }

        private void btnFix_Click(object sender, RoutedEventArgs e)
        {
            LOAIHEO loaiheo = (LOAIHEO)listviewHeo.SelectedItem;
            if (loaiheo != null)
            {
                SuaLoaiHeo sua = new SuaLoaiHeo(loaiheo);
                sua.ShowDialog();
                updating(sua.tranferCode());
            }
        }


        /// <summary>
        /// Thêm entity
        /// </summary>
        /// <param name="loaiheo"></param>
        private void Add_ustomer(LOAIHEO loaiheo)
        {
            try
            {
                DataProvider.Ins.DB.Entry(loaiheo).State = System.Data.Entity.EntityState.Added;
                DataProvider.Ins.DB.SaveChanges();
                reloadUsingDTProvider();
            }

            catch (Exception)
            {

                MessageBox.Show("Lỗi nhập xuất", "", MessageBoxButton.OK);
            }
        }

        public void updating(LOAIHEO LH)
        {
            var t = DataProvider.Ins.DB.LOAIHEOs.FirstOrDefault(s => s.MaLoaiHeo.Equals(LH.MaLoaiHeo));
            if (t != null)
            {
                try
                {
                    t.TenLoaiHeo = LH.TenLoaiHeo;
                    t.MoTa = LH.MoTa;
                    DataProvider.Ins.DB.SaveChanges();
                }
                catch (Exception)
                {

                    MessageBox.Show("Có vấn đề trong việc nhập liệu, thử lại sau", "", MessageBoxButton.OK);
                }
                reloadUsingDTProvider();
            }
            else
            {
                MessageBox.Show("Không tìm thấy", "", MessageBoxButton.OK);
            }
        }

        private void Delete(LOAIHEO lOAIHEO)
        {
            try
            {
                DataProvider.Ins.DB.LOAIHEOs.Remove(lOAIHEO);
                DataProvider.Ins.DB.SaveChanges();
                reloadUsingDTProvider();
            }
            catch (Exception)
            {

                MessageBox.Show("Gặp lỗi khi xóa.", "", MessageBoxButton.OK);
            }
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
                //To do somthing later
            }
        }

        private void Timkiem(string a)
        {

            var BaseLoaiHeotemp = DataProvider.Ins.DB.LOAIHEOs.Where(s => s.TenLoaiHeo.Contains(a)).ToList();
            if (BaseLoaiHeotemp != null)
            {
                BaseLoaiHeo.Clear();
                foreach (var items in BaseLoaiHeotemp)
                {
                    BaseLoaiHeo.Add(items);
                }
                listviewHeo.ItemsSource = null;
                listviewHeo.ItemsSource = BaseLoaiHeo;
            }
            else
            {
                MessageBox.Show("Không tìm thấy", "", MessageBoxButton.OK);
            }

        }

        private bool Isexist(string check)
        {
            var BaseLoaiHeotemp = DataProvider.Ins.DB.LOAIHEOs.FirstOrDefault(s => s.TenLoaiHeo.Contains(check));
            if (BaseLoaiHeotemp == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        void reloadUsingDTProvider()
        {
            listviewHeo.ItemsSource = null;
            BaseLoaiHeo.Clear();
            BaseLoaiHeo = DataProvider.Ins.DB.LOAIHEOs.ToList();
            listviewHeo.ItemsSource = BaseLoaiHeo;
        }

        private void Find_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Timkiem(Find_textbox.Text);
        }
    }
}
