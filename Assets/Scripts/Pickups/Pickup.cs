using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // if object gets to close to player destoy it
        {
            Destroy(gameObject);
        }
    }
}
