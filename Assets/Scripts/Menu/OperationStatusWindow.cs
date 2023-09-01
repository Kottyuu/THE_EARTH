
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OperationStatusWindow : MonoBehaviour
{

	[SerializeField]
	private GameObject propertyWindow;

	[SerializeField]
	private GameObject quitWindow;

	[SerializeField]
	private GameObject operateWindow;
	//�@�X�e�[�^�X�E�C���h�E�̑S���̉��
	[SerializeField]
	private GameObject[] windowLists;

	[SerializeField]
	private AudioClip decisionSe;
	[SerializeField]
	private AudioClip synthesisSe;

	[SerializeField,Header("BackGround")]
	RectTransform rect;

	[SerializeField]
	Transform aaa;

	public bool isMenu = false; //���j���[���J���Ă��邩
	public bool isQuitMenu = false; //�I�����j���[���J���Ă��邩
	public bool isPossible = false;  //���j���[���J���邩�ǂ���

	AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
		isPossible = false;
	}

    void Update()
	{
		//�@�X�e�[�^�X�E�C���h�E�̃I���E�I�t
		//if (Input.GetButtonDown("Start"))
		//{
		//	propertyWindow.SetActive(!propertyWindow.activeSelf);
		//	//�@MainWindow���Z�b�g
		//	ChangeWindow(windowLists[0]);
		//}

		Debug.Log("���j���[" + isMenu);

		if(isMenu)
		{
			Time.timeScale = 0.0f;
			rect.localPosition += new Vector3(100, 0, 0);
			if (rect.localPosition.x > 0)
			{
				rect.localPosition = new Vector3(0, 0, 0);
			}
		}
		else if(!isMenu)
		{
			rect.localPosition -= new Vector3(100, 0, 0);
			if (rect.localPosition.x < -900)
			{
				rect.localPosition = new Vector3(-900, 0, 0);
				propertyWindow.SetActive(false);
			}
			Time.timeScale = 1.0f;
		}
	}

	//�@�X�e�[�^�X��ʂ̃E�C���h�E�̃I���E�I�t���\�b�h
	public void ChangeWindow(GameObject window,int number)
	{
		foreach (var item in windowLists)
		{
			if (item == window)
			{
				item.SetActive(true);
				EventSystem.current.SetSelectedGameObject(null);
			}
			else
			{
				item.SetActive(false);
				Time.timeScale = 1.0f;
			}

			if (number != 3)//������@���j���[�ȊO
			{
				//�@���ꂼ��̃E�C���h�E��MenuArea�̍ŏ��̎q�v�f���A�N�e�B�u�ȏ�Ԃɂ���
				EventSystem.current.SetSelectedGameObject(window.transform.Find("MenuArea").GetChild(0).gameObject);
            }
            //else
            //{
            //	EventSystem.current.SetSelectedGameObject(window.transform.Find("MenuArea").GetChild(0).gameObject);
            //	//EventSystem.current.SetSelectedGameObject(aaa.GetChild(0).gameObject);
            //}
        }
	}

	public void OnQuitMenu(InputAction.CallbackContext context)
	{
		if(!isMenu)
		{
			if (isPossible)
			{
				if (context.performed)
				{
					quitWindow.SetActive(!quitWindow.activeSelf);
					ChangeWindow(windowLists[2],2);

					if (quitWindow.activeSelf)
					{
						isMenu = true;
						isQuitMenu = true;
					}
					else
					{
						isMenu = false;
						isQuitMenu = false;

					}
				}
			}
		}
	}

	public void OnMenu(InputAction.CallbackContext context)
	{
		if (!isQuitMenu)
		{
			if (isPossible)
			{
				if (context.performed)
				{
					if (!isMenu)
					{
						propertyWindow.SetActive(true);
						//�@MainWindow���Z�b�g
						ChangeWindow(windowLists[0], 0);
						isMenu = true;
					}
					else
					{
						if (operateWindow.activeSelf)
						{
							ChangeWindow(windowLists[0], 0);
						}
						else
						{
							isMenu = false;
						}
					}
				}
			}
		}
	}

	public void OnOperateMenu(InputAction.CallbackContext context)
    {
		if (!isQuitMenu)
		{
			if (isPossible)
			{
				if (context.performed)
				{
					if (!isMenu)
					{
						propertyWindow.SetActive(true);
						//�@MainWindow���Z�b�g
						ChangeWindow(windowLists[3], 3);
						isMenu = true;
					}
					else
					{
						if (operateWindow.activeSelf)
						{
							isMenu = false;
						}
					}
				}
			}
		}
	}

	public void SynthesisSe()
    {
		audioSource.PlayOneShot(synthesisSe);
	}
}
