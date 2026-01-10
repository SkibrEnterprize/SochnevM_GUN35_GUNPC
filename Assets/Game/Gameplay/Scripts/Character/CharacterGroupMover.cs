using UnityEngine;
using UnityEngine.AI;
using SampleProject;

public class CharacterGroupMover : MonoBehaviour
{
    [SerializeField] private bool _arrivedFirst = false;
    [SerializeField] private bool _stoppedBySignal = false;
    [SerializeField] private float _signalRadius = 2f;
    [SerializeField] private bool _drawDebugLines = false;
    [SerializeField] private LayerMask _unitMask;   // оптимизация для снижения нагрузки при поиске

    public bool ArrivedFirst => _arrivedFirst;
    public bool StoppedBySignal => _stoppedBySignal;

    private NavMeshAgent _agent;
    private CommandController _commandController;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _commandController = GetComponent<CommandController>();
    }

    void Update()
    {
        if (!_agent.pathPending) { return; }
        if (!_arrivedFirst && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            Debug.Log("Arrived first");
            _arrivedFirst = true;
            _commandController.Stop();
        }

        if (!_arrivedFirst)
        {
            // Проверяем близость к 'стопнутым' юнитам
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _signalRadius, _unitMask);
            foreach (var hit in hitColliders)
            {
                CharacterGroupMover other = hit.GetComponent<CharacterGroupMover>();
                if (other != null && (other.ArrivedFirst || other.StoppedBySignal))
                {
                    Debug.Log("Arrived second");
                    _stoppedBySignal = true;
                    _commandController.Stop();
                    break;
                }
            }
        }
    }

    private void OnDisable()
    {
        _arrivedFirst = false;
        _stoppedBySignal = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 1f);
        if (_drawDebugLines) Gizmos.DrawWireSphere(transform.position, _signalRadius);
    }
}


