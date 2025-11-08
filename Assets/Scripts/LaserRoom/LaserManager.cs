using UnityEngine;
using System.Collections.Generic;

public sealed class LaserManager : MonoBehaviour
{
    [Header("Sources & receivers")]
    [SerializeField] private List<LaserReceiver> _receivers = new();

    private void Awake()
    {
        foreach (LaserReceiver receiver in _receivers)
        {
            receiver.OnActivated += OnActivatedHandler;
        }
    }

    private void OnActivatedHandler(LaserReceiver receiver)
    {
        receiver.OnActivated -= OnActivatedHandler;

        _receivers.Remove(receiver);

        if (_receivers.Count == 0)
        {
            Debug.Log("Все приёмники подсвечены! Задание выполнено!");
        }
    }

    [ContextMenu("Reset all")]
    public void ResetAll()
    {
        foreach (var r in _receivers) r.ResetReceiver();
    }

    private void OnDestroy()
    {
        foreach (LaserReceiver receiver in _receivers)
        {
            receiver.OnActivated -= OnActivatedHandler;
        }
    }
   
}

