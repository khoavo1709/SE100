using FarmManagementSoftware.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace FarmManagementSoftware.ViewModel
{
    public class SuaNhanVienVM : BaseViewModel
    {
        public ICommand SuaCommand { get; set; }
        public ObservableCollection<CHUCVU> listChucVu { get; set; }
        public CHUCVU chucvu { get; set; }
        public NHANVIEN TTNhanVien { get; set; }

        public SuaNhanVienVM()
        {
            System.Windows.MessageBox.Show("Khởi tạo không tham số, BUG !!!!");
        }

        public SuaNhanVienVM(NHANVIEN nhanVien)
        {
            listChucVu = new ObservableCollection<CHUCVU>();
            LoadListChucVu();
            SuaCommand = new RelayCommand<Window>((p) => { return true; }, p => { Sua(p); });

            TTNhanVien = nhanVien;
            chucvu = nhanVien.CHUCVU;

        }
        private void LoadListChucVu()
        {
            var listchucvu = DataProvider.Ins.DB.CHUCVUs.ToList();
            foreach (var items in listchucvu)
                listChucVu.Add(items);

        }
        private void Sua(Window p)
        {
            if (TTNhanVien.HoTen == String.Empty || TTNhanVien.HoTen == null)
            {
                MessageBox.Show("Vui lòng nhập họ tên ! ", "Thông báo!", MessageBoxButton.OK);
                return;
            }

            if (TTNhanVien.C_Username == String.Empty || TTNhanVien.C_Username == null)
            {
                MessageBox.Show("Vui lòng nhập Tên đăng nhập! ", "Thông báo!", MessageBoxButton.OK);
                return;
            }
            try { Convert.ToInt32(TTNhanVien.HeSoLuong); }
            catch
            {
                MessageBox.Show("Vui lòng đúng thông tin! ", "Thông báo!", MessageBoxButton.OK);
                return;
            }
            TTNhanVien.MaChucVu = chucvu.MaChucVu;

            TTNhanVien.HoTen.ToString().Replace(" ", "");
            TTNhanVien.C_Username.ToString().Replace(" ", "");


            DataProvider.Ins.DB.SaveChanges();
            System.Windows.MessageBox.Show("Sửa thành công");

            p.Close();

        }

    }
}
