using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyTraiHeo.ViewModel
{
    public class LapLichTiemVM: BaseViewModel
    {
        private List<Heo> danhSachHeoLapLich;

        public List<Heo> DanhSachHeoLapLich { get => danhSachHeoLapLich; set { danhSachHeoLapLich = value; OnPropertyChanged(); } }

        public LapLichTiemVM()
        {
            HienThiDanhSachHeoLapLich();
        }

        void HienThiDanhSachHeoLapLich()
        {
            danhSachHeoLapLich = new List<Heo>();

            danhSachHeoLapLich.Add(new Heo() { STT = 1, MaHeo = "15", LoaiHeo = "Heo Me" });
            danhSachHeoLapLich.Add(new Heo() { STT = 2, MaHeo = "16", LoaiHeo = "Heo Con" });

        }

    }

    public class Heo
    {
        public int STT { get; set; }
        public string MaHeo { get; set; }
        public string LoaiHeo { get; set; }
        public int SoLieu { get; set; }

    }
}
