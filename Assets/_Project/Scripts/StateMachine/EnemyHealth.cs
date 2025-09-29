using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Настройки здоровья")]
    [SerializeField] private int _maxHealth = 100;      
    [SerializeField] private float _invincibilityTime = 0.5f;

 
    public int CurrentHealth => _currentHealth;
 
    public event System.Action<EnemyHealth> OnDeath;

  
    private int _currentHealth;
    private float _invincibilityTimer; 

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _invincibilityTimer = 0f;
    }

    public void TakeDamage(float damage)
    {
        if (_invincibilityTimer > 0f) return;        

        _currentHealth -= Mathf.RoundToInt(damage);
        _currentHealth = Mathf.Max(_currentHealth, 0);

        // Сброс таймера
        _invincibilityTimer = _invincibilityTime;

        if (_currentHealth <= 0)
            Die();
    }
    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }


    private void Die()
    {
        OnDeath?.Invoke(this);          
        Destroy(gameObject);
    }


    private void Update()
    {
        if (_invincibilityTimer > 0f)
            _invincibilityTimer -= Time.deltaTime;
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = (_currentHealth > 0) ? Color.green : Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
#endif
}