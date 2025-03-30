using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public sealed class Armour : EquipItem
    {
        public Armour(uint defence, uint durability, string name) : base(durability, name) => Defence = defence;

        public uint Defence { get; private set; }

        public override EquipSlot Slot => EquipSlot.Armour;

        public void DowngradeDefence(uint downgradeNumber) // Задание 1 метод для снижения защиты
        {
            Defence -= downgradeNumber;
        }
    }
}
