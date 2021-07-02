using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{

    // Update is called once per frame
    public void LoadMainMenu()
    {
        GameManager.instance.ChangeScene(0);
    }

    public void LoadVS()
    {
        GameManager.instance.ChangeScene(3);
    }

    public void LoadSinglePlayer()
    {
        GameManager.instance.ChangeScene(1);
    }

    public void LoadFinalScene()
    {
        GameManager.instance.ChangeScene(2);
    }

    public void RestartLevel()
    {
        GameManager.instance.RestartLevel();
    }
}
