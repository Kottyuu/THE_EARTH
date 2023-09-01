using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animals : MonoBehaviour
{
    [SerializeField, Header("���O")]
    private string AnimalName;
    [SerializeField, Header("�����Ȃ�`�F�b�N")]
    private bool isAnimal;

    [SerializeField, Header("�X�s�[�h")]
    private float animalSpeed = 2.0f;

    private bool isDeath;

    private int MaxLife = 3;
    private int Life;

    [SerializeField,Header("HP�\���p�X���C�_�[")]
    private Slider hpSlider;

    private float chargeTime = 5.0f;
    private float timeCount;

    [SerializeField,Header("����Particle")]
    private GameObject particle;

    private Text_Controller text_Controller;
    private StatusWindowStatus statusWindowStatus;
    private StatusWindowItemDataBase statusWindowItemDataBase;

    Quaternion target;

    // Start is called before the first frame update
    void Start()
    {
        Life = MaxLife;
        hpSlider.value = 1f;
        isDeath = false;
        text_Controller = GameObject.Find("Text_Controller").GetComponent<Text_Controller>();
        statusWindowStatus = Camera.main.GetComponent<StatusWindowStatus>();
        statusWindowItemDataBase = Camera.main.GetComponent<StatusWindowItemDataBase>();

        chargeTime = Random.Range(3.0f, 8.0f);

        target = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimal)
        {
            if (!isDeath)
            {
                //particle.SetActive(false);

                timeCount += Time.deltaTime;

                // �����O�i
                transform.position += animalSpeed * (transform.forward * Time.deltaTime);

                // �w�莞�Ԃ̌o�߁i�����j
                if (timeCount > chargeTime)
                {
                    // �i�H�������_���ɕύX����
                    Vector3 course = new Vector3(0, Random.Range(-180, 180), 0);
                    target = Quaternion.Euler(course);

                    chargeTime = Random.Range(3.0f, 8.0f);

                    // �^�C���J�E���g���O�ɖ߂�
                    timeCount = 0;
                }

                //���炩�ɉ�]
                transform.localRotation = Quaternion.Lerp(transform.localRotation, target, 0.002f);

                //Hp����
                hpSlider.value = (float)Life / (float)MaxLife;

                if (Life <= 0)
                {
                    //���ŏ���
                    DestroyAnimals();
                }
            }
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    public void DestroyAnimals()
    {
        Destroy(hpSlider.gameObject);
        particle.SetActive(true);
        isDeath = true;

        var name = AnimalName;
        for(int i = 0; i < statusWindowItemDataBase.itemlist.Length; i++)
        {
            if(name == statusWindowItemDataBase.itemlist[i].name)
            {
                statusWindowItemDataBase.itemlist[i].count--;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Animal"))
        {
            if (isAnimal)
            {
                //transform.Rotate(new Vector3(0, 180, 0));
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player_Hand"))
        {
            //�A�C�e���擾
            statusWindowStatus.SetItemData(AnimalName, false);

            //���C�t�}�C�i�X
            Life--;

            //�v���C���[�̃R���C�_�[�폜
            var Col = other.gameObject.GetComponent<Collider>();
            Col.enabled = false;

            //�e�L�X�g�\��
            text_Controller.OutputLog(AnimalName, isAnimal);
        }

        if(other.gameObject.CompareTag("Ocean"))
        {
            if (isAnimal)
            {
                transform.Rotate(new Vector3(0, 180, 0));
            }
        }
    }
}
