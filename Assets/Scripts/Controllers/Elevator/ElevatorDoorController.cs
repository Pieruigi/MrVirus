using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Virus
{
    public class ElevatorDoorController : MonoBehaviour
    {
        [SerializeField]
        Animator doorAnimator;

        string doorOpenTrigger = "Open";
        string doorCloseTrigger = "Close";

        float openTime = .5f;
        float closeTime = .5f;

        bool isOpen = false;
        public bool IsOpen
        {
            get { return isOpen; }
        }

        System.DateTime lastOpenTime;
        float autoClosingTime = 10f;

       
        // Update is called once per frame
        void Update()
        {
            // Close door after a given amount of time
            if (isOpen && (System.DateTime.Now-lastOpenTime).TotalSeconds > autoClosingTime)
            {
                Close();
            }
        }

        public void Open()
        {
            if (isOpen)
                return;
            // Set the door open
            isOpen = true; 
            // The last time we opened the doot
            lastOpenTime = System.DateTime.Now;
            // Play opening animation
            doorAnimator.SetTrigger(doorOpenTrigger);
            
        }

        public async void Close()
        {
            if (!isOpen)
                return;
            // Set the door closed
            isOpen = false;
            // Play animation
            doorAnimator.SetTrigger(doorCloseTrigger);
            // We may need to wait for the door to physically close, for example before the elevator can move from one
            // floor to another
            await Task.Delay(System.TimeSpan.FromSeconds(closeTime));
        }
    }

}
