using UnityEngine;

public class PlayerReference : MonoBehaviour
{
    public static GameObject Player { get; private set; }
    
    private void Awake()
    {
        Player = gameObject;
    }
}