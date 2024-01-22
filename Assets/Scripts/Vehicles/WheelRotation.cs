using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public float m_RotationSpeed = 200f; // Adjust the rotation speed as needed

    void Update()
    {
        // Rotate the wheel in the local X-axis
        transform.Rotate(Vector3.right * m_RotationSpeed * Time.deltaTime);
    }
}