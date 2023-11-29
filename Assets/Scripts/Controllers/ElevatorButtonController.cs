using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Virus
{
    public class ElevatorButtonController : MonoBehaviour
    {
        [SerializeField]
        bool externalButton = false;
        public bool External
        {
            get { return externalButton; }
        }

        [SerializeField]
        Material workingMaterial, notWorkingMaterial;

        

        bool working = false;

        private void Awake()
        {
            if (externalButton)
            {
                // Since the external button changes depending on the floor the elevator is on we cache materials 
                // for further use
                workingMaterial = new Material(workingMaterial);
                notWorkingMaterial = new Material(notWorkingMaterial);
            }
        }

        public void SetWorking(bool value)
        {
            working = value;
            // Set the working material
            Material mat = null;
            if (value)
                mat = externalButton ? workingMaterial : new Material(workingMaterial);
            else
                mat = externalButton ? notWorkingMaterial : new Material(notWorkingMaterial);

            GetComponent<Renderer>().material = mat;
        }


    }

}
