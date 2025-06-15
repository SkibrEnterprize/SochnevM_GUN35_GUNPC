using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using Zenject;

public class InputManager : MonoBehaviour
{
    private Controls _controls;

    [SerializeField] private GameObject _indicatorBase;
    [SerializeField] private Image _indicator;

    private Coroutine _coroutine;

    private bool _isProgressRestarting = false;
    private float _delay = 2f;

    [Inject]
    private void Construct(Controls controls)
    {
        _controls = controls;
    }

    private void Awake()
    {
        //_controls = new Controls();
        _controls.Enable();
        _indicatorBase.SetActive(false);
        _indicator.fillAmount = 0f;
    }

    private void OnEnable()
    {
        _controls.Game.Restart.started += OnRestartPressed;
        //_controls.Game.Restart.performed += OnRestartHold;
        _controls.Game.Restart.canceled += OnRestartCancel;
    }
    private void OnDestroy()
    {
        _controls.Game.Restart.started -= OnRestartPressed;
        //_controls.Game.Restart.performed -= OnRestartHold;
        _controls.Game.Restart.canceled -= OnRestartCancel;
    }
    private void OnDisable()
    {
        _controls.Game.Restart.started -= OnRestartPressed;
        //_controls.Game.Restart.performed -= OnRestartHold;
        _controls.Game.Restart.canceled -= OnRestartCancel;
    }

    private void OnRestartCancel(InputAction.CallbackContext context)
    {
        _isProgressRestarting = false;
        _indicatorBase.SetActive(false);
    }

    private void OnRestartPressed(InputAction.CallbackContext context)
    {
        _isProgressRestarting = true;
        Debug.Log("Press Space!!!");
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(FillAmount());

    }

    private IEnumerator FillAmount()
    {
        _indicatorBase.SetActive(true);
        var time = 0f;
        while (time < _delay && _isProgressRestarting)
        {
            var percent = time / _delay;
            _indicator.fillAmount = percent;
            time += Time.deltaTime;
            yield return null;
        }
        ReloadScene();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

}
