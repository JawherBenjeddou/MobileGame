using System.Collections;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        // Check if it's time to spawn a new car
        if (Time.time > m_NextSpawn)
        {
            // Randomly choose a car from the array
            m_WhatToSpawn = Random.Range(0, m_CarPrefabs.Length);

            // Calculate a random spawn position between spawnPosition1 and spawnPosition2
            Vector3 spawnPosition = Vector3.Lerp(m_SpawnPosition1, m_SpawnPosition2, Random.Range(0f, 1f));

            // Instantiate the chosen car at the calculated spawn position
            Instantiate(m_CarPrefabs[m_WhatToSpawn], spawnPosition, Quaternion.identity);

            // Update the next spawn time
            m_NextSpawn = Time.time + m_SpawnRate;
        }
    }
    [Header("Vehicle Settings")]
    [SerializeField] private GameObject[] m_CarPrefabs;  // Array of car prefabs
    [SerializeField] private Vector3 m_SpawnPosition1;    // First spawn position
    [SerializeField] private Vector3 m_SpawnPosition2;    // Second spawn position
    [SerializeField] private float m_SpawnRate = 2f;      // Rate at which cars spawn

    private float m_NextSpawn = 0f;  // Time at which the next car will spawn
    private int m_WhatToSpawn;       // Index of the car to spawn

}
