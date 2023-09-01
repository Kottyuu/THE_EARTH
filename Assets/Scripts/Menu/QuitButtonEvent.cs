using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuitButtonEvent : MonoBehaviour
{
	//�@���g�̐e��CanvasGroup
	private CanvasGroup canvasGroup;
	//�@�O�̉�ʂɖ߂�{�^��
	private GameObject returnButton;

	private OperationStatusWindow operationStatus;

	void Awake()
	{
		operationStatus = Camera.main.GetComponent<OperationStatusWindow>();
		canvasGroup = GetComponentInParent<CanvasGroup>();
		returnButton = transform.parent.Find("No").gameObject;
	}

	void OnEnable()
	{
		//�����グ���ɗL��������
		//GetComponent<Button>().interactable = true;
	}

	//�@�{�^���̏�Ƀ}�E�X�����������A�܂��̓L�[����ňړ����Ă�����
	public void OnSelected()
	{
		if (canvasGroup == null || canvasGroup.interactable)
		{
			//�@�C�x���g�V�X�e���̃t�H�[�J�X�����̃Q�[���I�u�W�F�N�g�ɂ��鎞���̃Q�[���I�u�W�F�N�g�Ƀt�H�[�J�X
			if (EventSystem.current.currentSelectedGameObject != gameObject)
			{
				EventSystem.current.SetSelectedGameObject(gameObject);
			}

		}
	}
	//�@�X�e�[�^�X�E�C���h�E���A�N�e�B�u�ɂ���
	public void DisableWindow()
	{
		if (canvasGroup == null || canvasGroup.interactable)
		{
			//�@�E�C���h�E���A�N�e�B�u�ɂ���
			transform.root.gameObject.SetActive(false);
			operationStatus.isQuitMenu = false;
			operationStatus.isMenu = false;
			Time.timeScale = 1.0f;
		}
	}
	//�@�O�̉�ʂɖ߂�{�^����I����Ԃɂ���
	public void SelectReturnButton()
	{
		EventSystem.current.SetSelectedGameObject(returnButton);
	}
	//�Q�[���I������
	public void OnQuit()
    {
		Debug.Log("�I��!!!");
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
	}
}
