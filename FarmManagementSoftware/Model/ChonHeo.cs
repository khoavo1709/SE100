using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmManagementSoftware.ViewModel;

namespace FarmManagementSoftware.Model
{
    public class ChonHeo : BaseViewModel
    {
        public bool _IsChecked;
        public bool IsChecked { get => _IsChecked; set { _IsChecked = value; OnPropertyChanged(); } }
        public HEO heo { get; set; }
        public int _Tuoi;
        public int Tuoi { get => _Tuoi; set { _Tuoi = value; OnPropertyChanged(); } }
        public string ShowTuoi { get; set; }
        public ChonHeo()
        {

        }
    }

}
