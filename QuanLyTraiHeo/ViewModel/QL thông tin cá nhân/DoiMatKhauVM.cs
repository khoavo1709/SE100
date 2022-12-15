using QuanLyTraiHeo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyTraiHeo.ViewModel
{
    public class DoiMatKhauVM: BaseViewModel
    {
        #region Attributes
        private string newPassword = "";
        private string reNewPassword = "";
        string newPassWordMD5 = "";
        #endregion

        #region Property
        public MainWindowVM MainWindowVM { get; set; }
        #endregion


        #region Event Command
        public ICommand NewPasswordChangedCommand { get; set; }
        public ICommand ReNewPasswordChangedCommand { get; set; }
        public ICommand UpdatePasswordCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        #endregion]

        public DoiMatKhauVM()
        {
            
        }

        public DoiMatKhauVM(MainWindowVM mainWindowVM)
        {
            NewPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, p => { newPassword = p.Password; });
            ReNewPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, p => { reNewPassword = p.Password; });
            UpdatePasswordCommand = new RelayCommand<Button>((p) => { return CheckNewPassEqualReNewPass(); }, p => { UpdatePassword(); });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, p => { p.Close(); });
            MainWindowVM = mainWindowVM;
        }

        #region Method

        bool CheckNewPassEqualReNewPass()
        {
            string oldPassword = MainWindowVM.NhanVien.C_PassWord;
            newPassWordMD5 = LoginVM.MD5Hash(newPassword);

            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(reNewPassword) || newPassword != reNewPassword || newPassWordMD5 == oldPassword)
            {
                return false;
            }

            return true;
        }

        void UpdatePassword()
        {
            if(MainWindowVM.NhanVien==null) return;

            if (MessageBox.Show("Bạn có chắc muốn thay đổi mật khẩu?", "Chú ý", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MainWindowVM.NhanVien.C_PassWord = newPassWordMD5;
                MainWindowVM.UpdateNhanVien();

                DataProvider.Ins.DB.SaveChanges();

                MessageBox.Show("Thay đổi thành công");
            }
        }

        #endregion
    }
}
