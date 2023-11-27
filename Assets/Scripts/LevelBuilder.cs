using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Virus.Scriptables;

namespace Virus.Builder
{
    public class LevelBuilder : Singleton<LevelBuilder>
    {
        List<Floor> floors = new List<Floor>();
        //public IList<Floor> Floors
        //{
        //    get { return floors.AsReadOnly(); }
        //}

        List<Elevator> elevators = new List<Elevator>();
        

        int startingFloorIndex = -1;
        public int StartingFloorIndex
        {
            get { return startingFloorIndex; }
        }

        int minFloorCount = 6;
        int maxFloorCount = 8;
        int elevatorCount = 3;
        int maxUndergroundFloors = 2;

        string themeName = "DefaultTheme";


#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                // Clear all
                floors.Clear();
                elevators.Clear();
                startingFloorIndex = -1;
                int count = transform.childCount;
                for (int i = 0; i < count; i++)
                    DestroyImmediate(transform.GetChild(0).gameObject);
                // Build new
                UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
                LevelBuilder.Instance.Build();
            }
        }
#endif
        public void Build()
        {
            Initialize();

            CreateLogicBuild();

            LoadAssets();

            StartCoroutine(CreateGeometry());
        }

        IEnumerator CreateGeometry()
        {
            yield return FloorManager.Instance.Initialize(floors, startingFloorIndex);

            ElevatorManager.Instance.Initialize(elevators);
        }

        void Initialize()
        {
            themeName = GameManager.Instance.Theme.ToString();
        }

        void CreateLogicBuild()
        {
            Debug.Log($"Building level...");
            // Create elevators
            // Each elevator can reach multiple floors, maybe skipping the ones in the middle, it depends on the topology
            for(int i=0; i<elevatorCount; i++)
            {
                GameObject g = new GameObject($"Elevator-{i}");
                g.transform.parent = transform;
                Elevator c = g.AddComponent<Elevator>();
                elevators.Add(c);
            }

            // Create floors
            int count = Random.Range(minFloorCount, maxFloorCount + 1);
            for(int i=0; i<count; i++)
            {
                GameObject g = new GameObject($"Floor-{i}");
                g.transform.parent = transform;
                Floor c = g.AddComponent<Floor>();
                floors.Add(c);
            }

            // Connect all floors together
            List<Floor> notConnectedFloors = new List<Floor>(floors.ToArray());
            // Choose the floor to start with
            Floor nextFloor = notConnectedFloors[Random.Range(0, notConnectedFloors.Count)];
            notConnectedFloors.Remove(nextFloor);
            // Choose the elevator
            Elevator nextElevator = elevators[Random.Range(0, elevators.Count)];
            // Add the selected floor to the selected elevator
            nextElevator.AddFloor(nextFloor);
            
            // Last elevator 
            Elevator lastElevator = null;

            List<Elevator> usedElevators = new List<Elevator>();
            usedElevators.Add(nextElevator);

            // Loop through all the remaining floors
            while(notConnectedFloors.Count > 0)
            {
                Debug.Log("NotConnected:" + notConnectedFloors.Count);
                // Get a new floor to connect to the last selected
                nextFloor = notConnectedFloors[Random.Range(0, notConnectedFloors.Count)];
                notConnectedFloors.Remove(nextFloor);
                // Connect both floors
                nextElevator.AddFloor(nextFloor);
                // Get any elevator different from the last two used elevators
                List<Elevator> tmpElevators = elevators.FindAll(e => e != nextElevator && e != lastElevator);
                lastElevator = nextElevator;
                nextElevator = tmpElevators[Random.Range(0, tmpElevators.Count)];

                // An elevator that has already been used means the next floor is reacheable from a previously
                // connected floor; in this way we can have less connections
                if (!usedElevators.Contains(nextElevator))
                {
                    usedElevators.Add(nextElevator);
                    // Add the new floor to the new elevator
                    nextElevator.AddFloor(nextFloor);
                }
                
            }

            // Choose the starting floor
            startingFloorIndex = Random.Range(0, maxUndergroundFloors + 1);
        }

        /// <summary>
        /// Once we created the logic build we need to choose the type of floors we want to load
        /// </summary>
        void LoadAssets()
        {
            // Load all the assets 
            List<FloorAsset> floorAssets = new List<FloorAsset>(Resources.LoadAll<FloorAsset>(System.IO.Path.Combine(FloorAsset.ResourceFolder, themeName)));
            Debug.Log($"{floorAssets.Count} floor assets found");
            // Choose random floors
            for(int i=0; i<floors.Count; i++)
            {
                List<FloorAsset> tmpList = null;
                
                // Fill the temp list with the right assets, depending on the floor type
                if (i < startingFloorIndex)
                {
                    tmpList = floorAssets.FindAll(f => f.UndergroundFloor);
                }
                else
                {
                    if(i == startingFloorIndex)
                    {
                        tmpList = floorAssets.FindAll(f => f.StartingFloor);
                    }
                    else
                    {
                        tmpList = floorAssets.FindAll(f => f.NormalFloor);
                    }
                }

                // Manage weights
                int count = tmpList.Count;
                for(int j=0; j<count; j++)
                {
                    for (int k = 0; k < tmpList[j].Weight; k++)
                        tmpList.Add(tmpList[j]);
                }

                // Init logic floor
                floors[i].Init(tmpList[Random.Range(0, tmpList.Count)]);
            }

        }

        
    }

}
