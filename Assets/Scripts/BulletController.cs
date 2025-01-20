using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _deathTimer = 7f;

    private void Awake()
    {
        StartCoroutine(DeathTimer());
    }

    private void FixedUpdate()
    {
        _rb.velocity = transform.forward * _speed;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            col.gameObject.SendMessage("TakeDamage", _damage);
            Destroy(gameObject);
        }
    }

    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(7f);
        Destroy(gameObject);
    }
}