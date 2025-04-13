namespace GameInConsole.Utils
{

    public class WrongDiceNumberException : Exception
    {
        public WrongDiceNumberException(string message) : base(message) { }
    }
}

