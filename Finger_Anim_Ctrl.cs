using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
//using Photon.Pun;

public class Finger_Anim_Ctrl : MonoBehaviour
{
    public Transform Player;
    public Transform Origin_Pos;
    public Vector3 Base_Rot;

    public enum Hand { R_hand, L_hand };
    public Hand Hand_check;
    public string Hand_str;

    XRController Hand_Ctrl;

    public Transform Hand_Obj;
    public Animator Anim;

    public Vector2 Sel_Thumb;
    public Vector2 Thumb;

    public float Thumb_X;
    public float Thumb_Y;
    public float Trigger;
    public float Grip;
    public float Over_Btn;
    public float Under_Btn;
    public float Menu_Btn;

    public bool Is_Selected;
    public bool Is_Grabed;

    public GameObject Selected_Item;

    public string Item_Name;
    public Grab_Item grab_item;

    float Base_LazerDis;

    private void Start()
    {
        // 해당 스크립트가 위치한 손, 양손을 컨트롤 하는 부분
        Hand_Ctrl = GetComponent<XRController>();
        Base_LazerDis = Hand_Ctrl.GetComponent<XRRayInteractor>().maxRaycastDistance;

        if (Hand_check == Hand.L_hand) { Hand_str = "L_hand"; }
        if (Hand_check == Hand.R_hand) { Hand_str = "R_hand"; }
    }
    // Update is called once per frame
    void Update()
    {
        // 물건을 쥐었을때, true / 아닐때 false를 가져온다.
        Is_Selected = GetComponent<XRRayInteractor>().isSelectActive;
        if (GetComponent<XRRayInteractor>().selectTarget != null)
        { 
            Selected_Item = GetComponent<XRRayInteractor>().selectTarget.gameObject;
            Is_Grabed = true;
        }
        else 
        {
            Selected_Item = null;
            Is_Grabed = false;
        }

        // Grip 버튼의 수치를 가져온다.
        if (Hand_Ctrl.inputDevice.TryGetFeatureValue(CommonUsages.grip, out Grip)) { }
        // 트리거 버튼의 수치를 가져온다.
        if (Hand_Ctrl.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out Trigger)) { }
        // (A, B / X, Y)윗 버튼의 수치를 가져온다.
        if (Hand_Ctrl.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool Over)) { }
        // (A, B / X, Y)아래 버튼의 수치를 가져온다.
        if (Hand_Ctrl.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool Under)) { }

        if (Over == true) { Over_Btn = 1f; }
        else { Over_Btn = 0f; }

        if (Under == true) { Under_Btn = 1f; }
        else { Under_Btn = 0f; }

        if (Hand_check == Hand.L_hand)
        {
            if (Is_Selected == true)
            {
                if(GetComponent<XRRayInteractor>().maxRaycastDistance > 0)
                { GetComponent<XRRayInteractor>().maxRaycastDistance = 0f; }
                In_Hand_Obj();
            }

            if (Is_Selected == false)
            {
                if (GetComponent<XRRayInteractor>().maxRaycastDistance <= 0)
                { GetComponent<XRRayInteractor>().maxRaycastDistance = Base_LazerDis; }
                Out_Hand_Obj();
            }
        }

        if (Hand_check == Hand.R_hand )
        {
            if (Is_Selected == true && GetComponent<XRRayInteractor>().selectTarget != null)
            {
                if (GetComponent<XRRayInteractor>().maxRaycastDistance > 0)
                { GetComponent<XRRayInteractor>().maxRaycastDistance = 0f; }
                In_Hand_Obj();
            }

            if ( GetComponent<XRRayInteractor>().selectTarget == null)
            {
                if (GetComponent<XRRayInteractor>().maxRaycastDistance <= 0)
                { GetComponent<XRRayInteractor>().maxRaycastDistance = Base_LazerDis; }
                Out_Hand_Obj();
            }
        }

        //왼손일때, 
        if (Hand_check == Hand.L_hand)
        {
            //왼손의 메뉴버튼을 눌렀을떄,
            if (Hand_Ctrl.inputDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool Menu)) { }
            // 메뉴버튼이 눌리면 신호를 On/Off를 float으로 표현
            if (Menu == true) { Menu_Btn = 1f; }
            else { Menu_Btn = 0f; }
        }
    }

    void In_Hand_Obj()
    {
        if (Hand_check == Hand.R_hand && Player.GetComponent<DeviceBasedSnapTurnProvider>().enabled == true)
        { Player.GetComponent<DeviceBasedSnapTurnProvider>().enabled = false; }

        if (Hand_Ctrl.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Sel_Thumb)) { }

        if (grab_item != null && grab_item.Finger_Ctrl == null)
        { grab_item.Finger_Ctrl = GetComponent<Finger_Anim_Ctrl>(); }

        // 기본상태에서 쥐었던 상태의 애니메이션의 블랜드를 0로 잡아준다.
        Anim.SetFloat("Trigger", 0);
        Anim.SetFloat("Grip", 0);
        Anim.SetFloat("Thumb_R", 0);
        Anim.SetFloat("Thumb_L", 0);
        Anim.SetFloat("Thumb_Up", 0);
        Anim.SetFloat("Thumb_Down", 0);
        Anim.SetFloat("Over_Btn", 0);
        Anim.SetFloat("Under_Btn", 0);


        // 각 아이템 Name을 받아, 애니메이션으로 변환한다.

        if (Item_Name == "Phone_Roller") { Anim.SetFloat("Phone_Roller", 1); }

        if (Item_Name == "L.P_Trig")
        {
            if (Over_Btn <= 0.25) 
            {
                Anim.SetFloat("Leakage Clamp", 1);
                Anim.SetFloat("L.P_Trig", 0);
            }
            else
            {
                Anim.SetFloat("Leakage Clamp", 0);
                Anim.SetFloat("L.P_Trig", Over_Btn);
            }
        }
        if (Item_Name == "Earth Tester")
        { Anim.SetFloat("Earth Tester", 1); }

        if(Item_Name == "Door_Handle")
        { Anim.SetFloat("Door_Handle", 1); }

        if (Item_Name == "Door_2nd")
        { Anim.SetFloat("Door_2nd", 1); }
    }
    void Out_Hand_Obj()
    {
        if (grab_item != null) { grab_item = null; }

        if (GetComponent<XRInteractorLineVisual>().enabled == false)
        { GetComponent<XRInteractorLineVisual>().enabled = true; }

        if (Hand_Obj.transform.parent != Origin_Pos)
        {
            Hand_Obj.transform.SetParent(Origin_Pos);
        }
        else
        {
            Hand_Obj.transform.localPosition = new Vector3(0, 0, 0);
            Hand_Obj.transform.localEulerAngles = Base_Rot;
        }

        // 만약 조이스틱의 버튼을 눌렀을때,
        if (Hand_Ctrl.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Thumb)) { }
        // 조이스틱의 Vector2를 수치로 변환한다.
        Thumb_X = Thumb.x;
        Thumb_Y = Thumb.y;

        Anim.SetFloat("Trigger", Trigger);
        Anim.SetFloat("Grip", Grip);

        // Vector2가 변환된 수치를 애니메이션에 반영시킨다.
        Anim.SetFloat("Thumb_R", Thumb_X);
        Anim.SetFloat("Thumb_L", -Thumb_X);
        Anim.SetFloat("Thumb_Up", Thumb_Y);
        Anim.SetFloat("Thumb_Down", -Thumb_Y);

        Anim.SetFloat("Over_Btn", Over_Btn);
        Anim.SetFloat("Under_Btn", Under_Btn);

        // 쥐었던 물건의 애니메이션의 블랜드를 0로 잡아준다.
        Anim.SetFloat("Phone_Roller", 0);

        Anim.SetFloat("Leakage Clamp", 0);
        Anim.SetFloat("L.P_Trig", 0);
        Anim.SetFloat("Earth Tester", 0);
        Anim.SetFloat("Door_Handle", 0);
        Anim.SetFloat("Door_2nd", 0);
    }

    public void Origin_Hand()
    {
        Hand_Obj.SetParent(Origin_Pos);
        Hand_Obj.transform.localPosition = new Vector3(0, 0, 0);
        Hand_Obj.transform.localEulerAngles = Base_Rot;
    }
}
