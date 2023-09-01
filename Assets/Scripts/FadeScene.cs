using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FadeScene : MonoBehaviour
{

    private bool isPossible = false;

    [SerializeField]
    private AudioClip fadeSe;
    [SerializeField]
    private AudioSource titleBGM;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Color_FadeIn());

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SceneChange(string scene)
    {
        if (isPossible)
        {
            audioSource.PlayOneShot(fadeSe);
            StartCoroutine(Color_FadeOut(scene));
        }
    }
    IEnumerator Color_FadeIn()
    {
        // 色を変えるゲームオブジェクトからImageコンポーネントを取得
        Image fade = GetComponent<Image>();

        // フェード元の色を設定（黒）
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (255.0f / 255.0f));

        // フェードインにかかる時間（秒）
        const float fade_time = 1.5f;

        // ループ回数（0はエラー
        const int loop_count = 51;

        // ウェイト時間算出
        float wait_time = fade_time / loop_count;

        // 色の間隔を算出
        float alpha_interval = 255.0f / loop_count;

        // 色を徐々に変えるループ
        for (float alpha = 255.0f; alpha >= 0.0f; alpha -= alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            // Alpha値を少しずつ下げる
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }

        isPossible = true;

    }

    IEnumerator Color_FadeOut(string scene)
    {
        // 色を変えるゲームオブジェクトからImageコンポーネントを取得
        Image fade = GetComponent<Image>();

        // フェード後の色を設定（黒）
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (0.0f / 255.0f));

        // フェードインにかかる時間（秒）
        const float fade_time = 1.5f;

        // ループ回数（0はエラー）
        const int loop_count = 51;

        // ウェイト時間算出
        float wait_time = fade_time / loop_count;

        // 色の間隔を算出
        float alpha_interval = 255.0f / loop_count;

        // 色を徐々に変えるループ
        for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            // Alpha値を少しずつ上げる
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
            titleBGM.volume -= 0.02f;
        }

        SceneManager.LoadScene(scene);

    }
}
