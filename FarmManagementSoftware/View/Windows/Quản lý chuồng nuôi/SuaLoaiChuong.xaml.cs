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
    /// Interaction logic for SuaLoaiChuong.xaml
    /// </summary>
    public partial class SuaLoaiChuong : Window
    {
        public SuaLoaiChuong(LOAICHUONG LC)
        {
            InitializeComponent();
            textcode.Text = LC.MaLoaiChuong;
            textname.Text = LC.TenLoai;
            textmota.Text = LC.MoTa;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textcode.Text == "")
            {
                MessageBox.Show("Chưa nhập mã loại chuồng.", "", MessageBoxButton.OK);
                return;
            }
            if (textname.Text == "" || textmota.Text == "")
            {
                MessageBox.Show("Chưa nhập đầy đủ thông tin.", "", MessageBoxButton.OK);
                return;
            }
            this.Close();
        }

        public LOAICHUONG tranferCode()
        {
            LOAICHUONG LH = new LOAICHUONG();
            LH.MaLoaiChuong = textcode.Text;
            LH.TenLoai = textname.Text;
            LH.MoTa = textmota.Text;
            return LH;
        }
    }
}
