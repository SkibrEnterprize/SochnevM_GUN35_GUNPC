using System.Collections;
using UnityEngine;
using Zenject;

public class ThrowBall : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private GameObject _ballVisualize;
    [SerializeField] private float _launchForce;
    [SerializeField] private float _timeToLive = 20f;

    private ArrowController _arrow;
    private Controls _controls;
    private SignalBus _signalBus;
    private ObjectPoolOfBall _ballPool;
    private GameObject _ball;

    [Inject]
    public void Construct(Controls controls, SignalBus signalBus, ObjectPoolOfBall ballPool)
    {
        _controls = controls;
        _signalBus = signalBus;
        _ballPool = ballPool;
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
    private void OnDisable()
    {
        _controls.Player.Fire.performed -= context => ThrowingBall();
        _signalBus.Unsubscribe<InAction>(BallVisualizeActive);
        _signalBus.Unsubscribe<EndAction>(BallVisualizeDeactive);
    }
      
    private void Fire()
    {
        _signalBus.Fire<InAction>();
    }
    private void ThrowingBall()
    {
        if (this == null) return;
        //GameObject ball = Instantiate(_ballPrefab, transform.position, Quaternion.identity);
         _ball = _ballPool.GetObject();
        _ball.transform.position = transform.position;
        Rigidbody rb = _ball.GetComponent<Rigidbody>();
        Vector3 direction = _arrow.GetDirection();
        rb.AddForce(direction * _launchForce);
        _signalBus.Fire<InAction>();
        StartCoroutine(ReturnBall());
    }   

    public IEnumerator ReturnBall()
    {
        yield return new WaitForSeconds(_timeToLive);
        _ballPool.ReturnObject(_ball);
    } 
}
