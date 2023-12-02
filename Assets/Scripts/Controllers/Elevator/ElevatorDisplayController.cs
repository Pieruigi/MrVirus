using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Virus.Builder;

namespace Virus
{
    public class ElevatorDisplayController : MonoBehaviour
    {
        [SerializeField]
        Color unreacheableColor;

        [SerializeField]
        Color reacheableColor;

        ElevatorController elevatorController;

        int index = -1;
        bool reacheable = false;

        TMP_Text displayText;
        Floor floor;

        Vector3 scaleDefault;
        float scaleMul = 1.2f;
        bool selected = false;

        // Start is called before the first frame update
        void Start()
        {
            // Get the elevator controller
            elevatorController = GetComponentInParent<ElevatorController>();

            // Get the display text and set the default scale
            displayText = GetComponentInChildren<TMP_Text>();
            scaleDefault = displayText.transform.localScale;

            // Get the corresponding index
            for (int i=0; i<transform.parent.childCount && index<0; i++)
            {
                if (transform.parent.GetChild(i) == transform)
                    index = i;
            }

            // Check if the corresponding floor is reacheable by the elevator
            Floor floor = FloorManager.Instance.GetFloorAt(index);
            SetReacheable(elevatorController.CanReachFloor(floor));

            if (reacheable)
            {
                // Update display
                UpdateDisplay(elevatorController.ElevatorFloor);
                // Register event
                elevatorController.OnFloorReached += (floor, reached) => { UpdateDisplay(floor); };
            }
                 
        }

        // Update is called once per frame
        void Update()
        {

        }

        void UpdateDisplay(Floor elevatorFloor)
        {
            if(elevatorFloor == FloorManager.Instance.GetFloorAt(index))
            {
                if (!selected)
                {
                    selected = true;
                    // Current floor
                    displayText.transform.DOScale(scaleDefault * scaleMul, 0.5f);
                }
                
            }
            else
            {
                // Another floor
                if (selected)
                {
                    selected = false;
                    displayText.transform.DOScale(scaleDefault, 0.5f);
                }
            }
        }
       
        void SetReacheable(bool value)
        {
            reacheable = value;
            if (reacheable)
                displayText.color = reacheableColor;
            else
                displayText.color = unreacheableColor;
        }
    }

}
