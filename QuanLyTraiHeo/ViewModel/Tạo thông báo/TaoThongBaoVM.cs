using QuanLyTraiHeo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using QuanLyTraiHeo.View.Windows;
using System.Security.Cryptography.Pkcs;

namespace QuanLyTraiHeo.ViewModel
{
    public class TaoThongBaoVM: BaseViewModel
    {
        #region Attributes
        public NHANVIEN nguoigui;
        private CHUCVU _selectCHUCVU;
        private ObservableCollection<CHUCVU> _listCHUCVU;
        private ObservableCollection<NHANVIEN> _listNGUOIGUI;
        private string _txtNGUOIGUI;
        private string _Tieude;
        private string _Noidung;
        #endregion

        #region Property
        public CHUCVU selectCHUCVU { get => _selectCHUCVU; set { _selectCHUCVU = value; OnPropertyChanged(); } }
        public ObservableCollection<CHUCVU> listCHUCVU { get => _listCHUCVU; set { _listCHUCVU = value; OnPropertyChanged(); } }
        public ObservableCollection<NHANVIEN> ListNGUOIGUI { get => _listNGUOIGUI; set { _listNGUOIGUI = value; OnPropertyChanged(); }  }
        public string txtNGUOIGUI { get => _txtNGUOIGUI; set { _txtNGUOIGUI = value; OnPropertyChanged(); } }
        public string Tieude { get => _Tieude; set { _Tieude = value; OnPropertyChanged(); } }
        public string Noidung { get => _Noidung; set { _Noidung = value; OnPropertyChanged(); } }

        #endregion

        #region CommandOpenWindow
        public ICommand ChonNguoiGuiCommand { get; set; }
        public ICommand AddThongBao { get; set; }
        public ICommand HuyBoBtnCommand { get; set; }
        #endregion
        public TaoThongBaoVM()
        {
            txtNGUOIGUI = "";
            Tieude = "";
            Noidung = "";
            ListNGUOIGUI = new ObservableCollection<NHANVIEN>();
            #region tạo list CHUCVU
            listCHUCVU = new ObservableCollection<CHUCVU>(DataProvider.Ins.DB.CHUCVUs);
            CHUCVU chucvu = new CHUCVU();
            chucvu.TenChucVu = "Tất cả";
            listCHUCVU.Add(chucvu);
            #endregion

            #region code button chọn người gửi
            ChonNguoiGuiCommand = new RelayCommand<Grid>((p) => { return true; }, p => {
                ChonNguoiGui wc = new ChonNguoiGui();
                if(selectCHUCVU == null)
                {
                    MessageBox.Show("Bạn phải chọn bộ phận để gửi trước!!!");
                    return;
                }    
                ChonNguoiGuiVM vm = new ChonNguoiGuiVM(this);
                wc.DataContext = vm;
                wc.ShowDialog();
            });
            #endregion

            #region code tạo thông báo
            AddThongBao = new RelayCommand<Window>(
                (p) => {
                    if (ListNGUOIGUI.Count < 1)
                        return false;
                    if (Tieude == "")
                        return false;
                    if (Noidung == "")
                        return false;
                    return true; 
                }, 
                p => {
                    foreach(var nguoinhan in ListNGUOIGUI)
                    {
                        ThongBao thongbao = new ThongBao();
                        thongbao.MaThongBao = TaoMaThongBao();
                        thongbao.C_MaNguoiGui = nguoigui.MaNhanVien;
                        thongbao.C_MaNguoiNhan = nguoinhan.MaNhanVien;
                        thongbao.TinhTrang = "Chưa đọc";
                        thongbao.ThoiGian = DateTime.Now;
                        thongbao.TieuDe = Tieude;
                        thongbao.NoiDung = Noidung;

                        DataProvider.Ins.DB.ThongBaos.Add(thongbao);
                        DataProvider.Ins.DB.SaveChanges();
                    }

                    p.Close();
                }
            );
            #endregion

            #region code btn Huỷ bỏ
            HuyBoBtnCommand = new RelayCommand<Window>((p) => { return true; }, p => {

                MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn huỷ bỏ", "Thông báo", MessageBoxButton.OKCancel);
                if(result == MessageBoxResult.OK)
                {
                    p.Close();
                }
                return;

            });
            #endregion

        }
        public void loadTxtNGUOIGUI()
        {
            string text = "";
            foreach(var nguoigui in ListNGUOIGUI)
            {
                text += nguoigui.MaNhanVien + " ";
            }
            txtNGUOIGUI = text;
        }
        
        string TaoMaThongBao()
        {
            string maTB = "";
            ObservableCollection<ThongBao> thongBaos = new ObservableCollection<ThongBao>(DataProvider.Ins.DB.ThongBaos);
            var soThongBao = thongBaos.Count;
            //var lastThongBao = DataProvider.Ins.DB.ThongBaos.First();
            if (soThongBao == 0)
            {
                maTB = "TB000001";
            }
            else
            {
                int STT = soThongBao;
                do
                {
                    STT++;
                    string strSTT = STT.ToString();
                    for (int i = strSTT.Length; i <= 6; i++)
                    {
                        strSTT = "0" + strSTT;
                    }

                    maTB = "TB" + strSTT;
                }
                while (DataProvider.Ins.DB.ThongBaos.Count(x => x.MaThongBao == maTB) > 0);
            }
            return maTB;
        }
    }
}
