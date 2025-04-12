using System.Diagnostics.CodeAnalysis;

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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Card otherCard = (Card)obj;
            return Suits == otherCard.Suits && Values == otherCard.Values;
        }

        
    }

}
