namespace GameInConsole.Games
{
    public abstract class CasinoGameBase()
    {
        public event Action OnWin;
        public event Action OnLoose;
        public event Action OnDraw;
                
        protected virtual void OnWinInvoke()
        {   
            OnWin?.Invoke();
        }
        protected void OnLooseInvoke()
        {
            OnLoose?.Invoke();
        }
        protected void OnDrawInvoke()
        {
            OnDraw?.Invoke();
        }
        protected abstract void FactoryMethod();
        public abstract void PlayGame();

        public abstract void PrintResultsInConsole();   

    }

}
