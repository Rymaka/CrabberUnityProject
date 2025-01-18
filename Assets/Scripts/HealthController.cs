using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] protected float _maxHealth = 100f;
    [SerializeField] protected float _health = 100f;
    protected bool _dead = false;

    private void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake()
    {
        _health = _maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        if (!_dead)
        {
            _health -= damage;
            if (_health <= 0f)
            {
                _health = 0f;
                OnDeath();
            }

            Debug.Log(_health + " " + gameObject.name);
        }
    }

    public virtual void GetHeal(float heal)
    {
        _health += heal;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    protected virtual void OnDeath()
    {
        _dead = true;
    }
}