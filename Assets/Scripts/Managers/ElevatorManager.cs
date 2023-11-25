using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virus.Builder;

namespace Virus
{
    public class ElevatorManager : Singleton<ElevatorManager>
    {
        [SerializeField]
        GameObject elevatorPrefab;

        List<ElevatorController> controllers = new List<ElevatorController>();

        public void Initialize(List<Elevator> elevators)
        {
            GameObject root = new GameObject("Elevators");

            // We need to instantiate and initialize each elevator
            foreach(var elevator in elevators)
            {
                GameObject g = Instantiate(elevatorPrefab, root.transform);
                ElevatorController ctrl = g.GetComponent<ElevatorController>();
                ctrl.Initialize(elevator);
                controllers.Add(ctrl);
            }
        }
    }

}
