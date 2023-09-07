using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Grab_HoverBtn : MonoBehaviour
{
    public int Order;
    Button Btn;
    EventTrigger Evnt_trig;
    public Grab_UI GrabUI;

    public int Step;

    // Start is called before the first frame update
    void Start()
    {
        Step = 0;
        Btn = GetComponent<Button>();
        Btn.gameObject.AddComponent<EventTrigger>();
    }

    void Hover_Event()
    {
        // 지정된 호버재질로 변경
        GrabUI.Renderer[Order].material = GrabUI.Hover_Mat;
    }
    void Leave_Event() 
    {
        GrabUI.Renderer[Order].material = GrabUI.Base_Mat[Order];
    }

    private void Update()
    {
        if(Step == 0)
        {
            EventTrigger.Entry Enter = new EventTrigger.Entry();
            Enter.eventID = EventTriggerType.PointerEnter;
            Enter.callback.AddListener((eventData) => { Hover_Event(); });

            EventTrigger.Entry Leave = new EventTrigger.Entry();
            Leave.eventID = EventTriggerType.PointerExit;
            Leave.callback.AddListener((eventData) => { Leave_Event(); });

            Btn.GetComponent<EventTrigger>().triggers.Add(Enter);
            Btn.GetComponent<EventTrigger>().triggers.Add(Leave);

            Step = 1;
        }
    }
}
