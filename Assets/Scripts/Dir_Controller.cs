using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dir_Controller : MonoBehaviour
{
    [SerializeField]
    Transform camera_rot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rot = camera_rot.rotation;
        rot.x = 00.0f;
        rot.z = 00.0f;
        gameObject.transform.rotation = rot;
    }
}
