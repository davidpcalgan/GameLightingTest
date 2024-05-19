using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{

    public static bool isPaused;
    
    public static float deltaTime
    {
        get
        {
            if (isPaused)
            {
                return 0;
            } else {
                return Time.deltaTime;
            }
        }
        set
        {
            deltaTime = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
