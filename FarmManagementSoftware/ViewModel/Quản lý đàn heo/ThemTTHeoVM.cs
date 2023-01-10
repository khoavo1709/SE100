using FarmManagementSoftware.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.Forms.MessageBox;
using TextBox = System.Windows.Controls.TextBox;

namespace FarmManagementSoftware.ViewModel
{
    public class ThemTTHeoVM : BaseViewModel
    {
        private string _MaHeo;
        private string _MaLoaiHeo;
        private string _MaGiongHeo;
        private string _GioiTinh;
        private int _TrongLuong;
        private DateTime? _NgaySinh;
        private string _MaChuong;
        private string _NguonGoc;
        private string _TinhTrang;
        private HEO _SelectedMe;
        private HEO _SelectedCha;
        private ObservableCollection<HEO> _ListHeoAdd;

        public ObservableCollection<HEO> ListHeoAdd { get => _ListHeoAdd; set { _ListHeoAdd = value; OnPropertyChanged(); } }
        public ObservableCollection<LOAIHEO> ListLoai { get; set; }
        public List<HEO> ListCha { get; }
        public List<HEO> ListMe { get; }
        public ObservableCollection<GIONGHEO> ListGiong { get; set; }
        public ObservableCollection<CHUONGTRAI> ListChuong { get; set; }



        public HEO HeoAdd { get; set; }
        public HEO SelectedMe { get => _SelectedMe; set { _SelectedMe = value; OnPropertyChanged(); } }
        public HEO SelectedCha { get => _SelectedCha; set { _SelectedCha = value; OnPropertyChanged(); } }
        public LOAIHEO SelectedLoai { get; set; }
        public GIONGHEO SelectedGiong { get; set; }
        public CHUONGTRAI SelectedChuong { get; set; }
        public string MaHeo { get => _MaHeo; set { _MaHeo = value; OnPropertyChanged(); } }
        public string MaLoaiHeo { get => _MaLoaiHeo; set { _MaLoaiHeo = value; OnPropertyChanged(); } }
        public string MaGiongHeo { get => _MaGiongHeo; set { _MaGiongHeo = value; OnPropertyChanged(); } }
        public string GioiTinh { get => _GioiTinh; set { _GioiTinh = value; OnPropertyChanged(); } }
        public int TrongLuong { get => _TrongLuong; set { _TrongLuong = value; OnPropertyChanged(); } }
        public DateTime? NgaySinh { get => _NgaySinh; set { _NgaySinh = value; OnPropertyChanged(); } }
        public string MaChuong { get => _MaChuong; set { _MaChuong = value; OnPropertyChanged(); } }
        public string MaHeoCha { get; set; }
        public string MaHeoMe { get; set; }
        public string NguonGoc { get => _NguonGoc; set { _NguonGoc = value; OnPropertyChanged(); } }
        public string TinhTrang { get => _TinhTrang; set { _TinhTrang = value; OnPropertyChanged(); } }



        public ICommand AddCommand { get; set; }
        public ICommand HuyCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ExcelCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        public ICommand TimKiemTheoMa_TenCommand { get; set; }

