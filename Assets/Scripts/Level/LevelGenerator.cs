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
            if (city.transform.position.z < (player.position.z - 15))
            {
                // Destroy the city
                Destroy(city);

                // Remove it from the list
                m_Cities.RemoveAt(i);
            }
        }
    }

    // Coroutine to generate a new city after a specified time interval
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

        // Wait for a certain time before allowing the next city generation
        yield return new WaitForSeconds(m_TimeToGenerate);

        // Reset the flag to allow the next city generation
        m_CreatingCity = false;
    }
    // Array of city prefabs to be instantiated
    public GameObject[] m_CitiesPrefabs;

    // List to keep track of instantiated cities
    public List<GameObject> m_Cities = new List<GameObject>();

    // Initial position for city instantiation
    private float m_Zpos = 0.0f;

    // Flag to control city generation
    public bool m_CreatingCity;

    // Randomly selected city number
    public int m_CityNumber;

    // Time interval to generate a new city
    public float m_TimeToGenerate;

    // Speed at which cities move
    public int m_MoveSpeed;

    // Reference to the player's transform for position comparison
    public Transform player;
}