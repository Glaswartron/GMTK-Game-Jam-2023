using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool collected = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null && !collected)
        {
            CoinCounter.instance.CountCoinsUp();
            collected = true;
            GetComponent<Animator>().Play("CoinDissolve");
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Reactivate()
    {
        gameObject.SetActive(true);
        collected = false;
    }


}
