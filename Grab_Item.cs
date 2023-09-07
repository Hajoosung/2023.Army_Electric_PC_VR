using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
//using EPOOutline;

public class Grab_Item : MonoBehaviour
{
    //Outlinable outline;

    public bool Fix_Hand;
    public bool Fix_Rot;
    public bool Trigger_On;

    public Transform Grab_Obj;
    public Transform Grab_Obj_02;
    public Finger_Anim_Ctrl Finger_Ctrl;

    public Transform Return_Pos;

    public string Item_Name;

    XRGrabInteractable Item;

    public Transform R_Hand_Pos;
    public Transform L_Hand_Pos;

    public Transform R_Fixed_Hand_Rot;
    public Transform L_Fixed_Hand_Rot;

    string Grab;
    public List<Collider> Coll;

    [Header("그랩시 등장UI")]
    public GameObject Grab_UI;

    void Start()
    {
        Grab = "False";

        //outline = GetComponent<Outlinable>();
        if (GetComponent<XRGrabInteractable>() != null) 
        { Item = GetComponent<XRGrabInteractable>(); }

        if (Item != null)
        {
            for(int i = 0; i <= (Item.colliders.Count - 1); i++)
            { Coll.Add(Item.colliders[i]); }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 이 물건이 쥐어졌을때, 
        if (Item.isSelected == true)
        {
            if (Item != null) { Finger_Ctrl = Item.selectingInteractor.GetComponent<Finger_Anim_Ctrl>(); }
            // 이물건을 쥔손의 스크립트값에 Item_Name을 지정해준다.
            Finger_Ctrl.Item_Name = Item_Name;
            Finger_Ctrl.grab_item = transform.GetComponent<Grab_Item>();

            if (Fix_Hand == true)
            {

                if (Finger_Ctrl.Hand_str == "R_hand")
                {
                    //Finger_Ctrl.Hand_Obj.SetParent(R_Hand_Pos);
                    //Finger_Ctrl.Hand_Obj.position = R_Hand_Pos.position;
                    if (Fix_Rot == true)
                    {
                        Finger_Ctrl.Hand_Obj.SetParent(R_Fixed_Hand_Rot);
                        Finger_Ctrl.Hand_Obj.localPosition = new Vector3(0, 0, 0);
                        Finger_Ctrl.Hand_Obj.localEulerAngles = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        Finger_Ctrl.Hand_Obj.SetParent(R_Hand_Pos);
                        Finger_Ctrl.Hand_Obj.localPosition = new Vector3(0, 0, 0);
                        Finger_Ctrl.Hand_Obj.localEulerAngles = new Vector3(0, 0, 0);
                    }
                }

                if (Finger_Ctrl.Hand_str == "L_hand")
                {
                    //Finger_Ctrl.Hand_Obj.SetParent(L_Hand_Pos);
                    //Finger_Ctrl.Hand_Obj.position = L_Hand_Pos.position;

                    if (Fix_Rot == true)
                    {
                        Finger_Ctrl.Hand_Obj.SetParent(L_Fixed_Hand_Rot);
                        Finger_Ctrl.Hand_Obj.localPosition = new Vector3(0, 0, 0);
                        Finger_Ctrl.Hand_Obj.localEulerAngles = new Vector3(0, 0, 0); 
                    }
                    else
                    {
                        Finger_Ctrl.Hand_Obj.SetParent(L_Hand_Pos);
                        Finger_Ctrl.Hand_Obj.localPosition = new Vector3(0, 0, 0);
                        Finger_Ctrl.Hand_Obj.localEulerAngles = new Vector3(0, 0, 0);
                    }
                }
            }
            else if (Fix_Hand == false)
            {
                if (Grab_Obj != null)
                {
                    if (GetComponent<XRGrabInteractable>().trackPosition == true
                        && GetComponent<XRGrabInteractable>().trackRotation == true
                        )
                    {
                        if (Finger_Ctrl.Hand_str == "R_hand")
                        {
                            Grab_Obj.position = R_Hand_Pos.position;
                            Grab_Obj.eulerAngles = R_Hand_Pos.eulerAngles;
                        }

                        if (Finger_Ctrl.Hand_str == "L_hand")
                        {
                            Grab_Obj.position = L_Hand_Pos.position;
                            Grab_Obj.eulerAngles = L_Hand_Pos.eulerAngles;
                        }
                    }
                    else
                    {
                        if (Return_Pos != null)
                        {
                            Grab_Obj.position = Return_Pos.position;
                            Grab_Obj.eulerAngles = Return_Pos.eulerAngles;
                        }
                    }

                }

                if (Grab_Obj_02 != null)
                {
                    if (GetComponent<XRGrabInteractable>().trackPosition == true
                      && GetComponent<XRGrabInteractable>().trackRotation == true
                         )
                    {
                        if (Finger_Ctrl.Hand_str == "R_hand")
                        {
                            Grab_Obj_02.position = R_Hand_Pos.position;
                            Grab_Obj_02.eulerAngles = R_Hand_Pos.eulerAngles;
                        }

                        if (Finger_Ctrl.Hand_str == "L_hand")
                        {
                            Grab_Obj_02.position = L_Hand_Pos.position;
                            Grab_Obj_02.eulerAngles = L_Hand_Pos.eulerAngles;
                        }
                    }
                    else
                    {
                        if (Return_Pos != null)
                        {
                            Grab_Obj_02.position = Return_Pos.position;
                            Grab_Obj_02.eulerAngles = Return_Pos.eulerAngles;
                        }
                    }
                }
            }

            if(Grab == "False")
            {
                for (int i = 0; i <= (Coll.Count - 1); i++)
                { Coll[i].isTrigger = true; }
                Grab = "True";
            }

            if (Grab_UI != null)
            {
                if (Grab_UI.activeSelf == false)
                { Grab_UI.SetActive(true); }
            }
            //outline.enabled = false;
        }
        if (Item.isSelected == false)
        {
            Finger_Ctrl = null;

            if (Grab_Obj != null && Return_Pos != null)
            {
                Grab_Obj.position = Return_Pos.position;
                Grab_Obj.eulerAngles = Return_Pos.eulerAngles;
            }

            if (Grab == "True")
            {
                for (int i = 0; i <= (Coll.Count - 1); i++)
                { Coll[i].isTrigger = false; }
                Grab = "False";
            }

            if (Grab_UI != null)
            {
                if (Grab_UI.activeSelf == true)
                { Grab_UI.SetActive(false); }
            }
            //if (Item.isHovered == true) { outline.enabled = true; }
            //else { outline.enabled = false; }
        }
    }

    public void Grab_On()
    {
        // 이 물건의 소유권을 이전한다.
        //transform.GetComponent<PhotonView>().RequestOwnership();
    }
}
