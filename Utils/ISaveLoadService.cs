namespace GameInConsole.Utils
{

    public interface ISaveLoadService
    {
        public void SaveData(PlayerProfile data, string id);
        public bool TryToLoadData(string id, out PlayerProfile data);
    }

}
