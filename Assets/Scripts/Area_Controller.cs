using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Area_Controller : MonoBehaviour
{
    [System.Serializable]
    public struct Area
    {
        public string name;
        public AudioSource BGM;
        public AudioClip footSteps;
        [Header("�������(�Đ���)")]
        public float regenerationRate;
    }

    public Area[] area;

    [SerializeField]
    private AudioClip titleSe;

    [HideInInspector]
    public int AreaNumber;
    [HideInInspector]
    public int AreaNumberMax;

    public Text Areatext;

    private bool isSeEnd;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private GameObject log;
    [SerializeField]
    private GameObject fantasy_Log;
    private Text logText;

    private bool releaseFantasy;

    [SerializeField, Header("�Ñ�̊�")]
    private GameObject Ancient_Rock;

    [System.Serializable]
    public struct Sky
    {
        public Material skyboxes;
        public Vector3 lightColor;
    }

    [SerializeField]
    Sky[] sky;

    [SerializeField]
    GameObject _light;

    OperationStatusWindow operationStatus;

    // Start is called before the first frame update
    void Start()
    {
        Areatext.text = area[AreaNumber].name;
        logText = log.GetComponentInChildren<Text>();
        operationStatus = Camera.main.GetComponent<OperationStatusWindow>();
        releaseFantasy = false;
        Sounds();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(area[AreaNumber].name);
        //Debug.Log("AreaNumberMax" + AreaNumberMax);

        //���ꂼ��̏ꏊ��BGM��炷
        if (!audioSource.isPlaying && !isSeEnd)
        {
            area[AreaNumber].BGM.Play();
            isSeEnd = true;
        }
        
        //�I�����j���[�̏ꍇ�͎~�߂�
        if(operationStatus.isQuitMenu)
        {
            area[AreaNumber].BGM.Pause();
        }
        else
        {
            area[AreaNumber].BGM.UnPause();

        }
    }

    public void Sounds()
    {
        isSeEnd = false;

        audioSource.PlayOneShot(titleSe);
        area[AreaNumber].BGM.Stop();
    }

    /// <summary>
    /// �G���A�`�F���W���Ƀ����_���ŋ�ƃ��C�g��ς���
    /// </summary>
    public void SkyChange()
    {
        var randNum = Random.Range(0, sky.Length);
        RenderSettings.skybox = sky[randNum].skyboxes;

        _light.GetComponent<Light>().color = new Color(sky[randNum].lightColor.x / 255, sky[randNum].lightColor.y / 255, sky[randNum].lightColor.z / 255);
    }

    /// <summary>
    /// �G���A���
    /// </summary>
    /// <param name="nowRegRate"></param>
    /// <param name="totle"></param>
    /// <returns></returns>
    public float ReleaseArea(float nowRegRate,int totle)
    {
        log.SetActive(false);

        if (AreaNumberMax < area.Length - 1)
        {
            if (area[AreaNumberMax + 1].regenerationRate <= nowRegRate)
            {
                log.SetActive(true);
                nowRegRate += AreaNumberMax * 2;//���������Đ�����������
                AreaNumberMax++;
            }

            logText.text = area[AreaNumberMax].name + " �K ��� �T���}�V�^";
        }

        //100%�ɂȂ�����
        if(nowRegRate >= 100 && !releaseFantasy)
        {
            releaseFantasy = true;//��x�����Ă�
            log.SetActive(true);
            fantasy_Log.SetActive(true);
            logText.text = "�Đ��� �K 100% �j �i���}�V�^";
        }


        //�Ñ�̈�`�q���
        if (totle >= 50 && !Ancient_Rock.activeSelf)//50�̐������������Ă��Ȃ�������
        {
            log.SetActive(true);
            Ancient_Rock.SetActive(true);
            logText.text = "??? �m ��`�q �K ��� �T���}�V�^";
        }

        return nowRegRate;
    }
}
