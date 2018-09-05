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
            Int32.TryParse(Console.ReadLine(), out var maxNum);

            var fizzBuzzer = new FizzBuzzGame(1, maxNum);

            foreach (var value in fizzBuzzer.Play())
            {
                Console.WriteLine(value);
            }
        }
    }

    class FizzBuzzGame
    {
        private int _minNum;
        private int _maxNum;
        
        public FizzBuzzGame(int minNum = 1, int maxNum = 100)
        {
            _minNum = minNum;
            _maxNum = maxNum;
        }

        static readonly Dictionary<int, Action<List<string>>> Rules = new Dictionary<int, Action<List<string>>>()
            {
                {3,  CreateUpdateFunc(AppendLast, "Fizz")},
                {5,  CreateUpdateFunc(AppendLast, "Buzz")},
                {7,  CreateUpdateFunc(AppendLast, "Bang")},
                {11, CreateUpdateFunc(ExclusiveCase, "Bong")},
                {13, CreateUpdateFunc(AppendBeforeB, "Fezz")},
                {17, CreateUpdateFunc(Reverse, null)}
            };

        private static Action<List<string>> CreateUpdateFunc(Action<List<string>, string> updateBehaviour, string keyword)
        {
            return (existingStrings) => updateBehaviour(existingStrings, keyword);
        }

        private static void AppendLast(List<string> generatedStringsList, string keyword)
        {
            generatedStringsList.Add(keyword);
        }


        private static void ExclusiveCase(List<string> generatedStringsList, string keyword)
        {
            generatedStringsList.Clear();
            generatedStringsList.Add(keyword);
        }

        private static int FindIndexCharBeginInStrArray(List<string> givenList, char givenChar)
        {
            return givenList.FindIndex(stringToCheck => stringToCheck[0] == givenChar);
        }

        private static void AppendBeforeB(List<string> generatedStringsList, string keyword)
        {
            int indexB = FindIndexCharBeginInStrArray(generatedStringsList, 'B');
            if (indexB >= 0)
            {
                generatedStringsList.Insert(indexB, keyword);
            }
            else
            {
                AppendLast(generatedStringsList, keyword);
            }
        }

        private static void Reverse(List<string> generatedStringsList, string keyword)
        {
            generatedStringsList.Reverse();
        }

        public IEnumerable<string> Play()
        {
            for (int i = _minNum; i < _maxNum; i++)
            {
                List<string> returnStrings = new List<string>();
                // for each number applies all the suitable rules
                foreach (var rule in Rules)
                {
                    var divisor = rule.Key;
                    var stringsUpdateFunc = rule.Value;
                    // if this number is dividable by the key, so the rule should be applied
                    if (i % divisor == 0)
                    {
                        stringsUpdateFunc(returnStrings);
                    }
                }

                // if no rules has been applied, the number itself should be printed
                if (!returnStrings.Any())
                {
                    returnStrings.Add(i.ToString());
                }
                yield return String.Join("", returnStrings);
            }
        }
    }
}
