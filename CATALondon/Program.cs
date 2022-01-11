using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CATALondon
{
    class Program
    {
        static Dictionary<string, Dictionary<DateTime, int>> sportagak = new Dictionary<string, Dictionary<DateTime, int>>();
        static void Main(string[] args)
        {
            Beolvasas();
            F02();
            F03();
            F04();
            F05();
            F06();
            Console.ReadLine();
        }

        private static void F06()
        {
            var keresettNap = new DateTime(2012, 07, 29);
            var jul29 = sportagak.Sum(x => x.Value[keresettNap]);

            Console.WriteLine($"6. feladat:\n\t{keresettNap.Day}.-án/én {jul29} db döntő volt.");
        }

        private static void F05()
        {
            int ermekSzama = sportagak.Sum(x => x.Value.Sum(y => y.Value)); //LINQ-val

            #region ForCiklussal
            /*
            int sum = 0;
            foreach (var sportag in sportagak)
            {
                foreach (var nap in sportag.Value)
                {
                    sum += nap.Value;
                }
            }
            */
            #endregion

            Console.WriteLine($"5. feladat:\n\t{ermekSzama} db aranyérmet osztottak ki az olimpián.");
        }

        private static void F04()
        {
            var model = new Dictionary<DateTime, int>();
            foreach (var sportag in sportagak)
            {
                foreach (var nap in sportag.Value)
                {
                    if (!model.ContainsKey(nap.Key))
                    {
                        model.Add(nap.Key, nap.Value);
                    }
                    else
                    {
                        model[nap.Key] += nap.Value;
                    }
                }
            }

            var legtobbDontosNap = model.OrderBy(x => x.Value).Last();

            #region Bejáróciklus
            /*
            foreach (var nap in model)
            {
                Console.WriteLine($"{nap.Key.ToString("MM-dd")} - {nap.Value} db");
            }
            */
            #endregion

            Console.WriteLine($"4. feladat:\n\tA legtöbb döntő ({legtobbDontosNap.Value} db) {legtobbDontosNap.Key.Day}.-án/én volt");
        }

        private static void F03()
        {
            int uszasarany = sportagak["Úszás"].Sum(x => x.Value);

            Console.WriteLine($"3. feladat:\n\tAranyérmek száma úszásban: {uszasarany} db");
        }

        private static void F02()
        {
            int db = sportagak["Atlétika"].Count(x => x.Value > 0);

            Console.WriteLine($"2. feladat:\n\tDöntős napok száma atlétikai sportágban: {db} db");
        }

        private static void Beolvasas()
        {
            using (var sr = new StreamReader(@"London2012.txt", Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    var sor = sr.ReadLine().Split(';');

                    sportagak.Add(sor[0], new Dictionary<DateTime, int>());

                    var datum = new DateTime(2012, 07, 28);

                    for (int i = 1; i <= 16; i++)
                    {
                        sportagak[sor[0]].Add(datum, int.Parse(sor[i]));
                        datum = datum.AddDays(1);
                    }
                }
            }
        }
    }
}
