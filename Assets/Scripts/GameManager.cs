using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Virus.Builder;

namespace Virus
{
    public class GameManager : Singleton<GameManager>
    {
        
        bool inGame = false;
        bool loading = false;

        int menuSceneBuildIndex = 0;
        int gameSceneBuildIndex = 1;

        int currentSeed;

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
                UnityEngine.Random.InitState(seed);
                currentSeed = seed;
                LevelBuilder.Instance.Build();        
            };
        }
    }

}
