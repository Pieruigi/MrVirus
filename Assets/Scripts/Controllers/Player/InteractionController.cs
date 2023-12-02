using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Virus.Interfaces;

namespace Virus
{
    public class InteractionController : MonoBehaviour
    {
        public UnityAction OnInteractableOver;
   
        [SerializeField]
        float range = 1.5f;

        IInteractable interactable;
    

        // Update is called once per frame
        void Update()
        {
            
            CheckInput();
        }

        
        void CheckInput()
        {
            // Camera raycast
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo;
            IInteractable hit = null;
            
            if(Physics.Raycast(ray, out hitInfo, range))
            {
                // Try to get the interactable interface if any
                hit = hitInfo.collider.GetComponent<IInteractable>();
            }

         
            // If we were interacting with something then we stop interacting
            if(interactable != null && interactable != hit)
            {
                interactable.StopInteraction();
                interactable = null;
            }

            // Check the interactable object if any
            if (hit != null)
            {
                if(interactable == null && hit.IsInteractable())
                {
                    // Can be used by the ui to show some clue
                    OnInteractableOver?.Invoke();

                    // Check the mouse button down
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log($"Interacting with object {(hit as MonoBehaviour).name}");
                        interactable = hit;
                        interactable.StartInteraction();
                    }
                   
                }
                else // Interactable is not null
                {
                    // Check the mouse button up
                    if (Input.GetMouseButtonUp(0))
                    {
                        interactable.StopInteraction();
                        interactable = null;
                    }
                }
                

                
            }


        }
    }

}
