using UnityEngine;
[CreateAssetMenu(fileName = "Stats Handler", menuName = "ScriptableObjects/Stats Handler", order = 1)]

public class StatsHandler : ScriptableObject
{
    public int playerDamage = 3;
    public float attackSpeed = 3f;
    public float moveSpeed = 7f;
    //TODO: add logic to increase size
    public float size = 1f;
    public int enemyBonusDamage = 1;
    public int bonusWood = 0;
    //TODO: add logic to bounce
    public bool bounceImmune = false;
}
