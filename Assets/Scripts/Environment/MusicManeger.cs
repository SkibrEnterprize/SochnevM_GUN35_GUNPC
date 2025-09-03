using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 1.5f;
    [SerializeField] private MusicZone[] _musicZones;
    [SerializeField] private float _maxMusicVolume = 0.2f;

    private Coroutine _currentFadeRoutine;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_musicZones != null)
        {
            foreach (var zone in _musicZones)
            {
                zone.OnEntered += HandleEnter;
                zone.OnExited += HandleExit;
            }
        }
        else
        {
            Debug.Log("Music zone is not install on Inspector. Music is turn off");
        }
    }


    private void HandleEnter(MusicZone zone)
    {
        if (_audioSource.clip == zone.Track) return;

        _audioSource.clip = zone.Track;
        _audioSource.loop = zone.Loop;
        _audioSource.volume = 0f;
        _audioSource.Play();
        FadeIn();
    }

    private void HandleExit(MusicZone zone)
    {
        if (_audioSource.clip != zone.Track) return;

        FadeOut(() => _audioSource.Stop());
    }

    private void FadeIn()
    {
        if (_currentFadeRoutine != null) StopCoroutine(_currentFadeRoutine);
        _currentFadeRoutine = StartCoroutine(FadeVolume(0f, _maxMusicVolume, _fadeDuration));
    }

    private void FadeOut(System.Action onComplete)
    {
        if (_currentFadeRoutine != null) StopCoroutine(_currentFadeRoutine);
        _currentFadeRoutine = StartCoroutine(FadeVolume(_audioSource.volume, 0f, _fadeDuration,
                                                       () => { onComplete?.Invoke(); _currentFadeRoutine = null; }));
    }

    private IEnumerator FadeVolume(float from, float to, float duration, System.Action onComplete = null)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        _audioSource.volume = to;
        onComplete?.Invoke();
    }
}