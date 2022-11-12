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

namespace FarmManagementSoftware.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

            Tb_Username.Text = FarmManagementSoftware.Properties.Settings.Default.Username;
            PasswordBox.Password = FarmManagementSoftware.Properties.Settings.Default.Password;

            btn_DangNhap.KeyDown += Btn_DangNhap_KeyDown;
        }

        private void Btn_DangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
