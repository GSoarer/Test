﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TorqueMonitoring
{
    /// <summary>
    /// Interaktionslogik für Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void addBohrprozess_Click(object sender, RoutedEventArgs e)
        {
            //titleTextBlock.Text = "Torque Monitoring";
            this.NavigationService.Navigate(new NewProcessMenu());
        }
    }
}
