using UnityEngine;

public class WeaponController : MonoBehaviour
{
    /*
     * Base script for all weapon controllers
     */
    [Header("Weapon Stats")]
    public WeaponsScriptableObjects weaponData;
    public float currentCooldown;

    protected PlayerMovment pm;

    // virtual allows the method to be overridden in a derived class
    // protected allows the method to be accessed in a derived class
    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovment>();
        currentCooldown = weaponData.CooldownDuration; // At the start set the current cooldown to be the cooldown duration
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
        currentCooldown = weaponData.CooldownDuration; // Reset the cooldown
    }
}
