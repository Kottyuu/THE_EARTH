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

        // �F��ς���Q�[���I�u�W�F�N�g����Image�R���|�[�l���g���擾
        Image fade = GetComponent<Image>();

        // �t�F�[�h���̐F��ݒ�i���j
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (255.0f / 255.0f));

        // �t�F�[�h�C���ɂ����鎞�ԁi�b�j
        const float fade_time = 0.5f;

        // ���[�v�񐔁i0�̓G���[
        const int loop_count = 51;

        // �E�F�C�g���ԎZ�o
        float wait_time = fade_time / loop_count;

        // �F�̊Ԋu���Z�o
        float alpha_interval = 255.0f / loop_count;

        // �F�����X�ɕς��郋�[�v
        for (float alpha = 255.0f; alpha >= 0.0f; alpha -= alpha_interval)
        {
            // �҂�����
            yield return new WaitForSeconds(wait_time);

            area_Controller.area[area_Controller.AreaNumber].BGM.volume += 0.02f;

            // Alpha�l��������������
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

            // �F��ς���Q�[���I�u�W�F�N�g����Image�R���|�[�l���g���擾
            Image fade = GetComponent<Image>();

            // �t�F�[�h��̐F��ݒ�i��
            fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (0.0f / 255.0f));

            // �t�F�[�h�C���ɂ����鎞�ԁi�b
            const float fade_time = 0.1f;

            // ���[�v�񐔁i0�̓G���[)
            const int loop_count = 51;

            // �E�F�C�g���ԎZ�o
            float wait_time = fade_time / loop_count;

            // �F�̊Ԋu���Z�o
            float alpha_interval = 255.0f / loop_count;

            // �F�����X�ɕς��郋�[�v
            for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)
            {
                // �҂�����
                yield return new WaitForSeconds(wait_time);

                area_Controller.area[area_Controller.AreaNumber].BGM.volume -= 0.02f;

                // Alpha�l���������グ��
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
