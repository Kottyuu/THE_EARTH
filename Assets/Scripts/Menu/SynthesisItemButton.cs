
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SynthesisItemButton : MonoBehaviour
{

	// SynthesisItemButtonに保持するアイテムデータ
	private StatusWindowItemData statusWindowItemData;
	private Transform synthesisArea;
	private Transform menuArea;
	private Transform synthesisItemArea;
	private Text informationText;
	//　現在選択している装備ボタンを保持するスクリプト
	private SelectedSynthesisButton selectedSynthesisButton;
	private OperationStatusWindow operationStatus;


	[SerializeField]
	Text text;
	
	[SerializeField]
	GameObject selectImage;

	[SerializeField]
	AudioClip selectSe;

	private AudioSource audioSource;

	[HideInInspector]
	public int NowStock; //表示上の残り個数(実際の残り個数は合成ボタンを押したときに減らす)

	void Start()
	{

		synthesisArea = transform.parent.parent.parent.Find("SynthesisArea");
		menuArea = transform.parent.parent.parent.Find("MenuArea");
		synthesisItemArea = transform.parent.parent.parent.Find("SynthesisItemArea");
		informationText = transform.parent.parent.parent.Find("Information/Text").GetComponent<Text>();
		selectedSynthesisButton = synthesisArea.GetComponent<SelectedSynthesisButton>();
		operationStatus = Camera.main.GetComponent<OperationStatusWindow>();
		NowStock = statusWindowItemData.GetItemStock();
		text.text = NowStock.ToString();
		audioSource = GetComponent<AudioSource>();
	}

    private void Update()
    {
		if(NowStock >= 9999)
        {
			NowStock = 9999;
        }
		//テキストを更新
		text.text = NowStock.ToString();
	}

    //　SynthesisButtonを押した時
    public void OnClick()
	{
		if (GetComponentInParent<CanvasGroup>().interactable && statusWindowItemData.GetItemStock() > 0)
		{
			//数を減らす
			NowStock--;
			if (NowStock <= -1)
			{
				NowStock = 0;
			}
			else //残り個数が1以上なら
			{
				//　イベントシステムのセレクトをオフ
				EventSystem.current.SetSelectedGameObject(null);
				//　SynthesisAreaを有効化
				synthesisArea.GetComponent<CanvasGroup>().interactable = true;
				//　MenuAreaを有効化
				menuArea.GetComponent<CanvasGroup>().interactable = true;
				//　アイテムボタンを無効化
				synthesisItemArea.GetComponent<CanvasGroup>().interactable = false;
				//statusWindowItemData.StockMinus();

				var synthesisButton = synthesisArea.transform.GetChild(selectedSynthesisButton.GetSelectedSynthesisButton()).GetComponentInChildren<SynthesisButton>();
				synthesisButton.SetStatusWindowItemData(statusWindowItemData);

				//　SynthesisAreaの最初のボタンをフォーカスする
				EventSystem.current.SetSelectedGameObject(synthesisArea.GetChild(selectedSynthesisButton.GetSelectedSynthesisButton()).GetChild(0).gameObject);
			}
		}
	}

	public void OnCancel()
	{
		var Image =  synthesisArea.transform.GetChild(selectedSynthesisButton.GetSelectedSynthesisButton()).GetChild(1).GetComponent<Image>();
		if (Image.sprite.name != "UISprite")
        {
			//Debug.Log(Image.sprite.name);
			NowStock--;
        }
		
		//　イベントシステムのセレクトをオフ
		EventSystem.current.SetSelectedGameObject(null);
		//　SynthesisAreaを有効化
		synthesisArea.GetComponent<CanvasGroup>().interactable = true;
		//　MenuAreaを有効化
		menuArea.GetComponent<CanvasGroup>().interactable = true;
		//　アイテムボタンを無効化
		synthesisItemArea.GetComponent<CanvasGroup>().interactable = false;

		//　SynthesisAreaの最初のボタンをフォーカスする
		EventSystem.current.SetSelectedGameObject(synthesisArea.GetChild(selectedSynthesisButton.GetSelectedSynthesisButton()).GetChild(0).gameObject);
	}

	//　SynthesisButtonが選択された時
	public void OnSelected()
	{
		if (GetComponent<Button>().interactable)
		{
			if (GetComponent<Image>().sprite != null)
			{
				informationText.text = statusWindowItemData.GetItemInformation();
				selectImage.SetActive(true);
				audioSource.PlayOneShot(selectSe);
			}
			//operationStatus.Sounds();
			transform.parent.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
		}
	}
	//　SynthesisItemButtonから移動したら情報を削除
	public void OnDeselected()
	{
		selectImage.SetActive(false);
		informationText.text = "";
	}

	public void SetStatusWindowItemData(StatusWindowItemData itemData)
	{
		statusWindowItemData = itemData;
	}
}
