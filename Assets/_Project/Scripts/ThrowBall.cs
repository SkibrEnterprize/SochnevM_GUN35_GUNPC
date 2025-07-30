using UnityEngine;
using Zenject;

public class ThrowBall : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private GameObject _ballVisualize;
    [SerializeField] private float _launchForce;

    private ArrowController _arrow;
    private Controls _controls;
    private SignalBus _signalBus;

    [Inject]
    public void Construct(Controls controls, SignalBus signalBus)
    {
        _controls = controls;
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _arrow = GetComponentInChildren<ArrowController>();
        if (_arrow == null) print("Arrow is not finding!");
    }
    private void OnEnable()
    {
        _controls.Player.Fire.performed += context => ThrowingBall();
        _signalBus.Subscribe<InAction>(BallVisualizeActive);
        _signalBus.Subscribe<EndAction>(BallVisualizeDeactive);
    }

    private void BallVisualizeActive() => _ballVisualize.SetActive(false);
    private void BallVisualizeDeactive() => _ballVisualize.SetActive(true);


    private void OnDestroy()
    {
        _controls.Player.Fire.performed -= context => ThrowingBall();
        _signalBus.Unsubscribe<InAction>(BallVisualizeActive);
        _signalBus.Unsubscribe<EndAction>(BallVisualizeDeactive);
    }
    private void ThrowingBall()
    {
        GameObject ball = Instantiate(_ballPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        Vector3 direction = _arrow.GetDirection();
        rb.AddForce(direction * _launchForce);
        _signalBus.Fire<InAction>();
    }
}
