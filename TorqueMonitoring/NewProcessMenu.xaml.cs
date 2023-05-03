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

     
        private void cbWerkstoffgruppen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            cbSpezWerkstoff.Items.Clear();


            cbSpezWerkstoff.IsEnabled = true;


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
    }
}
