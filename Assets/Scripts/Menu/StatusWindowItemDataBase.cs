
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
		[Header("���O")]
		public string name;
		[Header("������Prefab")]
		public GameObject[] animalPrefab;
		[Header("��`�q��Sprite")]
		public Sprite sprite;
		[Header("�o���ꏊ")]
		public Area areas;
		[Header("����")]
		public Group group;
		[Header("���f�Ȃ�`�F�b�N")]
		public bool isElement;
		[Header("�_�b�Ȃ�`�F�b�N")]
		public bool isFantasy;
		[Header("�����m��(10~100%)"), Range(1, 10)]
		public int probability;
		[Header("�������X�g")]
		public Synthesis synthesis;

		public int count;//���̓����̑�������
		public bool isMade;//���̓��������ꂽ���ǂ���
	}

	[Header("�������X�g")]
	public ItemList[] itemlist;

	private StatusWindowItemData[] itemLists;

	void Awake()
	{
		itemLists = new StatusWindowItemData[itemlist.Length];

		//�@�A�C�e���̑S�����쐬
		for (int i = 0; i < itemlist.Length; i++)
		{
			var Prefab = itemlist[i];
			itemLists[i] = new StatusWindowItemData(Prefab.sprite, Prefab.name, Prefab.name + " �m " + (Prefab.isElement ? "���f" : "��`�q"), 0);
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
