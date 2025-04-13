using GameInConsole.Utils;
using ReadAndLoadData;

namespace GameInConsole.Games
{

    public struct Dice
    {
        private int _min;
        private int _max;
        public readonly int Number => new Random().Next(_min, _max + 1);
       
        public Dice(int min, int max)
        {

            if (min < 1 || max > int.MaxValue)
            {
                throw new WrongDiceNumberException(max, "Некорректный диапазон чисел. Минимальное значение должно быть больше или равно 1, а максимальное - меньше или равно int.MaxValue.");
            }

            _min = min;
            _max = max;
        }
    }

    }


