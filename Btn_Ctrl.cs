using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class Btn_Ctrl : MonoBehaviour
{
    H_Step_Mgr StepMgr;
    //T_Mission Tmission;

    public enum Class { Btn, Coll }
    public Class Type;

    public enum Btn_Axis { X_Axis, Y_Axis, Z_Axis };
    public Btn_Axis Btn_axis;

    AudioSource As;
    public AudioClip Btn_Click;

    public bool Clicked;
    public enum Case
    {
        None,
        Calibration, // 추가: Haptic_Menu 클릭시 햅틱센서 리셋및 새로고침?
        Count01, // 클릭시 StepMgr의 Count가 오른다.(중복클릭은 안된다.)
        Next_Step, //추가:Btn_Assist 클릭시 now_Step을 false, Next_Step을 True로 합니다.// Move_Pos추가시 이동가능 // Anim추가시 Anim 기능
        Error, // 클릭시 진동, Error_UI에 UI추가가능, 추가: Btn_Assist시 Anim기능을 가져올 수 있습니다.
        Anim, // 추가:Btn_Assist 클릭시 Btn_Assist의 Anim의 트리거 Anim_Str을 실행시킵니다.// Move_Pos추가시 이동가능 // Next_Step기능사용가능
        Next_Pick,  // 클릭시 다음단계로 이동할 수 있습니다.// Move_Pos추가시 이동가능 // 추가:Btn_Assist 클릭시 now_Step을 false, Next_Step을 True로 합니다.
        Step_Start,  // StepList의 Start Btn기능을 가져옵니다.
        Menu, // 추가:Btn_Assist 클릭시 메뉴를 켜거나, 끌 수 있습니다.
        MenuSelect, // 추가:Btn_Assist 메뉴를 선택해서 켤 수 있는 기능입니다.
        Tutorial_Start, // T_Mission를 통해 튜토리얼 씬을 시작할 수 있습니다.
        Tutorial_Next,  // T_Mission를 통해 튜토리얼의 다음버튼을 누를 수 있습니다.
        Tutorial_Count,  // T_Mission를 통해 튜토리얼의 카운트를 늘릴 수 있습니다.
        Scene_Move, // 추가:Btn_Assist Scene에 이름을 추가해서 씬을 이동할 수 있습니다.
    };
    public Case Btn_es;

    public float min_Size;
    public float Max_Size;

    public GameObject Error_UI;

    Animator Btn_Anim;
    ConfigurableJoint Btn_joint;
    public string Btn_Check;

    public string Step;
    public string Click;

    public GameObject FinUI;
    public TMP_Text Fin_txt;
    public List<string> Fin_String;

    private void Awake()
    {
        if (Type == Class.Btn)
        {
            Btn_joint = GetComponent<ConfigurableJoint>();
        }

        As = GetComponent<AudioSource>();
        As.playOnAwake = false;

        if (GameObject.Find("Step_Mgr") != null)
        { StepMgr = GameObject.Find("Step_Mgr").GetComponent<H_Step_Mgr>(); }

        if (Btn_es == Case.Calibration)
        {
            //Haptic_Menu Haptic = GetComponent<Haptic_Menu>();
            //Haptic.Calibration();
        }
    }

    private void Start()
    {
        if (Type == Class.Btn)
        {
            Click = "False";
            if (GetComponent<Animator>() != null) { Btn_Anim = GetComponent<Animator>(); }

            if (Btn_axis == Btn_Axis.X_Axis)
            { Btn_Check = "X_Axis"; }
            else if (Btn_axis == Btn_Axis.Y_Axis)
            { Btn_Check = "Y_Axis"; }
            else if (Btn_axis == Btn_Axis.Z_Axis)
            { Btn_Check = "Z_Axis"; }

            if (GameObject.Find("Pc_Player") != null)
            {
                Destroy(gameObject.GetComponent<ConfigurableJoint>());
                transform.GetChild(0).gameObject.AddComponent<Pc_Btn>();
                transform.GetChild(0).GetComponent<Pc_Btn>().BtnCtrl = this;
            }
        }
    }

    private void OnEnable()
    {
        Click = "False";
    }

    private void Update()
    {
        if(Type == Class.Btn)
        { Btn(); }
    }

    void Btn()
    {
        if (Btn_axis == Btn_Axis.X_Axis)
        {
            if (transform.localPosition.x > Max_Size)
            { transform.localPosition = new Vector3(min_Size, 0, 0); }

            if (transform.localPosition.x < min_Size)
            { transform.localPosition = new Vector3(min_Size, 0, 0); }

            if (transform.localPosition.x >= Max_Size && Step == "0")
            {
                // 버튼이 한번만 눌려지는 구간
                Btn_OneClick();
            }
            if (transform.localPosition.x >= Max_Size)
            {
                // 버튼을 계속 누른다.
                Btn_ContinueClick();
            }

            if (transform.localPosition.x < Max_Size && Step != "0")
            {
                // 버튼에서 떨어진다.
                Btn_Up();
            }
        }
        if (Btn_axis == Btn_Axis.Y_Axis)
        {
            if (transform.localPosition.y > Max_Size)
            { transform.localPosition = new Vector3(0, Max_Size, 0); }

            if (transform.localPosition.y < min_Size)
            { transform.localPosition = new Vector3(0, min_Size, 0); }

            if (transform.localPosition.y >= min_Size && transform.localPosition.y <= Max_Size)
            { transform.localPosition = new Vector3(0, transform.localPosition.y, 0); }

            if (transform.localPosition.y >= Max_Size && Step == "0")
            {
                // 버튼이 한번만 눌려지는 구간
                Btn_OneClick();
            }
            if (transform.localPosition.y >= Max_Size)
            {
                // 버튼을 계속 누른다.
                Btn_ContinueClick();
                if (Clicked != true) { Clicked = true; }
            }

            if (transform.localPosition.y < Max_Size && Step != "0")
            {
                // 버튼에서 떨어진다.
                Btn_Up();
                if (Clicked != false) { Clicked = false; }
            }
        }
        if (Btn_axis == Btn_Axis.Z_Axis)
        {
            if (transform.localPosition.z > Max_Size)
            { transform.localPosition = new Vector3(0, 0, Max_Size); }

            if (transform.localPosition.z < min_Size)
            { transform.localPosition = new Vector3(0, 0, min_Size); }

            if (transform.localPosition.z >= min_Size && transform.localPosition.z <= Max_Size)
            { transform.localPosition = new Vector3(0, 0, transform.localPosition.z); }

            if (transform.localPosition.z >= Max_Size && Step == "0")
            {
                // 버튼이 한번만 눌려지는 구간
                Btn_OneClick();
            }
            if (transform.localPosition.z >= Max_Size)
            {
                // 버튼을 계속 누른다.
                Btn_ContinueClick();
                if (Clicked != true) { Clicked = true; }
            }

            if (transform.localPosition.z < Max_Size && Step != "0")
            {
                // 버튼에서 떨어진다.
                Btn_Up();
                if (Clicked != false) { Clicked = false; }
            }
        }

        if (Step == "1")
        {
            if (Btn_Anim != null)
            {
                Btn_Anim.SetFloat("Anim_Off", 0);
                Btn_Anim.SetFloat("Anim_On", 1);
            }
        }
        else
        {
            if (Btn_Anim != null)
            {
                Btn_Anim.SetFloat("Anim_Off", 1);
                Btn_Anim.SetFloat("Anim_On", 0);
            }
        }
    }
    void Btn_OneClick()
    {
        As.clip = Btn_Click;
        As.Play();
        //Debug.Log(Btn_es + "1번만 눌렸다.");

        Function();
        Step = "1";
    }
    void Btn_ContinueClick()
    {
        //Debug.Log(Btn_es + "를 계속 누른다.");
        Step = "1";
    }
    void Btn_Up()
    {
        // 버튼이 떨어지는 구간

        //Debug.Log(Btn_es + "에서 떨어졌다.");
        Step = "0";
    }
    void MoveOn()
    {
        if (GetComponent<Btn_Assist>() != null)
        {
            Btn_Assist BtnAssist = GetComponent<Btn_Assist>();
            if (BtnAssist.Move_Pos != null)
            {
                StepMgr.Move_Pos = BtnAssist.Move_Pos;

                if (BtnAssist.Teleport == false) { StepMgr.Teleport = false; }
                else { StepMgr.Teleport = true; }

                StepMgr.Move_On = true;
            }
        }
    }
    void NextStep()
    {
        if(GetComponent<Btn_Assist>() != null)
        {
            Btn_Assist BtnAssist = GetComponent<Btn_Assist>();
            BtnAssist.NextStep();
        }
    }
    void Anim()
    {
        if (GetComponent<Btn_Assist>() != null)
        {
            Btn_Assist BtnAssist = GetComponent<Btn_Assist>();
            BtnAssist.Anim_Trigger();
        }
    }
    void FinUI_True()
    {
        if (FinUI != null && Fin_txt != null)
        {
            for(int i = 0; i <= (Fin_String.Count-1); i++)
            {
                if (Fin_txt.text != "") { Fin_txt.text = Fin_String[i]; }
                else { Fin_txt.text = Fin_txt.text + "\n" + Fin_String[i]; }
            }
            FinUI.SetActive(false);
            FinUI.SetActive(true);
        }
    }
    void Function()
    {
        if (Btn_es == Case.Calibration)
        {
            //Haptic_Menu Haptic = GetComponent<Haptic_Menu>();
            //Haptic.Calibration();
        }
        if (Btn_es == Case.Count01)
        {
            if (Click == "False")
            {
                StepMgr.Count01 = StepMgr.Count01 + 1;

                if (StepMgr.Count01 >= StepMgr.Count01_Lim)
                {
                    if (StepMgr.Count01_False.Count >= 1)
                    {
                        for (int i = 0; i <= (StepMgr.Count01_False.Count - 1); i++)
                        { StepMgr.Count01_False[i].SetActive(false); }
                    }

                    if (StepMgr.Count01_True.Count >= 1)
                    {
                        for (int i = 0; i <= (StepMgr.Count01_True.Count - 1); i++)
                        { StepMgr.Count01_True[i].SetActive(true); }
                    }
                }
                Click = "True";
            }
        }
        if (Btn_es == Case.Error)
        {
            StepMgr.ErrorTime = 3f;
            if (Error_UI != null)
            {
                Error_UI.SetActive(false);
                Error_UI.SetActive(true);
            }
            Anim();
        }
        if (Btn_es == Case.Next_Step)
        {
            MoveOn();
            NextStep();
            Anim();
        }
        if (Btn_es == Case.Anim)
        {
            MoveOn();
            Anim();
            NextStep();
        }
        if (Btn_es == Case.Next_Pick)
        {
            MoveOn();
            NextStep();
            //H_StepList StepList = GameObject.Find("Step_Mgr").GetComponent<H_StepList>();
            //StepList.Next_Pick();
        }
        if (Btn_es == Case.Step_Start)
        {
            //H_StepList StepList = GameObject.Find("Step_Mgr").GetComponent<H_StepList>();
            //StepList.Start_Btn();
        }
        if (Btn_es == Case.Menu)
        {
            Btn_Assist BtnAssist = GetComponent<Btn_Assist>();
            BtnAssist.Menu_Ctrl();
        }
        if (Btn_es == Case.MenuSelect)
        { }
        if (Btn_es == Case.Tutorial_Start)
        {
            //Tmission = GameObject.FindGameObjectWithTag("Tutorial").GetComponent<T_Mission>();
            //Tmission.StartBtn();
        }
        if (Btn_es == Case.Tutorial_Next)
        {
            //Tmission = GameObject.FindGameObjectWithTag("Tutorial").GetComponent<T_Mission>();
            //Tmission.Tutorial_Clear();
        }
        if (Btn_es == Case.Tutorial_Count)
        {
            //Tmission = GameObject.FindGameObjectWithTag("Tutorial").GetComponent<T_Mission>();
            //Tmission.Counting();
        }
        if (Btn_es == Case.Scene_Move)
        {
            if (GetComponent<Btn_Assist>() != null)
            {
                Btn_Assist BtnAssist = GetComponent<Btn_Assist>();
                SceneManager.LoadScene(BtnAssist.SceneName);
            }
        }
            FinUI_True();
    }

    private void OnTriggerEnter(Collider Coll)
    {
        if(Type == Class.Coll)
        {
            Function();
        }
    }

    private void OnMouseDown()
    {
        if (Type == Class.Coll)
        {
            Function();
        }
    }
}
