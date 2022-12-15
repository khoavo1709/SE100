using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyTraiHeo.Model;

namespace QuanLyTraiHeo.ViewModel
{
    public class SuaChucVuVM :BaseViewModel
    {
        public ICommand SuaCommand { get; set; }

        public ObservableCollection<PERMISION> listPermission { get; set; }
        public CHUCVU ChucVu { get; set; }

        public string TenChucVu { get; set; }
        public int HeSoLuong { get; set; }
        public string Mota { get; set; }
        public PERMISION permission { get; set; }
        public SuaChucVuVM()
        {
        }

        public SuaChucVuVM(CHUCVU chucvu)
        {
            ChucVu = chucvu;
            TenChucVu = ChucVu.TenChucVu;
            HeSoLuong = Convert.ToInt32( ChucVu.LuongCoBan);
            permission = ChucVu.PERMISION;
            Mota = chucvu.MoTa;
            listPermission = new ObservableCollection<PERMISION>();
            listPermission.Clear();
            var list = DataProvider.Ins.DB.PERMISIONs.ToList();
            foreach (var items in list)
                listPermission.Add(items);
            SuaCommand = new RelayCommand<Window>((p) => { return true; }, p => {  Sua( p); });
        }
        private void Sua(Window p)
        {
            if(TenChucVu == String.Empty)
            {
                MessageBox.Show("Tên chức vụ trống !");
                return;
            }
            try
            {
                Convert.ToInt32(HeSoLuong);
            }
            catch(Exception)
            {
                MessageBox.Show("Lương cơ bản không hợp lệ");
                return;
            }
            if (HeSoLuong < 0) {
                MessageBox.Show("Lương cơ bản không hợp lệ");
            return ;
            }
            ChucVu.TenChucVu = TenChucVu;
            ChucVu.LuongCoBan = HeSoLuong;
            ChucVu.ID_Permision = permission.ID_Permision;
            ChucVu.MoTa = Mota;
            DataProvider.Ins.DB.SaveChanges();

            MessageBox.Show("Sửa thành công !");
            p.Close();
        }
    }
}
