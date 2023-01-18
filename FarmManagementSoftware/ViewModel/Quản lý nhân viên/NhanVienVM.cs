﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Navigation;
using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.Windows.Quản_lý_nhân_viên;
using ListView = System.Windows.Controls.ListView;
using MessageBox = System.Windows.MessageBox;

namespace FarmManagementSoftware.ViewModel
{
    public class NhanVienVM:BaseViewModel
    {
        #region Atributies
        public ObservableCollection<NHANVIEN> lstNhanvien { get; set; }
        public ObservableCollection<ChucVuModel> lstChucVu { get; set; }
        private int listviewSelectedIndex { get; set; }
        private string textTimKiem { get; set; }

        #endregion


        #region Properties
        public int ListViewSelectedIndex { get => listviewSelectedIndex; set { listviewSelectedIndex = value; OnPropertyChanged(); } }
        public string TextTimKiem { get => textTimKiem; set { textTimKiem = value; OnPropertyChanged(); } }
        #endregion


        #region EventCommand
        public ICommand ThemNhanVienCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand TextTimKiemChangeCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion


        public NhanVienVM()
        {
            #region Initial Atributes
            TextTimKiem = "";
            lstNhanvien = new ObservableCollection<NHANVIEN>();
            lstChucVu = new ObservableCollection<ChucVuModel>();
            ListViewSelectedIndex = 0;
            #endregion

            #region Initial Command
            ThemNhanVienCommand = new RelayCommand<Window>((p) => { return true; }, p => { ThemNhanVien(p); });
            EditCommand = new RelayCommand<Window>((p) => { return true; }, p => { Edit(p); });
            TextTimKiemChangeCommand = new RelayCommand<ListView>((p) => { return true; }, p => { TextTimKiemChanged(p); });
            DeleteCommand = new RelayCommand<Window>((p) => { return true; }, p => { Delete(); });

            #endregion

            #region LoadData
            LoadListNhanVien();
            LoadListChucVu();
            #endregion
        }

        #region Methods
        public void ThemNhanVien(Window p)
        {
            ThemNhanVien themNhanVien = new ThemNhanVien();
            themNhanVien.ShowDialog();
            LoadListNhanVien();
        }

        private void TextTimKiemChanged(ListView p)
        {
            LoadListNhanVien(p);
            
        }
        private void LoadListNhanVien(ListView p  = null)
        {
            lstNhanvien.Clear();

            var listnhanvien = DataProvider.Ins.DB.NHANVIENs.Where(s => s.HoTen.Contains(TextTimKiem) && s.TrangThai != "Đã ẩn").ToList();
            foreach (var items in listnhanvien)
            {
                int flag = 0;
                foreach(var items2 in lstChucVu)
                {
                    if (items2.isSelected == false)
                        if (items.CHUCVU.TenChucVu == items2.TenChucVu)
                        {
                            flag = 1;
                            break;
                        }
                }
                if(flag == 0)
                lstNhanvien.Add(items);
            }
        }

        void LoadListChucVu()
        {
            lstChucVu.Clear();

            var listchucvu = DataProvider.Ins.DB.CHUCVUs.ToList();
            foreach (var items in listchucvu)
                lstChucVu.Add(new ChucVuModel(true, items.TenChucVu));

        }
        public void Delete()
        {
            if (ListViewSelectedIndex < 0)
                return;
            if (MessageBox.Show("Bạn có chắc muốn xóa nhân viên " + lstNhanvien[ListViewSelectedIndex].HoTen + " ?", "Chú ý", 
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                lstNhanvien[ListViewSelectedIndex].TrangThai = "Đã ẩn";
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Đã xóa thông tin nhân viên ! ", "Thông báo!", MessageBoxButton.OK);
                LoadListNhanVien();
            }
        }

        public void Edit(Window p)
        {
            if(ListViewSelectedIndex <0)
                return;
            SuaNhanVienVM suaNhanVienVM = new SuaNhanVienVM(lstNhanvien[ListViewSelectedIndex]);
            SuaNhanVien suaNhanVien = new SuaNhanVien();
            suaNhanVien.DataContext = suaNhanVienVM;
            suaNhanVien.ShowDialog();
            LoadListNhanVien();
        }
        #endregion
    }
}
