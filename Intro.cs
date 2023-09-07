using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    public VideoPlayer Vp;
    public GameObject Video_Fin;
    public GameObject Fin_UI;
    // Start is called before the first frame update
    void Start()
    {
        Video_Fin.SetActive(false);
        Fin_UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Video_Fin.gameObject.activeSelf == false)
        {
            if (Vp.time / Vp.length >= 0.9)
            {Video_Fin.gameObject.SetActive(true); }
        }
    }

    public void FinUI_On()
    { Fin_UI.gameObject.SetActive(true); }

    public void FinUI_Off()
    { Fin_UI.gameObject.SetActive(false); }
}
