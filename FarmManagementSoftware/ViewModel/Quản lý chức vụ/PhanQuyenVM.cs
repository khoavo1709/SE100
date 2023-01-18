using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
using FarmManagementSoftware.Model;
using MessageBox = System.Windows.MessageBox;

namespace FarmManagementSoftware.ViewModel
{
    public class PhanQuyenVM : BaseViewModel
    {
        private PERMISION modifyPermission { get; set; }
        public PERMISION ModifyPermission { get => modifyPermission; set { modifyPermission = value; OnPropertyChanged(); } }
        private string permissionName { get; set; }
        public string PermissionName { get => permissionName; set { permissionName = value; OnPropertyChanged(); } }
        public ObservableCollection<PERMISION> lstPermission { get; set; }
        public ICommand ChinhSuaPermissionCommand { get; set; }
        public ICommand permissionSelectionChangedCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddPerCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public ObservableCollection<PermissionModel> permissionModels { get; set; }
        
        public int lstPermissionIndex { get; set; }
        public PhanQuyenVM()
        {
            permissionModels = new ObservableCollection<PermissionModel>();
            lstPermission = new ObservableCollection<PERMISION>();
            ChinhSuaPermissionCommand = new RelayCommand<Button>((p) => { return true; }, p => { ChinhSuaPermission(); });
            permissionSelectionChangedCommand = new RelayCommand<Button>((p) => { return true; }, p => { PermissionSelectionChanged(); });
            DeleteCommand = new RelayCommand<Button>((p) => { return true; }, p => { DeletePermission(); });
            AddPerCommand = new RelayCommand<Button>((p) => { return true; }, p => { AddPermission(); });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, p => { p.Close(); });


            LoadlstPermission();

            permissionModels.Clear();
            permissionModels.Add(new PermissionModel(false, "Quản lý nhân viên", 1));
            permissionModels.Add(new PermissionModel(false, "Quản lý đàn heo", 2));
            permissionModels.Add(new PermissionModel(false, "Quản lý kho", 3));
            permissionModels.Add(new PermissionModel(false, "Quản lý tài chính", 4));
            permissionModels.Add(new PermissionModel(false, "Quản lý cây mục tiêu", 5));
            permissionModels.Add(new PermissionModel(false, "Quản lý nhật ký", 6));
        }
        private void LoadlstPermission()
        {

            lstPermission.Clear();

            var list = DataProvider.Ins.DB.PERMISIONs.ToList();
            foreach (var items in list)
                lstPermission.Add(items);

        }

        private void ChinhSuaPermission()
        {
            if (lstPermissionIndex < 0 || lstPermissionIndex >= lstPermission.Count())
                return;
            ModifyPermission = lstPermission[lstPermissionIndex];
            if (PermissionName == String.Empty)
            {
                MessageBox.Show("Vui lòng điền tên chức vụ!");
                return;
            }
            int count = DataProvider.Ins.DB.PERMISIONs.Where(p => (p.Name_Permision == PermissionName && ModifyPermission.ID_Permision != p.ID_Permision)).Count();
            if(count > 0)
            {
                MessageBox.Show("Tên chức vụ bị trùng, vui lòng chọn tên khác!");
                return;
            }

            List<PERMISION_DETAIL> tasdasda = ModifyPermission.PERMISION_DETAIL.ToList();

            foreach(var item in tasdasda)
            {
                DataProvider.Ins.DB.PERMISION_DETAIL.Remove(item);
            }
            //DataProvider.Ins.DB.PERMISION_DETAIL.RemoveRange(DataProvider.Ins.DB.PERMISION_DETAIL.Where(x => x.ID_Permision == ModifyPermission.ID_Permision));
            DataProvider.Ins.DB.SaveChanges();
           
            int val = 0;

            if (DataProvider.Ins.DB.PERMISION_DETAIL.Count() > 0)
            {
                string id = DataProvider.Ins.DB.PERMISION_DETAIL.ToList().Last().ID_PermisionDetail.ToString();
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

            foreach (var item in permissionModels)
                if (item.isSelected)
                {
                    PERMISION_DETAIL pERMISION_DETAIL = new PERMISION_DETAIL();
                    pERMISION_DETAIL.ID_PermisionDetail = "PD" + val.ToString("D6");
                    pERMISION_DETAIL.ActionDetail = item.ActionDetail;
                    pERMISION_DETAIL.ID_Permision = ModifyPermission.ID_Permision;
                    DataProvider.Ins.DB.PERMISION_DETAIL.Add(pERMISION_DETAIL);
                    val++;
                }
            var aaaaaaaaaaaaaaa = DataProvider.Ins.DB.PERMISION_DETAIL.Where(x => x.ID_Permision == ModifyPermission.ID_Permision);

            DataProvider.Ins.DB.SaveChanges();
            ModifyPermission = lstPermission[lstPermissionIndex];
            MessageBox.Show("Chỉnh sửa thành công!");
        }

        private void DeletePermission()
        {

            int count = DataProvider.Ins.DB.CHUCVUs.Where(x => x.ID_Permision == ModifyPermission.ID_Permision).Count();
            if(count > 0)
            {
                MessageBox.Show("Đang có chức vụ có quyền này, không thể xóa!");
                return;
            }


            DataProvider.Ins.DB.PERMISION_DETAIL.RemoveRange(DataProvider.Ins.DB.PERMISION_DETAIL.Where(x => x.ID_Permision == ModifyPermission.ID_Permision));
            DataProvider.Ins.DB.PERMISIONs.Remove( ModifyPermission);

            DataProvider.Ins.DB.SaveChanges();
            LoadlstPermission();
            MessageBox.Show("Xóa thành công !");

        }

        private void AddPermission()
        {
            if (PermissionName == String.Empty)
            {
                MessageBox.Show("Vui lòng điền tên chức vụ!");
                return;
            }
            int count = DataProvider.Ins.DB.PERMISIONs.Where(p => (p.Name_Permision == PermissionName)).Count();
            if (count > 0)
            {
                System.Windows.MessageBox.Show("Tên chức vụ bị trùng, vui lòng chọn tên khác!");
                return;
            }

            int val = 0;

            if (DataProvider.Ins.DB.PERMISIONs.Count() > 0)
            {
                string id = DataProvider.Ins.DB.PERMISIONs.ToList().Last().ID_Permision.ToString();
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

            PERMISION temp = new PERMISION();

            temp.ID_Permision = "Per" + val.ToString("D2");

            temp.Name_Permision = PermissionName;
            DataProvider.Ins.DB.PERMISIONs.Add(temp);
            DataProvider.Ins.DB.SaveChanges();

            LoadlstPermission();
            MessageBox.Show("Thêm thành công !");
        }
        private void PermissionSelectionChanged()
        {
            if (ModifyPermission == null)
            {
                PermissionName = "";
                permissionModels.Clear();
                permissionModels.Add(new PermissionModel(false, "Quản lý nhân viên", 1));
                permissionModels.Add(new PermissionModel(false, "Quản lý đàn heo", 2));
                permissionModels.Add(new PermissionModel(false, "Quản lý kho", 3));
                permissionModels.Add(new PermissionModel(false, "Quản lý tài chính", 4));
                permissionModels.Add(new PermissionModel(false, "Quản lý cây mục tiêu", 5));
                permissionModels.Add(new PermissionModel(false, "Quản lý nhật ký", 6));

                return;
            }
            PermissionName = ModifyPermission.Name_Permision;
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
    }
}
