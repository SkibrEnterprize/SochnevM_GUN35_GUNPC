
namespace LearnOfCollections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter 1,2 or 3 to check task 1,2 or 3");
            int.TryParse(Console.ReadLine(), out int task);
            //int task = int.Parse(Console.ReadLine()); // Используйте tryParse
            switch (task)
            {
                case 1:
                    CheckTaskFirst(); // Выполнение задания в отдельном методе
                    break;
                case 2:
                    CheckTaskSecond();
                    break;
                case 3:
                    CheckTaskThird();
                    break;
                default:
                    Console.WriteLine("Input is incorrect!!!");
                    break;
            }
        }

        private static void CheckTaskFirst()
        {
            var listTask = new ListTask();
            listTask.TaskLoop();
        }

        private static void CheckTaskSecond()
        {
            var dictonaryTask = new DictionaryTask();
            dictonaryTask.TaskLoop();
        }
        private static void CheckTaskThird()
        {
            var doublyLinkedList = new DoublyLinkedList();
            doublyLinkedList.TaskLoop();
        }


        // Задание 1
        private class ListTask
        {
            private List<string> _listOfString; // Тип данных любой
            private string _newElement;

            public ListTask()
            {
                _listOfString = new List<string>();
                _listOfString.Add("Element 1");
                _listOfString.Add("Element 2");
                _listOfString.Add("Element 3");
            }


            public void TaskLoop()
            {
                // проверка ввода и вывод результата
                {
                    int cnt = 1;
                    do
                    {
                        Console.WriteLine($"Дополняем список List\nВведите новый элемент #{cnt}. Для отмены операции введите '-exit'");
                        _newElement = Console.ReadLine();
                        if (cnt == 1)
                        {
                            _listOfString.Add(_newElement);
                            cnt++;
                        }
                        else
                        {
                            _listOfString.Insert(_listOfString.Count / 2, _newElement);
                            cnt++;
                        }

                    } while (cnt != 3 && _newElement != "-exit"); //);  
                    PrintOfList();
                }
            }

            private void PrintOfList()
            {
                Console.WriteLine("\nИтоговый список выгдядит так:");
                foreach (var item in _listOfString)
                {
                    Console.WriteLine(item);
                }
            }


        }

        // Задание 2
        private class DictionaryTask
        {
            private Dictionary<string, int> _dictonary;
            private string _newStudent;
            private string _score;
            public DictionaryTask()
            {
                _dictonary = new Dictionary<string, int>();

            }
            public void TaskLoop()
            {
                // проверка ввода и вывод результата
                {
                    do
                    {
                        Console.WriteLine($"Заполняем информацию о средних оценках студентов\nВведите имя студента\n(Для отмены операции введите '-exit'):");
                        _newStudent = Console.ReadLine();
                        if (_newStudent == "-exit")
                        {
                            break;
                        }
                        Console.WriteLine($"Заполняем информацию о средних оценках студентов\nВведите среднюю оценку студента\n(Для отмены операции введите '-exit'):");
                        _score = Console.ReadLine();
                        if (_score == "-exit")
                        {
                            break;
                        }
                        if (int.TryParse(_score, out int score) && score >= 2 && score <= 5)
                        {
                            _dictonary.Add(_newStudent, score);
                        }
                        else
                        {
                            Console.WriteLine("Оценка введена не корректно");

                        }
                    } while (_newStudent != "-exit" && _score != "-exit"); //);  

                    Console.WriteLine("Введите имя студента для просмотра средней оценки:");
                    string nameForInfo = Console.ReadLine();
                    if (_dictonary.TryGetValue(nameForInfo, out int scoreForInfo))
                    {
                        Console.WriteLine($"Студент по имени {nameForInfo} имеет среднюю оценку {scoreForInfo}");
                    }
                    else
                    {
                        Console.WriteLine("Студента с таким именем не существует");
                    }

                    //PrintOfDictionary();
                }
            }
            //private void PrintOfDictionary()
            //{
            //    Console.WriteLine("\nИтоговый список выгдядит так:");
            //    foreach (var item in _dictonary)
            //    {
            //        Console.WriteLine($"{item.Value} средняя оценка {item.Key}");
            //    }
            //}
        }

        // Задание 3
        private class DoublyLinkedList

        {
            private class Node // Узел списка
            {
                public string Data { get; set; }
                public Node Previous { get; set; }
                public Node Next { get; set; }

                public Node(string data)
                {
                    Data = data;
                    Previous = null;
                    Next = null;
                }
            }
            private Node _head;
            private Node _tail;
            private int _size;

            public DoublyLinkedList()
            {
                _head = null;
                _tail = null;
                _size = 0;
            }

            public void AddLast(string data)
            {
                Node newNode = new Node(data);
                if (_head == null)
                {
                    _head = newNode;
                    _tail = newNode;
                }
                else
                {
                    newNode.Previous = _tail;
                    _tail.Next = newNode;
                    _tail = newNode;
                }
                _size++;
            }
            public void PrintForward()
            {
                Node current = _head;
                while (current != null)
                {
                    Console.Write(current.Data + " ");
                    current = current.Next;
                }
                Console.WriteLine();
            }

            // Вывести список в обратном порядке
            public void PrintBackward()
            {
                Node current = _tail;
                while (current != null)
                {
                    Console.Write(current.Data + " ");
                    current = current.Previous;
                }
                Console.WriteLine();
            }

            public void TaskLoop()
            {
                DoublyLinkedList myList = new DoublyLinkedList();
                Console.Write("Введите количество элементов в списке (от 3 до 6): ");
                int count = int.Parse(Console.ReadLine());

                if (count < 3 || count > 6)
                {
                    Console.WriteLine("Некорректное количество элементов.  Введено от 3 до 6.");
                    return;
                }

                for (int i = 0; i < count; i++)
                {
                    Console.Write($"Введите элемент {i + 1}: ");
                    string data = Console.ReadLine();
                    myList.AddLast(data);
                }

                Console.WriteLine("Список в прямом порядке:");
                myList.PrintForward();

                Console.WriteLine("Список в обратном порядке:");
                myList.PrintBackward();
            }

        }

    }
}

