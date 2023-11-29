using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Virus
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        [SerializeField]
        GameObject player;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void MoveToSpawnPoint()
        {
            Transform sp = FloorManager.Instance.GetSpawnPoint();
            GetPlayer().transform.position = sp.position;
            GetPlayer().transform.rotation = sp.rotation;
        }

        public GameObject GetPlayer()
        {
            return player;
        }
    }

}
