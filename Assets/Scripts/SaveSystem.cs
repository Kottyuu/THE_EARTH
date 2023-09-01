using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Save
{
    public class SaveSystem
    {
        #region Singleton
        private static SaveSystem instance = new SaveSystem();
        public static SaveSystem Instance => instance;
        #endregion
        private SaveSystem()
        {
            //Load();
        }

        public string Path => Application.dataPath + "/data.json";

        public UserData UserData { get; private set; }

        public void Save()
        {
            string jsonData = JsonUtility.ToJson(UserData);
            StreamWriter writer = new StreamWriter(Path, false);
            writer.WriteLine(jsonData);
            writer.Flush();
            writer.Close();
        }

        public void Reset()
        {
            if(File.Exists(Path))
            {
                File.Delete(Path);
            }
        }

        public void Load(bool isReset)
        {
            var statusWindowItemDataBase = Camera.main.GetComponent<StatusWindowItemDataBase>();
            var animal_Controller = GameObject.Find("Animals").GetComponent<Animal_Controller>();
            var statusWindowStatus = Camera.main.GetComponent<StatusWindowStatus>();

            if (!File.Exists(Path))
            {
                Debug.Log("èââÒãNìÆ");
                UserData = new UserData();

                SaveSystem.Instance.UserData.animalCount = new int[statusWindowItemDataBase.itemlist.Length];
                SaveSystem.Instance.UserData.item = new int[statusWindowItemDataBase.itemlist.Length];
                SaveSystem.Instance.UserData.itemFrag = new bool[statusWindowStatus.itemFlags.Length];

                Save();
                return;
            }

            StreamReader reader = new StreamReader(Path);
            string jsonData = reader.ReadToEnd();
            UserData = JsonUtility.FromJson<UserData>(jsonData);
            reader.Close();

            for (int i = 0; i < statusWindowItemDataBase.itemlist.Length; i++)
            {
                if (Instance.UserData.itemFrag[i])
                {
                    statusWindowStatus.SetHozonItemData(statusWindowItemDataBase.itemlist[i].name, Instance.UserData.item[i]);
                }

                for (int j = 0; j < Instance.UserData.animalCount[i]; j++)
                {
                    animal_Controller.Generate(i, true);
                }
            }

            animal_Controller.regenerationRate = Instance.UserData.regenerationRate;
        }
    }
}
