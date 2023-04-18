using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
{
    [SerializeField]
    private List<Torque> m_wheelList = null;
    [SerializeField]
    private float m_fMaxAccelForce = 0f;
    [SerializeField]
    private float m_fMultiplier = 0f;
    [SerializeField]
    private float m_fDecelForce = 0f;

    private bool m_isGrounded = false;

    public static int keycardZone = 0;

    [SerializeField]
    private int JumperForce = 100;

    private Vector3 m_vForce = Vector3.zero;
    private void FixedUpdate()
    {
        UpdateWheels();

        //Right
        if (Input.GetKey(KeyCode.D))
        {
            m_vForce.x = Mathf.Lerp(m_vForce.x, m_fMaxAccelForce, Time.fixedDeltaTime * m_fMultiplier);
        }
        //jump
        if (Input.GetKey(KeyCode.Space) && m_isGrounded == true)
        {
            
            gameObject.GetComponent<Rigidbody>().AddExplosionForce(JumperForce, transform.position, 1,JumperForce);
        }
        //Left
        if (Input.GetKey(KeyCode.A))
        {
            m_vForce.x = Mathf.Lerp(m_vForce.x, -m_fMaxAccelForce, Time.fixedDeltaTime * m_fMultiplier);
        }

        m_vForce = Vector3.Lerp(m_vForce, Vector3.zero, Time.fixedDeltaTime * m_fDecelForce);
    }

    //send the force value
    private void UpdateWheels()
    {
        foreach (Torque wheel in m_wheelList)
        {
            wheel.OnAccelerate(m_vForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Checks to see if the player can jump
        if(collision.gameObject.tag == "Platform")
        {
            m_isGrounded = true;
            Debug.Log("Works!!");
        }
        
        if(collision.gameObject.tag == "Boost")
        {
            Debug.Log("Boost!!");
            //If the player enters a boost pad, boost them forward
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * 10.0f, ForceMode.Impulse);
        }

        if (collision.gameObject.tag == "Slowdown")
        {
            Debug.Log("Hold up!!");
            //Pushes player back
            m_vForce.x = -25f;
        }
        //gives player access to the next stage
        if (collision.gameObject.tag == "keycard")
        {
            keycardZone += 1;
            Debug.Log(keycardZone);
            Destroy(collision.gameObject);
        }
        //resets the game when they collide with a hazard,
        if (collision.gameObject.tag == "Hazard")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            keycardZone = 0;
        }
    }
    //Once they leave the platform, they won't be able to jump
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            m_isGrounded = false;
            Debug.Log("Works!!");
        }
    }

}
