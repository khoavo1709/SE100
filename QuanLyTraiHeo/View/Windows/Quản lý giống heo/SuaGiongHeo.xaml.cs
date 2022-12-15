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

namespace QuanLyTraiHeo.View.Windows.Quản_lý_giống_heo
{
    /// <summary>
    /// Interaction logic for SuaGiongHeo.xaml
    /// </summary>
    public partial class SuaGiongHeo : Window
    {
        public SuaGiongHeo(GIONGHEO GH)
        {
            InitializeComponent();
            textcode.Text = GH.MaGiongHeo;
            textname.Text = GH.TenGiongHeo;
            textmota.Text = GH.MoTa;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textcode.Text == "")
            {
                MessageBox.Show("Chưa nhập mã loại heo.", "", MessageBoxButton.OK);
                return;
            }
            if (textname.Text == "" || textmota.Text == "")
            {
                MessageBox.Show("Chưa nhập đầy đủ thông tin.", "", MessageBoxButton.OK);
                return;
            }
            this.Close();
        }

        public GIONGHEO tranferCode()
        {
            GIONGHEO GH = new GIONGHEO();
            GH.MaGiongHeo = textcode.Text;
            GH.TenGiongHeo = textname.Text;
            GH.MoTa = textmota.Text;
            return GH;
        }
    }
}
