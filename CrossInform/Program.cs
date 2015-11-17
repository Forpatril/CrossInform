using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossInform
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "заданная,по,умолчанию,строка,на,входе,в,которой,производится,поиск,триплетов";
            Dictionary<string,int> triplet = new Dictionary<string,int>();
            //Console.Write("Введите строку: ");
            //s=Console.ReadLine();
            if (s.Length > 3)
            {
                int i = 3;
                string trip = s.Substring(0, 3);
                triplet.Add(trip, 1);
                while (i < s.Length)
                {
                    if (Char.IsLetter(s[i]))
                    {
                        trip = trip.Substring(1) + s[i];
                        if (triplet.ContainsKey(trip))
                        {
                            triplet[trip]++;
                        }
                        else
                        {
                            triplet.Add(trip, 1);
                        }
                        i++;
                    }
                    else
                    {
                        trip = s.Substring(i, 3);
                        i += 3;
                    }
                }
                Console.WriteLine(triplet.OrderByDescending(x => x.Value).FirstOrDefault().Key + " " + triplet.OrderByDescending(x => x.Value).FirstOrDefault().Value.ToString());
            }
            else if (s.Length == 3)
            {
                Console.WriteLine(s + " " + 1);
            } 
            else
            {
                Console.WriteLine("В строке нет ни одного триплета");
            }
            Console.ReadKey();
        }
    }
}
