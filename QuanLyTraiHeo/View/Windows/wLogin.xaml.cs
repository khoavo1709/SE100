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

namespace WpfApp_MVVM.View.Windows
{
    /// <summary>
    /// Interaction logic for wLogin.xaml
    /// </summary>
    public partial class wLogin : Window
    {
        public wLogin()
        {
            InitializeComponent();

            Tb_Username.Text = QuanLyTraiHeo.Properties.Settings.Default.Username;
            PasswordBox.Password = QuanLyTraiHeo.Properties.Settings.Default.Password;

            //btn_DangNhap.KeyDown += Btn_DangNhap_KeyDown;
        }

        //private void Btn_DangNhap_KeyDown(object sender, KeyEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
