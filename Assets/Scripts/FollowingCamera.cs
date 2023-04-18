using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField]
    public Transform m_targetTransform;

    private Vector3 vOffset;

    // Use this for initialization
    void Start()
    {
        //keep the distance between camera and object, moving the camera
        vOffset = transform.position - m_targetTransform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        //updates the canera position, following whatever the target transform
        transform.position = m_targetTransform.position + vOffset;
    }
}
