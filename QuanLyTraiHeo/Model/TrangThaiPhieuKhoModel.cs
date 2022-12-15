using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyTraiHeo.Model
{
    public class TrangThaiPhieuKhoModel
    {
        public bool isSelected { get; set; }
        public string trangThai { get; set; }
        public TrangThaiPhieuKhoModel()
        {
            MessageBox.Show("Nếu thông báo này hiện, chương trình chắc chắn crash !!!");
        }
        public TrangThaiPhieuKhoModel(bool isSelected, string trangThai)
        {
            this.isSelected = isSelected;
            this.trangThai = trangThai;
        }
    }
}