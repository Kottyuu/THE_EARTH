using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuitButtonEvent : MonoBehaviour
{
	//　自身の親のCanvasGroup
	private CanvasGroup canvasGroup;
	//　前の画面に戻るボタン
	private GameObject returnButton;

	private OperationStatusWindow operationStatus;

	void Awake()
	{
		operationStatus = Camera.main.GetComponent<OperationStatusWindow>();
		canvasGroup = GetComponentInParent<CanvasGroup>();
		returnButton = transform.parent.Find("No").gameObject;
	}

	void OnEnable()
	{
		//立ち上げ時に有効化する
		//GetComponent<Button>().interactable = true;
	}

	//　ボタンの上にマウスが入った時、またはキー操作で移動してきた時
	public void OnSelected()
	{
		if (canvasGroup == null || canvasGroup.interactable)
		{
			//　イベントシステムのフォーカスが他のゲームオブジェクトにある時このゲームオブジェクトにフォーカス
			if (EventSystem.current.currentSelectedGameObject != gameObject)
			{
				EventSystem.current.SetSelectedGameObject(gameObject);
			}

		}
	}
	//　ステータスウインドウを非アクティブにする
	public void DisableWindow()
	{
		if (canvasGroup == null || canvasGroup.interactable)
		{
			//　ウインドウを非アクティブにする
			transform.root.gameObject.SetActive(false);
			operationStatus.isQuitMenu = false;
			operationStatus.isMenu = false;
			Time.timeScale = 1.0f;
		}
	}
	//　前の画面に戻るボタンを選択状態にする
	public void SelectReturnButton()
	{
		EventSystem.current.SetSelectedGameObject(returnButton);
	}
	//ゲーム終了処理
	public void OnQuit()
    {
		Debug.Log("終了!!!");
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
	}
}
