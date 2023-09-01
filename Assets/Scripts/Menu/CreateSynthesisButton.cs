
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateSynthesisButton : MonoBehaviour
{

	//　主人公キャラクターのステータス
	private StatusWindowStatus statusWindowStatus;
	//　アイテムデータベース
	private StatusWindowItemDataBase statusWindowItemDataBase;
	//　Synthesisボタンのプレハブ
	public GameObject synthesisButtonPrefab;
	//　アイテムボタンを入れておくゲームオブジェクト
	private GameObject[] item;

	RectTransform rect;

	public void SetItem()
    {
		foreach (Transform n in gameObject.transform)
		{
			GameObject.Destroy(n.gameObject);
		}

		//　アイテム総数分アイテムボタンを作成
		for (var i = 0; i < statusWindowItemDataBase.GetItemTotal(); i++)
		{
			//　アイテムを持っていない時、また個数が1つ以下の場合
			if (!statusWindowStatus.GetItemFlag(i) /*|| statusWindowItemDataBase.GetItemData()[i].GetItemStock() <= 1*/)
			{
				continue;
			}

			item[i] = GameObject.Instantiate(synthesisButtonPrefab) as GameObject;

			item[i].name = "SynthesisItem" + i;
			//　アイテムボタンの親要素をこのスクリプトが設定されているゲームオブジェクトにする
			item[i].transform.SetParent(transform);

			//サイズを1にする
			rect = item[i].GetComponent<RectTransform>();
			rect.localScale = new Vector3(1, 1, 2);

			//　アイテムデータベースの情報からスプライトを取得しアイテムボタンのスプライトに設定
			item[i].transform.GetChild(0).GetComponent<Image>().sprite = statusWindowItemDataBase.GetItemData()[i].GetItemSprite();

			//　SynthesisItemAreaを無効化してからSynthesisButtonを有効化（ボタンが点滅しているように見えてしまう為）
			item[i].transform.GetChild(0).GetComponent<Button>().interactable = true;

			item[i].transform.GetChild(0).GetComponent<SynthesisItemButton>().SetStatusWindowItemData(statusWindowItemDataBase.GetItemData()[i]);
		}
	}

	//　ゲームオブジェクトがアクティブになった時実行
	void OnEnable()
	{
		//　SynthesisItemAreaを無効化
		GetComponent<CanvasGroup>().interactable = false;

		statusWindowStatus = Camera.main.GetComponent<StatusWindowStatus>();
		statusWindowItemDataBase = Camera.main.GetComponent<StatusWindowItemDataBase>();
		item = new GameObject[statusWindowItemDataBase.GetItemTotal()];

		SetItem();
	}


	void OnDisable()
	{
		//　SynthesisItemAreaを無効化
		GetComponent<CanvasGroup>().interactable = true;
		//　ゲームオブジェクトが非アクティブになる時に作成したアイテムボタンインスタンスを削除する
		for (var i = 0; i < statusWindowItemDataBase.GetItemTotal(); i++)
		{
			Destroy(item[i]);
		}
	}
}
