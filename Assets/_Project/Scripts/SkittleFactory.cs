using UnityEngine;


    public sealed class SkittleFactory
    {
        private readonly Skittle _skittlePrefab;

        public SkittleFactory(Skittle skittlePrefab)
        {
            _skittlePrefab = skittlePrefab;
        }

        public Skittle SpawnSkittle(Vector3 position)
        {
            return Object.Instantiate(_skittlePrefab, position, Quaternion.identity);
        }
    }