using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timescale : MonoBehaviour
{
    public float time;

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = time;
    }
}
