using System.Collections.Generic;
using UnityEngine;
public class ObjectPool<T> where T : MonoBehaviour
{
    private List<T> _pool;
    private T _prefab;
    private Transform _parent;
    public ObjectPool(T prefab, int initialSize, Transform parent)
    {
        _prefab = prefab;
        _pool = new List<T>();
        _parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            T obj = Object.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(false);
            _pool.Add(obj);
        }
    }
    public T Get()
    {
        foreach (var obj in _pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        T newObj = Object.Instantiate(_prefab);
        _pool.Add(newObj);
        return newObj;
    }
    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
    }
}
