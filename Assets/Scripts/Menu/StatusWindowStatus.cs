
using UnityEngine;
using System.Collections;

public class StatusWindowStatus : MonoBehaviour
{

	//　アイテムを持っているかどうかのフラグ
	public bool[] itemFlags;

	private StatusWindowItemDataBase statusWindowItemDataBase;
	private OperationStatusWindow operationStatus;

	void Start()
	{
		statusWindowItemDataBase = GetComponent<StatusWindowItemDataBase>();
		itemFlags = new bool[statusWindowItemDataBase.GetItemData().Length];
		operationStatus = Camera.main.GetComponent<OperationStatusWindow>();
		//SetItemData("豚");
		//SetItemData("羊");
	}

	//　アイテムを持っているかどうか
	public bool GetItemFlag(int num)
	{
		return itemFlags[num];
	}

	//　アイテムをセット
	public void SetItemData(string name, bool cheat)
	{
		var itemDatas = statusWindowItemDataBase.GetItemData();
		for (int i = 0; i < itemDatas.Length; i++)
		{
			if (itemDatas[i].GetItemName() == name)
			{
				itemFlags[i] = true;
				if (!cheat)
				{
					itemDatas[i].StockPlas();
					operationStatus.SynthesisSe();
				}
				else
				{
					itemDatas[i].CheatStockPlas();
				}
			}
		}
	}

	public void SetHozonItemData(string name,int item)
	{
		var itemDatas = statusWindowItemDataBase.GetItemData();
		for (int i = 0; i < itemDatas.Length; i++)
		{
			if (itemDatas[i].GetItemName() == name)
			{
				itemFlags[i] = true;

				itemDatas[i].HozonStock(item);
			}
		}
	}
}