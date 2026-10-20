using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    // This script was created with the help of AI to help replace the old chunk spawning system.
    // that used the physics engine to spawn new chunks on empty gameobjects.
    public List<GameObject> terrainChunks; // List of terrain chunks to spawn
    public GameObject player;              // Reference to the player object
    public float checkerRadius;            // Radius for checking if a chunk already exists
    public Vector2 chunkSize = new Vector2(20f, 20f); // Distance between neighboring chunk centers
    public LayerMask terrainMask;          // Layer mask for terrain chunks
    public GameObject currentChunck;       // Chunk the player is currently on (updated automatically)

    Vector3 noTerrainPosition;
    PlayerMovment pm;

    void Start()
    {
        pm = FindAnyObjectByType<PlayerMovment>();
        if (!player)
        {
            player = pm.gameObject;
        }
    }

    void Update()
    {
        UpdateCurrentChunk();
        Chunkchecker();
    }

    void UpdateCurrentChunk()
    {
        // Which terrain collider is under the player right now?
        Collider2D hit = Physics2D.OverlapPoint(player.transform.position, terrainMask);
        if (hit)
        {
            // Use the collider's root object in case the collider is on a child
            currentChunck = hit.attachedRigidbody ? hit.attachedRigidbody.gameObject : hit.transform.root.gameObject;
        }
    }

    void Chunkchecker()
    {
        if (!currentChunck)
        {
            return;
        }

        int dirX = pm.moveDir.x > 0 ? 1 : (pm.moveDir.x < 0 ? -1 : 0);
        int dirY = pm.moveDir.y > 0 ? 1 : (pm.moveDir.y < 0 ? -1 : 0);

        if (dirX == 0 && dirY == 0)
        {
            return;
        }

        Vector3 checkPosition = currentChunck.transform.position + new Vector3(dirX * chunkSize.x, dirY * chunkSize.y, 0f);

        if (!Physics2D.OverlapCircle(checkPosition, checkerRadius, terrainMask))
        {
            noTerrainPosition = checkPosition;
            SpawnChunk();
        }
    }

    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
    }
}