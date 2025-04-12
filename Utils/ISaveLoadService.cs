namespace ReadAndLoadData
{
           
        public interface ISaveLoadService<T>
        {
            public void SaveData(T data, string id);
            public T LoadData(string id);
        }
    
}
