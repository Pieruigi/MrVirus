using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Virus.Builder;
using Virus.Enumerators;

namespace Virus
{
    public class GameManager : Singleton<GameManager>
    {
        public UnityAction<int> OnFloorsLoaded;

        
        bool inGame = false;
        bool loading = false;

        int menuSceneBuildIndex = 0;
        int gameSceneBuildIndex = 1;

        int currentSeed;

        Theme theme = Theme.Default;
        public Theme Theme
        {
            get { return theme; }
        }



        public void StartGame()
        {
            int seed = (int)DateTime.Now.Ticks;
            StartGame(seed);
        }


        public void StartGame(int seed)
        {
            
            if (inGame || loading)
                return;

            loading = true;
            
            SceneManager.LoadSceneAsync(gameSceneBuildIndex, LoadSceneMode.Single).completed += (op) => 
            {
                loading = false;
                inGame = true;

                Debug.Log($"Game scene loaded, initializing random state with seed {seed}");
                // Set the seed and build the level
                UnityEngine.Random.InitState(seed);
                currentSeed = seed;
                LevelBuilder.Instance.Build();
                // Load actual scenes
                LoadFloors();
            };
        }

        void LoadFloors()
        {

            
            // Load all the scenes
            foreach(var floor in LevelBuilder.Instance.Floors)
            {
                // Load scene in additive mode
                SceneManager.LoadScene(floor.GetSceneName(), LoadSceneMode.Additive);
            
            }

            Debug.Log("Scene:" + SceneManager.GetSceneAt(1).name);

            OnFloorsLoaded?.Invoke(LevelBuilder.Instance.Floors.Count);
           
        }

        
    }

}
