using QuanLyTraiHeo.Model;
using QuanLyTraiHeo.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyTraiHeo.ViewModel
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
                MessageBox.Show("Đã lưu thành công!","Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                p.Close();
            });
        }
    }
}