using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Torque : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_vForce = Vector3.zero;
    [SerializeField]
    private Vector3 m_vCenterOfMass = Vector3.zero;
    [SerializeField]
    private Vector3 m_vForcePoint = Vector3.zero;

    private Vector3 m_vTorque = Vector3.zero;

    private Rigidbody m_rb = null;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.maxAngularVelocity = 50f;
    }

    private void FixedUpdate()
    {
        // get the cross product between our force vector and the vector from the center of mass and froce point
        m_vTorque = Vector3.Cross(m_vForce, m_vForcePoint - m_vCenterOfMass);
        m_rb.AddTorque(m_vTorque);
    }

    public void OnAccelerate(Vector3 vForce)
    {
        m_vForce = Vector3.Lerp(m_vForce, vForce, Time.deltaTime);
    }
}
