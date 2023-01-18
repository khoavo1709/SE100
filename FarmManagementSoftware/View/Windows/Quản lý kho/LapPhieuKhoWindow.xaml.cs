﻿using System;
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

namespace FarmManagementSoftware
{
    /// <summary>
    /// Interaction logic for LapPhieuKhoWindow.xaml
    /// </summary>
    public partial class LapPhieuKhoWindow : Window
    {
        public LapPhieuKhoWindow()
        {
            InitializeComponent();
            dtp_DateMin.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtp_DateMax.SelectedDate = DateTime.Today;
        }
    }
}
