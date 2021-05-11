using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace tanfel
{
    class Bejegyzes
    {
        public string Tanar { get; set; }
        public string Targy { get; set; }
        public string OsztalyAzon { get; set; }
        public int Evfolyam { get; set; }
        public char Osztaly { get; set; }
        public int OrakHetente { get; set; }

        public Bejegyzes (string tanar, string targy, string osztaly, string orakSzama)
        {
            this.Tanar = tanar;
            this.Targy = targy;
            this.OsztalyAzon = osztaly;

            string[] osztalyArr = osztaly.Split('.');
            this.Evfolyam = Convert.ToInt32(osztalyArr[0]);
            this.Osztaly = Convert.ToChar(osztalyArr[1]);
            this.OrakHetente = Convert.ToInt32(orakSzama);
        }
    }

    class MainClass
    {
        static public List<Bejegyzes> bejegyzesek = new List<Bejegyzes>();

        public static void Main(string[] args)
        {
            Console.WriteLine("1. Feladat:");
            StreamReader sr = new StreamReader(new FileStream("beosztas.txt", FileMode.Open));

            while(!sr.EndOfStream)
            {
                string[] sor = new string[4];
                for (int i = 0; i < 4; i++)
                {
                    sor[i] = sr.ReadLine();
                }

                Bejegyzes bejegyzes = new Bejegyzes(sor[0], sor[1], sor[2], sor[3]);
                bejegyzesek.Add(bejegyzes);
            }

            sr.Close();

            foreach (Bejegyzes bejegyzes in bejegyzesek)
            {
                Console.WriteLine($"{bejegyzes.Tanar}, {bejegyzes.Targy}, {bejegyzes.Evfolyam}.{bejegyzes.Osztaly}, {bejegyzes.OrakHetente}");
            }

            Feladat2();
            Feladat3();
            Feladat4();
            Feladat5();
            Feladat6();
            Feladat7();
        }

        public static void Feladat2()
        {
            Console.WriteLine("\n2. Feladat:");
            Console.WriteLine($"A fajlban {bejegyzesek.Count} bejegyzes van.");
        }

        public static void Feladat3()
        {
            Console.WriteLine("\n3. Feladat:");
            int osszesOra = bejegyzesek.Sum(bejegyzes => bejegyzes.OrakHetente);
            Console.WriteLine($"Az iskolaban heti osszoraszam: {osszesOra}");
        }

        public static void Feladat4()
        {
            Console.WriteLine("\n4. Feladat:");
            Console.Write("Adja meg egy tanar nevet: ");
            string tanar = Console.ReadLine();
            int hetiOrai = bejegyzesek.Where(bejegyzes => bejegyzes.Tanar == tanar).Sum(bejegyzes => bejegyzes.OrakHetente);
            Console.WriteLine($"{tanar} {hetiOrai} darab orat tart hetente.");
        }

        public static void Feladat5()
        {
            Console.WriteLine("\n5. Feladat:");
            StreamWriter sw = new StreamWriter(new FileStream("of.txt", FileMode.Create));
            IEnumerable<Bejegyzes> osztalyfonokiek = bejegyzesek.Where(bejegyzes => bejegyzes.Targy == "osztalyfonoki");
            foreach (Bejegyzes bejegyzes in osztalyfonokiek)
            {
                sw.WriteLine($"{bejegyzes.OsztalyAzon} - {bejegyzes.Tanar}");
            }

            sw.Close();

            Console.WriteLine("5. Feladat kiirva az of.txt fajlba.");
        }

        public static void Feladat6()
        {
            Console.Write("\n6. Feladat:");

            Console.WriteLine("Osztaly: ");
            string osztaly = Console.ReadLine();

            Console.Write("Tantargy: ");
            string targy = Console.ReadLine();

            string hogyTanuljak = bejegyzesek.Where(bejegyzes => bejegyzes.OsztalyAzon == osztaly && bejegyzes.Targy == targy).Count() == 1 ? "Osztalyszinten" : "Csoportbontasban";

            Console.WriteLine($"{hogyTanuljak} tanuljak.");
        }

        public static void Feladat7()
        {
            Console.WriteLine("\n7. Feladat:");

            int tanarokSzama = bejegyzesek.GroupBy(bejegyzes => bejegyzes.Tanar).Count();

            Console.WriteLine($"Az iskolaban {tanarokSzama} tanar tanit.");
        }
    }
}
