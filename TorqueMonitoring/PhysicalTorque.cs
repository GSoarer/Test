using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace TorqueMonitoring
{
    public class PhysicalTorque
    {

        private double T, durchmesserWelle, brückenspannung;

        public PhysicalTorque()
        {

        }



        public void doTorqueCalculations()
        {
            // Exponentialrechnung für Drehmomentrechnung
            double D3 = (Math.Pow(durchmesserWelle, 3.0));

            //
            double F = (41735.56);

            // Drehmoment berechnen
            T = (brückenspannung * D3 / F);

            
        }

        public string getT()
        {
            return T.ToString("N3", new CultureInfo("de-DE"));
        }

        public void setDurchmesserWelle(double durchmesserWelle)
        {
            this.durchmesserWelle = durchmesserWelle;
        }

        public void setBrückenspannung(double brückenspannung)
        {
            this.brückenspannung = brückenspannung;
        }
    }
}
