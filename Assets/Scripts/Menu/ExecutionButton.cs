using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExecutionButton : MonoBehaviour
{

    [SerializeField,Header("左のボタン")]
    private Button Synthesis_1;
    [SerializeField,Header("右のボタン")]
    private Button Synthesis_2;

    [SerializeField,Header("SynthesisItemArea")]
    private GameObject SynthesisItemArea;

    [SerializeField,Header("初期ボタンSprite")]
    private Sprite BackGround;

    [Header("合成テキスト")]
    public Text text;

    private Transform menuArea;
    //　戻るボタン
    private GameObject returnButton;

    [HideInInspector]
    public bool isSynth;            //合成ボタンが押されたかどうか

    [SerializeField,Header("選択時")]
    GameObject selectImage;

    private int LeftStock;      //左右にセットされているアイテムの数
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
        Sprite LeftSprite = Synthesis_1.GetComponent<Image>().sprite;   //左右のスプライトセット
        Sprite RightSprite = Synthesis_2.GetComponent<Image>().sprite;
        isSynth = true;


        //Debug.Log("L" + LeftStock + "R" + RightStock);

        //合成処理
        if ((LeftSprite.name == "UISprite" || RightSprite.name == "UISprite"))
        {
            sound_controller.NotPossible();
            text.text = "スロット ニ アイテム ヲ 入レテ クダサイ";
            return;
        }
        else
        {
            //個数を減らす
            if (LeftSprite == RightSprite)
            {
                RightStock = synthesis_2.Stock - 2;     //同じなので片方を-2する
            }
            else
            {
                LeftStock = synthesis_1.Stock - 1;      //左右のボタンにセットされている個数を1ずつ減らする
                RightStock = synthesis_2.Stock - 1;
            }
            synthesis_1.statusWindowItemData.itemStock = LeftStock;     //減らした個数をアイテムにも反映させる
            synthesis_2.statusWindowItemData.itemStock = RightStock;

            //Debug.Log("合成可");
            text.text = "";

            _animal_controller.GenerateConditions(LeftSprite,RightSprite);//合成処理

        }
        Synthesis_1.GetComponent<Image>().sprite = BackGround;  //左右のスプライトを初期化
        Synthesis_2.GetComponent<Image>().sprite = BackGround;

        createSynthesis.SetItem();
    }

    //　SynthesisItemButtonから移動したら情報を削除
    public void OnDeselected()
    {
        text.text = "";
        selectImage.SetActive(false);
    }

    //　前の画面に戻るボタンを選択状態にする
    public void SelectReturnButton()
    {
        EventSystem.current.SetSelectedGameObject(returnButton);
    }


}
