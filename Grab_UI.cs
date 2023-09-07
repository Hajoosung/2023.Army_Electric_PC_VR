using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Grab_UI : MonoBehaviour
{
    [Header("재질을 바꿀 오브젝트")]
    public List<MeshRenderer> Renderer;
    [Header("호버시 재질을 바꿀 버튼")]
    public List<Button> Btns;
    [Header("호버시 바뀔 재질")]
    public Material Hover_Mat;

    [Header("지정 X")]
    public List<Material> Base_Mat;
    private void Start()
    {
        for (int i = 0; i <= (Renderer.Count - 1); i++)
        { Base_Mat.Add(Renderer[i].material); }

        for (int i = 0; i <= (Btns.Count - 1); i++)
        {
            Btns[i].gameObject.AddComponent<Grab_HoverBtn>();
            Grab_HoverBtn Hover_Btn = Btns[i].GetComponent<Grab_HoverBtn>();

            Hover_Btn.Order = i;
            Hover_Btn.GrabUI = this;
        }
    }
}
