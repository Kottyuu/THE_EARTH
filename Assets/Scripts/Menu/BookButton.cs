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
	//�@�A�C�e���{�^�����I�����ꂽ�����\��
	public void OnSelected()
	{
		//operationStatus.Sounds();
		informationText.text = transform.GetComponentInChildren<Text>().text + " �m ���";
		selectImage.SetActive(true);

		var number = GetItemNum();
		var animal = statusWindowItemDataBase.itemlist[number];

		data.GetChild(0).GetComponent<Image>().enabled = true;
		data.GetChild(0).GetComponent<Image>().sprite = animal.isMade ? animal.sprite : questionImage;                      //�摜

		data.GetChild(1).GetComponent<Text>().text = animal.isMade ? animal.name : "�H�H�H";                               //���O

		data.GetChild(2).GetComponent<Text>().text = "���� : " + (animal.isMade ? animalController.animalGroup[(int)animal.group] : "�H�H�H");      //����
		
		if (animal.group.ToString() != "Fantasy")
		{
			data.GetChild(3).GetComponent<Text>().text = "�����n : " + (animal.isMade ? areaController.area[(int)animal.areas].name : "�H�H�H");    //�����n

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
        else //�_�b�̏ꍇ�́H�H�H
        {
			data.GetChild(3).GetComponent<Text>().text = "�����n : �H�H�H";
			data.GetChild(4).GetComponent<Text>().text = "�H�H�H";
			data.GetChild(5).GetComponent<Text>().text = "�H�H�H";

		}

		data.GetChild(6).GetComponent<Text>().text = "�������@ :";  

		audioSource.PlayOneShot(selectSe);
	}

	private void OnName(int i,int childNumber)
    {
		if (statusWindowStatus.itemFlags[i])//���̈�`�q����肵�����ǂ���
		{
			data.GetChild(childNumber).GetComponent<Text>().text =
				statusWindowItemDataBase.itemlist[i].name + " �m " + (statusWindowItemDataBase.itemlist[i].isElement ? "���f" : "��`�q");  //����
		}
		else
		{
			data.GetChild(childNumber).GetComponent<Text>().text = "�H�H�H";
		}
	}
	
	//�@�A�C�e���{�^������ړ�����������폜
    public void OnDeselected()
	{
		//�ŏ��̏��
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
