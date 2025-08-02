using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolOfBall : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    // Список доступных объектов
    private GameObject[] poolObjects;
    // Максимальное количество объектов в пуле
    [SerializeField] private int maxPoolSize = 10;
    // Количество созданных объектов (для создания новых, если нужно)
    [SerializeField] private int initialPoolSize = 5;

    void Start()
    {
        poolObjects = new GameObject[maxPoolSize];
        for (int i = 0; i < maxPoolSize; i++)
        {
            // Создаем копию объекта, который будем использовать в пуле.
            GameObject pooledObject = Instantiate(_prefab);
            pooledObject.SetActive(false);  // Скрываем объект изначально
            poolObjects[i] = pooledObject;
        }
    }

    public GameObject GetObject()
    {
        foreach (GameObject obj in poolObjects)
        {
            if (!obj.activeInHierarchy) // Ищем неактивный объект
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // Если нет свободных объектов, создаем новый
        GameObject newObject = Instantiate(_prefab);
        newObject.SetActive(false);
        poolObjects[poolObjects.Length - 1] = newObject;
        return newObject;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        print("Object Is recive");
    }
}
