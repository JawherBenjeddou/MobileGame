using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       if(m_Cities.Length == 0) { Debug.Log("No Citites assigned to the array in the inspector"); }
       if(m_Character == null) { Debug.Log("No Character is assigned in the inspector"); }
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

        // Move the collider along its local Z-axis based on the first city's movement
        if (m_Cities.Length > 0)
        {
           // m_ForwardMovement = m_Cities[0].transform.forward * m_MoveSpeed * Time.deltaTime;
            transform.position += m_Cities[0].transform.forward * m_MoveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider that entered is the character collider
        if (other.gameObject == m_Character)
        {
            OnCharacterCollision(other);
        }
    }

    private void OnCharacterCollision(Collider other)
    {
        // This method will be called when the character collides with the terrain
        Debug.Log(other.name + " collided with terrain!");

        // Instantiate a new city prefab after the last city in the array
        GameObject newCity = Instantiate(m_CityPrefab, GetNextCityPosition(), Quaternion.identity);

        // Add the new city to the array
        AddCity(newCity);
    }

    private Vector3 GetNextCityPosition()
    {
        // Find the position of the last city in the array
        if (m_Cities.Length > 0)
        {
            // Get the position and forward direction of the last city
            Vector3 lastCityPosition = m_Cities[m_Cities.Length - 1].transform.position;
            Vector3 lastCityForward = m_Cities[m_Cities.Length - 1].transform.forward;

            // Calculate the next position based on the last city's position and forward direction
            return lastCityPosition + lastCityForward * m_CitySpacing;
        }
        else
        {
            // If no cities exist, start from the terrain's position
            return transform.position;
        }
    }

    private void AddCity(GameObject newCity)
    {
        // Create a new array with increased size
        GameObject[] newCities = new GameObject[m_Cities.Length + 1];

        // Copy existing cities to the new array
        for (int i = 0; i < m_Cities.Length; i++)
        {
            newCities[i] = m_Cities[i];
        }

        // Add the new city to the last position in the new array
        newCities[newCities.Length - 1] = newCity;

        // Update the m_Cities array
        m_Cities = newCities;
    }

    [SerializeField] private GameObject m_CityPrefab;
    [SerializeField] private GameObject[] m_Cities;
    [SerializeField] private GameObject m_Character;
    //uncomment when ForwardMov is needed
    //public Vector3 m_ForwardMovement;
    [SerializeField] private float m_MoveSpeed = 4.0f;
    [SerializeField] private float m_CitySpacing = 10.0f;
}
