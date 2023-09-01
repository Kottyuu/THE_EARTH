
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SynthesisItemButton : MonoBehaviour
{

	// SynthesisItemButton�ɕێ�����A�C�e���f�[�^
	private StatusWindowItemData statusWindowItemData;
	private Transform synthesisArea;
	private Transform menuArea;
	private Transform synthesisItemArea;
	private Text informationText;
	//�@���ݑI�����Ă��鑕���{�^����ێ�����X�N���v�g
	private SelectedSynthesisButton selectedSynthesisButton;
	private OperationStatusWindow operationStatus;


	[SerializeField]
	Text text;
	
	[SerializeField]
	GameObject selectImage;

	[SerializeField]
	AudioClip selectSe;

	private AudioSource audioSource;

	[HideInInspector]
	public int NowStock; //�\����̎c���(���ۂ̎c����͍����{�^�����������Ƃ��Ɍ��炷)

	void Start()
	{

		synthesisArea = transform.parent.parent.parent.Find("SynthesisArea");
		menuArea = transform.parent.parent.parent.Find("MenuArea");
		synthesisItemArea = transform.parent.parent.parent.Find("SynthesisItemArea");
		informationText = transform.parent.parent.parent.Find("Information/Text").GetComponent<Text>();
		selectedSynthesisButton = synthesisArea.GetComponent<SelectedSynthesisButton>();
		operationStatus = Camera.main.GetComponent<OperationStatusWindow>();
		NowStock = statusWindowItemData.GetItemStock();
		text.text = NowStock.ToString();
		audioSource = GetComponent<AudioSource>();
	}

    private void Update()
    {
		if(NowStock >= 9999)
        {
			NowStock = 9999;
        }
		//�e�L�X�g���X�V
		text.text = NowStock.ToString();
	}

    //�@SynthesisButton����������
    public void OnClick()
	{
		if (GetComponentInParent<CanvasGroup>().interactable && statusWindowItemData.GetItemStock() > 0)
		{
			//�������炷
			NowStock--;
			if (NowStock <= -1)
			{
				NowStock = 0;
			}
			else //�c�����1�ȏ�Ȃ�
			{
				//�@�C�x���g�V�X�e���̃Z���N�g���I�t
				EventSystem.current.SetSelectedGameObject(null);
				//�@SynthesisArea��L����
				synthesisArea.GetComponent<CanvasGroup>().interactable = true;
				//�@MenuArea��L����
				menuArea.GetComponent<CanvasGroup>().interactable = true;
				//�@�A�C�e���{�^���𖳌���
				synthesisItemArea.GetComponent<CanvasGroup>().interactable = false;
				//statusWindowItemData.StockMinus();

				var synthesisButton = synthesisArea.transform.GetChild(selectedSynthesisButton.GetSelectedSynthesisButton()).GetComponentInChildren<SynthesisButton>();
				synthesisButton.SetStatusWindowItemData(statusWindowItemData);

				//�@SynthesisArea�̍ŏ��̃{�^�����t�H�[�J�X����
				EventSystem.current.SetSelectedGameObject(synthesisArea.GetChild(selectedSynthesisButton.GetSelectedSynthesisButton()).GetChild(0).gameObject);
			}
		}
	}

	public void OnCancel()
	{
		var Image =  synthesisArea.transform.GetChild(selectedSynthesisButton.GetSelectedSynthesisButton()).GetChild(1).GetComponent<Image>();
		if (Image.sprite.name != "UISprite")
        {
			//Debug.Log(Image.sprite.name);
			NowStock--;
        }
		
		//�@�C�x���g�V�X�e���̃Z���N�g���I�t
		EventSystem.current.SetSelectedGameObject(null);
		//�@SynthesisArea��L����
		synthesisArea.GetComponent<CanvasGroup>().interactable = true;
		//�@MenuArea��L����
		menuArea.GetComponent<CanvasGroup>().interactable = true;
		//�@�A�C�e���{�^���𖳌���
		synthesisItemArea.GetComponent<CanvasGroup>().interactable = false;

		//�@SynthesisArea�̍ŏ��̃{�^�����t�H�[�J�X����
		EventSystem.current.SetSelectedGameObject(synthesisArea.GetChild(selectedSynthesisButton.GetSelectedSynthesisButton()).GetChild(0).gameObject);
	}

	//�@SynthesisButton���I�����ꂽ��
	public void OnSelected()
	{
		if (GetComponent<Button>().interactable)
		{
			if (GetComponent<Image>().sprite != null)
			{
				informationText.text = statusWindowItemData.GetItemInformation();
				selectImage.SetActive(true);
				audioSource.PlayOneShot(selectSe);
			}
			//operationStatus.Sounds();
			transform.parent.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
		}
	}
	//�@SynthesisItemButton����ړ�����������폜
	public void OnDeselected()
	{
		selectImage.SetActive(false);
		informationText.text = "";
	}

	public void SetStatusWindowItemData(StatusWindowItemData itemData)
	{
		statusWindowItemData = itemData;
	}
}
