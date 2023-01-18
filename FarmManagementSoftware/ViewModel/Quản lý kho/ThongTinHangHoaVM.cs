using FarmManagementSoftware.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ICommand ExitCommand { get; set; }
        public HANGHOA TTHangHoa { get; set; }
        public HANGHOA hangHoa { get; set; }
        public ObservableCollection<LoaiHangHoaModel> listLoaiHangHoa { get; set; }

        public ThongTinHangHoaVM()
        {
        }
        public ThongTinHangHoaVM(HANGHOA hangHoa)
        {
            listLoaiHangHoa = new ObservableCollection<LoaiHangHoaModel>();
            listLoaiHangHoa.Clear();
            var listloaihanghoa = from c in DataProvider.Ins.DB.HANGHOAs
                                  select new { c.LoaiHangHoa };
            var listloaihanghoanodupes = listloaihanghoa.Distinct().ToList();
            foreach (var items in listloaihanghoanodupes)
            {
                listLoaiHangHoa.Add(new LoaiHangHoaModel(true, items.LoaiHangHoa));
            }
            SuaCommand = new RelayCommand<Window>((p) => { return true; }, p => { Sua(p); });
            ExitCommand = new RelayCommand<Window>((p) => { return true; }, p => { p.Close(); });
            this.TTHangHoa = hangHoa;
            this.hangHoa = new HANGHOA();
            this.hangHoa.MaHangHoa = TTHangHoa.MaHangHoa;
            this.hangHoa.DonGia = TTHangHoa.DonGia;
            this.hangHoa.TenHangHoa = TTHangHoa.TenHangHoa;
            this.hangHoa.LoaiHangHoa = TTHangHoa.LoaiHangHoa;
            this.hangHoa.TinhTrang = TTHangHoa.TinhTrang;
            this.hangHoa.SoLuongTonKho = TTHangHoa.SoLuongTonKho;
        }
        private void Sua(Window p)
        {
            if (hangHoa.TenHangHoa == String.Empty || TTHangHoa.TenHangHoa == null)
            {
                MessageBox.Show("Vui lòng nhập tên hàng hoá ! ", "Thông báo!", MessageBoxButton.OK);
                return;
            }

            if (hangHoa.DonGia.ToString() == String.Empty || TTHangHoa.DonGia.ToString() == null)
            {
                MessageBox.Show("Vui lòng nhập đơn giá! ", "Thông báo!", MessageBoxButton.OK);
                return;
            }
            if (hangHoa.TinhTrang == String.Empty || TTHangHoa.TinhTrang == null)
            {
                MessageBox.Show("Vui lòng nhập tình trạng hàng hoá! ", "Thông báo!", MessageBoxButton.OK);
                return;
            }
            if (hangHoa.SoLuongTonKho.ToString() == String.Empty || TTHangHoa.SoLuongTonKho.ToString() == null)
            {
                MessageBox.Show("Vui lòng nhập số lượng tồn kho! ", "Thông báo!", MessageBoxButton.OK);
                return;
            }
            if (hangHoa.LoaiHangHoa == String.Empty || TTHangHoa.LoaiHangHoa == null)
            {
                MessageBox.Show("Vui lòng nhập số lượng tồn kho! ", "Thông báo!", MessageBoxButton.OK);
                return;
            }

            hangHoa.TenHangHoa.ToString().Replace(" ", "");
            hangHoa.DonGia.ToString().Replace(" ", "");
            hangHoa.TinhTrang.ToString().Replace(" ", "");
            hangHoa.SoLuongTonKho.ToString().Replace(" ", "");
            hangHoa.LoaiHangHoa.ToString().Replace(" ", "");

            TTHangHoa.DonGia = hangHoa.DonGia;
            TTHangHoa.TenHangHoa = hangHoa.TenHangHoa;
            TTHangHoa.LoaiHangHoa = hangHoa.LoaiHangHoa;
            TTHangHoa.TinhTrang = hangHoa.TinhTrang;
            TTHangHoa.SoLuongTonKho = hangHoa.SoLuongTonKho;
            DataProvider.Ins.DB.SaveChanges();
            System.Windows.MessageBox.Show("Sửa thành công");

            p.Close();
        }
    }
}
