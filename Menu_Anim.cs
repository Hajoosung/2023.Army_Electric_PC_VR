using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Anim : MonoBehaviour
{
    public string Menu;
    Animator Anim;
    private void Start()
    {
        Menu = "Close";

        Anim = GetComponent<Animator>();
        Anim.SetTrigger("Close");
    }

    public void Menu_Btn()
    {
        if(Menu == "Close")
        {
            Anim.SetTrigger("Open");
            Menu = "Open";
        }
        else if(Menu == "Open")
        {
            Anim.SetTrigger("Close");
            Menu = "Close";
        }
    }
}
