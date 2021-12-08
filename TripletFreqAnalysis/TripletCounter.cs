using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TripletFreqAnalysis
{
    public class TripletCounter
    {
        string text = "";

        public async void ReadFileAsync(string inputPath)
        {
            string path = inputPath;

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    text = await sr.ReadToEndAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public IEnumerable<string> CountTriplets()
        {
            string s = text;
            int length = 3;

            for (int i = length; i <= s.Length; i++)
                yield return s.Substring(i - length, length);
        }
    }
}
