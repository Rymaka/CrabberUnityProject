using TMPro;
using UnityEngine;

public class ResourceUpdater : MonoBehaviour
{
    [SerializeField]  private TextMeshProUGUI _wood;
    [SerializeField]  private TextMeshProUGUI _gold;
    [SerializeField]  private ResourceHandlerSO _resourceHandlerSo;

    private void Awake()
    {
        UpdateResources();
    }
    public void UpdateResources()
    {
        _wood.text = _resourceHandlerSo._wood.ToString();
        _gold.text = _resourceHandlerSo._gold.ToString();
    }
}
