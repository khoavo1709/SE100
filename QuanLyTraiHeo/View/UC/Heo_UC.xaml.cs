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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyTraiHeo.View.UC
{
    /// <summary>
    /// Interaction logic for Heo_UC.xaml
    /// </summary>
    public partial class Heo_UC : UserControl
    {
        public Heo_UC()
        {
            InitializeComponent();
        }


        public event RoutedEventHandler Click;

        void onButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(this, e);
            }
        }
    }
}
