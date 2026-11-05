using UnityEngine;

public class WeaponController : MonoBehaviour
{
    /*
     * Base script for all weapon controllers
     */
    [Header("Weapon Stats")]
    public GameObject Prefab;
    public float damage;
    public float speed;
    public float cooldownDuration;
    public float currentCooldown;
    public float pirce;

    protected PlayerMovment pm;

    // virtual allows the method to be overridden in a derived class
    // protected allows the method to be accessed in a derived class
    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovment>();
        currentCooldown = cooldownDuration; // At the start set the current cooldown to be the cooldown duration
    }

    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime; // Decrease the current cooldown
        if (currentCooldown <= 0f) // Once cooldown becomes zero the player will attack
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = cooldownDuration; // Reset the cooldown
    }
}
