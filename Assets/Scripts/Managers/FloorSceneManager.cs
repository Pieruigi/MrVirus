using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Virus.Builder;

namespace Virus
{
    public class FloorSceneManager : Singleton<FloorSceneManager>
    {
        public const string FloorRootTag = "SceneRoot";

        List<Scene> scenes = new List<Scene>();

        // Caching
        List<FloorController> controllers = new List<FloorController>();

        int startingFloorIndex = -1;
        int currentFloorIndex = -1;
       
        void ActivateFloor(int index, bool value)
        {
            Debug.Log($"Enabling root to floor '{scenes[index].name}':{value}");
            controllers[index].Activate(value);
            
        }

        IEnumerator InstantiateFloors(List<Floor> floors, int startingFloorIndex)
        {

            // Load all the scenes
            foreach (var floor in floors)
            {
                // Load scene in additive mode
                SceneManager.LoadScene(floor.GetSceneName(), LoadSceneMode.Additive);
            }


            Debug.Log("Scene:" + SceneManager.GetSceneAt(1).name);

            yield return null;

            this.startingFloorIndex = startingFloorIndex;
            //this.floors = floors;
            for (int i = 0; i < floors.Count; i++)
            {
                Scene floor = SceneManager.GetSceneAt(i + 1);
                scenes.Add(floor);
                controllers.Add(new List<GameObject>(floor.GetRootGameObjects()).Find(o => o.GetComponent<FloorController>()).GetComponent<FloorController>());
                controllers[controllers.Count - 1].Initialize(floors[i]);
                ActivateFloor(i, false);
            }

            // Activate the starting floor
            ActivateFloor(LevelBuilder.Instance.StartingFloorIndex, true);

        }

        public void ActivateFloor(int floorIndex)
        {
            if (currentFloorIndex >= 0)
                ActivateFloor(currentFloorIndex, false);

            currentFloorIndex = floorIndex;
            ActivateFloor(currentFloorIndex, true);
        }

        public void Initialize(List<Floor> floors, int startingFloorIndex)
        {
            StartCoroutine(InstantiateFloors(floors, startingFloorIndex));

        }
      
    }


    
}
