/*using MaterialDesignThemes.Wpf;
using FarmManagementSoftware.Model;
using FarmManagementSoftware.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FarmManagementSoftware.View.Windows.Lập_lịch
{
    /// <summary>
    /// Interaction logic for SuaLichPhoiGiong.xaml
    /// </summary>
    public partial class SuaLichPhoiGiong : Window
    {
        LICHPHOIGIONG phoigiong;
        LapLichPhoiGiongVM lapLichPhoiGiongVM;
        public bool OK { get; set; }
        string temp { get; set; }
        static int check = 0;


        public SuaLichPhoiGiong(LICHPHOIGIONG LPG, LapLichPhoiGiongVM _lapLichPhoiGiongVM)
        {
            InitializeComponent();

            lapLichPhoiGiongVM = _lapLichPhoiGiongVM;
            OK = false;

            if (LPG != null)
            {
                phoigiong = LPG;
                Pigcode_textd.Text = LPG.MaHeoDuc;
                Pigcode_textn.Text = LPG.MaHeoCai;
                Datepicker_Ngayphoigiong.SelectedDate = LPG.NgayPhoiGiong;
                TrangThai.Text = LPG.Trangthai;
                Datepicker_ngayde.SelectedDate = LPG.NgayDuKienDe;
                Ngaycaisua.SelectedDate = LPG.NgayCaiSua;
                Ngaydethucte.SelectedDate = LPG.NgayDeThucTe;
                NgayPhoiGiongLai.SelectedDate = LPG.NgayPhoiGiongLaiDuKien;
                Socon.Text = LPG.SoCon.ToString();
                Soconchon.Text = LPG.SoConChon.ToString();
                Sochet.Text = LPG.SoConChet.ToString();

                SetUp(LPG.Trangthai);

                if (LPG.Trangthai == "Đã đẻ")
                {
                    TrangThai.IsEnabled = false;
                }
            }
            else
            {
                Datepicker_Ngayphoigiong.SelectedDate = DateTime.Today; Datepicker_ngayde.IsEnabled = false;
                Ngaycaisua.IsEnabled = true;
                Ngaydethucte.IsEnabled = false;
                NgayPhoiGiongLai.IsEnabled = false;
                Socon.IsEnabled = false;
                Soconchon.IsEnabled = false;
                Sochet.IsEnabled = false;
            }


        }
        public LICHPHOIGIONG returnValue()
        {
            if (check == 1)
            {
                LICHPHOIGIONG phoigiong = new LICHPHOIGIONG();
                phoigiong.MaLichPhoi = temp;
                phoigiong.MaHeoDuc = Pigcode_textd.Text;
                phoigiong.MaHeoCai = Pigcode_textn.Text;
                phoigiong.NgayPhoiGiong = Datepicker_Ngayphoigiong.SelectedDate;
                phoigiong.Trangthai = TrangThai.Text;
                phoigiong.NgayDuKienDe = Datepicker_ngayde.SelectedDate;
                phoigiong.NgayCaiSua = Ngaycaisua.SelectedDate;
                phoigiong.NgayDeThucTe = Ngaydethucte.SelectedDate;
                phoigiong.NgayPhoiGiongLaiDuKien = NgayPhoiGiongLai.SelectedDate;
                if (Socon.Text == "")
                {
                    phoigiong.SoCon = null;
                }
                else
                {
                    phoigiong.SoCon = int.Parse(Socon.Text);
                }

                if (Soconchon.Text == "")
                {
                    phoigiong.SoConChon = null;
                }
                else
                {
                    phoigiong.SoConChon = Convert.ToInt16(Soconchon.Text);
                }

                if (Sochet.Text == "")
                {
                    phoigiong.SoConChet = null;
                }
                else
                {
                    phoigiong.SoConChet = Convert.ToInt16(Sochet.Text);
                }

                return phoigiong;
            }
            return null;
        }
        private void ListHeod_button_Click(object sender, RoutedEventArgs e)
        {
            ShowListHeod();
        }

        private void ListHeon_button_Click(object sender, RoutedEventArgs e)
        {
            ShowListHeoc();
        }


        void ShowListHeod()
        {
            DanhsachHeoDuc duc = new DanhsachHeoDuc();
            duc.ShowDialog();
            Pigcode_textd.Text = duc.TranferCode();
        }

        void ShowListHeoc()
        {
            DanhsachHeoCai cai = new DanhsachHeoCai();
            cai.ShowDialog();
            Pigcode_textn.Text = cai.TranferCode();
        }

        private void Confirm_button_Click(object sender, RoutedEventArgs e)
        {
            if (Datepicker_Ngayphoigiong.SelectedDate.Value < DateTime.Today)
            {
                MessageBox.Show("Ngày giao phối phải từ hôm nay trở đi");
                return;
            }


            if (phoigiong == null)
            {
                LICHPHOIGIONG lpg = DataProvider.Ins.DB.LICHPHOIGIONGs.Where(x => x.MaHeoCai == Pigcode_textn.Text && x.Trangthai == "Chưa phối giống").SingleOrDefault();
                if (lpg != null)
                {
                    MessageBox.Show(String.Format("Heo cái này đang có một lịch phối giống khác vào ngày {0} chưa được thực hiện", lpg.NgayPhoiGiong));
                    return;
                }


                try
                {
                    LICHPHOIGIONG lich = new LICHPHOIGIONG();

                    lich.MaLichPhoi = Lichphoigiongcode_generate();
                    lich.MaHeoDuc = Pigcode_textd.Text;
                    lich.MaHeoCai = Pigcode_textn.Text;
                    lich.NgayPhoiGiong = Datepicker_Ngayphoigiong.SelectedDate;
                    lich.Trangthai = TrangThai.Text;
                    lich.NgayDuKienDe = Datepicker_ngayde.SelectedDate;
                    lich.NgayCaiSua = Ngaycaisua.SelectedDate;
                    lich.NgayDeThucTe = Ngaydethucte.SelectedDate;
                    lich.NgayPhoiGiongLaiDuKien = NgayPhoiGiongLai.SelectedDate;
                    lich.SoCon = 0;
                    lich.SoConChon = 0;
                    lich.SoConChet = 0;

                    DataProvider.Ins.DB.LICHPHOIGIONGs.Add(lich);

                    DataProvider.Ins.DB.SaveChanges();

                    OK = true;

                }
                catch (Exception exception)
                {
                    MessageBox.Show("Có lỗi xảy ra khi thêm");
                }
            }
            else
            {
                phoigiong.MaHeoDuc = Pigcode_textd.Text;
                phoigiong.MaHeoCai = Pigcode_textn.Text;
                phoigiong.NgayPhoiGiong = Datepicker_Ngayphoigiong.SelectedDate;
                phoigiong.Trangthai = TrangThai.Text;
                phoigiong.NgayDuKienDe = Datepicker_ngayde.SelectedDate;
                phoigiong.NgayCaiSua = Ngaycaisua.SelectedDate;
                phoigiong.NgayDeThucTe = Ngaydethucte.SelectedDate;
                phoigiong.NgayPhoiGiongLaiDuKien = NgayPhoiGiongLai.SelectedDate;
                if (Socon.Text == "")
                {
                    phoigiong.SoCon = null;
                }
                else
                {
                    phoigiong.SoCon = int.Parse(Socon.Text);
                }

                if (Soconchon.Text == "")
                {
                    phoigiong.SoConChon = null;
                }
                else
                {
                    phoigiong.SoConChon = Convert.ToInt16(Soconchon.Text);
                }

                if (Sochet.Text == "")
                {
                    phoigiong.SoConChet = null;
                }
                else
                {
                    phoigiong.SoConChet = Convert.ToInt16(Sochet.Text);
                }

                DataProvider.Ins.DB.SaveChanges();
                OK = true;
            }

            this.Close();
        }

        private void Huy_button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chăc muốn hủy bỏ mọi thay đổi?", "Chú ý!", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Datepicker_Ngayphoigiong_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            THAMSO thamso = DataProvider.Ins.DB.THAMSOes.SingleOrDefault();

            Datepicker_ngayde.SelectedDate = Datepicker_Ngayphoigiong.SelectedDate.Value.AddDays(90);
            //Ngaycaisua.SelectedDate = Datepicker_Ngayphoigiong.SelectedDate.Value.AddDays(thamso.);
            //NgayPhoiGiongLai.SelectedDate = Datepicker_Ngayphoigiong.SelectedDate.Value.AddDays(90);
        }

        private void TrangThai_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((TrangThai.SelectedItem as ComboBoxItem).Content != null)
            {
                string trangThai = (TrangThai.SelectedItem as ComboBoxItem).Content.ToString();
                SetUp(trangThai);
            }
        }

        void SetUp(string trangThai)
        {
            Pigcode_textd.IsEnabled = true;
            Pigcode_textn.IsEnabled = true;
            Datepicker_Ngayphoigiong.IsEnabled = true;
            TrangThai.IsEnabled = true;
            Datepicker_ngayde.IsEnabled = false;
            Ngaycaisua.IsEnabled = false;
            Ngaydethucte.IsEnabled = false;
            NgayPhoiGiongLai.IsEnabled = true;
            Socon.IsEnabled = false;
            Soconchon.IsEnabled = false;
            Sochet.IsEnabled = false;

            if (trangThai == "Đã xảy thai" || trangThai == "Đã mang thai")
            {
                NgayPhoiGiongLai.IsEnabled = false;
            }
            else if (trangThai == "Đã đẻ")
            {
                Pigcode_textd.IsEnabled = true;
                Pigcode_textn.IsEnabled = true;
                Datepicker_Ngayphoigiong.IsEnabled = true;
                TrangThai.IsEnabled = true;
                Datepicker_ngayde.IsEnabled = false;
                Ngaycaisua.IsEnabled = true;
                Ngaydethucte.IsEnabled = false;
                NgayPhoiGiongLai.IsEnabled = false;
                Socon.IsEnabled = true;
                Soconchon.IsEnabled = true;
                Sochet.IsEnabled = true;

                Ngaydethucte.SelectedDate = DateTime.Today;
            }
        }

        string Lichphoigiongcode_generate()
        {
            int count = DataProvider.Ins.DB.LICHPHOIGIONGs.Count() + 1;
            return "PG" + count;
        }
    }
}


*/

