using UnityEngine;


/*
 * Base script for all melee behaviours [to be placed on a melee weapon prefab]
 */
public class MeleeWeaponBehavure : MonoBehaviour
{
    public float destroyAfterSeconds;

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

}
