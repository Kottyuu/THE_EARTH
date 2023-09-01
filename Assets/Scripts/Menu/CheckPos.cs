using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CheckPos : MonoBehaviour
{
    public Button buttonComponent = null;
    GameObject P;
    GameObject P2;
    GameObject P3;
    GameObject Scroll;

    ScrollRect ScrollRect;

    public static int co;
    static int u;
    static int d;

    private float rect1;
    private float rect2;

    SelectIf selectIf;

    void Start()
    {
        P = transform.parent.gameObject;
        P2 = P.transform.parent.gameObject;
        P3 = P2.transform.parent.gameObject;
        Scroll = GameObject.FindWithTag("Scroll");
        ScrollRect = Scroll.GetComponent<ScrollRect>();

        selectIf = GetComponent<SelectIf>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().position);
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        }
        //ÉXÉNÉçÅ[ÉãÇÃè„å¿Ç∆â∫å¿
        rect1 = transform.parent.GetChild(0).GetChild(0).GetComponent<RectTransform>().position.y;
        rect2 = transform.parent.GetChild(6).GetChild(0).GetComponent<RectTransform>().position.y;
        //Debug.Log(rect1 + " / " + rect2);
        selectIf.RectPos(rect1, rect2);
    }
}