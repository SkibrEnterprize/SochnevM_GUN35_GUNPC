using Netologia.Quest.Audio;
using Netologia.Quest.Characters.Player;
using System.Collections;
using UnityEngine;
using Zenject;
public class CharacterToggle : MonoBehaviour
{

    [SerializeField] private GameObject _mainModel;
    [SerializeField] private GameObject _secondModel;
    [SerializeField] private GameObject _flashOfToggleHolder;
    [SerializeField] private float _flashDuration = 0.2f;

    private bool _isFirstActive = true;
    private Light _flashOfToggle;
    private Coroutine _currentFlashCoroutine;
    private AudioController _audioController;

    [Inject]
    private void Construct(AudioController audioController)
    {
         _audioController = audioController;
    }


    private void Awake()
    {
        if (_mainModel == null || _secondModel == null)
        {
            Debug.LogError($"{nameof(CharacterToggle)}: дочерние объекты не назначены в инспекторе.");
            enabled = false;
            return;

        }

        if (_flashOfToggleHolder != null)
            _flashOfToggle = _flashOfToggleHolder.GetComponent<Light>();

        _mainModel.SetActive(_isFirstActive);
        _secondModel.SetActive(!_isFirstActive);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Trigger!!!");
        if (!other.TryGetComponent<PlayerController>(out _))
            return;

        Toggle();
        _audioController.PlayPortalEffect();
    }

    private void Toggle()
    {
        _isFirstActive = !_isFirstActive;
        _mainModel.SetActive(_isFirstActive);
        _secondModel.SetActive(!_isFirstActive);
        FlashLight();
    }

    private void FlashLight()
    {
        if (_flashOfToggle == null)
            return;

        if (_currentFlashCoroutine != null)
            StopCoroutine(_currentFlashCoroutine);

        _currentFlashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        _flashOfToggle.enabled = true;
        yield return new WaitForSeconds(_flashDuration);
        _flashOfToggle.enabled = false;

        _currentFlashCoroutine = null;
    }
}