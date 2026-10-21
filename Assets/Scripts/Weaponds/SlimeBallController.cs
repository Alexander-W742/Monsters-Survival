using UnityEngine;

public class SlimeBallController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawndSlimeBall = Instantiate(Prefab);
        spawndSlimeBall.transform.position = transform.position; // Set the position of the spawned slime ball to be the same as the player
        spawndSlimeBall.GetComponent<SlimeBallBehavure>().DirectionChecker(pm.moveDir); // Set the direction of the spawned slime ball to be the same as the player's direction
    }
}
