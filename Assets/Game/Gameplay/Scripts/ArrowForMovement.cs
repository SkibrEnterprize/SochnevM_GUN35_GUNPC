using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class ArrowForMovement : MonoBehaviour
{
    [SerializeField] private float _delaySeconds;
    private Vector3 _position;
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public async UniTask Activate(Vector3 position)
    {
        this.transform.position = position;
        gameObject.SetActive(true); 
        await UniTask.Delay(TimeSpan.FromSeconds(_delaySeconds));
        gameObject.SetActive(false);        
    }
}
