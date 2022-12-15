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

namespace QuanLyTraiHeo
{
    /// <summary>
    /// Interaction logic for QuanLyNhatKyWindow.xaml
    /// </summary>
    public partial class QuanLyNhatKyWindow : Window
    {
        public QuanLyNhatKyWindow()
        {
            InitializeComponent();
            ListNhatKy.Items.GroupDescriptions.Clear();
            ListNhatKy.Items.GroupDescriptions.Add(new PropertyGroupDescription("Ngay"));

        }

    }
}
