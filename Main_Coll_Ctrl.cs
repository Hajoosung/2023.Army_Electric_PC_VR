using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class Main_Coll_Ctrl : MonoBehaviour
{
    // ������ ����� �پ��ִ� ������Ʈ
    public XRGrabInteractable Grab_Obj;
    // ������ ������ �ݶ��̴�
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

        // ���࿡ ������ �����ְ�, ���� �� �ֵ��� �ݶ��̴��� True���?
        if (Grab_Obj.isSelected == true && Grab_Coll.enabled == true)
        // ������ �ʵ��� �ݶ��̴��� False��ŵ�ϴ�.
        { Grab_Coll.enabled = false; }

        // ���࿡ ������ ������ �ʾҰ�, ������ �ʵ��� �ݶ��̴��� False���?
        if (Grab_Obj.isSelected == false && Grab_Coll.enabled == false)
        // �������� �ݶ��̴��� True��ŵ�ϴ�.
        { Grab_Coll.enabled = true; }
    }
}
