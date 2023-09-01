
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateSynthesisButton : MonoBehaviour
{

	//�@��l���L�����N�^�[�̃X�e�[�^�X
	private StatusWindowStatus statusWindowStatus;
	//�@�A�C�e���f�[�^�x�[�X
	private StatusWindowItemDataBase statusWindowItemDataBase;
	//�@Synthesis�{�^���̃v���n�u
	public GameObject synthesisButtonPrefab;
	//�@�A�C�e���{�^�������Ă����Q�[���I�u�W�F�N�g
	private GameObject[] item;

	RectTransform rect;

	public void SetItem()
    {
		foreach (Transform n in gameObject.transform)
		{
			GameObject.Destroy(n.gameObject);
		}

		//�@�A�C�e���������A�C�e���{�^�����쐬
		for (var i = 0; i < statusWindowItemDataBase.GetItemTotal(); i++)
		{
			//�@�A�C�e���������Ă��Ȃ����A�܂�����1�ȉ��̏ꍇ
			if (!statusWindowStatus.GetItemFlag(i) /*|| statusWindowItemDataBase.GetItemData()[i].GetItemStock() <= 1*/)
			{
				continue;
			}

			item[i] = GameObject.Instantiate(synthesisButtonPrefab) as GameObject;

			item[i].name = "SynthesisItem" + i;
			//�@�A�C�e���{�^���̐e�v�f�����̃X�N���v�g���ݒ肳��Ă���Q�[���I�u�W�F�N�g�ɂ���
			item[i].transform.SetParent(transform);

			//�T�C�Y��1�ɂ���
			rect = item[i].GetComponent<RectTransform>();
			rect.localScale = new Vector3(1, 1, 2);

			//�@�A�C�e���f�[�^�x�[�X�̏�񂩂�X�v���C�g���擾���A�C�e���{�^���̃X�v���C�g�ɐݒ�
			item[i].transform.GetChild(0).GetComponent<Image>().sprite = statusWindowItemDataBase.GetItemData()[i].GetItemSprite();

			//�@SynthesisItemArea�𖳌������Ă���SynthesisButton��L�����i�{�^�����_�ł��Ă���悤�Ɍ����Ă��܂��ׁj
			item[i].transform.GetChild(0).GetComponent<Button>().interactable = true;

			item[i].transform.GetChild(0).GetComponent<SynthesisItemButton>().SetStatusWindowItemData(statusWindowItemDataBase.GetItemData()[i]);
		}
	}

	//�@�Q�[���I�u�W�F�N�g���A�N�e�B�u�ɂȂ��������s
	void OnEnable()
	{
		//�@SynthesisItemArea�𖳌���
		GetComponent<CanvasGroup>().interactable = false;

		statusWindowStatus = Camera.main.GetComponent<StatusWindowStatus>();
		statusWindowItemDataBase = Camera.main.GetComponent<StatusWindowItemDataBase>();
		item = new GameObject[statusWindowItemDataBase.GetItemTotal()];

		SetItem();
	}


	void OnDisable()
	{
		//�@SynthesisItemArea�𖳌���
		GetComponent<CanvasGroup>().interactable = true;
		//�@�Q�[���I�u�W�F�N�g����A�N�e�B�u�ɂȂ鎞�ɍ쐬�����A�C�e���{�^���C���X�^���X���폜����
		for (var i = 0; i < statusWindowItemDataBase.GetItemTotal(); i++)
		{
			Destroy(item[i]);
		}
	}
}
