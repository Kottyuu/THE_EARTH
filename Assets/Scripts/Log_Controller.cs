using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Log_Controller : MonoBehaviour
{
    RectTransform rect;
    private OperationStatusWindow _menu;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        _menu = Camera.main.GetComponent<OperationStatusWindow>();
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_menu.isMenu)
        {
            if (rect.localPosition.x < -260)
            {
                rect.localPosition += new Vector3(15, 0, 0);
            }
        }

        if(!_menu.isMenu)
        {
            rect.localPosition -= new Vector3(15, 0, 0);
            if (rect.localPosition.x <= -600)
            {
                //rect.localPosition = new Vector3(-600, 200, 0);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        rect.localPosition = new Vector3(-600, rect.localPosition.y, 0);
    }
}
