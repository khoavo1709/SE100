using QuanLyTraiHeo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTraiHeo.Model
{
    public class CTPhieuModel : BaseViewModel
    {
        private string maChuong = "";
        private string moTa = "";
        public string MaChuong { get => maChuong; set { maChuong = value; OnPropertyChanged(); } }
        public string MoTa { get => moTa; set { moTa = value; OnPropertyChanged(); } }
        public CTPhieuModel() { }
        public CTPhieuModel(string a, string b)
        {
            MaChuong = a;
            MoTa = b;
        }
    }
}
