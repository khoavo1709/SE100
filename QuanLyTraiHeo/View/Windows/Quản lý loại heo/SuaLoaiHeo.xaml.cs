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

namespace QuanLyTraiHeo.View.Windows.Quản_lý_loại_heo
{
    /// <summary>
    /// Interaction logic for SuaLoaiHeo.xaml
    /// </summary>
    public partial class SuaLoaiHeo : Window
    {
        public SuaLoaiHeo(LOAIHEO LH)
        {
            InitializeComponent();
            Textcode.Text = LH.MaLoaiHeo;
            Textname.Text = LH.TenLoaiHeo;
            textmota.Text = LH.MoTa;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Textcode.Text == "")
            {
                MessageBox.Show("Chưa nhập mã loại heo.", "", MessageBoxButton.OK);
                return;
            }
            if (Textname.Text == "" || textmota.Text == "")
            {
                MessageBox.Show("Chưa nhập đầy đủ thông tin.", "", MessageBoxButton.OK);
                return;
            }
            this.Close();
        }
        
        public LOAIHEO tranferCode()
        {
            LOAIHEO LH = new LOAIHEO();
            LH.MaLoaiHeo = Textcode.Text;
            LH.TenLoaiHeo = Textname.Text;
            LH.MoTa = textmota.Text;
            return LH;
        }
    }                         
}
