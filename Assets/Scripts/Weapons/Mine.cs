using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioPlayShoot))]
[RequireComponent(typeof(AudioSource))]
public class Mine : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private ParticleSystem _explosionEffect;

    [SerializeField] private Light _explosionLight;
    [SerializeField] private float _lightDuration = 0.2f;

    [SerializeField] private float _blastRadius = 5f;
    [SerializeField] private float _blastForce = 700f;
    [SerializeField, Range(0f, 1f)] private float _upwardModifier = 0.3f;

    private AudioPlayShoot _playShoot;
    private bool _isExploding;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _playShoot = GetComponent<AudioPlayShoot>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _isExploding = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isExploding) return;

        if (other.gameObject.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(_damage, -transform.up);
            StartCoroutine(ExplosionRoutine());
        }
        if (other.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(_damage, -transform.right);
            StartCoroutine(ExplosionRoutine());
        }
    }
    [ContextMenu("Boom")]
    private IEnumerator ExplosionRoutine()
    {
        _playShoot.PlayShootSound();
        _isExploding = true;

        if (_explosionLight != null)
            _explosionLight.enabled = true;
        yield return null;

        _playShoot.PlayShootSound();

        if (_explosionEffect != null)
        {
            _explosionEffect.transform.position = transform.position;
            _explosionEffect.Play();
        }

        var colliders = Physics.OverlapSphere(transform.position, _blastRadius);
        foreach (var col in colliders)
        {
            if (col.attachedRigidbody == null) continue;

            Vector3 dir = (col.transform.position - transform.position).normalized;
            dir.y += _upwardModifier;
            col.attachedRigidbody.AddForce(dir * _blastForce, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(_lightDuration);

        if (_explosionLight != null)
            _explosionLight.enabled = false;

        _meshRenderer.enabled = false;
        yield return new WaitForSeconds(9f); // идея не очень. рефакторить

        Destroy(gameObject);
    }
}
