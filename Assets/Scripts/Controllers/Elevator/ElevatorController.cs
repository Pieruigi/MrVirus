
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public Floor ElevatorFloor
        {
            get { return elevatorFloor; }
        }

        ElevatorDoorController doorController;
        float timePerFloor = 2f;
        bool isMoving = false;

        private void Awake()
        {
            externalButtonController = GetComponentsInChildren<ElevatorButtonController>().Where(b => b.External).First();
            internalButtonsControllers = new List<ElevatorButtonController>(GetComponentsInChildren<ElevatorButtonController>().Where(b => !b.External));
        }

        private void Start()
        {
            doorController = GetComponentInChildren<ElevatorDoorController>();
          
        }

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
            // Initialize buttons
            // Check the external button
            externalButtonController.SetWorking(elevator.Floors.Contains(FloorManager.Instance.CurrentFloor));
            // The internal button only once
            for(int i=0; i<internalButtonsControllers.Count; i++)
            {
                if (i >= FloorManager.Instance.FloorCount)
                    internalButtonsControllers[i].SetWorking(false);
                else
                    internalButtonsControllers[i].SetWorking(elevator.Floors.Contains(FloorManager.Instance.GetFloorAt(i)));
            }
                
           
        }

        public void ProcessElevatorButton(ElevatorButtonController buttonController)
        {
            if (buttonController != externalButtonController && !internalButtonsControllers.Contains(buttonController))
            {
                Debug.LogWarning($"No button found");
                return;
            }

            if (buttonController == externalButtonController)
                ProcessExternalButton();
            else
                ProcessInternalButton(buttonController);

        }

        /// <summary>
        /// Used when the elevator is on a different floor.
        /// </summary>
        async void ProcessExternalButton()
        {
            Debug.Log($"Call elevator {elevator.name}");

            if (!elevator.Floors.Contains(FloorManager.Instance.CurrentFloor) || isMoving)
                return; 

            if(FloorManager.Instance.CurrentFloor == elevatorFloor)
            {
                // If door is close then open it
                if (!doorController.IsOpen)
                    doorController.Open();
            }
            else
            {
                isMoving = true;
                // We can play some effect here
                // Get the current elevator floor index
                int floorDiff = FloorManager.Instance.GetFloorIndex(elevatorFloor) - FloorManager.Instance.GetFloorIndex(FloorManager.Instance.CurrentFloor);                   
                float time = timePerFloor * Mathf.Abs(floorDiff);
                Debug.Log($"Elevator called at {System.DateTime.Now}");
                await Task.Delay(System.TimeSpan.FromSeconds(time));
                Debug.Log($"Elevator arrived at {System.DateTime.Now}");
                // The elevator is on place
                elevatorFloor = FloorManager.Instance.CurrentFloor;
                isMoving = false;
                // Open door
                doorController.Open();
            }

            
        }

        void ProcessInternalButton(ElevatorButtonController buttonController)
        {
            Floor newFloor = FloorManager.Instance.GetFloorAt(internalButtonsControllers.IndexOf(buttonController));
            Debug.Log($"Move elevator {elevator.name} from {elevatorFloor} to {newFloor}");

            if (newFloor == elevatorFloor || !elevator.Floors.Contains(newFloor) || isMoving)
                return;

            // Add some effect
            elevatorFloor = newFloor;
            // We need to activate the new floor
            FloorManager.Instance.ActivateFloor(elevatorFloor);
        }

        public void OpenDoors()
        {
            Debug.Log("Open Doors");
            doorController.Open();
        }

        public void CloseDoors()
        {
            doorController.Close();
        }

    }

}
