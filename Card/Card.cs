namespace GameInConsole.Card
{
    public struct Card
    {
        public readonly string Suits;
        public readonly string Values;

        public Card(string suits, string values)
        {
            Suits = suits;
            Values = values;
        }
    }

}
