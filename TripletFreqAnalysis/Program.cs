using System;
using System.Linq;

namespace TripletFreqAnalysis
{

    /*Необходимо написать консольное приложение на C#, выполняющее частотный анализ текста.
    Входные параметры: путь к текстовому файлу.
    Выходные результаты: вывести на экран 10 самых часто встречающихся в тексте триплетов 
    (3 идущих подряд буквы слова) и число их повторений, и на последней строке время работы программы в миллисекундах.
    Требования: программа должна обрабатывать текст в многопоточном режиме.*/
    class Program
    {
        static void Main(string[] args)
        {
            TripletCounter counter = new TripletCounter();

            Console.WriteLine("Please enter the directory path below:");

            string path = Console.ReadLine();

            counter.ReadFileAsync(path);

            var groups = counter.CountTriplets()
                .Where(str => str.All(ch => char.IsLetter(ch)))
                .GroupBy(str => str);

            Console.WriteLine(string.Join
                (
                    Environment.NewLine,
                    groups.OrderByDescending(gr => gr.Count())
                    .Take(10).Select(gr => $"\"{gr.Key}\" occurs {gr.Count()} times")
                ));
        }
    }
}
