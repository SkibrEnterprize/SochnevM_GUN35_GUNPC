namespace ReadAndLoadData
{
    public partial class Casino
    {
        public class WrongDiceNumberException : Exception
        {
            public WrongDiceNumberException(string message) : base(message) { }
        }
    }
}
