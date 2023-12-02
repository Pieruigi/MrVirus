using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virus.Builder;
using Virus.Scriptables;

namespace Virus
{
    public class ElevatorManager : Singleton<ElevatorManager>
    {

        List<ElevatorController> controllers = new List<ElevatorController>();

        public void Initialize(List<Elevator> elevators)
        {
            // Read assets by theme
            ElevatorAsset asset = Resources.LoadAll<ElevatorAsset>(System.IO.Path.Combine(ElevatorAsset.ResourceFolder, GameManager.Instance.Theme.ToString()))[0];
            GameObject elevatorPrefab = asset.ElevatorPrefab;
            GameObject elevatorSpawnGroupPrefab = asset.ElevatorSpawnGroupPrefab;

            // Instantiate the spawn group
            GameObject root = Instantiate(elevatorSpawnGroupPrefab, Vector3.zero, Quaternion.identity);

            // We need to instantiate and initialize each elevator
            for(int i=0; i<elevators.Count; i++)
            {
                // Instantiate geometry
                GameObject g = Instantiate(elevatorPrefab, root.transform.GetChild(i).position, root.transform.GetChild(i).rotation, root.transform);
                // Initialize elevator controller
                ElevatorController ctrl = g.GetComponent<ElevatorController>();
                ctrl.Initialize(elevators[i]);
                controllers.Add(ctrl);
            }
        }
    }

}
