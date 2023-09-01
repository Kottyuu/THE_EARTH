
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActivateSynthesisWindow : MonoBehaviour
{

	//�@������ʂ𑀍쒆�ɃX�e�[�^�X��ʂ�������p�ɑ�����ʂ��A�N�e�B�u�ɂȂ������ɏ���������������
	void OnEnable()
	{ 
		//�@SynthesisArea��L����
		transform.Find("SynthesisArea").GetComponent<CanvasGroup>().interactable = true;
		//�@MenuArea��L����
		transform.Find("MenuArea").GetComponent<CanvasGroup>().interactable = true;

		//�@�A�C�e���{�^���𖳌���
		transform.Find("SynthesisItemArea").GetComponent<CanvasGroup>().interactable = false;
	}
}
