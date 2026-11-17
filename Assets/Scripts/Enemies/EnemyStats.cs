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

    public float despawnDistance = 20f;
    Transform player;

    private void Awake()
    {
        // Initialize current stats with the values from the scriptable object
        currentMovSpeed = enemyData.MovSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>().transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReTurnEnemy();
        }
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

    private void OnDestroy()
    {
        EnemySpawner es = FindAnyObjectByType<EnemySpawner>();
        es.OnEnemyKilled();
    }

    void ReTurnEnemy()
    {
        EnemySpawner es = FindAnyObjectByType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }
}
