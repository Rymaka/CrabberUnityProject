using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour
{
    [SerializeField] protected float _woodPrice;
    [SerializeField] protected float _goldPrice;
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

    public void BuyCard()
    {
        
    }
    
}
