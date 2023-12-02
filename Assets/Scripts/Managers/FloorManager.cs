using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Virus.Builder;

namespace Virus
{
    public class FloorManager : Singleton<FloorManager>
    {
        
        public UnityAction OnFloorChanged;

        List<Scene> scenes = new List<Scene>();

        // Caching
        List<FloorController> controllers = new List<FloorController>();
        //List<Floor> floors = new List<Floor>();

        int startingFloorIndex = -1;
        int currentFloorIndex = -1;

        public int FloorCount
        {
            get { return controllers.Count; }
        }

        public Floor CurrentFloor
        {
            get { return controllers[currentFloorIndex].Floor; }
        }

        void SetFloorActive(int index, bool value)
        {
            controllers[index].Activate(value);
            
        }

        public IEnumerator Initialize(List<Floor> floors, int startingFloorIndex)
        {
            //this.floors = floors;
            // Load all the scenes
            foreach (var floor in floors)
            {
                // Load scene in additive mode
                SceneManager.LoadScene(floor.GetSceneName(), LoadSceneMode.Additive);
            }


          
            yield return null;
            

            this.startingFloorIndex = startingFloorIndex;
            //this.floors = floors;
            for (int i = 0; i < floors.Count; i++)
            {
                Scene floor = SceneManager.GetSceneAt(i + 1);
                scenes.Add(floor);
                controllers.Add(new List<GameObject>(floor.GetRootGameObjects()).Find(o => o.GetComponent<FloorController>()).GetComponent<FloorController>());
                controllers[controllers.Count - 1].Initialize(floors[i]);
                SetFloorActive(i, false); // We deactivate all the floor
            }

            // Activate the starting floor
            ActivateFloor(startingFloorIndex);

        }

        void ActivateFloor(int floorIndex)
        {
            if (currentFloorIndex >= 0)
                SetFloorActive(currentFloorIndex, false);

            Debug.Log($"Setting current floor index to {floorIndex}");
            currentFloorIndex = floorIndex;
            SetFloorActive(currentFloorIndex, true);
            OnFloorChanged?.Invoke();
        }

     

        public void ActivateFloor(Floor floor)
        {
            ActivateFloor(GetFloorIndex(floor));
        }

       

        public int GetFloorIndex(Floor floor)
        {
            return controllers.FindIndex(c => c.Floor == floor);
        }

        public Floor GetFloorAt(int index)
        {
            if(index < controllers.Count)
                return controllers[index].Floor;

            return null;
        }

        public Transform GetSpawnPoint()
        {
            return controllers[startingFloorIndex].PlayerSpawnPoint;
        }

    }


    
}
