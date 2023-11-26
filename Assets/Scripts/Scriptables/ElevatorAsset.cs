using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virus.Enumerators;

namespace Virus.Scriptables
{
    public class ElevatorAsset : ScriptableObject
    {
        public const string ResourceFolder = "Elevators";

        [SerializeField]
        GameObject elevatorPrefab;
        public GameObject ElevatorPrefab
        {
            get { return elevatorPrefab; }
        }

        [SerializeField]
        GameObject elevatorSpawnGroupPrefab;
        public GameObject ElevatorSpawnGroupPrefab
        {
            get { return elevatorSpawnGroupPrefab; }
        }

    }

}
