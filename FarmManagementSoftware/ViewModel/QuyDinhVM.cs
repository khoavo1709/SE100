using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.Windows.Quản_lý_nhân_viên;
using FarmManagementSoftware.View.Windows.Quy_Định;

namespace FarmManagementSoftware.ViewModel
{
    public class QuyDinhVM : BaseViewModel
    {
        #region Attributes
        private THAMSO thamso;
        private QuyDinhTiemHeo modifyQDTH;
        private MuaDichBenh modifyMDB;
        public string TextQuyDinh { get;set; }
        #endregion
        #region Properties
        public THAMSO ThamSo { get => thamso; set { thamso = value; OnPropertyChanged(); } }
        public QuyDinhTiemHeo ModifyQDTH { get => modifyQDTH; set { modifyQDTH = value; OnPropertyChanged(); } }
        public MuaDichBenh ModifyMDB { get => modifyMDB; set { modifyMDB = value; OnPropertyChanged(); } }
        private int qdthSelectedIndex { get; set; }
        public int QDTHSelectedIndex { get => qdthSelectedIndex; set { qdthSelectedIndex = value; OnPropertyChanged(); } }

        private int mdbSelectedIndex { get; set; }
        public int MDBSelectedIndex { get => mdbSelectedIndex; set { mdbSelectedIndex = value; OnPropertyChanged(); } }

        public ObservableCollection<QuyDinhTiemHeo> listQDTH { get; }
        public ObservableCollection<HANGHOA> listVacxin { get; }
        public ObservableCollection<MuaDichBenh> listMuaDichBenh { get; }

        #endregion

        #region Commands
        public ICommand LuuThamSoCommand { get; set; }
        public ICommand LuuQDTHCommand { get; set; }
        public ICommand LuuMDBCommand { get; set; }

        public ICommand EditQDTHCommand { get; set; }
        public ICommand DeleteQDTHCommand { get; set; }
        public ICommand EditMDBCommand { get; set; }
        public ICommand DeleteMDBCommand { get; set; }


        #endregion
        public QuyDinhVM()
        {
            TextQuyDinh = "Đây là danh sách các quy định về heo của trang trại, " 
                + "bao gồm các quy định về việc nhập xuất heo, "
                +"tách heo con, chăm sóc heo. " +
                "Cần chú ý khi thay đổi giá trị của các quy định, " +
                "nếu không có thể phần mềm có thể chạy sai dựa vào " +
                "số liệu mà bạn thay đổi.";
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
            EditQDTHCommand = new RelayCommand<Button>((p) => { return true; }, p => EditQDTH());
            DeleteQDTHCommand = new RelayCommand<Button>((p) => { return true; }, p => DeleteQDTH());

            EditMDBCommand = new RelayCommand<Button>((p) => { return true; }, p => EditMDB());
            DeleteMDBCommand = new RelayCommand<Button>((p) => { return true; }, p => DeleteMDB());

        }
        #region Methods

