using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class Animal_Controller : MonoBehaviour
{
    private Quaternion AnimalRotate;    //生成Rotate
    private float AnimalScale;          //生成Scale

    [SerializeField, Header("informationText")]
    private Text information;

    [HideInInspector]
    public int TotalAnimal = 0; //総生成数

    private int[] count;       //それぞれの生成数
    private Sprite[] sprites;  //それぞれのスプライト

    [SerializeField, Header("ログ")]
    private GameObject Log;
    private Text text;

    [Header("動物の分類")]
    public string[] animalGroup;

    private bool isEnd;//無限ループ防止用

    public float regenerationRate = 0.00f;//再生率

    [SerializeField,Header("再生率テキスト")]
    private Text regenerationText;

    [SerializeField,Header("合成音")]
    AudioClip audioClip;
    [SerializeField,Header("失敗音")]
    AudioClip notSound;

    Area_Controller area_Controller;
    private OperationStatusWindow _menu;
    private StatusWindowItemDataBase dataBase;
    private StatusWindowStatus statusWindowStatus;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        area_Controller = GameObject.Find("Area").GetComponent<Area_Controller>();
        _menu = Camera.main.GetComponent<OperationStatusWindow>();
        dataBase = Camera.main.GetComponent<StatusWindowItemDataBase>();
        statusWindowStatus = Camera.main.GetComponent<StatusWindowStatus>();
        audioSource = GetComponent<AudioSource>();
        text = Log.GetComponentInChildren<Text>();
        count = new int[dataBase.itemlist.Length];
        sprites = new Sprite[dataBase.itemlist.Length];

        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i] = dataBase.itemlist[i].sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(!_menu.isMenu)
        //{
        //    Log.SetActive(false);
        //}
        //Generate(9);
        regenerationText.text = "再生率 : " + regenerationRate.ToString("f2") + "%";
        if(regenerationRate >= 100.0f)
        {
            regenerationRate = 100.0f;
        }
    }

    public void OncCheat(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            for (int i = 9; i < sprites.Length; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Generate(i, true);
                    statusWindowStatus.SetItemData(dataBase.itemlist[i].name, true);
                    statusWindowStatus.SetItemData(dataBase.itemlist[i - 9].name, true);
                }

                //for (int j = 0; j < 5; j++)
                //{
                //    Generate(i, true);
                //}
            }
        }
    }

    /// <summary>
    /// 動物生成
    /// </summary>
    /// <param name="number"></param>
    /// <param name="Pos"></param>
    public void Generate(int number,bool cheat)
    {
        var Prefab = dataBase.itemlist[number];
        var Probability = Random.Range(1, 11); //確率

        //statusWindowStatus.SetItemData(Prefab.name);

        if (Prefab.isElement)
        {
            statusWindowStatus.SetItemData(Prefab.name,false);
            information.text = Prefab.name + " ノ 元素 ガ 生成 サレマシタ";
            return;
        }

        if (Probability > Prefab.probability)//生成失敗
        {
            audioSource.PlayOneShot(notSound);
            information.text = "生成 ニ 失敗 シマシタ";
            return;
        }

        if(Prefab.group.ToString() == "Fantasy" && regenerationRate < 100)//神獣の場合、再生率が足りなかったら失敗
        {
            audioSource.PlayOneShot(notSound);
            information.text = "今 ノ再生率 デハ 生成 デキナイ";
            return;
        }

        AnimalRotate = Quaternion.Euler(0, Random.Range(-180f, 180), 0);    //生成Rotateランダム
        AnimalScale = Random.Range(0.8f, 1.5f);                             //生成Scaleランダム

        var area = AreaCheck(Prefab);

        int AnimalNumber = Random.Range(0, Prefab.animalPrefab.Length);

        regenerationRate += Random.Range(0.7f, 1.6f);//再生率
        //regenerationRate += 30.0f;

        TotalAnimal++;//総数プラス
        dataBase.itemlist[number].count++;//動物ごとの生成数プラス
        dataBase.itemlist[number].isMade = true;

        ///生成処理
        var Obj = Instantiate(Prefab.animalPrefab[AnimalNumber], area.Position, AnimalRotate);
        Obj.name = Prefab.sprite.name + Prefab.count.ToString();
        Obj.transform.localScale = Vector3.one * AnimalScale;

        if (!cheat)
        {
            audioSource.PlayOneShot(audioClip);
        }

        var areaName = Prefab.group.ToString() == "Fantasy" ? "？？？" : area.name;

        information.text = areaName + " ニ " + Prefab.name.ToString() + " ヲ 生成 シマシタ ";

        //ReleaseArea(dataBase, number);
        regenerationRate = area_Controller.ReleaseArea(regenerationRate,TotalAnimal);

    }


    /// <summary>
    /// 左右が同じ
    /// </summary>
    /// <param name="name"></param>
    private void SameGenerate(Sprite name)
    {
        for (int i = 0; i < dataBase.itemlist.Length; i++)
        {
            var Prefab = dataBase.itemlist[i];

            if (Prefab.sprite == name)
            {            
                if (Prefab.name == "草食" || Prefab.name == "肉食" || Prefab.name == "古代")
                {
                    audioSource.PlayOneShot(notSound);
                    information.text = "別 ノ 組ミ合ワセ ヲ 試シテミヨウ";
                    return;
                }

                audioSource.PlayOneShot(audioClip);
                Generate(i,false);

            }
        }
    }

    /// <summary>
    /// 存在しない組み合わせ
    /// </summary>
    private void NotGenerate(Sprite left, Sprite right)
    {
        audioSource.PlayOneShot(notSound);

        var target = "Element";

        if (left.name.Contains(target) || right.name.Contains(target))//Elementが含まれていたら
        {
            if (left.name.Contains(target) && right.name.Contains(target))//両方ともの場合
            {
                information.text = "別 ノ 組ミ合ワセ ヲ 試シテミヨウ";
            }
            else
            {
                information.text = "元素 ハ 元素同士 デナイト 無理 ソウダ";
            }
        }
        else
        {
            information.text = "別 ノ 組ミ合ワセ ヲ 試シテミヨウ";
        }
    }

    /// <summary>
    /// 生息地と生成場所
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    private (string name, Vector3 Position) AreaCheck(StatusWindowItemDataBase.ItemList prefab)
    {
        var area = prefab.areas;

        string areaName = "";

        //生成処理
        float PosX = Random.Range(-200, 200);
        float PosZ = Random.Range(200, -200);

        Vector3 Pos = new Vector3(PosX, 50, PosZ);

        int num = (int)area;

        Pos.x += 2500 * num;
        areaName = area_Controller.area[num].name;

        if(area_Controller.AreaNumberMax < num)
        {
            return ("???", Pos);
        }

        return (areaName, Pos);
    }

    /// <summary>
    /// 動物生成条件分岐
    /// </summary>
    /// <param name="L"></param>
    /// <param name="R"></param>
    public void GenerateConditions(Sprite leftSprite, Sprite rightSprite)
    {
        var animalList = dataBase.itemlist;
        isEnd = false;

        for (int i = 0; i < animalList.Length; i++)
        {
            if (animalList[i].synthesis.sy_1 == leftSprite)
            {
                if (animalList[i].synthesis.sy_2 == rightSprite)
                {
                    Generate(i,false);
                    return;
                }
            }

            if (i >= animalList.Length - 1)
            {
                if (isEnd)//入れ替えた後も存在しなければ
                {
                    if (leftSprite == rightSprite)//左右が同じだったら
                    {
                        SameGenerate(leftSprite);
                        return;
                    }
                    else//それでも違ったら
                    {
                        NotGenerate(leftSprite, rightSprite);
                        return;
                    }
                }

                isEnd = true;//無限ループ防止

                //入れ替え処理
                Sprite save = leftSprite;
                leftSprite = rightSprite;
                rightSprite = save;

                i = 0;//最初からもう一度
            }
        }
    }
}
