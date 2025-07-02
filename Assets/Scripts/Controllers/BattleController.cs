using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using Zenject;
public class BattleController : MonoBehaviour
{
    private Controls _controls;

    [SerializeField] private GameObject _indicatorBase;
    [SerializeField] private Image _indicator;

    private SignalBus _signalBus;

    private Coroutine _coroutine;

    private bool _isProgressRestarting = false;
    private float _delay = 2f;

    [Inject]
    private void Construct(
        Controls controls,
        SignalBus signalBus)
    {
        _controls = controls;
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _indicatorBase.SetActive(false);
        _indicator.fillAmount = 0f;
    }

    private void OnEnable()
    {
        _controls.Game.Confirm.performed += OnConfirmPressed;
        _controls.Game.Cancel.performed += OnCancelPressed;
        _controls.Game.Restart.started += OnRestartPressed;
        _controls.Game.Restart.canceled += OnRestartCancel;
    }


    private void OnDisable()
    {
        _controls.Game.Confirm.performed -= OnConfirmPressed;
        _controls.Game.Cancel.performed -= OnCancelPressed;
        _controls.Game.Restart.started -= OnRestartPressed;
        _controls.Game.Restart.canceled -= OnRestartCancel;
    }

    private void OnConfirmPressed(InputAction.CallbackContext context)
    {
        _signalBus.Fire<SelectConfirm>();
    }

    private void OnCancelPressed(InputAction.CallbackContext context)
    {
        _signalBus.Fire<SelectCancel>();
    }
    private void OnRestartPressed(InputAction.CallbackContext context)
    {
        _isProgressRestarting = true;
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(FillAmount());
    }
    private void OnRestartCancel(InputAction.CallbackContext context)
    {
        _isProgressRestarting = false;
        _indicatorBase.SetActive(false);
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
            Debug.Log(_indicator.fillAmount);
            if (_indicator.fillAmount > 0.95) ReloadScene();
        }
    }
    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
