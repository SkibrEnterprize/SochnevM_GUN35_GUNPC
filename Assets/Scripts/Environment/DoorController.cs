
using UnityEngine;
using DG.Tweening;

public class SlidingDoor : MonoBehaviour
{
    [SerializeField] private Vector3 _slideOffset = new Vector3(5f, 0f, 0f);
    [SerializeField] private float _moveDuration = 1.2f;
    [SerializeField] private Collider _triggerZone;

    private bool _isOpen;
    private Vector3 _closedPosition;
    private Vector3 _openPosition;
    private Tween _currentTween;

    private void Awake()
    {
        _closedPosition = transform.position;
        _openPosition = _closedPosition + _slideOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsCharacter(other)) OpenDoor();
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsCharacter(other)) CloseDoor();
    }

    public void OpenDoor()
    {
        if (_isOpen) return;
        _isOpen = true;
        _currentTween?.Kill();

        _currentTween = transform.DOMove(_openPosition, _moveDuration)
                                .SetEase(Ease.OutCubic);
    }
    public void CloseDoor()
    {
        if (!_isOpen) return;
        _isOpen = false;

        _currentTween?.Kill();

        _currentTween = transform.DOMove(_closedPosition, _moveDuration)
                                .SetEase(Ease.InCubic);
    }

    private bool IsCharacter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Ragdoll>(out _)) return true;
        return other.gameObject.TryGetComponent<CharacterController>(out _);
    }
}