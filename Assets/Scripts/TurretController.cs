using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] protected float _maxRange = 100f;
    [SerializeField] protected float _cooldown = 2f;
    [SerializeField] protected GameObject _bullet;
    [SerializeField] protected Transform _shootingPoint;
    [SerializeField] protected Transform _target;
    [SerializeField] protected PlayerStatsController _playerStats;
    protected GameObject[] _enemies;
    protected GameObject _closestEnemy;
    protected bool _isCD;
    protected int _additionalDamage;
    protected float _cooldownBonus;

    public void AddBonusStats()
    {
        _additionalDamage = _playerStats.bonusDamage;
        _cooldownBonus = _playerStats.bonusAttackSpeed;
        _cooldown -= _cooldownBonus;
    }
    private void FixedUpdate()
    {
        OnFixedUpdate();
    }

    protected virtual void OnFixedUpdate()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (_enemies.Length > 0)
        {
            GetClosestEnemy();
            transform.LookAt(_closestEnemy.transform);
            Shoot();
        }
    }

    protected virtual void Shoot()
    {
        if (_closestEnemy != null && !_isCD)
            if (Vector3.Distance(_closestEnemy.transform.position, transform.position) <= _maxRange)
            {
                Instantiate(_bullet, _shootingPoint.position, _shootingPoint.rotation);
                var bc = _bullet.GetComponent<BulletController>();
                bc._damage += _additionalDamage;
                _isCD = true;
                StartCoroutine(Cooldown());
            }
    }
    
    protected virtual void GetClosestEnemy()
    {
        _closestEnemy = null;
        var closestDistanceSqr = Mathf.Infinity;
        var currentPosition = transform.position;
        foreach (var go in _enemies)
        {
            var directionToTarget = go.transform.position - currentPosition;
            var dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                _closestEnemy = go;
            }
        }
    }

    protected virtual IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(_cooldown);
        _isCD = false;
    }
}