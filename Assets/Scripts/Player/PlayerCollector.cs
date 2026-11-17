using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        // check if the other game object has the ICollectible interface
        if (collider.gameObject.TryGetComponent(out ICollectible collectible))
        {
            // if it does, call the collect method
            collectible.Collect();
        }
    }
}
