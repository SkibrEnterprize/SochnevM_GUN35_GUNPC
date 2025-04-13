namespace GameInConsole.Utils
{

    public class WrongDiceNumberException : Exception
    {
        public int Number { get; set; }

        public WrongDiceNumberException(int number, string message) : base(message)
        {
            Number = number;
        }
    }
}

