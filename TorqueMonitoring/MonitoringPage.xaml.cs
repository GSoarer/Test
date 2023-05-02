using System;
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
    /// Interaktionslogik für MonitoringPage.xaml
    /// </summary>
    public partial class MonitoringPage : Page
    {
        

        public MonitoringPage(TheoreticalTorque tt, PhysicalTorque pt)
        {

            InitializeComponent();



            tt.doTorqueCalculations();
            pt.doTorqueCalculations();


            ttResultLabel.Content = tt.getT();
            ptResultLabel.Content = pt.getT();


        }
    }
}
