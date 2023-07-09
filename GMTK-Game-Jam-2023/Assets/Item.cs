using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
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
            GetComponent<Animator>().Play("ItemDissolve");

            ApplyEffect(collision.GetComponent<Player>());

            GameManager.instance.audioManager.Play("ItemOrVictory");
        }
    }

    public abstract void ApplyEffect(Player player);

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
