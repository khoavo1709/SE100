using FarmManagementSoftware.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManagementSoftware.Model
{
    public class HEOXUAT : BaseViewModel
    {
        private bool _IsChecked;
        private int _DonGia;
        public bool IsChecked { get => _IsChecked; set { _IsChecked = value; OnPropertyChanged(); } }
        public HEO heo { get; set; }

        public int DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }
        public HEOXUAT()
        {

        }
    }
}
