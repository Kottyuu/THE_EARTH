
using UnityEngine;
using System.Collections;

public class SelectedSynthesisButton : MonoBehaviour
{

	//　押したボタン番号
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
