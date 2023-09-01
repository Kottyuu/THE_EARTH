
using UnityEngine;
using System.Collections;

public class StatusWindowItemDataBase : MonoBehaviour
{
	[System.Serializable]
	public enum Area
	{
		ocean,
		desert,
		grassland,
		jungle,
		antarctic,
	}
	
	[System.Serializable]
	public enum Group
	{
		Fish,
		Reptiles,
		Amphibian,
		Mammalian,
		Insect,
		Bird,
		Ancient,
		Fantasy,
	}

	[System.Serializable]
	public struct Synthesis
	{
		public Sprite sy_1;
		public Sprite sy_2;
	}

    [System.Serializable]
	public struct ItemList
	{
		[Header("名前")]
		public string name;
		[Header("生物のPrefab")]
		public GameObject[] animalPrefab;
		[Header("遺伝子のSprite")]
		public Sprite sprite;
		[Header("出現場所")]
		public Area areas;
		[Header("分類")]
		public Group group;
		[Header("元素ならチェック")]
		public bool isElement;
		[Header("神獣ならチェック")]
		public bool isFantasy;
		[Header("成功確率(10~100%)"), Range(1, 10)]
		public int probability;
		[Header("合成リスト")]
		public Synthesis synthesis;

		public int count;//この動物の総生成数
		public bool isMade;//この動物が作られたかどうか
	}

	[Header("生物リスト")]
	public ItemList[] itemlist;

	private StatusWindowItemData[] itemLists;

	void Awake()
	{
		itemLists = new StatusWindowItemData[itemlist.Length];

		//　アイテムの全情報を作成
		for (int i = 0; i < itemlist.Length; i++)
		{
			var Prefab = itemlist[i];
			itemLists[i] = new StatusWindowItemData(Prefab.sprite, Prefab.name, Prefab.name + " ノ " + (Prefab.isElement ? "元素" : "遺伝子"), 0);
		}
	}

	public StatusWindowItemData[] GetItemData()
	{
		return itemLists;
	}

	public int GetItemTotal()
	{
		return itemLists.Length;
	}

}
