
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{

	private Text informationText;
	private StatusWindowItemDataBase itemDataBase;
	private int itemNum;

	private OperationStatusWindow operationStatus;

	[SerializeField]
	GameObject selectImage;

	[SerializeField]
	AudioClip selectSe;

	private AudioSource audioSource;

	void Start()
	{
		itemDataBase = Camera.main.GetComponent<StatusWindowItemDataBase>();
		operationStatus = Camera.main.GetComponent<OperationStatusWindow>();
		informationText = transform.parent.parent.parent.Find("Information/Text").GetComponent<Text>();
		audioSource = GetComponent<AudioSource>();
	}
	//�@�A�C�e���{�^�����I�����ꂽ�����\��
	public void OnSelected()
	{
		//operationStatus.Sounds();
		informationText.text = itemDataBase.GetItemData()[itemNum].GetItemInformation();
		selectImage.SetActive(true);

		audioSource.PlayOneShot(selectSe);
	}
	//�@�A�C�e���{�^������ړ�����������폜
	public void OnDeselected()
	{
		informationText.text = "";
		selectImage.SetActive(false);
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
