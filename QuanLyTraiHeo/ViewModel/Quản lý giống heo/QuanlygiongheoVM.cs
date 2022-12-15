using QuanLyTraiHeo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyTraiHeo.ViewModel
{
    public class QuanlygiongheoVM : BaseViewModel
    {
        public ObservableCollection<GIONGHEO> ListGiong { get; set; }

        public ICommand DeleteCommand;

        public GIONGHEO SelectedGiong { get; set; }

        public QuanlygiongheoVM()
        {
            ListGiong = new ObservableCollection<GIONGHEO>(DataProvider.Ins.DB.GIONGHEOs);
            DeleteCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, p =>
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn xoá ?", "Cảnh báo", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataProvider.Ins.DB.GIONGHEOs.Remove(SelectedGiong);
                    ListGiong.Remove(SelectedGiong);
                    DataProvider.Ins.DB.SaveChanges();

                }
            });
        }
    }
}
