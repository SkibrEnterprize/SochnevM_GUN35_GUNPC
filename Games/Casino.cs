
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
            _service.LoadData(Console.ReadLine());            
            Console.ReadKey();

        }


        public void SelectGame()
        {
        }
    }
}
