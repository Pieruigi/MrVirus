
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Virus.Builder;

namespace Virus
{
    public class ElevatorController : MonoBehaviour
    {
        /// <summary>
        /// Called when the elevator is moving
        /// Param1: the current floor
        /// Param2: true is the destination has been reached, otherwise false
        /// </summary>
        public UnityAction<Floor, bool> OnFloorReached; 
        

     
        Elevator elevator;

        Floor elevatorFloor = null; // The actual floor the elevator is on
        public Floor ElevatorFloor
        {
            get { return elevatorFloor; }
        }

        //ElevatorDoorController doorController;
        float timePerFloor = 2f;
        bool isMoving = false;

        ElevatorDoorController doorController;

        

#if UNITY_EDITOR
        private void Update()
        {
            //if (elevator.name.EndsWith("-0"))
            //{
            //    if (Input.GetKeyDown(KeyCode.KeypadPlus))
            //    {
            //        if (elevatorFloor != elevator.Floors[elevator.Floors.Count - 1])
            //        {
            //            int index = new List<Floor>(elevator.Floors).IndexOf(elevatorFloor);
            //            index++;
            //            MoveToFloor(elevator.Floors[index]);
            //        }
            //    }
            //    if (Input.GetKeyDown(KeyCode.KeypadMinus))
            //    {
            //        if (elevatorFloor != elevator.Floors[0])
            //        {
            //            int index = new List<Floor>(elevator.Floors).IndexOf(elevatorFloor);
            //            index--;
            //            MoveToFloor(elevator.Floors[index]);
            //        }
            //    }
            //    if (Input.GetKeyDown(KeyCode.KeypadMultiply))
            //    {
            //        CallElevator();
            //    }
            //}
            
        }
#endif

        public void Initialize(Elevator elevator)
        {
            this.elevator = elevator;
            // Set the current floor randomly choosing among all recheable floors 
            elevatorFloor = elevator.Floors[Random.Range(0, elevator.Floors.Count)];
            
        }

        public void SetDoorController(ElevatorDoorController doorController)
        {
            this.doorController = doorController;
        }

        public async void MoveToFloor(Floor destinationFloor)
        {
            // Floor not reacheable
            if (!CanReachFloor(destinationFloor) || isMoving)
                return;

            if(destinationFloor == elevatorFloor)
            {
                // Already on destination, just open the door
                OnFloorReached?.Invoke(destinationFloor, true);
                return;
            }

            
            // Await for door to close if needed
            if (doorController)
                await doorController.Close();

            // Let's move
            isMoving = true;
            int startingFloorIndex = FloorManager.Instance.GetFloorIndex(elevatorFloor);
            int destinationFloorIndex = FloorManager.Instance.GetFloorIndex(destinationFloor);
            int steps = destinationFloorIndex - startingFloorIndex;
            int dir = (int)Mathf.Sign(steps); // >0: up, <0: down
            steps = Mathf.Abs(steps);
            while(steps > 0)
            {
                await Task.Delay(System.TimeSpan.FromSeconds(timePerFloor));
                // New floor reached
                steps--;
                if (steps == 0)
                    OnFloorReached?.Invoke(destinationFloor, true);
                else
                    OnFloorReached?.Invoke(FloorManager.Instance.GetFloorAt(destinationFloorIndex - dir * steps), false);
            }
            elevatorFloor = destinationFloor;
            isMoving = false;
            // Change floor
            FloorManager.Instance.ActivateFloor(elevatorFloor);

        }


        public bool CanReachFloor(Floor floor)
        {
            if (!floor)
                return false;

            return elevator.Floors.Contains(floor);
        }
    }

}
