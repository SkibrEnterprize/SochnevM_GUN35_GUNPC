using UnityEngine;
using Zenject;

public sealed class SkittleSpawn : IInitializable
{
    private readonly SkittleFactory _skittleFactory;
    private readonly Transform _spawnPoint;

    private readonly int _rows;
    private readonly float _spacingOfRows;

    private int _skittleTotal;

    public int SkittleTotal => _skittleTotal;

    public SkittleSpawn(SkittleFactory skittleFactory, SkittleConfig config)
    {
        _skittleFactory = skittleFactory;

        _spacingOfRows = config.SpacingOfRows;
        _spawnPoint = config.SpawnPoint;
        _rows = config.Rows;
    }

    void IInitializable.Initialize()
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
                var position = new Vector3(
                    pin * _spacingOfRows - row * _spacingOfRows / 2,
                    0,
                    row * _spacingOfRows
                );

                _skittleFactory.SpawnSkittle(_spawnPoint.position + position);

                _skittleTotal++;
            }
        }
    }
}

