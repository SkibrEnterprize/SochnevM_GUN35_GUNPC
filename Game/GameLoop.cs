using GamePrototype.Combat;
using GamePrototype.Dungeon;
using GamePrototype.Units;
using GamePrototype.Utils;

namespace GamePrototype.Game
{
    public sealed class GameLoop
    {
        private Unit _player;
        private DungeonRoom _dungeon;
        private readonly CombatManager _combatManager = new CombatManager();
        private int _choice;
        private Difficulty _selectedDifficulty;

        public void StartGame()
        {
            Initialize();
            Console.WriteLine("Entering the dungeon");
            StartGameLoop();
        }

        #region Game Loop

        private void Initialize()
        {
            Console.WriteLine("Welcome, player!");
            //_dungeon = Utils.Dungeon.BuildDungeon();
            Console.WriteLine("Enter your name");
            GeneratePlayer(); // Задание 3
            Console.WriteLine("Enter difficult of game: 1 - Easy, 2 - Hard");
            ChooseDifficult(); // Задание 3
            GenerateDungeon(); // Задание 3
            //_player = UnitFactoryDemo.CreatePlayer(Console.ReadLine());
            Console.WriteLine($"Hello {_player.Name}");
        }

        private void GeneratePlayer()
        {
            if (_selectedDifficulty == Difficulty.Easy)
            {
                _player = UnitFactoryDemo.CreatePlayer(Console.ReadLine(), Difficulty.Easy);

            }
            else
            {
                _player = UnitFactoryDemo.CreatePlayer(Console.ReadLine(), Difficulty.Easy);
            }
        }

        private void GenerateDungeon()
        {
            if (_selectedDifficulty == Difficulty.Easy)
            {
                _dungeon = new EasyDangeon("Easy").BuildDungeon();

            }
            else
            {
                _dungeon = new HardDangeon("Hard").BuildDungeon();
            }
        }

        private void ChooseDifficult()
        {

            _choice = int.Parse(Console.ReadLine());

            switch (_choice)
            {
                case 1:
                    _selectedDifficulty = Difficulty.Easy;
                    break;
                case 2:
                    _selectedDifficulty = Difficulty.Hard;
                    break;
                default:
                    Console.WriteLine("Incorrect input. Your choise selected on Easy");
                    _selectedDifficulty = Difficulty.Easy;
                    break;
            }
        }

        private void StartGameLoop()
        {
            var currentRoom = _dungeon;

            while (currentRoom.IsFinal == false)
            {
                StartRoomEncounter(currentRoom, out var success);
                if (!success)
                {
                    Console.WriteLine("Game over!");
                    return;
                }
                DisplayRouteOptions(currentRoom);
                while (true)
                {
                    if (Enum.TryParse<Direction>(Console.ReadLine(), out var direction))
                    {
                        currentRoom = currentRoom.Rooms[direction];
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong direction!");
                    }
                }
            }
            Console.WriteLine($"Congratulations, {_player.Name}");
            Console.WriteLine("Result: ");
            Console.WriteLine(_player.ToString());
        }

        private void StartRoomEncounter(DungeonRoom currentRoom, out bool success)
        {
            success = true;
            if (currentRoom.Loot != null)
            {
                _player.AddItemToInventory(currentRoom.Loot);
            }
            if (currentRoom.Enemy != null)
            {
                if (_combatManager.StartCombat(_player, currentRoom.Enemy) == _player)
                {
                    _player.HandleCombatComplete();
                    LootEnemy(currentRoom.Enemy);
                }
                else
                {
                    success = false;
                }
            }

            void LootEnemy(Unit enemy)
            {
                _player.AddItemsFromUnitToInventory(enemy);
            }
        }

        private void DisplayRouteOptions(DungeonRoom currentRoom)
        {
            Console.WriteLine("Where to go?");
            foreach (var room in currentRoom.Rooms)
            {
                Console.Write($"{room.Key} - {(int)room.Key}\t");
            }
        }


        #endregion
    }
}
