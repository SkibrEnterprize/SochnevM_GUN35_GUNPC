
namespace ReadAndLoadData
{
    public class PlayerProfile
    {
        public string Name { get; set; }
        public int Money { get; set; }
        public int TotalWin { get; set; }
        public int TotalLose { get; set; }
        public int TotalDraw { get; set; }
        public PlayerProfile(string name = "Player")
        {
            Name = name;
            Money = 100;
        }

        public override string ToString()
        {
            return $"Name:{Name},Money:{Money},TotalWin:{TotalWin},TotalLose:{TotalLose},TotalDraw:{TotalDraw}";
        }
    }
}
