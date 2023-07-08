using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            Debug.Log("WAAAAAAA");
            GetComponentInParent<Enemy>().TakeHit();
        }
    }

}
