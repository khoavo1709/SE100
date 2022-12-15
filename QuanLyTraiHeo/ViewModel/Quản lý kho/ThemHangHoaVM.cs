using QuanLyTraiHeo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ListView = System.Windows.Controls.ListView;

namespace QuanLyTraiHeo.ViewModel
{
    public class ThemHangHoaVM : BaseViewModel
    {
        private HANGHOA hHANGHOA;

        public ICommand ThemCommand { get; set; }
        public ObservableCollection<LoaiHangHoaModel> listLoaiHangHoa { get; set; }
        public LoaiHangHoaModel loaihanghoa { get; set; }
        public HANGHOA newHangHoa { get; set; }
        public ThemHangHoaVM()
        {
            newHangHoa = new HANGHOA();
            listLoaiHangHoa = new ObservableCollection<LoaiHangHoaModel>();
            LoadListLoaiHangHoa();
            ThemCommand = new RelayCommand<Window>((p) => { return true; }, p => { Them(p); });

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
            newHangHoa.MaHangHoa = ("HH" + DataProvider.Ins.DB.HANGHOAs.Count().ToString()).Replace(" ", "");
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
    }
}
