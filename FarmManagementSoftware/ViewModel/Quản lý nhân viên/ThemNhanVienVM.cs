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
using FarmManagementSoftware.Model;

namespace FarmManagementSoftware.ViewModel
{
    public class ThemNhanVienVM : BaseViewModel
    {
        private NHANVIEN nHANVIEN;
        public ICommand CloseCommand { get; set; }
        public ICommand ImageChangedCommand { get; set; }
        public ICommand ThemCommand { get; set; }
        public ObservableCollection<CHUCVU> listChucVu { get; set; }
        public CHUCVU chucvu { get; set; }
        public NHANVIEN newNhanVien { get; set; }
        private System.Windows.Media.Imaging.BitmapImage image;
        public System.Windows.Media.Imaging.BitmapImage MyImage { get => image; set { image = value; OnPropertyChanged(); } }

        public ThemNhanVienVM()
        {
            newNhanVien = new NHANVIEN();
            listChucVu = new ObservableCollection<CHUCVU>();
            chucvu = new CHUCVU();
            LoadListChucVu();
            ThemCommand = new RelayCommand<Window>((p) => { return true; }, p => { Them(p); });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, p => { p.Close(); });
            ImageChangedCommand = new RelayCommand<object>((p) => { return true; }, p => { ChangeImage(); });

        }
        private void LoadListChucVu()
        {
            var listchucvu = DataProvider.Ins.DB.CHUCVUs.ToList();
            foreach (var items in listchucvu)
                listChucVu.Add(items); 

        }
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static System.Windows.Media.Imaging.BitmapImage BytesToBitmapImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new System.Windows.Media.Imaging.BitmapImage();
            using (var mem = new System.IO.MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = System.Windows.Media.Imaging.BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
        void ChangeImage()
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter = "Image (*.jpg)|*.jpg";
            dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please select an image file to encrypt.";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(dialog.FileName);
                newNhanVien.BytesImage = (byte[])ImageToByteArray(bitmap);
                MyImage = BytesToBitmapImage(newNhanVien.BytesImage);
            }
        }
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
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
            int count = DataProvider.Ins.DB.NHANVIENs.Where(temp => temp.C_Username == newNhanVien.C_Username).Count();
            if(count > 0)
            {
                MessageBox.Show("Tên đăng nhập bị trùng, vui lòng chọn tên khác! ", "Thông báo!", MessageBoxButton.OK);
                return;
            }

            if (!IsValidEmail(newNhanVien.email) && !string.IsNullOrEmpty(newNhanVien.email))
            {
                MessageBox.Show("Địa chỉ email không hợp lệ! ", "Thông báo!", MessageBoxButton.OK);
                return;
            }


            if (newNhanVien.HeSoLuong == null)
            {
                newNhanVien.HeSoLuong = 0;
            }

            int val = 0;

            if (DataProvider.Ins.DB.NHANVIENs.Count() > 0)
            {
                string id = DataProvider.Ins.DB.NHANVIENs.ToList().Last().MaNhanVien.ToString();
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

            newNhanVien.MaNhanVien = "NV" + val.ToString("D6");


            newNhanVien.C_Username.ToString().Replace(" ", "");
            newNhanVien.NgayVaoLam = DateTime.Now;
            newNhanVien.MaChucVu = chucvu.MaChucVu;
            DataProvider.Ins.DB.NHANVIENs.Add(newNhanVien);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Thêm nhân viên mới thành công! ", "Thông báo!", MessageBoxButton.OK);
            newNhanVien = new NHANVIEN();
            p.Close();
        }
    }
}