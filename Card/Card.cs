

namespace GameInConsole.Card
{
    public struct Card
    {
        public readonly CardSuits Suits;
        public readonly CardValues Values;

        public Card(CardSuits suits, CardValues values)
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
