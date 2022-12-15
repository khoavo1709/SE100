using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyTraiHeo.Model;

namespace QuanLyTraiHeo.ViewModel
{
    public class QuyDinhVM : BaseViewModel
    {
        #region Attributes
        private THAMSO thamso;
        private QuyDinhTiemHeo modifyQDTH;
        public int listQDTHIndexBinding;
        private MuaDichBenh modifyMDB;

        #endregion
        #region Properties
        public THAMSO ThamSo { get => thamso; set { thamso = value; OnPropertyChanged(); } }
        public int listQDTHSelectedIndex;
        public QuyDinhTiemHeo ModifyQDTH { get => modifyQDTH; set { modifyQDTH = value; OnPropertyChanged(); } }
        public MuaDichBenh ModifyMDB { get => modifyMDB; set { modifyMDB = value; OnPropertyChanged(); } }

        public ObservableCollection<QuyDinhTiemHeo> listQDTH { get; }
        public ObservableCollection<HANGHOA> listVacxin { get; }
        public ObservableCollection<MuaDichBenh> listMuaDichBenh { get; }

        #endregion

        #region Commands
        public ICommand LuuThamSoCommand { get; set; }
        public ICommand LuuQDTHCommand { get; set; }
        public ICommand LuuMDBCommand { get; set; }

        public ICommand ThayDoiQDTHCommand { get; set; }
        public ICommand BindingToModifyQDTH { get; set; }

        #endregion
        public QuyDinhVM()
        {
            listQDTH = new ObservableCollection<QuyDinhTiemHeo>();
            listVacxin = new ObservableCollection<HANGHOA>();
            listMuaDichBenh = new ObservableCollection<MuaDichBenh>();

            ModifyQDTH = new QuyDinhTiemHeo();
            ModifyQDTH.HANGHOA = null;
            ModifyQDTH.MoTa = "";
            ModifyQDTH.TuoiTiem = 0;
            
            ModifyMDB = new MuaDichBenh();
            ModifyMDB.NgayBatDau = DateTime.Today;
            ModifyMDB.NgayKetThuc = DateTime.Today;
            ModifyMDB.NguyenNhan = "";
            ModifyMDB.BienPhap = "";

            LoadThamSo();
            LoadQDTiemHeo();
            LoadListVacxin();
            LoadListMuaDichBenh();

            ModifyQDTH = new QuyDinhTiemHeo();
            LuuThamSoCommand = new RelayCommand<Button>((p) => { return true; }, p => LuuThamSo());
            LuuQDTHCommand = new RelayCommand<Button>((p) => { return true; }, p => LuuQDTH());
            LuuMDBCommand = new RelayCommand<Button>((p) => { return true; }, p => LuuMDB());
            ThayDoiQDTHCommand = new RelayCommand<Button>((p) => { return true; }, p => ThayDoiQDTH());
            BindingToModifyQDTH = new RelayCommand<Button>((p) => { return true; }, p => ThayDoiQDTH());

        }
        #region Methods

        private void LoadThamSo()
        {
             thamso = DataProvider.Ins.DB.THAMSOes.ToList().FirstOrDefault();
        }
        private void LuuThamSo()
        {
            DataProvider.Ins.DB.SaveChanges();
        }
        private void LoadQDTiemHeo()
        {
            listQDTH.Clear();
            var listqdth =  DataProvider.Ins.DB.QuyDinhTiemHeos.ToList();

            foreach(var i in listqdth)
            {
                listQDTH.Add(i);
            }
        }
        private void LuuQDTH()
        {
            if (ModifyQDTH.HANGHOA == null)
            {
                MessageBox.Show("Vui lòng chọn loại vacxin ");
                return;
            }


            ModifyQDTH.MaVaxin = ModifyQDTH.HANGHOA.MaHangHoa;
            ModifyQDTH.MaTiemHeo = "QDTH" + listQDTH.Count().ToString() ;
            DataProvider.Ins.DB.QuyDinhTiemHeos.Add(modifyQDTH);
            DataProvider.Ins.DB.SaveChanges();

            LoadQDTiemHeo();
            ModifyQDTH = new QuyDinhTiemHeo();
            ModifyQDTH.HANGHOA = null;
            ModifyQDTH.MoTa = "";
            ModifyQDTH.TuoiTiem = 0;


        }
        private void LuuMDB()
        {

            if(string.IsNullOrEmpty(ModifyMDB.TenDichBenh))
            {
                MessageBox.Show("Vui lòng nhập tên bệnh ");
                return;
            }


            ModifyMDB.MaDichBenh = "MDB" +listMuaDichBenh.Count().ToString();

            DataProvider.Ins.DB.MuaDichBenhs.Add(modifyMDB);
            DataProvider.Ins.DB.SaveChanges();

            LoadListMuaDichBenh();

            ModifyMDB = new MuaDichBenh();
            ModifyMDB.NgayBatDau = DateTime.Today;
            ModifyMDB.NgayKetThuc = DateTime.Today;
            ModifyMDB.NguyenNhan = "";
            ModifyMDB.BienPhap = "";

        }

        private void ThayDoiQDTH()
        {

        }

        private void LoadListVacxin()
        {
            listVacxin.Clear();
            var listvacxin = DataProvider.Ins.DB.HANGHOAs.Where(p => p.LoaiHangHoa == "Vacxin").ToList();

            foreach (var i in listvacxin)
            {
                listVacxin.Add(i);
            }
          
        }

        private void LoadListMuaDichBenh()
        {
            listMuaDichBenh.Clear();
            var listdichbenh = DataProvider.Ins.DB.MuaDichBenhs.ToList();

            foreach (var i in listdichbenh)
            {
                listMuaDichBenh.Add(i);
            }
        }

        #endregion
    }


}
