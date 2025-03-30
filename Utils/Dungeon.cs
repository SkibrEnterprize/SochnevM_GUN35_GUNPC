using GamePrototype.Dungeon;

namespace GamePrototype.Utils
{
    public abstract class Dungeon
    {

        public string Name { get; protected set; }
        public Difficulty Level { get; protected set; }

        public Dungeon(string name, Difficulty level)
        {
            Name = name;
            Level = level;
        }

        // Метод для добавления комнаты в подземелье (абстрактный, должен быть реализован наследниками)
        public abstract DungeonRoom BuildDungeon();

        //    public static DungeonRoom BuildDungeon()
        //    {
        //        var enter = new DungeonRoom("Enter");
        //        var monsterRoom = new DungeonRoom("Monster", UnitFactoryDemo.CreateGoblinEnemy());
        //        var emptyRoom = new DungeonRoom("Empty", new ArmourHeavy(20, 30, "ArmourHeavy"));
        //        var lootRoom = new DungeonRoom("Loot1", new Gold());
        //        var lootStoneRoom = new DungeonRoom("Loot1", new Grindstone("Stone"));
        //        var finalRoom = new DungeonRoom("Final", new Grindstone("Stone1"));

        //        enter.TrySetDirection(Direction.Right, monsterRoom);
        //        enter.TrySetDirection(Direction.Left, emptyRoom);

        //        monsterRoom.TrySetDirection(Direction.Forward, lootRoom);
        //        monsterRoom.TrySetDirection(Direction.Left, emptyRoom);

        //        emptyRoom.TrySetDirection(Direction.Forward, lootStoneRoom);

        //        lootRoom.TrySetDirection(Direction.Forward, finalRoom);
        //        lootStoneRoom.TrySetDirection(Direction.Forward, finalRoom);

        //        return enter;
        //    }
        //}
    }

}
