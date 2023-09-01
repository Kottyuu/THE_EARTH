
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SynthesisButton : MonoBehaviour
{

	//�@SynthesisButton�ɕێ�����A�C�e���f�[�^
	public StatusWindowItemData statusWindowItemData;

	private Transform synthesisArea;
	private Transform menuArea;
	private Transform synthesisItemArea;
	private Text informationText;
	private SelectedSynthesisButton selectedSynthesisButton;
	private CanvasGroup canvasGroup;
	private OperationStatusWindow operationStatus;
	private Sound_Controller sound_controller;

	//�@��l���L�����N�^�[�̃X�e�[�^�X
	private StatusWindowStatus statusWindowStatus;
	//�@�A�C�e���f�[�^�x�[�X
	private StatusWindowItemDataBase statusWindowItemDataBase;
	//�@�A�C�e���{�^�������Ă����Q�[���I�u�W�F�N�g
	private GameObject[] item;

	public int Stock;

	[SerializeField]
	GameObject selectImage;

	//�@���̂̔ԍ�
	[SerializeField]
	private int synthesisNum;
	//�@�߂�{�^��
	private GameObject returnButton;

	[SerializeField]
	private Sprite BackGround;

	[SerializeField]
	private ExecutionButton _synthesisButton;

	[SerializeField,Header("�A�C�e���G���A")]
	GameObject obj;


	void Start()
	{
		_synthesisButton.isSynth = false;
		synthesisArea = transform.parent.parent;
		menuArea = transform.parent.parent.parent.Find("MenuArea");
		synthesisItemArea = transform.parent.parent.parent.Find("SynthesisItemArea");
		informationText = transform.parent.parent.parent.Find("Information/Text").GetComponent<Text>();
		selectedSynthesisButton = GetComponentInParent<SelectedSynthesisButton>();
		canvasGroup = synthesisArea.GetComponent<CanvasGroup>();
		returnButton = menuArea.Find("Exit").gameObject;
		statusWindowStatus = Camera.main.GetComponent<StatusWindowStatus>();
		statusWindowItemDataBase = Camera.main.GetComponent<StatusWindowItemDataBase>();
		operationStatus = Camera.main.GetComponent<OperationStatusWindow>();
		sound_controller = GameObject.Find("Sound_Controller").GetComponent<Sound_Controller>();
		item = new GameObject[statusWindowItemDataBase.GetItemTotal()];
	}

	//�@SynthesisButton����������
	public void OnClick()
	{
		if (canvasGroup.interactable)
		{
			if(obj.transform.childCount <= 0)
            {
				sound_controller.NotPossible();
				return;		//�A�C�e���������Ă��Ȃ�������I��
            }
			
			//�@�A�C�e���������T��
			for (var i = 0; i < obj.transform.childCount; i++)
			{
				//�A�C�e���G���A�̎q�̎q��sprite���Q��
				item[i] = obj.transform.GetChild(i).gameObject;
				item[i] = item[i].transform.GetChild(0).gameObject;

				if(item[i].GetComponent<Image>().sprite == GetComponent<Image>().sprite)
                {
					//sprite�������������琔���v���X
					item[i].GetComponent<SynthesisItemButton>().NowStock++;
					//Debug.Log(item[i].GetComponent<SynthesisItemButton>().NowStock);
                }
			}

			//�@�I�𒆂̃X���b�g�ł��邱�Ƃ��킩��悤�ɔw�i�F��ύX����
			transform.parent.GetComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f, 1f);
			//�@�C�x���g�V�X�e���̃Z���N�g���I�t
			EventSystem.current.SetSelectedGameObject(null);
			//�@SynthesisArea�𖳌���
			synthesisArea.GetComponent<CanvasGroup>().interactable = false;
			//�@MenuArea�𖳌���
			menuArea.GetComponent<CanvasGroup>().interactable = false;
			//�@�A�C�e���{�^����L����
			synthesisItemArea.GetComponent<CanvasGroup>().interactable = true;
			//�@���ݑI�𒆂̃{�^���̔ԍ����Z�b�g����
			selectedSynthesisButton.SetSelectedSynthesisButton(synthesisNum);
			//Debug.Log(GetComponent<Image>().sprite);
			_synthesisButton.isSynth = false;
			//�@SynthesisItemArea�̍ŏ��̃{�^�����t�H�[�J�X����
			EventSystem.current.SetSelectedGameObject(synthesisItemArea.GetChild(0).GetChild(0).gameObject);



		}
	}
	//�@SynthesisButton���I�����ꂽ��
	public void OnSelected()
	{
		if (canvasGroup.interactable)
		{
			informationText.text = "";
			selectImage.SetActive(true);
			transform.parent.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
		}
	}
	//�@SynthesisButton����ړ�����������폜
	public void OnDeselected()
	{
		informationText.text = "";
		selectImage.SetActive(false);
	}
	//�@SynthesisItemButton��������X���b�g�ɃA�C�e�����Z�b�g
	public void SetStatusWindowItemData(StatusWindowItemData itemData)
	{
		statusWindowItemData = itemData;
		//Debug.Log("statusWindowItemData" + statusWindowItemData);
		//Minus(statusWindowItemData);
		Stock = itemData.itemStock;
		//Debug.Log("Stock"+Stock);
		GetComponent<Image>().sprite = statusWindowItemData.GetItemSprite();
		//GetComponent<Text>().text = statusWindowItemData.GetItemStock().ToString();
		//Debug.Log(statusWindowItemData.GetItemStock());
		transform.parent.GetComponent<Image>().color = new Color(1f, 1f, 1f, 100f / 255f);

	}

    public void Minus(StatusWindowItemData itemData)
    {
		statusWindowItemData = itemData;

		statusWindowItemData.StockMinus();
    }
    void OnEnable()
	{
		GetComponent<Button>().interactable = true;
	}
	//�@�L�[����ŃX�e�[�^�X��ʂ�������͑I�𒆂̃{�^�������ɖ߂�
	public void OnDisable()
	{
		transform.parent.GetComponent<Image>().color = new Color(1f, 1f, 1f, 100f / 255f);
		GetComponent<Image>().sprite = BackGround;
	}

	//�@�O�̉�ʂɖ߂�{�^����I����Ԃɂ���
	public void SelectReturnButton()
	{
		EventSystem.current.SetSelectedGameObject(returnButton);
	}
}
