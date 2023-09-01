using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sound_Controller : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip selectSE;
    [SerializeField]
    private AudioClip notPossibleSE;

    private OperationStatusWindow operationStatus;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        operationStatus = Camera.main.GetComponent<OperationStatusWindow>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (operationStatus.isMenu)
            {
                //Debug.Log("Sound!");
                //audioSource.PlayOneShot(selectSE);
            }
        }
    }
    public void Select()
    {
        audioSource.PlayOneShot(selectSE);
    }

    public void NotPossible()
    {
        audioSource.PlayOneShot(notPossibleSE);
    }
}
