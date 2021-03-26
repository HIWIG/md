using System;
using System.Collections.Generic;

namespace md
{
    class Program
    {
        static void Main(string[] args)
        {

            int[,] weight = new int[10,10];
            int temp = 9;
            for (int i = 1; i < 10; i++)
            {
                
                for (int j = i+1; j < 10; j++)
                {
                    temp += 1;
                    weight[i, j] = temp;
                    weight[j, i] = temp;
                }
            }
            
            //wypisywanie tabeli wag
            // for (int i = 0; i < 10; i++)
            // {
            //     for (int j = 0; j < 10; j++)
            //     {
            //         Console.Write($"{weight[i,j]} ");
            //     }
            //     Console.WriteLine();
            // }

            
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