using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Virus.Interfaces
{
    public interface IInteractable
    {
        void StartInteraction();

        void StopInteraction();

        bool IsInteractable();
    }

}
