using DG.Tweening;
using Netologia.Quest.Characters.Player;
using System;
using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]   

public sealed class LaserMirror : MonoBehaviour, ITrigger
{
    public event Action<ITrigger> OnEnter;
        
    private TriggerObserver m_triggerObserver;

    [Header("Rotation settings")]
    [SerializeField]
    private float _rotateAngleDeg = 90f;
    [SerializeField]
    private float _duration = 0.4f;

    [SerializeField,
     Tooltip("Тип easing (ускорение/замедление)")]
    private Ease _easeType = Ease.OutCubic;

    public Transform Transform => transform;

    private void Awake()
    {
        m_triggerObserver = GetComponent<TriggerObserver>();
        m_triggerObserver.OnEnter += OnEnterHandler;
    }

    private void OnDestroy()
    {
        m_triggerObserver.OnEnter -= OnEnterHandler;
    }

    private void OnEnterHandler()
    {
        OnEnter?.Invoke(this);
    }

    public void RotateMirror()
    {
        Debug.Log("RotateMirror!");
        transform.DOKill();

        transform.DORotate(new Vector3(0f, _rotateAngleDeg, 0f), _duration,
                           RotateMode.LocalAxisAdd)
                  .SetEase(_easeType);
    }

    // private void OnMouseDown()
    // {
    //     RotateMirror();
    // }

    public void Interact()
    {
        RotateMirror();
    }

    //[Header("Rotation settings")]
    //[SerializeField]
    //private float _rotateAngleDeg = 90f;
    //[SerializeField]
    //private float _duration = 0.4f;

    //[SerializeField,
    // Tooltip("Тип easing (ускорение/замедление)")]
    //private Ease _easeType = Ease.OutCubic;   

    //public void RotateMirror()
    //{
    //    transform.DOKill();

    //    transform.DORotate(new Vector3(0f, _rotateAngleDeg, 0f), _duration,
    //                       RotateMode.LocalAxisAdd)
    //              .SetEase(_easeType);
    //}
    //private void OnMouseDown()
    //{
    //    RotateMirror();
    //}
}