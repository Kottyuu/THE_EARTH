
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActivateSynthesisWindow : MonoBehaviour
{

	//　装備画面を操作中にステータス画面を閉じた時用に装備画面がアクティブになった時に初期化処理をする
	void OnEnable()
	{ 
		//　SynthesisAreaを有効化
		transform.Find("SynthesisArea").GetComponent<CanvasGroup>().interactable = true;
		//　MenuAreaを有効化
		transform.Find("MenuArea").GetComponent<CanvasGroup>().interactable = true;

		//　アイテムボタンを無効化
		transform.Find("SynthesisItemArea").GetComponent<CanvasGroup>().interactable = false;
	}
}
