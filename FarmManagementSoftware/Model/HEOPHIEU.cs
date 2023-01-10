using FarmManagementSoftware.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManagementSoftware.Model
{
    public class HEOPHIEU : BaseViewModel
    {
        private bool _IsChecked;
        private int _DonGia;
        public bool IsChecked
        {
            get => _IsChecked; set
            {
                //if(value != false)
                //{
                //    string msg;
                //    THAMSO thamso = DataProvider.Ins.DB.THAMSOes.FirstOrDefault();
                //    TimeSpan tuoiheo = (TimeSpan)(DateTime.Now.Date - heo.NgaySinh);
                //    if (tuoiheo.Days < thamso.MonthXuatChuongMin)
                //    {
                //        msg = "Heo chưa đến tuổi xuất chuồng";
                //        MessageBox.Show(msg);
                //        return;
                //    }
                //    if (tuoiheo.Days > thamso.MonthXuatChuongMax)
                //    {
                //        msg = "Heo đã quá tuổi xuất chuồng";
                //        MessageBox.Show(msg);
                //        return;
                //    }
                //    if (heo.TrongLuong < thamso.XuatChuongMin)
                //    {
                //        msg = "Heo chưa đủ cân nặng xuất chuồng";
                //        MessageBox.Show(msg);
                //        return;
                //    }
                //    if (heo.TrongLuong > thamso.XuatChuongMax)
                //    {
                //        msg = "Heo đã quá cân nặng xuất chuồng";
                //        MessageBox.Show(msg);
                //        return;
                //    }
                //}    

                _IsChecked = value;
                OnPropertyChanged();
            }
        }
        public HEO heo { get; set; }

        public int DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }
        public HEOPHIEU()
        {
            _IsChecked = false;
            _DonGia = 0;
            heo = new HEO();
        }
    }
}
