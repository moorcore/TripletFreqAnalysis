using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

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

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            counter.ReadFileAsync(path);

            counter.LaunchCount();

            List<KeyValuePair<string, int>> myList = TripletCounter.AllTripletsCounted.ToList();

            myList.Sort(
                delegate (KeyValuePair<string, int> pair1,
                KeyValuePair<string, int> pair2)
                {
                    return pair2.Value.CompareTo(pair1.Value);
                }
            );

            Console.WriteLine();
            Console.WriteLine(string.Join
                (
                    Environment.NewLine,
                    myList.Take(10).Select(gr => $"\"{gr.Key}\" occurs {gr.Value} times")
                ));

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00} hours, {1:00} minutes, {2:00} seconds, {3:000} milliseconds",
                        ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine();
            Console.WriteLine("Program execution time: " + elapsedTime);
        }
    }
}
