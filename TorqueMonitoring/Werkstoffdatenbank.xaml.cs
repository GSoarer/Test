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

using Microsoft.Office.Interop.Excel;

using System.Windows.Shapes;
using System.IO;


namespace TorqueMonitoring
{
    /// <summary>
    /// Interaktionslogik für Werkstoffdatenbank.xaml
    /// </summary>
    public partial class Werkstoffdatenbank : System.Windows.Controls.Page
    {

        double d = 10;
        double fz, vc, kc1_1, mc;


        Boolean isReadOnly = true;


        Microsoft.Office.Interop.Excel.Application excelApp = null;
        Workbook excelWorkbook = null;
        Sheets excelSheets = null;
        Worksheet xlws = null;

        string fileName = "SchnittdatenControl.xlsx";
        string filePath;

        string currentSheet = "Tabelle1";



        public Werkstoffdatenbank()
        {
            InitializeComponent();




            filePath = System.IO.Path.Combine(Environment.CurrentDirectory, fileName);
            excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelWorkbook = excelApp.Workbooks.Open(filePath);
            excelSheets = excelWorkbook.Worksheets;
            xlws = (Worksheet)excelSheets.get_Item(currentSheet);

            fillWDB();

            

        }


        public void setReadOnly(Boolean isReadOnly)
        {
            Editability _editability = new Editability()
            {
                ReadOnly = isReadOnly
            };
            
            this.DataContext = _editability;

        }

        public void setD(double d)
        {
            this.d = d;
        }


        private void labelWerkstoff_1_1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            retreiveSchnittdatenFromExcel(d, 1);
        }

        private void fillWDB()
        {
           

            labelWerkstoff_1.Text = (xlws.Cells[5, "H"] as Range).Value.ToString();
            labelWerkstoff_1_1.Text = (xlws.Cells[5, "I"] as Range).Value.ToString();

            labelWerkstoff_2.Text = (xlws.Cells[6, "H"] as Range).Value.ToString();
            labelWerkstoff_2_1.Text = (xlws.Cells[6, "I"] as Range).Value.ToString();

            labelWerkstoff_3.Text = (xlws.Cells[6, "H"] as Range).Value.ToString();
            labelWerkstoff_3_1.Text = (xlws.Cells[6, "I"] as Range).Value.ToString();

        }

     

        private void retreiveSchnittdatenFromExcel(double dc, int werkstoff)
        {

            xlws.Cells[2, "D"] = dc;
            xlws.Cells[3, "D"] = werkstoff;

           
            vc = (double)(xlws.Cells[7, "D"] as Range).Value;
            fz = (double)(xlws.Cells[9, "D"] as Range).Value;
            kc1_1 = (double)(xlws.Cells[10, "D"] as Range).Value;
            mc = (double)(xlws.Cells[11, "D"] as Range).Value;

            Editability _editability = new Editability()
            {
                Schnittdaten = "Schnittdaten: " + "\nVc: " + vc
                + "\nfz: " + fz
                + "\nkc1_1: " + kc1_1
                + "\nmc: " + mc

            };

            this.DataContext = _editability;
        }


        public Tuple<double, double, double, double> getSchnittdaten()
        {
            var returnTupel = new Tuple<double, double, double, double>(fz, vc, kc1_1, mc);
            return returnTupel;
        }

        

        public void closeExcel()
        {
            excelWorkbook.Close(false);
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }

        public void saveExcel()
        {

        }

       
    }



    class Editability
    {
        public Editability()
        {
           
        }

        public Boolean ReadOnly { get; set; }

        public string Schnittdaten { get; set; }
       
    }
}
