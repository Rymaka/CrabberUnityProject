using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TreeController : HealthController
{
    //[SerializeField] private float PrecentHPForResource = 50f;
    //private float _precentHP;
    private int _maxResources;
    private int _resorsesGiven = 0;
    //private float _precentThreshold = 100;
    [SerializeField] private ResourceHandlerSO _resourceHandlerSO;
    [SerializeField] private ResourceUpdater _resourceUpdater;
    [SerializeField] private int _resourceOnDeath = 10;
    private bool _cooldown = false;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private float _cooldownTimer = 1f;
    //private ResourceFarm _player;
    
    protected override void OnAwake()
    {
        base.OnAwake();
        //_player = GameObject.FindGameObjectWithTag("Player").GetComponent<ResourceFarm>();
        //_maxResources = 100 - (int)_precentHP;
        Debug.Log(_maxResources + "max resources");
    }

    public override void TakeDamage(float damage)
    {
        if (!_cooldown && !_dead)
        {
            StartCoroutine(CooldownTimer());
            base.TakeDamage(damage);
            //_precentHP = (_health / _maxHealth) * 100f;

            /*if (_maxResources > 0 && _precentThreshold > 0)
            {
                _precentThreshold = (_precentHP / 100f) - _resorsesGiven * PrecentHPForResource;
                _resorsesGiven++;
                _maxResources--;
            } */
        }
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        StartCoroutine(MoveUnderGround(new Vector3(_collider.center.x, _collider.center.y, _collider.center.z)
            , new Vector3(_collider.center.x, _collider.center.y - 5f, _collider.center.z), 0.3f));
        _resourceHandlerSO.AddWood(_resourceOnDeath);
        _resourceUpdater.UpdateResources();
    }

    IEnumerator MoveUnderGround(Vector3 beginPos, Vector3 endPos, float time)
    {
        for (float t = 0; t < 1; t += Time.deltaTime / time)
        {
            _collider.center = Vector3.Lerp(beginPos, endPos, t);
            yield return null;
        }
        //TODO: remake this
        Destroy(gameObject);
    }

    private IEnumerator CooldownTimer()
    {
        _cooldown = true;
        yield return new WaitForSeconds(_cooldownTimer);
        _cooldown = false;
    }
}