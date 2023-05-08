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

using System.IO;
using System.Reflection;

namespace TorqueMonitoring
{
    /// <summary>
    /// Interaction logic for OptionsMenu.xaml
    /// </summary>
    public partial class OptionsMenu : System.Windows.Controls.Page
    {
        public OptionsMenu()
        {
            InitializeComponent();
            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            string projectFolderPath = Directory.GetParent(assemblyPath).Parent.Parent.FullName;

        }
    }
}
