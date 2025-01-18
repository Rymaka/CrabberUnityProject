using UnityEngine;

public class ResourceFarm : MonoBehaviour
{
    [SerializeField] private string _woodTag = "Wood";
    [SerializeField] private float _damagePerSec = 50f;
    [SerializeField] private SpiderController _playerScript;
    [SerializeField] private float _playerSpeedInTree;
    private float _initalSpeed;

    private void Start()
    {
        _initalSpeed = _playerScript._speed;
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag(_woodTag))
        {
            var tree = col.gameObject.GetComponent<TreeController>();
            tree.TakeDamage(_damagePerSec);
            _playerScript._speed = _playerSpeedInTree;
        }
        
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(_woodTag))
        {
            ResetSpeed();
            Debug.Log("Exit");
        }
    }

    public void ResetSpeed()
    {
        _playerScript._speed = _initalSpeed;
    }
}