using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreateBook : MonoBehaviour
{
	//　主人公キャラクターのステータス
	private StatusWindowStatus statusWindowStatus;
	//　アイテムデータベース
	private StatusWindowItemDataBase statusWindowItemDataBase;
	//　アイテムボタンのプレハブ
	public GameObject itemPrefab;
	//　アイテムボタンを入れておくゲームオブジェクト
	private GameObject[] item;

	public Sprite questionImage;

	RectTransform rect;

	[HideInInspector]
	public float rect_1;
	[HideInInspector]
	public float rect_2;

    private void Update()
    {
    }

    //　ゲームオブジェクトがアクティブになった時実行
    void OnEnable()
	{

		statusWindowStatus = Camera.main.GetComponent<StatusWindowStatus>();
		statusWindowItemDataBase = Camera.main.GetComponent<StatusWindowItemDataBase>();
		item = new GameObject[statusWindowItemDataBase.GetItemTotal()];

		//　アイテム総数分アイテムボタンを作成
		for (var i = 9; i < item.Length; i++)
		{
			item[i] = GameObject.Instantiate(itemPrefab) as GameObject;
			item[i].name = "Book" + i;
			item[i].GetComponentInChildren<Text>().text = statusWindowItemDataBase.itemlist[i].isMade ? statusWindowItemDataBase.itemlist[i].name : "???";
			//　アイテムボタンの親要素をこのスクリプトが設定されているゲームオブジェクトにする
			item[i].transform.SetParent(transform);

			//サイズを1にする
			rect = item[i].GetComponent<RectTransform>();
			rect.localScale = new Vector3(1, 1, 2);

			//　ボタンにユニークな番号を設定（アイテムデータベース番号と対応）
			item[i].transform.GetChild(0).GetComponent<BookButton>().SetItemNum(i);
		}
	}

	void OnDisable()
	{
		GetComponent<RectTransform>().localPosition = new Vector3(148, 0, 0);//初期値に戻す

		//　ゲームオブジェクトが非アクティブになる時に作成したアイテムボタンインスタンスを削除する
		for (var i = 0; i < item.Length; i++)
		{
			Destroy(item[i]);
		}
	}
}