        private void LoadThamSo()
        {
            thamso = DataProvider.Ins.DB.THAMSOes.ToList().FirstOrDefault();
        }
        private void LuuThamSo()
        {
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Lưu thành công!");

        }
        private void LoadQDTiemHeo()
        {
            listQDTH.Clear();
            var listqdth = DataProvider.Ins.DB.QuyDinhTiemHeos.ToList();

            foreach (var i in listqdth)
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

            int val = 0;

            if (listQDTH.Count > 0)
            {
                string id = listQDTH.Last().MaTiemHeo.ToString();
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
            ModifyQDTH.MaTiemHeo = "QDTH" + val.ToString("D6");


            ModifyQDTH.MaVaxin = ModifyQDTH.HANGHOA.MaHangHoa;
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

            if (string.IsNullOrEmpty(ModifyMDB.TenDichBenh))
            {
                MessageBox.Show("Vui lòng nhập tên bệnh ");
                return;
            }

            int val = 0;
            if (listMuaDichBenh.Count > 0)
            {
                string id = listMuaDichBenh.Last().MaDichBenh.ToString();
                string b = "";
                for (int i = 3; i < id.Length; i++)
                {
                    if (Char.IsDigit(id[i]))
                        b += id[i];
                }

                if (b.Length > 0)
                    val = int.Parse(b);
                val += 1;
            }

                ModifyMDB.MaDichBenh = "MDB" + val.ToString("D4");


            DataProvider.Ins.DB.MuaDichBenhs.Add(modifyMDB);
            DataProvider.Ins.DB.SaveChanges();

            LoadListMuaDichBenh();

            ModifyMDB = new MuaDichBenh();
            ModifyMDB.NgayBatDau = DateTime.Today;
            ModifyMDB.NgayKetThuc = DateTime.Today;
            ModifyMDB.NguyenNhan = "";
            ModifyMDB.BienPhap = "";

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

        private void EditQDTH()
        {
            if (QDTHSelectedIndex < 0)
                return;
            EditQDTHVM qdthVM = new EditQDTHVM(listQDTH[QDTHSelectedIndex]);
            EditQDTHWindow qdthWD = new EditQDTHWindow();
            qdthWD.DataContext = qdthVM;
            qdthWD.ShowDialog();
            LoadQDTiemHeo();

        }
        private void DeleteQDTH()
        {
            if (QDTHSelectedIndex < 0)
                return;

            QuyDinhTiemHeo temp = listQDTH[QDTHSelectedIndex];

            if (MessageBox.Show("Bạn có chắc muốn xóa quy định tiêm " + temp.HANGHOA.TenHangHoa + " ?", "Chú ý", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DataProvider.Ins.DB.QuyDinhTiemHeos.Remove(temp);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Xóa thành công !");
                LoadQDTiemHeo();
            }
        }

        private void EditMDB()
        {
            if (MDBSelectedIndex < 0)
                return;
            EditMDBVM mdbVM = new EditMDBVM(listMuaDichBenh[mdbSelectedIndex]);
            EditMDBWindow mdbWD = new EditMDBWindow();
            mdbWD.DataContext = mdbVM;
            mdbWD.ShowDialog();
            LoadListMuaDichBenh();
        }
        private void DeleteMDB()
        {
            if (MDBSelectedIndex < 0)
                return;

            MuaDichBenh temp = listMuaDichBenh[MDBSelectedIndex];

            if (MessageBox.Show("Bạn có chắc muốn xóa thông tin " + temp.TenDichBenh + " ?", "Chú ý", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DataProvider.Ins.DB.MuaDichBenhs.Remove(temp);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Xóa thành công !");
                LoadQDTiemHeo();
            }
            LoadListMuaDichBenh();
        }

        #endregion
    }
    public class EditQDTHVM : BaseViewModel
    {
        private QuyDinhTiemHeo modifyQDTH;
        string id;
        public ObservableCollection<HANGHOA> listVacxin { get; }

        public QuyDinhTiemHeo ModifyQDTH { get => modifyQDTH; set { modifyQDTH = value; OnPropertyChanged(); } }
        public ICommand SaveCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        public EditQDTHVM(QuyDinhTiemHeo qd)
        {
            listVacxin = new ObservableCollection<HANGHOA>();
            listVacxin.Clear();
            var listvacxin = DataProvider.Ins.DB.HANGHOAs.Where(p => p.LoaiHangHoa == "Vacxin").ToList();

            foreach (var i in listvacxin)
            {
                listVacxin.Add(i);
            }

            id = qd.MaTiemHeo;
            ModifyQDTH = new QuyDinhTiemHeo();
            ModifyQDTH.MaVaxin = qd.MaVaxin;
            ModifyQDTH.HANGHOA = qd.HANGHOA;
            ModifyQDTH.MoTa = qd.MoTa;
            ModifyQDTH.TuoiTiem = qd.TuoiTiem;

            SaveCommand = new RelayCommand<Window>((p) => { return true; }, p => { SaveQDTH(p); });
            ExitCommand = new RelayCommand<Window>((p) => { return true; }, p => { ExitQDTH(p); });

        }
        public EditQDTHVM() { }

        private void SaveQDTH(Window p)
        {
            var qdth = (from c in DataProvider.Ins.DB.QuyDinhTiemHeos
                        where (c.MaTiemHeo == id)
                        select c).First();
            qdth.HANGHOA = modifyQDTH.HANGHOA;
            qdth.MaVaxin = modifyQDTH.MaVaxin;
            qdth.MoTa = modifyQDTH.MoTa;
            qdth.TuoiTiem = modifyQDTH.TuoiTiem;
            DataProvider.Ins.DB.SaveChanges();


            p.Close();
        }
        private void ExitQDTH(Window p)
        {
            p.Close();
        }
    }
    public class EditMDBVM : BaseViewModel
    {
        private MuaDichBenh modifyMDB;
        string id;
        public MuaDichBenh ModifyMDB { get => modifyMDB; set { modifyMDB = value; OnPropertyChanged(); } }
        public ICommand SaveCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        public EditMDBVM(MuaDichBenh  qd)
        {

            id = qd.MaDichBenh;
            ModifyMDB = new MuaDichBenh
            {
                TenDichBenh = qd.TenDichBenh,
                NgayBatDau = qd.NgayBatDau,
                NgayKetThuc = qd.NgayKetThuc,
                NguyenNhan = qd.NguyenNhan,
                BienPhap = qd.BienPhap
            };

            SaveCommand = new RelayCommand<Window>((p) => { return true; }, p => { SaveMDB(p); });
            ExitCommand = new RelayCommand<Window>((p) => { return true; }, p => { ExitMDB(p); });

        }
        public EditMDBVM() { }

        private void SaveMDB(Window p)
        {
            if (string.IsNullOrEmpty(ModifyMDB.TenDichBenh))
            {
                MessageBox.Show("Vui lòng nhập tên bệnh ");
                return;
            }

            var mdb = (from c in DataProvider.Ins.DB.MuaDichBenhs
                        where (c.MaDichBenh == id)
                        select c).First();
            mdb.TenDichBenh = ModifyMDB.TenDichBenh;
            mdb.NgayBatDau = ModifyMDB.NgayBatDau;
            mdb.NgayKetThuc = ModifyMDB.NgayKetThuc;
            mdb.NguyenNhan = ModifyMDB.NguyenNhan;
            mdb.BienPhap = ModifyMDB.BienPhap;

            DataProvider.Ins.DB.SaveChanges();


            p.Close();
        }
        private void ExitMDB(Window p)
        {
            p.Close();
        }

    }

}



