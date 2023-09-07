using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class TimeLine_Events : MonoBehaviour
{
    Transform Player;
    public string Platform;
    public bool Is_Playing;

    [Header("Ÿ�Ӷ���")]
    public PlayableDirector TimeLine;
    public float Time;

    [Header("ī�޶� ����ٴϱ�")]
    public float Spd;
    public bool Cam_Follow;
    public Transform Cam_Pos;
    public Transform Fin_Pos;
    [Header("Vr Ű����")]
    public float Height;
    [Header("Vr�̶� PcŰ�� �޶�� �Ҷ�")]
    public bool Height_Diff;
    [Header("Pc Ű����")]
    public float Pc_Height;

    void Start()
    {
        Is_Playing = false;

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (Player.name.Contains("Pc"))
        { Platform = "Pc"; }
        else
        { Platform = "Vr"; }
    }

    void Update()
    {
        if (Cam_Follow == true)
        {
            Time = ((float)(TimeLine.time / TimeLine.duration));

            // Ÿ�Ӷ����� ������� 98%������ ��, 
            if (Time <= 0.98f)
            {
                if(Is_Playing == false) { Is_Playing = true; }

                if(Player.GetComponent<NavMeshAgent>().enabled != false)
                { Player.GetComponent<NavMeshAgent>().enabled = false; }

                if (Platform == "Pc")
                {
                    Pc_Input_Ctrl Input = Player.GetComponent<Pc_Input_Ctrl>();
                    if (Input.OwnerShip != "False") { Input.OwnerShip = "False"; }
                }

                if (Platform == "Vr")
                {
                    Input_Ctrl Input = Player.GetComponent<Input_Ctrl>();
                    if (Input.OwnerShip != "False") { Input.OwnerShip = "False"; }
                }

                if (Height_Diff == true && Platform == "Pc")
                {
                    Vector3 MovePos = new Vector3(Cam_Pos.position.x, Cam_Pos.position.y - Pc_Height, Cam_Pos.position.z);
                    Vector3 MoveRot = new Vector3(Player.eulerAngles.x, Cam_Pos.eulerAngles.y, Player.eulerAngles.z);

                    Player.position = Vector3.Lerp(Player.position, MovePos, Spd);
                    Player.eulerAngles = Vector3.Lerp(Player.eulerAngles, MoveRot, Spd);
                }
                else
                {
                    Vector3 MovePos = new Vector3(Cam_Pos.position.x, Cam_Pos.position.y - Height, Cam_Pos.position.z);
                    Vector3 MoveRot = new Vector3(Player.eulerAngles.x, Cam_Pos.eulerAngles.y, Player.eulerAngles.z);

                    Player.position = Vector3.Lerp(Player.position, MovePos, Spd);
                    Player.eulerAngles = Vector3.Lerp(Player.eulerAngles, MoveRot, Spd);
                }
            }
            // Ÿ�Ӷ����� ������� 98%�̻��� ��,
            else if (Time > 0.98f)
            {
                if (Is_Playing == true)
                {
                    if (Fin_Pos != null)
                    {
                        Player.position = Fin_Pos.position;
                        Player.eulerAngles = Fin_Pos.eulerAngles;
                    }

                    if (Platform == "Pc")
                    {
                        Pc_Input_Ctrl Input = Player.GetComponent<Pc_Input_Ctrl>();
                        if (Input.OwnerShip != "True") { Input.OwnerShip = "True"; }
                    }

                    if (Platform == "Vr")
                    {
                        Input_Ctrl Input = Player.GetComponent<Input_Ctrl>();
                        if (Input.OwnerShip != "True") { Input.OwnerShip = "True"; }
                    }

                    Is_Playing = false;
                }
                else
                {
                    if (Player.GetComponent<NavMeshAgent>().enabled != true)
                    { Player.GetComponent<NavMeshAgent>().enabled = true; }
                }
            }
        }
    }
}
