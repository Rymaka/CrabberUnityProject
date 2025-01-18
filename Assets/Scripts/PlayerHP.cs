using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : HealthController
{
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private Slider _HPSlider;
    private float _hpRatio;

    protected override void OnAwake()
    {
        base.OnAwake();
        _hpRatio = _health / _maxHealth;
        _hpText.text = _health.ToString() + "/" + _maxHealth.ToString();
        _HPSlider.value = _hpRatio;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        _hpText.text = _health.ToString() + "/" + _maxHealth.ToString();
        _HPSlider.value = _hpRatio;
    }

    public override void GetHeal(float damage)
    {
        base.GetHeal(damage);
        _hpText.text = _health.ToString() + "/" + _maxHealth.ToString();
        _HPSlider.value = _hpRatio;
    }
}