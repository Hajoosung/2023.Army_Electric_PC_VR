using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Pc_Btn : MonoBehaviour
{
    public Btn_Ctrl BtnCtrl;
    public PwBtn_Ctrl PwBtnCtrl;

    private void Awake()
    {
        gameObject.AddComponent<Rigidbody>();

        Rigidbody Rigid = GetComponent<Rigidbody>();
        Rigid.useGravity = false;
        Rigid.isKinematic = true;

        transform.localPosition = new Vector3(0, 0, 0);
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    private void Start()
    { Btn_Up(); }
    private void OnMouseDown()
    {
        if (BtnCtrl != null)
        {
            if (BtnCtrl.Btn_Check == "X_Axis")
            {
                BtnCtrl.transform.localPosition
                      = new Vector3(BtnCtrl.Max_Size, 0, 0);
            }
            if (BtnCtrl.Btn_Check == "Y_Axis")
            {
                BtnCtrl.transform.localPosition
                      = new Vector3(0, BtnCtrl.Max_Size, 0);
            }
            if (BtnCtrl.Btn_Check == "Z_Axis")
            {
                BtnCtrl.transform.localPosition
                      = new Vector3(0, 0, BtnCtrl.Max_Size);
            }

        }
        if (PwBtnCtrl != null)
        {
            if (PwBtnCtrl.Btn_Check == "X_Axis")
            {
                PwBtnCtrl.transform.localPosition
                      = new Vector3(PwBtnCtrl.Max_Size, 0, 0);
            }
            if (PwBtnCtrl.Btn_Check == "Y_Axis")
            {
                PwBtnCtrl.transform.localPosition
                      = new Vector3(0, PwBtnCtrl.Max_Size, 0);
            }
            if (PwBtnCtrl.Btn_Check == "Z_Axis")
            {
                PwBtnCtrl.transform.localPosition
                      = new Vector3(0, 0, PwBtnCtrl.Max_Size);
            }
        }

        Invoke("Btn_Up", 0.3f);
    }
    private void OnMouseUp()
    {
        Btn_Up();
    }

    void Btn_Up()
    {
        if (BtnCtrl != null)
        {
            if (BtnCtrl.Btn_Check == "X_Axis")
            {
                BtnCtrl.transform.localPosition
                      = new Vector3(0, 0, 0);
            }
            if (BtnCtrl.Btn_Check == "Y_Axis")
            {
                BtnCtrl.transform.localPosition
                      = new Vector3(0, 0, 0);
            }
            if (BtnCtrl.Btn_Check == "Z_Axis")
            {
                BtnCtrl.transform.localPosition
                      = new Vector3(0, 0, 0);
            }
        }

        if (PwBtnCtrl != null)
        {
            if (PwBtnCtrl.Btn_Check == "X_Axis")
            {
                PwBtnCtrl.transform.localPosition
                      = new Vector3(0, 0, 0);
            }
            if (PwBtnCtrl.Btn_Check == "Y_Axis")
            {
                PwBtnCtrl.transform.localPosition
                      = new Vector3(0, 0, 0);
            }
            if (PwBtnCtrl.Btn_Check == "Z_Axis")
            {
                PwBtnCtrl.transform.localPosition
                      = new Vector3(0, 0, 0);
            }
        }
    }
}
