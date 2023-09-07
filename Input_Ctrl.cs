using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Input_Ctrl : MonoBehaviour
{
    public string OwnerShip;

    public XRController R_Ctrl_er;
    public XRController L_Ctrl_er;

    public bool Menu_Btn;
    public bool Move_Touch;

    public bool Menu_On;
    public float Menu_time;
    public Vector2 Joystick_Touch;
    public Vector2 Joystick_Rot;

    public bool X_Btn;
    public float Tel_time;

    public Transform L_Base_Arm;
    public Transform L_Tel_Arm;
    public Transform R_Base_Arm;

    public Transform Vr_Menu_Pos;
    public Transform Vr_Main_UI;

    public Transform Target;
    public float Stop_Spd;
    public float speed = 0.5f; // 이동속도
    public float Rotspeed = 3f; // 회전속도

    public string Error;
    private void Awake()
    {
        Error = "False";
        OwnerShip = "True";
        Stop_Spd = 1f;
    }

    void Update()
    {
        if(Error == "True")
        {
            R_Ctrl_er.SendHapticImpulse(0.7f, 3f);
            L_Ctrl_er.SendHapticImpulse(0.7f, 3f);
            Error = "False";
        }

        // 메뉴버튼이 false일때                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
        if (Menu_On == false)
        {
            if (R_Ctrl_er.GetComponent<XRRayInteractor>().selectTarget != null
                && R_Ctrl_er.GetComponent<XRRayInteractor>().selectTarget.gameObject.GetComponent<Grab_Item>().Fix_Hand == true) {}
            else if (L_Ctrl_er.GetComponent<XRRayInteractor>().selectTarget != null
                && L_Ctrl_er.GetComponent<XRRayInteractor>().selectTarget.gameObject.GetComponent<Grab_Item>().Fix_Hand == true) {}
            else
            {                
                //메인카메라가 바라보는 방향입니다.
                Vector3 dir = Target.transform.localRotation * Vector3.forward;
                Vector3 dIr_R = Target.transform.localRotation * Vector3.right;
                if (OwnerShip == "True")
                {
                    //바라보는 시점 방향으로 이동합니다.
                    gameObject.transform.Translate(dir * Joystick_Touch.y * Stop_Spd * speed * Time.deltaTime);
                    gameObject.transform.Translate(dIr_R * Joystick_Touch.x * Stop_Spd * speed * Time.deltaTime);
                }
            }
        }

        if (Menu_On == true) { }

        if (L_Ctrl_er.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Joystick_Touch)) { }

        if (Joystick_Rot.x >= 0.7f) { Camera.main.transform.parent.GetChild(0).Rotate(Vector3.up * Rotspeed * Time.deltaTime); }
        if (Joystick_Rot.x <= -0.7f) { Camera.main.transform.parent.GetChild(0).Rotate(Vector3.down * Rotspeed * Time.deltaTime); }

        if (R_Ctrl_er.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Joystick_Rot)) { }

        // 왼쪽 메뉴 버튼을 클릭하였을때, Menu_Btn = True
        if (L_Ctrl_er.inputDevice.TryGetFeatureValue(CommonUsages.menuButton, out Menu_Btn))
        {
            if (Menu_time == 0)
            {
                if (Menu_Btn == true) { Menu_time = +0.1f; }
                else if (Menu_Btn == false && Menu_time != 0f)
                { Menu_time = 0.1f; }
            }
            else { Menu_time = 0.1f; }
        }
        if (Menu_time == 0.1f && Menu_Btn == false)
        {
            if (Menu_On == true)
            {
                Vr_Main_UI.gameObject.SetActive(false);
                Menu_On = false;
                Menu_time = 0;
            }
            else
            {
                Vr_Main_UI.gameObject.SetActive(true);
                Vr_Main_UI.position = Vr_Menu_Pos.position;
                Menu_On = true;
                Menu_time = 0;
            }
        }
    }
}