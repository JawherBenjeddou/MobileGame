using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // Check if it's time to spawn a new obstacle
        if (Time.time > m_NextSpawn)
        {
            // Randomly choose an obstacle from the array
            m_WhatToSpawn = Random.Range(0, m_PropPrefabs.Length);

            // Calculate a random spawn position between spawnPosition1 and spawnPosition2
            Vector3 spawnPosition = Vector3.Lerp(m_SpawnPosition1, m_SpawnPosition2, Random.Range(0f, 1f));

            // Instantiate the chosen obstacle at the calculated spawn position
            Instantiate(m_PropPrefabs[m_WhatToSpawn], spawnPosition, Quaternion.identity);

            // Update the next spawn time
            m_NextSpawn = Time.time + m_SpawnRate;
        }
    }
    [Header("Obstacle Settings")]
    [SerializeField] private GameObject[] m_PropPrefabs;  // Array of obstacle prefabs
    [SerializeField] private Vector3 m_SpawnPosition1;    // First spawn position
    [SerializeField] private Vector3 m_SpawnPosition2;    // Second spawn position
    [SerializeField] private float m_SpawnRate = 2f;      // Rate at which obstacles spawn

    private float m_NextSpawn = 0f;  // Time at which the next obstacle will spawn
    private int m_WhatToSpawn;       // Index of the obstacle to spawn

}
