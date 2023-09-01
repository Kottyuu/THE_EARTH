using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookData : MonoBehaviour
{

    [SerializeField]
    Text informationText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        //ç≈èâÇÃèÛë‘
        informationText.text = "";
        transform.GetChild(0).GetComponent<Image>().enabled = false;
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Text>().text = "";
        }
    }
}
