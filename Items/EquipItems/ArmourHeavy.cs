using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public sealed class ArmourHeavy : EquipItem // Задание 2
    {
        public ArmourHeavy(uint defence, uint durability, string name) : base(durability, name) => Defence = defence;

        public uint Defence { get; }

        public override EquipSlot Slot => EquipSlot.Armour;
        
    }
}
