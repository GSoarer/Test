using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Office.Interop.Excel;

using System.Runtime.InteropServices;


using System.IO;

namespace TorqueMonitoring
{
    class ExcelAnbindung
    {
        Application excelApp = null;
        Workbook excelWorkbook = null;
        Sheets excelSheets = null;
        Worksheet xlws = null;
        public ExcelAnbindung()
        {
            string fileName = "SchnittdatenControl.xlsx";
            string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

            excelApp = new Application();
            excelWorkbook = excelApp.Workbooks.Open(filePath);
            excelSheets = excelWorkbook.Worksheets;


            string currentSheet = "Tabelle1";

            xlws = (Worksheet)excelSheets.get_Item(currentSheet);

           excelWorkbook.Save();
           excelWorkbook.Close(true);
           
        }

        public string getText(int zeile, string spalte)
        {

             



             




           
            


           // xlws.Cells[2, "D"] = 10;
            //xlws.Cells[3, "D"] = 1;



            var text = (xlws.Cells[zeile, spalte] as Range).Value;
            
            return text.ToString();
            //return "12345";
            
           
        }
        

    }
}
