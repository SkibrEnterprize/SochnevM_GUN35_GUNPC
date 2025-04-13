using GameInConsole.Utils;
using ReadAndLoadData;

namespace GameInConsole.Games
{
    public class Casino : IGame
    {
        private static string _filePath = Environment.CurrentDirectory;
        private FileSystemSaveLoadService _service = new FileSystemSaveLoadService(_filePath);
        private string _nameOfPlayers;
        private PlayerProfile _loadedPlayer;
        private int _playerBet;
        private int _computerBet;
        private int _casinoBank = 200;
        private bool _isGameOver = false;
        public void StartGame()
        {
            Console.WriteLine("Привет, мистер!");
            LoadOrCreateProfile();
            SelectGame();
            _service.SaveData(_loadedPlayer, _nameOfPlayers);
            Console.WriteLine("Игра окончена!");
        }



        private void LoadOrCreateProfile()
        {
            Console.WriteLine("Начинаем игру в наше казино!\nВведите имя:");
            _nameOfPlayers = Console.ReadLine();

            if (_service.TryToLoadData(_nameOfPlayers, out _loadedPlayer))
            {
                Console.WriteLine($"\nОго, какие люди!!! Вы уже у нас были. Вот Ваша статистика:\nИмя - {_loadedPlayer.Name},\nДенег - {_loadedPlayer.Money},\nПобед - {_loadedPlayer.TotalWin},\nПроигрышей - {_loadedPlayer.TotalLose},\nНичья - {_loadedPlayer.TotalDraw}");
                _casinoBank += _loadedPlayer.Money;
            }
            else
            {
                _loadedPlayer = new PlayerProfile(_nameOfPlayers);
                _service.SaveData(_loadedPlayer, _nameOfPlayers);
            }            
        }

        public void SelectGame()
        {

            string choice = "";
            while (!_isGameOver)
            {
                Console.WriteLine("\nВыберите игру:");
                Console.WriteLine("1. БлэкДжек");
                Console.WriteLine("2. Игральные кости");
                Console.WriteLine("3. Для выхода");
                choice = Console.ReadLine();

                if (choice == "1")
                {
                    PlaceBet();
                    StartBlackJackGame();
                    CheckMoney();
                }
                else if (choice == "2")
                {
                    PlaceBet();
                    StartDiceGame();
                    CheckMoney();
                }
                else if (choice == "3")
                {
                    _isGameOver = true;
                }
                else
                {
                    Console.WriteLine("Неверный выбор!");
                }
            }

        }

        private void CheckMoney()
        {
            if (_casinoBank<=0)
            {
                Console.WriteLine($"Вы разорили казино и на его месте построят новое. Итоговый банк - {_loadedPlayer.Money}");
                _isGameOver = true;
            }
            else if (_loadedPlayer.Money <= 0)
            {
                Console.WriteLine("No money? Kicked!");
                _isGameOver = true;
            }

        }

        private void PlaceBet()
        {
            Console.WriteLine($"Пожалуйста введите Вашу ставку. Сейчас у Вас денег - {_loadedPlayer.Money}");
            int bank = Convert.ToInt32(Console.ReadLine());
            if (bank >= _loadedPlayer.Money)
            {
                bank = _loadedPlayer.Money;
                _playerBet = bank;
            }
            else
            {
                _playerBet = bank;
            }

            _computerBet = _playerBet;
            Console.WriteLine($"Оппонент ставит столько же - {_computerBet}");
        }

        private void StartBlackJackGame()
        {
            BlackJackGame blackJackGame = new BlackJackGame(40);
            blackJackGame.OnWin += Game_OnWin;
            blackJackGame.OnWin += blackJackGame.PrintResultsInConsole;
            blackJackGame.OnLoose += Game_OnLoose;
            blackJackGame.OnLoose += blackJackGame.PrintResultsInConsole;
            blackJackGame.OnDraw += Game_OnDraw;
            blackJackGame.OnDraw += blackJackGame.PrintResultsInConsole;
            blackJackGame.PlayGame();
            blackJackGame.OnWin -= Game_OnWin;
            blackJackGame.OnWin -= blackJackGame.PrintResultsInConsole;
            blackJackGame.OnLoose -= Game_OnLoose;
            blackJackGame.OnLoose -= blackJackGame.PrintResultsInConsole;
            blackJackGame.OnDraw -= Game_OnDraw;
            blackJackGame.OnDraw -= blackJackGame.PrintResultsInConsole;
        }

        private void StartDiceGame()
        {
            DiceGame diceGame = new DiceGame(4, 1, 6);
            diceGame.OnWin += Game_OnWin;
            diceGame.OnWin += diceGame.PrintResultsInConsole;
            diceGame.OnLoose += Game_OnLoose;
            diceGame.OnLoose += diceGame.PrintResultsInConsole;
            diceGame.OnDraw += Game_OnDraw;
            diceGame.OnDraw += diceGame.PrintResultsInConsole;
            diceGame.PlayGame();
            diceGame.OnWin -= Game_OnWin;
            diceGame.OnWin -= diceGame.PrintResultsInConsole;
            diceGame.OnLoose -= Game_OnLoose;
            diceGame.OnLoose -= diceGame.PrintResultsInConsole;
            diceGame.OnDraw -= Game_OnDraw;
            diceGame.OnDraw -= diceGame.PrintResultsInConsole;
        }

        private void Game_OnLoose()
        {
            _loadedPlayer.TotalLose++;
            _loadedPlayer.Money -= _playerBet;
            _casinoBank += _playerBet;
        }
        private void Game_OnDraw()
        {
            _loadedPlayer.TotalDraw++;
            Console.WriteLine("У Вас ничья - играем снова!");
        }

        private void Game_OnWin()
        {
            _loadedPlayer.TotalWin++;
            _loadedPlayer.Money += _computerBet;
            _casinoBank -= _computerBet;
        }
    }
}
