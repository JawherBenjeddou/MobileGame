using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // Check if spawn points and vehicle prefabs are available
        if (m_SpawnPoints.Length == 0 || m_VehiclesPrefabs.Length == 0)
        {
            Debug.LogWarning("Spawn points or vehicle prefabs are not set!");
            return;
        }

        // Call the vehicle spawn method
        VehicleSpawn();
    }

    // Vehicle spawning logic
    void VehicleSpawn()
    {
        // Iterate through all spawn points and spawn a different vehicle at each one
        foreach (Transform spawnPoint in m_SpawnPoints)
        {
            // Get a different vehicle than the last frame
            GameObject vehiclePrefab = GetDifferentVehicleThanLastFrame();

            // Instantiate the selected vehicle prefab at the current spawn point
            GameObject spawnedVehicle = Instantiate(vehiclePrefab, spawnPoint.position, spawnPoint.rotation);

            // Optionally, you can parent the spawned vehicle to the VehicleManager
            spawnedVehicle.transform.parent = transform;

            // Update the last spawned vehicle
            m_LastSpawnedVehiclePrefab = spawnedVehicle;

            // Add the spawned vehicle to the list
            m_VehiclesSpawned.Add(spawnedVehicle);
        }
    }

    // Get a vehicle prefab different than the last frame
    private GameObject GetDifferentVehicleThanLastFrame()
    {
        GameObject newVehiclePrefab;
        do
        {
            // Randomly select a vehicle prefab
            newVehiclePrefab = m_VehiclesPrefabs[Random.Range(0, m_VehiclesPrefabs.Length)];
        } while (newVehiclePrefab == m_LastSpawnedVehiclePrefab);

        return newVehiclePrefab;
    }

    // Variables
    [SerializeField] private Transform[] m_SpawnPoints; // Array of spawn points for vehicles
    [SerializeField] private GameObject[] m_VehiclesPrefabs; // Array of vehicle prefabs

    private GameObject m_LastSpawnedVehiclePrefab; // Reference to the last spawned vehicle prefab
    private List<GameObject> m_VehiclesSpawned = new List<GameObject>(); // List to store spawned vehicles

}
