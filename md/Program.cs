using System;
using System.Collections.Generic;

namespace md
{
    class Program
    {
        static void Main(string[] args)
        {

            for (int c = 0; c < 100; c++)
            {


                int treeLenght = 10;
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

                        wyn.Add("{" + a + " " + b + "} ");

                    }

                    T.Add(R[x]);
                    R.RemoveAt(x);
                }




                foreach (var x in wyn)
                {
                    Console.Write(x);
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}