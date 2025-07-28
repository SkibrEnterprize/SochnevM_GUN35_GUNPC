using UnityEngine;
using Zenject;

public class GateMover : MonoBehaviour
{
    [SerializeField] private float moveDistance = 5f;
    [SerializeField] private float moveSpeed = 2f; 
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingToTarget;
    private SignalBus _signalBus;

    [Inject]
    private void Construct(
        SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    internal void SetActive(bool v)
    {
        enabled = true;
    }
    void OnEnable()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(0, 0, -moveDistance);
        movingToTarget = true;
}
        void Update()
    {
        if (movingToTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                movingToTarget = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

            if (transform.position == startPosition)
            {
                this.enabled = false;
                _signalBus.Fire<EndAction>();
            }
        }
    }
}

