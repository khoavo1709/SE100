using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyTraiHeo.Model;
using QuanLyTraiHeo.View.Windows.Quản_lý_chức_vụ;

namespace QuanLyTraiHeo.ViewModel
{
    public class ChucVuVM : BaseViewModel
    {
        #region Atributes
        private int listviewSelectedIndex { get; set; }
        public PERMISION modifyPermission { get; set; }
        private CHUCVU newChucVu { get; set; }
        private string textTimKiem { get; set; }
        private string permissionName { get; set; }
        #endregion


        #region Properties
        public CHUCVU NewChucVu             { get => newChucVu; set { newChucVu = value; OnPropertyChanged(); } }
        public PERMISION ModifyPermission   { get => modifyPermission; set { modifyPermission = value; OnPropertyChanged(); } }
        public int  ListViewSelectedIndex   { get => listviewSelectedIndex; set { listviewSelectedIndex = value; OnPropertyChanged(); } }
        public string PermissionName        { get => permissionName; set { permissionName = value; OnPropertyChanged(); } }
        public string TextTimKiem           { get => textTimKiem; set { textTimKiem = value; OnPropertyChanged(); } }
        public ObservableCollection<CHUCVU> lstChucVu { get; set; }
        public ObservableCollection<PERMISION> lstPermission { get; set; }
        public ObservableCollection<PermissionModel> permissionModels { get; set; }

        #endregion


        #region EventCommand
        public ICommand themCommand { get; set; }
        public ICommand TextTimKiemChangeCommand { get; set; }
        public ICommand ChinhSuaPermissionCommand { get; set; }
        public ICommand permissionSelectionChangedCommand { get; set; }
        public ICommand EditCommand { get; set; }
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
            ModifyPermission = new PERMISION();
            #endregion

            #region Initial commands
            themCommand = new RelayCommand<Grid>((p) => { return true; }, p => { ThemChucVu(); });
            EditCommand = new RelayCommand<Window>((p) => { return true; }, p => { Edit(p); });
            ChinhSuaPermissionCommand = new RelayCommand<Button>((p) => { return true; }, p => { ChinhSuaPermission(); });
            permissionSelectionChangedCommand = new RelayCommand<Button>((p) => { return true; }, p => { PermissionSelectionChanged(); });


            TextTimKiemChangeCommand = new RelayCommand<ListView>((p) => { return true; }, p =>
            {
                LoadlstChucVu();
            });
            #endregion

            #region LoadData
            LoadlstPermission();
            LoadlstChucVu();
            permissionModels = new ObservableCollection<PermissionModel>();
            permissionModels.Add(new PermissionModel(false, "Quản lý nhân viên", 1));
            permissionModels.Add(new PermissionModel(false, "Quản lý đàn heo", 2));
            permissionModels.Add(new PermissionModel(false, "Quản lý kho", 3));
            permissionModels.Add(new PermissionModel(false, "Quản lý tài chính", 4));
            permissionModels.Add(new PermissionModel(false, "Quản lý cây mục tiêu", 5));
            permissionModels.Add(new PermissionModel(false, "Quản lý nhật ký", 6));
            #endregion
        }
        
        #region Methods
        private void ChinhSuaPermission()
        {
            if (PermissionName == String.Empty)
            {
                MessageBox.Show("Vui lòng điền tên chức vụ!");
                return;
            }
            if (ModifyPermission == null)
            {

                ModifyPermission = new PERMISION();
                ModifyPermission.ID_Permision = "Per" + lstPermission.Count.ToString();
                ModifyPermission.Name_Permision = PermissionName;
                DataProvider.Ins.DB.PERMISIONs.Add(ModifyPermission);
            }
            else if (ModifyPermission.Name_Permision != PermissionName)
            {

                ModifyPermission = new PERMISION();
                ModifyPermission.ID_Permision = "Per" + lstPermission.Count.ToString();
                ModifyPermission.Name_Permision = PermissionName;
                DataProvider.Ins.DB.PERMISIONs.Add(ModifyPermission);

            }

            DataProvider.Ins.DB.SaveChanges();


            DataProvider.Ins.DB.PERMISION_DETAIL.RemoveRange(DataProvider.Ins.DB.PERMISION_DETAIL.Where(x => x.ID_Permision == ModifyPermission.ID_Permision));
            DataProvider.Ins.DB.SaveChanges();


            foreach (var item in permissionModels)
                if (item.isSelected)
                {
                    PERMISION_DETAIL pERMISION_DETAIL = new PERMISION_DETAIL();
                    pERMISION_DETAIL.ID_PermisionDetail = ("PD" + ModifyPermission.ID_Permision + item.number.ToString()).ToString().Replace(" ", "");
                    pERMISION_DETAIL.ActionDetail = item.ActionDetail;
                    pERMISION_DETAIL.ID_Permision = ModifyPermission.ID_Permision;
                    MessageBox.Show(pERMISION_DETAIL.ID_PermisionDetail);
                    DataProvider.Ins.DB.PERMISION_DETAIL.Add(pERMISION_DETAIL);
                }

            DataProvider.Ins.DB.SaveChanges();
            LoadlstPermission();

        }
        private void LoadlstChucVu()
        {
            lstChucVu.Clear();

            var list = DataProvider.Ins.DB.CHUCVUs.Where(s => s.TenChucVu.Contains(textTimKiem)).ToList();
            foreach (var items in list)
                lstChucVu.Add(items);

        }
        private void PermissionSelectionChanged()
        {
            if (ModifyPermission == null)
                return;
            permissionModels.Clear();
            permissionModels.Add(new PermissionModel(false, "Quản lý nhân viên", 1));
            permissionModels.Add(new PermissionModel(false, "Quản lý đàn heo", 2));
            permissionModels.Add(new PermissionModel(false, "Quản lý kho", 3));
            permissionModels.Add(new PermissionModel(false, "Quản lý tài chính", 4));
            permissionModels.Add(new PermissionModel(false, "Quản lý cây mục tiêu", 5));
            permissionModels.Add(new PermissionModel(false, "Quản lý nhật ký", 6));
            foreach (var item in ModifyPermission.PERMISION_DETAIL)
                foreach (var item2 in permissionModels)
                    if (item.ActionDetail == item2.ActionDetail)
                    {
                        item2.isSelected = true;
                    }
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
            NewChucVu.MaChucVu = ("CV" + DataProvider.Ins.DB.CHUCVUs.Count().ToString()).Replace(" ", "");
            NewChucVu.ID_Permision = NewChucVu.PERMISION.ID_Permision;
            DataProvider.Ins.DB.CHUCVUs.Add(NewChucVu);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Thêm nhân viên mới thành công! ", "Thông báo!", MessageBoxButton.OK);

            NewChucVu = new CHUCVU();
            NewChucVu.MaChucVu = "";
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
        #endregion

    }
}
