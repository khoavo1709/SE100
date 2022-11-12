using FarmManagementSoftware.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace FarmManagementSoftware.ViewModel
{
    public class ChiTietThongBaoVM : BaseViewModel
    {
        private MainWindowVM vmMainW;
        private ObservableCollection<ThongBao> _listTHONGBAOs;
        public ObservableCollection<ThongBao> listTHONGBAOs { get => _listTHONGBAOs; set { _listTHONGBAOs = value; OnPropertyChanged(); } }

        private ThongBao _SelectedItem;
        public ThongBao SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }

        public ICommand CloseChiTietThongBaoW { get; set; }
        public ChiTietThongBaoVM()
        {
        }
        public ChiTietThongBaoVM(MainWindowVM vm)
        {
            vmMainW = vm;

            listTHONGBAOs = vmMainW.listTHONGBAO;
            SelectedItem = vmMainW.selectedItem;

            CloseChiTietThongBaoW = new RelayCommand<Window>((p) => { return true; }, p => {
                vmMainW.selectedItem = null;
            });
        }

    }
}
