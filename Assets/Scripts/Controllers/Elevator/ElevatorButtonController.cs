using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virus.Builder;
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

        int index = -1; // Only for internal buttons

        private void Start()
        {
            // Cache the elevator controller
            elevatorController = GetComponentInParent<ElevatorController>();
       
            if (externalButton)
            {
                // Since the external button changes depending on the floor the elevator is on we cache materials 
                // for further use
                workingMaterial = new Material(workingMaterial);
                notWorkingMaterial = new Material(notWorkingMaterial);
                // The button is working only if the current player floor is reacheable by the current elevator
                SetWorking(elevatorController.CanReachFloor(FloorManager.Instance.CurrentFloor));
                // Only external buttons must check every time the floor changes
                FloorManager.Instance.OnFloorChanged += () => { SetWorking(elevatorController.CanReachFloor(FloorManager.Instance.CurrentFloor));};
            }
            else // Internal button
            {
                // Get the button index
                for(int i=0; i<transform.parent.childCount && index<0; i++)
                {
                    if (transform.parent.GetChild(i) == transform)
                        index = i;           
                }
                // The button is working only if the corresponding floor is reacheable by the current elevator
                SetWorking(elevatorController.CanReachFloor(FloorManager.Instance.GetFloorAt(index)));
            }

           
        }

        void SetWorking(bool value)
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
            // Get the right destination depending on the button is internal or externale
            Floor destinationFloor = null;
            if (externalButton)
            {
                destinationFloor = FloorManager.Instance.CurrentFloor;
            }
            else
            {
                destinationFloor = FloorManager.Instance.GetFloorAt(index);
            }
            // Move the elevator
            elevatorController.MoveToFloor(destinationFloor);
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
