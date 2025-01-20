using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    [SerializeField] private StatsHandler _statsHandler;
    [SerializeField] private SpiderController _player;
    [SerializeField] private ResourceHandlerSO _resourceHandler;
    public float bonusAttackSpeed = 0f;
    public int bonusDamage = 0;
    public int enemyBonusDamage = 0;
    public float additionalPlayerSize = 0f;
    public bool bounceImmune = false;
    [SerializeField] private TurretController _turretController;
    private SpiderController _playerController;
    private PlayerHP _playerHP;
    private void Awake()
    {
        ReloadStats();
        _playerController = _player.GetComponent<SpiderController>();
        _playerHP = _player.GetComponent<PlayerHP>();
    }
    
    public void ReloadStats()
    {
        bonusAttackSpeed = _statsHandler.attackSpeed;
        bonusDamage = _statsHandler.playerDamage;
        enemyBonusDamage = _statsHandler.enemyBonusDamage;
        additionalPlayerSize = _statsHandler.size;
        bounceImmune = _statsHandler.bounceImmune;
        ApplyBonusStats();
    }

    public void ApplyBonusStats()
    {
        _turretController.AddBonusStats();
        _playerController._speed = _statsHandler.moveSpeed;
        _resourceHandler.bonusWood = _statsHandler.bonusWood;
        _playerHP.bonusDamage = enemyBonusDamage;
    }

    public void AddPlayerDamage(int damage)
    {
        _statsHandler.playerDamage += damage;
        ReloadStats();
    }

    public void AddAttackSpeed(float attackSpeed)
    {
        _statsHandler.attackSpeed = +attackSpeed;
        ReloadStats();
    }
    
    public void IncreaseMoveSpeed(float moveSpeed)
    {
        _statsHandler.moveSpeed += moveSpeed;
        ReloadStats();
    }

    public void IncreaseSize(float size)
    {
        _statsHandler.size += size;
        ReloadStats();
    }

    public void IncreaseEnemyDamage(int damage)
    {
        _statsHandler.enemyBonusDamage += damage;
        ReloadStats();
    }

    public void IncreaseBonusWood(int bonusWood)
    {
        _statsHandler.bonusWood += bonusWood;
        ReloadStats();
    }

    public void BounceImmune(bool bounceImmune)
    {
        _statsHandler.bounceImmune = bounceImmune;
        ReloadStats();
    }
}
