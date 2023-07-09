using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySideCollider : MonoBehaviour
{
    //public Direction side;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() == null)
        {
            if(collision.GetComponent<Coin>() == null)
            { 
                GetComponentInParent<Enemy>().ChangeDirection();
            }
        }
        else if(collision.GetComponent<Player>() != null)
        {
            collision.GetComponent<Player>().TakeHit(); //ToDO Implement TakeHit() in Player.cs
        }
    }
}
