using UnityEngine;
[CreateAssetMenu(fileName = "Stats Handler", menuName = "ScriptableObjects/Stats Handler", order = 1)]

public class StatsHandler : ScriptableObject
{
    public int bonusPlayerDamage = 3;
    public float bonusAttackSpeed = 3f;
    public float moveSpeed = 7f;
    //TODO: add logic to increase size
    public float size = 1f;
    public int enemyBonusDamage = 0;
    public int bonusWood = 0;
    //TODO: add logic to bounce
    public bool bounceImmune = false;
}
