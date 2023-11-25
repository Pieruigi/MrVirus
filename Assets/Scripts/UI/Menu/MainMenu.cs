using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Virus.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        List<GameObject> panels;

        int defaultPanelIndex = 0;

        // Start is called before the first frame update
        void Start()
        {
            ShowPanel(defaultPanelIndex);
        }

      
        void HideAll()
        {
            foreach(var panel in panels)
            {
                panel.SetActive(false);
            }
        }

        public void ShowPanel(int index)
        {
            HideAll();
            panels[index].SetActive(true);
        }
    }

}
