using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gripped_Door : MonoBehaviour
{
    public enum Axis {x, y, z};
    public Axis Axis_check;

    Grab_Item Grab;

    public Transform Door;

    public float min_Size;
    public float max_Size;
    void Start()
    {
        Grab = GetComponent<Grab_Item>();
    }

    // Update is called once per frame
    void Update()
    {

            if (Axis_check == Axis.x)
            {
                if (Door.position.x < min_Size) { Door.position = new Vector3(min_Size, Door.position.y, Door.position.z); }
                else if (Door.position.x > max_Size) { Door.position = new Vector3(max_Size, Door.position.y, Door.position.z); }
                else if (Door.position.x >= min_Size && Door.position.x <= max_Size)
                {
                    if (Grab.Finger_Ctrl != null)
                    {
                        if (Grab.Finger_Ctrl.transform.position.x >= min_Size && Grab.Finger_Ctrl.transform.position.x <= max_Size)
                        { Door.position = new Vector3(Grab.Finger_Ctrl.transform.position.x, Door.position.y, Door.position.z); }
                    }
                }
            }


            if (Axis_check == Axis.y)
            {
                if (Door.position.y < min_Size) { Door.position = new Vector3(Door.position.x, min_Size, Door.position.z); }
                else if (Door.position.y > max_Size) { Door.position = new Vector3(Door.position.x, max_Size, Door.position.z); }
                else if (Door.position.y >= min_Size && Door.position.y <= max_Size)
                {
                    if (Grab.Finger_Ctrl != null)
                    {
                        if (Grab.Finger_Ctrl.transform.position.y >= min_Size && Grab.Finger_Ctrl.transform.position.y <= max_Size)
                        {
                            Door.position = new Vector3(Door.position.x, Grab.Finger_Ctrl.transform.position.y, Door.position.z);
                        }
                    }
                }
            }


            if (Axis_check == Axis.z)
            {
                if (Door.position.z < min_Size) { Door.position = new Vector3(Door.position.x, Door.position.y, min_Size); }
                else if (Door.position.z > max_Size) { Door.position = new Vector3(Door.position.x, Door.position.y, max_Size); }
                else if (Door.position.z >= min_Size && Door.position.z <= max_Size)
                {
                    if (Grab.Finger_Ctrl != null)
                    {
                        if (Grab.Finger_Ctrl.transform.position.z >= min_Size && Grab.Finger_Ctrl.transform.position.z <= max_Size)
                        {
                            Door.position = new Vector3(Door.position.x, Door.position.y, Grab.Finger_Ctrl.transform.position.z);
                        }
                    }
                }
            }
    }
}
