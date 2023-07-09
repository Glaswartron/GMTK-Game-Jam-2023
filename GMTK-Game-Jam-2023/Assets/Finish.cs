using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public string nextLevelSceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Locked = true;

            GameManager.instance.audioManager.Play("ItemOrVictory");

            SceneFader.instance.SwitchToScene(nextLevelSceneName, 2f);
        }
    }
}
