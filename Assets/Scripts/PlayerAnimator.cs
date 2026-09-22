using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    //Refrence to the Animator component
    Animator am;
    PlayerMovment pm;
    SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        am = GetComponent<Animator>();
        pm = GetComponent<PlayerMovment>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.moveDir.x != 0 || pm.moveDir.y != 0)
        {
            am.SetBool("Move", true);

            SpriteDictionChecker();
        }
        else
        {
            am.SetBool("Move", false);
        }
    }

    void SpriteDictionChecker()
    {
        if (pm.lastHorizontalVector < 0)
        {
            sr.flipX = true;

        }
        else
        {
            sr.flipX = false;
        }
    }
}
