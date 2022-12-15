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
    /// Interaction logic for LichUC.xaml
    /// </summary>
    public partial class LichUC : UserControl
    {
        public LichUC()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler Click;

        void onButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(this, e);
                UpdateColor();
            }
        }
        public LichUC LichBeforSelected;
        void UpdateColor()
        {
            var bc = new BrushConverter();

            if (LichBeforSelected != null)
            {
                LichBeforSelected.border_Lich.BorderBrush = (Brush)bc.ConvertFrom("#54acf3");
            }

            this.border_Lich.BorderBrush = (Brush)bc.ConvertFrom("#FFAB91");
        }
    }
}
