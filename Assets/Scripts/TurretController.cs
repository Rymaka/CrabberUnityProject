using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private float _maxRange = 100f;
    [SerializeField] private float _cooldown = 2f;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shootingPoint;
    private GameObject[] _enemies;
    private GameObject _closestEnemy;
    private bool _isCD;


    private void Update()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (_enemies.Length > 0)
        {
            GetClosestEnemy();
            transform.LookAt(_closestEnemy.transform);
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_closestEnemy != null && !_isCD)
            if (Vector3.Distance(_closestEnemy.transform.position, transform.position) <= _maxRange)
            {
                Instantiate(_bullet, _shootingPoint.position, _shootingPoint.rotation);
                _isCD = true;
                StartCoroutine(Cooldown());
            }
    }


    private void GetClosestEnemy()
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

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(_cooldown);
        _isCD = false;
    }
}