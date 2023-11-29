using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Virus
{
    public class PlayerController : MonoBehaviour
    {
        CharacterController cc;

        Vector3 targetVelocity;
        float acce = 20f;
        float maxSpeed = 4;
        float runMul = 3f;
        float crouchMul = .5f;

        bool running = false;
        bool crouching = false;

        private void Awake()
        {
            cc = GetComponent<CharacterController>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            CheckInput();
        }


        void CheckInput()
        {
            if (Input.GetKeyDown(KeyMapping.MoveForward))
            {
                targetVelocity = transform.forward;
            }
            else
            {
                if(Input.GetKeyDown(KeyMapping.MoveBackward))
                    targetVelocity = -transform.forward;
            }

            if (Input.GetKeyDown(KeyMapping.StrafeRight))
            {
                targetVelocity = transform.right;
            }
            else
            {
                if (Input.GetKeyDown(KeyMapping.StrafeLeft))
                    targetVelocity = -transform.right;
            }

            

        }
    }

}
