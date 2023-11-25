using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Virus.Builder;

namespace Virus
{
    public class FloorManager : Singleton<FloorManager>
    {
        List<Scene> floors = new List<Scene>();

        int currentFloorIndex = -1;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        private void OnEnable()
        {
            GameManager.Instance.OnFloorsLoaded += HandleOnFloorsLoaded;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnFloorsLoaded -= HandleOnFloorsLoaded;
        }

        void HandleOnFloorsLoaded(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Scene floor = SceneManager.GetSceneAt(i + 1);
                floors.Add(floor);
                SetFloorEnabled(floor, false);
            }

            
        }

        void SetFloorEnabled(Scene floor, bool enabled)
        {
            Debug.Log($"Enabling root to floor '{floor.name}':{enabled}");
            foreach (var r in floor.GetRootGameObjects())
                Debug.Log("r.name:" + r.name);
            new List<GameObject>(floor.GetRootGameObjects()).Find(o => o.CompareTag("SceneRoot")).SetActive(enabled);
        }

        public void ActivateFloor(int floorIndex)
        {
            if (currentFloorIndex >= 0)
                SetFloorEnabled(floors[currentFloorIndex], false);

            currentFloorIndex = floorIndex;
            SetFloorEnabled(floors[currentFloorIndex], true);
        }

      
    }


    
}
