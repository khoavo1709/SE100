using FarmManagementSoftware.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;
namespace FarmManagementSoftware.ViewModel
{
    public class ThongTinHangHoaVM : BaseViewModel
    {
        public ICommand SuaCommand { get; set; }
        public HANGHOA TTHangHoa { get; set; }

        public ThongTinHangHoaVM()
        {
        }
        public ThongTinHangHoaVM(HANGHOA hangHoa)
        {
            SuaCommand = new RelayCommand<Window>((p) => { return true; }, p => { Sua(p); });
            TTHangHoa = hangHoa;
        }

        private void Sua(Window p)
        {
            if (TTHangHoa.TenHangHoa == String.Empty || TTHangHoa.TenHangHoa == null)
            {
                MessageBox.Show("Vui lòng nhập tên hàng hoá ! ", "Thông báo!", MessageBoxButton.OK);
                return;
            }

            if (TTHangHoa.DonGia.ToString() == String.Empty || TTHangHoa.DonGia.ToString() == null)
            {
                MessageBox.Show("Vui lòng nhập đơn giá! ", "Thông báo!", MessageBoxButton.OK);
                return;
            }
            if (TTHangHoa.TinhTrang == String.Empty || TTHangHoa.TinhTrang == null)
            {
                MessageBox.Show("Vui lòng nhập tình trạng hàng hoá! ", "Thông báo!", MessageBoxButton.OK);
                return;
            }
            if (TTHangHoa.SoLuongTonKho.ToString() == String.Empty || TTHangHoa.SoLuongTonKho.ToString() == null)
            {
                MessageBox.Show("Vui lòng nhập số lượng tồn kho! ", "Thông báo!", MessageBoxButton.OK);
                return;
            }
            if (TTHangHoa.LoaiHangHoa == String.Empty || TTHangHoa.LoaiHangHoa == null)
            {
                MessageBox.Show("Vui lòng nhập số lượng tồn kho! ", "Thông báo!", MessageBoxButton.OK);
                return;
            }

            TTHangHoa.TenHangHoa.ToString().Replace(" ", "");
            TTHangHoa.DonGia.ToString().Replace(" ", "");
            TTHangHoa.TinhTrang.ToString().Replace(" ", "");
            TTHangHoa.SoLuongTonKho.ToString().Replace(" ", "");
            TTHangHoa.LoaiHangHoa.ToString().Replace(" ", "");

            DataProvider.Ins.DB.SaveChanges();
            System.Windows.MessageBox.Show("Sửa thành công");

            p.Close();
        }
    }
}
