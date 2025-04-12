
namespace GameInConsole.Utils
{
    public struct PlayerData
    {
        public readonly string Name;
        public readonly int Age;
        public int Bank { get; private set; }

        public PlayerData(string name, int age, int bank)
        {
            Name = name; 
            Age = age; 
            Bank = bank;
        }
    }
}
