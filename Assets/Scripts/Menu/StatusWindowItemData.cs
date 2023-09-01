
using UnityEngine;
using System.Collections;

public class StatusWindowItemData : object
{

	//　アイテムのImage画像
	private Sprite itemSprite;
	//　アイテムの名前
	private string itemName;
	//　アイテムの情報
	private string itemInformation;
	//	アイテムの個数
	public int itemStock;

	public StatusWindowItemData(Sprite image, string itemName, string information, int itemStock)
	{
		this.itemSprite = image;
		this.itemName = itemName;
		this.itemInformation = information;
		this.itemStock = itemStock;
	}

	public Sprite GetItemSprite()
	{
		return itemSprite;
	}

	public string GetItemName()
	{
		return itemName;
	}


	public string GetItemInformation()
	{
		return itemInformation;
	}

	public void StockPlas()
    {
		//itemStock += 300;

		var rand = Random.Range(2, 5);//取得数ランダム
		itemStock += rand;
		if (itemStock >= 9999)
		{
			itemStock = 9999;
		}
	}

	public void HozonStock(int hozonItem)
    {
		itemStock = hozonItem;
    }

	public void CheatStockPlas()
	{
		itemStock = 9999;
	}

	public void StockMinus()
    {
		itemStock--;
    }

	public int GetItemStock()
    {

		if(itemStock <= 0)
        {
			return 0;
        }
		if(itemStock >= 9999)
        {
			return 9999;
        }
		return itemStock;
    }

}
