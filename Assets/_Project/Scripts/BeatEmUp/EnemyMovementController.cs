using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] private float _speed = 3f; 
    public void UpdateMovement(Transform target)
    {
        if (target == null) return;

        Vector3 diff = target.position - transform.position;
        Vector2 direction = new Vector2(diff.x, diff.y).normalized;      

        Vector2 movement = direction * _speed * Time.deltaTime;
        transform.position += new Vector3(movement.x, movement.y, 0f);
    }
}
