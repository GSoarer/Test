using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.IO;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace TorqueMonitoring
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        TheoreticalTorque tt = new TheoreticalTorque();
        PhysicalTorque pt = new PhysicalTorque();

        

        Werkstoffdatenbank wdb = null;

        public NewProcessMenu np;

        public OptionsMenu opt;

        public MainWindow()
        {
            InitializeComponent();
            

            pt.setBrückenspannung(10);
            pt.setDurchmesserWelle(30);


            Loaded += MyWindow_Loaded;
            Closing += MainWindow_Closing;


        }

        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new Page1());
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
                if (np != null)
                {
                    // Call the method on the existing page object
                    np.killExcel();
                }
            
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            titleTextBlock.Text = "Select Process";
            frame.NavigationService.Navigate(new Page1());

            wdbSideBar.Visibility = Visibility.Hidden;
            newProcessMenuSideBar.Visibility = Visibility.Hidden;
            
            homeScreenSideBar.Visibility = Visibility.Visible;

            //label.Content = np.d_Input.Text.ToString();
            
        }

        private void database_Click(object sender, RoutedEventArgs e)
        {



            /* wdb = new Werkstoffdatenbank();
             wdb.setReadOnly(true);

            // titleTextBlock.Text = "Werkstoffdatenbank";
            // frame.NavigationService.Navigate(wdb);

             /*
             wdbSideBar.Visibility = Visibility.Visible;
             homeScreenSideBar.Visibility = Visibility.Hidden;
             newProcessMenuSideBar.Visibility = Visibility.Hidden;
             */
            string fileName = "SchnittdatenControl.xlsx";
            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            string projectFolderPath = Directory.GetParent(assemblyPath).Parent.Parent.FullName;
            // Console.WriteLine("Project Folder Path: " + projectFolderPath);

            string filePath = projectFolderPath + "/files/" + fileName;

            Process.Start(filePath);

        }

        private void addBohrprozessButton_Click(object sender, RoutedEventArgs e)
        {
            np = new NewProcessMenu();

            titleTextBlock.Text = "Parameter eingeben";
            
            frame.NavigationService.Navigate(np);

            wdbSideBar.Visibility = Visibility.Hidden;
            homeScreenSideBar.Visibility = Visibility.Hidden;
            newProcessMenuSideBar.Visibility = Visibility.Visible;
           
        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {
            opt = new OptionsMenu();

            titleTextBlock.Text = "Optionen";

            frame.NavigationService.Navigate(opt);

            wdbSideBar.Visibility = Visibility.Hidden;
            homeScreenSideBar.Visibility = Visibility.Hidden;
            newProcessMenuSideBar.Visibility = Visibility.Hidden;
        }

        private void editWDBButton_Click(object sender, RoutedEventArgs e)
        {

            wdb.setReadOnly(false);
            
            

            editWDBButton.Visibility = Visibility.Hidden;
            finishEditWDBButton.Visibility = Visibility.Visible;

        }

        private void finishEditWDBButton_Click(object sender, RoutedEventArgs e)
        {
            wdb.setReadOnly(true);

            

            editWDBButton.Visibility = Visibility.Visible;
            finishEditWDBButton.Visibility = Visibility.Hidden;
         

        }

       

        private void startMonitoringButton_Click(object sender, RoutedEventArgs e)
        {
           


            tt.setWerkstoff(np.getSchnittdaten());
            tt.setBohrerspitzenwinkel(double.Parse(np.a_Input.Text));
            tt.setSchneiddurchmesser(double.Parse(np.dc_Input.Text));
            tt.setSchneidenanzahl(int.Parse(np.z_Input.Text));

            

            np.killExcel();
          
            titleTextBlock.Text = "Torque Monitoring";
            frame.NavigationService.Navigate(new MonitoringPage(tt, pt));
          

        }

       
    }
}
