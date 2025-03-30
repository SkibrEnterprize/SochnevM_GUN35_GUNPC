using GamePrototype.Dungeon;
using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;
using System.Text;

namespace GamePrototype.Units
{
    public sealed class Player : Unit
    {
        private readonly Dictionary<EquipSlot, EquipItem> _equipment = new();

        public Player(string name, uint health, uint maxHealth, uint baseDamage) : base(name, health, maxHealth, baseDamage)
        {
        }

        public override uint GetUnitDamage()
        {
            if (_equipment.TryGetValue(EquipSlot.Weapon, out var item) && item is Weapon weapon)
            {
                return BaseDamage + weapon.Damage;
            }
            return BaseDamage;
        }

        public override void HandleCombatComplete()
        {
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] is EconomicItem economicItem)
                {
                    UseEconomicItem(economicItem);
                    Inventory.TryRemove(items[i]);
                }
            }
        }

        public override void AddItemToInventory(Item item) // Задание 2 
        {
            if (item is EquipItem equipItem)
            {
                if (!_equipment.ContainsKey(equipItem.Slot))
                {
                    _equipment.TryAdd(equipItem.Slot, equipItem);// Item was equipped
                }
                else if (_equipment.ContainsKey(equipItem.Slot))
                {
                    Console.WriteLine($"You Are Collect new Item \"{equipItem.Name}\", but your current Equip Item is \"{_equipment[equipItem.Slot].Name}\"");
                    Console.WriteLine($"Do you want to apply new Item?");
                    ChangingInSlot(equipItem);
                }
                return;
            }

            base.AddItemToInventory(item);
        }

        private void UseEconomicItem(EconomicItem economicItem)
        {
            if (economicItem is HealthPotion healthPotion)
            {
                Health += healthPotion.HealthRestore;
            }
        }

        protected override uint CalculateAppliedDamage(uint damage)
        {
            if (_equipment.TryGetValue(EquipSlot.Armour, out var item) && item is Armour armour)
            {
                //Console.WriteLine($"Armor Defence Before Damage is -{armour.Defence}-");
                damage -= (uint)(damage * (armour.Defence / 100f));

                // Задание 1
                armour.DowngradeDefence(1);
                //Console.WriteLine($"Defence Defence After Damage is -{armour.Defence}-");
            }
            return damage;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(Name);
            builder.AppendLine($"Health {Health}/{MaxHealth}");
            builder.AppendLine("Loot:");
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++)
            {
                builder.AppendLine($"[{items[i].Name}] : {items[i].Amount}");
            }
            return builder.ToString();
        }

        public void ChangingInSlot(EquipItem equipItem) // Задание 2 метод для запроса изменения и изменения экипировки
        {
            while (true)
            {
                Console.WriteLine($"Write:\n1 for YES\n2 for NO");
                if (int.TryParse(Console.ReadLine(), out int option) && option == 1)
                {
                    _equipment[equipItem.Slot] = equipItem;
                    Console.WriteLine($"Yuor \"{equipItem.Slot.ToString()}\" was changed to \"{equipItem.Name}\"");
                    break;
                }
                else if (option == 2)
                {
                    break;
                }
                Console.WriteLine("Wrong input!");
            }
        }

        //public void PrintEquipment(Dictionary<EquipSlot, EquipItem> equipment) // метод для вывода содержания экипировки
        //{            
        //    foreach (KeyValuePair<EquipSlot, EquipItem> pair in equipment)
        //    {
        //        Console.WriteLine($"In slot\t{pair.Key} eqiupment is\t{pair.Value.Name}");
        //    }
        //}
    }
}
