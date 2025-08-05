using UnityEngine;

public class Dirt : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<CleanerMover>(out CleanerMover mover)) Destroy(gameObject);
    }
}
