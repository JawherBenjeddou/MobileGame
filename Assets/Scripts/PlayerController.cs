using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()    
    {
        m_rb = GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    void Update()
    {
        // Get the joystick input
        float horizontalInput = m_Joystick.Horizontal;
        float verticalInput = m_Joystick.Vertical;

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        m_rb.MovePosition(m_rb.position + movement * m_MoveSpeed * Time.deltaTime);
    }
    [SerializeField] private FixedJoystick m_Joystick;
    [SerializeField] private float m_MoveSpeed = 5f;
    private Rigidbody m_rb;
}
