using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    // Start is called before the first frame update
    private void Start()
    {
        // Instantiate cities at the start
        foreach (var cityPrefab in m_CitiesPrefabs)
        {
            // Randomly select a city prefab
            m_CityNumber = Random.Range(0, m_CitiesPrefabs.Length);

            // Instantiate the city at the specified position
            GameObject newCity = Instantiate(m_CitiesPrefabs[m_CityNumber], new Vector3(0, 0, m_Zpos), Quaternion.identity);

            // Add the instantiated city to the list
            m_Cities.Add(newCity);

            // Increment the Z position for the next city instantiation
            m_Zpos += 58;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if city generation is allowed
        if (!m_CreatingCity)
        {
            // Set the flag to start city generation
            m_CreatingCity = true;

            // Start coroutine for generating a new city
            StartCoroutine(GenerateCity());
        }

        // Move existing cities
        MoveCities();
    }

    // Move cities based on player position and destroy them if they have passed
    private void MoveCities()
    {
        // Iterate through cities in reverse order to safely remove elements while iterating
        for (int i = m_Cities.Count - 1; i >= 0; i--)
        {
            // Get the current city
            GameObject city = m_Cities[i];

            // Move the city based on the specified speed
            city.transform.position += Vector3.back * m_MoveSpeed * Time.deltaTime;

            // Check if the city has passed the player's position
            if (city.transform.position.z < (m_Player.position.z - 15))
            {
                // Destroy the city
                Destroy(city);

                // Remove it from the list
                m_Cities.RemoveAt(i);
            }
        }
    }

    // Coroutine to generate a new city
    private IEnumerator GenerateCity()
    {
        // Randomly select a city prefab
        m_CityNumber = Random.Range(0, m_CitiesPrefabs.Length);

        // Instantiate the city at the specified position
        GameObject newCity = Instantiate(m_CitiesPrefabs[m_CityNumber], new Vector3(0, 0, m_Zpos), Quaternion.identity);

        // Add the instantiated city to the list
        m_Cities.Add(newCity);

        // Increment the Z position for the next city instantiation
        m_Zpos += 28;

        // Spawn props at specific positions and make them children of the city
        StartCoroutine(SpawnPropsWithDelay(newCity.transform, new Vector3(4.4f, 0f, 90.0f), new Vector3(-2.23f, 0f, 90.0f)));

        // Wait for a certain time before allowing the next city generation
        yield return new WaitForSeconds(m_TimeToGenerate);

        // Reset the flag to allow the next city generation
        m_CreatingCity = false;
    }

    // Coroutine to spawn props at specific positions with a delay between them
    private IEnumerator SpawnPropsWithDelay(Transform parent, Vector3 position1, Vector3 position2)
    {
        SpawnProp(parent, position1);

        // Wait for a certain time before spawning the next prop
        yield return new WaitForSeconds(Random.Range(5, m_PropSpawnDelay));

        // Apply Z-offset to the second prop position
        position2.z += zOffsetValue;

        SpawnProp(parent, position2);
    }

    // Function to spawn props at a specific position and make them children of the parent
    private void SpawnProp(Transform parent, Vector3 position)
    {
        // Instantiate the prop at the specified position and make it a child of the parent
        GameObject prop = Instantiate(m_PropPrefabs[Random.Range(0, m_PropPrefabs.Length)], position, Quaternion.identity);
        prop.transform.parent = parent;
    }

    [Header("City Settings")]
    [SerializeField] private GameObject[] m_CitiesPrefabs; // Array of city prefabs
    [SerializeField] private List<GameObject> m_Cities = new List<GameObject>(); // List to track instantiated cities
    [SerializeField] private float m_PropSpawnDelay; // Delay between prop spawns

    [Header("Player and Props")]
    [SerializeField] private Transform m_Player; // Reference to the player's transform
    [SerializeField] private GameObject[] m_PropPrefabs; // Array of prop prefabs
    [SerializeField] private float zOffsetValue = 10.0f; // Z-axis offset for prop spawning

    [Header("Generation Parameters")]
    [SerializeField] private float m_TimeToGenerate = 2.0f; // Time interval to generate a new city
    [SerializeField] private int m_MoveSpeed = 15; // Speed at which cities move

    // Initial position for city instantiation
    private float m_Zpos = 0.0f;

    // Flag to control city generation
    private bool m_CreatingCity;

    // Randomly selected city number
    private int m_CityNumber;


}
