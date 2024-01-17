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
            // Set the new position
            city.transform.position += city.transform.forward * m_MoveSpeed * Time.deltaTime;
            transform.position += transform.forward * m_MoveSpeed * Time.deltaTime;
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
        Debug.Log(other.name+" collided with terrain!");
    }
    [SerializeField] private float m_MoveSpeed = 1.0f;
    [SerializeField] private GameObject[] m_Cities;
    [SerializeField] private GameObject m_Character;
}
