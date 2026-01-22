// скрипт для спавна объектов из пула в случайном порядке в пределах заданной области(Rect)
// с проверкой на наличие занятого места при расстановке

using UnityEngine;
using Zenject;

public class ObjectForPooling : MonoBehaviour
{
    private ObjectPool<Prefab> _pool;

    [Header("Dirt spawn area")]
    [SerializeField] private GameObject _areaForSpawn;
    [SerializeField] private LayerMask _unitLayerMask;
    private Vector3 _areaCenter;
    private Vector3 _areaSize;
    [Header("Spawn parameters")]
    [SerializeField] private int _objectsToSpawn = 20;
    [SerializeField] private float _yOffset = 0.5f;
    [SerializeField] private float _collisionRadius = 0.6f;


    [Inject]
    public void Construct(ObjectPool<Prefab> pool)
    {
        _pool = pool;
    }
      


    private void Start()
    {
        CheckingArea();
        SpawnRandomly();
    }    

    #region Spawn
    private void CheckingArea()
    {
        if (_areaForSpawn == null)
        {
            Debug.LogError($"Cant find Area for spawn, please choose in inspector in GameController");
            return;
        }
        Bounds bounds = _areaForSpawn.GetComponent<Renderer>().bounds;

        _areaCenter = bounds.center;
        _areaSize = new Vector3(bounds.size.x, 0f, bounds.size.z);
    }

    private void SpawnRandomly()
    {
        for (int i = 0; i < _objectsToSpawn; i++)
        {
            Prefab prefab = _pool.Get();
            prefab.SetPool(_pool);

            if (!TryFindFreePosition(out Vector3 spawnPos))
            {
                Debug.LogWarning($"Cant find empty place for {prefab.name}. Object is not instantiate.");
                _pool.Return(prefab);
                continue;
            }

            prefab.transform.position = spawnPos;

            float randomYRot = UnityEngine.Random.Range(0f, 360f);
            prefab.transform.rotation = Quaternion.Euler(0f, randomYRot, 0f);
        }
    }

    private bool TryFindFreePosition(out Vector3 position)
    {

        float randX = UnityEngine.Random.Range(_areaCenter.x - _areaSize.x * 0.5f,
                                   _areaCenter.x + _areaSize.x * 0.5f);
        float randZ = UnityEngine.Random.Range(_areaCenter.z - _areaSize.z * 0.5f,
                                   _areaCenter.z + _areaSize.z * 0.5f);

        Vector3 candidatePos = new Vector3(randX, _yOffset, randZ);

        if (!Physics.CheckSphere(candidatePos, _collisionRadius, _unitLayerMask))
        {
            position = candidatePos;
            return true;
        }


        position = Vector3.zero;
        return false;
    }
    #endregion

}
