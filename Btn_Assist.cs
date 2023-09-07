using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Btn_Assist : MonoBehaviour
{
    [Header("씬이동")]
    public string SceneName;

    [Header("메뉴On/Off")]
    public bool Menu_On;
    public GameObject Menu_Obj;
    public List<GameObject> Menu_List;
    public int Menu_Num;

    [Header("FinUI_Anim용")]
    public GameObject FinUI;
    public TMP_Text Fin_txt;

    [Header("순간 이동및 이동관련")]
    public bool Teleport; // Check시 텔레포트이동
    public float Spd = 0.5f;
    public Transform Move_Pos;

    [Header("NextStep에서 활용")]
    public List<GameObject> now_Step;
    public List<GameObject> Next_Step;
    [Header("Anim에서 활용")]
    public Animator Anim;
    public string Anim_Str;

    [Header("AnimEvent용으로 NextStep")]
    public List<GameObject> now_Step02;
    public List<GameObject> Next_Step02;

    public List<GameObject> now_Step03;
    public List<GameObject> Next_Step03;

    public List<GameObject> now_Step04;
    public List<GameObject> Next_Step04;

    public List<GameObject> now_Step05;
    public List<GameObject> Next_Step05;

    [Header("AnimEvent용으로 Anim_Trig")]
    public Animator Anim02;
    public string Anim_Str02;
    public Animator Anim03;
    public string Anim_Str03;
    public Animator Anim04;
    public string Anim_Str04;

    private void Start()
    {
        Menu_On = false;
    }
    public void Menu_Ctrl()
    {
        if(Menu_On == true) 
        {
            Menu_On = false;
            Menu_Obj.SetActive(false);
        }
        else 
        {
            Menu_On = true;
            Menu_Obj.SetActive(true);
        }
    }
    public void MenuSelect()
    {
        if (Menu_List.Count > 1)
        {
            for (int i = 0; i <= (Menu_List.Count - 1); i++)
            { if (Menu_List[i] != null) { Menu_List[i].SetActive(false); } }

            Menu_List[Menu_Num].SetActive(true);
        }
    }
    void FinUI_True(string str)
    {
        if (FinUI != null && Fin_txt != null)
        {
            Fin_txt.text = str;
            FinUI.SetActive(false);
            FinUI.SetActive(true);
        }
    }

    void FinTxtPlus_True(string str)
    {
        if (FinUI != null && Fin_txt != null)
        {
            Fin_txt.text = Fin_txt.text + "\n" + str;
            FinUI.SetActive(false);
            FinUI.SetActive(true);
        }
    }

    public void NextStep()
    {
        Invoke("NextStep_Main", 0.5f);
    }
    void NextStep_Main()
    {     
        if (now_Step.Count >= 1)
        {
            for (int i = 0; i <= (now_Step.Count - 1); i++)
            { now_Step[i].SetActive(false); }
        }

        if (Next_Step.Count >= 1)
        {
            for (int i = 0; i <= (Next_Step.Count - 1); i++)
            { Next_Step[i].SetActive(true); }
        }
    }
    void NextStep_02()
    {
        if (now_Step02.Count >= 1)
        {
            for (int i = 0; i <= (now_Step02.Count - 1); i++)
            { now_Step02[i].SetActive(false); }
        }

        if (Next_Step02.Count >= 1)
        {
            for (int i = 0; i <= (Next_Step02.Count - 1); i++)
            { Next_Step02[i].SetActive(true); }
        }
    }
    void NextStep_03()
    {
        if (now_Step03.Count >= 1)
        {
            for (int i = 0; i <= (now_Step03.Count - 1); i++)
            { now_Step03[i].SetActive(false); }
        }

        if (Next_Step03.Count >= 1)
        {
            for (int i = 0; i <= (Next_Step03.Count - 1); i++)
            { Next_Step03[i].SetActive(true); }
        }
    }
    void NextStep_04()
    {
        if (now_Step04.Count >= 1)
        {
            for (int i = 0; i <= (now_Step04.Count - 1); i++)
            { now_Step04[i].SetActive(false); }
        }

        if (Next_Step04.Count >= 1)
        {
            for (int i = 0; i <= (Next_Step04.Count - 1); i++)
            { Next_Step04[i].SetActive(true); }
        }
    }
    void NextStep_05()
    {
        if (now_Step05.Count >= 1)
        {
            for (int i = 0; i <= (now_Step05.Count - 1); i++)
            { now_Step05[i].SetActive(false); }
        }

        if (Next_Step05.Count >= 1)
        {
            for (int i = 0; i <= (Next_Step05.Count - 1); i++)
            { Next_Step05[i].SetActive(true); }
        }
    }

    public void Anim_Trigger()
    {
        if (Anim != null)
        { Anim.SetTrigger(Anim_Str); }
    }

    void Anim_Trig02()
    {
        if (Anim02 != null)
        { Anim02.SetTrigger(Anim_Str02); }
    }
    void Anim_Trig03()
    {
        if (Anim03 != null)
        { Anim03.SetTrigger(Anim_Str03); }
    }
    void Anim_Trig04()
    {
        if (Anim04 != null)
        { Anim04.SetTrigger(Anim_Str04); }
    }
}
