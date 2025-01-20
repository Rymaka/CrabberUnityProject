using UnityEngine;
using UnityEngine.AI;

public class EnemyController : HealthController
{
    [SerializeField] protected float _attackRange = 1f;
    [SerializeField] protected float _visionRange = 5f;
    public readonly float _damage = 1f;
    private NavMeshAgent _agent;
    private GameObject _player;
    private bool _isAttacking = false;
    private bool _activatted = false;

    protected virtual void Start()
    {
        _player = PlayerReference.Player;
        _agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void FixedUpdate()
    {
        var distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
        var canSeePlayer = distanceToPlayer <= _visionRange;
        //if (canSeePlayer) transform.LookAt(_player.transform.position);

        if (distanceToPlayer > _attackRange && !_isAttacking && (canSeePlayer || _activatted))
        {
            MoveToPlayer();
        }
        else
        {
            _agent.isStopped = true;
            if (!_isAttacking && canSeePlayer) Attack();
        }
    }

    protected virtual void Attack()
    {
        _isAttacking = true;
    }

    public void EndAttackAnimation()
    {
        _isAttacking = false;
    }

    protected virtual void MoveToPlayer()
    {
        if ((_agent != null) & (_player != null))
        {
            _agent.isStopped = false;
            _agent.updateRotation = true;
            _agent.SetDestination(_player.transform.position);
        }

        _activatted = true;
    }
}