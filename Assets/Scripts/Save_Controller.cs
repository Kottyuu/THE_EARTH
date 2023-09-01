using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Save
{
    public class Save_Controller : MonoBehaviour
    {
        private StatusWindowItemDataBase statusWindowItemDataBase;
        private StatusWindowStatus statusWindowStatus;
        private Area_Controller area_Controller;
        private Animal_Controller animal_Controller;
        private StatusWindowItemData statusWindowItemData;

        bool isOne;

        private void Start()
        {
            statusWindowItemDataBase = Camera.main.GetComponent<StatusWindowItemDataBase>();
            statusWindowStatus = Camera.main.GetComponent<StatusWindowStatus>();

            area_Controller = GameObject.Find("Area").GetComponent<Area_Controller>();
            animal_Controller = GameObject.Find("Animals").GetComponent<Animal_Controller>();

            isOne = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isOne)
            {
                SaveSystem.Instance.Load(false);
                isOne = true;
            }
        }

        public void SaveClick()
        {
            SaveSystem.Instance.UserData.animalCount = new int[statusWindowItemDataBase.itemlist.Length];
            SaveSystem.Instance.UserData.item = new int[statusWindowItemDataBase.itemlist.Length];
            SaveSystem.Instance.UserData.itemFrag = new bool[statusWindowStatus.itemFlags.Length];

            for (int i = 0; i < statusWindowItemDataBase.itemlist.Length; i++)
            {
                //Debug.Log(SaveSystem.Instance.UserData.animalCount.Length);
                SaveSystem.Instance.UserData.animalCount[i] = statusWindowItemDataBase.itemlist[i].count;
                //SaveSystem.Instance.UserData.item[i] = statusWindowItemData.GetItemStock();

                SaveSystem.Instance.UserData.item[i] = statusWindowItemDataBase.GetItemData()[i].GetItemStock();
                SaveSystem.Instance.UserData.itemFrag[i] = statusWindowStatus.itemFlags[i];
            }

            SaveSystem.Instance.UserData.regenerationRate = animal_Controller.regenerationRate;

            SaveSystem.Instance.UserData.areaNumberMax = area_Controller.AreaNumberMax;
            SaveSystem.Instance.UserData.totalAnimalCount = animal_Controller.TotalAnimal;
            SaveSystem.Instance.Save();

            Debug.Log("セーブしました");
            Debug.Log("終了!!!");

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
        }
    }
}

