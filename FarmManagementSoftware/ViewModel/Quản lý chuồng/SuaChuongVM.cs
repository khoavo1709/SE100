using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.Windows;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FarmManagementSoftware.ViewModel
{
    public class SuaChuongVM : BaseViewModel
    {
        public string _TinhTrang { get; set; }
        public int? _suchuaTD { get; set; }
        #region Command
        public ICommand XacNhanCommand { get; set; }
        public CHUONGTRAI cHUONGTRAI { get; set; }
        #endregion

        public SuaChuongVM()
        {
            //XacNhanCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            //{
            //    DataProvider.Ins.DB.SaveChanges();
            //    MessageBox.Show("Đã lưu thành công!","Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            //    p.Close();
            //});
        }

        public SuaChuongVM(CHUONGTRAI ChuongTrai)
        {
            cHUONGTRAI = ChuongTrai;
            _TinhTrang = ChuongTrai.TinhTrang;
            _suchuaTD = ChuongTrai.SuaChuaToiDa;
            XacNhanCommand = new RelayCommand<Window>((p) => { return true; }, p => { Sua(p); });
        }

        private void Sua(Window p)
        {
            if (_suchuaTD == null)
            {
                MessageBox.Show("Vui lòng nhập sức chứa tối đa! ", "Thông báo!", MessageBoxButton.OK);
                return;
            }
            cHUONGTRAI.TinhTrang = _TinhTrang;
            cHUONGTRAI.SuaChuaToiDa = _suchuaTD;
            DataProvider.Ins.DB.SaveChanges();
            System.Windows.MessageBox.Show("Sửa thành công");
            p.Close();
        }
    }
}