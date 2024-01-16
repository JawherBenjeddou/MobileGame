using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
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
    }

    // FixedUpdate is used for physics-related updates
    void FixedUpdate()
    {
        // Move the character forward
        Vector3 forwardmovement = transform.forward * m_ForwardSpeed * Time.fixedDeltaTime;
        m_rb.MovePosition(m_rb.position + forwardmovement);

    }

    [SerializeField] private float m_ForwardSpeed = 5f;
    private Rigidbody m_rb;
}
