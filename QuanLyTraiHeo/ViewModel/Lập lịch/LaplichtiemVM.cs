using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using QuanLyTraiHeo.Model;
using QuanLyTraiHeo.View.Windows;
using System.Windows.Controls;

namespace QuanLyTraiHeo.ViewModel
{
    public class LaplichtiemVM : BaseViewModel
    {
        //public ObservableCollection<HEO> ListHeo { get; set; }
        //public ObservableCollection<LICHTIEMHEO> Lichtiem { get; set; }
        public List<LICHTIEMHEO> LICHTIEMTEST { get; set; }
        //public ObservableCollection<LOAIHEO> ListLoai { get; set; }
        //public ObservableCollection<GIONGHEO> ListGiong { get; set; }

        public string MaTim;

        public HEO SelectedHeo { get; set; }
        public LOAIHEO SelectedLoai { get; set; }
        public GIONGHEO SelectedGiong { get; set; }
        public LICHTIEMHEO tiemheo { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand ShowCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand TimKiemTheoMa_TenCommand { get; set; }

        public LaplichtiemVM()
        {
            //ListHeo = new ObservableCollection<HEO>(DataProvider.Ins.DB.HEOs);
            //ListLoai = new ObservableCollection<LOAIHEO>(DataProvider.Ins.DB.LOAIHEOs);
            //ListGiong = new ObservableCollection<GIONGHEO>(DataProvider.Ins.DB.GIONGHEOs);
            //Lichtiem = new ObservableCollection<LICHTIEMHEO>(DataProvider.Ins.DB.LICHTIEMHEOs);
            LICHTIEMTEST = new List<LICHTIEMHEO>(DataProvider.Ins.DB.LICHTIEMHEOs);

            AddCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                //ThemTTHeo themTTHeo = new ThemTTHeo();
                //themTTHeo.ShowDialog();
                //Lichtiem = new ObservableCollection<LICHTIEMHEO>(DataProvider.Ins.DB.LICHTIEMHEOs);
                var t = new LICHTIEMHEO();
                {
                    t.MaLichTiem = "";
                    //t.MaHeo = Pigcode_text.Text;
                    /*t.MaHeo = SelectedHeo.MaHeo;
                    t. = SelectedLoai.MaLoai;
                    t.MaGiong = SelectedGiong.MaGiong;
                    t.NgayTiem = tiemheo.NgayTiem;
                    t.NgayHenTiem = tiemheo.NgayHenTiem;
                    t.TenThuoc = tiemheo.TenThuoc;
                    t.SoLuong = tiemheo.SoLuong;
                    t.DonVi = tiemheo.DonVi;
                    t.GhiChu = tiemheo.GhiChu;*/
                }
                DataProvider.Ins.DB.LICHTIEMHEOs.Add(t);
            });

            ShowCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                //ThongTinHeo thongTinHeo = new ThongTinHeo();
                //thongTinHeo.DataContext = SelectedHeo;
                //thongTinHeo.ShowDialog();
            });
            EditCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                //SuaTTHeo suaTTHeo = new SuaTTHeo();
                //suaTTHeo.DataContext = SelectedHeo;
                //suaTTHeo.ShowDialog();
            });
            DeleteCommand = new RelayCommand<Window>((p) =>
            {
                if (SelectedHeo == null)
                    return false;
                if (SelectedHeo.CT_PHIEUHEO.Count() > 0)
                    return false;
                if (SelectedHeo.LICHTIEMHEOs.Count() > 0)
                    return false;
                if (SelectedHeo.LICHPHOIGIONGs.Count() > 0)
                    return false;

                return true;
            }, p =>
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn xoá ?", "Cảnh báo", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataProvider.Ins.DB.HEOs.Remove(SelectedHeo);
                    //ListHeo.Remove(SelectedHeo);
                    DataProvider.Ins.DB.SaveChanges();

                }
            }
            );

            TimKiemTheoMa_TenCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                MaTim = p.Text;
                TimTheoMa();
            });
        }
        void TimTheoMa()
        {
            //ListHeo.Clear();
            var HeoTheoMa = DataProvider.Ins.DB.HEOs.Where(Heo => Heo.MaHeo.Contains(MaTim)).ToList();
            foreach (var Heo in HeoTheoMa)
            {
                //ListHeo.Add(Heo);
            }
        }
        }

    }

