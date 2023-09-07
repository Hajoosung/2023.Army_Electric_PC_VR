using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class Part_Ctrl : MonoBehaviour
{
    Transform Base_Parent;
    Vector3 Base_Pos;
    Vector3 Base_Rot;

    public XRGrabInteractable Main_Grab;
    public XRGrabInteractable Part_Grab;

    Transform Part;
    public Transform Part_target;
    // Start is called before the first frame update
    void Start()
    {
        Part = GetComponent<Transform>();

        Base_Parent = transform.parent;
        Base_Pos = transform.localPosition;
        Base_Rot = transform.localEulerAngles;
    }

    void Update()
    {
        if (Part_Grab.isSelected == true)
        {
            Part.position = Part_target.position;
            Part.rotation = Part_target.rotation;
        }
        else
        {
            if( transform.parent != Base_Parent)
            {
                Part.SetParent(Base_Parent);
                transform.localPosition = Base_Pos;

                Part.rotation = Part_target.rotation;
            }
            
        }

        if (Main_Grab.isSelected == true && Part_Grab.enabled == false)
        { Part_Grab.enabled = true; }

        else if (Main_Grab.isSelected == false && Part_Grab.enabled == true)
        { Part_Grab.enabled = false; }
    }
}
