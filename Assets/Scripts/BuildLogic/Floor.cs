using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virus.Scriptables;

namespace Virus.Builder
{
   
    public class Floor: MonoBehaviour
    {
        FloorAsset asset;

        /// <summary>
        /// Each floor may have more than one configuration; for configuration we mean for example the way cameras and
        /// patrol points have been set up ( this may even involve the type of cyber-monster we are going to encounter )
        /// </summary>
        int configurationId = -1;
              
        /// <summary>
        /// Initialize the floor, also choosing a random configuration
        /// </summary>
        /// <param name="asset"></param>
        public void Init(FloorAsset asset)
        {
            this.asset = asset;
            // Set a random configuration
            configurationId = Random.Range(0, asset.NumberOfConfigurations);
        }
    }

}
