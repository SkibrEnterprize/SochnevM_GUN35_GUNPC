/* Задача A: создать 4 массива внутри метода Main

Задание 1
Чи́сла Фибона́ччи - элементы числовой последовательности в которой первые два числа равны 0 и 1, а каждое последующее число равно сумме двух предыдущих чисел, 
т.е. 0, 1, 1, 2, 3, 5 и т.д. Создайте массив, сожержащий первые 8 чисел данной последовательности

Задание 2
Создайте массив типа string, содержащий название 12 месяцев. Названия должны быть на английском и начинаться с заглавной буквы. Также не нужно использовать 
пробелы и лишние символы, только строка с названием

Задание 3
Создайте двумерный массив (матрицу) 3x3.Вам нужно будет создать и проинициализировать двумерный массив типа int. Элементы массива-матрицы: Первая строка - 
числа 2, 3 и 4 в степени 1 Вторая строка - числа 2,3 и 4 в степени 2 Третья - числа 2,3 и 4 в степени 3

Задание 4
Вам нужно будет создать и проинициализировать jagged array (ломанный массив). То есть массив, содержащий массивы разного размера Должен содержать следующие 
элементы (тип double) Первый массив - числа от 1 до 5 Второй массив - константы e и pi (используйте класс math) Третий массив - логарифм по основанию 10 чисел 1, 10, 100 и 1000 (используя функцию log).
Важно! Используйте статический класс Math для констант и логарифмов Ссылка

Задача Б: Вам дано два массива int[] array = { 1, 2, 3, 4, 5 }; int[] array2 = { 7, 8, 9, 10, 11, 12, 13 };

Задание 5
Скопируйте первые 3 элемента первого массива во второй. Воспользуйтесь классом Array.

Задание 6
Измените размер первого массива так, чтобы в нём стало в два раза больше элементов Воспользуйтесь классом Array, метод Resize. ВАЖНО! Массив передаётся через ref. 
Это же ключевое слово вы будете использовать при вызове метода Resize, то есть: Array.Resize(ref array, newSize);
*/

namespace LerningOfArray
{
    class Program
    {
        static void Main(string[] args)
        {
            // Задача А

            // Здесь массивы заданий 1-4

            // Задание 1

            int[] fibonacciNumbers = new int[8] { 0, 1, 1, 2, 3, 5, 8, 12 };


            // Задание 2

            string[] month = new string[12] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };


            // Задание 3

            int[,] twoDimensionalArray = new int[3, 3]; //инициализируем массив значениями по-умолчанию
            twoDimensionalArray[0, 0] = 2; // заполняем первый массив вручную
            twoDimensionalArray[0, 1] = 3;
            twoDimensionalArray[0, 2] = 4;

            for (int i = 1; i < twoDimensionalArray.GetLength(0); i++) // заполняем второй и третий массивы в цикле
            {
                int pow = twoDimensionalArray[0, i - 1];
                for (int j = 0; j < twoDimensionalArray.GetLength(1); j++)
                {
                    twoDimensionalArray[i, j] = Convert.ToInt32(Math.Pow(twoDimensionalArray[0, j], pow));
                }
            }

            Console.WriteLine("Задание 3 проверка вывода");
            for (int i = 0; i < twoDimensionalArray.GetLength(0); i++) // проверка вывода массива для контроля правильности
            {
                Console.WriteLine();
                for (int j = 0; j < twoDimensionalArray.GetLength(1); j++)
                {
                    Console.Write($"{twoDimensionalArray[i, j]}, ");
                }
            }
            Console.WriteLine(new string('\n', 2));

            // Задание 4

            double[][] jaggedArray = new double[3][];

            jaggedArray[0] = new double[5] { 1, 2, 3, 4, 5 };
            jaggedArray[1] = new double[2] { Math.E, Math.PI };
            jaggedArray[2] = new double[4] { Math.Log(1, 10), Math.Log(10, 10), Math.Log(100, 10), Math.Log(1000, 10), };


            // Задача Б

            // Задание 5

            int[] array = { 1, 2, 3, 4, 5 };
            int[] array2 = { 7, 8, 9, 10, 11, 12, 13 };
            Array.Copy(array, array2, 3);

            Console.WriteLine("Задание 5 - вывод:");
            foreach (int element in array2) // Вывод результата
            {
                Console.Write(element + ", ");
            }
            Console.WriteLine(new string('\n', 2));

            // Задание 6

            string[] sample = { "", "" };
            Array.Resize(ref sample, sample.Length*2);

            Console.WriteLine("Задание 6 - вывод:");
            foreach (string element in sample) // Вывод результата
            {
                Console.Write(element + ", ");
            }

            Console.ReadKey();
        }
    }
}
