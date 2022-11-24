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

namespace FarmManagementSoftware.View.Windows
{
    /// <summary>
    /// Interaction logic for ThietLapCayMucTieuWindow.xaml
    /// </summary>
    public partial class ThietLapCayMucTieuWindow : Window
    {
        CayMucTieu cmt = new CayMucTieu();
        Object obj;
        static int check = 0;
        public ThietLapCayMucTieuWindow()
        {
            InitializeComponent();
            cmt.Close();
            obj = cmt.Content;
            cmt.Content = null;
            showmake.Children.Clear();
            showmake.Children.Add(obj as UIElement);
        }

    }

}
