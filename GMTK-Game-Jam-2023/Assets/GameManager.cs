using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject GameOverScreen;

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

        GetComponent<AudioManager>().StartMusic();
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
    }
}
