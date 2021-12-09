using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace TripletFreqAnalysis
{
    public class TripletCounter
    {
        public static ConcurrentDictionary<string, int> AllTripletsCounted = 
            new ConcurrentDictionary<string, int>();

        List<string> splittedText = new List<string>();

        Regex RgxUrl = new Regex("[^a-zA-ZА-Яа-я]");

        public void ReadFileAsync(string inputPath)
        {
            string path = inputPath;

            try
            {
                var splitted = File.ReadAllLines(path);

                for (int i = 0; i < splitted.Length; i++)
                {
                    splittedText.Add(splitted[i]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void LaunchCount()
        {  
            for (int j = 0; j < splittedText.Count(); j++)
            {
                ThreadPool.QueueUserWorkItem(CountTriplets, splittedText[j]);
            }

            while (ThreadPool.PendingWorkItemCount != 0)
            {

            }

        }

        public void CountTriplets(object state)
        {
            int length = 3;

            string s = (string)state;

            for (int i = length; i <= s.Length; i++)
            {
                var t = s.Substring(i - length, length);

                if (RgxUrl.IsMatch(t))
                {
                    continue;
                }

                if (AllTripletsCounted.ContainsKey(t))
                {
                    AllTripletsCounted[t] = AllTripletsCounted[t] + 1; 
                }
                else
                {
                    AllTripletsCounted.TryAdd(t, 1);
                }
            }    
        }
    }
}
