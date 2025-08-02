using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _timeToLive = 1f;
    private Coroutine _coroutine;

    private ObjectPoolOfBall _ballPool;

    [Inject]
    public void Construct(ObjectPoolOfBall ballPool)
    {
        _ballPool = ballPool;
    }

    //void OnEnable()
    //{

    //    StartCoroutine(BallCycleOfLifeRoutine());
    //}
    ////    //    //if (_coroutine != null)
    ////    //    //{
    ////    //    //    StopCoroutine(_coroutine);
    ////    //    //}
    ////    //    //else
    ////    //    //{
    ////    //    //    _coroutine = StartCoroutine(BallCycleOfLifeRoutine());
    ////    //    //}
    ////}
    //IEnumerator BallCycleOfLifeRoutine()
    //{
    //    yield return new WaitForSeconds(_timeToLive);
    //    _ballPool.ReturnObject(_ball);
    //}
}

