using UnityEngine;
using System.Collections.Generic;

public sealed class LaserManager : MonoBehaviour
{
    [Header("Sources & receivers")]
    //[SerializeField] private List<LaserSource> _sources = new();
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
        //foreach (var s in _sources) s.Emit();
        foreach (var r in _receivers) r.ResetReceiver();
    }

    private void OnDestroy()
    {
        foreach (LaserReceiver receiver in _receivers)
        {
            receiver.OnActivated -= OnActivatedHandler;
        }
    }

    //public static LaserManager Instance { get; private set; }

    //[Header("Sources & receivers")]
    //[SerializeField] private List<LaserSource> _sources = new();
    //[SerializeField] private List<LaserReceiver> _receivers = new();

    //private void Awake()
    //{
    //    if (Instance != null) Destroy(gameObject);
    //    else
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}


    //public void NotifyReceiverActivated(LaserReceiver receiver)
    //{
    //    if (AllReceiversActive())
    //        Debug.Log("Все приёмники подсвечены! Задание выполнено!");
    //}

    //private bool AllReceiversActive()
    //{
    //    foreach (var r in _receivers)
    //        if (!r.IsActive) return false;
    //    return true;
    //}

    //[ContextMenu("Reset all")]
    //public void ResetAll()
    //{
    //    foreach (var s in _sources) s.Emit();    
    //    foreach (var r in _receivers) r.ResetReceiver();
    //}
}

