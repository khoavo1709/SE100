using QuanLyTraiHeo.Model;
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

namespace QuanLyTraiHeo.View.Windows.Lập_lịch
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
