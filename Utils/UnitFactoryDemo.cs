using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;

namespace GamePrototype.Utils
{
    public class UnitFactoryDemo
    {
        public static Unit CreatePlayer(string name, Difficulty difficulty)
        {
            if (difficulty == Difficulty.Easy)
            {
                var player = new Player(name, 30, 30, 6);
                player.AddItemToInventory(new Weapon(10, 15, "Sword"));
                player.AddItemToInventory(new RangeWeapon(5, 10, "Bow")); // Задание 2
                player.AddItemToInventory(new Armour(10, 15, "Armour"));
                player.AddItemToInventory(new HealthPotion("Potion"));
                player.AddItemToInventory(new Grindstone("Grindstone")); // Задание 1
                return player;
            }
            else
            {
                var player = new Player(name, 20, 20, 3);
                player.AddItemToInventory(new Weapon(5, 10, "Sword"));
                player.AddItemToInventory(new RangeWeapon(3, 8, "Bow"));
                player.AddItemToInventory(new Armour(5, 10, "Armour"));
                player.AddItemToInventory(new HealthPotion("Potion"));
                player.AddItemToInventory(new Grindstone("Grindstone"));
                return player;
            }
        }


        public static Unit CreateGoblinEnemy() => new Goblin(GameConstants.Goblin, 20, 20, 5);
    }
}
