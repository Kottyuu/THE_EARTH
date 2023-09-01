using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Title_Controller : MonoBehaviour
{
    [SerializeField]
    Button beginningButton;
    [SerializeField]
    GameObject continueButton;
    [SerializeField]
    GameObject fadePanal;

    [SerializeField]
    AudioClip notPossible;

    [SerializeField]
    GameObject operatePanel;

    FadeScene fadeScene;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        fadeScene = fadePanal.GetComponent<FadeScene>();
        audioSource = GetComponent<AudioSource>();

        beginningButton.Select();
    }

    public void Beginning()
    {
        Save.SaveSystem.Instance.Reset();
        fadeScene.SceneChange("Prologue");
    }

    public void Continue()
    {
        if (!File.Exists(Save.SaveSystem.Instance.Path))
        {
            audioSource.PlayOneShot(notPossible);
            return;
        }
        fadeScene.SceneChange("Main");
    }

    public void OperatePanel()
    {
        operatePanel.SetActive(true);
    }
    public void OperatePanelClose()
    {
        operatePanel.SetActive(false);
    }
}
