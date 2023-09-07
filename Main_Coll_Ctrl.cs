using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class Main_Coll_Ctrl : MonoBehaviour
{
    // 잡히는 기능이 붙어있는 오브젝트
    public XRGrabInteractable Grab_Obj;
    // 물건이 잡히는 콜라이더
    public Collider Grab_Coll;
    Transform Render_Tex;
    private void Start()
    {
        if (transform.name == "Phone") { Render_Tex = transform.Find("Obj_es").Find("Obj").Find("Render_Cam_Ctrl"); }
    }
    void Update()
    {
        if (transform.name == "Phone")
        {
            if (Grab_Obj.isSelected == true)
            { Render_Tex.gameObject.SetActive(true); }
            else
            { Render_Tex.gameObject.SetActive(false); }
        }

        // 만약에 물건이 잡혀있고, 잡힐 수 있도록 콜라이더가 True라면?
        if (Grab_Obj.isSelected == true && Grab_Coll.enabled == true)
        // 잡히지 않도록 콜라이더를 False시킵니다.
        { Grab_Coll.enabled = false; }

        // 만약에 물건이 잡히지 않았고, 잡히지 않도록 콜라이더가 False라면?
        if (Grab_Obj.isSelected == false && Grab_Coll.enabled == false)
        // 잡히도록 콜라이더를 True시킵니다.
        { Grab_Coll.enabled = true; }
    }
}
