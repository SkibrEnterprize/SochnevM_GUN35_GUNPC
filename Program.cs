
namespace LearnOfClassesRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            Dungeon dangeon = new Dungeon();
            dangeon.ShowRooms();
            Console.ReadKey();
        }
    }

    public struct Interval
    {
        private Random _random;
        public int Min { get; }
        public int Max { get; }
        public int Get
        {
            get
            {
                return _random.Next(Min, Max);
            }
        }

        public Interval(int minValue, int maxValue)
        {
            _random = new Random();
            if (minValue > maxValue)
            {
                (minValue, maxValue) = (maxValue, minValue);
                ErrorValue();
            }
            else if (minValue < 0)
            {
                minValue = 0;
                ErrorValue();
            }
            else if (maxValue < 0)
            {
                maxValue = 0;
                ErrorValue();
            }
            else if (minValue == maxValue)
            {
                maxValue += 10;
                ErrorValue();
            }
            //minValue = minValue < 0 ? 0 : minValue; // если веденная величина меньше 0, то присваиваем ей 0
            //maxValue = maxValue < 0 ? 0 : maxValue;

            //maxValue = maxValue == minValue ? maxValue += 10 : maxValue; // если оба числа равны, то присваиваем Max+10

            Min = minValue;
            Max = maxValue;

            void ErrorValue()
            {
                Console.WriteLine($"Entered Minimal or Maximal value is incorrect");

            }

        }
    }

    public struct Room
    {
        public Unit Unit { get; }
        public Weapon Weapon { get; }

        public Room(Unit unit, Weapon weapon)
        {
            Unit = unit;
            Weapon = weapon;
        }
    }
}
