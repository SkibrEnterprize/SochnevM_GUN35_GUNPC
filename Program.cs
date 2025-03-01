/*
 Задание 1
С помощью цикла for (или while) выведите первые 10 чисел Фиббоначи (см. Задание из 3 урока)

Задание 2
Используя цикл for, выведите все чётные числа от 2 до 20

Задание 3
С помощью вложенных циклов for выведите таблицу умножения от 1 до 5. Каждая строка таблицы должна быть выведена в отдельной строке.

Задание 4
Дана строка string password = “qwerty”; Напишите программу для ввода пароля, которая считывает пользовательский ввод Console.ReadLine. Подсказка: используйте do-while

Шаблон домашнего задания


*/

namespace LerningOfCycle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Задание 1

            int sum = 0;
            int numFirst = 1;
            int numSecond = 0;

            Console.WriteLine("Exercise #1\r\nFirst ten Fibonacci number is:");

            for (int i = 0; i < 10; i++)
            {
                Console.Write($"{sum}, ");
                sum = numFirst + numSecond;
                numFirst = numSecond;
                numSecond = sum;
            }

            Console.WriteLine();
            Console.WriteLine();

            // Задание 2

            Console.WriteLine("Exercise #2\r\nEven numbers from 2 to 20 is:");

            for (int i = 2; i <= 20; i++)
            {
                if (i % 2 == 0)
                {
                    Console.Write($"{i}, ");
                }
            }
            Console.WriteLine();
            Console.WriteLine();

            // Задание 3

            Console.WriteLine("Exercise #3\r\nMultiplication table from 1 to 5 is");

            int multTab = 5;

            for (int i = 1; i <= multTab; i++)
            {
                for (int j = 1; j <= multTab; j++)
                {
                    Console.Write(i * j + "\t");
                }
                Console.WriteLine("\r\n");
            }

            // Задание 4

            Console.WriteLine("Exercise #4\r\nEnter your psassword here:");

            string password = "qwerty";
            string userPassword = "";

            do
            {
                userPassword = Convert.ToString(Console.ReadLine());
                if (password == userPassword)
                {
                    Console.WriteLine("User password is correct!!!");
                    break;
                }
                Console.WriteLine("User password is incorrect\n\bplease try again:");
            }
            while (true);

            Console.ReadKey();
        }
    }
}
