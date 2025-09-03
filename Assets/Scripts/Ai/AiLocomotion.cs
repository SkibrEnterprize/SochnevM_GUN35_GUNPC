using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]

public class AiLocomotion : MonoBehaviour
{
    [SerializeField] private AudioClip _footstepSound;
    [SerializeField] private float _volume = 0.1f;
    [SerializeField] private float _intervalWalk = 0.5f;
    [SerializeField] private float _intervalRun = 0.3f;
    NavMeshAgent agent;
    Animator animator;

    private AudioSource _audioSrc;
    private float _timeToNextStep;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        _audioSrc = GetComponent<AudioSource>();
        _audioSrc.playOnAwake = false;
    }


    void Update()
    {
        float speed = agent.hasPath ? agent.velocity.magnitude : 0f;
        animator.SetFloat("speed", speed);


        if (speed > 0.1f)
        {
            _timeToNextStep -= Time.deltaTime;
            if (_timeToNextStep <= 0f)
            {
                PlayFootstep();
                _timeToNextStep = speed < 2f ? _intervalWalk : _intervalRun;
            }
        }
    }

    public void PlayFootstep()
    {
        if (_footstepSound == null) return;
        _audioSrc.PlayOneShot(_footstepSound, _volume);
        print("play step");
    }
}
