using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class Screenshot : MonoBehaviour
{

    [Header("�ۑ���̐ݒ�")]
    [SerializeField]
    string folderName = "Screenshots";

    bool isCreatingScreenShot = false;
    string path;

    [SerializeField, Header("�V���b�^�[��")]
    private AudioClip audioClip;

    private AudioSource audioSource;

    void Start()
    {
        path = Application.dataPath + "/" + folderName + "/";
        audioSource = GetComponent<AudioSource>();
    }

    public void PrintScreen()
    {
        Debug.Log("�X�N���[���V���b�g�I�I�I�I");
        StartCoroutine("PrintScreenInternal");
    }


    /// <summary>
    /// �X�N���[���V���b�g
    /// </summary>
    /// <param name="context"></param>
    public void OnScreenShot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartCoroutine("PrintScreenInternal");
        }
    }



    IEnumerator PrintScreenInternal()
    {
        if (isCreatingScreenShot)
        {
            yield break;
        }

        isCreatingScreenShot = true;

        yield return null;

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string date = DateTime.Now.ToString("yy-MM-dd_HH-mm-ss");
        string fileName = path + date + ".png";

        Debug.Log("�X�N���[���V���b�g�I�I�I�I");
        audioSource.PlayOneShot(audioClip);
        ScreenCapture.CaptureScreenshot(fileName);

        yield return new WaitUntil(() => File.Exists(fileName));

        StartCoroutine("Interval");

    }

    IEnumerator Interval()
    {
        yield return new WaitForSeconds(1);

        isCreatingScreenShot = false;
    }

}