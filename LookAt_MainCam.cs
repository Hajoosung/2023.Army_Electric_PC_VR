using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt_MainCam : MonoBehaviour
{
    Transform MainCam;
    void Start()
    {
        if(Camera.main != null)
        { MainCam = Camera.main.transform; }
    }

    // Update is called once per frame
    void Update()
    {
        if(MainCam != null)
        {
            transform.LookAt(MainCam);
        }
    }
}
