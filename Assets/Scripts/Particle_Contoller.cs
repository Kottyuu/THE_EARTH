using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Contoller : MonoBehaviour
{

    [SerializeField]
    GameObject particle;

    private Vector3 pos;
    private Vector3 scale;

    private bool isQuitting;

    private Vector3 ParScale;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnParticleSystemStopped()
    {
        pos = transform.position;
        scale = transform.parent.localScale;
        ParScale = new Vector3((transform.localScale.x / 2) * scale.x, (transform.localScale.y / 2) * scale.y, (transform.localScale.z / 2) * scale.z);
        Destroy(transform.parent.gameObject);
    }

    private void OnDestroy()
    {
        if (!isQuitting)
        {
            var par = Instantiate(particle, pos, Quaternion.identity);
            par.transform.localScale = ParScale;
        }
    }

}
