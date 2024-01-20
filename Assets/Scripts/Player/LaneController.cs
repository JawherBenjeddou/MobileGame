using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LaneController : MonoBehaviour
{
    private Rigidbody m_rb;

    // Start is called before the first frame update
    void Start()
    {
        InitializeComponents();
    }

    // Initialize necessary components
    void InitializeComponents()
    {
        m_rb = GetComponent<Rigidbody>();
        if (m_rb == null)
        {
            Debug.Log("RigidBody does not exist");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // No logic in Update for now
    }

    //// FixedUpdate is used for physics-related updates
    //void FixedUpdate()
    //{
    //    MoveCharacterForward();
    //}

    // Move the character forward using physics
    void MoveCharacterForward()
    {
        // Vector representing forward movement
        Vector3 forwardMovement = transform.forward * m_ForwardSpeed * Time.fixedDeltaTime;

        // Move the character using Rigidbody
        m_rb.MovePosition(m_rb.position + forwardMovement);
    }

    [SerializeField] private float m_ForwardSpeed = 5f;
}
