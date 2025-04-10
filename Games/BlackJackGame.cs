
using GameInConsole.Card;
using GameInConsole.Games;

namespace ReadAndLoadData
{
    public partial class Casino
    {
        public class BlackJackGame : CasinoGameBase
        {
            public int _numberOfCards { get; private set; }
            private Queue<Card> _deck;

            public BlackJackGame(int numberOfCards)
            {
                _numberOfCards = numberOfCards;
                FactoryMethod();
            }
            public override void PlayGame()
            {
                throw new NotImplementedException();
            }

            protected override void FactoryMethod()
            {
                throw new NotImplementedException();
            }

            public override void PrintResultsInConsole()
            {
                throw new NotImplementedException();
            }

            public override void GameMechanics()
            {
                throw new NotImplementedException();
            }

            public override void CheckConstructValue()
            {
                throw new NotImplementedException();
            }

            public Card CreateCard()
            {
                Random rnd = new Random();
                CardSuits suit = (CardSuits)rnd.Next(0,4);
                CardValues value = (CardValues)rnd.Next(0,8);
                return new Card(suit.ToString(), value.ToString());
            }
        }
    }
}
