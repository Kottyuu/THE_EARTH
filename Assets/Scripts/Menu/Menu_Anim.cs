using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Anim : MonoBehaviour
{

    RectTransform rect;


    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        if (rect.localPosition.x < 0)
        {
            rect.localPosition += new Vector3(20, 0, 0);
        }
    }
    public void Close()
    {
        if (rect.localPosition.x > 900)
        {
            rect.localPosition -= new Vector3(20, 0, 0);
        }
    }
    private void OnEnable()
    {
        rect.localPosition = new Vector3(-900, 0, 0);
    }
}
