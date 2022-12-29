using FarmManagementSoftware.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManagementSoftware.Model
{
    public class CTPhieuModel : BaseViewModel
    {
        private string maChuong = "";
        private string moTa = "";
        private string tienSuaChua = "";
        public string MaChuong { get => maChuong; set { maChuong = value; OnPropertyChanged(); } }
        public string MoTa { get => moTa; set { moTa = value; OnPropertyChanged(); } }
        public string TienSuaChua { get => tienSuaChua; set { tienSuaChua = value; OnPropertyChanged(); } }
        public CTPhieuModel() { }
        public CTPhieuModel(string a, string b, string c)
        {
            MaChuong = a;
            MoTa = b;
            TienSuaChua = c;
        }
    }
}
