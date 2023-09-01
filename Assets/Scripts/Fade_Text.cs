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


    IEnumerator Wait() //3�b�҂���Fade
    {
        // �F��ς���Q�[���I�u�W�F�N�g����Image�R���|�[�l���g���擾
        Text fade = GetComponent<Text>();

        Color color = fade.color;

        // �t�F�[�h���̐F��ݒ�i���j
        fade.color = new Color(color.r, color.g, color.b, (255.0f / 255.0f));

        yield return new WaitForSeconds(2.0f);
        
        StartCoroutine(Color_FadeIn(fade));

    }
    IEnumerator Color_FadeIn(Text fade)
    {
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

            // Alpha�l��������������
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
    }
}
