using MaterialDesignThemes.Wpf;
using QuanLyTraiHeo.View.Windows;
using QuanLyTraiHeo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyTraiHeo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class wMainWindow : Window
    {
        public wMainWindow()
        {
            InitializeComponent();
            TrangChuWindow wc = new TrangChuWindow();
            wc.Close();
            Object content = wc.Content;
            wc.Content = null;
            grb_TabWindow.Children.Clear();
            grb_TabWindow.Children.Add(content as UIElement);
        }
        void formatMenu()
        {
            for (int i = 0; i < lB_Menu.Items.Count; i++)
            {
                var bc = new BrushConverter();
                (lB_Menu.Items[i] as ListBoxItem).Foreground = Brushes.LightGray;
                (lB_Menu.Items[i] as ListBoxItem).Background = (Brush)bc.ConvertFrom("#3ab19b");
            }
        }
        void CollapsedMenuDtail()
        {
            for (int i = 0; i < Tree_menu_detail.Items.Count; i++)
            {
                (Tree_menu_detail.Items[i] as TreeViewItem).Visibility = Visibility.Collapsed;
            }
        }

        private void MenuHome_Selected(object sender, RoutedEventArgs e)
        {
            CollapsedMenuDtail();
            Tree_menu_detail_TrangChu.Visibility = Visibility.Visible;
            exp_test.IsExpanded = true;
            tgl_menu.IsChecked = true;
            formatMenu();
            var bc = new BrushConverter();
            (sender as ListBoxItem).Foreground = Brushes.White;
            (sender as ListBoxItem).Background = (Brush)bc.ConvertFrom("#41c4ac");
        }
        private void MenuDanHeo_Selected(object sender, RoutedEventArgs e)
        {
            CollapsedMenuDtail();
            Tree_menu_detail_CaThe.Visibility = Visibility.Visible;
            Tree_menu_detail_Loaiheo.Visibility = Visibility.Visible;
            Tree_menu_detail_Giongheo.Visibility = Visibility.Visible;
            Tree_menu_detail_PhieuHeo.Visibility = Visibility.Visible;  
            Tree_menu_detail_LapLich.Visibility = Visibility.Visible;
            exp_test.IsExpanded = true;
            tgl_menu.IsChecked = true;
            formatMenu();
            var bc = new BrushConverter();
            (sender as ListBoxItem).Foreground = Brushes.White;
            (sender as ListBoxItem).Background = (Brush)bc.ConvertFrom("#41c4ac");
        }
        private void MenuChuongNuoi_Selected(object sender, RoutedEventArgs e)
        {
            CollapsedMenuDtail();
            Tree_menu_detail_Chuong.Visibility = Visibility.Visible;
            Tree_menu_detail_SoDoChuong.Visibility = Visibility.Visible;
            Tree_menu_detail_PhieuSuaChua.Visibility = Visibility.Visible;
            exp_test.IsExpanded = true;
            tgl_menu.IsChecked = true;
            formatMenu();
            var bc = new BrushConverter();
            (sender as ListBoxItem).Foreground = Brushes.White;
            (sender as ListBoxItem).Background = (Brush)bc.ConvertFrom("#41c4ac");
        }
        private void MenuKho_Selected(object sender, RoutedEventArgs e)
        {
            CollapsedMenuDtail();
            Tree_menu_detail_HangHoa.Visibility = Visibility.Visible;
            Tree_menu_detail_PhieuKho.Visibility = Visibility.Visible;
            exp_test.IsExpanded = true;
            tgl_menu.IsChecked = true;
            formatMenu();
            var bc = new BrushConverter();
            (sender as ListBoxItem).Foreground = Brushes.White;
            (sender as ListBoxItem).Background = (Brush)bc.ConvertFrom("#41c4ac");
        }
        private void MenuBaoCao_Selected(object sender, RoutedEventArgs e)
        {
            CollapsedMenuDtail();
            Tree_menu_detail_BaoCaoHeo.Visibility = Visibility.Visible;
            Tree_menu_detail_BaoCaoSuaChua.Visibility = Visibility.Visible;
            Tree_menu_detail_BaoCaoChiTieu.Visibility = Visibility.Visible;
            Tree_menu_detail_TonKho.Visibility = Visibility.Visible;
            exp_test.IsExpanded = true;
            tgl_menu.IsChecked = true;
            formatMenu();
            var bc = new BrushConverter();
            (sender as ListBoxItem).Foreground = Brushes.White;
            (sender as ListBoxItem).Background = (Brush)bc.ConvertFrom("#41c4ac");
        }
        private void MenuNhanVien_Selected(object sender, RoutedEventArgs e)
        {
            CollapsedMenuDtail();
            Tree_menu_detail_NhanVien.Visibility = Visibility.Visible;
            Tree_menu_detail_ChucVu.Visibility = Visibility.Visible;
            exp_test.IsExpanded = true;
            tgl_menu.IsChecked = true;
            formatMenu();
            var bc = new BrushConverter();
            (sender as ListBoxItem).Foreground = Brushes.White;
            (sender as ListBoxItem).Background = (Brush)bc.ConvertFrom("#41c4ac");
        }
        private void MenuNhatKy_Selected(object sender, RoutedEventArgs e)
        {
            CollapsedMenuDtail();
            Tree_menu_detail_NhatKy.Visibility = Visibility.Visible;
            exp_test.IsExpanded = true;
            tgl_menu.IsChecked = true;
            formatMenu();
            var bc = new BrushConverter();
            (sender as ListBoxItem).Foreground = Brushes.White;
            (sender as ListBoxItem).Background = (Brush)bc.ConvertFrom("#41c4ac");
        }
        private void MenuCayMucTieu_Selected(object sender, RoutedEventArgs e)
        {
            CollapsedMenuDtail();
            Tree_menu_detail_MucTieu.Visibility = Visibility.Visible;
            exp_test.IsExpanded = true;
            tgl_menu.IsChecked = true;
            formatMenu();
            var bc = new BrushConverter();
            (sender as ListBoxItem).Foreground = Brushes.White;
            (sender as ListBoxItem).Background = (Brush)bc.ConvertFrom("#41c4ac");
        }
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            exp_test.IsExpanded = true;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            exp_test.IsExpanded = false;
        }

        private void bad_ThongBao_BadgeChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Badged p = (sender as Badged);
            if(p.Badge.ToString() == "0")
            {
                p.BadgeBackground = Brushes.Transparent;
                p.BadgeForeground = Brushes.Transparent;
            }
            else
            {
                p.BadgeBackground = Brushes.Red;
                p.BadgeForeground = Brushes.White;
            }
        }

        private void btn_OpenSoDo_Click(object sender, RoutedEventArgs e)
        {
            wSoDo wc = new wSoDo();
            SoDoVM soDoVM = new SoDoVM();
            wc.DataContext = soDoVM;
            wc.ShowDialog();
        }

        private void Menu_QuyDinh_Selected(object sender, RoutedEventArgs e)
        {
            CollapsedMenuDtail();
            Tree_menu_QuyDinh.Visibility = Visibility.Visible;
            exp_test.IsExpanded = true;
            tgl_menu.IsChecked = true;
            formatMenu();
            var bc = new BrushConverter();
            (sender as ListBoxItem).Foreground = Brushes.White;
            (sender as ListBoxItem).Background = (Brush)bc.ConvertFrom("#41c4ac");

        }
    }
}
