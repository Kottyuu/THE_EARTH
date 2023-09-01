using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class Animal_Controller : MonoBehaviour
{
    private Quaternion AnimalRotate;    //����Rotate
    private float AnimalScale;          //����Scale

    [SerializeField, Header("informationText")]
    private Text information;

    [HideInInspector]
    public int TotalAnimal = 0; //��������

    private int[] count;       //���ꂼ��̐�����
    private Sprite[] sprites;  //���ꂼ��̃X�v���C�g

    [SerializeField, Header("���O")]
    private GameObject Log;
    private Text text;

    [Header("�����̕���")]
    public string[] animalGroup;

    private bool isEnd;//�������[�v�h�~�p

    public float regenerationRate = 0.00f;//�Đ���

    [SerializeField,Header("�Đ����e�L�X�g")]
    private Text regenerationText;

    [SerializeField,Header("������")]
    AudioClip audioClip;
    [SerializeField,Header("���s��")]
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
        regenerationText.text = "�Đ��� : " + regenerationRate.ToString("f2") + "%";
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
    /// ��������
    /// </summary>
    /// <param name="number"></param>
    /// <param name="Pos"></param>
    public void Generate(int number,bool cheat)
    {
        var Prefab = dataBase.itemlist[number];
        var Probability = Random.Range(1, 11); //�m��

        //statusWindowStatus.SetItemData(Prefab.name);

        if (Prefab.isElement)
        {
            statusWindowStatus.SetItemData(Prefab.name,false);
            information.text = Prefab.name + " �m ���f �K ���� �T���}�V�^";
            return;
        }

        if (Probability > Prefab.probability)//�������s
        {
            audioSource.PlayOneShot(notSound);
            information.text = "���� �j ���s �V�}�V�^";
            return;
        }

        if(Prefab.group.ToString() == "Fantasy" && regenerationRate < 100)//�_�b�̏ꍇ�A�Đ���������Ȃ������玸�s
        {
            audioSource.PlayOneShot(notSound);
            information.text = "�� �m�Đ��� �f�n ���� �f�L�i�C";
            return;
        }

        AnimalRotate = Quaternion.Euler(0, Random.Range(-180f, 180), 0);    //����Rotate�����_��
        AnimalScale = Random.Range(0.8f, 1.5f);                             //����Scale�����_��

        var area = AreaCheck(Prefab);

        int AnimalNumber = Random.Range(0, Prefab.animalPrefab.Length);

        regenerationRate += Random.Range(0.7f, 1.6f);//�Đ���
        //regenerationRate += 30.0f;

        TotalAnimal++;//�����v���X
        dataBase.itemlist[number].count++;//�������Ƃ̐������v���X
        dataBase.itemlist[number].isMade = true;

        ///��������
        var Obj = Instantiate(Prefab.animalPrefab[AnimalNumber], area.Position, AnimalRotate);
        Obj.name = Prefab.sprite.name + Prefab.count.ToString();
        Obj.transform.localScale = Vector3.one * AnimalScale;

        if (!cheat)
        {
            audioSource.PlayOneShot(audioClip);
        }

        var areaName = Prefab.group.ToString() == "Fantasy" ? "�H�H�H" : area.name;

        information.text = areaName + " �j " + Prefab.name.ToString() + " �� ���� �V�}�V�^ ";

        //ReleaseArea(dataBase, number);
        regenerationRate = area_Controller.ReleaseArea(regenerationRate,TotalAnimal);

    }


    /// <summary>
    /// ���E������
    /// </summary>
    /// <param name="name"></param>
    private void SameGenerate(Sprite name)
    {
        for (int i = 0; i < dataBase.itemlist.Length; i++)
        {
            var Prefab = dataBase.itemlist[i];

            if (Prefab.sprite == name)
            {            
                if (Prefab.name == "���H" || Prefab.name == "���H" || Prefab.name == "�Ñ�")
                {
                    audioSource.PlayOneShot(notSound);
                    information.text = "�� �m �g�~�����Z �� ���V�e�~���E";
                    return;
                }

                audioSource.PlayOneShot(audioClip);
                Generate(i,false);

            }
        }
    }

    /// <summary>
    /// ���݂��Ȃ��g�ݍ��킹
    /// </summary>
    private void NotGenerate(Sprite left, Sprite right)
    {
        audioSource.PlayOneShot(notSound);

        var target = "Element";

        if (left.name.Contains(target) || right.name.Contains(target))//Element���܂܂�Ă�����
        {
            if (left.name.Contains(target) && right.name.Contains(target))//�����Ƃ��̏ꍇ
            {
                information.text = "�� �m �g�~�����Z �� ���V�e�~���E";
            }
            else
            {
                information.text = "���f �n ���f���m �f�i�C�g ���� �\�E�_";
            }
        }
        else
        {
            information.text = "�� �m �g�~�����Z �� ���V�e�~���E";
        }
    }

    /// <summary>
    /// �����n�Ɛ����ꏊ
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    private (string name, Vector3 Position) AreaCheck(StatusWindowItemDataBase.ItemList prefab)
    {
        var area = prefab.areas;

        string areaName = "";

        //��������
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
    /// ����������������
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
                if (isEnd)//����ւ���������݂��Ȃ����
                {
                    if (leftSprite == rightSprite)//���E��������������
                    {
                        SameGenerate(leftSprite);
                        return;
                    }
                    else//����ł��������
                    {
                        NotGenerate(leftSprite, rightSprite);
                        return;
                    }
                }

                isEnd = true;//�������[�v�h�~

                //����ւ�����
                Sprite save = leftSprite;
                leftSprite = rightSprite;
                rightSprite = save;

                i = 0;//�ŏ����������x
            }
        }
    }
}
