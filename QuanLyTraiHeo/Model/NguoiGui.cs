using QuanLyTraiHeo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTraiHeo.Model
{
    public class NguoiGui: BaseViewModel
    {
        public bool _IsChecked;
        public bool IsChecked { get => _IsChecked; set { _IsChecked = value; OnPropertyChanged(); } } 
        public NHANVIEN nhanvien { get; set; }  
        public NguoiGui()
        {

        }
    }
}
