using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    private float m_fGateSpeed = 4f;
    [SerializeField]
    private int keycardRequirementLevel = 1;
    [SerializeField]
    private GameObject m_GateObj = null;

    private Vector3 m_vGateOpenPos;
    private Vector3 m_vGateClosedPos;

    enum GateStates
    {
        GateClosed,
        GateOpen
    }
    //allows users to check if door is opened or not
    private GateStates m_eGatestate = GateStates.GateClosed;
    
    //Sets the position of a closed and opened door
    private void Start()
    {
        m_vGateClosedPos = m_GateObj.transform.position;
        m_vGateOpenPos = m_vGateClosedPos + new Vector3(0f, 1.5f, 0f);
    }

    private void Update()
    {
        //Checks to see if the gate is open, move the gate up to let the player through
        if (m_eGatestate == GateStates.GateOpen)
        {
            m_GateObj.transform.position = Vector3.Lerp(m_GateObj.transform.position, m_vGateOpenPos, m_fGateSpeed * Time.deltaTime);
        }
        // other wise, keep it down
        else
        {
            m_GateObj.transform.position = Vector3.Lerp(m_GateObj.transform.position, m_vGateClosedPos, m_fGateSpeed * Time.deltaTime);
        }
    }
    //When the player enters the collision box, if it has the required level, it will open the closed
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("SphereLayer") && BallMovement.keycardZone >= keycardRequirementLevel)
        {
            m_eGatestate = GateStates.GateOpen;
        }
    }
    //When the player leaves or is out of the box, the gate is closed
    private void OnTriggerExit(Collider other)
    {
        m_eGatestate = GateStates.GateClosed;
    }
}
