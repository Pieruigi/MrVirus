using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SwitchEmitTest : MonoBehaviour
{
    Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();

        rend.material = new Material(rend.material);
    }

    // Start is called before the first frame update
    void Start()
    {

    }
     
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            DynamicGI.SetEmissive(rend, Color.yellow * 10f);
            DynamicGI.UpdateEnvironment();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            DynamicGI.SetEmissive(rend, Color.red * 10f);
            DynamicGI.UpdateEnvironment();
        }
    }
}
