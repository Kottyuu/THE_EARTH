using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Prologue_Text : MonoBehaviour
{
    [SerializeField]
    Text text;
    [SerializeField,Header("�e�L�X�g���X�g")]
    List<string> textList = new List<string>();
    [SerializeField,Header("�����̑���")]
    float Speed;
    int textListIndex = 0;

    int textcount;

    bool isNext;

    FadeScene fadeScene;

    [SerializeField]
    AudioClip clip;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        fadeScene = GetComponentInChildren<FadeScene>();
        StartCoroutine(TextStart());
    }

    private void Update()
    {
        Application.targetFrameRate = 60;
    }

    private IEnumerator TextStart()
    {
        isNext = false;
        textcount = 0;
        text.text = "";

        //�e�L�X�g�̍Ō�܂ňꕶ�����o��
        while (textList[textListIndex].Length > textcount)
        {
            text.text += textList[textListIndex][textcount];
            audioSource.PlayOneShot(clip);
            textcount++;
            yield return new WaitForSeconds(Speed);
        }

        isNext = true;
        textListIndex++;
    }

    public void OnStop(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (textListIndex < textList.Count)
            {
                if (isNext)
                {
                    StartCoroutine(TextStart());
                }
            }
            //���X�g�̍Ō�܂�
            else if (textListIndex == textList.Count)
            {
                //�V�[���J��
                Debug.Log("�V�[���J��");
                fadeScene.SceneChange("Main");
            }
        }
    }
}