        THAMSO thamso = new THAMSO();
        public ThemTTHeoVM()
        {
            thamso = DataProvider.Ins.DB.THAMSOes.First();
            HEO X = new HEO();
            X.MaHeo = "Không chọn";
            SelectedMe = SelectedCha = X;
            NguonGoc = "Sinh trong trang trại";
            TinhTrang = "Sức khoẻ tốt";
            ListHeoAdd = new ObservableCollection<HEO>();
            ListMe = new ObservableCollection<HEO>().ToList();
            ListCha = new ObservableCollection<HEO>().ToList();
            ListLoai = new ObservableCollection<LOAIHEO>(DataProvider.Ins.DB.LOAIHEOs);
            ListCha.Add(X);
            var Cha = new ObservableCollection<HEO>(DataProvider.Ins.DB.HEOs.Where(x => x.GioiTinh == "Đực")).ToList();

            foreach (HEO x in Cha)
            {
                ListCha.Add(x);
            }
            ListMe.Add(X);
            var Me = new ObservableCollection<HEO>(DataProvider.Ins.DB.HEOs.Where(x => x.GioiTinh == "Cái")).ToList();
            foreach (HEO x in Me)
            {
                ListMe.Add(x);
            }
            ListGiong = new ObservableCollection<GIONGHEO>(DataProvider.Ins.DB.GIONGHEOs);
            ListChuong = new ObservableCollection<CHUONGTRAI>(DataProvider.Ins.DB.CHUONGTRAIs.Where(x => x.SuaChuaToiDa > x.SoLuongHeo).ToList());
            MaHeo = LayMa();

            AddCommand = new RelayCommand<TextBox>((p) => {
                return true;
            }, p =>
            {
                if (GioiTinh == null)
                {
                    MessageBox.Show("Vui lòng chọn giới tính");
                    return;
                }
                if (NgaySinh == null || NgaySinh > DateTime.Today)
                {
                    MessageBox.Show("Vui lòng chọn lại ngày sinh");
                    return;
                }
                if (TrongLuong == null || TrongLuong <= 0)
                {
                    MessageBox.Show("Vui lòng nhập đúng trọng lượng");
                    return;
                }
                if (SelectedLoai == null)
                {
                    MessageBox.Show("Vui lòng chọn loại heo");
                    return;
                }
                if (SelectedGiong == null)
                {
                    MessageBox.Show("Vui lòng chọn giống heo");
                    return;
                }
                if (SelectedChuong == null)
                {
                    MessageBox.Show("Vui lòng chọn mã chuồng");
                    return;
                }
                if (TinhTrang == null)
                {
                    MessageBox.Show("Vui lòng chọn tình trạng");
                    return;
                }
                if (NguonGoc == null)
                {
                    MessageBox.Show("Vui lòng chọn nguồn gốc");
                    return;
                }
                HeoAdd = new HEO();
                MaHeo = LayMa();
                HeoAdd.MaHeo = MaHeo;
                HeoAdd.GioiTinh = GioiTinh;
                HeoAdd.NgaySinh = NgaySinh;
                HeoAdd.TrongLuong = TrongLuong;
                HeoAdd.LOAIHEO = new LOAIHEO();
                HeoAdd.LOAIHEO = SelectedLoai;
                HeoAdd.GIONGHEO = new GIONGHEO();
                HeoAdd.GIONGHEO = SelectedGiong;
                if (SelectedMe.MaHeo != "Không chọn")
                    HeoAdd.MaHeoMe = SelectedMe.MaHeo;
                if (SelectedCha.MaHeo != "Không chọn")
                    HeoAdd.MaHeoCha = SelectedCha.MaHeo; ;
                HeoAdd.CHUONGTRAI = new CHUONGTRAI();
                HeoAdd.CHUONGTRAI = SelectedChuong;
                HeoAdd.NguonGoc = NguonGoc;
                HeoAdd.TinhTrang = TinhTrang;
                if (!KiemTra())
                    return;
                ListHeoAdd.Add(HeoAdd);
                ClearForm();

            });

            HuyCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                p.Close();
                ListHeoAdd = null;
            });

            DeleteCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                ListHeoAdd.Remove(HeoAdd);
                MessageBox.Show(ListHeoAdd.Count().ToString());
            });

            ExcelCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {

                //if(ListHeoAdd.Count>0)
                //{
                //    MessageBox.Show("Vui lòng chỉ import heo từ excel khi danh sách còn trống");
                //    return;
                //}
                string filePath = "";
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Excel | *.xlsx";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = dialog.FileName;
                }
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("Đường dẫn không hợp lệ!");
                    return;
                }
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                try
                {

                    var package = new ExcelPackage(dialog.FileName);
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                    {
                        int j = 1;
                        HeoAdd = new HEO();
                        HeoAdd.MaHeo = LayMa();
                        HeoAdd.GioiTinh = worksheet.Cells[i, j++].Value.ToString();
                        HeoAdd.NgaySinh = DateTime.Parse(worksheet.Cells[i, j++].Value.ToString());

                        HeoAdd.TrongLuong = int.Parse(worksheet.Cells[i, j++].Value.ToString());

                        var tenloai = worksheet.Cells[i, j++].Value.ToString();
                        HeoAdd.LOAIHEO = new LOAIHEO();
                        foreach (LOAIHEO loai in ListLoai)
                        {
                            if (tenloai == loai.TenLoaiHeo)
                            {
                                HeoAdd.LOAIHEO = loai;
                                break;
                            }

                        }
                        var tengiong = worksheet.Cells[i, j++].Value.ToString();
                        HeoAdd.GIONGHEO = new GIONGHEO();
                        foreach (GIONGHEO giong in ListGiong)
                        {
                            if (tengiong == giong.TenGiongHeo)
                            {
                                HeoAdd.GIONGHEO = giong;
                                break;
                            }
                        }
                        if (worksheet.Cells[i, j++].Value != null)
                            HeoAdd.MaHeoMe = worksheet.Cells[i, j].Value.ToString();

                        if (worksheet.Cells[i, j++].Value != null)
                            HeoAdd.MaHeoCha = worksheet.Cells[i, j].Value.ToString();
                        var mchuong = worksheet.Cells[i, j++].Value.ToString();
                        HeoAdd.CHUONGTRAI = new CHUONGTRAI();
                        foreach (CHUONGTRAI chuong in ListChuong)
                        {
                            if (chuong.MaChuong.Contains(mchuong))
                            {
                                HeoAdd.CHUONGTRAI = chuong;
                                break;
                            }
                        }
                        HeoAdd.TinhTrang = worksheet.Cells[i, j++].Value.ToString();
                        HeoAdd.NguonGoc = worksheet.Cells[i, j++].Value.ToString();
                        if (HeoAdd.CHUONGTRAI.MaChuong == null)
                        {
                            MessageBox.Show("Chuồng nuôi " + mchuong + " đã đầy!");
                            return;
                        }
                        if (!KiemTra())
                            return;
                        ListHeoAdd.Add(HeoAdd);
                    }
                }
                catch
                {
                    MessageBox.Show("Lỗi import từ excel");
                }
            });


            XacNhanCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                foreach (var item in ListHeoAdd)
                {
                    if (item.CHUONGTRAI.SoLuongHeo < item.CHUONGTRAI.SuaChuaToiDa)
                    {
                        item.CHUONGTRAI.SoLuongHeo += 1;
                        DataProvider.Ins.DB.HEOs.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Sức chứa của chuồng không đủ. Heo" + item.MaHeo + " chưa được lưu");
                    }
                }
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Thêm thành công");
                p.Close();
            });

        }
        string CreatMaHeo(int lan)
        {
            ObservableCollection<HEO> Heos = new ObservableCollection<HEO>(DataProvider.Ins.DB.HEOs);
            int soHeo;
            if (ListHeoAdd != null)
            { soHeo = Heos.Count + ListHeoAdd.Count + lan; }
            else
            {
                soHeo = Heos.Count + lan;
            }
            string maHeo;
            if (soHeo == 0)
            {
                maHeo = "HEO0000001" + DateTime.Now.ToString("_ddMM");
            }
            else
            {
                int STT = soHeo;
                STT++;
                string strSTT = STT.ToString();
                for (int i = strSTT.Length; i <= 6; i++)
                {
                    strSTT = "0" + strSTT;
                }

                maHeo = "HEO" + strSTT + DateTime.Now.ToString("_ddMM");
            }
            return maHeo;
        }
        string LayMa()
        {
            string MaCu = CreatMaHeo(0);
            int i = 0;
            var SL = new List<HEO>(DataProvider.Ins.DB.HEOs.Where(x => x.MaHeo == MaCu));
            while (SL.Count > 0)
            {
                i++;
                MaCu = CreatMaHeo(i);
                SL = new List<HEO>(DataProvider.Ins.DB.HEOs.Where(x => x.MaHeo == MaCu));
            }
            return MaCu;
        }
        bool KiemTra()
        {
            string msg;
            if (HeoAdd.GioiTinh == "Cái" && HeoAdd.LOAIHEO.TenLoaiHeo.Contains("đực"))
            {
                msg = "Chọn sai giới tính hoặc loại heo";
                MessageBox.Show(msg);
                return false;
            }
            if (HeoAdd.GioiTinh == "Đực" && HeoAdd.LOAIHEO.TenLoaiHeo.Contains("nái"))
            {
                msg = "Chọn sai giới tính hoặc loại heo";
                MessageBox.Show(msg);
                return false;
            }
            TimeSpan tuoiheo = (TimeSpan)(DateTime.Now.Date - HeoAdd.NgaySinh);
            if (tuoiheo.Days < thamso.TuoiNhapDan && HeoAdd.LOAIHEO.TenLoaiHeo != "Heo con")
            {
                msg = "Heo còn quá nhỏ, chưa thể nhập đàn";
                MessageBox.Show(msg);
                return false;
            }
            if (HeoAdd.MaHeoMe != null && HeoAdd.MaHeoCha != null)
            {
                if (!(HeoAdd.LOAIHEO.TenLoaiHeo.Contains("con")) && (HeoAdd.MaHeoMe != "Không chọn" && HeoAdd.MaHeoCha != "Không chọn"))
                {
                    msg = "Chỉ chọn heo cha, heo mẹ cho heo thuộc loại heo con";
                    MessageBox.Show(msg);
                    return false;
                }
                if ((HeoAdd.LOAIHEO.TenLoaiHeo.Contains("con")) && (HeoAdd.MaHeoMe == "Không chọn" || HeoAdd.MaHeoCha == "Không chọn"))
                {
                    msg = "Heo con phải có cả heo cha và heo mẹ";
                    MessageBox.Show(msg);
                    return false;
                }
            }


            if (HeoAdd.LOAIHEO.TenLoaiHeo.Contains("nái") && (!HeoAdd.CHUONGTRAI.MaChuong.Contains("HN") && !HeoAdd.CHUONGTRAI.MaChuong.Contains("HD")))
            {
                msg = "Chuồng hiện tại không phù hợp với loại heo nái";
                MessageBox.Show(msg);
                return false;
            }
            else if (HeoAdd.LOAIHEO.TenLoaiHeo.Contains("con") && HeoAdd.CHUONGTRAI.MaChuong.Contains("DG"))
            {
                msg = "Heo con không thể ở chuồng đực giống";
                MessageBox.Show(msg);
                return false;
            }
            else if (HeoAdd.LOAIHEO.TenLoaiHeo.Contains("đực") && (HeoAdd.CHUONGTRAI.MaChuong.Contains("N") || HeoAdd.CHUONGTRAI.MaChuong.Contains("HD")))
            {
                msg = "Heo đực không thể ở chuồng heo nái khác";
                MessageBox.Show(msg);
                return false;
            }
            else if (HeoAdd.LOAIHEO.TenLoaiHeo.Contains("thịt") && !HeoAdd.CHUONGTRAI.MaChuong.Contains("T"))
            {
                msg = "Heo thịt chỉ có thể ở chuồng heo thịt";
                MessageBox.Show(msg);
                return false;
            }
            return true;
        }
        void ClearForm()
        {
            HeoAdd = null;
            MaHeo = LayMa();
            GioiTinh = null;
            NgaySinh = null;
            TrongLuong = 0;
            SelectedLoai = null;
            MaLoaiHeo = null;
            SelectedGiong = null;
            MaGiongHeo = null;
            MaHeoCha = MaHeoMe = "Không chọn";
            SelectedChuong = null;
            MaChuong = null;
            HEO X = new HEO();
            X.MaHeo = "Không chọn";
            SelectedMe = SelectedCha = X;
            NguonGoc = "Sinh trong trang trại";
            TinhTrang = "Sức khoẻ tốt";
        }
    }

}
