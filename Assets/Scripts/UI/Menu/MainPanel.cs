using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Virus.UI
{
    public class MainPanel : MonoBehaviour
    {
        [SerializeField]
        Button playButton;

        private void Awake()
        {
            playButton.onClick.AddListener(PlayGame);
        }

        void PlayGame()
        {
            Debug.Log($"Starting a new game");
            GameManager.Instance.StartGame();
        }
    }

}
