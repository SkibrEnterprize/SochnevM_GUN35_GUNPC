namespace GamePrototype.Items.EconomicItems
{
    public sealed class Grindstone : EconomicItem
    {
        public uint DurabilityRestore => 5;
        public override bool Stackable => false;

        public Grindstone(string name) : base(name)
        {
        }    
    }
}
