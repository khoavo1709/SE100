using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FarmManagementSoftware.ViewModel
{
    public class SuaChuongVM : BaseViewModel
    {
        #region Command
        public ICommand XacNhanCommand { get; set; }
        public CHUONGTRAI cHUONGTRAI { get; set; }
        #endregion

        public SuaChuongVM() {}

        public SuaChuongVM(CHUONGTRAI ChuongTrai)
        {
            XacNhanCommand = new RelayCommand<Window>((p) => { return true; }, p => { Sua(p); });
            cHUONGTRAI = ChuongTrai;
        }

        private void Sua(Window p)
        {
            if (cHUONGTRAI.SuaChuaToiDa == null)
            {
                MessageBox.Show("Vui lòng nhập sức chứa tối đa! ", "Thông báo!", MessageBoxButton.OK);
                return;
            }
            DataProvider.Ins.DB.SaveChanges();
            System.Windows.MessageBox.Show("Sửa thành công");
            p.Close();
        }
    }
}