using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyTraiHeo.Model;

namespace QuanLyTraiHeo.ViewModel
{
    public class ThemNhanVienVM : BaseViewModel
    {
        private NHANVIEN nHANVIEN;

        public ICommand ThemCommand { get; set; }
        public ObservableCollection<CHUCVU> listChucVu { get; set; }
        public CHUCVU chucvu { get; set; }
        public NHANVIEN newNhanVien { get; set; }
        public ThemNhanVienVM()
        {
            newNhanVien = new NHANVIEN();
            listChucVu = new ObservableCollection<CHUCVU>();
            chucvu = new CHUCVU();
            LoadListChucVu();
            ThemCommand = new RelayCommand<Window>((p) => { return true; }, p => { Them(p); });

        }
        private void LoadListChucVu()
        {
            var listchucvu = DataProvider.Ins.DB.CHUCVUs.ToList();
            foreach (var items in listchucvu)
                listChucVu.Add(items); 

        }
        private void Them(Window p)
        {
            if (newNhanVien.HoTen == String.Empty || newNhanVien.HoTen == null)
            {
                MessageBox.Show("Vui lòng nhập họ tên ! ","Thông báo!",MessageBoxButton.OK);
                return;
            }
            if (newNhanVien.C_Username == String.Empty || newNhanVien.C_Username == null)
            {
                MessageBox.Show("Vui lòng nhập Tên đăng nhập! ", "Thông báo!", MessageBoxButton.OK);
                return;
            }
            if (newNhanVien.HeSoLuong == null)
            {
                newNhanVien.HeSoLuong = 0;
                return;
            }
            newNhanVien.HoTen.ToString().Replace(" ", "");
            newNhanVien.C_Username.ToString().Replace(" ", "");

            newNhanVien.MaChucVu = chucvu.MaChucVu;
            newNhanVien.MaNhanVien = ("NV" + DataProvider.Ins.DB.NHANVIENs.Count().ToString()).Replace(" ","");
            DataProvider.Ins.DB.NHANVIENs.Add(newNhanVien);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Thêm nhân viên mới thành công! ", "Thông báo!", MessageBoxButton.OK);
            newNhanVien = new NHANVIEN();
            p.Close();
        }
    }
}