using UnityEngine;

public class SlimeBallBehavure : ProjectialWeaponBehaver
{
    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        transform.position += direction * currentSpeed * Time.deltaTime; // Move the slime ball in the specified direction at the specified speed
    }
}
