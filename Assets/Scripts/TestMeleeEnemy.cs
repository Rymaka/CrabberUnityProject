using UnityEngine;

public class TestMeleeEnemy : EnemyController
{
    [SerializeField] private Animator _animator;
    protected override void Attack()
    {
        base.Attack();
        _animator.SetTrigger("Attack");
    }
    protected override void OnDeath()
    {
        base.OnDeath();
        Destroy(gameObject);
    }
}
