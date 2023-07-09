using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject GameOverScreen;

    [HideInInspector] public AudioManager audioManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }

        audioManager = GetComponent<AudioManager>();

        if (SceneManager.GetActiveScene().name != "Level 1")
            audioManager.StartMusic("OverworldLevels");
        else
            audioManager.StartMusic("CastleLevel");
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);

        if (SceneManager.GetActiveScene().name != "Level 1")
            audioManager.StopMusic("OverworldLevels");
        else
            audioManager.StopMusic("CastleLevel");

        audioManager.Play("DeathSound");
    }
}
