using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEvent : MonoBehaviour
{

	//　インフォメーションテキストに表示する文字列
	[SerializeField]
	private string informationString;
	//　インフォメーションテキスト
	[SerializeField]
	private Text informationText;
	//　自身の親のCanvasGroup
	private CanvasGroup canvasGroup;
	//　前の画面に戻るボタン
	private GameObject returnButton;

	[SerializeField]
	GameObject selectImage;

	private OperationStatusWindow operationStatus;

	void Awake()
	{
		operationStatus = Camera.main.GetComponent<OperationStatusWindow>();
		canvasGroup = GetComponentInParent<CanvasGroup>();
		returnButton = transform.parent.Find("Exit").gameObject;
	}

	void OnEnable()
	{
		//　装備アイテム選択中にステータス画面を抜けた時にボタンが無効化したままの場合もあるので立ち上げ時に有効化する
		GetComponent<Button>().interactable = true;
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

			selectImage.SetActive(true);
			informationText.text = informationString;
		}
	}
	//　ボタンから移動したら情報を削除
	public void OnDeselected()
	{
		selectImage.SetActive(false);
		informationText.text = "";
	}

	//　ステータスウインドウを非アクティブにする
	public void DisableWindow()
	{
		if (canvasGroup == null || canvasGroup.interactable)
		{
			//　ウインドウを非アクティブにする
			transform.root.gameObject.SetActive(false);			
			operationStatus.isMenu = false;
			Time.timeScale = 1.0f;
		}
	}

	//　他の画面を表示する
	public void WindowOnOff(GameObject window)
	{
			selectImage.SetActive(false);
		if (canvasGroup == null || canvasGroup.interactable)
		{
			Camera.main.GetComponent<OperationStatusWindow>().ChangeWindow(window,0);
			//operationStatus.ChangeWindow(window);
		}
	}
	//　前の画面に戻るボタンを選択状態にする
	public void SelectReturnButton()
	{
		EventSystem.current.SetSelectedGameObject(returnButton);
	}
}
