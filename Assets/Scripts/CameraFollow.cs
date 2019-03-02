using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    private Vector3 offset = new Vector3(0f, 0f, -10f);

    void Start()
    {
        
    }
    
    void Update()
    {
        transform.position = target.position + offset;
    }
}
