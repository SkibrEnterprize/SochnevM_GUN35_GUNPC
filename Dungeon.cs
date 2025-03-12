
namespace LearnOfClassesRPG
{
    public class Dungeon
    {
        private Room[] _rooms;

        public Room[] Rooms => _rooms;
        public Dungeon()
        {
            _rooms = new Room[]
            {
                new Room(new Unit("Varvar", 2), new Weapon("Axe", 2, 6)),
                new Room(new Unit("Berserk", 3), new Weapon("Sword", 3, 8)),
                new Room(new Unit("Witch", 5), new Weapon("Broom", 4, 9)),
                new Room(new Unit("Witcher", 5), new Weapon("Silver Sword", 5, 10)),
                new Room(new Unit("Dwarf", 5), new Weapon("Hammer", 2, 8))
            };
        }
        public void ShowRooms()
        {
            for (int i = 0; i < _rooms.Length; i++)
            {
                var room = Rooms[i];
                Console.WriteLine($"Unit of room is:   {room.Unit.Name}");
                Console.WriteLine($"Weapon of room is: {room.Weapon.Name}");
                Console.WriteLine("--");
            }
        }
    }

}
