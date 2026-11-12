using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")] 
public class EnemyScriptableObject : ScriptableObject
{
    // Base stats for the enemy
    [SerializeField]
    float movSpeed;
    public float MovSpeed { get => movSpeed; private set => movSpeed = value; }
    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }
}
