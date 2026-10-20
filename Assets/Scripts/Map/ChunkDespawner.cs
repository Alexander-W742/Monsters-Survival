using System.Collections.Generic;
using UnityEngine;

public class ChunkDespawner : MonoBehaviour
{
    // Deletes terrain chunks that are too far from the chunk the player is on.

    [HideInInspector]                        // This variable prevents a public variable from showing up in the Unity Editor Inspector window
    public MapController mc;                 // Reference to the MapController
    public int deleteDistance = 2;           // Chunks this many far away (or more) get deleted
    public float checkInterval = 1.0f;       // Seconds between cleanup passes

    float timer;
    readonly HashSet<GameObject> chunksFound = new HashSet<GameObject>();

    void Start()
    {
        if (!mc)
        {
            mc = FindAnyObjectByType<MapController>();
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer < checkInterval)
        {
            return;
        }
        timer = 0f;

        DeleteFarChunks();
    }

    void DeleteFarChunks()
    {
        if (!mc || !mc.currentChunck)
        {
            return;
        }

        Vector3 center = mc.currentChunck.transform.position;

        // Search radius big enough to cover every chunk that could be at or past deleteDistance
        float radius = Mathf.Max(mc.chunkSize.x, mc.chunkSize.y) * (deleteDistance + 2);
        Collider2D[] hits = Physics2D.OverlapCircleAll(center, radius, mc.terrainMask);

        // Collect unique chunk root objects
        chunksFound.Clear();
        foreach (Collider2D hit in hits)
        {
            GameObject chunk = hit.attachedRigidbody ? hit.attachedRigidbody.gameObject : hit.transform.root.gameObject;
            chunksFound.Add(chunk);
        }

        foreach (GameObject chunk in chunksFound)
        {
            if (chunk == mc.currentChunck)
            {
                continue;
            }

            // How many chunks away is this chunk
            Vector3 offset = chunk.transform.position - center;
            int stepsX = Mathf.Abs(Mathf.RoundToInt(offset.x / mc.chunkSize.x));
            int stepsY = Mathf.Abs(Mathf.RoundToInt(offset.y / mc.chunkSize.y));
            int distance = Mathf.Max(stepsX, stepsY);

            if (distance >= deleteDistance)
            {
                Destroy(chunk);
            }
        }
    }
}