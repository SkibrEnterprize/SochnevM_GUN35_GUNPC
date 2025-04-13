namespace GameInConsole.Utils
{

    public class FileSystemSaveLoadService : ISaveLoadService
    {
        private string _filePath { get; }
        public FileSystemSaveLoadService(string filePath)
        {
            _filePath = filePath;
        }

        public void SaveData(PlayerProfile data, string id)
        {
            try
            {
                string filePath = $"player_data_{id}.txt";

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"Name: {data.Name}");
                    writer.WriteLine($"Money: {data.Money}");
                    writer.WriteLine($"TotalWin: {data.TotalWin}");
                    writer.WriteLine($"TotalLose: {data.TotalLose}");
                    writer.WriteLine($"TotalDraw: {data.TotalDraw}");
                }

                Console.WriteLine($"Данные игрока {id} сохранены в файле {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении данных игрока {id}: {ex.Message}");
            }
        }

        public bool TryToLoadData(string id, out PlayerProfile data)
        {
            try
            {
                string filePath = $"player_data_{id}.txt";

                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        data = new PlayerProfile
                        {
                            Name = reader.ReadLine()?.Split(':')[1].Trim(),
                            Money = int.Parse(reader.ReadLine()?.Split(':')[1].Trim()),
                            TotalWin = int.Parse(reader.ReadLine()?.Split(':')[1].Trim()),
                            TotalLose = int.Parse(reader.ReadLine()?.Split(':')[1].Trim()),
                            TotalDraw = int.Parse(reader.ReadLine()?.Split(':')[1].Trim())
                        };

                        return true;
                    }
                }

                data = null;
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных игрока {id}: {ex.Message}");
                data = null;
                return false;
            }
        }
    }
}

