using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.Windows.Quản_lý_kho;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FarmManagementSoftware.ViewModel
{
    public class ChonHangHoaVM:BaseViewModel
    {
        TaoPhieuKhoVM vm;
        string _TenHH;
        int _soLuongMax;

        private ObservableCollection<ChonHangHoa> _listHangHoa;
        public ObservableCollection<ChonHangHoa> ListHangHoa { get => _listHangHoa; set { _listHangHoa = value; OnPropertyChanged(); } }

        private List<string> _listLoaiHH;
        public List<string> listLoaiHH { get => _listLoaiHH; set { _listLoaiHH = value; OnPropertyChanged(); } }
        private string _selectLoaiHH;
        public string selectLoaiHH { get => _selectLoaiHH; set { _selectLoaiHH = value; OnPropertyChanged(); } }

        public ICommand btnHoanTatcommand { get; set; }
        public ICommand timkiemLoaiHHcommand { get; set; }
        public ICommand timkiemTenHHcommand { get; set; }
        public ICommand timkiemSLTDcommand { get; set; }
        public ChonHangHoaVM()
        {

        }
        public ChonHangHoaVM(TaoPhieuKhoVM vm)
        {
            this.vm = vm;
            ListHangHoa = new ObservableCollection<ChonHangHoa>();
            listLoaiHH = new List<string>();
            _TenHH = "";
            _soLuongMax = -1;

            #region load loại hàng hoá
            var loaiHH = DataProvider.Ins.DB.HANGHOAs.Select(x => x.LoaiHangHoa).Distinct().ToList();
            foreach(var item in loaiHH)
            {
                listLoaiHH.Add(item);
            }
            listLoaiHH.Add("Tất cả");

            selectLoaiHH = "Tất cả";
            #endregion

            loaddsHangHoa();

            #region command btn hoàn tất
            btnHoanTatcommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if (vm.selectedLoaiPhieu != "Phiếu kiểm kho" && !checkListChon())
                    return;
                foreach(var item in ListHangHoa)
                {
                    if(item.IsChecked == true)
                    {
                        if(vm.selectedLoaiPhieu=="Phiếu kiểm kho")
                        {
                            CT_PHIEUKIEMKHO ctkk = new CT_PHIEUKIEMKHO();
                            ctkk.MaHangHoa = item.HangHoa.MaHangHoa;
                            ctkk.SoLuongHienCo = item.HangHoa.SoLuongTonKho;
                            ctkk.HANGHOA = item.HangHoa;
                            if(CheckListHangHoaKiemKho(ctkk))
                            {
                                vm.CTKKs.Add(ctkk);
                            }    
                        }
                        else
                        {
                            CT_PHIEUHANGHOA cthh = new CT_PHIEUHANGHOA();
                            cthh.MaHangHoa = item.HangHoa.MaHangHoa;
                            cthh._donGia = item.HangHoa.DonGia;
                            cthh.HANGHOA = item.HangHoa;
                            cthh._soLuong = 1;
                            if (CheckListHangHoa(cthh))
                            {
                                vm.CTHHs.Add(cthh);
                            }
                        } 
                    }
                }
                vm.TinhTongTien();
                p.Close();
            });
            #endregion

            #region command tìm kiếm theo loại HH
            timkiemLoaiHHcommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                loaddsHangHoa();
            });
            #endregion

            #region command tìm kiếm tên HH
            timkiemTenHHcommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                _TenHH = p.Text;
                loaddsHangHoa();
            });
            #endregion

            #region command tìm kiếm số lượng HH tối đa
            timkiemSLTDcommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                try
                {
                    _soLuongMax = int.Parse(p.Text);
                }
                catch(Exception e)
                {
                    if(p.Text != null && p.Text !="")
                        MessageBox.Show("Nhập số không đúng");
                    p.Text = "";
                    _soLuongMax = -1;
                }
                loaddsHangHoa();
            });
            #endregion
        }

        void loaddsHangHoa()
        {
            ListHangHoa.Clear();

            var listHH = DataProvider.Ins.DB.HANGHOAs.ToList();
            if(selectLoaiHH != "Tất cả")
            {
                listHH = listHH.Where(x => x.LoaiHangHoa == selectLoaiHH).ToList();
            }
            listHH = listHH.Where(x=>x.TenHangHoa.Contains(_TenHH)).ToList();
            if(_soLuongMax >= 0)
            {
                listHH = listHH.Where(x=>x.SoLuongTonKho <= _soLuongMax).ToList();
            }
            foreach(var item in listHH)
            {
                ChonHangHoa hhselect = new ChonHangHoa();
                hhselect.IsChecked = false;
                hhselect.HangHoa = item;
                ListHangHoa.Add(hhselect);
            }
        }

        bool CheckListHangHoa(CT_PHIEUHANGHOA ct)
        {
            foreach(var item in vm.CTHHs)
            {
                if(item.MaHangHoa == ct.MaHangHoa)
                {
                    return false;
                }
            }
            return true;
        }

        bool CheckListHangHoaKiemKho(CT_PHIEUKIEMKHO ct)
        {
            foreach (var item in vm.CTKKs)
            {
                if (item.MaHangHoa == ct.MaHangHoa)
                {
                    return false;
                }
            }
            return true;
        }

        bool checkListChon()
        {
            foreach (var item in ListHangHoa)
            {
                if (item.IsChecked == true && item.HangHoa.SoLuongTonKho == 0)
                {
                    MessageBox.Show("Mặt hàng " + item.HangHoa.MaHangHoa + " hiện đã hết");
                    return false;
                }
            }
            return true;
        }
    }

    public class ChonHangHoa:BaseViewModel
    {
        private bool _IsChecked;
        public bool IsChecked { get => _IsChecked; set { _IsChecked = value;  OnPropertyChanged(); } }
        public HANGHOA HangHoa { get; set; }
        public ChonHangHoa()
        {
            
        }

    }
}
