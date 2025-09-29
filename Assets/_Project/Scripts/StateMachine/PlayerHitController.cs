using UnityEngine;

public class PlayerHitController : MonoBehaviour
{
    [Header("Настройки удара")]
    [SerializeField] private float _range = 2.0f;          // Длина луча
    [SerializeField] private LayerMask _enemyLayer;        // Слой противников
    [SerializeField] private float _damage = 15f;         // Урон

    /// <summary>
    /// Вызывается из Animation Event (или напрямую) – отправляем луч.
    /// </summary>
    public void PerformAttack()
    {
        // Направление удара: вправо, если объект смотрит налево/вправо
        Vector2 direction = transform.right * Mathf.Sign(transform.localScale.x);

        // Выполняем Raycast
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,   // Точка начала луча – центр объекта
            direction,
            _range,
            _enemyLayer           // Проверяем только слои противников
        );

        // Отладочный луч в редакторе
#if UNITY_EDITOR
        Debug.DrawLine(transform.position,
                       hit.collider != null ? hit.point : transform.position + (Vector3)(direction * _range),
                       hit.collider != null ? Color.red : Color.green, 1f);
#endif

        if (hit.collider != null)
        {
            print(hit.point);
            var enemyFSM = hit.collider.TryGetComponent<EnemyFSM>(out EnemyFSM enemy);
            enemy.IsHitted = true;
            //// Попали – наносим урон
            //var enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            //if (enemyHealth != null)
            //    enemyHealth.TakeDamage(_damage);

            //// Можно добавить визуальный эффект «попадания» здесь …
        }
    }

    /// <summary>
    /// Отрисовка луча в редакторе при выделении объекта
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (_enemyLayer == 0) return;

        Vector3 dir = transform.right * Mathf.Sign(transform.localScale.x);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + dir * _range);
    }
}
