using GameInConsole.Games;

namespace ReadAndLoadData
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
        }
        public override void PlayGame()
        {
            OnWin += PrintResultsInConsole;
            OnLoose += PrintResultsInConsole;
            OnDraw += PrintResultsInConsole;
            GameMechanics();
            OnWin -= PrintResultsInConsole;
            OnLoose -= PrintResultsInConsole;
            OnDraw -= PrintResultsInConsole;
        }

        private void GameMechanics()
        {
            Random random = new Random();
            Console.WriteLine("Кубики кидаете Вы...");
            for (int i = 0; i < _numberOfDice; i++)
            {
                _sumOfPlayer += random.Next(_minDiceValue, _maxDiceValue);
            }

            Console.WriteLine("Кубики кидает оппонент...");
            for (int i = 0; i < _numberOfDice; i++)
            {
                _sumOfComputer += random.Next(_minDiceValue, _maxDiceValue);
            }

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

