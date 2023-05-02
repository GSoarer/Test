using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorqueMonitoring
{
    class Schnittdatenassistent
    {

        //Deklarierung der Auswahlnummern der Werkstoff-Übergruppen und der spezifischen Werkstoffe
        int werkstoffÜbergruppenAuswahl, werkstoffSpezifischeAuswahl;
        string gewählterSpezifischerWerkstoff;


        /*
        * 
        * 
        * ++++++++++++++DATENBANK VERWALTEN:
        * 
        */

        private string[] werkstoffÜbergruppen = new string[] {      "Stahl",
                                                                    "Nichtrostender Stahl",
                                                                    "Gusseisen",
                                                                    "NE-Metalle",
                                                                    "Schwer zerspanbare Werkstoffe",
                                                                    "Harte Werkstoffe",
                                                                    "Andere",
                                                                    "Schnittwerte manuell festlegen"};


        private string[] stahlSorten = new string[] {               "Unlegierte und niedriglegierte Stähle, C > 0,25%,niedrige und mittlere Festigkeit",
                                                                    "Unlegierte und niedriglegierte Stähle, C > 0,55%, nicht vergütet",
                                                                    "Niedrig- und hochlegierte Stähle, niedrige Vergütungsstufe",
                                                                    "Rostfreie ferritische / martensitische Stähle, vergütet",
                                                                    "Niedrig- und hochlegierte Stähle, mittlere Vergütungsstufe",
                                                                    "Niedrig- und hochlegierte Stähle, hohe Vergütungsstufe"};

        string[] nichtrostendeStahlSorten = new string[] {          "Nichtrostende, austenitische Stähle",
                                                                    "Nichtrostende, austenitische / ferritische Stähle + Duplex",
                                                                    "Nichtrostende, austenitische Stähle, ausscheidungsgehärtet (PH-Stähle)"};

        string[] gusseisenSorten = new string[] {                   "Grauguss + CGI + Temperguss, niedrige Festigkeit",
                                                                    "Kugelgraphitguss niedrige Festigkeit + Temperguss höhere Festigkeit",
                                                                    "Grauguss höhere Festigkeit",
                                                                    "Kugelgraphitguss hohe Festigkeit + ADI hohe Festigkeit, unlegiert + legiert"};

        string[] neMetallSorten = new string[] {                    "Aluminium-Knetlegierung, nicht ausgehärtet",
                                                                    "Aluminium-Knetlegierung, augehärtet",
                                                                    "Aluminium-Gusslegierung < 12% Si, nicht ausgehärtet",
                                                                    "Aluminium-Gusslegierung < 12% Si, ausgehärtet, Aluminium-Gusslegierung >= 12%",
                                                                    "Rein-Kupfer, Kupferlegierung (Messing, Bronze) mit niedriger Festigkeit",
                                                                    "Hochfeste Kupferlegierungen, Bronze hoher Festigkeit"};

        string[] schwerZerspanbareWSSorten = new string[] {         "Warmfeste Legierungen, Eisen-Basis, geglüht",
                                                                    "Warmfeste Legierungen, Eisen-Basis, ausgehärtet",
                                                                    "Rein-Titan",
                                                                    "Titanlegierungen, Alpha-, Alpha/Beta- und Betalegierungen",
                                                                    "Warmfeste Legierungen, Nickel-Cobalt-Basis, geglüht",
                                                                    "Warmfeste Legierungen, Nickel-Cobalt-Basis, ausgehärtet",
                                                                    "Warmfeste Legierungen, Nickel-Cobalt-Basis, gegossen"};

        string[] harteWSSorten = new string[] {                     "Gehärtete Stähle 46 - 52 HRC",
                                                                    "Gehärtete Stähle 52 - 58 HRC",
                                                                    "Gehärtete Stähle 58 - 62 HRC",
                                                                    "Gehärtetes Gusseisen 50 - 60 HRC"};

        string[] andereSorten = new string[] {                      "Thermo- und Duroplaste, ohne abrasive Füllstoffe",
                                                                    "Faserverstärkte Kunststoffe",
                                                                    "Graphit"};


        //Spezifische Materialwerte der spezifischen Werkstoffe (in Reihenfolge innerhalb der Werkstoff-Übergruppen gemäß oben)
        double[] kcWerteStahl = new double[] { 1500, 1700, 2000, 2200, 2500, 3000 }; //spezifische Schnittkräfte der Stahlwerkstoffe
        double[] mcWerteStahl = new double[] { 0.21, 0.25, 0.25, 0.25, 0.25, 0.25 }; //spezifische Anstiegswerte der Stahlwerkstoffe


        double[] kcWerteNichtrostenderStahl = new double[] {1800, 2000, 2400 };
        double[] mcWerteNichtrostenderStahl = new double[] { 0.21, 0.21, 0.21 };


        double[] kcWerteGusseisen = new double[] { 800, 950, 1200, 1400, 3500 }; //spezifische Schnittkräfte der Gusseisenwerkstoffe
        double[] mcWerteGusseisen = new double[] { 0.28, 0.28, 0.28, 0.28, 0.25 }; //spezifische Anstiegswerte der Gusseisenwerkstoffe


        double[] kcWerteNEMetalle = new double[] { 350, 600, 600, 700, 550, 1000 }; //spezifische Schnittkräfte der NE-Werkstoffe
        double[] mcWerteNEMetalle = new double[] { 0.25, 025, 0.25, 0.25, 0.25, 0.25 }; //spezifische Anstiegswerte der NE-Werkstoffe


        double[] kcWerteSchwerZerspanbareWS = new double[] { 2400, 2500, 1300, 1500, 2800, 2900, 3000 }; //spezifische Schnittkräfte der schwer zerspanbaren WS
        double[] mcWerteSchwerZerspanbareWS = new double[] { 0.25, 0.25, 0.25, 0.25, 0.25, 0.25, 0.25 }; //spezifische Anstiegswerte der schwer zerspanbaren WS


        double[] kcWerteHarteWS = new double[] { 3000, 3700, 4300, 3500 }; //spezifische Schnittkräfte harter Werkstoffe
        double[] mcWerteHarteWS = new double[] { 0.25, 0.25, 0.25, 0.25 }; //spezifische Anstiegswerte harter Werkstoffe


        double[] kcWerteAndere = new double[] { 150, 300, 400 }; //spezifische Schnittkräfte anderer Werkstoffe
        double[] mcWerteAndere = new double[] { 0.2, 0.3, 0.25 }; //spezifische Anstiegswerte anderer Werkstoffe






        //Zahnvorschub-Rechenwerte für die einzelnen spezifischen Werkstoffe in den jeweiligen Werkstoff-Übergruppen (in Reihenfolge gemäß oben)

        double[] fzRechenwerteStahl = new double[] { 0.025, 0.025, 0.01, 0.01, 0.01, 0.01 };

        double[] fzRechenwerteNichtrostenderStahl = new double[] { 0.01, 0.01, 0.01 };

        double[] fzRechenwerteGusseisen = new double[] { 0.025, 0.025, 0.025, 0.02 };

        double[] fzRechenwerteNEMetalle = new double[] { 0.025, 0.025, 0.015, 0.015, 0.045, 0.025 };

        double[] fzRechenwerteSchwerZerspanbareWS = new double[] { 0.052, 0.052, 0.052, 0.052, 0.052, 0.052, 0.052 };

        double[] fzRechenwerteHarteWS = new double[] { 0.045, 0.04, 0.035, 0.04 };

        double[] fzRechenwerteAndere = new double[] { 0.025, 0.11, 0.0775 };




        //Schnittgeschwindigkeitswerte für die einzelnen spezifischen Werkstoffe je nach Bohrerbeschichtung

        double[] vcWerteStahlHSS = new double[] { 50, 37, 21, 16, 16, 12 };
        double[] vcWerteStahlHM = new double[] { 85, 85, 75, 70, 70, 65 };          //für Stahl


        double[] vcWerteNichtrostenderStahlHSS = new double[] { 19, 19, 19 };
        double[] vcWerteNichtorstenderStahlHM = new double[] { 40, 40, 40 };          //für nichtorstenden Stahl


        double[] vcWerteGusseisenHSS = new double[] { 31, 25, 31, 25 };
        double[] vcWerteGusseisenHM = new double[] { 105, 85, 105, 85 };          //für Gusseisen


        double[] vcWerteNEMetalleHSS = new double[] { 87, 87, 50, 50, 75, 50 };
        double[] vcWerteNEMetalleHM = new double[] { 240, 240, 170, 170, 170, 170 };          //für NE-Metalle


        double[] vcWerteSchwerZerspanbareWSHM = new double[] { 45, 45, 45, 45, 45, 45, 45 };          //für Schwer zerspanbare Werkstoffe


        double[] vcWerteHarteWSHM = new double[] { 28, 16, 12, 16 };          //für Harte Werkstoffe


        double[] vcWerteAndereHSS = new double[] { 30 };
        double[] vcWerteAndereHM = new double[] { 100, 80, 340 };          //für Andere



        string[] bohrerBeschichtungen = new string[] { "HSS", "HM" };







        //Konsolentext für alle zur Werkstoff-Übergruppen aus der Datenbank
        public string getWerkstoffÜbergruppeText()
        {
            return ("Wähle zu bearbeitenden Werkstoff:\n\n" +
                            "       1       Stahl\n" +
                            "       2       Nichtrostender Stahl\n" +
                            "       3       Gusseisen\n" +
                            "       4       NE-Metalle\n" +
                            "       5       Schwer zerspanbare Werkstoffe\n" +
                            "       6       Harte Werkstoffe\n" +
                            "       7       Andere\n"   +
                            "       8       Schnittwerte manuell festlegen");  
        }

        //Konsolentexte für die jeweilige spezifische Werkstoffauswahl in den einzelnen Werkstoff-Übergruppen
        public string getWerkstoffSpezifischText(int werkstoffwahl)
        {
            string text = "Wähle spezifischen Werkstoff:\n\n";
            switch (werkstoffwahl)
            {
                case 1:

                    for(int i=0; i<stahlSorten.Length; i++)
                    {
                        text += "       " + (i+1).ToString() + "    " + stahlSorten.GetValue(i) + "\n";
                       
                    }

                    break;
                case 2:

                    for (int i = 0; i < nichtrostendeStahlSorten.Length; i++)
                    {
                        text += "       " + (i + 1).ToString() + "    " + nichtrostendeStahlSorten.GetValue(i) + "\n";

                    }
                  


                    break;
                case 3:
                    for (int i = 0; i < gusseisenSorten.Length; i++)
                    {
                        text += "       " + (i + 1).ToString() + "    " + gusseisenSorten.GetValue(i) + "\n";

                    }
                  
                    break;
                case 4:

                    for (int i = 0; i < neMetallSorten.Length; i++)
                    {
                        text += "       " + (i + 1).ToString() + "    " + neMetallSorten.GetValue(i) + "\n";

                    }

                 
                    break;
                case 5:
                    for (int i = 0; i < schwerZerspanbareWSSorten.Length; i++)
                    {
                        text += "       " + (i + 1).ToString() + "    " + schwerZerspanbareWSSorten.GetValue(i) + "\n";

                    }
               
                    break;
                case 6:

                    for (int i = 0; i < harteWSSorten.Length; i++)
                    {
                        text += "       " + (i + 1).ToString() + "    " + harteWSSorten.GetValue(i) + "\n";

                    }
              
                    break;
                case 7:

                    for (int i = 0; i < andereSorten.Length; i++)
                    {
                        text += "       " + (i + 1).ToString() + "    " + andereSorten.GetValue(i) + "\n";

                    }
                    break;
                default:
                    text =  "Keine weitere Auswahl nötig\n";
                    break;


            }
            return text;
        }



        //Getter für die in der Datenbank vorhandene Anzahl an spezifischen Werkstoffen in den jeweiligen Werkstoff-Übergruppen
        public int getAnzahlWerkstoffSpezGruppen(int werkstoffSpezifischeAuswahl)
        {
            int anzahlSpezifischeWerkstoffe;

            switch (werkstoffÜbergruppenAuswahl)
            {
                case 1:
                    anzahlSpezifischeWerkstoffe = stahlSorten.Length;
                    break;
                case 2:
                    anzahlSpezifischeWerkstoffe = nichtrostendeStahlSorten.Length;
                    break;
                case 3:
                    anzahlSpezifischeWerkstoffe = gusseisenSorten.Length;
                    break;
                case 4:
                    anzahlSpezifischeWerkstoffe = neMetallSorten.Length;
                    break;
                case 5:
                    anzahlSpezifischeWerkstoffe = schwerZerspanbareWSSorten.Length;
                    break;
                case 6:
                    anzahlSpezifischeWerkstoffe = harteWSSorten.Length;
                    break;
                case 7:
                    anzahlSpezifischeWerkstoffe = andereSorten.Length;
                    break;
                default:
                    anzahlSpezifischeWerkstoffe = 0;
                    break;
            }

            return anzahlSpezifischeWerkstoffe;
        }


        //Getter für die in der Datenbank vorhandene Anzahl an Werkstoff-Übergruppen
        public int getAnzahlWerkstoffÜbergruppen()
        {
            return werkstoffÜbergruppen.Length;
        }



        //Setter für die user-ausgewählte Werkstoff-Übergruppe
        public void setWerkstoffÜbergruppenAuswahl(int auswahl)
        {
            this.werkstoffÜbergruppenAuswahl = auswahl;
        }

        //Gibt die Bezeichnung der gewählten Werkstoff-Übergruppe zurück
        public string getWerkstoffÜbergruppenAuswahlBezeichnung()
        {
            return (string)werkstoffÜbergruppen.GetValue(werkstoffÜbergruppenAuswahl-1);
        }

       


        //Setter für den user-ausgewählten, spezifischen Werkstoff
        public void setWerkstoffSpezifischeAuswahl(int auswahl)
        {
            switch (werkstoffÜbergruppenAuswahl)
            {
                case 1:
                    gewählterSpezifischerWerkstoff = (string)stahlSorten.GetValue(auswahl - 1);
                    break;
                case 2:
                    gewählterSpezifischerWerkstoff = (string)nichtrostendeStahlSorten.GetValue(auswahl - 1);
                    break;
                case 3:
                    gewählterSpezifischerWerkstoff = (string)gusseisenSorten.GetValue(auswahl - 1);
                    break;
                case 4:
                    gewählterSpezifischerWerkstoff = (string)neMetallSorten.GetValue(auswahl - 1);
                    break;
                case 5:
                    gewählterSpezifischerWerkstoff = (string)schwerZerspanbareWSSorten.GetValue(auswahl - 1);
                    break;
                case 6:
                    gewählterSpezifischerWerkstoff = (string)harteWSSorten.GetValue(auswahl - 1);
                    break;
                case 7:
                    gewählterSpezifischerWerkstoff = (string)andereSorten.GetValue(auswahl - 1);
                    break;
             }

            this.werkstoffSpezifischeAuswahl = auswahl;
        }


        //Gibt die Bezeichnung des gewählten, spezifischen Werkstoffes zurück
        public string getWerkstoffSpezifisheAuswahlBezeichnung()
        {
            return gewählterSpezifischerWerkstoff;
        }



        //Getter für den Zahnvorschub der ausgewählten Werkstoff-Übergruppe
        public double getZahnvorschub(double dc)
        {
            double fz = 0;

            switch(werkstoffÜbergruppenAuswahl)
            {
                case 1:
                    fz = (double) fzRechenwerteStahl.GetValue(werkstoffSpezifischeAuswahl - 1) * dc * 0.5;
                    break;
                case 2:
                    fz = (double) fzRechenwerteNichtrostenderStahl.GetValue(werkstoffSpezifischeAuswahl - 1) * dc * 0.5;
                    break;
                case 3:
                    fz = (double) fzRechenwerteGusseisen.GetValue(werkstoffSpezifischeAuswahl - 1) * dc * 0.5;
                    break;
                case 4:
                    fz = (double) fzRechenwerteNEMetalle.GetValue(werkstoffSpezifischeAuswahl - 1) * dc * 0.5;
                    break;
                case 5:
                    fz = (double) fzRechenwerteSchwerZerspanbareWS.GetValue(werkstoffSpezifischeAuswahl - 1);
                    break;
                case 6:
                    fz = (double) fzRechenwerteHarteWS.GetValue(werkstoffSpezifischeAuswahl - 1);
                    break;
                case 7:
                    if(werkstoffSpezifischeAuswahl == 1)
                    {
                        fz = (double) fzRechenwerteAndere.GetValue(werkstoffSpezifischeAuswahl - 1) * dc * 0.5;
                    }
                    else
                    {
                        fz = (double) fzRechenwerteAndere.GetValue(werkstoffSpezifischeAuswahl - 1);
                    }
                    break;

            }
            
            return fz;
        }

        //Getter für die Schnittgeschwindigkeit der ausgewählten Werkstoff-Übergruppe
        public double getSchnittgeschwindigkeit()
        {
            //return (double)vcWerte.GetValue(werkstoffÜbergruppenAuswahl - 1);
            double vc = 0;
            int beschichtung;

            switch (werkstoffÜbergruppenAuswahl)
            {
                case 1:

                    beschichtung = beschichtungsAuswahl();
                    if(beschichtung == 1)
                    {
                        vc = (double)vcWerteStahlHSS.GetValue(werkstoffSpezifischeAuswahl - 1);
                    }
                    else if(beschichtung == 2)
                    {
                        vc = (double)vcWerteStahlHM.GetValue(werkstoffSpezifischeAuswahl - 1);
                    }
                    break;
                case 2:
                    beschichtung = beschichtungsAuswahl();
                    if (beschichtung == 1)
                    {
                        vc = (double)vcWerteNichtrostenderStahlHSS.GetValue(werkstoffSpezifischeAuswahl - 1);
                    }
                    else if(beschichtung == 2)
                    {
                        vc = (double)vcWerteNichtorstenderStahlHM.GetValue(werkstoffSpezifischeAuswahl - 1);
                    }
                    break;
                case 3:
                    beschichtung = beschichtungsAuswahl();
                    if(beschichtung == 1)
                    {
                        vc = (double)vcWerteGusseisenHSS.GetValue(werkstoffSpezifischeAuswahl - 1);
                    } else if(beschichtung == 2)
                    {
                        vc = (double)vcWerteGusseisenHM.GetValue(werkstoffSpezifischeAuswahl - 1);
                    }
                    break;
                case 4:
                    beschichtung = beschichtungsAuswahl();
                    if(beschichtung == 1)
                    {
                        vc = (double)vcWerteNEMetalleHSS.GetValue(werkstoffSpezifischeAuswahl - 1);
                    } else if(beschichtung == 2)
                    {
                        vc = (double)vcWerteNEMetalleHM.GetValue(werkstoffSpezifischeAuswahl - 1);
                    }
                    break;
                case 5:
                    vc = (double)vcWerteSchwerZerspanbareWSHM.GetValue(werkstoffSpezifischeAuswahl - 1);
                    break;
                case 6:
                    vc = (double)vcWerteHarteWSHM.GetValue(werkstoffSpezifischeAuswahl - 1);
                    break;
                case 7:
                    if (werkstoffSpezifischeAuswahl == 1)
                    {
                        beschichtung = beschichtungsAuswahl();
                        if (beschichtung == 1)
                        {
                            vc = (double)vcWerteAndereHSS.GetValue(werkstoffSpezifischeAuswahl - 1);
                        }else if(beschichtung == 2)
                        {
                            vc = (double)vcWerteAndereHM.GetValue(werkstoffSpezifischeAuswahl - 1);
                        }
                    }
                    else
                    {
                        vc = (double)vcWerteAndereHM.GetValue(werkstoffSpezifischeAuswahl - 1);
                    }
                    break;
            }
            return vc;
        }

        

       
        //Getter für die spezifische Schnittkraft des ausgewählten, spezifischen Werkstoffes
        public double getSpezifischeSchnittkraft()
        {

            double kc = 0;
            switch (werkstoffÜbergruppenAuswahl)
            {
                case 1:

                    kc = (double)kcWerteStahl.GetValue(werkstoffSpezifischeAuswahl - 1);    //Wenn Stahl als Hauptgruppe (1) gewählt wurde, dann auf kc-Liste für Stahl zugreifen
                    break;
                case 2:
                    kc = (double)kcWerteNichtrostenderStahl.GetValue(werkstoffSpezifischeAuswahl - 1);
                    break;
                case 3:
                    kc = (double)kcWerteGusseisen.GetValue(werkstoffSpezifischeAuswahl - 1);    //Wenn Gusseisen als Hauptgruppe (2) gewählt wurde, dann auf kc-Liste für Stahl zugreifen
                    break;
                case 4:
                    kc = (double)kcWerteNEMetalle.GetValue(werkstoffSpezifischeAuswahl - 1);    //Wenn Aluminium als Hauptgruppe (3) gewählt wurde, dann auf kc-Liste für Stahl zugreifen
                    break;
                case 5:
                    kc = (double)kcWerteSchwerZerspanbareWS.GetValue(werkstoffSpezifischeAuswahl - 1);
                    break;
                case 6:
                    kc = (double)kcWerteHarteWS.GetValue(werkstoffSpezifischeAuswahl - 1);
                    break;
                case 7:
                    kc = (double)kcWerteAndere.GetValue(werkstoffSpezifischeAuswahl - 1);
                    break;


            }

            return kc;
        }


        //Getter für den Anstiegswert des ausgewählten, spezifischen Werkstoffes
        public double getAnstiegswert()
        {
            double mc = 0;
            switch (werkstoffÜbergruppenAuswahl)
            {
               
                case 1:

                    mc = (double)mcWerteStahl.GetValue(werkstoffSpezifischeAuswahl - 1);    //Wenn Stahl als Hauptgruppe (1) gewählt wurde, dann auf kc-Liste für Stahl zugreifen
                    break;
                case 2:
                    mc = (double)mcWerteNichtrostenderStahl.GetValue(werkstoffSpezifischeAuswahl - 1);
                    break;
                case 3:
                    mc = (double)mcWerteGusseisen.GetValue(werkstoffSpezifischeAuswahl - 1);    //Wenn Gusseisen als Hauptgruppe (2) gewählt wurde, dann auf kc-Liste für Stahl zugreifen
                    break;
                case 4:
                    mc = (double)mcWerteNEMetalle.GetValue(werkstoffSpezifischeAuswahl - 1);    //Wenn Aluminium als Hauptgruppe (3) gewählt wurde, dann auf kc-Liste für Stahl zugreifen
                    break;
                case 5:
                    mc = (double)mcWerteSchwerZerspanbareWS.GetValue(werkstoffSpezifischeAuswahl - 1);
                    break;
                case 6:
                    mc = (double)mcWerteHarteWS.GetValue(werkstoffSpezifischeAuswahl - 1);
                    break;
                case 7:
                    mc = (double)mcWerteAndere.GetValue(werkstoffSpezifischeAuswahl - 1);
                    break;


            }
            return mc;
        }



        private int beschichtungsAuswahl()
        {


            bool korrekteEingabe = false;
            int beschichtung = 0;
            Console.Clear();
            while (!korrekteEingabe)
            {
                korrekteEingabe = true;


                string text = "Wähle Beschichtung des Bohrers:\n\n";


                for (int i = 0; i < bohrerBeschichtungen.Length; i++)
                {
                    text += "       " + (i + 1).ToString() + "    " + bohrerBeschichtungen.GetValue(i) + "\n";

                }

                
                
                while (true) //Auf Zahleneingabe warten (Buchstaben sind ungültig)
                {


                    Console.Write(text); //Beschichtungen drucken
                    if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out beschichtung))
                    {
                        break;
                    }
                    Console.Clear();
                    Console.WriteLine("Inkorrekte Eingabe. Das Programm erwartet eine Zahl.\n");
                   
                }




                if (beschichtung < 1 || beschichtung > bohrerBeschichtungen.Length)
                {
                    korrekteEingabe = false;
                    Console.Clear();
                    Console.WriteLine("Ungültige Eingabe. Bitte Zahl aus bestehendem Beschichtungskatalog eingeben.\n");

                }

            }

            Console.WriteLine("\n" + bohrerBeschichtungen.GetValue(beschichtung - 1) + "-beschichteter Bohrer ausgewählt\n");


            return beschichtung;

        }


    }
}