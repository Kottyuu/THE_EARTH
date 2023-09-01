using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarEnable : MonoBehaviour
{
    [SerializeField]
    GameObject HpBar;

    OperationStatusWindow operationStatusWindow;
    Collider col;

    // Start is called before the first frame update
    void Start()
    {
        HpBar.SetActive(false);
        operationStatusWindow = Camera.main.GetComponent<OperationStatusWindow>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        col.enabled = !operationStatusWindow.isMenu;

        if (operationStatusWindow.isMenu)
        {
            HpBar.SetActive(false);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            HpBar.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HpBar.SetActive(false);
        }
    }
}
