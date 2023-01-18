using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.Windows.Quản_lý_chức_vụ;

namespace FarmManagementSoftware.ViewModel
{
    public class ChucVuVM : BaseViewModel
    {
        #region Atributes
        private int listviewSelectedIndex { get; set; }
        private CHUCVU newChucVu { get; set; }
        private string textTimKiem { get; set; }
        #endregion


        #region Properties
        public CHUCVU NewChucVu             { get => newChucVu; set { newChucVu = value; OnPropertyChanged(); } }
        public int  ListViewSelectedIndex   { get => listviewSelectedIndex; set { listviewSelectedIndex = value; OnPropertyChanged(); } }
        public string TextTimKiem           { get => textTimKiem; set { textTimKiem = value; OnPropertyChanged(); } }
        public ObservableCollection<CHUCVU> lstChucVu { get; set; }
        public ObservableCollection<PERMISION> lstPermission { get; set; }

        #endregion


        #region EventCommand
        public ICommand themCommand { get; set; }
        public ICommand TextTimKiemChangeCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand EditPermissionCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        #endregion


        public ChucVuVM()
        {
            #region Initial attributes

            textTimKiem = "";
            lstChucVu = new ObservableCollection<CHUCVU>();
            lstPermission = new ObservableCollection<PERMISION>();
            NewChucVu = new CHUCVU();
            NewChucVu.MaChucVu = "";
            NewChucVu.TenChucVu = "";
            NewChucVu.LuongCoBan = 0;
            NewChucVu.PERMISION = null;
            #endregion

            #region Initial commands
            themCommand = new RelayCommand<Grid>((p) => { return true; }, p => { ThemChucVu(); });
            EditCommand = new RelayCommand<Window>((p) => { return true; }, p => { Edit(p); });
            EditPermissionCommand = new RelayCommand<Window>((p) => { return true; }, p => { OpenPhanQuyenWindow(); });
            DeleteCommand  = new RelayCommand<Window>((p) => { return true; }, p => { Delete(); });

            TextTimKiemChangeCommand = new RelayCommand<ListView>((p) => { return true; }, p =>
            {
                LoadlstChucVu();
            });
            #endregion

            #region LoadData
            LoadlstPermission();
            LoadlstChucVu();
            #endregion
        }
        #region Methods
        private void LoadlstChucVu()
        {
            lstChucVu.Clear();

            var list = DataProvider.Ins.DB.CHUCVUs.Where(s => s.TenChucVu.Contains(textTimKiem)).ToList();
            foreach (var items in list)
                lstChucVu.Add(items);

        }
        private void ThemChucVu()
        {
            if (NewChucVu.TenChucVu == String.Empty || NewChucVu.TenChucVu == "")
            {
                MessageBox.Show("Vui lòng nhập tên chức vụ");
                return;
            }
            if (NewChucVu.PERMISION == null)
            {
                MessageBox.Show("Vui lòng chọn quyền ");
                return;
            }
            try { Convert.ToInt32(NewChucVu.LuongCoBan); }
            catch
            {
                MessageBox.Show("Vui lòng đúng thông tin! ", "Thông báo!", MessageBoxButton.OK);
                return;
            }

            int val = 0;

            if (DataProvider.Ins.DB.CHUCVUs.Count() >0)
            {
                string id = DataProvider.Ins.DB.CHUCVUs.ToList().Last().MaChucVu.ToString();
                string b = "";
                for (int i = 0; i < id.Length; i++)
                {
                    if (Char.IsDigit(id[i]))
                        b += id[i];
                }

                if (b.Length > 0)
                    val = int.Parse(b);
                val += 1;
            }

            NewChucVu.MaChucVu = "CV" + val.ToString("D6");

            NewChucVu.ID_Permision = NewChucVu.PERMISION.ID_Permision;
            DataProvider.Ins.DB.CHUCVUs.Add(NewChucVu);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Thêm nhân viên mới thành công! ", "Thông báo!", MessageBoxButton.OK);

            NewChucVu = new CHUCVU();
            NewChucVu.TenChucVu = "";
            NewChucVu.LuongCoBan = 0;
            NewChucVu.PERMISION = null;
            LoadlstChucVu();
        }
        private void LoadlstPermission()
        {

            lstPermission.Clear();

            var list = DataProvider.Ins.DB.PERMISIONs.ToList();
            foreach (var items in list)
                lstPermission.Add(items);

        }
        public void Edit(Window p)
        {
            if (listviewSelectedIndex < 0)
                return;
            SuaChucVu suaChucVuW = new SuaChucVu();

            SuaChucVuVM suaChucVuVM = new SuaChucVuVM(lstChucVu[listviewSelectedIndex]);
            suaChucVuW.DataContext = suaChucVuVM;
            suaChucVuW.ShowDialog();
            LoadlstChucVu();
        }
        private void   Delete()
        {
            CHUCVU temp = lstChucVu[listviewSelectedIndex];

            if (MessageBox.Show("Bạn có chắc muốn xóa chức vụ "+ temp.TenChucVu +" ?", "Chú ý", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {


                int count = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaChucVu == temp.MaChucVu).Count();
                if (count > 0)
                {
                    MessageBox.Show("Đang có nhân viên giữ chức vụ này, không thể xóa!");
                    return;
                }


                DataProvider.Ins.DB.CHUCVUs.Remove(temp);

                DataProvider.Ins.DB.SaveChanges();
                LoadlstChucVu();
                MessageBox.Show("Xóa thành công !");
            }

        }
        private void OpenPhanQuyenWindow()
        {
            PhanQuyenWindow temp  = new PhanQuyenWindow();

           temp.ShowDialog();
            LoadlstPermission();
        }
        #endregion

    }
}
