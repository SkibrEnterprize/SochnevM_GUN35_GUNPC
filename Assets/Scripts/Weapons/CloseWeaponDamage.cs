using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RaycastWeapon))]
public class CloseWeaponDamage : MonoBehaviour
{
    private RaycastWeapon _weapon;

    private void Awake()
    {
        _weapon = GetComponent<RaycastWeapon>();
    }
    private void OnTriggerEnter(Collider other)
    {
        print("Take!!!!");
        if(other.gameObject.TryGetComponent<Health>(out Health health))            
        {            
            health.TakeDamage(_weapon.damage, -transform.right);
        }
        if (other.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(_weapon.damage, transform.up);
        }
    }
}
