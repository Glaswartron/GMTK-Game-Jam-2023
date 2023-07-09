using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void Retry()
    {
        SceneFader.instance.SwitchToScene(SceneManager.GetActiveScene().name, 2f);
    }
    public void Mainmenu()
    {
        SceneFader.instance.SwitchToScene("MainMenu", 2f);
    }
}
