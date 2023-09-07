using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Help : MonoBehaviour
{
    public int Step;

    public Button Prev_Btn;
    public Button Next_Btn;

    public Image Help_Img;
    public Sprite[] Help_Spr;

    // Start is called before the first frame update
    void Start()
    {
        Step = 0;

        Prev_Btn.onClick.AddListener(Prev_Step);
        Next_Btn.onClick.AddListener(Next_Step);
    }

    // Update is called once per frame
    void Update()
    {

        if (Step <= 0)
        {
            if (Prev_Btn.gameObject.activeSelf == true)
            { Prev_Btn.gameObject.SetActive(false); }
        }
        else
        {
            if (Prev_Btn.gameObject.activeSelf == false)
            { Prev_Btn.gameObject.SetActive(true); }
        }

        if (Step >= (Help_Spr.Length-1))
        {
            if (Next_Btn.gameObject.activeSelf == true)
            { Next_Btn.gameObject.SetActive(false); }
        }
        else
        {
            if (Next_Btn.gameObject.activeSelf == false)
            { Next_Btn.gameObject.SetActive(true); }
        }

        if (Step <= (Help_Spr.Length - 1))
        {
            if (Help_Img.sprite != Help_Spr[Step])
            { Help_Img.sprite = Help_Spr[Step]; }
        }
    }
    void Next_Step() { Step = Step + 1; }
    void Prev_Step() { Step = Step - 1; }
}
