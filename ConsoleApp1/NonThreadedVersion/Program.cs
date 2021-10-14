using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace NonThreadedVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = new Stopwatch();
            watch.Start();

            GetWordsArray();
            GetShortestWord();
            GetLongestWord();
            GetAverageWordLength();
            GetFiveMostCommonWords();
            GetFiveLeastCommonWords();

            watch.Stop();
            Console.WriteLine($"Exec time: {watch.ElapsedMilliseconds}");
        }

        public static void GetWordsArray()
        {
            string text = File.ReadAllText("book.txt", Encoding.Default);

            char[] delimiters = new char[] { ' ', '\r', '\n' };
            var words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).OrderBy(w => w.Length).ToArray();

            Console.WriteLine("Number of words: " + words.Length);
        }

        public static void GetShortestWord()
        {
            string text = File.ReadAllText("book.txt", Encoding.Default);

            char[] delimiters = new char[] { ' ', '\r', '\n' };
            var words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).OrderBy(w => w.Length).ToArray();

            Console.WriteLine("Shortest word: " + words.FirstOrDefault());
        }

        public static void GetLongestWord()
        {
            string text = File.ReadAllText("book.txt", Encoding.Default);

            char[] delimiters = new char[] { ' ', '\r', '\n' };
            var words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).OrderBy(w => w.Length).ToArray();

            Console.WriteLine("Longest word: " + words.LastOrDefault());
        }

        public static void GetAverageWordLength()
        {
            string text = File.ReadAllText("book.txt", Encoding.Default);

            char[] delimiters = new char[] { ' ', '\r', '\n' };
            var words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).OrderBy(w => w.Length).ToArray();

            Console.WriteLine("Average word length: " + words.Average(w => w.Length));
        }

        public static void GetFiveMostCommonWords()
        {
            string text = File.ReadAllText("book.txt", Encoding.Default);

            char[] delimiters = new char[] { ' ', '\r', '\n' };
            var words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).OrderBy(w => w.Length).ToArray();

            var commonWords = words
                    .GroupBy(x => x)
                    .Select(x => new {
                        KeyField = x.Key,
                        Count = x.Count()
                    })
                    .OrderByDescending(x => x.Count)
                    .Take(5).ToList();

            Console.WriteLine("Five most common words: " + string.Join(' ', commonWords.Select(o => o.KeyField)));
        }

        public static void GetFiveLeastCommonWords()
        {
            string text = File.ReadAllText("book.txt", Encoding.Default);

            char[] delimiters = new char[] { ' ', '\r', '\n' };
            var words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).OrderBy(w => w.Length).ToArray();

            var leastCommonWords = words
                    .GroupBy(x => x)
                    .Select(x => new {
                        KeyField = x.Key,
                        Count = x.Count()
                    })
                    .OrderBy(x => x.Count)
                    .Take(5).ToList();

            Console.WriteLine("Five least common words: " + string.Join(' ', leastCommonWords.Select(o => o.KeyField)));
        }
    }
}
