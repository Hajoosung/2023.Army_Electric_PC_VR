using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Grab_UI : MonoBehaviour
{
    [Header("������ �ٲ� ������Ʈ")]
    public List<MeshRenderer> Renderer;
    [Header("ȣ���� ������ �ٲ� ��ư")]
    public List<Button> Btns;
    [Header("ȣ���� �ٲ� ����")]
    public Material Hover_Mat;

    [Header("���� X")]
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
