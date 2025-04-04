namespace ReadAndLoadData
{
    public partial class Casino
    {        
        public interface ISaveLoadService<T>
        {
            public void SaveData(T data, string id);
            public T LoadData(string id);
        }
    }
}
