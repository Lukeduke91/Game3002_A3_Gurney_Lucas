using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleTrap : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_vTorque;
    [SerializeField]
    private float m_fSpeed = 0f;
    [SerializeField]
    private float m_fMaxOffsetZ = 5f;
    [SerializeField]
    private float m_fMinOffsetZ = -5f;

    private float m_fDefaultX = 0f;
    private float m_fDefaultY = 0f;

    private Rigidbody m_rb = null;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.maxAngularVelocity = 50f;

        //cache initial y/z positions
        m_fDefaultX = transform.position.x;
        m_fDefaultY = transform.position.y;
        
    }

    private void FixedUpdate()
    {
        m_rb.AddTorque(m_vTorque);
    }

    private void Update()
    {
        // ping pong the position so the item move from 1 end to the other (back and forth)
        transform.position = new Vector3(m_fDefaultX, m_fDefaultY, Mathf.PingPong(Time.time * m_fSpeed, m_fMaxOffsetZ - m_fMinOffsetZ)
            + m_fMinOffsetZ);
    }
}
