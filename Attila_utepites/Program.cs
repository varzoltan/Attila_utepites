using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Attila_utepites
{
    internal class Program
    {
        struct Adat
        {
            public TimeSpan ido { get; set; }
            public int masodperc { get; set; }
            public string varos { get; set; }
            public Adat(string sor)//Konstruktor
            {
                string[] db = sor.Split(' ');
                ido = TimeSpan.Parse(db[0]+":"+db[1]+":"+db[2]);
                masodperc = int.Parse(db[3]);
                varos = db[4];
            }
        }

        static List<Adat> lista = new List<Adat>();
        static void Main(string[] args)
        {
            //1.feladat: Beolvasás
            foreach (var i in File.ReadLines("forgalom.txt").Skip(1)) lista.Add(new Adat(i));
            Console.WriteLine("1.feladat: beolvasás kész!");

            //2.feladat
            Console.Write($"Kérem adja meg az \"n\"-dik belépő járművet: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("2.feladat");
            if (lista[n-1].varos == "F")
            {
                Console.WriteLine($"Alsó város felé haladt {lista[n-1].ido}");
            }
            else
            {
                Console.WriteLine($"Felső város felé haladt {lista[n-1].ido}");
            }
            Console.ReadKey();
        }
    }
}
