using FarmManagementSoftware.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace FarmManagementSoftware.ViewModel
{
    public class SuaChuongVM : BaseViewModel
    {
        #region Command
        public ICommand XacNhanCommand { get; set; }
        #endregion

        public SuaChuongVM()
        {
            XacNhanCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Đã lưu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                p.Close();
            });
        }
    }
}
