using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virus.Builder;

namespace Virus
{
    public class ElevatorController : MonoBehaviour
    {
        Elevator elevator;

        public void Initialize(Elevator elevator)
        {
            this.elevator = elevator;
        }
    }

}
