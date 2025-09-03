using UnityEngine;
using UnityEngine.AI;

public class BotMover : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform _target; // куда идти

    private NavMeshAgent _agent;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        if (_target != null)
            MoveTo(_target.position);
    }

    public void MoveTo(Vector3 destination)
    {
        if (!_agent.enabled) return;
        _agent.SetDestination(destination);
    }

    // Для динамической смены цели
    public void SetNewTarget(Transform newTarget)
    {
        _target = newTarget;
        MoveTo(_target.position);
    }
}