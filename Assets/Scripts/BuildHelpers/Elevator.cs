using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Virus.Builder
{
    /// <summary>
    /// Elevators start from the left side, this means elevators[0] is the elevator to the left, the middle one is on index 1
    /// and the one to the right has index 2.
    /// </summary>
    public class Elevator: MonoBehaviour
    {
        // The list of floors the elevator can reach
       
        List<Floor> floors = new List<Floor>();

       
        /// <summary>
        /// Add a floor that is reacheable by this elevator
        /// </summary>
        public void AddFloor(Floor floor)
        {
            // Ordering
            int id = int.Parse(floor.gameObject.name.Substring(floor.gameObject.name.IndexOf("-") + 1));
            int index = -1;
            for(int i=0; i<floors.Count && index < 0; i++)
            {
                int otherId = int.Parse(floors[i].gameObject.name.Substring(floors[i].gameObject.name.IndexOf("-") + 1));
                if (otherId > id)
                    index = i;
            }
            if (index < 0)
                floors.Add(floor);
            else
                floors.Insert(index, floor);
        }

        
    }

}
