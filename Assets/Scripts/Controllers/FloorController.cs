using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Virus.Builder;

namespace Virus
{
    public class FloorController : MonoBehaviour
    {
        Floor floor;
        
        public void Activate(bool value)
        {
            gameObject.SetActive(value);
        }

        public void Initialize(Floor floor)
        {
            this.floor = floor;
            Configure();
        }

        void Configure()
        {
            Debug.Log($"Setting configuration {floor.ConfigurationId} for {floor.name}");
        }
    }

    
}
