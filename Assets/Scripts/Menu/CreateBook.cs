using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreateBook : MonoBehaviour
{
	//�@��l���L�����N�^�[�̃X�e�[�^�X
	private StatusWindowStatus statusWindowStatus;
	//�@�A�C�e���f�[�^�x�[�X
	private StatusWindowItemDataBase statusWindowItemDataBase;
	//�@�A�C�e���{�^���̃v���n�u
	public GameObject itemPrefab;
	//�@�A�C�e���{�^�������Ă����Q�[���I�u�W�F�N�g
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

    //�@�Q�[���I�u�W�F�N�g���A�N�e�B�u�ɂȂ��������s
    void OnEnable()
	{

		statusWindowStatus = Camera.main.GetComponent<StatusWindowStatus>();
		statusWindowItemDataBase = Camera.main.GetComponent<StatusWindowItemDataBase>();
		item = new GameObject[statusWindowItemDataBase.GetItemTotal()];

		//�@�A�C�e���������A�C�e���{�^�����쐬
		for (var i = 9; i < item.Length; i++)
		{
			item[i] = GameObject.Instantiate(itemPrefab) as GameObject;
			item[i].name = "Book" + i;
			item[i].GetComponentInChildren<Text>().text = statusWindowItemDataBase.itemlist[i].isMade ? statusWindowItemDataBase.itemlist[i].name : "???";
			//�@�A�C�e���{�^���̐e�v�f�����̃X�N���v�g���ݒ肳��Ă���Q�[���I�u�W�F�N�g�ɂ���
			item[i].transform.SetParent(transform);

			//�T�C�Y��1�ɂ���
			rect = item[i].GetComponent<RectTransform>();
			rect.localScale = new Vector3(1, 1, 2);

			//�@�{�^���Ƀ��j�[�N�Ȕԍ���ݒ�i�A�C�e���f�[�^�x�[�X�ԍ��ƑΉ��j
			item[i].transform.GetChild(0).GetComponent<BookButton>().SetItemNum(i);
		}
	}

	void OnDisable()
	{
		GetComponent<RectTransform>().localPosition = new Vector3(148, 0, 0);//�����l�ɖ߂�

		//�@�Q�[���I�u�W�F�N�g����A�N�e�B�u�ɂȂ鎞�ɍ쐬�����A�C�e���{�^���C���X�^���X���폜����
		for (var i = 0; i < item.Length; i++)
		{
			Destroy(item[i]);
		}
	}
}
