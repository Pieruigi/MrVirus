using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Virus.Builder;

namespace Virus
{
    public class ElevatorController : MonoBehaviour
    {

        //[SerializeField]
        ElevatorButtonController externalButtonController;

        //[SerializeField]
        List<ElevatorButtonController> internalButtonsControllers; 

        Elevator elevator;

        Floor elevatorFloor = null; // The actual floor the elevator is on

        private void Awake()
        {
            externalButtonController = GetComponentsInChildren<ElevatorButtonController>().Where(b => b.External).First();
            internalButtonsControllers = new List<ElevatorButtonController>(GetComponentsInChildren<ElevatorButtonController>().Where(b => !b.External));
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (elevator.name.EndsWith("-0"))
            {
                if (Input.GetKeyDown(KeyCode.KeypadPlus))
                {
                    if (elevatorFloor != elevator.Floors[elevator.Floors.Count - 1])
                    {
                        int index = new List<Floor>(elevator.Floors).IndexOf(elevatorFloor);
                        index++;
                        MoveToFloor(elevator.Floors[index]);
                    }
                }
                if (Input.GetKeyDown(KeyCode.KeypadMinus))
                {
                    if (elevatorFloor != elevator.Floors[0])
                    {
                        int index = new List<Floor>(elevator.Floors).IndexOf(elevatorFloor);
                        index--;
                        MoveToFloor(elevator.Floors[index]);
                    }
                }
                if (Input.GetKeyDown(KeyCode.KeypadMultiply))
                {
                    CallElevator();
                }
            }
            
        }
#endif

        public void Initialize(Elevator elevator)
        {
            this.elevator = elevator;
            // Set the current floor randomly choosing among all recheable floors 
            Debug.Log("Elevator Floor Count:" + elevator.Floors.Count);
            elevatorFloor = elevator.Floors[Random.Range(0, elevator.Floors.Count)];
            Debug.Log($"{name} Floor:{elevatorFloor}" );
            // Initialize buttons
            // The external button will be checke floor by floor
            externalButtonController.SetWorking(elevatorFloor == FloorManager.Instance.CurrentFloor);
            // The internal button only once
            for(int i=0; i<internalButtonsControllers.Count; i++)
            {
                if (i >= FloorManager.Instance.FloorCount)
                    internalButtonsControllers[i].SetWorking(false);
                else
                    internalButtonsControllers[i].SetWorking(elevator.Floors.Contains(FloorManager.Instance.GetFloorAt(i)));
            }
                
           
        }

        /// <summary>
        /// Used when the elevator is on a different floor.
        /// </summary>
        void CallElevator()
        {
            Debug.Log($"Call elevator {elevator.name}");

            if (FloorManager.Instance.CurrentFloor == elevatorFloor || !elevator.Floors.Contains(FloorManager.Instance.CurrentFloor))
                return; 

            // We can play some effect here
            elevatorFloor = FloorManager.Instance.CurrentFloor;
        }

        void MoveToFloor(Floor newFloor)
        {
            Debug.Log($"Move elevator {elevator.name} from {elevatorFloor} to {newFloor}");

            if (newFloor == elevatorFloor || !elevator.Floors.Contains(newFloor))
                return;

            // Add some effect
            elevatorFloor = newFloor;
            // We need to activate the new floor
            FloorManager.Instance.ActivateFloor(elevatorFloor);
        }
    }

}
