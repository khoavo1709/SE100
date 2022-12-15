using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyTraiHeo.Model
{
    public partial class ChucVuModel
    {

       public bool isSelected { get; set; }
       public string TenChucVu { get; set; }
       public ChucVuModel()
        {
            MessageBox.Show("Nếu thông báo này hiện, chương trình chắc chắn crash !!!");
        }
       public ChucVuModel(bool isSelected, string tenChucVu)
        {
            this.isSelected = isSelected;
            TenChucVu = tenChucVu;
        }
    }

    public partial class NhatKy
    {
        public string icon { get; set; }
        public string TenNhanVien { get; set; }
        public string HanhDong { get; set; }
        public string MaPhieu { get; set; }
        public DateTime ThoiGian { get; set; }
        public string Ngay { get; set; }
    }
    public partial class HanhDong
    {
        public bool ischecked { get; set; }
        public string TenHanhDong { get; set; }
        public HanhDong(bool ischecked, string tenHanhDong)
        {
            this.ischecked = ischecked;
            TenHanhDong = tenHanhDong;
        }
    }

}
