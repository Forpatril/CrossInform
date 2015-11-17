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
            Dictionary<string,int> triplet = new Dictionary<string,int>();      //список триплетов с количеством повторений каждого
            Console.Write("Введите строку: ");
            s=Console.ReadLine();
            int err = Check(ref s);   //проверка строки
            if (err == 0)       //строка соответствует условию, в строке больше одного триплета
            {
                int i = 3;
                string trip = s.Substring(0, 3).ToLower(); //выделение подстроки с приведением к строчным буквам (считаем триплеты «Три» и «три» одинаковыми)
                triplet.Add(trip, 1);  //первый триплет автоматически попадает в список
                while (i < s.Length)
                {
                    if (Char.IsLetter(s[i]))  //продолжается ли слово
                    {
                        trip = trip.Substring(1) + Char.ToLower(s[i]); //изменение триплета
                        if (triplet.ContainsKey(trip)) //есть ли уже такой триплет
                        {
                            triplet[trip]++; //увеличение количества вхождений для найденного триплета
                        }
                        else
                        {
                            triplet.Add(trip, 1); //добавление нового триплета
                        }
                        i++;
                    }
                    else
                    {
                        trip = s.Substring(i, 3); // натолкнулись на запятую, создаём триплет, содержащий её и две следующие буквы. Так как из строки удалены слова из двух букв, триплет гарантировано будет
                        i += 3;
                    }
                }
                Console.WriteLine(String.Concat(triplet.OrderByDescending(x => x.Value).FirstOrDefault().Key, " ", triplet.OrderByDescending(x => x.Value).FirstOrDefault().Value.ToString())); //выводим наиболее частый триплет и число его вхождений
            }
            else if (err == 1)  //в строке только три символа или только одно слово из трёх символов
            {
                Console.WriteLine(String.Concat(s, " 1"));
            } 
            else if (err == 2) //в строке нет триплетов
            {
                Console.WriteLine("В строке нет ни одного триплета");
            }
            else //в строке присутствуют отличные от букв и запятой символы
            {
                Console.WriteLine("Строка не соответствует условию задачи");
            }
            Console.ReadKey();
        }

        static int Check(ref string s)
        {
            if (s.Length < 3)  // длина строки меньше 3, в ней нет триплетов
            {
                return 2;
            }
            foreach (char c in s) 
            {
                if (!Char.IsLetter(c) && !(c == ',')) // проверка, что в строке только буквы и запятые
                    return 4;
            }
            if (s.Length == 3)  // в строке три символа
                if (s.Contains(','))  // есть запятая, триплетов нет
                    return 2;
                else
                    return 1;
            else
            {
                List<string> ss = new List<string>(s.Split(','));  //делим строку на массив подстрок для исключения коротких
                if (ss.Count > 1)
                {
                    for (int i = 0; i < ss.Count; i++)
                    {
                        if (ss[i].Length < 3) //короткое слово, удаляем из списка
                            ss.RemoveAt(i--);
                    }
                    if (ss.Count < 1) //не осталось слов – в строке нет триплетов
                        return 2;
                    s = ""; 
                    foreach (string str in ss)
                    {
                        s = String.Concat(s, str, ','); //создаём строку заново из слов, в которых не меньше трёх букв
                    }
                    s = s.Substring(0, s.Length - 1); //удаляем последнюю запятую
                }
                return 0;
            }
        }
    }
}
