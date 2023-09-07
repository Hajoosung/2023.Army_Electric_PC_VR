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
        Calibration, // �߰�: Haptic_Menu Ŭ���� ��ƽ���� ���¹� ���ΰ�ħ?
        Count01, // Ŭ���� StepMgr�� Count�� ������.(�ߺ�Ŭ���� �ȵȴ�.)
        Next_Step, //�߰�:Btn_Assist Ŭ���� now_Step�� false, Next_Step�� True�� �մϴ�.// Move_Pos�߰��� �̵����� // Anim�߰��� Anim ���
        Error, // Ŭ���� ����, Error_UI�� UI�߰�����, �߰�: Btn_Assist�� Anim����� ������ �� �ֽ��ϴ�.
        Anim, // �߰�:Btn_Assist Ŭ���� Btn_Assist�� Anim�� Ʈ���� Anim_Str�� �����ŵ�ϴ�.// Move_Pos�߰��� �̵����� // Next_Step��ɻ�밡��
        Next_Pick,  // Ŭ���� �����ܰ�� �̵��� �� �ֽ��ϴ�.// Move_Pos�߰��� �̵����� // �߰�:Btn_Assist Ŭ���� now_Step�� false, Next_Step�� True�� �մϴ�.
        Step_Start,  // StepList�� Start Btn����� �����ɴϴ�.
        Menu, // �߰�:Btn_Assist Ŭ���� �޴��� �Ѱų�, �� �� �ֽ��ϴ�.
        MenuSelect, // �߰�:Btn_Assist �޴��� �����ؼ� �� �� �ִ� ����Դϴ�.
        Tutorial_Start, // T_Mission�� ���� Ʃ�丮�� ���� ������ �� �ֽ��ϴ�.
        Tutorial_Next,  // T_Mission�� ���� Ʃ�丮���� ������ư�� ���� �� �ֽ��ϴ�.
        Tutorial_Count,  // T_Mission�� ���� Ʃ�丮���� ī��Ʈ�� �ø� �� �ֽ��ϴ�.
        Scene_Move, // �߰�:Btn_Assist Scene�� �̸��� �߰��ؼ� ���� �̵��� �� �ֽ��ϴ�.
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
                // ��ư�� �ѹ��� �������� ����
                Btn_OneClick();
            }
            if (transform.localPosition.x >= Max_Size)
            {
                // ��ư�� ��� ������.
                Btn_ContinueClick();
            }

            if (transform.localPosition.x < Max_Size && Step != "0")
            {
                // ��ư���� ��������.
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
                // ��ư�� �ѹ��� �������� ����
                Btn_OneClick();
            }
            if (transform.localPosition.y >= Max_Size)
            {
                // ��ư�� ��� ������.
                Btn_ContinueClick();
                if (Clicked != true) { Clicked = true; }
            }

            if (transform.localPosition.y < Max_Size && Step != "0")
            {
                // ��ư���� ��������.
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
                // ��ư�� �ѹ��� �������� ����
                Btn_OneClick();
            }
            if (transform.localPosition.z >= Max_Size)
            {
                // ��ư�� ��� ������.
                Btn_ContinueClick();
                if (Clicked != true) { Clicked = true; }
            }

            if (transform.localPosition.z < Max_Size && Step != "0")
            {
                // ��ư���� ��������.
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
        //Debug.Log(Btn_es + "1���� ���ȴ�.");

        Function();
        Step = "1";
    }
    void Btn_ContinueClick()
    {
        //Debug.Log(Btn_es + "�� ��� ������.");
        Step = "1";
    }
    void Btn_Up()
    {
        // ��ư�� �������� ����

        //Debug.Log(Btn_es + "���� ��������.");
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
