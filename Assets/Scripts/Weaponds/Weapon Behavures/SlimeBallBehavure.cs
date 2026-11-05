using UnityEngine;

public class SlimeBallBehavure : ProjectialWeaponBehaver
{
    SlimeBallController slimeC;

    protected override void Start()
    {
        base.Start();
        slimeC = FindObjectOfType<SlimeBallController>();
    }

    void Update()
    {
        transform.position += direction * slimeC.speed * Time.deltaTime; // Move the slime ball in the specified direction at the specified speed
    }
}
