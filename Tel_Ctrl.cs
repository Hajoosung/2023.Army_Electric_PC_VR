using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tel_Ctrl : MonoBehaviour
{
    Transform Player;
    public string Move;
    public List<Transform> Move_Pos;

    private void Start()
    {
        Move = "False";
    }

    public void Teleport(int i)
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (Move == "False")
        {
            Player.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            Player.position = Move_Pos[i].position;
            Player.eulerAngles = Move_Pos[i].eulerAngles;
            Player.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            Move = "True";
        }
    }
}
