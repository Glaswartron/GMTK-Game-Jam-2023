using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter instance;
    public int coins;
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        if(instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void CountCoinsUp()
    {
        coins++;
        UIMaster.instance.SetCoinText(coins.ToString());
    }

}
