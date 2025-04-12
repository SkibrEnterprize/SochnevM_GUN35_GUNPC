namespace ReadAndLoadData
{

    public class FileSystemSaveLoadService : ISaveLoadService<string>
    {
        private string _filePath { get; }
        public FileSystemSaveLoadService(string filePath)
        {
            _filePath = filePath;
        }

        public void SaveData(string data, string id)
        {
            string fileName = $"{id}.txt";
            string fullPath = Path.Combine(_filePath, fileName);

            try
            {
                File.WriteAllText(fullPath, data.ToString());
                Console.WriteLine($"Данные успешно сохранены как: {fileName}\nв расположение {_filePath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

        public string LoadData(string id)
        {
            string fileName = $"{id}.txt";
            string fullPath = Path.Combine(_filePath, fileName);

            try
            {
                if (File.Exists(fullPath))
                {

                    Console.WriteLine($"О-о-о, вот это встреча!!!\nТак Вы же уже были в нашем Казино!!!\nДанные успешно загружены из: {fileName} из расположения {_filePath}");
                    return File.ReadAllText(fullPath);

                }
                else
                {
                    Console.WriteLine($"Файл с ID '{id}' не найден в расположении {_filePath}.");
                    SaveData(fullPath, id);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Ошибка при загрузке данных: {ex.Message} из расположения {_filePath}");
                return null;
            }
        }

        //public string Serialize(PlayerProfile profile)
        //{
        //    return $"Name:{profile.Name},Money:{profile.Money},TotalWin:{profile.TotalWin},TotalLose:{profile.TotalLose},TotalDraw:{profile.TotalDraw}";
        //}

        //public PlayerProfile Deserialize(string data)
        //{
        //    var parts = data.Split(',');
        //    PlayerProfile profile = new PlayerProfile();
        //    profile.Name = parts[0].Replace("Name:", "");
        //    profile.Money = int.Parse(parts[1].Replace("Money:", ""));
        //    profile.TotalWin = int.Parse(parts[2].Replace("TotalWin:", ""));
        //    profile.TotalLose = int.Parse(parts[3].Replace("TotalLose:", ""));
        //    profile.TotalDraw = int.Parse(parts[4].Replace("TotalDraw:", ""));
        //    return profile;
        //}

    }
}

