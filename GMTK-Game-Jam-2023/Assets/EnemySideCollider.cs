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
            GetComponentInParent<Enemy>().ChangeDirection();
        }
    }
}
