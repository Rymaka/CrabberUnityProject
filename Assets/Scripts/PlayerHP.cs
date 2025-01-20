using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : HealthController
{
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private Slider _HPSlider;
    private float _hpRatio;
    public float bonusDamage;

    protected override void OnAwake()
    {
        base.OnAwake();
        _hpText.text = _health.ToString() + "/" + _maxHealth.ToString();
        UpdateHPRatio();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage + bonusDamage);
        _hpText.text = _health.ToString() + "/" + _maxHealth.ToString();
        UpdateHPRatio();
    }

    public override void GetHeal(float damage)
    {
        base.GetHeal(damage);
        _hpText.text = _health.ToString() + "/" + _maxHealth.ToString();
        UpdateHPRatio();
    }

    private void UpdateHPRatio()
    {
        _hpRatio = _health / _maxHealth;
        _HPSlider.value = _hpRatio;
    }
}