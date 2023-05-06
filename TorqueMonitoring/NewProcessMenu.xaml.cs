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
    /// Interaktionslogik für NewProcessMenu.xaml
    /// </summary>
    /// 

    public partial class NewProcessMenu : System.Windows.Controls.Page
    {

        TheoreticalTorque tt = new TheoreticalTorque();

        Microsoft.Office.Interop.Excel.Application excelApp = null;
        Workbook excelWorkbook = null;
        Sheets excelSheets = null;
        Worksheet xlws = null;
        string fileName = "SchnittdatenControl.xlsx";
        string currentSheet = "Tabelle1";


        private bool handle = true;

        double fz, kc1_1, mc;
        double vcHM = 0;
        double vcHSS = 0;


        private bool wsIsSelected = false;
        private bool dcIsEntered = false;

        private bool hmSelected = true;


        public NewProcessMenu()
        {
            InitializeComponent();

            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            string projectFolderPath = Directory.GetParent(assemblyPath).Parent.Parent.FullName;
            // Console.WriteLine("Project Folder Path: " + projectFolderPath);

            string filePath = projectFolderPath + "/files/" + fileName;

            excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelWorkbook = excelApp.Workbooks.Open(filePath);
            excelSheets = excelWorkbook.Worksheets;
            xlws = (Worksheet)excelSheets.get_Item(currentSheet);

            int i = 19;
            bool listEnd = false;
            string previous = "";

            
            while (!listEnd)
            {
                string current="";
                var currentVar = (xlws.Cells[i, "D"] as Range).Value;
                if(currentVar != null)
                {
                    current = currentVar.ToString();
                    if (!current.Equals(previous))
                    {
                        cbWerkstoffgruppen.Items.Add(current);
                        previous = current;

                    }
                }
                
                    
               

                if ((xlws.Cells[i+1, "E"] as Range).Value == null)
                {
                    listEnd = true;
                }
                i++;
            }

        }

        private void dc_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = dc_Input.Text;
            if (text == null || text.Equals(""))
            {
                dcIsEntered = false;

            }
            else
            {
                dcIsEntered = true;
                xlws.Cells[6, "D"] = double.Parse(text);
            }


            if (dcIsEntered && wsIsSelected)
            {
                cbSpezWerkstoff.IsEnabled = true;
            }
            else
            {
                cbSpezWerkstoff.IsEnabled = false;
            }
        }

        private void coating_Input_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)coating_Input.IsChecked)
            {
                ControlTemplate controlTemplate = coating_Input.Template;
                System.Windows.Shapes.Rectangle backgroundRectangle = (System.Windows.Shapes.Rectangle)controlTemplate.FindName("background", coating_Input);
                backgroundRectangle.Fill = new SolidColorBrush(Colors.Gold);
                coatingText.Text = "HSS-Beschichtung";

                BitmapImage image = new BitmapImage(new Uri("/assets/hss_bohrer.png", UriKind.Relative));
                drillImage.Source = image;

                hmSelected = false;

            }
            else
            {
                ControlTemplate controlTemplate = coating_Input.Template;
                System.Windows.Shapes.Rectangle backgroundRectangle = (System.Windows.Shapes.Rectangle)controlTemplate.FindName("background", coating_Input);
                backgroundRectangle.Fill = new SolidColorBrush(Colors.LightGreen);

                coatingText.Text = "HM-Beschichtung";

                BitmapImage image = new BitmapImage(new Uri("/assets/hm_bohrer.png", UriKind.Relative));
                drillImage.Source = image;

                hmSelected = true;

            }
        }


        private void cbWerkstoffgruppen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            cbSpezWerkstoff.Items.Clear();


            wsIsSelected = true;

            if (wsIsSelected && dcIsEntered)
            {
                cbSpezWerkstoff.IsEnabled = true;
            }
            


            //cbSpezWerkstoff.Items.Add(cbWerkstoffgruppen.SelectedItem);


                bool listEnd = false;
            int i = 19;
            while (!listEnd)
            {
                
                string currentMainGroup;
                var currentVar = (xlws.Cells[i, "D"] as Range).Value;
                if (currentVar != null)
                {
                   
                    currentMainGroup = currentVar.ToString();

                    if (currentMainGroup.Equals(cbWerkstoffgruppen.SelectedItem))
                    {

                        int f = 0;
                        bool spezEnd = false;
                        while (!spezEnd)
                        {
                            var currentSpezVar = (xlws.Cells[i + f, "E"] as Range).Value;
                            if (currentSpezVar != null)
                            {
                                cbSpezWerkstoff.Items.Add(currentSpezVar.ToString());
                            }
                            
                            if((xlws.Cells[i+f+1, "E"] as Range).Value == null)
                            {
                                spezEnd = true;
                                listEnd = true;
                            }

                            
                            var currentParallelVar = (xlws.Cells[i+f + 1, "D"] as Range).Value;
                            if (currentParallelVar != null)
                            {
                                spezEnd = true;
                                listEnd = true;
                            }
                            f++;
                        }
                       
                    }
                }

                if ((xlws.Cells[i + 1, "E"] as Range).Value == null)
                {
                    listEnd = true;
                }
                i++;

               

                    
                 
                
                
            }
           


        }

        private void cbSpezWerkstoff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Range searchRange = xlws.UsedRange;
            Range resultRange = searchRange.Find(cbSpezWerkstoff.SelectedItem);

            Range HSSCell = resultRange.Offset[0, 1]; // Offset by 1 column
            if (HSSCell.Value != null)
            {
                vcHSS = HSSCell.Value;
            }
            else
            {
                vcHSS = 0;
            }
           

            Range HMCell = resultRange.Offset[0, 2]; // Offset by 1 column
            if (HMCell.Value != null)
            {
                vcHM = HMCell.Value;
            }
            else
            {
                vcHM = 0;
            }

            Range FzCell = resultRange.Offset[0, 3];
            if (FzCell.Value != null)
            {
                fz = FzCell.Value;
            }
            else
            {
                fz = 0;
            }


            Range Kc1_1Cell = resultRange.Offset[0, 4];
            if (Kc1_1Cell.Value != null)
            {
                kc1_1 = Kc1_1Cell.Value;
            }
            else
            {
                kc1_1 = 0;
            }


            Range McCell = resultRange.Offset[0, 5];
            if (McCell.Value != null)
            {
                mc = McCell.Value;
            }
            else
            {
                mc = 0;
            }


            if (vcHSS == 0)
            {
                flipCoatingSwitchToFixedHM();
            }
            else
            {
                coating_Input.IsEnabled = true;

                if ((bool)coating_Input.IsChecked)
                {
                    ControlTemplate controlTemplate = coating_Input.Template;
                    System.Windows.Shapes.Rectangle backgroundRectangle = (System.Windows.Shapes.Rectangle)controlTemplate.FindName("background", coating_Input);
                    backgroundRectangle.Fill = new SolidColorBrush(Colors.Gold);

                   
                }
                else
                {

                    ControlTemplate controlTemplate = coating_Input.Template;
                    System.Windows.Shapes.Rectangle backgroundRectangle = (System.Windows.Shapes.Rectangle)controlTemplate.FindName("background", coating_Input);
                    backgroundRectangle.Fill = new SolidColorBrush(Colors.LightGreen);

                }
            }


            

        }

        public void flipCoatingSwitchToFixedHM()
        {
            
            
            coatingText.Text = "HM-Beschichtung";
            coating_Input.IsChecked = false;
            coating_Input.IsEnabled = false;

            ControlTemplate controlTemplate = coating_Input.Template;
            System.Windows.Shapes.Rectangle backgroundRectangle = (System.Windows.Shapes.Rectangle)controlTemplate.FindName("background", coating_Input);
            backgroundRectangle.Fill = new SolidColorBrush(Colors.LightGray);

            BitmapImage image = new BitmapImage(new Uri("/assets/hm_bohrer.png", UriKind.Relative));
            drillImage.Source = image;

            hmSelected = true;


        }

        public Tuple<double, double, double, double> getSchnittdaten()
        {

            double coatingReturn = vcHM;
            if (!hmSelected)
            {
                coatingReturn = vcHSS;
            }
            var returnTupel = new Tuple<double, double, double, double>(fz, coatingReturn, kc1_1, mc);
            return returnTupel;
        }


        public void killExcel()
        {
            excelWorkbook.Save();
            excelWorkbook.Close();
           
            excelApp.Quit();
            
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }


       
        
    }
}
