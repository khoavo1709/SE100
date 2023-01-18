using MaterialDesignThemes.Wpf;
using FarmManagementSoftware.Model;
using FarmManagementSoftware.View.UC;
using FarmManagementSoftware.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace FarmManagementSoftware.ViewModel
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

        /// <summary>
        /// Hiệu ứng nhấp nháy
        /// </summary>
        /// <param name="image"></param>
        /// <param name="length"></param>
        /// <param name="repetition"></param>
        public void BlinkingImage(PackIcon image, int length, double repetition)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation { From = 1.0, To = 0.0, Duration = new Duration (TimeSpan. FromMilliseconds(length)), AutoReverse = true, RepeatBehavior = new RepeatBehavior(repetition)};
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(opacityAnimation);
            Storyboard.SetTarget(opacityAnimation, image);
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));
            storyboard.Begin(image);
        }

            void Load()
        {
            listArea = new List<string>();
            listTypePigsty = new List<string>();

            listTypePigsty.Add("Tất cả");
            foreach (var item in DataProvider.Ins.DB.LOAICHUONGs)
            {
                ListTypePigsty.Add(item.TenLoai);
            }

            wd.cbb_ChonLoaiChuong.ItemsSource = ListTypePigsty;

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
                List<LICHCHUONG> lcs = DataProvider.Ins.DB.LICHCHUONGs.Where(x => x.MaChuong == item.MaChuong && x.NgayLam.Day == DateTime.Today.Day && x.TrangThai == "Chưa làm").ToList();
                if (lcs != null)
                {
                    foreach (var lc in lcs)
                    {
                        chuong.IconChamThan.Visibility = Visibility.Visible;
                    }
                }
                list.Add(chuong);
            }

            return list;
        }

        List<IconChuongUC> Search(string loaiChuong)
        {
            List<IconChuongUC> list = new List<IconChuongUC>();

            if (loaiChuong == "Tất cả")
            {
                return LoadAll();
            }

            string _maLoaiChuong = DataProvider.Ins.DB.LOAICHUONGs.Where(x => x.TenLoai == loaiChuong).SingleOrDefault().MaLoaiChuong;

            foreach (var item in listChuongTrai)
            {
                if (_maLoaiChuong == item.MaLoaiChuong)
                {
                    IconChuongUC chuong = new IconChuongUC() { Tag = item };
                    chuong.tb_SoLuongHeo.Text = DataProvider.Ins.DB.CHUONGTRAIs.Where(x => x.MaChuong == item.MaChuong).SingleOrDefault().HEOs.Count().ToString();
                    chuong.tb_TenChuong.Text = item.MaChuong;
                    chuong.DataContext = new ChuongUC_VM() { _SoLuongHeo = Int16.Parse(chuong.tb_SoLuongHeo.Text) }; 
                    LICHCHUONG lc = DataProvider.Ins.DB.LICHCHUONGs.Where(x => x.MaChuong == item.MaChuong && x.NgayLam.Day == DateTime.Today.Day && x.TrangThai == "Chưa làm").SingleOrDefault();
                    if (lc != null)
                    {
                        chuong.IconChamThan.Visibility = Visibility.Visible;
                    }
                    list.Add(chuong);
                }
            }

            return list;
        }

        void Add_IconChuong_ToWP()
        {
            listIconChuongFinded = Search(wd.cbb_ChonLoaiChuong.SelectedItem as string);

            wd.wp_ListChuong.Children.Clear();

            foreach (IconChuongUC chuong in listIconChuongFinded)
            {
                wd.wp_ListChuong.Children.Add(chuong);

                if(chuong.IconChamThan.Visibility == Visibility.Visible)
                {
                    BlinkingImage(chuong.IconChamThan, 3000, 50.0);
                }
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
