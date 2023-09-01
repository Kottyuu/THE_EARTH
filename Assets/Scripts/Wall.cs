using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    GameObject obj;

    [SerializeField]
    private bool isSide;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            var pos = collision.gameObject.transform.position;
            pos.y += 5.0f;
            var rot = gameObject.transform.localEulerAngles;
            if(isSide)
            {
                pos.x = gameObject.transform.position.x;
            }
            else
            {
                pos.z = gameObject.transform.position.z;
            }
            var a = Instantiate(obj, pos, Quaternion.Euler(0, rot.y, 0));
            Destroy(a, 2.0f);
        }
    }
}
