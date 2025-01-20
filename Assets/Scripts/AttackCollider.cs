using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private float _damage = 0f;
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private string _playerTag = "Player";

    private void Awake()
    {
        _damage = _enemyController._damage;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(_playerTag))
        {
            var player = col.GetComponent<HealthController>();
            player.TakeDamage(_damage);
        }
    }

    public void EndAttack()
    {
        _enemyController.EndAttackAnimation();
    }
}