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
        // �F��ς���Q�[���I�u�W�F�N�g����Image�R���|�[�l���g���擾
        Image fade = GetComponent<Image>();

        // �t�F�[�h���̐F��ݒ�i���j
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (255.0f / 255.0f));

        // �t�F�[�h�C���ɂ����鎞�ԁi�b�j
        const float fade_time = 1.5f;

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

        isPossible = true;

    }

    IEnumerator Color_FadeOut(string scene)
    {
        // �F��ς���Q�[���I�u�W�F�N�g����Image�R���|�[�l���g���擾
        Image fade = GetComponent<Image>();

        // �t�F�[�h��̐F��ݒ�i���j
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (0.0f / 255.0f));

        // �t�F�[�h�C���ɂ����鎞�ԁi�b�j
        const float fade_time = 1.5f;

        // ���[�v�񐔁i0�̓G���[�j
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

            // Alpha�l���������グ��
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
            titleBGM.volume -= 0.02f;
        }

        SceneManager.LoadScene(scene);

    }
}
