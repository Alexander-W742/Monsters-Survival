using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    // current stats for the enemy
    public float currentMovSpeed;
    public float currentHealth;
    public float currentDamage;

    private void Awake()
    {
        // Initialize current stats with the values from the scriptable object
        currentMovSpeed = enemyData.MovSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    public void TakeDamage(float dmg)
    {
            currentHealth -= dmg;

            if (currentHealth <= 0)
            {
                Kill();
            }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
       
}
