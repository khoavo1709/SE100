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

namespace QuanLyTraiHeo.View.Windows
{
    /// <summary>
    /// Interaction logic for wSoDo.xaml
    /// </summary>
    public partial class wSoDo : Window
    {
        public wSoDo()
        {
            InitializeComponent();
        }

        private void Txt_SoChuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                Txt_SoChuong.Text = " ";
            }
        }
    }
}
