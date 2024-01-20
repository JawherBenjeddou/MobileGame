using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    // Initialization
    void Start()
    {
        // Check if there are no cities assigned to the array in the inspector
        if (m_Cities.Count == 0)
        {
            Debug.Log("No cities assigned to the array in the inspector");
        }

        // Check if no character is assigned in the inspector
        if (m_Character == null)
        {
            Debug.Log("No character is assigned in the inspector");
        }

        // Calculate the starting position for the cities
        Vector3 startPosition = Vector3.zero;

        // Instantiate cities at the calculated position with spacing along the Z-axis
        for (int i = 0; i < m_CityPrefab.Count; i++)
        {
            GameObject newCity = Instantiate(m_CityPrefab[i], startPosition, Quaternion.identity);
            m_Cities.Add(newCity);
            startPosition.z += m_CitySpacingZ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move cities along their local Z-axis
        foreach (var city in m_Cities)
        {
            city.transform.position += Vector3.forward * m_MoveSpeed * Time.deltaTime;
        }

        // Move the collider along its Z-axis based on the first city's movement
        if (m_Cities.Count > 0)
        {
            transform.position += m_Cities[0].transform.forward * m_MoveSpeed * Time.deltaTime;
        }
    }

    // Triggered when a collider enters this object's trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider that entered is the character collider
        if (other.gameObject == m_Character)
        {
            // Perform actions for character collision
            OnCharacterCollision();
        }
    }

    // Actions to perform when the character collides with the terrain
    private void OnCharacterCollision()
    {
        // Move the collider along the Z-axis only
        Vector3 colliderPosition = transform.position;
        colliderPosition.z += m_CitySpacingZ;
        transform.position = colliderPosition;

        // Destroy the oldest city
        if (m_Cities.Count > 0)
        {
            Destroy(m_Cities[0]);
            m_Cities.RemoveAt(0);
        }

        // Calculate the position for the new city
        colliderPosition.z += m_CitySpacingZ * (m_Cities.Count - 1);

        // Instantiate a new city
        GameObject newCity = Instantiate(m_CityPrefab[Random.Range(0, m_CityPrefab.Count)], colliderPosition, Quaternion.identity);
        m_Cities.Add(newCity);
    }

    // List to store instantiated cities
    [SerializeField] private List<GameObject> m_Cities = new List<GameObject>();

    // Prefabs for cities to be instantiated
    [SerializeField] private List<GameObject> m_CityPrefab = new List<GameObject>();

    // Reference to the character GameObject
    [SerializeField] private GameObject m_Character;

    // Speed at which cities move
    [SerializeField] private float m_MoveSpeed = 4.0f;

    // Spacing between cities along the Z-axis
    [SerializeField] private float m_CitySpacingZ = 50.0f;

}
