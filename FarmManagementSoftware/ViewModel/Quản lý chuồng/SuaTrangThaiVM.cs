using FarmManagementSoftware.Model;
using FarmManagementSoftware.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace FarmManagementSoftware.ViewModel
{
    public class SuaTrangThaiVM
    {
        #region Command
        public ICommand XacNhanCommand { get; set; }
        public PHIEUSUACHUA pHIEUSUACHUA { get; set; }
        #endregion

        public SuaTrangThaiVM()
        {
            //XacNhanCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            //{
            //    DataProvider.Ins.DB.SaveChanges();
            //    MessageBox.Show("Đã lưu thành công!","Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            //    p.Close();
            //});
        }

        public SuaTrangThaiVM(PHIEUSUACHUA X)
        {
            XacNhanCommand = new RelayCommand<Window>((p) => { return true; }, p => { Sua(p); });
            pHIEUSUACHUA = X;
        }

        private void Sua(Window p)
        {
            DataProvider.Ins.DB.SaveChanges();
            System.Windows.MessageBox.Show("Sửa thành công");

            p.Close();
        }
    }
}
