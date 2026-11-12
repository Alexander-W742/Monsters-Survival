using UnityEngine;

public class AcidWaveController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedAcidWave = Instantiate(weaponData.PreFab);
        spawnedAcidWave.transform.position = transform.position; // Assign the position to the player's position
        spawnedAcidWave.transform.parent = transform; // Set the parent to the player
    }
}
