using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FarmManagementSoftware.View;

namespace FarmManagementSoftware.ViewModel
{
    public class MainWindowVM : BaseViewModel
    {
        public bool IsLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public MainWindowVM()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, p => {
                IsLoaded = true;
                p.Hide();

                Login wLogin = new Login();
                wLogin.ShowDialog();

                if (wLogin.DataContext == null) return;

                var loginWD = wLogin.DataContext as LoginVM;

                if (loginWD.IsLogin)
                {
                    p.Show();
                }
                else
                {
                    p.Close();
                }

            });

        }
    }
}