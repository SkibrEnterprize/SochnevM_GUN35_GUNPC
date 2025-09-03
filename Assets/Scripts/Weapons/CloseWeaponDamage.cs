using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(RaycastWeapon))]
public class CloseWeaponDamage : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitEffectBlood;
    private RaycastWeapon _weapon;
    private AudioPlayShoot _playShoot;
    private void Awake()
    {
        _playShoot = GetComponent<AudioPlayShoot>();
        _weapon = GetComponent<RaycastWeapon>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(_weapon.Damage, -transform.right);
            _playShoot.PlayShootSound();
        }
        if (other.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(_weapon.Damage, transform.up);
            _playShoot.PlayShootSound();
        }

        _hitEffectBlood.Play();
    }
}
