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
        [Header("解放条件(再生率)")]
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

    [SerializeField, Header("古代の岩")]
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

        //それぞれの場所のBGMを鳴らす
        if (!audioSource.isPlaying && !isSeEnd)
        {
            area[AreaNumber].BGM.Play();
            isSeEnd = true;
        }
        
        //終了メニューの場合は止める
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
    /// エリアチェンジ時にランダムで空とライトを変える
    /// </summary>
    public void SkyChange()
    {
        var randNum = Random.Range(0, sky.Length);
        RenderSettings.skybox = sky[randNum].skyboxes;

        _light.GetComponent<Light>().color = new Color(sky[randNum].lightColor.x / 255, sky[randNum].lightColor.y / 255, sky[randNum].lightColor.z / 255);
    }

    /// <summary>
    /// エリア解放
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
                nowRegRate += AreaNumberMax * 2;//少しだけ再生率をあげる
                AreaNumberMax++;
            }

            logText.text = area[AreaNumberMax].name + " ガ 解放 サレマシタ";
        }

        //100%になったら
        if(nowRegRate >= 100 && !releaseFantasy)
        {
            releaseFantasy = true;//一度だけ呼ぶ
            log.SetActive(true);
            fantasy_Log.SetActive(true);
            logText.text = "再生率 ガ 100% ニ ナリマシタ";
        }


        //古代の遺伝子解放
        if (totle >= 50 && !Ancient_Rock.activeSelf)//50体生成かつ解放されていなかったら
        {
            log.SetActive(true);
            Ancient_Rock.SetActive(true);
            logText.text = "??? ノ 遺伝子 ガ 解放 サレマシタ";
        }

        return nowRegRate;
    }
}
