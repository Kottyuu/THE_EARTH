using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animals : MonoBehaviour
{
    [SerializeField, Header("名前")]
    private string AnimalName;
    [SerializeField, Header("動物ならチェック")]
    private bool isAnimal;

    [SerializeField, Header("スピード")]
    private float animalSpeed = 2.0f;

    private bool isDeath;

    private int MaxLife = 3;
    private int Life;

    [SerializeField,Header("HP表示用スライダー")]
    private Slider hpSlider;

    private float chargeTime = 5.0f;
    private float timeCount;

    [SerializeField,Header("消滅Particle")]
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

                // 自動前進
                transform.position += animalSpeed * (transform.forward * Time.deltaTime);

                // 指定時間の経過（条件）
                if (timeCount > chargeTime)
                {
                    // 進路をランダムに変更する
                    Vector3 course = new Vector3(0, Random.Range(-180, 180), 0);
                    target = Quaternion.Euler(course);

                    chargeTime = Random.Range(3.0f, 8.0f);

                    // タイムカウントを０に戻す
                    timeCount = 0;
                }

                //滑らかに回転
                transform.localRotation = Quaternion.Lerp(transform.localRotation, target, 0.002f);

                //Hp処理
                hpSlider.value = (float)Life / (float)MaxLife;

                if (Life <= 0)
                {
                    //消滅処理
                    DestroyAnimals();
                }
            }
        }
    }

    /// <summary>
    /// 消滅
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
            //アイテム取得
            statusWindowStatus.SetItemData(AnimalName, false);

            //ライフマイナス
            Life--;

            //プレイヤーのコライダー削除
            var Col = other.gameObject.GetComponent<Collider>();
            Col.enabled = false;

            //テキスト表示
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
