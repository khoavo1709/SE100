using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace QuanLyTraiHeo.View.Windows
{
    /// <summary>
    /// Interaction logic for CapNhatTaiKhoanWindow.xaml
    /// </summary>
    public partial class CapNhatTaiKhoanWindow : Window
    {
        public CapNhatTaiKhoanWindow()
        {
            InitializeComponent();
        }

        #region Chỉ cho TextBox SDT và Hệ số lương nhập kí tự số
        private void tb_HeSoLuong_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (tb_HeSoLuong.Text.Length > 7) e.Handled = true;

            // Here e.Text is string so we need to convert it into char
            char ch = e.Text[0];

            if ((Char.IsDigit(ch) || ch == '.'))
            {
                //Here TextBox1.Text is name of your TextBox
                //int SoLuongDauChamChoPhep = tb_HeSoLuong.Text.Count(f => f == '.');
                if (ch == '.' && tb_HeSoLuong.Text.Contains('.'))
                    e.Handled = true;
            }
            else
                e.Handled = true;
        }

        private void tb_SDT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion

        private void btn_UpdateImage_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }
    }

    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, "Bắt buộc")
                : ValidationResult.ValidResult;
        }
    }
}
