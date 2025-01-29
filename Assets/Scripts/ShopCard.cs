using System;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour
{
    [SerializeField] protected int _woodPrice;
    [SerializeField] protected int _goldPrice;
    [SerializeField] protected Image _imageWindow;
    [SerializeField] protected Sprite _image;
    [SerializeField] protected TextMeshProUGUI _woodPriceText;
    [SerializeField] protected TextMeshProUGUI _goldPriceText;
    [SerializeField] protected TextMeshProUGUI _cardNameText;
    [SerializeField] protected TextMeshProUGUI _cardDescText;
    [SerializeField] protected GameObject _woodPanel;
    [SerializeField] protected GameObject _goldPanel;
    [SerializeField] protected string _cardName;
    [SerializeField] protected string _cardDescription;
    [SerializeField] protected ResourceHandlerSO _resourceHandler;
    private GameObject _player;
    private PlayerStatsController _playerStatsController;
    [SerializeField] protected int _bonusDamage = 0;
    [SerializeField] protected float bonusAttackSpeed = 0;
    [SerializeField] protected float additionalPlayerSize = 0;
    [SerializeField] protected float bonusMoveSpeed = 0;
    [SerializeField] protected int enemyBonusDamage = 0;
    [SerializeField] protected int bonusWood = 0;
    [SerializeField] protected bool bounceImmune = false;

    private void Start()
    {
        _player = PlayerReference.Player;
        _playerStatsController = _player.GetComponent<PlayerStatsController>();
    }
    
    private void Awake()
    {
        OnAwake();
    }

    protected virtual void DrawCard()
    {
        _imageWindow.sprite = _image;
        _cardNameText.text = _cardName;
        _cardDescText.text = _cardDescription;
        
        if (_woodPrice > 0)
        {
            _woodPriceText.text = _woodPrice.ToString();
        }
        else
        {
            _woodPanel.SetActive(false);
            
        }

        if (_goldPrice > 0)
        {
            _goldPriceText.text = _goldPrice.ToString();
        }
        else
        {
            _goldPanel.SetActive(false);
        }
        
    }
    
    protected virtual void OnAwake()
    {
        DrawCard();
    }

    protected virtual void GiveBuff()
    {
        if (_bonusDamage != 0)
        {
            _playerStatsController.AddPlayerDamage(_bonusDamage);
        }

        if (bonusAttackSpeed != 0)
        {
            _playerStatsController.AddAttackSpeed(bonusAttackSpeed);
        }

        if (enemyBonusDamage != 0)
        {
            _playerStatsController.IncreaseEnemyDamage(enemyBonusDamage);
        }

        if (enemyBonusDamage != 0)
        {
            _playerStatsController.IncreaseEnemyDamage(enemyBonusDamage);
        }

        if (additionalPlayerSize != 0)
        {
            _playerStatsController.IncreaseSize(additionalPlayerSize);
        }

        if (bounceImmune)
        {
            _playerStatsController.BounceImmune(bounceImmune);
        }

        if (bonusMoveSpeed != 0)
        {
            _playerStatsController.IncreaseMoveSpeed(bonusMoveSpeed);
        }

        if (bonusWood != 0)
        {
            _playerStatsController.IncreaseBonusWood(bonusWood);
        }
        
    }

    public void BuyCard()
    {
        _resourceHandler._wood -= _woodPrice;
        _resourceHandler._gold -= _goldPrice;
        GiveBuff();
        Destroy(gameObject);
    }
    
}
