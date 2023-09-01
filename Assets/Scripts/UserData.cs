using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Save
{
    [System.Serializable]
    public class UserData
    {
        public int[] animalCount = null;
        public int areaNumberMax = 0;
        public int totalAnimalCount = 0;
        public float regenerationRate = 0;
        public int[] item = null;
        public bool[] itemFrag = null;
    }
}
