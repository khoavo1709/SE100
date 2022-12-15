using QuanLyTraiHeo.Model;
using QuanLyTraiHeo.View.Windows;
using QuanLyTraiHeo.View.Windows.Quản_lý_nhân_viên;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using QuanLyTraiHeo.View.UC;
using System.Windows.Controls;

namespace QuanLyTraiHeo.ViewModel
{
    public class ChiTietThongBaoVM:BaseViewModel
    {
        public MainWindowVM vmMainW;
        public string maNhanVien = "";
        //private ObservableCollection<ThongBao> _listTHONGBAOs;
        //public ObservableCollection<ThongBao> listTHONGBAOs { get => _listTHONGBAOs; set { _listTHONGBAOs = value; OnPropertyChanged(); } }

        private ThongBao _SelectedItem;
        public ThongBao SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }

        private listThongBaoTrongNgayVM _selectedNgay;
        public listThongBaoTrongNgayVM selectedNgay { get => _selectedNgay; set { _selectedNgay = value; OnPropertyChanged(); } }

        private ObservableCollection<listThongBaoTrongNgayVM> _thongBaoTheoNgays;
        public ObservableCollection<listThongBaoTrongNgayVM> thongBaoTheoNgays { get => _thongBaoTheoNgays; set { _thongBaoTheoNgays = value; OnPropertyChanged(); } }

        //private string _txtTieuDe = "";
        private string txtTieuDe;
        public ComboBoxItem cbTinhTrang { get; set; }

        public ICommand CloseChiTietThongBaoW { get; set; }
        public ICommand TimKiemTheoTieuDeCommand { get; set; }
        public ICommand TimKiemTheoTinhTrangCommand { get; set; }
        public ICommand deleteThongBao { get; set; }
        //public ICommand 

        public ChiTietThongBaoVM()
        {
        }
        public ChiTietThongBaoVM(MainWindowVM vm)
        {
            vmMainW = vm;
            maNhanVien = vmMainW.NhanVien.MaNhanVien;
            SelectedItem = vmMainW.selectedItem;
            SelectedItem.TinhTrang = "Đã đọc";
            DataProvider.Ins.DB.SaveChanges();
            txtTieuDe = "";
            cbTinhTrang = new ComboBoxItem();
            cbTinhTrang.Content = "Tất cả";
            selectedNgay = new listThongBaoTrongNgayVM();

            LoadDSThongBao();

            CloseChiTietThongBaoW = new RelayCommand<Window>((p) => { return true; }, p => {
                vmMainW.selectedItem = null;
                vmMainW.loadCountThongBao();
            });
            TimKiemTheoTieuDeCommand = new RelayCommand<TextBox>((p) => { return true; }, p => {
                txtTieuDe = p.Text;
                TimKiem();
            });
            TimKiemTheoTinhTrangCommand = new RelayCommand<ComboBox>((p) => { return true; }, p => {
                TimKiem();
            });
            deleteThongBao = new RelayCommand<Object>(
                (p) => {
                    try
                    {
                        if (SelectedItem != null)
                        {
                            if (SelectedItem.C_MaNguoiGui == maNhanVien)
                            {
                                return true;
                            }
                            else return false;
                        }
                        else return false;
                    }
                    catch(Exception e)
                    {
                        return false;
                    }
                }, 
                p => {
                    ThongBao deleteTB = SelectedItem;
                    SelectedItem = null;
                    var listThongBao = new List<ThongBao>();
                    foreach(var listhongbaoNgay in thongBaoTheoNgays)
                    {
                        foreach(var thongbao in listhongbaoNgay.thongbaotrongngay)
                        {
                            listThongBao.Add(thongbao.tb);
                        }
                    }
                    int i = 0;
                    for (; i < listThongBao.Count; i++)
                    {
                        if (listThongBao[i].MaThongBao == deleteTB.MaThongBao)
                        {
                            DataProvider.Ins.DB.ThongBaos.Remove(deleteTB);
                            DataProvider.Ins.DB.SaveChanges();
                            if (listThongBao.Count > 1)
                            {
                                SelectedItem = listThongBao[i + 1];
                            }
                            else if(listThongBao.Count == 1)
                            {
                                SelectedItem = null;
                            }
                            else if(i == listThongBao.Count - 1)
                            {
                                SelectedItem = listThongBao[0];
                            }
                            break;
                        }
                    }
                    TimKiem();


                });
        }
        void LoadDSThongBao()
        {
            thongBaoTheoNgays = new ObservableCollection<listThongBaoTrongNgayVM>();
            //listTHONGBAOs = new ObservableCollection<ThongBao>(DataProvider.Ins.DB.ThongBaos.Where(x => x.C_MaNguoiNhan == maNhanVien || x.C_MaNguoiGui == maNhanVien).OrderByDescending(x=>x.ThoiGian));
            var listThongBao = new List<ThongBao>(DataProvider.Ins.DB.ThongBaos.Where(x => x.C_MaNguoiNhan == maNhanVien || x.C_MaNguoiGui == maNhanVien).OrderByDescending(x=>x.ThoiGian).ToList());
            var listNgayThongBao = listThongBao.Select(x => x.ThoiGian.Value.Date).Distinct().ToList();
            foreach (var NgaythongBao in listNgayThongBao)
            {
                listThongBaoTrongNgayVM thongbaotrongngay = new listThongBaoTrongNgayVM(NgaythongBao, this);
                thongBaoTheoNgays.Add(thongbaotrongngay);
            }
        }
        void TimKiem()
        {
            //thongBaoTheoNgays = thongBaoTheoNgays2;
            foreach (var listThongBaoTrongNgay in thongBaoTheoNgays)
            {
                listThongBaoTrongNgay.TimKiem(txtTieuDe, cbTinhTrang.Content.ToString());

            }
        }
    }
}
