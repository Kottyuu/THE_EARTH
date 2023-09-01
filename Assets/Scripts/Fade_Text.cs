using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade_Text : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
    //    StartCoroutine("Wait");
    //}

    private void OnEnable()
    {
        StartCoroutine("Wait");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator Wait() //3秒待ってFade
    {
        // 色を変えるゲームオブジェクトからImageコンポーネントを取得
        Text fade = GetComponent<Text>();

        Color color = fade.color;

        // フェード元の色を設定（黒）
        fade.color = new Color(color.r, color.g, color.b, (255.0f / 255.0f));

        yield return new WaitForSeconds(2.0f);
        
        StartCoroutine(Color_FadeIn(fade));

    }
    IEnumerator Color_FadeIn(Text fade)
    {
        // フェードインにかかる時間（秒）
        const float fade_time = 0.5f;

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
    }
}
