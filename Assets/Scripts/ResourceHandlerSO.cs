using UnityEngine;

[CreateAssetMenu(fileName = "Resource Handler", menuName = "ScriptableObjects/Resource Handler", order = 1)]
public class ResourceHandlerSO : ScriptableObject
{
    public int _wood;
    public int _gold;

    public void AddWood(int wood)
    {
        _wood += wood;
    }
}