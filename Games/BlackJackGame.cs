
using GameInConsole.Card;
using GameInConsole.Games;
using System.Data;

namespace ReadAndLoadData
{
    public class BlackJackGame : CasinoGameBase
    {
        private Queue<Card> _deck;
        private int _playerCardsSum;
        private int _computerCardsSum;
        public int _numberOfCards { get; private set; }



        public BlackJackGame(int numberOfCards)
        {
            _numberOfCards = numberOfCards;
            _deck = new Queue<Card>();
            FactoryMethod();
        }
        public override void PlayGame()
        {
            GameMechanics();
        }

        protected override void FactoryMethod()
        {
            while (_deck.Count != _numberOfCards)
            {

                Card newCard = CreateCard();
                if (_deck.Count == 0)
                {
                    _deck.Enqueue(newCard);
                }
                else if (!IsCardExist(newCard))
                {
                    _deck.Enqueue(newCard);
                }
            }
        }

        private bool IsCardExist(Card newCard)
        {
            foreach (Card card in _deck)
            {
                if (card.Equals(newCard))
                {
                    return true;
                }
            }
            return false;
        }

        private void GameMechanics()
        {
            Shuffle();
            Card playerCard1 = _deck.Dequeue();
            Card playerCard2 = _deck.Dequeue();

            _playerCardsSum = (int)playerCard1.Values + (int)playerCard2.Values;

            Card computerCard1 = _deck.Dequeue();
            Card computerCard2 = _deck.Dequeue();

            _computerCardsSum = (int)computerCard1.Values + (int)computerCard2.Values;

            Console.WriteLine($"Вы получили: {playerCard1.Values} и {playerCard2.Values}");
            Console.WriteLine($"Оппонент получает: {computerCard1.Values} и {computerCard2.Values}");

            if (IsPointsEquals(_playerCardsSum, _computerCardsSum))
            {
                while (IsPointsEquals(_playerCardsSum, _computerCardsSum))
                {
                    Console.WriteLine($"У Вас с оппонентом одинаково по {_playerCardsSum} очков. Раздается еще по карте!");
                    Card playerCard3 = _deck.Dequeue();
                    _playerCardsSum += (int)playerCard3.Values;
                    Card computerCard3 = _deck.Dequeue();
                    _computerCardsSum += (int)computerCard3.Values;
                    Console.WriteLine($"Вы получили дополнительную карту: {playerCard3.Values}");
                    Console.WriteLine($"Оппонент получает дополнительную карту: {computerCard1.Values}");

                }
                //return;
            }
            if (_playerCardsSum == 21 && _computerCardsSum == 21 || _playerCardsSum > 21 && _computerCardsSum > 21)
            {
                Console.WriteLine("У Вас и оппонента 21 очко или более, ничья!!!");
                OnDrawInvoke();
                return;
            }

            if (_playerCardsSum > 21)
            {
                Console.WriteLine("Вы проиграли!");
                OnLooseInvoke();
                return;
            }

            if (_computerCardsSum > 21)
            {
                Console.WriteLine("Оппонент проиграл, Вы выиграли!");
                OnWinInvoke();
                return;
            }


            if (_playerCardsSum == 21 && _computerCardsSum != 21)
            {
                Console.WriteLine("У Вас Blackjack! Вы выиграли!");
                OnWinInvoke();
                return;
            }

            if (_computerCardsSum == 21 && _playerCardsSum != 21)
            {
                Console.WriteLine("У Оппонента Blackjack! Вы проиграли!");
                OnLooseInvoke();
                return;
            }

            if (_playerCardsSum > _computerCardsSum)
            {
                Console.WriteLine("У Вас больше очков - Вы выиграли!");
                OnWinInvoke();
            }
            else
            {
                Console.WriteLine("У оппонента больше очков - Вы проиграли!");
                OnLooseInvoke();
            }
        }

        private bool IsPointsEquals(int playerSum, int computerSum)
        {
            return playerSum == computerSum && playerSum < 21 && computerSum < 21;
        }        

        public Card CreateCard()
        {
            Random rnd = new Random();
            CardSuits suit = (CardSuits)rnd.Next((int)CardSuits.Hearts, (int)CardSuits.Spades + 1);
            CardValues value = (CardValues)rnd.Next((int)CardValues.Jack, (int)CardValues.Ace + 1);
            return new Card(suit, value);
        }

        public void Shuffle()
        {
            List<Card> cardsList = _deck.ToList();
            Random rnd = new Random();
            cardsList = cardsList.OrderBy(x => rnd.Next()).ToList();
            _deck.Clear();
            foreach (var card in cardsList)
            {
                _deck.Enqueue(card);
            }
            _deck = new Queue<Card>(cardsList);
        }
        public override void PrintResultsInConsole()
        {
            Console.WriteLine($"Итого у Вас было {_playerCardsSum} очков\nУ оппонента было {_computerCardsSum} очков");
        }
    }
}

