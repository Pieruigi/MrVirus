using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Virus.Scriptables
{
    public class ThemeAsset : ScriptableObject
    {
        [SerializeField]
        int minFloorCount = 6;

        [SerializeField]
        int maxFloorCount = 8;

        [SerializeField]
        int elevatorCount = 3;

        [SerializeField]
        int minUndergroundCount = 0;

        [SerializeField]
        int maxUndergroundCount = 2;
    }

}
