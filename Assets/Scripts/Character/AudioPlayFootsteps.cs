using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class AudioPlayFootsteps : MonoBehaviour
{
    [SerializeField] private AudioClip _footstepSound;
    [SerializeField] private float _volume = 0.1f;
   
    private AudioSource _audioSrc;


    void Awake()
    {        
        _audioSrc = GetComponent<AudioSource>();
        _audioSrc.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayFootstep();
    }


    public void PlayFootstep()
    {
        if (_footstepSound == null) return;
        _audioSrc.PlayOneShot(_footstepSound, _volume);
       
    }
}
