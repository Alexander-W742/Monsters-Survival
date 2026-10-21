using UnityEngine;

public class ProjectialWeaponBehaver : MonoBehaviour
{
    /*
     * Base script for all projectile weapons [to be placed on a prefab of a weapon that is a projectile]
     */

    protected Vector3 direction;
    public float DestroyAfterSeconds;

    protected virtual void Start()
    {
        Destroy(gameObject, DestroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;
    }
}
