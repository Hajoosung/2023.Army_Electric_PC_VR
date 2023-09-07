using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Ctrl : MonoBehaviour
{
    public void Intro()
    { SceneManager.LoadScene("1 Intro"); }
    public void Help()
    { SceneManager.LoadScene("2 Tutorial"); }
    public void Index()
    { SceneManager.LoadScene("3 Index"); }
    public void Index1()
    { SceneManager.LoadScene("3 Index1"); }
    public void Main1_1()
    { TestLoadingScene.LoadScene("4 Main1.1"); }
    public void Main1_2()
    { TestLoadingScene.LoadScene("4 Main1.2"); }
    public void Main1_3()
    { TestLoadingScene.LoadScene("4 Main1.3"); }
    public void Main1_4()
    { TestLoadingScene.LoadScene("4 Main1.4"); }
    public void Go_Scene(string str)
    { SceneManager.LoadScene(str); }

    public void PassWord()
    {
        SceneManager.LoadScene("0502 PassWord UI");
    }
}

