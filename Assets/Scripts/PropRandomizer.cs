using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomizer : MonoBehaviour
{

    public List<GameObject> propSpawnPoints; // List of prop spawn points
    public List<GameObject> propPrefabs; // List of prop prefabs to spawn

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnProps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnProps()
    {
        foreach (GameObject sp in propSpawnPoints)
        {
            int rand = Random.Range(0, propPrefabs.Count); // Get a random index for the prop prefab
            GameObject prop = Instantiate(propPrefabs[rand], sp.transform.position, sp.transform.rotation); // Instantiate the prop prefab at the spawn point
            prop.transform.parent = sp.transform; // Set the parent of the prop to the spawn point
        }
    }
}
