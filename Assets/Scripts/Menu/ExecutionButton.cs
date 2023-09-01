using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExecutionButton : MonoBehaviour
{

    [SerializeField,Header("���̃{�^��")]
    private Button Synthesis_1;
    [SerializeField,Header("�E�̃{�^��")]
    private Button Synthesis_2;

    [SerializeField,Header("SynthesisItemArea")]
    private GameObject SynthesisItemArea;

    [SerializeField,Header("�����{�^��Sprite")]
    private Sprite BackGround;

    [Header("�����e�L�X�g")]
    public Text text;

    private Transform menuArea;
    //�@�߂�{�^��
    private GameObject returnButton;

    [HideInInspector]
    public bool isSynth;            //�����{�^���������ꂽ���ǂ���

    [SerializeField,Header("�I����")]
    GameObject selectImage;

    private int LeftStock;      //���E�ɃZ�b�g����Ă���A�C�e���̐�
    private int RightStock;

    Animal_Controller _animal_controller;
    private SynthesisButton synthesis_1;
    private SynthesisButton synthesis_2;
    private CreateSynthesisButton createSynthesis;
    private Sound_Controller sound_controller;

    // Start is called before the first frame update
    void Awake()
    {
        _animal_controller = GameObject.Find("Animals").GetComponent<Animal_Controller>();
        sound_controller = GameObject.Find("Sound_Controller").GetComponent<Sound_Controller>();
        menuArea = transform.parent.parent.parent.Find("MenuArea");
        returnButton = menuArea.Find("Exit").gameObject;
        synthesis_1 = Synthesis_1.GetComponent<SynthesisButton>();
        synthesis_2 = Synthesis_2.GetComponent<SynthesisButton>();
        createSynthesis = SynthesisItemArea.GetComponent<CreateSynthesisButton>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelect()
    {
        selectImage.SetActive(true);
    }


    public void OnClick()
    {
        Sprite LeftSprite = Synthesis_1.GetComponent<Image>().sprite;   //���E�̃X�v���C�g�Z�b�g
        Sprite RightSprite = Synthesis_2.GetComponent<Image>().sprite;
        isSynth = true;


        //Debug.Log("L" + LeftStock + "R" + RightStock);

        //��������
        if ((LeftSprite.name == "UISprite" || RightSprite.name == "UISprite"))
        {
            sound_controller.NotPossible();
            text.text = "�X���b�g �j �A�C�e�� �� �����e �N�_�T�C";
            return;
        }
        else
        {
            //�������炷
            if (LeftSprite == RightSprite)
            {
                RightStock = synthesis_2.Stock - 2;     //�����Ȃ̂ŕЕ���-2����
            }
            else
            {
                LeftStock = synthesis_1.Stock - 1;      //���E�̃{�^���ɃZ�b�g����Ă������1�����炷��
                RightStock = synthesis_2.Stock - 1;
            }
            synthesis_1.statusWindowItemData.itemStock = LeftStock;     //���炵�������A�C�e���ɂ����f������
            synthesis_2.statusWindowItemData.itemStock = RightStock;

            //Debug.Log("������");
            text.text = "";

            _animal_controller.GenerateConditions(LeftSprite,RightSprite);//��������

        }
        Synthesis_1.GetComponent<Image>().sprite = BackGround;  //���E�̃X�v���C�g��������
        Synthesis_2.GetComponent<Image>().sprite = BackGround;

        createSynthesis.SetItem();
    }

    //�@SynthesisItemButton����ړ�����������폜
    public void OnDeselected()
    {
        text.text = "";
        selectImage.SetActive(false);
    }

    //�@�O�̉�ʂɖ߂�{�^����I����Ԃɂ���
    public void SelectReturnButton()
    {
        EventSystem.current.SetSelectedGameObject(returnButton);
    }


}
