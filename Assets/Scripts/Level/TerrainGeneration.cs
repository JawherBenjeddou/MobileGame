using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    // Initialization
    void Start()
    {
        // Check if there are no cities assigned to the array in the inspector
        if (m_Cities.Length == 0)
        {
            Debug.Log("No cities assigned to the array in the inspector");
        }

        // Check if no character is assigned in the inspector
        if (m_Character == null)
        {
            Debug.Log("No character is assigned in the inspector");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move cities along their local Z-axis
        foreach (var city in m_Cities)
        {
            // Set the new position for cities
            city.transform.position += city.transform.forward * m_MoveSpeed * Time.deltaTime;
        }

        // Move the terrain collider along its local Z-axis based on the first city's movement
        if (m_Cities.Length > 0)
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
            OnCharacterCollision(other);
        }
    }

    // Actions to perform when the character collides with the terrain
    private void OnCharacterCollision(Collider other)
    {
        // Log the name of the character collided with the terrain
        Debug.Log(other.name + " collided with terrain!");

        // Calculate the position for the next city
        Vector3 lastCityPosition = GetLastCityPosition();

        // Destroy the first city in the array
        Destroy(m_Cities[0]);

        // Instantiate a new city at the calculated position
        GameObject newCity = Instantiate(
            m_CityPrefab[Random.Range(0, m_CityPrefab.Count)],
            new Vector3(lastCityPosition.x + m_CitySpacingX, lastCityPosition.y +m_CitySpacingY, lastCityPosition.z + m_CitySpacingZ),
            Quaternion.identity
        );
        // Move the collider along the Z-axis only
        Vector3 colliderPosition = transform.position;
        colliderPosition.z += m_CitySpacingZ;
        transform.position = colliderPosition;

        // Shift the array elements to maintain continuity
        m_Cities[0] = m_Cities[1];
        m_Cities[1] = newCity;
    }

    // Get the position of the last city in the array
    private Vector3 GetLastCityPosition()
    {
        return m_Cities[m_Cities.Length - 1].transform.position;
    }

    // Serialized fields for inspector visibility and adjustments
    [SerializeField] private List<GameObject> m_CityPrefab = new List<GameObject>();
    [SerializeField] private GameObject[] m_Cities;
    [SerializeField] private GameObject m_Character;
    [SerializeField] private float m_MoveSpeed = 4.0f;
    [SerializeField] private float m_CitySpacingX;
    [SerializeField] private float m_CitySpacingY;
    [SerializeField] private float m_CitySpacingZ = 50.0f;
}
