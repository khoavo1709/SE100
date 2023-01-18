using FarmManagementSoftware.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ListView = System.Windows.Controls.ListView;

namespace FarmManagementSoftware.ViewModel
{
    public class ThemHangHoaVM : BaseViewModel
    {

        public ICommand ThemCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ObservableCollection<LoaiHangHoaModel> listLoaiHangHoa { get; set; }
        public LoaiHangHoaModel loaihanghoa { get; set; }
        public HANGHOA newHangHoa { get; set; }
        public ThemHangHoaVM()
        {
            newHangHoa = new HANGHOA();
            newHangHoa.MaHangHoa = TaoMa();
            listLoaiHangHoa = new ObservableCollection<LoaiHangHoaModel>();
            LoadListLoaiHangHoa();
            ThemCommand = new RelayCommand<Window>((p) => { return true; }, p => { Them(p); });
            ExitCommand = new RelayCommand<Window>((p) => { return true; }, p => { p.Close(); });
        }
  
        private void Them(Window p)
        {
            if (newHangHoa.TenHangHoa == String.Empty || newHangHoa.TenHangHoa == null)
            {
                MessageBox.Show("Vui lòng nhập ten hàng hoá! ", "Thông báo!", MessageBoxButton.OK);
                return;
            }
            newHangHoa.TenHangHoa.ToString().Replace(" ", "");
            newHangHoa.LoaiHangHoa = loaihanghoa.loaiHangHoa;
            DataProvider.Ins.DB.HANGHOAs.Add(newHangHoa);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Thêm hàng hoá mới thành công! ", "Thông báo!", MessageBoxButton.OK);
            newHangHoa = new HANGHOA();
            p.Close();
        }

        private void LoadListLoaiHangHoa()
        {
            listLoaiHangHoa.Clear();
            var listloaihanghoa = from c in DataProvider.Ins.DB.HANGHOAs
                                  select new { c.LoaiHangHoa };
            var listloaihanghoanodupes = listloaihanghoa.Distinct().ToList();
            foreach (var items in listloaihanghoanodupes)
            {
                listLoaiHangHoa.Add(new LoaiHangHoaModel(true, items.LoaiHangHoa));
            }

        }
        string TaoMa()
        {
            string maHH = "";
            var HHs = new ObservableCollection<HANGHOA>(DataProvider.Ins.DB.HANGHOAs).ToList();
            var soHH = HHs.Count;
            //var lastThongBao = DataProvider.Ins.DB.ThongBaos.First();
            if (soHH == 0)
            {
                maHH = "HH000001";
            }
            else
            {
                int STT = soHH;
                do
                {
                    STT++;
                    string strSTT = STT.ToString();
                    for (int i = strSTT.Length; i <= 6; i++)
                    {
                        strSTT = "0" + strSTT;
                    }

                    maHH = "TB" + strSTT;
                }
                while (DataProvider.Ins.DB.ThongBaos.Count(x => x.MaThongBao == maHH) > 0);
            }
            return maHH;
        }
    }
}
