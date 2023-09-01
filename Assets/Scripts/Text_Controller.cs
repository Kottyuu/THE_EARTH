using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Controller : MonoBehaviour
{
    private int textCount; // 子オブジェクト(Text)の数
    private Text[] LogText;
    private Image[] LogImage;
    private textProperty[] textProperty;
    [SerializeField] float fadeoutTime = 1f; // 完全に透明になるまでにかかる時間(秒)
    [SerializeField] float fadeoutStartTime = 5f; // 透明化が始まるまでにかかる時間(秒)

    RectTransform rect;

    void Start()
    {
        textCount = transform.childCount;
        LogText = new Text[textCount];
        LogImage = new Image[textCount];
        textProperty = new textProperty[textCount];
        for (int i = 0; i < textCount; i++)
        {
            LogText[i] = transform.GetChild(i).GetChild(0).GetComponent<Text>();
            LogImage[i] = transform.GetChild(i).GetComponent<Image>();
            LogText[i].color = new Color(LogText[i].color.r, LogText[i].color.g, LogText[i].color.b, 0f);
            LogImage[i].color = LogText[i].color;
            textProperty[i].Alfa = 0f;
            textProperty[i].ElapsedTime = 0f;
        }
        rect = transform.GetChild(textCount-1).GetComponent<RectTransform>();
    }

    void Update()
    {
        // 一番上のテキストは強制的に透明化開始させる
        if (textProperty[0].Alfa == 1)
        {
            textProperty[0].ElapsedTime = fadeoutStartTime;
        }

        for (int i = textCount - 1; i >= 0; i--)
        {
            if (textProperty[i].Alfa > 0)
            {
                // 経過時間がfadeoutStartTime未満なら時間をカウント
                // そうでなければ透明度を下げる
                if (textProperty[i].ElapsedTime < fadeoutStartTime)
                {
                    textProperty[i].ElapsedTime += Time.deltaTime;
                }
                else
                {
                    textProperty[i].Alfa -= Time.deltaTime / fadeoutTime;
                    LogText[i].color = new Color(LogText[i].color.r, LogText[i].color.g, LogText[i].color.b,
                                       textProperty[i].Alfa);
                    LogImage[i].color = LogText[i].color;
                }
            }
            else
            {
                break;
            }
        }

        if (rect.localPosition.x > -90)
        {
            rect.localPosition -= new Vector3(15, 0, 0);
        }

    }
    // ログ出力
    public void OutputLog(string itemName , bool isAnimal)
    {
        if (textProperty[textCount - 1].Alfa > 0)
        {
            UplogText();
        }
        rect.localPosition = new Vector3(180,-38,0);
        LogText[textCount - 1].text = itemName + " ノ " + (isAnimal || itemName == "草食" || itemName == "肉食" || itemName == "古代" ? "遺伝子" : "元素") + " ヲ 入手 シマシタ";
        ResetTextPropety();
    }
    
    // ログを一つ上にずらす
    private void UplogText()
    {
        // 古いほうからずらす
        for (int i = 0; i < textCount - 1; i++)
        {
            LogText[i].text = LogText[i + 1].text;
            textProperty[i].Alfa = textProperty[i + 1].Alfa;
            textProperty[i].ElapsedTime = textProperty[i + 1].ElapsedTime;
            LogText[i].color = new Color(LogText[i].color.r, LogText[i].color.g, LogText[i].color.b,
                               textProperty[i].Alfa);
            LogImage[i].color = LogText[i].color;
        }
    }
    // ログの初期化
    private void ResetTextPropety()
    {
        textProperty[textCount - 1].Alfa = 1f;
        textProperty[textCount - 1].ElapsedTime = 0f;
        LogText[textCount - 1].color = new Color(LogText[textCount - 1].color.r, LogText[textCount - 1].color.g, LogText[textCount - 1].color.b,
                                       textProperty[textCount - 1].Alfa);
        LogImage[textCount - 1].color = LogText[textCount - 1].color;
    }
}

struct textProperty
{
    private float _alfa;
    public float Alfa // 透明度、0未満なら0にする
    {
        get
        {
            return _alfa;
        }
        set
        {
            _alfa = value < 0 ? 0 : value;
        }
    }
    public float ElapsedTime { get; set; } // ログが出力されてからの経過時間
}