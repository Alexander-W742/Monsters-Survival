using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    // current stats for the enemy
    [HideInInspector]
    public float currentMovSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats player = collision.gameObject.GetComponent < PlayerStats>();
            player.TakeDamage(currentDamage); // use currentDamage instead of weapon.damage in case any damage multiplyers in the future
        }
    }
}
