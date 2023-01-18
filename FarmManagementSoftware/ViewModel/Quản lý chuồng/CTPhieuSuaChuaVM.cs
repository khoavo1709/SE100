using FarmManagementSoftware.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FarmManagementSoftware.ViewModel
{
    public class CTPhieuSuaChuaVM: BaseViewModel
    {
        private PHIEUSUACHUA _phieuSC;
        public PHIEUSUACHUA phieuSC { get=> _phieuSC; set { _phieuSC = value; OnPropertyChanged(); }  }
        private string _TrangThai;
        public string TrangThai { get => _TrangThai; set { _TrangThai = value; OnPropertyChanged(); } }

        #region command
        public ICommand HoanTatCommand { get; set; }
        public ICommand HuyBoCommand { get; set; }
        #endregion
        public CTPhieuSuaChuaVM()
        {

        }
        public CTPhieuSuaChuaVM(PHIEUSUACHUA phieu)
        {
            phieuSC = phieu;
            TrangThai = phieu.TrangThai.ToString();

            #region command Hoàn tất
            HoanTatCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, p =>
            {
                phieu.TrangThai = TrangThai;
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Đã sửa thành công");
                p.Close();
            });
            #endregion

            #region command Hủy bỏ
            HuyBoCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, p =>
            {
                p.Close();
            });
            #endregion
        }
    }
}
