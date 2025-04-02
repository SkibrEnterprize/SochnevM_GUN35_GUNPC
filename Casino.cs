using System.Runtime.CompilerServices;

namespace GameInConsole
{
    public class Casino : IGame
    {
        private PlayerProfile _playerProfile = new PlayerProfile();
        public void StartGame()
        {
            Console.WriteLine("Привет, мистер! Начинаем игру в наше казино!");
            LoadOrCreatePlayerProfile();
        }

        public void LoadOrCreatePlayerProfile()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "player_profile.txt");

            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        _playerProfile.Name = reader.ReadLine();
                        _playerProfile.Money = int.Parse(reader.ReadLine());
                        _playerProfile.TotalWin = int.Parse(reader.ReadLine());
                        _playerProfile.TotalLose = int.Parse(reader.ReadLine());
                        _playerProfile.TotalDraw = int.Parse(reader.ReadLine());
                    }
                    Console.WriteLine($"Профиль игрока загружен из файла: {filePath}");
                    Console.WriteLine($"Имя игрока\t{_playerProfile.Name}\nДенег\t\t{_playerProfile.Money}\nПобед\t\t{_playerProfile.TotalWin}\nПоражений\t{_playerProfile.TotalLose}\nНичья\t\t{_playerProfile.TotalDraw}\n");

                }
                else
                {
                    Console.WriteLine("Файл профиля игрока не найден.");
                    CreateDefaultProfile(filePath); // Создаем новый файл, если его нет
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке профиля: {ex.Message}");
            }
        }

        private void CreateDefaultProfile(string filePath)
        {
            Console.WriteLine("Для создания нового профиля введите имя игрока:");
            _playerProfile = new PlayerProfile { Name = Console.ReadLine(), Money = 100, TotalDraw = 0, TotalLose = 0, TotalWin = 0 };
            SavePlayerProfile(_playerProfile, filePath); // Сохраняем новый профиль в файл
            Console.WriteLine("Профиль игрока успешно создан:");
            Console.WriteLine($"Имя игрока\t{_playerProfile.Name}\nДенег\t\t{_playerProfile.Money}\nПобед\t\t{_playerProfile.TotalWin}\nПоражений\t{_playerProfile.TotalLose}\nНичья\t\t{_playerProfile.TotalDraw}\n");


        }

        public void SavePlayerProfile(PlayerProfile playerProfile, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(playerProfile.Name);
                    writer.WriteLine(playerProfile.Money);
                    writer.WriteLine(playerProfile.TotalWin);
                    writer.WriteLine(playerProfile.TotalLose);
                    writer.WriteLine(playerProfile.TotalDraw);
                }
                Console.WriteLine($"Профиль игрока сохранен в файл: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении профиля: {ex.Message}");
            }
        }


        public void SelectGame()
        {
        }
    }
}
