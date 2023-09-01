using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Controller : MonoBehaviour
{
    private int textCount; // �q�I�u�W�F�N�g(Text)�̐�
    private Text[] LogText;
    private Image[] LogImage;
    private textProperty[] textProperty;
    [SerializeField] float fadeoutTime = 1f; // ���S�ɓ����ɂȂ�܂łɂ����鎞��(�b)
    [SerializeField] float fadeoutStartTime = 5f; // ���������n�܂�܂łɂ����鎞��(�b)

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
        // ��ԏ�̃e�L�X�g�͋����I�ɓ������J�n������
        if (textProperty[0].Alfa == 1)
        {
            textProperty[0].ElapsedTime = fadeoutStartTime;
        }

        for (int i = textCount - 1; i >= 0; i--)
        {
            if (textProperty[i].Alfa > 0)
            {
                // �o�ߎ��Ԃ�fadeoutStartTime�����Ȃ玞�Ԃ��J�E���g
                // �����łȂ���Γ����x��������
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
    // ���O�o��
    public void OutputLog(string itemName , bool isAnimal)
    {
        if (textProperty[textCount - 1].Alfa > 0)
        {
            UplogText();
        }
        rect.localPosition = new Vector3(180,-38,0);
        LogText[textCount - 1].text = itemName + " �m " + (isAnimal || itemName == "���H" || itemName == "���H" || itemName == "�Ñ�" ? "��`�q" : "���f") + " �� ���� �V�}�V�^";
        ResetTextPropety();
    }
    
    // ���O�����ɂ��炷
    private void UplogText()
    {
        // �Â��ق����炸�炷
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
    // ���O�̏�����
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
    public float Alfa // �����x�A0�����Ȃ�0�ɂ���
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
    public float ElapsedTime { get; set; } // ���O���o�͂���Ă���̌o�ߎ���
}