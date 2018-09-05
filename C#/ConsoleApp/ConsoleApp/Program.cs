using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter maximum number for extended FizzBuzz game: ");
            int maxNum = Console.Read();

            new FizzBuzzGame(1, maxNum).Play();
        }
    }

    class FizzBuzzGame : IEnumerable<string>
    {
        private int _minNum = 1;
        private int _maxNum = 100;

        public FizzBuzzGame() {}

        public FizzBuzzGame(int minNum, int maxNum)
        {
            _minNum = minNum;
            _maxNum = maxNum;
        }

        static readonly Dictionary<int, KeyValuePair<Func<List<string>, string, List<string>>, string>> Rules =
            new Dictionary<int, KeyValuePair<Func<List<string>, string, List<string>>, string>>()
            {
                {3, new KeyValuePair<Func<List<string>, string, List<string>>, string>(AppendLast, "Fizz")},
                {5, new KeyValuePair<Func<List<string>, string, List<string>>, string>(AppendLast, "Buzz")},
                {7, new KeyValuePair<Func<List<string>, string, List<string>>, string>(AppendLast, "Bang")},
                {11, new KeyValuePair<Func<List<string>, string, List<string>>, string>(ExclusiveCase, "Bong")},
                {13, new KeyValuePair<Func<List<string>, string, List<string>>, string>(AppendBeforeB, "Fezz")},
                {17, new KeyValuePair<Func<List<string>, string, List<string>>, string>(Reverse, "N/A")}
            };
        
        private static List<string> AppendLast(List<string> generatedStringsList, string keyword)
        {
            generatedStringsList.Add(keyword);
            return generatedStringsList;
        }


        private static List<string> ExclusiveCase(List<string> generatedStringsList, string keyword)
        {
            return new List<string> {keyword};
        }

        private static int FindIndexCharBeginInStrArray(List<string> givenList, char givenChar)
        {
            return givenList.FindIndex(stringToCheck => stringToCheck[0] == givenChar);
        }

        private static List<string> AppendBeforeB(List<string> generatedStringsList, string keyword)
        {
            int indexB = FindIndexCharBeginInStrArray(generatedStringsList, 'B');
            if (indexB >= 0)
            {
                generatedStringsList.Insert(indexB, keyword);
                return generatedStringsList;
            }

            return AppendLast(generatedStringsList, keyword);
        }

        private static List<string> Reverse(List<string> generatedStringsList, string keyword)
        {
            generatedStringsList.Reverse();
            return generatedStringsList;
        }

        public void Play()
        {
            for (int i = _minNum; i < _maxNum; i++)
            {
                List<string> strList = new List<string>();
                // for each number applies all the suitable rules
                foreach (KeyValuePair<int, KeyValuePair<Func<List<string>, string, List<string>>, string>> rule in Rules)
                {
                    // if this number is dividable by the key, so the rule should be applied
                    if (i % rule.Key == 0)
                    {
                        strList = rule.Value.Key(strList, rule.Value.Value);
                    }
                }

                // if no rules has been applied, the number itself should be printed
                if (strList.Count == 0)
                {
                    strList.Add(i.ToString());
                }
                Console.WriteLine(String.Join("", strList));
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
