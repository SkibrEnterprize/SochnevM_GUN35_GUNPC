using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioPlayShoot : MonoBehaviour
{
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private float _volume = 0.1f;

    private AudioSource _audioSrc;


    void Awake()
    {
        _audioSrc = GetComponent<AudioSource>();
        _audioSrc.playOnAwake = false;
    }

    public void PlayShootSound()
    {
        if (_shootSound == null) return;
        _audioSrc.PlayOneShot(_shootSound, _volume);
    }
}

