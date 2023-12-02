using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virus.Interfaces;

namespace Virus
{
    public class ElevatorButtonController : MonoBehaviour, IInteractable
    {
        [SerializeField]
        bool externalButton = false;
        public bool External
        {
            get { return externalButton; }
        }

        [SerializeField]
        Material workingMaterial, notWorkingMaterial;


        ElevatorController elevatorController;
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

            elevatorController = GetComponentInParent<ElevatorController>();
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

        public void StartInteraction()
        {
            // We simply pass the button we pressed and it's up to the elevator controller to check the button type
            elevatorController.ProcessElevatorButton(this);
         
        }

        public void StopInteraction()
        {
            
        }

        public bool IsInteractable()
        {
            return working;
        }
    }

}
