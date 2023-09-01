using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectIf : MonoBehaviour
{
    [SerializeField] public Button buttonComponent = null;

    GameObject Scroll;

    ScrollRect ScrollRect;
    StatusWindowItemDataBase statusWindowItemDataBase;

    float pos;

    CheckPos checkPos;

    float pos_1;
    float pos_2;
    float rect_1;
    float rect_2;

    bool isOne = true;

    void Start()
    {
        Scroll = GameObject.FindWithTag("Scroll");
        ScrollRect = Scroll.GetComponent<ScrollRect>();

        statusWindowItemDataBase = Camera.main.GetComponent<StatusWindowItemDataBase>();

        pos = 1f / ((float)(statusWindowItemDataBase.itemlist.Length - 9) - 7f);

        checkPos = GetComponent<CheckPos>();
        //Debug.Log("1 = " + checkPos.Select_1() + " / 2 = " + checkPos.Select_2());
    }

    public void RectPos(float rect_1,float rect_2)
    {
        //Debug.Log("1 = " + rect_1 + " / 2 = " + rect_2);
        pos_1 = rect_1;
        pos_2 = rect_2;
    }

    public void Update()
    {

        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if(EventSystem.current.currentSelectedGameObject.tag != "MenuButton")
            {
                if (isOne)//àÍâÒÇÃÇ›ë„ì¸
                {
                    rect_1 = (int)(pos_1 + 1);
                    rect_2 = (int)(pos_2 + 1 - 20);
                    //Debug.Log(rect_1 + " / " + rect_2);
                    isOne = false;
                }

                //ÉXÉNÉçÅ[Éãèàóù
                if (EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().position.y >= rect_1)
                {
                    ScrollRect.verticalNormalizedPosition = ScrollRect.verticalNormalizedPosition + pos;
                }
                else if (EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().position.y <= rect_2)
                {
                    ScrollRect.verticalNormalizedPosition = ScrollRect.verticalNormalizedPosition - pos;
                }
            }
        }
    }
}