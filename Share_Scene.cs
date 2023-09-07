using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Share_Scene : MonoBehaviour
{
    public List<string> SceneList;
    private void Awake()
    {
        Load_Shared();
    }

    void Load_Shared()
    {
        for(int i = 0; i <= (SceneList.Count-1); i++)
        { SceneManager.LoadSceneAsync(SceneList[i], LoadSceneMode.Additive); }
    }
}
