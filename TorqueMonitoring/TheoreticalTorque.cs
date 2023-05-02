using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace TorqueMonitoring
{
    public class TheoreticalTorque
    {

        //Deklarierung Schneiddurchmesser Dc, Bohrerspitzenwinkel a, Zahnvorschub fz, Schnittgeschwindigkeit vc, spez. Schnittkraft kc, Anstiegswert mc
        private double Dc, a, fz, vc, kc, mc, n, f, vf, Q, k, h, s, T;

        //Deklarierung Schneidenanzahl
        private int z;
        

        
        //Konstruktor der TheoreticalTorque
        public TheoreticalTorque()
        {
        }


        //Setzen des Schneiddurchmessers
        public void setSchneiddurchmesser(double Dc)
        {
            this.Dc = Dc;
        }

        //Setzen der Schneidenanzahl
        public void setSchneidenanzahl(int z)
        {
            this.z = z;
        }

        //Setzen des Bohrerspitzenwinkels
        public void setBohrerspitzenwinkel(double a)
        {
            this.a = a;
        }


        //Setzen der Werkstoff-Schnittdaten des ausgewählten Werkstoffes
        public void setWerkstoff(Tuple<double, double, double, double> werkstoffdaten)
        {

            this.fz = werkstoffdaten.Item1;
            this.vc = werkstoffdaten.Item2;
            this.kc = werkstoffdaten.Item3;
            this.mc = werkstoffdaten.Item4;
            
        }



        //Berechnung des thoretisch zu erwartenden Drehmomentes anhand vorher gesetzter Eingangsvariablen Dc, z, a
        public void doTorqueCalculations()
        {
           

            //Drehzahl berechnen
            
            n = ((vc * 1000) / (Dc * Math.PI));
            


            //Vorschub pro Umdrehung berechnen
            
            f = (fz * z);
           


            //Vorschubgeschwindigkeit berechnen
            
            vf = (f * n);
            

            //Zeitspanvolumen
            
            Q = (vf * Math.PI * (Dc * Dc)) / (4 * 1000);
           


            //Einstellwinkel berechnen
            
            k = (180 - a) / 2;
            k = Math.PI * k / 180;

            //Spanungsdicke berechnen

            h = fz * Math.Sin(k);


            //spezifische Schnittkraft berechnen
            
            s = kc / (Math.Pow(h, mc));

            //Drehmoment berechnen
            
            T = (Dc * Dc * s * f) / 8000;

 
        }

        public string getVc()
        {
            
            return vc.ToString("N3", new CultureInfo("de-DE"));
        }

        public string getN()
        {
            return n.ToString("N3", new CultureInfo("de-DE"));
        }

        public string getFz()
        {
            return fz.ToString("N3", new CultureInfo("de-DE"));
        }

        public string getF()
        {
            return f.ToString("N3", new CultureInfo("de-DE"));
        }

        public string getVf()
        {
            return vf.ToString("N3", new CultureInfo("de-DE"));
        }
        public string getQ()
        {
            return Q.ToString("N3", new CultureInfo("de-DE"));
        }

        public string getH()
        {
            return h.ToString("N3", new CultureInfo("de-DE"));
        }
        public string getS()
        {
            return s.ToString("N3", new CultureInfo("de-DE"));
        }
        public string getT()
        {
            return T.ToString("N3", new CultureInfo("de-DE"));
        }

    }
}
