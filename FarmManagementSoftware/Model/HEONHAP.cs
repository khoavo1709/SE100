using FarmManagementSoftware.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManagementSoftware.Model
{
    public class HEONHAP : BaseViewModel
    {
        private int _DonGia;
        public HEO heo { get; set; }

        public int DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }
        public HEONHAP()
        {
            heo=new HEO();  
        }
    }
}
