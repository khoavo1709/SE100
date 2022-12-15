using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyTraiHeo.Model
{
    public class TinhTrangHangHoaModel
    {
        public bool isSelected { get; set; }
        public string tinhTrang { get; set; }
        public TinhTrangHangHoaModel()
        {
            MessageBox.Show("Nếu thông báo này hiện, chương trình chắc chắn crash !!!");
        }
        public TinhTrangHangHoaModel(bool isSelected, string tinhTrang)
        {
            this.isSelected = isSelected;
            this.tinhTrang = tinhTrang;
        }
    }
}
