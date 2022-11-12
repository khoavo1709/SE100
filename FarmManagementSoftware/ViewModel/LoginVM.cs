using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FarmManagementSoftware.Model;
using FarmManagementSoftware.View;

namespace FarmManagementSoftware.ViewModel
{
    public class LoginVM : BaseViewModel
    {
        #region command
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand EnterEvent { get; set; }
        #endregion

        #region attributes
        string _username;
        string _password;
        NHANVIEN nhanVien;
        #endregion

        #region Proparty
        public bool IsLogin { get; set; }
        public string Username { get { return _username; } set { _username = value; OnPropertyChanged(); } }
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged(); } }

        public NHANVIEN NhanVien { get => nhanVien; set => nhanVien = value; }
        #endregion

        public LoginVM()
        {
            IsLogin = false;
            _username = FarmManagementSoftware.Properties.Settings.Default.Username;
            _password = FarmManagementSoftware.Properties.Settings.Default.Password;

            LoginCommand = new RelayCommand<Window>((p) => { return CheckEmtyUserNameAndPassword(); }, p => { Login(p); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, p => { Password = p.Password; });

        }

        void Login(Window p)
        {
            if (p == null) return;

            string _pass = MD5Hash(Password);

            nhanVien = DataProvider.Ins.DB.NHANVIENs.Where(x => x.C_Username == Username && x.C_PassWord == _pass).SingleOrDefault();

            if (nhanVien == null)
            {
                MessageBox.Show("Nhập sai tài khoản hoặc mật khẩu!");
                return;
            }

            #region xử lý nhớ mật khẩu
            if ((p as Login).Cb_RememberAccount.IsChecked == true)
            {
                Properties.Settings.Default.Username = Username;
                Properties.Settings.Default.Password = Password;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Username = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();
            }
            #endregion

            IsLogin = true;
            p.Close();
        }

        bool CheckEmtyUserNameAndPassword()
        {
            if (string.IsNullOrWhiteSpace(_username) || string.IsNullOrWhiteSpace(_password))
            {
                return false;
            }

            return true;
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();

            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();

            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }

            return hash.ToString();
        }
    }
}