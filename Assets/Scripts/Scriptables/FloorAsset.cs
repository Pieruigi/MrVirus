using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Virus.Enumerators;

namespace Virus.Scriptables
{
    public class FloorAsset : ScriptableObject
    {
        public const string ResourceFolder = "Floors";

     
        [SerializeField]
        bool normalFloor = false;
        public bool NormalFloor
        {
            get { return normalFloor; }
        }

        [SerializeField]
        bool startingFloor = false;
        public bool StartingFloor
        {
            get { return startingFloor; }
        }

        [SerializeField]
        bool undergroundFloor = false;
        public bool UndergroundFloor
        {
            get { return undergroundFloor; }
        }

        [SerializeField]
        int numberOfConfigurations = 1;
        public int NumberOfConfigurations
        {
            get { return numberOfConfigurations; }
        }

       
        [SerializeField]
        int weight = 1;
        public int Weight
        {
            get { return weight; }
        }
    }

}
