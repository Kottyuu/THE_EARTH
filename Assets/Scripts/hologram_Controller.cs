using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hologram_Controller : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    [Tooltip("x軸の回転角度")]
    private float rotateX = 0;

    [SerializeField]
    [Tooltip("y軸の回転角度")]
    private float rotateY = 0;

    [SerializeField]
    [Tooltip("z軸の回転角度")]
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
        // X,Y,Z軸に対してそれぞれ、指定した角度ずつ回転させている。
        // deltaTimeをかけることで、フレームごとではなく、1秒ごとに回転するようにしている。
        gameObject.transform.Rotate(new Vector3(rotateX, rotateY, rotateZ) * Time.deltaTime);

        timeCount += Time.deltaTime;

        // 指定時間の経過（条件）
        if (timeCount > chargeTime)
        {
            waitTime = Random.Range(0.1f, 0.3f);

            StartCoroutine(Wait(waitTime));

            chargeTime = Random.Range(1.0f, 5.0f);

            // タイムカウントを０に戻す
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
