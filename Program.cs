
using System.Text;
using System.Text.RegularExpressions;

namespace LearningOfStrings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в демонстрацию работы со строками в языке C#\nНажмите \"Enter\" для запуска первого задания\n");
            Console.ReadKey();

            // Задание 1
            ConcatinateString();

            // Задание 2
            GreetUser();

            // Задание 3
            Console.WriteLine("Задание №3 \'Операции со строками - 1\'\n\nВедите строку:");
            Console.WriteLine($"\nНовая строка выглядит так:\n{StringOperations(Console.ReadLine())}");
            EndOfTask();

            // Задание 4
            Console.WriteLine("Задание №4 \'Операции со строками - 2\'\n\nВедите строку(минимально 5 символов):\n");
            Console.WriteLine($"\nПервые пять символов введенной строки:\n{ReturnOfFiveSymbol(Console.ReadLine())}");
            EndOfTask();

            // Задание 5
            Console.WriteLine("Задание №5 \'Вывод экземпляра StringBuilder\'\nЗаполните массив введением значений пяти строк поочередно(через \'Enter\'):\n");
            string[] str = new string[5];
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Введите строку №{i + 1}");
                str[i] = Console.ReadLine();
            }
            Console.WriteLine($"Результат выполнения:\n{StringBuilderReturn(str).ToString()}");
            EndOfTask();

            // Задание 6
            Console.WriteLine("Задание №6 \'Регулярные выражения\'\n\nВведена строка:");
            string str1 = "Сшит колпак, да не по-колпаковски. Надо колпак переколпаковать";
            Console.WriteLine($"\"{str1}\"\n\nРезультат обработки через регулярные выражения:");
            Console.WriteLine(RegexExample(str1, "колпа", "червя"));
            Console.ReadKey();
        }

        static void ConcatinateString()
        {
            Console.WriteLine("Задание №1 \'Конкатенация\'\n\nВедите первую строку:");
            string str1 = Console.ReadLine();
            Console.WriteLine("Ведите вторую строку:");
            string str2 = Console.ReadLine();
            Console.WriteLine("\nКонкатенация строк выглядит так:\n");
            Console.WriteLine(String.Concat(str1, str2));
            EndOfTask();

        }

        static void GreetUser()
        {
            Console.WriteLine("Задание №2 \'Метод GreetUser\'\n\nВедите имя пользователя:");
            string name = Console.ReadLine();
            Console.WriteLine("Ведите возраст пользователя:");
            string age = Console.ReadLine();
            Console.WriteLine("\nВывод метода GreetUser():\n");
            Console.WriteLine($"Hello, {name}!\nYou are {age} years old.");
            EndOfTask();

        }

        static string StringOperations(string str)
        {
            string newStr = string.Format("Количество символов в строке - {0}. Строка в верхнем регистре:\"{1}\". Строка в нижнем регистре:\"{2}\"", str.Length, str.ToUpper(), str.ToLower());
            return newStr;
        }

        static string ReturnOfFiveSymbol(string str)
        {
            return str.Substring(0, 5);
        }

        static StringBuilder StringBuilderReturn(string[] array)
        {
            Console.WriteLine("\nМассив заполнен удачно и передан в StringBuilder\n");
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string str in array)
            {
                stringBuilder.Append(str + ' ');
            }
            return stringBuilder;
        }

        static string RegexExample(string str, string word1, string word2)
        {
            Regex regex = new Regex(word1);
            return str = regex.Replace(str, word2); 
        }
        static void EndOfTask()
        {
            Console.WriteLine("\nНажмите \"Enter\" для запуска следующего задания");
            Console.ReadKey();
            Console.Clear();
        }
    }
}

