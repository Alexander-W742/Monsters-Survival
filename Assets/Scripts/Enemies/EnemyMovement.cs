using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyStats enemy;
    Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindAnyObjectByType<PlayerMovment>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the enemy towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMovSpeed * Time.deltaTime);
    }
}
