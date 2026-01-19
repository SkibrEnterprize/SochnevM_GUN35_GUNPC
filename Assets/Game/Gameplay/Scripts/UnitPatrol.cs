using SampleProject;
using System.Linq;
using UnityEngine;

public class UnitPatrol : MonoBehaviour
{

    [SerializeField] private Transform _parentForPoint;
    [SerializeField] private Transform[] _points;

    private CommandController _commandController;

    private void Awake()
    {
        _commandController = GetComponent<CommandController>();

        _points = _parentForPoint.transform.GetComponentsInChildren<Transform>(true)
                    .Where(t => t != _parentForPoint.transform)   // убрать сам объект
                    .ToArray();

        if (_points != null) _commandController.Patrol(_points);
    }
}
