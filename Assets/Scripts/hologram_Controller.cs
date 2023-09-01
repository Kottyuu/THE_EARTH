using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hologram_Controller : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    [Tooltip("xŽ²‚Ì‰ñ“]Šp“x")]
    private float rotateX = 0;

    [SerializeField]
    [Tooltip("yŽ²‚Ì‰ñ“]Šp“x")]
    private float rotateY = 0;

    [SerializeField]
    [Tooltip("zŽ²‚Ì‰ñ“]Šp“x")]
    private float rotateZ = 0;

    public float noise = 5;

    [SerializeField]
    Material material;

    [SerializeField]
    AudioClip audioClip;

    private AudioSource audioSource;

    private float chargeTime;
    private float timeCount;
    private float waitTime;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        chargeTime = Random.Range(1.0f, 2.0f);
        noise = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // X,Y,ZŽ²‚É‘Î‚µ‚Ä‚»‚ê‚¼‚êAŽw’è‚µ‚½Šp“x‚¸‚Â‰ñ“]‚³‚¹‚Ä‚¢‚éB
        // deltaTime‚ð‚©‚¯‚é‚±‚Æ‚ÅAƒtƒŒ[ƒ€‚²‚Æ‚Å‚Í‚È‚­A1•b‚²‚Æ‚É‰ñ“]‚·‚é‚æ‚¤‚É‚µ‚Ä‚¢‚éB
        gameObject.transform.Rotate(new Vector3(rotateX, rotateY, rotateZ) * Time.deltaTime);

        timeCount += Time.deltaTime;

        // Žw’èŽžŠÔ‚ÌŒo‰ßiðŒj
        if (timeCount > chargeTime)
        {
            waitTime = Random.Range(0.1f, 0.3f);

            StartCoroutine(Wait(waitTime));

            chargeTime = Random.Range(1.0f, 5.0f);

            // ƒ^ƒCƒ€ƒJƒEƒ“ƒg‚ð‚O‚É–ß‚·
            timeCount = 0;
        }



        material.SetFloat("Noise",noise);

    }

    IEnumerator Wait(float waitTime)
    {
        noise = Random.Range(-0.08f, 0.08f);

        audioSource.volume = Mathf.Abs(noise * 10);

        audioSource.PlayOneShot(audioClip);
        yield return new WaitForSeconds(waitTime);
        noise = 0.0f;
    }
}
