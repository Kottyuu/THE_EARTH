
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateItemButton : MonoBehaviour {

	//　主人公キャラクターのステータス
	private StatusWindowStatus statusWindowStatus;
	//　アイテムデータベース
	private StatusWindowItemDataBase statusWindowItemDataBase;
	//　アイテムボタンのプレハブ
	public GameObject itemPrefab;
	//　アイテムボタンを入れておくゲームオブジェクト
	private GameObject[] item;

	RectTransform rect;

	//　ゲームオブジェクトがアクティブになった時実行
	void OnEnable() {
		statusWindowStatus = Camera.main.GetComponent<StatusWindowStatus>();
		statusWindowItemDataBase = Camera.main.GetComponent<StatusWindowItemDataBase>();
		item = new GameObject[statusWindowItemDataBase.GetItemTotal()];

		//　アイテム総数分アイテムボタンを作成
		for(var i = 0; i < statusWindowItemDataBase.GetItemTotal(); i++) {

			if (!statusWindowStatus.GetItemFlag(i) /*|| statusWindowItemDataBase.GetItemData()[i].GetItemStock() <= 1*/)
			{
				continue;
			}

			item[i] = GameObject.Instantiate(itemPrefab) as GameObject;
			item [i].name = "Item" + i;
			//　アイテムボタンの親要素をこのスクリプトが設定されているゲームオブジェクトにする
			item[i].transform.SetParent(transform);

			//サイズを1にする
			rect = item[i].GetComponent<RectTransform>();
			rect.localScale = new Vector3(1, 1, 2);

			//　アイテムを持っているかどうか
			if (statusWindowStatus.GetItemFlag(i)) {
				//　アイテムデータベースの情報からスプライトを取得しアイテムボタンのスプライトに設定
				item[i].transform.GetChild(0).GetComponent<Image>().sprite = statusWindowItemDataBase.GetItemData()[i].GetItemSprite ();
				item[i].transform.GetChild(1).GetComponent<Text>().text = statusWindowItemDataBase.GetItemData()[i].GetItemStock().ToString();
				//Debug.Log(item[i].transform.GetChild(1).GetComponent<Text>().text);
			} else {
				//　アイテムボタンのUI.Imageを不可視にし、マウスやキー操作で移動しないようにする
				item[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
				item[i].transform.GetChild(0).GetComponent<Button>().interactable = false;
				item[i].SetActive(false);
			}
			//　ボタンにユニークな番号を設定（アイテムデータベース番号と対応）
			item[i].transform.GetChild(0).GetComponent<ItemButton>().SetItemNum(i);
		}
	}

	void OnDisable() {
		//　ゲームオブジェクトが非アクティブになる時に作成したアイテムボタンインスタンスを削除する
		for(var i = 0; i < statusWindowItemDataBase.GetItemTotal(); i++) {
			Destroy(item[i]);
		}
	}
}
