using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition != new Vector3(0,0,0))
        { transform.localPosition = new Vector3(0, 0, 0); }

        if (transform.localEulerAngles != new Vector3(0, 0, 0))
        { transform.localEulerAngles = new Vector3(0, 0, 0); }
    }
}
