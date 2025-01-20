using UnityEngine;

[CreateAssetMenu(fileName = "Resource Handler", menuName = "ScriptableObjects/Resource Handler", order = 1)]
public class ResourceHandlerSO : ScriptableObject
{
    public int _wood;
    public int bonusWood = 0;
    public int _gold;
    public int _cores;

    public void AddWood(int wood)
    {
        _wood += wood + bonusWood;
    }
    
    public void AddGold(int gold)
    {
        _gold += gold;
    }
    
    public void AddCores(int cores)
    {
        _cores += cores;
    }
    
}