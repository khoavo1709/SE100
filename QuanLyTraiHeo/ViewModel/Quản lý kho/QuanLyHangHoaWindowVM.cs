using QuanLyTraiHeo.Model;
using QuanLyTraiHeo.View.Windows.Quản_lý_kho;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using ListView = System.Windows.Controls.ListView;

namespace QuanLyTraiHeo.ViewModel
{
    public class QuanLyHangHoaWindowVM : BaseViewModel
    {
        public ObservableCollection<HANGHOA> listHangHoa { get; set; }

        public ObservableCollection<TinhTrangHangHoaModel> listTinhTrang { get; set; }
        public ObservableCollection<LoaiHangHoaModel> listLoaiHangHoa { get; set; }
        public int listviewSelectedIndex { get; set; }
        public string textTimKiem { get; set; }
        public string textDonGiaToiThieu { get; set; }
        public string textDonGiaToiDa { get; set; }
        public string textSoLuongToiThieu { get; set; }
        public string textSoLuongToiDa { get; set; }
        public ICommand ThemHangHoaCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand TextTimKiemChangeCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DongiatoithieuChangeCommand { get; set; }
        public ICommand DongiatoidaChangeCommand { get; set; }
        public ICommand SoluongtoithieuChangeCommand { get; set; }
        public ICommand SoluongtoidaChangeCommand { get; set; }

        public QuanLyHangHoaWindowVM()
        {
            textTimKiem = "";
            listHangHoa = new ObservableCollection<HANGHOA>(DataProvider.Ins.DB.HANGHOAs);
            listLoaiHangHoa = new ObservableCollection<LoaiHangHoaModel>();
            listTinhTrang = new ObservableCollection<TinhTrangHangHoaModel>();
            listviewSelectedIndex = 0;
            ThemHangHoaCommand = new RelayCommand<Window>((p) => { return true; }, p => { ThemHangHoa(p); });
            EditCommand = new RelayCommand<Window>((p) => { return true; }, p => { Edit(p); });
            TextTimKiemChangeCommand = new RelayCommand<ListView>((p) => { return true; }, p => { TextTimKiemChanged(p); });
            DongiatoithieuChangeCommand = new RelayCommand<ListView>((p) => { return true; }, p => { DongiatoithieuChanged(p); });
            DongiatoidaChangeCommand = new RelayCommand<ListView>((p) => { return true; }, p => { DongiatoidaChanged(p); });
            SoluongtoithieuChangeCommand = new RelayCommand<ListView>((p) => { return true; }, p => { SoluongtoithieuChanged(p); });
            SoluongtoidaChangeCommand = new RelayCommand<ListView>((p) => { return true; }, p => { SoluongtoidaChanged(p); });
            LoadListLoaiHangHoa();
            LoadListTinhTrang();
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

        private void SoluongtoidaChanged(ListView p)
        {
            LoadListHangHoa();
        }

        private void SoluongtoithieuChanged(ListView p)
        {
            LoadListHangHoa();
        }

        private void DongiatoidaChanged(ListView p)
        {
            LoadListHangHoa();
        }

        private void DongiatoithieuChanged(ListView p)
        {
            LoadListHangHoa();
        }

        private void LoadListTinhTrang()
        {
            listTinhTrang.Clear();
            var listtinhtrang = from c in DataProvider.Ins.DB.HANGHOAs
                                select new { c.TinhTrang };
            var listtinhtrangnodupes = listtinhtrang.Distinct().ToList();
            foreach (var items in listtinhtrangnodupes)
            {
                listTinhTrang.Add(new TinhTrangHangHoaModel(true, items.TinhTrang));
            }
        }

        private void LoadListHangHoa()
        {
            //listHangHoa.Clear();

            //var listhanghoa = DataProvider.Ins.DB.HANGHOAs.Where(s => s.TenHangHoa.Contains(textTimKiem)).ToList();
            //foreach (var items in listhanghoa)
            //{
            //    int flag = 0;
            //    foreach (var items2 in listhanghoa)
            //    {
            //        if (exLoai== false)
            //            if ()
            //            {
            //                flag = 1;
            //                break;
            //            }
            //    }
            //    if (flag == 0)
            //        listHangHoa.Add(items);
            //}
            listHangHoa.Clear();
            var listhanghoa = DataProvider.Ins.DB.HANGHOAs.Where(s => s.TenHangHoa.Contains(textTimKiem)).ToList();
            if (!string.IsNullOrWhiteSpace(textDonGiaToiDa))
            {
                listhanghoa = listhanghoa.Where(s => s.DonGia <= int.Parse(textDonGiaToiDa)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(textDonGiaToiThieu))
            {
                listhanghoa = listhanghoa.Where(s => s.DonGia >= int.Parse(textDonGiaToiThieu)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(textSoLuongToiThieu))
            {
                listhanghoa = listhanghoa.Where(s => s.SoLuongTonKho >= int.Parse(textSoLuongToiThieu)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(textSoLuongToiDa))
            {
                listhanghoa = listhanghoa.Where(s => s.SoLuongTonKho <= int.Parse(textSoLuongToiDa)).ToList();
            }


            foreach (var items in listhanghoa.ToList())
            {
                int flag = 0;
                foreach (var items2 in listLoaiHangHoa)
                {
                    if (items2.isSelected == false)
                        if (items.LoaiHangHoa == items2.loaiHangHoa)
                        {
                            flag = 1;
                            break;
                        }
                }
                if (flag != 0)
                    listhanghoa.Remove(items);
            }
            foreach (var items in listhanghoa.ToList())
            {
                int flag = 0;
                foreach (var items2 in listTinhTrang)
                {
                    if (items2.isSelected == false)
                        if (items.TinhTrang == items2.tinhTrang)
                        {
                            flag = 1;
                            break;
                        }
                }
                if (flag != 0)
                    listhanghoa.Remove(items);
            }
            foreach (var items in listhanghoa)
            {
                listHangHoa.Add(items);
            }
        }

        private void TextTimKiemChanged(ListView p)
        {
            LoadListHangHoa();
        }

        private void ThemHangHoa(Window p)
        {
            ThemHangHoawindow themhanghoa = new ThemHangHoawindow();
            themhanghoa.ShowDialog();
        }

        private void Edit(Window p)
        {
            if (listviewSelectedIndex < 0)
                return;
            ThongTinHangHoaVM thongTinHangHoaVM = new ThongTinHangHoaVM(listHangHoa[listviewSelectedIndex]);
            ThongTinHangHoa thongTinHangHoa = new ThongTinHangHoa();
            thongTinHangHoa.DataContext = thongTinHangHoaVM;
            thongTinHangHoa.ShowDialog();
        }
    }
}
