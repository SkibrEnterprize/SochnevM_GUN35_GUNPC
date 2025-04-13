using Newtonsoft.Json;

namespace GameInConsole.Utils
{
    public class FileSystemSaveLoadService : ISaveLoadService
    {
        private readonly string _filePath;

        public FileSystemSaveLoadService(string filePath)
        {
            _filePath = filePath;
        }

        public void SaveData(PlayerProfile data, string id)
        {
            try
            {
                string filePath = $"player_data_{id}.json";

                string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);

                File.WriteAllText(filePath, jsonString);

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
                string filePath = $"player_data_{id}.json";

                if (File.Exists(filePath))
                {
                    string jsonString = File.ReadAllText(filePath);

                    data = JsonConvert.DeserializeObject<PlayerProfile>(jsonString);

                    return true;
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

