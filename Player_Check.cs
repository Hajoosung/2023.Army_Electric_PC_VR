using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Check : MonoBehaviour
{
    public string Select;

    public Transform Player;
    public string Platform;

    public List<GameObject> Pc_Obj;
    public List<GameObject> Vr_Obj;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        { Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); }

        if (Player != null)
        {
            if (Player.name.Contains("Vr") == true) { Platform = "Vr"; }
            else if (Player.name.Contains("Pc") == true) { Platform = "Pc"; }
        }

        if (Select == "")
        {
            if (Platform == "Vr")
            {
                for (int i = 0; i <= (Pc_Obj.Count - 1); i++)
                { Pc_Obj[i].SetActive(false); }

                for (int i = 0; i <= (Vr_Obj.Count - 1); i++)
                { Vr_Obj[i].SetActive(true); }
            }

            if (Platform == "Pc")
            {
                for (int i = 0; i <= (Vr_Obj.Count - 1); i++)
                { Vr_Obj[i].SetActive(false); }

                for (int i = 0; i <= (Pc_Obj.Count - 1); i++)
                { Pc_Obj[i].SetActive(true); }
            }
        }

        if(Select == "Pc")
        {
            for (int i = 0; i <= (Vr_Obj.Count - 1); i++)
            { Vr_Obj[i].SetActive(false); }

            for (int i = 0; i <= (Pc_Obj.Count - 1); i++)
            { Pc_Obj[i].SetActive(true); }
        }

        if(Select == "Vr")
        {
            for (int i = 0; i <= (Pc_Obj.Count - 1); i++)
            { Pc_Obj[i].SetActive(false); }

            for (int i = 0; i <= (Vr_Obj.Count - 1); i++)
            { Vr_Obj[i].SetActive(true); }
        }
    }
}
