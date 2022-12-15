using QuanLyTraiHeo.Model;
using QuanLyTraiHeo.View.UC;
using QuanLyTraiHeo.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyTraiHeo.ViewModel
{
    public class SoDoVM: BaseViewModel
    {
        #region Attributes
        wSoDo wd;

        List<string> listArea;
        List<string> listTypePigsty;
        string _textSoChuong = "";

        List<CHUONGTRAI> listChuongTrai = new List<CHUONGTRAI>();
        List<IconChuongUC> listIconChuongFinded;
        #endregion

        #region Propatry
        public List<string> ListArea { get => listArea; set { listArea = value; OnPropertyChanged(); } }
        public List<string> ListTypePigsty { get => listTypePigsty; set { listTypePigsty = value; OnPropertyChanged(); } }
        public string TextSoChuong { get => _textSoChuong; set { _textSoChuong = value; OnPropertyChanged(); } }

        #endregion

        #region Event command
        public ICommand LoadedWindowCommand { get; set; }

        public ICommand SeacrhCommand { get; set; }

        public ICommand TextSeacrhCommand { get; set; }
        #endregion
        public SoDoVM()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, p => { wd = p as wSoDo ; Load(); });

            SeacrhCommand = new RelayCommand<Window>((p) => { return true; }, p => { Add_IconChuong_ToWP(); });

            TextSeacrhCommand = new RelayCommand<string>((p) => { return true; }, p => { TextSeacrh(p); });
        }

        void Load()
        {
            listArea = new List<string>();
            listTypePigsty = new List<string>();

            listTypePigsty.Add("All");
            foreach (var item in DataProvider.Ins.DB.LOAICHUONGs)
            {
                ListTypePigsty.Add(item.TenLoai);
            }

            foreach (var item in DataProvider.Ins.DB.CHUONGTRAIs)
            {
                if (!ListArea.Contains(item.MaChuong.Substring(0, 1)))
                {
                    ListArea.Add(item.MaChuong.Substring(0, 1));
                }
            }
            listArea.Add("All");

            wd.cbb_ChonKhu.ItemsSource = ListArea;
            wd.cbb_ChonLoaiChuong.ItemsSource = ListTypePigsty;

            wd.cbb_ChonKhu.SelectedIndex = 0;
            wd.cbb_ChonLoaiChuong.SelectedIndex = 0;

            listChuongTrai = DataProvider.Ins.DB.CHUONGTRAIs.ToList();

            Add_IconChuong_ToWP();
        }

        List<IconChuongUC> LoadAll()
        {
            List<IconChuongUC> list = new List<IconChuongUC>();

            foreach (var item in listChuongTrai)
            {
                IconChuongUC chuong = new IconChuongUC() { Tag = item };
                chuong.tb_SoLuongHeo.Text = DataProvider.Ins.DB.CHUONGTRAIs.Where(x => x.MaChuong == item.MaChuong).SingleOrDefault().HEOs.Count().ToString();
                chuong.tb_TenChuong.Text = item.MaChuong;
                chuong.DataContext = new ChuongUC_VM() { _SoLuongHeo = Int16.Parse(chuong.tb_SoLuongHeo.Text) };
                list.Add(chuong);
            }

            return list;
        }

        List<IconChuongUC> Search(string khu, string loaiChuong)
        {
            List<IconChuongUC> list = new List<IconChuongUC>();

            if (loaiChuong as string == "All" && khu == "All")
            {
                return LoadAll();
            }

            string _loaiChuong = "";
            if (loaiChuong != "All")
            {
                _loaiChuong = DataProvider.Ins.DB.LOAICHUONGs.Where(x => x.TenLoai == loaiChuong).SingleOrDefault().MaLoaiChuong;
            }

            foreach (var item in listChuongTrai)
            {
                string _khu = item.MaChuong.Substring(0, 1);
                
                if (_khu == khu)
                {
                    if (loaiChuong == "All")
                    {
                        IconChuongUC chuong = new IconChuongUC() { Tag = item };
                        chuong.tb_SoLuongHeo.Text = DataProvider.Ins.DB.CHUONGTRAIs.Where(x => x.MaChuong == item.MaChuong).SingleOrDefault().HEOs.Count().ToString();
                        chuong.tb_TenChuong.Text = item.MaChuong;
                        chuong.DataContext = new ChuongUC_VM() { _SoLuongHeo = Int16.Parse(chuong.tb_SoLuongHeo.Text) };
                        list.Add(chuong);
                    }
                    else if(_loaiChuong == item.MaLoaiChuong)
                    {
                        IconChuongUC chuong = new IconChuongUC() { Tag = item };
                        chuong.tb_SoLuongHeo.Text = DataProvider.Ins.DB.CHUONGTRAIs.Where(x => x.MaChuong == item.MaChuong).SingleOrDefault().HEOs.Count().ToString();
                        chuong.tb_TenChuong.Text = item.MaChuong;
                        chuong.DataContext = new ChuongUC_VM() { _SoLuongHeo = Int16.Parse(chuong.tb_SoLuongHeo.Text) };
                        list.Add(chuong);
                    }
                }
            }

            return list;
        }

        void Add_IconChuong_ToWP()
        {
            listIconChuongFinded = Search(wd.cbb_ChonKhu.SelectedItem as string, wd.cbb_ChonLoaiChuong.SelectedItem as string);

            wd.wp_ListChuong.Children.Clear();

            foreach (IconChuongUC chuong in listIconChuongFinded)
            {
                wd.wp_ListChuong.Children.Add(chuong);
            }

            TextSoChuong = "";
        }

        void TextSeacrh(string txt)
        {
            TextSoChuong = txt;

            if (String.IsNullOrWhiteSpace(TextSoChuong))
            {
                Add_IconChuong_ToWP(); return;
            }

            if (wd.wp_ListChuong == null) return;

            //List<IconChuongUC> list = wd.wp_ListChuong.Children.OfType<IconChuongUC>().ToList();

            wd.wp_ListChuong.Children.Clear();

            foreach (var item in listIconChuongFinded)
            {
                if (RemoveWhitespace(item.tb_TenChuong.Text).Contains(TextSoChuong))
                {
                    wd.wp_ListChuong.Children.Add(item);
                }
            }
        }

        public static string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

    }
}
