using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Channels;
using Microsoft.VisualBasic.FileIO;

namespace md
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] tableweight = new int[100]; // Inicjalizacja tablicy do przechowywawnia wartości wag
            string[] result = new string[102]; // Inicjalziacja tablicy do przechowywania wszystkich drzew

            string filename = "drzewa.txt"; // nazwa pliku, gdzie drzewa zostaną zapisane
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename); // ścieżka zapisu do pliku
            Console.WriteLine(path); // wypisanie na ekranie konsoli ścieżki

            StringBuilder sb = new StringBuilder(); // Tego potrzebujemy pod dołem do ładnego formatowania wyniku.
            List<string> lines = new List<string>(); // Lista do przechowywania jednej linijki zawierającej opis drzewa.

            int[,] weight = new int[10, 10]; // Inicjalizacja tabeli wag gałęzi
            int temp = 9; // Waga poszczególnej gałęźci
            Random rnd = new Random(); // Obiekt klasy random, w celu losowania wartości pseudolosowych.
            for (int i = 1; i < 10; i++) // W tej pętli uzupełniamy tabelę wag.
            {
                for (int j = i + 1; j < 10; j++)
                {
                    temp += 1;
                    weight[i, j] = temp;
                    weight[j, i] = temp;
                }
            }

            for (int i = 0; i < 10; i++) // Kontynuacja uzupełniania tabeli wag.
            {
                weight[0, i] = i;
                weight[i, 0] = i;
            }


            for (int c = 0; c < 100; c++) // Pętla do tworzenia 100 drzew
            {
                int treeLenght = 10; // Ilość wierzchołków drzewa
                int sum = 0; // Aktualna waga drzeaw
                var R = new List<int>(); // Lista początkową, której będziemy zabierać elementy od listy T
                var T = new List<int>(); // Lista poczatkowa, której wartości będziemy zabierać z listy R
                var wyn = new List<string>(); // Lista przechowująca aktualny wierzchołek


                for (int i = 0; i < treeLenght; i++) // Uzupełnienie listy R wartościami od 1 do 10.
                {
                    R.Add(i + 1);
                }


                for (int i = 0; i < treeLenght; i++) // Pętla do losowania wierzchołków drzewa
                {
                    var x = rnd.Next(0, R.Count); // Wylosowanie indeksu do zamiany pomiędzy tablicami T i R

                    if (i >= 1)
                    {
                        int a; // pomocnicza zmienna przechowująca wartość wybranej liczby
                        int b = R[x]; // wylosowanie konkretnej wartości z tabeli R
                        a = T[rnd.Next(T.Count)]; // wylosowanie konkretnej wartości z tabeli T
                        sum += weight[-1 + a, -1 + b]; // Suma wag z aktualnego wierzchołka a,b
                        wyn.Add($"({a,2},{b,2})"); // Dodanie do tablicy wierzchołków drzewa konkretnego wierzchołka
                    }

                    T.Add(R[x]); // Dodanie do tablicy T wylosowanego elementu z tablicy R
                    R.RemoveAt(x); // Dodany element do tablicy T zostaje zabrany z R
                }

                tableweight[c] = sum; // Suma wag dla danego drzewa
                foreach (var x in wyn)
                {
                    sb.Append($"{x} "); // Zapisanie wszystkich wierzchołków z konkretnego drzewa
                }

                lines.Add($"{sb.ToString()} "); // Do tablicy reprezentującej poszczególną linię zawierającą wierzchołki oraz wage drzewa zostaje dodane konkretne drzewo
                sb.Clear(); // wyczyszczenie zmiennej do formatowania.
            }

            var q = tableweight.Select((item, index) => new {item, index}).OrderBy(x => x.item)
                .Select(x => x.index); // Posotowanie drzew
            Console.WriteLine("=====================================================================");

            for (int c = 2; c < 102; c++) // Dodanie do tablicy wyniku wszystkich posortowanych drzew wraz z wagami
            {
                result[c] =
                    $"Drzewo numer:{c - 1,2} {lines[q.ElementAt(c - 2)]} Waga:{tableweight[q.ElementAt(c - 2)]}";
            }

            /*result[0]=$"Program napisany przez:\nDamian Grygierczyk, Grzegorz Faber, Jakub Hoczek, Szymon Damek\n\n" +
                      $"Drzewo o najmniejszej wadze: {lines[q.ElementAt(0)],6} Waga:{tableweight[q.ElementAt(0)]}";
            result[1]=$"Drzewo o największej wadze:  {lines[q.ElementAt(99)],6} Waga:{tableweight[q.ElementAt(99)]}\n\nWszystkie drzewa:";
            File.WriteAllLines(path,result); // Zapis do pliku
            Console.WriteLine("Wygenerowano 100 drzew do pliku drzewa.txt. Program zamknie się za 5 sekund ");
            Thread.Sleep(5000); // Uśpienie programu na 5s.*/
            for (int i = 2; i < 102; i++)
            {
                Console.WriteLine(lines[q.ElementAt(i - 2)]);
            }

            int nr = 0;

            int[] tableWeights = new int[100];
            int[] numberTab = new int[100];
            foreach (var line in lines)
            {
                List<string> linesAfterSplit = line.Replace("(", "").Replace(" ", "").Split(')').ToList();
                int[,] tabAfterSplit = new int[linesAfterSplit.Count, 2];
                for (int i = 0; i < linesAfterSplit.Count - 1; i++)
                {
                    var x = linesAfterSplit[i].Split(',');

                    tabAfterSplit[i, 0] = int.Parse(x[0]);
                    tabAfterSplit[i, 1] = int.Parse(x[1]);
                }

                var positionToChange = rnd.Next(0, 9);
                int[] branch = new int[2];
                branch[0] = tabAfterSplit[positionToChange, 0];
                branch[1] = tabAfterSplit[positionToChange, 1];

                int count1 = 0;
                int count2 = 0;
                int y, z;
                for (int i = 0; i < linesAfterSplit.Count; i++)
                {
                    if (tabAfterSplit[i, 0] == branch[0] || tabAfterSplit[i, 1] == branch[0])
                    {
                        count1++;
                    }

                    if (tabAfterSplit[i, 1] == branch[1] || tabAfterSplit[i, 0] == branch[1])
                    {
                        count2++;
                    }
                }

                if (count1 > 1 && count2 > 1)
                {
                    // Przyjmujemy lewostronnego czyli branch[0]
                    var firstFriends = new HashSet<int>();
                    firstFriends.Add(branch[0]);
                    var searcher = branch[0];
                    int pos = 0;
                    for (int i = 0; i < firstFriends.Count; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            if (searcher == tabAfterSplit[j, 0] && tabAfterSplit[j, 1] != branch[1])
                            {
                                firstFriends.Add(tabAfterSplit[j, 1]);
                            }
                            else if (searcher == tabAfterSplit[j, 1] && tabAfterSplit[j, 0] != branch[0])
                            {
                                firstFriends.Add(tabAfterSplit[j, 0]);
                            }
                        }

                        pos++;
                        if (pos < firstFriends.Count)
                            searcher = firstFriends.ElementAt(pos);
                    }

                    //Console.WriteLine($"{line}");
                }
                else
                {

                    if (count1 > 1)
                    {
                        y = branch[0];
                        z = branch[1];
                        do
                        {
                            branch[0] = rnd.Next(1, 11);
                        } while (branch[0] == branch[1]);
                    }

                    if (count2 > 1)
                    {
                        y = branch[0];
                        z = branch[1];
                        do
                        {
                            branch[1] = rnd.Next(1, 11);
                        } while (branch[0] == branch[1]);
                    }

                    tabAfterSplit[positionToChange, 0] = branch[0];
                    tabAfterSplit[positionToChange, 1] = branch[1];
                }

                // LICZENIE SUPER WAG
                var currentTreeWeight = 0;
                for (int i = 0; i < 1; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        currentTreeWeight += weight[tabAfterSplit[j, 0] - 1, tabAfterSplit[j, 1] - 1];
                    }
                    Console.WriteLine($"WAGA {currentTreeWeight} Pozycja: {nr}");
                    Console.WriteLine($"{line}");
                    tableWeights[nr] = currentTreeWeight;
                    numberTab[nr] = nr;
                    currentTreeWeight = 0;
                }
                nr++;
            }
            Array.Sort(tableWeights,numberTab);
            int indexator = 0;
            Console.WriteLine("+============================================================+");
            foreach (var el in tableweight)
            {
                Console.WriteLine($"{lines[numberTab[indexator]]} Waga:{tableWeights[indexator]}");
                indexator++;
            }
        }
    }
}