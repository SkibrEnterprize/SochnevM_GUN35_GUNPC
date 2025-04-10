namespace ReadAndLoadData
{
    public partial class Casino
    {
        public struct Dice
        {
            public readonly int Number;
            private int _min;
            private int _max;

            public Dice(int min, int max)
            {

                _min = CheckNumber(min);
                _max = CheckNumber(max);
                Number = new Random().Next(min, max);
            }

            public static int CheckNumber(int number)
            {
                if (number < 1 || number > int.MaxValue)
                {
                    throw new WrongDiceNumberException($"Вы указали {number}, а допустимый диапазон от 1 до {int.MinValue}");
                }
                else
                {
                return number;
                }
            }

        }
    }
}
