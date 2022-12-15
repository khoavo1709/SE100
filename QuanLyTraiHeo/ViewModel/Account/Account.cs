using QuanLyTraiHeo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTraiHeo.ViewModel
{
    public class Account: BaseViewModel
    {
        //Tài khoản của người đang đăng nhập phần mềm
        public static NHANVIEN TaiKhoan { get; set; }
    }
}
