
/*  В калькуляторе пользователю предлагается ввести первое число, затем второе
    В случае ошибки есть предупреждение для пользователя, и настроен выход из программы
    Пользователю предлагается ввести оператор: & | или ^, и запрограммирована проверка ввода (введён корректный символ)
    В зависимости от ввода выводится результат побитовой операции
    Результат выводится в десятичной, двоичной и шестнадцатеричной форме
*/

namespace StydyOfNetology
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to logical operations calculator!!!");
            Console.ResetColor();
            Console.WriteLine("Please, enter any first number here:");

            if (int.TryParse(Console.ReadLine(), out int num1))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Your first number is: " + num1);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error!!!\r\nYou entered a value in an incorrect format, you should have entered a number! ");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Now enter your second number here:");
            if (int.TryParse(Console.ReadLine(), out int num2))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Your second number is: " + num2);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error!!!\r\nYou entered a value in an incorrect format, you should have entered a number! ");
                Console.ResetColor();
                return;
            }
            Console.WriteLine("And finally - enter the operator character for the action.\r\navailable operators: & | ^");

            string s = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Green;

            if (s == "&" || s == "|" || s == "^")
            {
                Console.WriteLine("The action is performed: {0} {2} {1}", num1, num2, s);
                switch (s)
                {
                    case "&":
                        //PrintOfResult(num1, num2, s);
                        Console.WriteLine("Result in decimal form: " + Convert.ToString(num1 & num2));
                        Console.WriteLine("Result in binary form: " + Convert.ToString(num1 & num2, 2));
                        Console.WriteLine("Result in hexadecimal form: " + Convert.ToString(num1 & num2, 16));
                        Console.ResetColor();
                        break;
                    case "|":
                        //PrintOfResult(num1, num2, s);
                        Console.WriteLine("Result in decimal form: " + Convert.ToString(num1 | num2));
                        Console.WriteLine("Result in binary form: " + Convert.ToString(num1 | num2, 2));
                        Console.WriteLine("Result in hexadecimal form: " + Convert.ToString(num1 | num2, 16));
                        Console.ResetColor();
                        break;
                    case "^":
                        //PrintOfResult(num1, num2, s);
                        Console.WriteLine("Result in decimal form: " + Convert.ToString(num1 ^ num2));
                        Console.WriteLine("Result in binary form: " + Convert.ToString(num1 ^ num2, 2));
                        Console.WriteLine("Result in hexadecimal form: " + Convert.ToString(num1 ^ num2, 16));
                        Console.ResetColor();
                        break;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You entered a value in an incorrect format, you should have entered one of available operators: & | ^!");
                Console.ResetColor();
                return;
            }
        }
        //    public static void PrintOfResult(int a, int b, string str)
        //{
        //    Console.WriteLine("Result in decimal form: " + Convert.ToString(a & b));
        //    Console.WriteLine("Result in binary form: " + Convert.ToString(a & b, 2));
        //    Console.WriteLine("Result in hexadecimal form: " + Convert.ToString(a & b, 16));
        //    Console.ResetColor();
        //}
    }
}
