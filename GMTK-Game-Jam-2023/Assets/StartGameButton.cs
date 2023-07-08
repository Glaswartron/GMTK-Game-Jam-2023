using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public string gameScene;
    public void StartGame()
    {
        SceneFader.instance.SwitchToScene(gameScene, 2);
    }
}
