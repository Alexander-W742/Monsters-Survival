using UnityEditor.U2D.Animation;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D playerCollector;
    public float pullSpeed;

    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>();
        playerCollector = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        playerCollector.radius = player.currentMagnet;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // pull animation
        // gets the rigidbody2D component on the item
        Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();
        // Vector2 pointing from the item to the player
        Vector2 forceDirection = (transform.position - collider.transform.position).normalized;
        // pulls the item
        rb.AddForce(forceDirection * pullSpeed);

        // check if the other game object has the ICollectible interface
        if (collider.gameObject.TryGetComponent(out ICollectible collectible))
        {
            // if it does, call the collect method
            collectible.Collect();
        }
    }
}
