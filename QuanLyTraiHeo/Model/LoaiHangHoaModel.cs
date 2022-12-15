using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyTraiHeo.Model
{
    public class LoaiHangHoaModel
    {
        public bool isSelected { get; set; }
        public string loaiHangHoa { get; set; }
        public LoaiHangHoaModel()
        {
            MessageBox.Show("Nếu thông báo này hiện, chương trình chắc chắn crash !!!");
        }
        public LoaiHangHoaModel(bool isSelected, string loaiHangHoa)
        {
            this.isSelected = isSelected;
            this.loaiHangHoa = loaiHangHoa;
        }
    }
}
