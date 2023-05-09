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
using Microsoft.Office.Interop.Excel;
using System.Configuration;
using System.ComponentModel;

using System.IO;
using System.Reflection;

namespace TorqueMonitoring
{
    /// <summary>
    /// Interaction logic for OptionsMenu.xaml
    /// </summary>
    /// 
    public partial class OptionsMenu : System.Windows.Controls.Page
    {
        PhysicalTorque pt = new PhysicalTorque();

        private bool wdIsChanged = false;
        private bool VIsChanged = false;
        private double durchmesser_d;
        private string durchmesser_s;
        private double spannung_d;
        private string spannung_s;
        public double V_InputValue { get; set; }
        public double wd_InputValue { get; set; }


        public OptionsMenu()
        {
            InitializeComponent();
            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            string projectFolderPath = Directory.GetParent(assemblyPath).Parent.Parent.FullName;
            //durchmesser = Properties.Settings.Default.durchmesser;
            //spannung = Properties.Settings.Default.spannung;
            //DataContext = this;

            durchmesser_d = Properties.Settings.Default.durchmesser;
            wd_Input.SetBinding(System.Windows.Controls.TextBox.TextProperty, new Binding("durchmesser")
            { Source = Properties.Settings.Default, Mode = BindingMode.TwoWay });
            wd_Input.Text = durchmesser_d.ToString();

            spannung_d = Properties.Settings.Default.spannung;
            V_Input.SetBinding(System.Windows.Controls.TextBox.TextProperty, new Binding("spannung")
            { Source = Properties.Settings.Default, Mode = BindingMode.TwoWay });
            V_Input.Text = spannung_d.ToString();
        }
   
        private void wd_Input_TextChanged(object sender, TextChangedEventArgs e)
        {

            double neuer_wert_wd;
            if (double.TryParse(wd_Input.Text, out neuer_wert_wd))
            {
                durchmesser_d = neuer_wert_wd;
                Properties.Settings.Default.durchmesser = durchmesser_d;
            }           
        }
        private void V_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            double neuer_wert_V;
            if (double.TryParse(V_Input.Text, out neuer_wert_V))
            {
                spannung_d = neuer_wert_V;
                Properties.Settings.Default.spannung = spannung_d;
            }
        }

        private void Speichern_Click(object sender, RoutedEventArgs e)
        {
            V_InputValue = Convert.ToDouble(V_Input.Text);
            wd_InputValue = Convert.ToDouble(wd_Input.Text);

            Properties.Settings.Default.spannung = V_InputValue;
            Properties.Settings.Default.durchmesser = wd_InputValue;
            Properties.Settings.Default.Save();
        }
    }
}
