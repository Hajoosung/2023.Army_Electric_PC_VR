using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset_Ctrl : MonoBehaviour
{
    public string Reset;

    public List<GameObject> True_Obj;
    public List<GameObject> False_Obj;

    public List<GameObject> Btn_True_Obj;
    public List<GameObject> Btn_False_Obj;

    // Start is called before the first frame update
    void Start()
    {
        Reset = "False";
    }
    private void OnEnable()
    {
        Reset = "False";
    }

    // Update is called once per frame
    void Update()
    {
        if (Reset == "False")
        {
            if (True_Obj.Count >= 1)
            {
                for (int i = 0; i <= (True_Obj.Count - 1); i++)
                { True_Obj[i].SetActive(true); }
            }
            if (False_Obj.Count >= 1)
            {
                for (int i = 0; i <= (False_Obj.Count - 1); i++)
                { False_Obj[i].SetActive(false); }
            }
            Reset = "True";
        }
    }

    public void BtnCheck()
    {
        if (Btn_True_Obj.Count >= 1)
        {
            for (int i = 0; i <= (Btn_True_Obj.Count - 1); i++)
            { Btn_True_Obj[i].SetActive(true); }
        }
        if (Btn_False_Obj.Count >= 1)
        {
            for (int i = 0; i <= (Btn_False_Obj.Count - 1); i++)
            { Btn_False_Obj[i].SetActive(false); }
        }
    }
}
