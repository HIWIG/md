using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Channels;

namespace md
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] tableweight = new int[100];
            List<string> test = new List<string>(){ "12","25","32","55","87","98","55","43","10","18"};
            string[] result = new string[102];

            string filename = "drzewa.txt"; 
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,filename);
            Console.WriteLine(path);
            
            StringBuilder sb = new StringBuilder();
            List<string> lines = new List<string>();

            int[,] weight = new int[10,10];
            int temp = 9;
            for (int i = 1; i < 10; i++)
            {
                for (int j = i + 1; j < 10; j++)
                {
                    temp += 1;
                    weight[i, j] = temp;
                    weight[j, i] = temp;
                }
            }

            for (int i = 0; i < 10; i++)
            {
                weight[0, i] = i;
                weight[i, 0] = i;
            }

            
            for (int c = 0; c < 100; c++)
            {
                int treeLenght = 10;
                int sum = 0;
                var R = new List<int>();
                var T = new List<int>();
                var wyn = new List<string>();
                Random rnd = new Random();

                for (int i = 0; i < treeLenght; i++)
                {
                    R.Add(i + 1);
                }
                

                for (int i = 0; i < treeLenght; i++)
                {
                    var x = rnd.Next(0, R.Count);

                    if (i >= 1)
                    {
                        int a;
                        int b = R[x];
                        a = T[rnd.Next(T.Count)];
                        sum += weight[-1+a, -1+b];
                        wyn.Add("{" + a + " " + b + "} ");
                    }

                    T.Add(R[x]);
                    R.RemoveAt(x);
                }

                tableweight[c] = sum;
                foreach (var x in wyn)
                {
                    sb.Append($"{x} ");
                }
                
                lines.Add($"{sb.ToString()} "); // TUTAJ DODAJ WYNIK WAGI
                sb.Clear();
            }
            var q=tableweight.Select((item,index)=> new{item,index}).OrderBy(x => x.item).Select(x=>x.index);
            Console.WriteLine("=====================================================================");
            
            for (int c = 2; c < 102; c++)
            {
               result[c]=$"Drzewo numer:{c-1} {lines[q.ElementAt(c-2)]} Waga:{tableweight[q.ElementAt(c-2)]}";
            }
            result[0]=$"Program napisany przez:\nDamian Grygierczyk, Grzegorz Faber, Jakub Hoczek, Szymon Damek\n\nDrzewo o najmniejszej wadze: {lines[q.ElementAt(0)]} Waga:{tableweight[q.ElementAt(0)]}";
            result[1]=$"Drzewo o największej wadze: {lines[q.ElementAt(99)]} Waga:{tableweight[q.ElementAt(99)]}\n\nWszystkie drzewa:";
            File.WriteAllLines(path,result);
            Console.WriteLine("Wygenerowano 100 drzew do pliku drzewa.txt. Program zamknie się za 5 sekund ");
            Thread.Sleep(5000);
        }
    }
}
