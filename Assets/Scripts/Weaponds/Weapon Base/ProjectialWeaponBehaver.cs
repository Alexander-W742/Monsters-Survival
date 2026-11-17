using UnityEngine;

public class ProjectialWeaponBehaver : MonoBehaviour
{
    /*
     * Base script for all projectile weapons [to be placed on a prefab of a weapon that is a projectile]
     */

    public WeaponsScriptableObjects weaponData;

    protected Vector3 direction;
    public float DestroyAfterSeconds;

    // current stats of projectial weapons
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }

    protected virtual void Start()
    {
        Destroy(gameObject, DestroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirx < 0 && diry == 0) // left
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }
        else if(dirx == 0 && diry < 0) // down
        {
            scale.y = scale.y * -1;
        }
        else if (dirx == 0 && diry > 0) // up
        {
            scale.x = scale.x * -1;
        }
        else if (dirx > 0 && diry > 0) // right up
        {
            rotation.z = 0f;
        }
        else if (dirx > 0 && diry < 0) // right down
        {
            rotation.z = -90f;
        }
        else if (dirx < 0 && diry > 0) // left up
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = -90f;
        }
        else if (dirx < 0 && diry > 0) // left down
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = 0f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Refferance the script from the collided collider and deal damage using TakeDamage()
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage); // use currentDamage just incase I use any damage multiplyers in the future
            ReducePierce();
        }
    }

    void ReducePierce() // destroy the projectile after it has hit a certain amount of enemies
    {
        currentPierce--;
        if (currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
