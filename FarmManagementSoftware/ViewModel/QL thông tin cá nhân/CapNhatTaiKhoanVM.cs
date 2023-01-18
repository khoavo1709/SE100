using FarmManagementSoftware.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FarmManagementSoftware.ViewModel
{
    public class CapNhatTaiKhoanVM : BaseViewModel
    {
        #region Attributes
        private NHANVIEN nhanVien;
        List<string> lstGender;

        string hoTen;
        string diaChi;
        string email;
        double? heSoLuong;
        string gioiTinh;
        DateTime? ngayVaoLam;
        DateTime? ngaySinh;
        string sDT;
        System.Windows.Media.Imaging.BitmapImage image;
        byte[] imageBytes;

        #endregion

        #region Property
        public NHANVIEN NhanVien { get => nhanVien; set { nhanVien = value; OnPropertyChanged(); } }

        public List<string> LstGender { get => lstGender; set => lstGender = value; }


        public string HoTen { get => hoTen; set { hoTen = value; OnPropertyChanged(); } }
        public string DiaChi { get => diaChi; set { diaChi = value; OnPropertyChanged(); } }
        public string Email { get => email; set { email = value; OnPropertyChanged(); } }
        public double? HeSoLuong { get => heSoLuong; set { heSoLuong = value; OnPropertyChanged(); } }
        public DateTime? NgayVaoLam { get => ngayVaoLam; set { ngayVaoLam = value; OnPropertyChanged(); } }
        public DateTime? NgaySinh { get => ngaySinh; set { ngaySinh = value; OnPropertyChanged(); } }
        public string SDT { get => sDT; set { sDT = value; OnPropertyChanged(); } }
        public string GioiTinh { get => gioiTinh; set { gioiTinh = value; OnPropertyChanged(); } }
        public System.Windows.Media.Imaging.BitmapImage MyImage { get => image; set { image = value; OnPropertyChanged(); } }
        public string TenChucVu { get; set ; }
        public string HoTenGoc { get; set; }

        public MainWindowVM MainWindowMD { get; set; }

        #endregion

        #region Command
        public ICommand UpdateCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        #endregion

        #region Event command
        public ICommand HoTenChangedCommand { get; set; }
        public ICommand EmailChangedCommand { get; set; }
        public ICommand DiaChiChangedCommand { get; set; }
        public ICommand HeSoLuongChangedCommand { get; set; }
        public ICommand NgayVaoLamChangedCommand { get; set; }
        public ICommand NgaySinhChangedCommand { get; set; }
        public ICommand SDTChangedCommand { get; set; }
        public ICommand GioiTinhChangedCommand { get; set; }
        public ICommand ImageChangedCommand { get; set; }


        #endregion


        public CapNhatTaiKhoanVM()
        {
            
        }

        public CapNhatTaiKhoanVM(MainWindowVM mainWindowMD)
        {
            MainWindowMD = mainWindowMD;

            CreateEventCommand();

            LoadInformation();

            UpdateCommand = new RelayCommand<Button>((p) => { return CheckInforBeforUpdate(); }, p => { UpdateInformation(); });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, p => { CloseWindow(p); });
        }

        #region Method
        void CreateEventCommand()
        {
            HoTenChangedCommand = new RelayCommand<TextBox>((p) => { return true; }, p => { HoTen = p.Text; });
            EmailChangedCommand = new RelayCommand<TextBox>((p) => { return true; }, p => { Email = p.Text; });
            DiaChiChangedCommand = new RelayCommand<TextBox>((p) => { return true; }, p => { DiaChi = p.Text; });
            HeSoLuongChangedCommand = new RelayCommand<TextBox>((p) => { return true; }, p => { HeSoLuongChanged(p); });
            NgayVaoLamChangedCommand = new RelayCommand<DatePicker>((p) => { return true; }, p => { NgayVaoLam = p.SelectedDate; });
            NgaySinhChangedCommand = new RelayCommand<DatePicker>((p) => { return true; }, p => { NgaySinh = p.SelectedDate; });
            SDTChangedCommand = new RelayCommand<TextBox>((p) => { return true; }, p => { SDT = p.Text; });
            GioiTinhChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, p => { GioiTinhChanged(p); });
            ImageChangedCommand = new RelayCommand<object>((p) => { return true; }, p => { ChangeImage(); });
        }

        void GioiTinhChanged(ComboBox p)
        {
            if (p.SelectedItem == null) return;

            GioiTinh = p.SelectedItem.ToString();
        }

        void HeSoLuongChanged(TextBox p)
        {
            
            if (!string.IsNullOrWhiteSpace(p.Text)) { HeSoLuong = Int32.Parse(p.Text); } else { heSoLuong = -1; }
        }

        void LoadInformation()
        {
            if (MainWindowMD.NhanVien == null) return;

            HoTen = MainWindowMD.NhanVien.HoTen;
            SDT = MainWindowMD.NhanVien.SDT;
            DiaChi = MainWindowMD.NhanVien.DiaChi;
            Email = MainWindowMD.NhanVien.email;
            NgayVaoLam = MainWindowMD.NhanVien.NgayVaoLam;
            NgaySinh = MainWindowMD.NhanVien.NgaySinh;
            HeSoLuong = MainWindowMD.NhanVien.HeSoLuong;
            HoTenGoc = HoTen;
            MyImage = BytesToBitmapImage(MainWindowMD.NhanVien.BytesImage);
            TenChucVu = MainWindowMD.NhanVien.CHUCVU.TenChucVu;


            LstGender = new List<string>() { "Nam", "Nữ" };
            if (MainWindowMD.NhanVien.GioiTinh == "Nam")
            {
                GioiTinh = "Nam";
            }
            else
            {
                GioiTinh = "Nữ";
            }
        }
        void UpdateInformation()
        {
            if (!IsValidEmail(Email))
            {
                MessageBox.Show("Email không hợp lệ");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn thay đổi thông tin?", "Chú ý", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MainWindowMD.NhanVien.HoTen = HoTen;
                MainWindowMD.NhanVien.SDT = SDT;
                MainWindowMD.NhanVien.DiaChi = DiaChi;
                MainWindowMD.NhanVien.email = Email;
                MainWindowMD.NhanVien.NgayVaoLam = NgayVaoLam;
                MainWindowMD.NhanVien.NgaySinh = NgaySinh;
                MainWindowMD.NhanVien.HeSoLuong = HeSoLuong;
                MainWindowMD.NhanVien.GioiTinh = GioiTinh;
                MainWindowMD.NhanVien.BytesImage = imageBytes;
                MainWindowMD.MyImage = MyImage;
                DataProvider.Ins.DB.SaveChanges();
                MainWindowMD.UpdateNhanVien();
                MessageBox.Show("Thay đổi thành công");
            }
            else
            {

            }
        }

        bool CheckInforBeforUpdate()
        {
            if (string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(diaChi) || string.IsNullOrWhiteSpace(gioiTinh) || string.IsNullOrWhiteSpace(sDT) || string.IsNullOrWhiteSpace(ngayVaoLam.ToString()) || string.IsNullOrWhiteSpace(ngaySinh.ToString()) || HeSoLuong == -1 )
            {
                return false;
            }

            if (email.Contains(" "))
            {
                return false;
            }

            return true;
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
                imageBytes = ImageToByteArray(bitmap);
                MyImage = BytesToBitmapImage(imageBytes);
            }
        }

        void CloseWindow(Window p)
        {
            MessageBoxResult dlr = MessageBox.Show("Bạn muốn thoát chương trình?", "Thông báo", MessageBoxButton.YesNo);
            if (dlr == MessageBoxResult.Yes)
            {
                p.Close();
                return;
            }
            return;
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

        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}