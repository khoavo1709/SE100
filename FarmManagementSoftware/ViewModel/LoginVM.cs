using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FarmManagementSoftware.View;

namespace FarmManagementSoftware.ViewModel
{
    public class LoginVM : BaseViewModel
    {
        public bool IsLogin { get; set; }

        #region command
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        #endregion

        string _username;
        string _password;

        public string Username { get { return _username; } set { _username = value; OnPropertyChanged(); } }
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged(); } }

        public LoginVM()
        {
            IsLogin = false;
            LoginCommand = new RelayCommand<Window>((p) => { return CheckEmtyUserNameAndPassword(); }, p => { Login(p); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, p => { Password = p.Password; });
        }

        void Login(Window p)
        {
            if (p == null) return;

            /*
             Xử lý đăng nhập với database ở đây
             */

            #region xử lý nhớ mật khẩu
            //if ((p as wLogin).Cb_RememberAccount.IsChecked == true)
            //{
            //    Properties.Settings.Default.Username = Username;
            //    Properties.Settings.Default.Password = Password;
            //    Properties.Settings.Default.Save();
            //}
            //else
            //{
            //    Properties.Settings.Default.Username = "";
            //    Properties.Settings.Default.Password = "";
            //    Properties.Settings.Default.Save();
            //}
            #endregion

            IsLogin = true;
            p.Close();
        }

        bool CheckEmtyUserNameAndPassword()
        {
            if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password))
            {
                return false;
            }
            return true;
        }
    }
}