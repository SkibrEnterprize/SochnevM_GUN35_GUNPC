using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class MusicZone : MonoBehaviour
{
    public AudioClip Track;
    public bool Loop = true;  

    public event Action<MusicZone> OnEntered;
    public event Action<MusicZone> OnExited;

    private void Awake()
    {
        var col = GetComponent<Collider>();
        if (!col.isTrigger) Debug.LogWarning($"{name} – Collider is not turn as Trigger");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsCharacter(other))
        {
            OnEntered?.Invoke(this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (IsCharacter(other))
        {
            OnExited?.Invoke(this);
        }
    }
    private bool IsCharacter(Collider other)
    {
        return other.gameObject.TryGetComponent<CharacterController>(out _);
    }
}