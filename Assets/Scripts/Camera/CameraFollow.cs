using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // used LateUpdate so camera starts after Target moves
    void LateUpdate()   
    {
        Vector3 finalposition = m_TargetTransform.position + m_Offset;
        //SmoothSpeed takes value between 0 and 1 if 0 than transform.position if 1 finalposition and everything in between
        //play with multiplying by delta time and see the diff
        Vector3 smoothedposition = Vector3.Lerp(transform.position, finalposition, m_SmoothSpeed * Time.deltaTime);
        transform.position = smoothedposition; 
    }


    [SerializeField] private Transform m_TargetTransform;
    [SerializeField] private float m_SmoothSpeed = 1.0f;
    [SerializeField] private Vector3 m_Offset;
}
