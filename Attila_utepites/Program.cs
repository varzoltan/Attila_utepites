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
            public TimeSpan Ido_add_mp{get{ return ido.Add(TimeSpan.FromSeconds(masodperc)); } }
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
            int n = Convert.ToInt32(Console.ReadLine()) - 1;
            Console.WriteLine("\n2.feladat");
            if (lista[n].varos == "F")
            {
                Console.WriteLine($"Alsó város felé haladt {lista[n].ido}");
            }
            else
            {
                Console.WriteLine($"Felső város felé haladt {lista[n].ido}");
            }

            //3.feladat
            Console.WriteLine("\n3.feladat");
            var valogat_A = lista.Where(x => x.varos == "A").OrderByDescending(x => x.ido);
            Console.WriteLine($"A Felső város irányába tartó utolsó két jármű ideje másodpecben: {valogat_A.ElementAt(0).ido.TotalSeconds - valogat_A.ElementAt(1).ido.TotalSeconds}");

            //4.feladat
            Console.WriteLine("\n4.feladat: statisztika");
            Console.WriteLine("Óra Alsó Felső");
            int Also = 0, Felso = 0;
            for (int i = lista.First().ido.Hours;i<=lista.Last().ido.Hours;i++)
            {
                for (int j = 0;j<lista.Count-1;j++)
                {
                    if (lista[j].ido.Hours == i)
                    {
                        if (lista[j].varos == "F")
                        {
                            Felso++;
                        }

                        if (lista[j].varos == "A")
                        {
                            Also++;
                        }
                    }
                }
                Console.WriteLine($"{i,2} {Also,3}db {Felso,3}db");
                Also = 0;Felso = 0;
            }

            //5.feladat
            Console.WriteLine("\n5.feladat");
            foreach (var i in lista.OrderBy(x => x.masodperc).Take(10))
            {
                Console.WriteLine("{0} {1} {2} m/s",i.ido,i.varos,((double)1000/i.masodperc).ToString("0.0"));
            }

            //6.feladat
            //szürés
            Console.WriteLine("\n6.feladat");
            var levalogat = lista.Where(x => x.varos == "F");
            StreamWriter ir = new StreamWriter("also.txt");
            for (int i = 0;i<levalogat.Count()-1;i++)
            {
                for (int j = i+1;j<levalogat.Count();j++)
                {
                    if (levalogat.ElementAt(i).Ido_add_mp >= levalogat.ElementAt(j).Ido_add_mp)
                    {
                        ir.WriteLine(levalogat.ElementAt(i).Ido_add_mp);
                    }
                    else
                    {
                        if (i==0)
                        {
                            ir.WriteLine(levalogat.ElementAt(i).Ido_add_mp);
                        }
                        ir.WriteLine( levalogat.ElementAt(j).Ido_add_mp);
                        i = j;
                    }
                }
            }
            ir.Close();
            Console.WriteLine("\"also.txt\" állomány kiírás kész!");
            Console.ReadKey();
        }
    }
}
