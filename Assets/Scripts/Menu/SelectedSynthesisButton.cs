
using UnityEngine;
using System.Collections;

public class SelectedSynthesisButton : MonoBehaviour
{

	//�@�������{�^���ԍ�
	public int selectedSynthesisButton;

	void Start()
	{
		selectedSynthesisButton = -1;
	}

	public void SetSelectedSynthesisButton(int select)
	{
		selectedSynthesisButton = select;
	}

	public int GetSelectedSynthesisButton()
	{
		return selectedSynthesisButton;
	}
}
