using FarmManagementSoftware.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FarmManagementSoftware.View.Windows.Quản_lý_đàn_heo
{
    /// <summary>
    /// Interaction logic for DonGia.xaml
    /// </summary>
    public partial class DonGia : Window
    {
        HEOXUAT heo = new HEOXUAT();
        public DonGia()
        {
            InitializeComponent();
        }
        public DonGia(HEOXUAT hx)
        {
            InitializeComponent();

            heo = hx;
            label.Content += hx.heo.MaHeo;
            txt_dongia.Text = hx.DonGia.ToString();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            heo.DonGia = int.Parse(txt_dongia.Text);
            heo.IsChecked = true;
            this.Close();
        }
    }
}
