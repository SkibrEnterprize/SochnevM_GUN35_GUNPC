using System;

namespace GameInConsole.Games
{
    public class DiceGame : CasinoGameBase
    {
        private int _numberOfDice;
        private int _minDiceValue;
        private int _maxDiceValue;
        private int _sumOfPlayer;
        private int _sumOfComputer;
        private List<Dice> _dices = new List<Dice>();
        public DiceGame(int numberOfDice, int minDiceValue, int maxDiceValue)
        {
            _numberOfDice = numberOfDice;
            _minDiceValue = minDiceValue;
            _maxDiceValue = maxDiceValue;
            FactoryMethod();
        }
        public override void PlayGame()
        {            
            GameMechanics();
        }

        private void GameMechanics()
        {
            Console.WriteLine("Кубики кидаете Вы...");
            _sumOfPlayer = ThrowDice(_dices);

            Console.WriteLine("Кубики кидает оппонент...");
           _sumOfComputer = ThrowDice(_dices);

            if (_sumOfComputer > _sumOfPlayer)
            {
                Console.WriteLine("Вы проиграли!");
                OnLooseInvoke();
                return;
            }
            if (_sumOfComputer < _sumOfPlayer)
            {
                Console.WriteLine("Вы выиграли!");
                OnWinInvoke();
                return;
            }
            if (_sumOfComputer == _sumOfPlayer)
            {
                Console.WriteLine("Ничья!");
                OnDrawInvoke();
                return;
            }

        }

        private int ThrowDice(List<Dice> dices)
        {
            Random random = new Random();
            int sum = 0;
            foreach (Dice dice in dices)
            {                
                sum += dice.Number;
            }
            return sum;
        }

        public override void PrintResultsInConsole()
        {
            Console.WriteLine($"Итого у Вас выпало {_sumOfPlayer} очков\nУ оппонента выпало {_sumOfComputer} очков");
        }

        protected override void FactoryMethod()
        {
            for (int i = 0; i < _numberOfDice; i++)
            {
                _dices.Add(new Dice(_minDiceValue, _maxDiceValue));
            }
        }
    }
}

