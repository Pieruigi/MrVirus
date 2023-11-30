using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Virus.UI
{
    public class Cursor : MonoBehaviour
    {
        [SerializeField]
        Color interactableOverColor = Color.red;

        [SerializeField]
        Color defaultColor = Color.white;

        [SerializeField]
        float overTime = 1f;

        InteractionController interactionController;
        
        DateTime lastOverTime;
        Image image;
        bool isOver = false;

        // Start is called before the first frame update
        void Start()
        {
            interactionController = FindObjectOfType<InteractionController>();
            interactionController.OnInteractableOver += HandleOnInteractableOver;
            image = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isOver)
            {
                if((DateTime.Now - lastOverTime).TotalSeconds > overTime)
                {
                    isOver = false;
                    image.color = defaultColor;
                }
            }
        }

        void HandleOnInteractableOver()
        {
            lastOverTime = DateTime.Now;
            if (!isOver)
            {
                isOver = true;
                image.color = interactableOverColor;
            }
            
        }
    }

}
