using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    private SignalBus _signalBus;
    private TextView _uiController;
    private int _totalDirtCollect;
    private ObjectPool<Dirt> _poolDirt;

    [Header("Dirt spawn area")]
    [SerializeField] private GameObject _areaForSpawn;
    private Vector3 _areaCenter;
    private Vector3 _areaSize;
    [Header("Spawn parameters")]
    [SerializeField] private int _objectsToSpawn = 20;
    [SerializeField] private float _yOffset = 0.5f;
    [SerializeField] private float _collisionRadius = 0.6f;


    [Inject]
    public void Construct(SignalBus signalBus, TextView uiController, ObjectPool<Dirt> poolDirt)
    {
        _signalBus = signalBus;
        _uiController = uiController;
        _poolDirt = poolDirt;
    }

    private void Start()
    {
        CheckingArea();
        SpawnRandomly();
    }


    private void OnEnable()
    {
        _signalBus.Subscribe<DirtCollected>(AddScore);
    }

    private void AddScore()
    {
        _totalDirtCollect++;
        _uiController.UpdateTotalDirtCollect(_totalDirtCollect.ToString());
    }
    private void OnDisable()
    {
        _signalBus.Unsubscribe<DirtCollected>(AddScore);
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
            Dirt dirt = _poolDirt.Get();
            dirt.SetPool(_poolDirt, _signalBus);

            if (!TryFindFreePosition(out Vector3 spawnPos))
            {
                Debug.LogWarning($"Cant find empty place for {dirt.name}. Object is not instantiate.");
                _poolDirt.Return(dirt);
                continue;
            }

            dirt.transform.position = spawnPos;

            float randomYRot = UnityEngine.Random.Range(0f, 360f);
            dirt.transform.rotation = Quaternion.Euler(0f, randomYRot, 0f);
        }
    }

    private bool TryFindFreePosition(out Vector3 position)
    {

        float randX = UnityEngine.Random.Range(_areaCenter.x - _areaSize.x * 0.5f,
                                   _areaCenter.x + _areaSize.x * 0.5f);
        float randZ = UnityEngine.Random.Range(_areaCenter.z - _areaSize.z * 0.5f,
                                   _areaCenter.z + _areaSize.z * 0.5f);

        Vector3 candidatePos = new Vector3(randX, _yOffset, randZ);

        if (!Physics.CheckSphere(candidatePos, _collisionRadius))
        {
            position = candidatePos;
            return true;
        }


        position = Vector3.zero;
        return false;
    }
    #endregion

}
