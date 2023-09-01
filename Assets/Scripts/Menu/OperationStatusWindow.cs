
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OperationStatusWindow : MonoBehaviour
{

	[SerializeField]
	private GameObject propertyWindow;

	[SerializeField]
	private GameObject quitWindow;

	[SerializeField]
	private GameObject operateWindow;
	//　ステータスウインドウの全部の画面
	[SerializeField]
	private GameObject[] windowLists;

	[SerializeField]
	private AudioClip decisionSe;
	[SerializeField]
	private AudioClip synthesisSe;

	[SerializeField,Header("BackGround")]
	RectTransform rect;

	[SerializeField]
	Transform aaa;

	public bool isMenu = false; //メニューが開いているか
	public bool isQuitMenu = false; //終了メニューが開いているか
	public bool isPossible = false;  //メニューが開けるかどうか

	AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
		isPossible = false;
	}

    void Update()
	{
		//　ステータスウインドウのオン・オフ
		//if (Input.GetButtonDown("Start"))
		//{
		//	propertyWindow.SetActive(!propertyWindow.activeSelf);
		//	//　MainWindowをセット
		//	ChangeWindow(windowLists[0]);
		//}

		Debug.Log("メニュー" + isMenu);

		if(isMenu)
		{
			Time.timeScale = 0.0f;
			rect.localPosition += new Vector3(100, 0, 0);
			if (rect.localPosition.x > 0)
			{
				rect.localPosition = new Vector3(0, 0, 0);
			}
		}
		else if(!isMenu)
		{
			rect.localPosition -= new Vector3(100, 0, 0);
			if (rect.localPosition.x < -900)
			{
				rect.localPosition = new Vector3(-900, 0, 0);
				propertyWindow.SetActive(false);
			}
			Time.timeScale = 1.0f;
		}
	}

	//　ステータス画面のウインドウのオン・オフメソッド
	public void ChangeWindow(GameObject window,int number)
	{
		foreach (var item in windowLists)
		{
			if (item == window)
			{
				item.SetActive(true);
				EventSystem.current.SetSelectedGameObject(null);
			}
			else
			{
				item.SetActive(false);
				Time.timeScale = 1.0f;
			}

			if (number != 3)//操作方法メニュー以外
			{
				//　それぞれのウインドウのMenuAreaの最初の子要素をアクティブな状態にする
				EventSystem.current.SetSelectedGameObject(window.transform.Find("MenuArea").GetChild(0).gameObject);
            }
            //else
            //{
            //	EventSystem.current.SetSelectedGameObject(window.transform.Find("MenuArea").GetChild(0).gameObject);
            //	//EventSystem.current.SetSelectedGameObject(aaa.GetChild(0).gameObject);
            //}
        }
	}

	public void OnQuitMenu(InputAction.CallbackContext context)
	{
		if(!isMenu)
		{
			if (isPossible)
			{
				if (context.performed)
				{
					quitWindow.SetActive(!quitWindow.activeSelf);
					ChangeWindow(windowLists[2],2);

					if (quitWindow.activeSelf)
					{
						isMenu = true;
						isQuitMenu = true;
					}
					else
					{
						isMenu = false;
						isQuitMenu = false;

					}
				}
			}
		}
	}

	public void OnMenu(InputAction.CallbackContext context)
	{
		if (!isQuitMenu)
		{
			if (isPossible)
			{
				if (context.performed)
				{
					if (!isMenu)
					{
						propertyWindow.SetActive(true);
						//　MainWindowをセット
						ChangeWindow(windowLists[0], 0);
						isMenu = true;
					}
					else
					{
						if (operateWindow.activeSelf)
						{
							ChangeWindow(windowLists[0], 0);
						}
						else
						{
							isMenu = false;
						}
					}
				}
			}
		}
	}

	public void OnOperateMenu(InputAction.CallbackContext context)
    {
		if (!isQuitMenu)
		{
			if (isPossible)
			{
				if (context.performed)
				{
					if (!isMenu)
					{
						propertyWindow.SetActive(true);
						//　MainWindowをセット
						ChangeWindow(windowLists[3], 3);
						isMenu = true;
					}
					else
					{
						if (operateWindow.activeSelf)
						{
							isMenu = false;
						}
					}
				}
			}
		}
	}

	public void SynthesisSe()
    {
		audioSource.PlayOneShot(synthesisSe);
	}
}
