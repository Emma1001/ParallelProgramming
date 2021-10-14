using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = new Stopwatch();
            watch.Start();

            string text = File.ReadAllText("book.txt", Encoding.Default);

            string[] words = GetWordsArray(text);

            Console.WriteLine("Number of words: " + words.Length);
            Console.WriteLine("Shortest word: " + words.FirstOrDefault());
            Console.WriteLine("Longest word: " + words.LastOrDefault());
            Console.WriteLine("Average word length: " + words.Average(w => w.Length));
            Console.WriteLine("Five most common words: " + GetFiveMostCommonWords(words));
            Console.WriteLine("Five least common words: " + GetFiveLeastCommonWords(words));

            watch.Stop();
            Console.WriteLine($"Exec time: {watch.ElapsedMilliseconds}");
        }

        public static string[] GetWordsArray(string text)
        {
            char[] delimiters = new char[] { ' ', '\r', '\n' };
            return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).OrderBy(w => w.Length).ToArray();
        }

        public static string GetFiveMostCommonWords(string[] words)
        {
            var commonWords = words
                    .GroupBy(x => x)
                    .Select(x => new {
                        KeyField = x.Key,
                        Count = x.Count()
                    })
                    .OrderByDescending(x => x.Count)
                    .Take(5).ToList();

            return string.Join(' ', commonWords.Select(o => o.KeyField));
        }

        public static string GetFiveLeastCommonWords(string[] words)
        {
            var leastCommonWords = words
                    .GroupBy(x => x)
                    .Select(x => new {
                        KeyField = x.Key,
                        Count = x.Count()
                    })
                    .OrderBy(x => x.Count)
                    .Take(5).ToList();

            return string.Join(' ', leastCommonWords.Select(o => o.KeyField));
        }
    }
}
