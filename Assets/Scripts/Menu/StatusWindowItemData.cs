
using UnityEngine;
using System.Collections;

public class StatusWindowItemData : object
{

	//�@�A�C�e����Image�摜
	private Sprite itemSprite;
	//�@�A�C�e���̖��O
	private string itemName;
	//�@�A�C�e���̏��
	private string itemInformation;
	//	�A�C�e���̌�
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

		var rand = Random.Range(2, 5);//�擾�������_��
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
