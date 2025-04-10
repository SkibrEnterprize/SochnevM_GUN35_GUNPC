namespace GameInConsole.Games
{
    public abstract class CasinoGameBase()
    {
        protected void OnWinInvoke()
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

        public Action? OnWin;
        public Action? OnLoose;
        public Action? OnDraw;

        public abstract void PrintResultsInConsole();
        public abstract void GameMechanics();
        public abstract void CheckConstructValue();

    }

}
