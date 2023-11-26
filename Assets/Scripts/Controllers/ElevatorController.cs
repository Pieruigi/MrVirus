using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virus.Builder;

namespace Virus
{
    public class ElevatorController : MonoBehaviour
    {
        Elevator elevator;

        Floor currentFloor = null; // The actual floor the elevator stands in

#if UNITY_EDITOR
        private void Update()
        {
            if (elevator.name.EndsWith("-0"))
            {
                if (Input.GetKeyDown(KeyCode.KeypadPlus))
                {
                    if (currentFloor != elevator.Floors[elevator.Floors.Count - 1])
                    {
                        int index = new List<Floor>(elevator.Floors).IndexOf(currentFloor);
                        index++;
                        MoveToFloor(elevator.Floors[index]);
                    }
                }
                if (Input.GetKeyDown(KeyCode.KeypadMinus))
                {
                    if (currentFloor != elevator.Floors[0])
                    {
                        int index = new List<Floor>(elevator.Floors).IndexOf(currentFloor);
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
            currentFloor = elevator.Floors[Random.Range(0, elevator.Floors.Count)];

        }

        /// <summary>
        /// Used when the elevator is on a different floor.
        /// </summary>
        void CallElevator()
        {
            Debug.Log($"Call elevator {elevator.name}");

            if (FloorManager.Instance.CurrentFloor == currentFloor)
                return; 

            // We can play some effect here
            currentFloor = FloorManager.Instance.CurrentFloor;
        }

        void MoveToFloor(Floor newFloor)
        {
            Debug.Log($"Move elevator {elevator.name} from {currentFloor} to {newFloor}");

            if (newFloor == currentFloor)
                return;

            // Add some effect
            currentFloor = newFloor;
            // We need to activate the new floor
            FloorManager.Instance.ActivateFloor(currentFloor);
        }
    }

}
