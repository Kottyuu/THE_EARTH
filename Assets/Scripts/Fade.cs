using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{

    Player_Controller player;

    [SerializeField]
    GameObject Areatext;

    OperationStatusWindow operationStatus;
    private Area_Controller area_Controller;

    // Start is called before the first frame update
    void Start()
    {
        //Areatext.SetActive(true);
        FadeIn();
        player = GameObject.Find("Player").GetComponent<Player_Controller>();
        operationStatus = Camera.main.GetComponent<OperationStatusWindow>();
        area_Controller = GameObject.Find("Area").GetComponent<Area_Controller>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeIn()
    {
        StartCoroutine("Color_FadeIn");
    }

    public void FadeOut(int a)
    {
        StartCoroutine(Color_FadeOut(a));
    }

    IEnumerator Color_FadeIn()
    {

        Areatext.SetActive(true);

        // 色を変えるゲームオブジェクトからImageコンポーネントを取得
        Image fade = GetComponent<Image>();

        // フェード元の色を設定（黒）
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (255.0f / 255.0f));

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

            area_Controller.area[area_Controller.AreaNumber].BGM.volume += 0.02f;

            // Alpha値を少しずつ下げる
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
        player.isMove = true;
        operationStatus.isPossible = true;
    }
    IEnumerator Color_FadeOut(int a)
    {
        if (area_Controller.AreaNumberMax >= 1)
        {
            player.isMove = false;
            operationStatus.isPossible = false;

            // 色を変えるゲームオブジェクトからImageコンポーネントを取得
            Image fade = GetComponent<Image>();

            // フェード後の色を設定（黒
            fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (0.0f / 255.0f));

            // フェードインにかかる時間（秒
            const float fade_time = 0.1f;

            // ループ回数（0はエラー)
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

                area_Controller.area[area_Controller.AreaNumber].BGM.volume -= 0.02f;

                // Alpha値を少しずつ上げる
                Color new_color = fade.color;
                new_color.a = alpha / 255.0f;
                fade.color = new_color;
            }

            StartCoroutine(Wait(a));
        }
    }
    IEnumerator Wait(int a)
    {
        Areatext.SetActive(false);

        player.AreaChange(a);
        area_Controller.SkyChange();

        yield return new WaitForSeconds(1.0f);

        FadeIn();
    }
}
