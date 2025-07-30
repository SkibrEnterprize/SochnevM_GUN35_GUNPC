using UnityEngine;

public class SkittleSpawn : MonoBehaviour
{

    [SerializeField] private GameObject _skittlePrefab; 
    [SerializeField] private Transform _spawnPoint;
    [SerializeField, Range(1, 6)] private int _rows = 3;
    [SerializeField, Range(1, 3)] private float _spacingOfRows = 2.5f;
    private int _skittleTotal;
    public int SkittleTotal => _skittleTotal;

    void Awake()
    {
        if (_spawnPoint == null)
        {
            Debug.LogError("Spawn Point is not assigned! Please drag the Spawn Point object in the inspector.");
            return;
        }
        SpawnSkittles();
    }

    void SpawnSkittles()
    {
        for (int row = 0; row < _rows; row++)
        {
            for (int pin = 0; pin <= row; pin++)
            {
                Vector3 position = new Vector3(
                    pin * _spacingOfRows - row * _spacingOfRows / 2,
                    0,
                    row * _spacingOfRows
                );
                Instantiate(_skittlePrefab, _spawnPoint.position + position, Quaternion.identity);
                _skittleTotal++;
            }
        }
    }
}

