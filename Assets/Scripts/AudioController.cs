using UnityEngine;

namespace Netologia.Quest.Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        [Header("Tracks")]
        [SerializeField] private AudioClip _mainTheme;
        [SerializeField] private AudioClip _portalEffect;

        private void Awake()
        {
            PlayMainTheme();
        }

        public void PlayMainTheme() => Play(_mainTheme);
        public void PlayPortalEffect() => PlayPortalEffect(_portalEffect);

        private void Play(AudioClip clip)
        {
            if (clip == null) return;
            _audioSource.clip = clip;
            _audioSource.Play();
        }
        private void PlayPortalEffect(AudioClip clip)
        {
            if (clip == null) return;
            _audioSource.PlayOneShot(clip);
        }
    }
}