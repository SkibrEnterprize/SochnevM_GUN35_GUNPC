using UnityEngine;

public class TriggerOfFall : MonoBehaviour
{
    private bool _isFalling = false;
    public bool IsFalling => _isFalling;        
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Ground>(out Ground ground)) _isFalling = true;
    }
}