using FarmManagementSoftware.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FarmManagementSoftware.View.Windows.Lập_lịch
{
    /// <summary>
    /// Interaction logic for SuaLichPhoiGiong.xaml
    /// </summary>
    public partial class SuaLichPhoiGiong : Window
    {
        string temp { get; set; }
        static int check = 0;

        public SuaLichPhoiGiong(LICHPHOIGIONG LPG)
        {
            InitializeComponent();
            temp = LPG.MaLichPhoi;
            Pigcode_textd.Text = LPG.MaHeoDuc;
            Pigcode_textn.Text = LPG.MaHeoCai;
            Datepicker_Ngayphoigiong.SelectedDate = LPG.NgayPhoiGiong;
            TrangThai.Text = LPG.Trangthai;
            Datepicker_ngayde.SelectedDate = LPG.NgayDuKienDe;
            Ngaycaisua.SelectedDate = LPG.NgayCaiSua;
            Ngaydethucte.SelectedDate = LPG.NgayDeThucTe;
            NgayPhoiGiongLai.SelectedDate = LPG.NgayPhoiGiongLaiDuKien;
            Socon.Text = LPG.SoCon.ToString();
            Soconchon.Text = LPG.SoConChon.ToString();
            Sochet.Text = LPG.SoConChet.ToString();
        }

        private void ListHeod_button_Click(object sender, RoutedEventArgs e)
        {
            ShowListHeod();
        }

        private void ListHeon_button_Click(object sender, RoutedEventArgs e)
        {
            ShowListHeoc();
        }


        void ShowListHeod()
        {
            DanhsachHeoDuc duc = new DanhsachHeoDuc();
            duc.ShowDialog();
            Pigcode_textd.Text = duc.TranferCode();
        }

        void ShowListHeoc()
        {
            DanhsachHeoCai cai = new DanhsachHeoCai();
            cai.ShowDialog();
            Pigcode_textn.Text = cai.TranferCode();
        }

        private void Confirm_button_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
            this.Close();
        }

        public LICHPHOIGIONG returnValue()
        {
            if (check == 1)
            {
                LICHPHOIGIONG phoigiong = new LICHPHOIGIONG();
                phoigiong.MaLichPhoi = temp;
                phoigiong.MaHeoDuc = Pigcode_textd.Text;
                phoigiong.MaHeoCai = Pigcode_textn.Text;
                phoigiong.NgayPhoiGiong = Datepicker_Ngayphoigiong.SelectedDate;
                phoigiong.Trangthai = TrangThai.Text;
                phoigiong.NgayDuKienDe = Datepicker_ngayde.SelectedDate;
                phoigiong.NgayCaiSua = Ngaycaisua.SelectedDate;
                phoigiong.NgayDeThucTe = Ngaydethucte.SelectedDate;
                phoigiong.NgayPhoiGiongLaiDuKien = NgayPhoiGiongLai.SelectedDate;
                if (Socon.Text == "")
                {
                    phoigiong.SoCon = null;
                }
                else
                {
                    phoigiong.SoCon = int.Parse(Socon.Text);
                }

                if (Soconchon.Text == "")
                {
                    phoigiong.SoConChon = null;
                }
                else
                {
                    phoigiong.SoConChon = Convert.ToInt16(Soconchon.Text);
                }

                if (Sochet.Text == "")
                {
                    phoigiong.SoConChet = null;
                }
                else
                {
                    phoigiong.SoConChet = Convert.ToInt16(Sochet.Text);
                }

                return phoigiong;
            }
            return null;
        }

        private void Huy_button_Click(object sender, RoutedEventArgs e)
        {
            check = 0;
            this.Close();
        }
    }
}