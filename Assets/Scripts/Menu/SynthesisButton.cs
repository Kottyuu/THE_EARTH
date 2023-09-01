
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SynthesisButton : MonoBehaviour
{

	//　SynthesisButtonに保持するアイテムデータ
	public StatusWindowItemData statusWindowItemData;

	private Transform synthesisArea;
	private Transform menuArea;
	private Transform synthesisItemArea;
	private Text informationText;
	private SelectedSynthesisButton selectedSynthesisButton;
	private CanvasGroup canvasGroup;
	private OperationStatusWindow operationStatus;
	private Sound_Controller sound_controller;

	//　主人公キャラクターのステータス
	private StatusWindowStatus statusWindowStatus;
	//　アイテムデータベース
	private StatusWindowItemDataBase statusWindowItemDataBase;
	//　アイテムボタンを入れておくゲームオブジェクト
	private GameObject[] item;

	public int Stock;

	[SerializeField]
	GameObject selectImage;

	//　このの番号
	[SerializeField]
	private int synthesisNum;
	//　戻るボタン
	private GameObject returnButton;

	[SerializeField]
	private Sprite BackGround;

	[SerializeField]
	private ExecutionButton _synthesisButton;

	[SerializeField,Header("アイテムエリア")]
	GameObject obj;


	void Start()
	{
		_synthesisButton.isSynth = false;
		synthesisArea = transform.parent.parent;
		menuArea = transform.parent.parent.parent.Find("MenuArea");
		synthesisItemArea = transform.parent.parent.parent.Find("SynthesisItemArea");
		informationText = transform.parent.parent.parent.Find("Information/Text").GetComponent<Text>();
		selectedSynthesisButton = GetComponentInParent<SelectedSynthesisButton>();
		canvasGroup = synthesisArea.GetComponent<CanvasGroup>();
		returnButton = menuArea.Find("Exit").gameObject;
		statusWindowStatus = Camera.main.GetComponent<StatusWindowStatus>();
		statusWindowItemDataBase = Camera.main.GetComponent<StatusWindowItemDataBase>();
		operationStatus = Camera.main.GetComponent<OperationStatusWindow>();
		sound_controller = GameObject.Find("Sound_Controller").GetComponent<Sound_Controller>();
		item = new GameObject[statusWindowItemDataBase.GetItemTotal()];
	}

	//　SynthesisButtonを押した時
	public void OnClick()
	{
		if (canvasGroup.interactable)
		{
			if(obj.transform.childCount <= 0)
            {
				sound_controller.NotPossible();
				return;		//アイテムを持っていなかったら終了
            }
			
			//　アイテム総数分探す
			for (var i = 0; i < obj.transform.childCount; i++)
			{
				//アイテムエリアの子の子のspriteを参照
				item[i] = obj.transform.GetChild(i).gameObject;
				item[i] = item[i].transform.GetChild(0).gameObject;

				if(item[i].GetComponent<Image>().sprite == GetComponent<Image>().sprite)
                {
					//spriteが同じだったら数をプラス
					item[i].GetComponent<SynthesisItemButton>().NowStock++;
					//Debug.Log(item[i].GetComponent<SynthesisItemButton>().NowStock);
                }
			}

			//　選択中のスロットであることをわかるように背景色を変更する
			transform.parent.GetComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f, 1f);
			//　イベントシステムのセレクトをオフ
			EventSystem.current.SetSelectedGameObject(null);
			//　SynthesisAreaを無効化
			synthesisArea.GetComponent<CanvasGroup>().interactable = false;
			//　MenuAreaを無効化
			menuArea.GetComponent<CanvasGroup>().interactable = false;
			//　アイテムボタンを有効化
			synthesisItemArea.GetComponent<CanvasGroup>().interactable = true;
			//　現在選択中のボタンの番号をセットする
			selectedSynthesisButton.SetSelectedSynthesisButton(synthesisNum);
			//Debug.Log(GetComponent<Image>().sprite);
			_synthesisButton.isSynth = false;
			//　SynthesisItemAreaの最初のボタンをフォーカスする
			EventSystem.current.SetSelectedGameObject(synthesisItemArea.GetChild(0).GetChild(0).gameObject);



		}
	}
	//　SynthesisButtonが選択された時
	public void OnSelected()
	{
		if (canvasGroup.interactable)
		{
			informationText.text = "";
			selectImage.SetActive(true);
			transform.parent.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
		}
	}
	//　SynthesisButtonから移動したら情報を削除
	public void OnDeselected()
	{
		informationText.text = "";
		selectImage.SetActive(false);
	}
	//　SynthesisItemButtonが押されスロットにアイテムをセット
	public void SetStatusWindowItemData(StatusWindowItemData itemData)
	{
		statusWindowItemData = itemData;
		//Debug.Log("statusWindowItemData" + statusWindowItemData);
		//Minus(statusWindowItemData);
		Stock = itemData.itemStock;
		//Debug.Log("Stock"+Stock);
		GetComponent<Image>().sprite = statusWindowItemData.GetItemSprite();
		//GetComponent<Text>().text = statusWindowItemData.GetItemStock().ToString();
		//Debug.Log(statusWindowItemData.GetItemStock());
		transform.parent.GetComponent<Image>().color = new Color(1f, 1f, 1f, 100f / 255f);

	}

    public void Minus(StatusWindowItemData itemData)
    {
		statusWindowItemData = itemData;

		statusWindowItemData.StockMinus();
    }
    void OnEnable()
	{
		GetComponent<Button>().interactable = true;
	}
	//　キー操作でステータス画面を閉じた時は選択中のボタンを元に戻す
	public void OnDisable()
	{
		transform.parent.GetComponent<Image>().color = new Color(1f, 1f, 1f, 100f / 255f);
		GetComponent<Image>().sprite = BackGround;
	}

	//　前の画面に戻るボタンを選択状態にする
	public void SelectReturnButton()
	{
		EventSystem.current.SetSelectedGameObject(returnButton);
	}
}
