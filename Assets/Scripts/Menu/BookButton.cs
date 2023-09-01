using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookButton : MonoBehaviour
{
	private Text informationText;
	private StatusWindowItemDataBase itemDataBase;
	private int itemNum;

	private OperationStatusWindow operationStatus;
	private StatusWindowStatus statusWindowStatus;
	private StatusWindowItemDataBase statusWindowItemDataBase;
	private Area_Controller areaController;
	private Animal_Controller animalController;

	[SerializeField]
	AudioClip selectSe;

	[SerializeField]
	GameObject selectImage;

	private AudioSource audioSource;

	private Transform data;

	private Sprite questionImage;

	void Start()
	{
		itemDataBase = Camera.main.GetComponent<StatusWindowItemDataBase>();
		operationStatus = Camera.main.GetComponent<OperationStatusWindow>();
		statusWindowStatus = Camera.main.GetComponent<StatusWindowStatus>();
		statusWindowItemDataBase = Camera.main.GetComponent<StatusWindowItemDataBase>();
		areaController = GameObject.Find("Area").GetComponent<Area_Controller>();
		animalController = GameObject.Find("Animals").GetComponent<Animal_Controller>();
		informationText = transform.parent.parent.parent.parent.parent.parent.Find("Information/Text").GetComponent<Text>();
		data = transform.parent.parent.parent.parent.parent.parent.Find("BookData");
		//questionImage = transform.parent.GetComponent<CreateBook>().questionImage;
		questionImage = transform.parent.parent.GetComponent<CreateBook>().questionImage;

		audioSource = GetComponent<AudioSource>();
	}
	//　アイテムボタンが選択されたら情報を表示
	public void OnSelected()
	{
		//operationStatus.Sounds();
		informationText.text = transform.GetComponentInChildren<Text>().text + " ノ 情報";
		selectImage.SetActive(true);

		var number = GetItemNum();
		var animal = statusWindowItemDataBase.itemlist[number];

		data.GetChild(0).GetComponent<Image>().enabled = true;
		data.GetChild(0).GetComponent<Image>().sprite = animal.isMade ? animal.sprite : questionImage;                      //画像

		data.GetChild(1).GetComponent<Text>().text = animal.isMade ? animal.name : "？？？";                               //名前

		data.GetChild(2).GetComponent<Text>().text = "分類 : " + (animal.isMade ? animalController.animalGroup[(int)animal.group] : "？？？");      //分類
		
		if (animal.group.ToString() != "Fantasy")
		{
			data.GetChild(3).GetComponent<Text>().text = "生息地 : " + (animal.isMade ? areaController.area[(int)animal.areas].name : "？？？");    //生息地

			for (int i = 0; i < statusWindowItemDataBase.itemlist.Length; i++)
			{
				if (animal.synthesis.sy_1 == statusWindowItemDataBase.itemlist[i].sprite)
				{
					OnName(i, 4);
				}
				if (animal.synthesis.sy_2 == statusWindowItemDataBase.itemlist[i].sprite)
				{
					OnName(i, 5);
				}
			}
		}
        else //神獣の場合は？？？
        {
			data.GetChild(3).GetComponent<Text>().text = "生息地 : ？？？";
			data.GetChild(4).GetComponent<Text>().text = "？？？";
			data.GetChild(5).GetComponent<Text>().text = "？？？";

		}

		data.GetChild(6).GetComponent<Text>().text = "生成方法 :";  

		audioSource.PlayOneShot(selectSe);
	}

	private void OnName(int i,int childNumber)
    {
		if (statusWindowStatus.itemFlags[i])//その遺伝子を入手したかどうか
		{
			data.GetChild(childNumber).GetComponent<Text>().text =
				statusWindowItemDataBase.itemlist[i].name + " ノ " + (statusWindowItemDataBase.itemlist[i].isElement ? "元素" : "遺伝子");  //合成
		}
		else
		{
			data.GetChild(childNumber).GetComponent<Text>().text = "？？？";
		}
	}
	
	//　アイテムボタンから移動したら情報を削除
    public void OnDeselected()
	{
		//最初の状態
		informationText.text = "";
		selectImage.SetActive(false);
		data.GetChild(0).GetComponent<Image>().enabled = false;
		for (int i = 1; i < data.childCount; i++)
		{
			data.GetChild(i).GetComponent<Text>().text = "";
		}
	}

	public void SetItemNum(int number)
	{
		itemNum = number;
	}

	public int GetItemNum()
	{
		return itemNum;
	}
}
