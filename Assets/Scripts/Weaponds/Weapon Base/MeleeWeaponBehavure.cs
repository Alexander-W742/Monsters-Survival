using UnityEngine;


/*
 * Base script for all melee behaviours [to be placed on a melee weapon prefab]
 */
public class MeleeWeaponBehavure : MonoBehaviour
{
    public WeaponsScriptableObjects weaponData;

    public float destroyAfterSeconds;

    // base stats for melee weapons
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
        Destroy(gameObject, destroyAfterSeconds);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Refferance the script from the collided collider and deal damage using TakeDamage()
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage); // use currentDamage just incase I use any damage multiplyers in the future
        }
        else if (collision.CompareTag("Prop"))
        {
            if (collision.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(currentDamage);
            }
        }
    }

}
