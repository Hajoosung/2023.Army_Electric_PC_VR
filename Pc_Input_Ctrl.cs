using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Pc_Input_Ctrl : MonoBehaviour
{
    public string OwnerShip;

    public Slider slider;

    public Transform Y_Axis;
    public float Spd;
    public float Rot_Spd;
    public float Y_Rot_Spd;

    public float Mouse_Y;

    public Transform Menu;
    public bool Stop_On;
    void Awake()
    {
        OwnerShip = "True";
        Menu.gameObject.SetActive(false); 
    }

    void Update()
    {
        // Ctrl ºÎºÐ
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        if (OwnerShip == "True")
        {
            transform.Translate(Vector3.right * Spd * Horizontal * Time.deltaTime);
            transform.Translate(Vector3.forward * Spd * Vertical * Time.deltaTime);
        }

        float Mouse_X = Input.GetAxis("Mouse X");
        float Mouse_y = Input.GetAxis("Mouse Y");

        if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0 ||
            Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        { Stop_Check(); }

        //Mouse_Y = Mathf.Clamp(Mouse_Y, -55, 55);
        //=================================================================
        Vector3 Cha_Angle = Y_Axis.localEulerAngles;
        Cha_Angle.x = (Cha_Angle.x > 180) ? Cha_Angle.x - 360 : Cha_Angle.x;
        Cha_Angle.x = Mathf.Clamp(Cha_Angle.x, -60, 60);

        Y_Axis.localEulerAngles = new Vector3(Cha_Angle.x,0,0);
        //=================================================================


        //if (!slider.IsInteractable()) 
        {
            if (Input.GetMouseButton(0))
            {
                Stop_Check();
                if (OwnerShip == "True")
                {
                    transform.Rotate(Vector3.up * Rot_Spd * Mouse_X * Time.deltaTime);
                    Y_Axis.Rotate(Vector3.right * Rot_Spd * -Mouse_y * Time.deltaTime);
                }
            }
        }
        Mouse_Y += Input.GetAxis("Mouse Y") * Y_Rot_Spd;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Menu.gameObject.activeSelf == true) { Menu.gameObject.SetActive(false); }
            else { Menu.gameObject.SetActive(true); }
        }

        if(Input.anyKeyDown)
        { Stop_Check(); }
    }

    void Stop_Check()
    { if(Stop_On == true) { Stop_On = false; } }
}
