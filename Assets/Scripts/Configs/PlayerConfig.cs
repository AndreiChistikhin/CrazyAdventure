using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Player", fileName = "PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public float MaxHealth;
    public float Damage;
    public float Cleavage;
}