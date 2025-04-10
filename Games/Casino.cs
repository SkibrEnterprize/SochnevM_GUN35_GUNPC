
using GameInConsole.Utils;

namespace ReadAndLoadData
{
    public partial class Casino : IGame
    {
        private static string _filePath = Environment.CurrentDirectory;
        private FileSystemSaveLoadService _service = new FileSystemSaveLoadService(_filePath);

        public void StartGame()
        {

            Console.WriteLine("Привет, мистер! Начинаем игру в наше казино!\nВведите имя:");
            string playerName = Console.ReadLine();
            Console.Write(_service.LoadData($"save_slot_{playerName}"));
        }


        public void SelectGame()
        {
        }
    }
}
