using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks; // List of terrain chunks to spawn
    public GameObject player; // Reference to the player object
    public float checkerRadius; // Radius for checking if the player is within a terrain chunk
    Vector3 noTerrainPosition; // Position where there is no terrain chunk
    public LayerMask terrainMask; // Layer mask for terrain chunks
    public GameObject currentChunck; // Reference to the current terrain chunk
    PlayerMovment pm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pm = FindAnyObjectByType<PlayerMovment>();
    }

    // Update is called once per frame
    void Update()
    {
        Chunkchecker();
    }

    void Chunkchecker()
    {
        if (!currentChunck)
        {
            return;
        }
        if (pm.moveDir.x > 0 && pm.moveDir.y == 0) // right
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunck.transform.Find("Right").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y == 0) // left
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunck.transform.Find("Left").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x == 0 && pm.moveDir.y > 0) // up
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunck.transform.Find("Up").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x == 0 && pm.moveDir.y < 0) // down
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunck.transform.Find("Down").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y > 0) // right up
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Right Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunck.transform.Find("Right Up").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y < 0) // right down
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Right Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunck.transform.Find("Right Down").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y > 0) // left up
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Left Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunck.transform.Find("Left Up").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y < 0) // left down
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Left Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunck.transform.Find("Left Down").position;
                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count); // Get a random index for the terrain chunk
        Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity); // Instantiate the terrain chunk at the noTerrainPosition
    }
}
